Option Strict On
Imports System.Threading
Imports System.Windows.Forms
Imports Microsoft.VisualBasic.ApplicationServices
Imports SphereERP.Data
Imports SphereERP.Services
Imports SphereERP.[Global]

Namespace My

    Partial Friend Class MyApplication

        Protected Overrides Function OnStartup(eventArgs As StartupEventArgs) As Boolean
            Global.System.Windows.Forms.Application.SetHighDpiMode(HighDpiMode.PerMonitorV2)

            AddHandler Global.System.Windows.Forms.Application.ThreadException, AddressOf OnThreadException
            AddHandler AppDomain.CurrentDomain.UnhandledException, AddressOf OnAppDomainUnhandledException

            Try
                AppState.InitializeServices()
            Catch ex As Exception
                MessageBox.Show("Failed to initialize application services: " & ex.Message, "Startup Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

            If Not ValidateLicenseAtStartup() Then
                Return False
            End If

            Return MyBase.OnStartup(eventArgs)
        End Function

        Private Shared Function ValidateLicenseAtStartup() As Boolean
            Dim settings = AppState.CurrentSettings

            If String.IsNullOrWhiteSpace(settings.LicenseKey) Then
                Using activationForm As New Forms.LicenseActivationForm()
                    If activationForm.ShowDialog() <> DialogResult.OK Then
                        MessageBox.Show("License activation is required to use this application.", "Activation Required", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Return False
                    End If
                    settings.LicenseKey = activationForm.LicenseKey
                    SettingsService.Save(settings)
                End Using
            End If

            Dim service As New LicenseService(settings.LicenseKey, settings.LicenseServerUrl)
            Dim cachedResult = service.QuickValidateFromCache()

            If cachedResult IsNot Nothing AndAlso Not cachedResult.IsValid Then
                If cachedResult.ExpiryDate.HasValue AndAlso DateTime.UtcNow > cachedResult.ExpiryDate.Value.AddDays(7) Then
                    MessageBox.Show(cachedResult.Message & vbCrLf & vbCrLf & "Please renew your subscription to continue.", "License Expired", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Return False
                End If

                Using activationForm As New Forms.LicenseActivationForm(settings.LicenseKey)
                    If activationForm.ShowDialog() <> DialogResult.OK Then
                        MessageBox.Show("License activation is required to use this application.", "Activation Required", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Return False
                    End If
                    settings.LicenseKey = activationForm.LicenseKey
                    SettingsService.Save(settings)
                End Using
            End If

            If cachedResult Is Nothing Then
                Using activationForm As New Forms.LicenseActivationForm(settings.LicenseKey)
                    If activationForm.ShowDialog() <> DialogResult.OK Then
                        MessageBox.Show("License activation is required to use this application.", "Activation Required", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Return False
                    End If
                    settings.LicenseKey = activationForm.LicenseKey
                    SettingsService.Save(settings)
                End Using
            End If

            Return True
        End Function

        Protected Overrides Sub OnCreateMainForm()
            Me.MainForm = New Forms.LoginForm()
        End Sub

        Private Shared Sub OnThreadException(sender As Object, e As ThreadExceptionEventArgs)
            HandleException(e.Exception)
        End Sub

        Private Shared Sub OnAppDomainUnhandledException(sender As Object, e As System.UnhandledExceptionEventArgs)
            HandleException(TryCast(e.ExceptionObject, Exception))
        End Sub

        Private Shared Sub HandleException(ex As Exception)
            Try
                Dim logger As New AppLogRepository()
                logger.Error("UnhandledException", If(ex IsNot Nothing, ex.ToString(), "Unknown error"))
            Catch
                ' Logging itself failed (e.g. DB unreachable) - fall through to message box only.
            End Try

            MessageBox.Show(
                "An unexpected error occurred:" & Environment.NewLine & Environment.NewLine &
                If(ex IsNot Nothing, ex.Message, "Unknown error"),
                "ETA Invoicing - Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Sub

    End Class

End Namespace
