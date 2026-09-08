Option Strict On
Imports System.Net
Imports System.Net.Mail
Imports System.Windows.Forms
Imports SphereERP.Data
Imports SphereERP.Models

Namespace Services

    ''' <summary>
    ''' Periodically polls the ETA portal for status changes on locally-tracked
    ''' "Submitted" documents, logs validation/issuance/rejection/cancellation events,
    ''' and surfaces them to the user via Windows balloon-tip notifications and,
    ''' optionally, e-mail - configurable from Settings.
    ''' </summary>
    Public Class NotificationService
        Implements IDisposable

        Private ReadOnly _settings As AppSettings
        Private ReadOnly _submissionService As DocumentSubmissionService
        Private ReadOnly _docRepo As New DocumentRepository()
        Private ReadOnly _notifRepo As New NotificationRepository()
        Private ReadOnly _logger As New AppLogRepository()
        Private ReadOnly _timer As System.Windows.Forms.Timer
        Private ReadOnly _trayIcon As NotifyIcon

        Public Event NewNotification(eventType As String, message As String)

        Public Sub New(settings As AppSettings, submissionService As DocumentSubmissionService, Optional trayIcon As NotifyIcon = Nothing)
            _settings = settings
            _submissionService = submissionService
            _trayIcon = trayIcon

            _timer = New System.Windows.Forms.Timer()
            AddHandler _timer.Tick, AddressOf OnTimerTick
        End Sub

        ''' <summary>Starts background polling at the interval configured in Settings.</summary>
        Public Sub Start()
            Dim minutes = Math.Max(1, _settings.NotificationPollingMinutes)
            _timer.Interval = minutes * 60 * 1000
            _timer.Start()
        End Sub

        Public Sub [Stop]()
            _timer.Stop()
        End Sub

        ''' <summary>Restarts the timer, e.g. after the polling interval setting changes.</summary>
        Public Sub RestartWithCurrentSettings()
            [Stop]()
            Start()
        End Sub

        Private Async Sub OnTimerTick(sender As Object, e As EventArgs)
            Await PollOnceAsync().ConfigureAwait(False)
        End Sub

        ''' <summary>Performs a single polling pass: checks status of all "Submitted" documents.</summary>
        Public Async Function PollOnceAsync() As Task
            Try
                Dim pending = _docRepo.Search(status:="Submitted")
                For Each doc In pending
                    If String.IsNullOrWhiteSpace(doc.Uuid) Then Continue For

                    Dim newStatus = Await _submissionService.RefreshDocumentStatusAsync(doc.Id, doc.Uuid, doc.InternalId).ConfigureAwait(False)

                    If Not String.Equals(newStatus, "Submitted", StringComparison.OrdinalIgnoreCase) AndAlso
                       Not String.Equals(newStatus, "Unknown", StringComparison.OrdinalIgnoreCase) Then
                        RaiseNotificationToUser(doc.InternalId, newStatus)
                    End If
                Next
            Catch ex As Exception
                _logger.Warning("NotificationService.PollOnceAsync", ex.Message)
            End Try
        End Function

        ''' <summary>Surfaces a status-change event to the user per configured channels (toast/email).</summary>
        Private Sub RaiseNotificationToUser(internalId As String, newStatus As String)
            Dim title = $"Document {internalId}: {newStatus}"
            Dim shouldNotify As Boolean = False

            Select Case newStatus
                Case "Valid"
                    shouldNotify = _settings.NotifyOnValidation OrElse _settings.NotifyOnIssuance
                Case "Invalid", "Rejected"
                    shouldNotify = _settings.NotifyOnRejection
                Case "Cancelled"
                    shouldNotify = _settings.NotifyOnCancellation
            End Select

            If Not shouldNotify Then Return

            RaiseEvent NewNotification(newStatus, title)

            If _settings.EnableDesktopToast AndAlso _trayIcon IsNot Nothing Then
                Try
                    _trayIcon.BalloonTipTitle = "ETA Invoicing"
                    _trayIcon.BalloonTipText = title
                    _trayIcon.ShowBalloonTip(5000)
                Catch
                    ' Balloon tips can fail silently on some systems; not critical.
                End Try
            End If

            If _settings.EnableEmailNotifications Then
                Try
                    SendEmailNotification(title)
                Catch ex As Exception
                    _logger.Warning("NotificationService.RaiseNotificationToUser", "Email send failed: " & ex.Message)
                End Try
            End If
        End Sub

        Private Sub SendEmailNotification(bodyText As String)
            If String.IsNullOrWhiteSpace(_settings.SmtpHost) OrElse String.IsNullOrWhiteSpace(_settings.NotificationEmail) Then Return

            Using client As New SmtpClient(_settings.SmtpHost, _settings.SmtpPort)
                client.EnableSsl = _settings.SmtpUseSsl
                If Not String.IsNullOrWhiteSpace(_settings.SmtpUsername) Then
                    client.Credentials = New NetworkCredential(_settings.SmtpUsername, _settings.SmtpPassword)
                End If

                Using mail As New MailMessage()
                    mail.From = New MailAddress(If(String.IsNullOrWhiteSpace(_settings.SmtpUsername), _settings.NotificationEmail, _settings.SmtpUsername))
                    mail.To.Add(_settings.NotificationEmail)
                    mail.Subject = "ETA Invoicing Notification"
                    mail.Body = bodyText
                    client.Send(mail)
                End Using
            End Using
        End Sub

        Public Sub Dispose() Implements IDisposable.Dispose
            _timer?.Stop()
            _timer?.Dispose()
        End Sub

    End Class

End Namespace
