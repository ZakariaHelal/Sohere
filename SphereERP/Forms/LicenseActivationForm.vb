Option Strict On
Imports System.Windows.Forms
Imports DevExpress.XtraEditors
Imports SphereERP.Services
Imports SphereERP.[Global]

Namespace Forms

    Public Class LicenseActivationForm

        Public ReadOnly Property LicenseKey As String
            Get
                Return txtLicenseKey.Text.Trim()
            End Get
        End Property

        Public Property IsActivated As Boolean = False

        ''' <summary>
        ''' Optional: pre-fill an existing license key (e.g. when re-activating after expiry).
        ''' </summary>
        Public Sub New(Optional existingKey As String = Nothing)
            InitializeComponent()
            If Not String.IsNullOrWhiteSpace(existingKey) Then
                txtLicenseKey.Text = existingKey
            End If
        End Sub

        Private Sub LicenseActivationForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            lblHwid.Text = "Machine ID: " & LicenseService.GetHardwareId()
        End Sub

        Private Async Sub btnActivate_Click(sender As Object, e As EventArgs) Handles btnActivate.Click
            If String.IsNullOrWhiteSpace(txtLicenseKey.Text) Then
                XtraMessageBox.Show("Please enter your license key.", "Activation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            btnActivate.Enabled = False
            lblStatus.Text = "Validating license..."
            Refresh()

            Try
                Dim key = txtLicenseKey.Text.Trim()
                Dim service As LicenseService
                If Not String.IsNullOrWhiteSpace(AppState.CurrentSettings?.LicenseServerUrl) Then
                    service = New LicenseService(key, AppState.CurrentSettings.LicenseServerUrl)
                Else
                    service = New LicenseService(key)
                End If

                Dim result = Await service.ValidateAsync()

                If result.IsValid Then
                    IsActivated = True
                    XtraMessageBox.Show($"License activated successfully!{vbCrLf}{result.Message}", "Activation", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    DialogResult = DialogResult.OK
                    Close()
                Else
                    lblStatus.Text = result.Message
                    XtraMessageBox.Show($"Activation failed: {result.Message}", "Activation", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    btnActivate.Enabled = True
                End If
            Catch ex As Exception
                lblStatus.Text = ex.Message
                XtraMessageBox.Show($"Activation error: {ex.Message}", "Activation", MessageBoxButtons.OK, MessageBoxIcon.Error)
                btnActivate.Enabled = True
            End Try
        End Sub

        Private Sub btnBuyNow_Click(sender As Object, e As EventArgs) Handles btnBuyNow.Click
            Try
                Process.Start("https://your-payment-page.com")
            Catch
            End Try
        End Sub

        Private Sub btnOfflineActivation_Click(sender As Object, e As EventArgs) Handles btnOfflineActivation.Click
            Using dlg As New XtraForm()
                dlg.Text = "Offline Activation"
                dlg.Width = 480
                dlg.Height = 300
                dlg.StartPosition = FormStartPosition.CenterParent

                Dim lblInfo As New LabelControl()
                lblInfo.Text = $"Machine ID:{vbCrLf}{lblHwid.Text}{vbCrLf}{vbCrLf}Paste the activation code received from support:"
                lblInfo.AutoSizeMode = LabelAutoSizeMode.Vertical
                lblInfo.Dock = DockStyle.Top
                lblInfo.Padding = New Padding(12)

                Dim txtCode As New MemoEdit()
                txtCode.Dock = DockStyle.Fill

                Dim btnOk As New SimpleButton()
                btnOk.Text = "Activate"
                btnOk.DialogResult = DialogResult.OK
                btnOk.Dock = DockStyle.Bottom

                dlg.Controls.Add(txtCode)
                dlg.Controls.Add(btnOk)
                dlg.Controls.Add(lblInfo)

                If dlg.ShowDialog(Me) = DialogResult.OK AndAlso Not String.IsNullOrWhiteSpace(txtCode.Text) Then
                    IsActivated = True
                    DialogResult = DialogResult.OK
                    Close()
                End If
            End Using
        End Sub

    End Class

End Namespace
