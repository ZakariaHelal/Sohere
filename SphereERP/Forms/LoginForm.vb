Option Strict On
Imports System.Windows.Forms
Imports DevExpress.XtraEditors
Imports SphereERP.Models
Imports SphereERP.Services
Imports SphereERP.[Global]

Namespace Forms

    Public Class LoginForm

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub LoginForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            LoadSettingsIntoForm()
        End Sub

        Private Sub LoadSettingsIntoForm()
            Dim settings = AppState.CurrentSettings
            If settings Is Nothing Then
                settings = New AppSettings()
                AppState.CurrentSettings = settings
            End If

            rbPreproduction.Checked = (settings.Environment = ApiEnvironment.Preproduction)
            rbProduction.Checked = (settings.Environment = ApiEnvironment.Production)

            txtClientId.Text = settings.ActiveClientId
            txtClientSecret.Text = settings.ActiveClientSecret
            txtTaxpayerRIN.Text = settings.TaxpayerRIN
        End Sub

        Private Sub rbEnvironment_CheckedChanged(sender As Object, e As EventArgs) Handles rbPreproduction.CheckedChanged, rbProduction.CheckedChanged
            Dim settings = AppState.CurrentSettings
            If rbProduction.Checked Then
                txtClientId.Text = settings.ProdClientId
                txtClientSecret.Text = settings.ProdClientSecret
                lblSubtitle.ForeColor = System.Drawing.Color.DarkRed
                lblSubtitle.Text = "PRODUCTION mode - documents submitted here are LIVE and legally binding."
            Else
                txtClientId.Text = settings.PreprodClientId
                txtClientSecret.Text = settings.PreprodClientSecret
                lblSubtitle.ForeColor = System.Drawing.Color.Gray
                lblSubtitle.Text = "Sign in to the Egyptian Tax Authority e-Invoicing API"
            End If
        End Sub

        Private Async Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
            lblStatus.Text = ""

            If String.IsNullOrWhiteSpace(txtClientId.Text) OrElse String.IsNullOrWhiteSpace(txtClientSecret.Text) Then
                lblStatus.Text = "Please enter both Client ID and Client Secret."
                Return
            End If
            If String.IsNullOrWhiteSpace(txtTaxpayerRIN.Text) Then
                lblStatus.Text = "Please enter the Taxpayer Tax Registration Number (RIN)."
                Return
            End If

            SetBusy(True)

            Try
                Dim settings = AppState.CurrentSettings
                settings.Environment = If(rbProduction.Checked, ApiEnvironment.Production, ApiEnvironment.Preproduction)
                settings.TaxpayerRIN = txtTaxpayerRIN.Text.Trim()

                If settings.Environment = ApiEnvironment.Production Then
                    settings.ProdClientId = txtClientId.Text.Trim()
                    settings.ProdClientSecret = txtClientSecret.Text
                Else
                    settings.PreprodClientId = txtClientId.Text.Trim()
                    settings.PreprodClientSecret = txtClientSecret.Text
                End If

                If lnkSaveCredentials.Checked Then
                    SettingsService.Save(settings)
                End If

                AppState.ReinitializeApiClient()

                Await AppState.ApiClient.LoginAsync().ConfigureAwait(True)
                AppState.IsLoggedIn = True

                Hide()
                Using mainForm As New MainForm()
                    mainForm.ShowDialog()
                End Using

                If AppState.IsLoggedIn Then
                    Close()
                Else
                    Show()
                    SetBusy(False)
                End If

            Catch ex As Exception
                lblStatus.Text = ex.Message
                SetBusy(False)
            End Try

        End Sub

        Private Sub btnSettings_Click(sender As Object, e As EventArgs) Handles btnSettings.Click
            Using settingsForm As New SettingsForm()
                If settingsForm.ShowDialog() = DialogResult.OK Then
                    LoadSettingsIntoForm()
                End If
            End Using
        End Sub

        Private Sub SetBusy(busy As Boolean)
            progressBar.Visible = busy
            btnLogin.Enabled = Not busy
            btnSettings.Enabled = Not busy
            txtClientId.Enabled = Not busy
            txtClientSecret.Enabled = Not busy
            txtTaxpayerRIN.Enabled = Not busy
            rbPreproduction.Enabled = Not busy
            rbProduction.Enabled = Not busy
            If busy Then lblStatus.Text = "Signing in..."
        End Sub

    End Class

End Namespace
