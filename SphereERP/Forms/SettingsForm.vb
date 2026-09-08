Option Strict On
Imports DevExpress.XtraEditors
Imports DevExpress.XtraTab
Imports System.Security.Cryptography.X509Certificates
Imports System.Windows.Forms
Imports SphereERP.Data
Imports SphereERP.Models
Imports SphereERP.Services
Imports SphereERP.[Global]

Namespace Forms

    Public Class SettingsForm

        Private ReadOnly _signingService As New CertificateSigningService()
        Private _certificates As List(Of X509Certificate2)

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub SettingsForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            Dim settings = AppState.CurrentSettings
            If settings Is Nothing Then settings = New AppSettings()

            ' Credentials tab
            txtPreprodClientId.Text = settings.PreprodClientId
            txtPreprodClientSecret.Text = settings.PreprodClientSecret
            txtPreprodBaseUrl.Text = settings.PreprodBaseUrl
            txtPreprodIdentityUrl.Text = settings.PreprodIdentityUrl

            txtProdClientId.Text = settings.ProdClientId
            txtProdClientSecret.Text = settings.ProdClientSecret
            txtProdBaseUrl.Text = settings.ProdBaseUrl
            txtProdIdentityUrl.Text = settings.ProdIdentityUrl

            ' Taxpayer tab
            txtTaxpayerName.Text = settings.TaxpayerName
            txtDefaultActivityCode.Text = settings.DefaultActivityCode
            txtDefaultBranchId.Text = settings.DefaultBranchId
            txtIssuerGovernate.Text = settings.IssuerGovernate
            txtIssuerRegionCity.Text = settings.IssuerRegionCity
            txtIssuerStreet.Text = settings.IssuerStreet
            txtIssuerBuildingNumber.Text = settings.IssuerBuildingNumber

            ' Notifications tab
            chkNotifyValidation.Checked = settings.NotifyOnValidation
            chkNotifyIssuance.Checked = settings.NotifyOnIssuance
            chkNotifyRejection.Checked = settings.NotifyOnRejection
            chkNotifyCancellation.Checked = settings.NotifyOnCancellation
            numPollingMinutes.Value = Math.Max(1, Math.Min(1440, settings.NotificationPollingMinutes))
            chkDesktopToast.Checked = settings.EnableDesktopToast
            chkEmailNotifications.Checked = settings.EnableEmailNotifications
            txtNotificationEmail.Text = settings.NotificationEmail
            txtSmtpHost.Text = settings.SmtpHost
            numSmtpPort.Value = If(settings.SmtpPort > 0, settings.SmtpPort, 587)
            txtSmtpUsername.Text = settings.SmtpUsername
            txtSmtpPassword.Text = settings.SmtpPassword
            chkSmtpSsl.Checked = settings.SmtpUseSsl

            ' Database tab
            chkDbUseSqlServer.Checked = Not settings.DbUseLocalDb
            chkDbUseLocalDb.Checked = settings.DbUseLocalDb
            txtDbServer.Text = settings.DbServer
            txtDbPort.Text = settings.DbPort.ToString()
            txtDbUsername.Text = settings.DbUsername
            txtDbPassword.Text = settings.DbPassword
            UpdateDbPanelVisibility()

            LoadCertificates(settings.SigningCertThumbprint)
            Try
                LoadSubscriptionInfo()
            Catch ex As Exception
                System.Diagnostics.Debug.WriteLine("LoadSubscriptionInfo error: " & ex.Message)
            End Try
        End Sub

        Private Sub chkDbUseSqlServer_CheckedChanged(sender As Object, e As EventArgs) Handles chkDbUseSqlServer.CheckedChanged
            If chkDbUseSqlServer.Checked Then UpdateDbPanelVisibility()
        End Sub

        Private Sub chkDbUseLocalDb_CheckedChanged(sender As Object, e As EventArgs) Handles chkDbUseLocalDb.CheckedChanged
            If chkDbUseLocalDb.Checked Then UpdateDbPanelVisibility()
        End Sub

        Private Sub UpdateDbPanelVisibility()
            Dim useSqlServer = chkDbUseSqlServer.Checked
            gbDbSqlServer.Visible = useSqlServer
            gbDbLocalDb.Visible = Not useSqlServer
            lblDbStatus.Text = ""
        End Sub

        Private Sub LoadCertificates(selectedThumbprint As String)
            cmbCertificates.Properties.Items.Clear()
            _certificates = _signingService.ListSigningCertificates()

            If _certificates.Count = 0 Then
                cmbCertificates.Properties.Items.Add("(No signing certificates found - insert your USB token)")
                cmbCertificates.SelectedIndex = 0
                lblCertDetails.Text = "No certificates with a private key were found in the Windows certificate store. " &
                                       "Ensure your USB token / HSM driver is installed and the token is inserted, then click Refresh."
                Return
            End If

            Dim selectedIndex As Integer = -1
            For i = 0 To _certificates.Count - 1
                Dim cert = _certificates(i)
                Dim display = $"{cert.GetNameInfo(X509NameType.SimpleName, False)}  (Expires: {cert.NotAfter:yyyy-MM-dd})"
                cmbCertificates.Properties.Items.Add(display)
                If cert.Thumbprint.Equals(selectedThumbprint, StringComparison.OrdinalIgnoreCase) Then
                    selectedIndex = i
                End If
            Next

            cmbCertificates.SelectedIndex = If(selectedIndex >= 0, selectedIndex, 0)
            UpdateCertDetails()
        End Sub

        Private Sub cmbCertificates_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbCertificates.SelectedIndexChanged
            UpdateCertDetails()
        End Sub

        Private Sub UpdateCertDetails()
            If _certificates Is Nothing OrElse _certificates.Count = 0 OrElse cmbCertificates.SelectedIndex < 0 OrElse cmbCertificates.SelectedIndex >= _certificates.Count Then
                Return
            End If

            Dim cert = _certificates(cmbCertificates.SelectedIndex)
            lblCertDetails.Text =
                $"Subject: {cert.Subject}{Environment.NewLine}" &
                $"Issuer: {cert.Issuer}{Environment.NewLine}" &
                $"Thumbprint: {cert.Thumbprint}{Environment.NewLine}" &
                $"Valid: {cert.NotBefore:yyyy-MM-dd} to {cert.NotAfter:yyyy-MM-dd}{Environment.NewLine}" &
                $"Private key accessible: {If(_signingService.IsCertificateAccessible(cert), "Yes", "No - check token connection")}"
        End Sub

        Private Sub btnRefreshCertificates_Click(sender As Object, e As EventArgs) Handles btnRefreshCertificates.Click
            LoadCertificates(If(AppState.CurrentSettings IsNot Nothing, AppState.CurrentSettings.SigningCertThumbprint, ""))
        End Sub

        Private Sub btnTestCertificate_Click(sender As Object, e As EventArgs) Handles btnTestCertificate.Click
            If _certificates Is Nothing OrElse _certificates.Count = 0 OrElse cmbCertificates.SelectedIndex < 0 Then
                MessageBox.Show("No certificate selected.", "Test Sign", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Try
                Dim cert = _certificates(cmbCertificates.SelectedIndex)
                Dim testBytes = System.Text.Encoding.UTF8.GetBytes("ETA Invoicing Desktop - certificate test " & DateTime.Now.ToString("O"))
                Dim signature = _signingService.SignCadesBes(testBytes, cert)

                MessageBox.Show(
                    "Signing succeeded. Your USB token / certificate is correctly configured for document signing." & Environment.NewLine & Environment.NewLine &
                    $"Signature length: {signature.Length} characters (base64).",
                    "Test Sign - Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Catch ex As Exception
                MessageBox.Show("Signing test failed: " & ex.Message, "Test Sign - Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Private Function BuildConnectionStringFromUi() As String
            If chkDbUseLocalDb.Checked Then
                Return $"Server=(localdb)\MSSQLLocalDB;Database=ETAInvoicingDB;Integrated Security=True;TrustServerCertificate=True;"
            Else
                Dim port = If(Integer.TryParse(txtDbPort.Text.Trim(), Nothing), txtDbPort.Text.Trim(), "1433")
                Return $"Data Source={txtDbServer.Text.Trim()},{port};Initial Catalog=ETAInvoicingDB;User ID={txtDbUsername.Text.Trim()};Password={txtDbPassword.Text};Trusted_Connection=False;TrustServerCertificate=True;"
            End If
        End Function

        Private Sub btnTestConnection_Click(sender As Object, e As EventArgs) Handles btnTestConnection.Click
            Try
                Dim connStr = BuildConnectionStringFromUi()
                DbConnectionFactory.SetConnectionString(connStr)
                DbConnectionFactory.EnsureDatabaseCreated()
                lblDbStatus.Appearance.ForeColor = System.Drawing.Color.DarkGreen
                lblDbStatus.Text = "Connection successful. Database schema is ready."
            Catch ex As Exception
                lblDbStatus.Appearance.ForeColor = System.Drawing.Color.DarkRed
                lblDbStatus.Text = "Failed: " & ex.Message
            End Try
        End Sub

        Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
            Dim settings = AppState.CurrentSettings
            If settings Is Nothing Then settings = New AppSettings()

            settings.PreprodClientId = txtPreprodClientId.Text.Trim()
            settings.PreprodClientSecret = txtPreprodClientSecret.Text
            settings.PreprodBaseUrl = txtPreprodBaseUrl.Text.Trim()
            settings.PreprodIdentityUrl = txtPreprodIdentityUrl.Text.Trim()

            settings.ProdClientId = txtProdClientId.Text.Trim()
            settings.ProdClientSecret = txtProdClientSecret.Text
            settings.ProdBaseUrl = txtProdBaseUrl.Text.Trim()
            settings.ProdIdentityUrl = txtProdIdentityUrl.Text.Trim()

            settings.TaxpayerName = txtTaxpayerName.Text.Trim()
            settings.DefaultActivityCode = txtDefaultActivityCode.Text.Trim()
            settings.DefaultBranchId = txtDefaultBranchId.Text.Trim()
            settings.IssuerGovernate = txtIssuerGovernate.Text.Trim()
            settings.IssuerRegionCity = txtIssuerRegionCity.Text.Trim()
            settings.IssuerStreet = txtIssuerStreet.Text.Trim()
            settings.IssuerBuildingNumber = txtIssuerBuildingNumber.Text.Trim()

            settings.NotifyOnValidation = chkNotifyValidation.Checked
            settings.NotifyOnIssuance = chkNotifyIssuance.Checked
            settings.NotifyOnRejection = chkNotifyRejection.Checked
            settings.NotifyOnCancellation = chkNotifyCancellation.Checked
            settings.NotificationPollingMinutes = CInt(numPollingMinutes.Value)
            settings.EnableDesktopToast = chkDesktopToast.Checked
            settings.EnableEmailNotifications = chkEmailNotifications.Checked
            settings.NotificationEmail = txtNotificationEmail.Text.Trim()
            settings.SmtpHost = txtSmtpHost.Text.Trim()
            settings.SmtpPort = CInt(numSmtpPort.Value)
            settings.SmtpUsername = txtSmtpUsername.Text.Trim()
            settings.SmtpPassword = txtSmtpPassword.Text
            settings.SmtpUseSsl = chkSmtpSsl.Checked
            settings.DbUseLocalDb = chkDbUseLocalDb.Checked
            settings.DbServer = txtDbServer.Text.Trim()
            Dim portVal As Integer = 1433
            Integer.TryParse(txtDbPort.Text.Trim(), portVal)
            settings.DbPort = portVal
            settings.DbUsername = txtDbUsername.Text.Trim()
            settings.DbPassword = txtDbPassword.Text
            settings.DatabaseConnectionString = BuildConnectionStringFromUi()

            If _certificates IsNot Nothing AndAlso _certificates.Count > 0 AndAlso cmbCertificates.SelectedIndex >= 0 AndAlso cmbCertificates.SelectedIndex < _certificates.Count Then
                settings.SigningCertThumbprint = _certificates(cmbCertificates.SelectedIndex).Thumbprint
            End If

            Try
                SettingsService.Save(settings)
                AppState.CurrentSettings = settings
                DbConnectionFactory.SetConnectionString(settings.BuildConnectionString())
                DbConnectionFactory.EnsureDatabaseCreated()
                AppState.ReinitializeApiClient()

                DialogResult = DialogResult.OK
                Close()
            Catch ex As Exception
                MessageBox.Show("Failed to save settings: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
            DialogResult = DialogResult.Cancel
            Close()
        End Sub

        Private _subService As LicenseService

        Private Sub LoadSubscriptionInfo()
            Dim settings = AppState.CurrentSettings
            If settings Is Nothing Then Return

            Dim key = settings.LicenseKey

            If String.IsNullOrWhiteSpace(key) Then
                lblSubKey.Text = "(No license activated)"
                lblSubCustomer.Text = "—"
                lblSubPlan.Text = "—"
                lblSubExpiry.Text = "—"
                lblSubStatus.Text = "Not activated"
                lblSubStatus.Appearance.ForeColor = System.Drawing.Color.Gray
                lblSubLastCheck.Text = "—"
                btnSubActivate.Text = "Activate License"
                Return
            End If

            _subService = New LicenseService(key, settings.LicenseServerUrl)
            lblSubKey.Text = key
            btnSubActivate.Text = "Change License"

            Dim cached = _subService.GetCachedLicenseData()
            If cached IsNot Nothing Then
                lblSubCustomer.Text = cached.CustomerName
                lblSubPlan.Text = If(String.IsNullOrWhiteSpace(cached.Plan), "—", cached.Plan)
                lblSubExpiry.Text = cached.ExpiryDate.ToString("yyyy-MM-dd")
                lblSubLastCheck.Text = cached.LastCheckUtc.ToString("yyyy-MM-dd HH:mm") & " UTC"

                Dim result = _subService.QuickValidateFromCache()
                If result IsNot Nothing AndAlso result.IsValid Then
                    lblSubStatus.Text = "Valid"
                    lblSubStatus.Appearance.ForeColor = System.Drawing.Color.DarkGreen
                Else
                    Dim msg = If(result IsNot Nothing, result.Message, "Expired")
                    lblSubStatus.Text = msg
                    lblSubStatus.Appearance.ForeColor = System.Drawing.Color.DarkRed
                End If
            Else
                lblSubCustomer.Text = "—"
                lblSubPlan.Text = "—"
                lblSubExpiry.Text = "—"
                lblSubLastCheck.Text = "—"
                lblSubStatus.Text = "No cached data — click Refresh"
                lblSubStatus.Appearance.ForeColor = System.Drawing.Color.Gray
            End If
        End Sub

        Private Async Sub btnSubRefresh_Click(sender As Object, e As EventArgs) Handles btnSubRefresh.Click
            btnSubRefresh.Enabled = False
            lblSubStatus.Text = "Validating..."
            Refresh()

            Try
                If _subService Is Nothing Then
                    Dim settings = AppState.CurrentSettings
                    If settings IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(settings.LicenseKey) Then
                        _subService = New LicenseService(settings.LicenseKey, settings.LicenseServerUrl)
                    End If
                End If

                If _subService IsNot Nothing Then
                    Await _subService.ValidateAsync()
                End If

                LoadSubscriptionInfo()
            Catch ex As Exception
                lblSubStatus.Text = "Error: " & ex.Message
                lblSubStatus.Appearance.ForeColor = System.Drawing.Color.DarkRed
            Finally
                btnSubRefresh.Enabled = True
            End Try
        End Sub

        Private Sub btnSubActivate_Click(sender As Object, e As EventArgs) Handles btnSubActivate.Click
            Dim key = If(AppState.CurrentSettings IsNot Nothing, AppState.CurrentSettings.LicenseKey, "")
            Using frm As New LicenseActivationForm(key)
                If frm.ShowDialog(Me) = DialogResult.OK Then
                    If AppState.CurrentSettings IsNot Nothing Then
                        AppState.CurrentSettings.LicenseKey = frm.LicenseKey
                        SettingsService.Save(AppState.CurrentSettings)
                    End If
                    LoadSubscriptionInfo()
                End If
            End Using
        End Sub

    End Class

End Namespace
