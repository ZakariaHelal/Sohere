Namespace Forms
    Partial Class SettingsForm
        Inherits DevExpress.XtraEditors.XtraForm

        <System.Diagnostics.DebuggerNonUserCode()>
        Protected Overrides Sub Dispose(disposing As Boolean)
            Try
                If disposing AndAlso components IsNot Nothing Then
                    components.Dispose()
                End If
            Finally
                MyBase.Dispose(disposing)
            End Try
        End Sub

        Private components As System.ComponentModel.IContainer

        <System.Diagnostics.DebuggerStepThrough()>
        Private Sub InitializeComponent()
            tabControl = New DevExpress.XtraTab.XtraTabControl()
            tabCertificate = New DevExpress.XtraTab.XtraTabPage()
            lblCertInstructions = New DevExpress.XtraEditors.LabelControl()
            cmbCertificates = New DevExpress.XtraEditors.ComboBoxEdit()
            btnRefreshCertificates = New DevExpress.XtraEditors.SimpleButton()
            lblCertDetails = New DevExpress.XtraEditors.LabelControl()
            btnTestCertificate = New DevExpress.XtraEditors.SimpleButton()
            tabCredentials = New DevExpress.XtraTab.XtraTabPage()
            envTabControl = New DevExpress.XtraTab.XtraTabControl()
            tabPreprod = New DevExpress.XtraTab.XtraTabPage()
            lblPreprodClientId = New DevExpress.XtraEditors.LabelControl()
            txtPreprodClientId = New DevExpress.XtraEditors.TextEdit()
            lblPreprodClientSecret = New DevExpress.XtraEditors.LabelControl()
            txtPreprodClientSecret = New DevExpress.XtraEditors.TextEdit()
            lblPreprodBaseUrl = New DevExpress.XtraEditors.LabelControl()
            txtPreprodBaseUrl = New DevExpress.XtraEditors.TextEdit()
            lblPreprodIdentityUrl = New DevExpress.XtraEditors.LabelControl()
            txtPreprodIdentityUrl = New DevExpress.XtraEditors.TextEdit()
            tabProd = New DevExpress.XtraTab.XtraTabPage()
            lblProdClientId = New DevExpress.XtraEditors.LabelControl()
            txtProdClientId = New DevExpress.XtraEditors.TextEdit()
            lblProdClientSecret = New DevExpress.XtraEditors.LabelControl()
            txtProdClientSecret = New DevExpress.XtraEditors.TextEdit()
            lblProdBaseUrl = New DevExpress.XtraEditors.LabelControl()
            txtProdBaseUrl = New DevExpress.XtraEditors.TextEdit()
            lblProdIdentityUrl = New DevExpress.XtraEditors.LabelControl()
            txtProdIdentityUrl = New DevExpress.XtraEditors.TextEdit()
            tabTaxpayer = New DevExpress.XtraTab.XtraTabPage()
            lblTaxpayerName = New DevExpress.XtraEditors.LabelControl()
            txtTaxpayerName = New DevExpress.XtraEditors.TextEdit()
            lblDefaultActivityCode = New DevExpress.XtraEditors.LabelControl()
            txtDefaultActivityCode = New DevExpress.XtraEditors.TextEdit()
            lblDefaultBranchId = New DevExpress.XtraEditors.LabelControl()
            txtDefaultBranchId = New DevExpress.XtraEditors.TextEdit()
            lblIssuerGovernate = New DevExpress.XtraEditors.LabelControl()
            txtIssuerGovernate = New DevExpress.XtraEditors.TextEdit()
            lblIssuerRegionCity = New DevExpress.XtraEditors.LabelControl()
            txtIssuerRegionCity = New DevExpress.XtraEditors.TextEdit()
            lblIssuerStreet = New DevExpress.XtraEditors.LabelControl()
            txtIssuerStreet = New DevExpress.XtraEditors.TextEdit()
            lblIssuerBuildingNumber = New DevExpress.XtraEditors.LabelControl()
            txtIssuerBuildingNumber = New DevExpress.XtraEditors.TextEdit()
            tabNotifications = New DevExpress.XtraTab.XtraTabPage()
            chkNotifyValidation = New DevExpress.XtraEditors.CheckEdit()
            chkNotifyIssuance = New DevExpress.XtraEditors.CheckEdit()
            chkNotifyRejection = New DevExpress.XtraEditors.CheckEdit()
            chkNotifyCancellation = New DevExpress.XtraEditors.CheckEdit()
            lblPollingInterval = New DevExpress.XtraEditors.LabelControl()
            numPollingMinutes = New DevExpress.XtraEditors.SpinEdit()
            chkDesktopToast = New DevExpress.XtraEditors.CheckEdit()
            chkEmailNotifications = New DevExpress.XtraEditors.CheckEdit()
            lblNotificationEmail = New DevExpress.XtraEditors.LabelControl()
            txtNotificationEmail = New DevExpress.XtraEditors.TextEdit()
            lblSmtpHost = New DevExpress.XtraEditors.LabelControl()
            txtSmtpHost = New DevExpress.XtraEditors.TextEdit()
            lblSmtpPort = New DevExpress.XtraEditors.LabelControl()
            numSmtpPort = New DevExpress.XtraEditors.SpinEdit()
            lblSmtpUsername = New DevExpress.XtraEditors.LabelControl()
            txtSmtpUsername = New DevExpress.XtraEditors.TextEdit()
            lblSmtpPassword = New DevExpress.XtraEditors.LabelControl()
            txtSmtpPassword = New DevExpress.XtraEditors.TextEdit()
            chkSmtpSsl = New DevExpress.XtraEditors.CheckEdit()
            tabDatabase = New DevExpress.XtraTab.XtraTabPage()
            chkDbUseSqlServer = New DevExpress.XtraEditors.CheckEdit()
            chkDbUseLocalDb = New DevExpress.XtraEditors.CheckEdit()
            gbDbSqlServer = New DevExpress.XtraEditors.GroupControl()
            lblDbServer = New DevExpress.XtraEditors.LabelControl()
            txtDbServer = New DevExpress.XtraEditors.TextEdit()
            lblDbPort = New DevExpress.XtraEditors.LabelControl()
            txtDbPort = New DevExpress.XtraEditors.TextEdit()
            lblDbUsername = New DevExpress.XtraEditors.LabelControl()
            txtDbUsername = New DevExpress.XtraEditors.TextEdit()
            lblDbPassword = New DevExpress.XtraEditors.LabelControl()
            txtDbPassword = New DevExpress.XtraEditors.TextEdit()
            gbDbLocalDb = New DevExpress.XtraEditors.GroupControl()
            lblDbLocalDbInfo = New DevExpress.XtraEditors.LabelControl()
            btnTestConnection = New DevExpress.XtraEditors.SimpleButton()
            lblDbStatus = New DevExpress.XtraEditors.LabelControl()
            btnSave = New DevExpress.XtraEditors.SimpleButton()
            btnCancel = New DevExpress.XtraEditors.SimpleButton()
            tabSubscription = New DevExpress.XtraTab.XtraTabPage()
            gbSubInfo = New DevExpress.XtraEditors.GroupControl()
            lblSubKeyLabel = New DevExpress.XtraEditors.LabelControl()
            lblSubKey = New DevExpress.XtraEditors.LabelControl()
            lblSubCustomerLabel = New DevExpress.XtraEditors.LabelControl()
            lblSubCustomer = New DevExpress.XtraEditors.LabelControl()
            lblSubPlanLabel = New DevExpress.XtraEditors.LabelControl()
            lblSubPlan = New DevExpress.XtraEditors.LabelControl()
            lblSubExpiryLabel = New DevExpress.XtraEditors.LabelControl()
            lblSubExpiry = New DevExpress.XtraEditors.LabelControl()
            lblSubStatusLabel = New DevExpress.XtraEditors.LabelControl()
            lblSubStatus = New DevExpress.XtraEditors.LabelControl()
            lblSubLastCheckLabel = New DevExpress.XtraEditors.LabelControl()
            lblSubLastCheck = New DevExpress.XtraEditors.LabelControl()
            lblSubServerUrlLabel = New DevExpress.XtraEditors.LabelControl()
            lblSubServerUrl = New DevExpress.XtraEditors.LabelControl()
            btnSubRefresh = New DevExpress.XtraEditors.SimpleButton()
            btnSubActivate = New DevExpress.XtraEditors.SimpleButton()
            CType(tabControl, ComponentModel.ISupportInitialize).BeginInit()
            tabControl.SuspendLayout()
            tabCertificate.SuspendLayout()
            CType(cmbCertificates.Properties, ComponentModel.ISupportInitialize).BeginInit()
            tabCredentials.SuspendLayout()
            CType(envTabControl, ComponentModel.ISupportInitialize).BeginInit()
            envTabControl.SuspendLayout()
            tabPreprod.SuspendLayout()
            CType(txtPreprodClientId.Properties, ComponentModel.ISupportInitialize).BeginInit()
            CType(txtPreprodClientSecret.Properties, ComponentModel.ISupportInitialize).BeginInit()
            CType(txtPreprodBaseUrl.Properties, ComponentModel.ISupportInitialize).BeginInit()
            CType(txtPreprodIdentityUrl.Properties, ComponentModel.ISupportInitialize).BeginInit()
            tabProd.SuspendLayout()
            CType(txtProdClientId.Properties, ComponentModel.ISupportInitialize).BeginInit()
            CType(txtProdClientSecret.Properties, ComponentModel.ISupportInitialize).BeginInit()
            CType(txtProdBaseUrl.Properties, ComponentModel.ISupportInitialize).BeginInit()
            CType(txtProdIdentityUrl.Properties, ComponentModel.ISupportInitialize).BeginInit()
            tabTaxpayer.SuspendLayout()
            CType(txtTaxpayerName.Properties, ComponentModel.ISupportInitialize).BeginInit()
            CType(txtDefaultActivityCode.Properties, ComponentModel.ISupportInitialize).BeginInit()
            CType(txtDefaultBranchId.Properties, ComponentModel.ISupportInitialize).BeginInit()
            CType(txtIssuerGovernate.Properties, ComponentModel.ISupportInitialize).BeginInit()
            CType(txtIssuerRegionCity.Properties, ComponentModel.ISupportInitialize).BeginInit()
            CType(txtIssuerStreet.Properties, ComponentModel.ISupportInitialize).BeginInit()
            CType(txtIssuerBuildingNumber.Properties, ComponentModel.ISupportInitialize).BeginInit()
            tabNotifications.SuspendLayout()
            CType(chkNotifyValidation.Properties, ComponentModel.ISupportInitialize).BeginInit()
            CType(chkNotifyIssuance.Properties, ComponentModel.ISupportInitialize).BeginInit()
            CType(chkNotifyRejection.Properties, ComponentModel.ISupportInitialize).BeginInit()
            CType(chkNotifyCancellation.Properties, ComponentModel.ISupportInitialize).BeginInit()
            CType(numPollingMinutes.Properties, ComponentModel.ISupportInitialize).BeginInit()
            CType(chkDesktopToast.Properties, ComponentModel.ISupportInitialize).BeginInit()
            CType(chkEmailNotifications.Properties, ComponentModel.ISupportInitialize).BeginInit()
            CType(txtNotificationEmail.Properties, ComponentModel.ISupportInitialize).BeginInit()
            CType(txtSmtpHost.Properties, ComponentModel.ISupportInitialize).BeginInit()
            CType(numSmtpPort.Properties, ComponentModel.ISupportInitialize).BeginInit()
            CType(txtSmtpUsername.Properties, ComponentModel.ISupportInitialize).BeginInit()
            CType(txtSmtpPassword.Properties, ComponentModel.ISupportInitialize).BeginInit()
            CType(chkSmtpSsl.Properties, ComponentModel.ISupportInitialize).BeginInit()
            tabDatabase.SuspendLayout()
            CType(chkDbUseSqlServer.Properties, ComponentModel.ISupportInitialize).BeginInit()
            CType(chkDbUseLocalDb.Properties, ComponentModel.ISupportInitialize).BeginInit()
            CType(gbSubInfo, ComponentModel.ISupportInitialize).BeginInit()
            gbSubInfo.SuspendLayout()
            tabSubscription.SuspendLayout()
            CType(gbDbSqlServer, ComponentModel.ISupportInitialize).BeginInit()
            gbDbSqlServer.SuspendLayout()
            CType(txtDbServer.Properties, ComponentModel.ISupportInitialize).BeginInit()
            CType(txtDbPort.Properties, ComponentModel.ISupportInitialize).BeginInit()
            CType(txtDbUsername.Properties, ComponentModel.ISupportInitialize).BeginInit()
            CType(txtDbPassword.Properties, ComponentModel.ISupportInitialize).BeginInit()
            CType(gbDbLocalDb, ComponentModel.ISupportInitialize).BeginInit()
            gbDbLocalDb.SuspendLayout()
            SuspendLayout()
            ' 
            ' tabControl
            ' 
            tabControl.Location = New System.Drawing.Point(12, 12)
            tabControl.Name = "tabControl"
            tabControl.SelectedTabPage = tabCredentials
            tabControl.Size = New System.Drawing.Size(620, 480)
            tabControl.TabIndex = 0
            tabControl.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {tabCredentials, tabCertificate, tabTaxpayer, tabNotifications, tabDatabase, tabSubscription})
            ' 
            ' tabCertificate
            ' 
            tabCertificate.Controls.Add(lblCertInstructions)
            tabCertificate.Controls.Add(cmbCertificates)
            tabCertificate.Controls.Add(btnRefreshCertificates)
            tabCertificate.Controls.Add(lblCertDetails)
            tabCertificate.Controls.Add(btnTestCertificate)
            tabCertificate.Name = "tabCertificate"
            tabCertificate.Padding = New System.Windows.Forms.Padding(10)
            tabCertificate.Size = New System.Drawing.Size(618, 455)
            tabCertificate.Text = "Digital Certificate (USB Token)"
            ' 
            ' lblCertInstructions
            ' 
            lblCertInstructions.Location = New System.Drawing.Point(15, 15)
            lblCertInstructions.Name = "lblCertInstructions"
            lblCertInstructions.Size = New System.Drawing.Size(877, 13)
            lblCertInstructions.TabIndex = 0
            lblCertInstructions.Text = "Insert your USB token / HSM, ensure its driver (CSP/minidriver) is installed, then select your signing certificate below. Windows will prompt for your token PIN when signing documents."
            ' 
            ' cmbCertificates
            ' 
            cmbCertificates.Location = New System.Drawing.Point(15, 85)
            cmbCertificates.Name = "cmbCertificates"
            cmbCertificates.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
            cmbCertificates.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
            cmbCertificates.Size = New System.Drawing.Size(470, 20)
            cmbCertificates.TabIndex = 1
            ' 
            ' btnRefreshCertificates
            ' 
            btnRefreshCertificates.Location = New System.Drawing.Point(495, 84)
            btnRefreshCertificates.Name = "btnRefreshCertificates"
            btnRefreshCertificates.Size = New System.Drawing.Size(100, 27)
            btnRefreshCertificates.TabIndex = 2
            btnRefreshCertificates.Text = "Refresh"
            ' 
            ' lblCertDetails
            ' 
            lblCertDetails.Appearance.ForeColor = Drawing.Color.DimGray
            lblCertDetails.Appearance.Options.UseForeColor = True
            lblCertDetails.Location = New System.Drawing.Point(15, 125)
            lblCertDetails.Name = "lblCertDetails"
            lblCertDetails.Size = New System.Drawing.Size(0, 13)
            lblCertDetails.TabIndex = 3
            ' 
            ' btnTestCertificate
            ' 
            btnTestCertificate.Location = New System.Drawing.Point(15, 235)
            btnTestCertificate.Name = "btnTestCertificate"
            btnTestCertificate.Size = New System.Drawing.Size(220, 32)
            btnTestCertificate.TabIndex = 4
            btnTestCertificate.Text = "Test Sign (verify PIN access)"
            ' 
            ' tabCredentials
            ' 
            tabCredentials.Controls.Add(envTabControl)
            tabCredentials.Name = "tabCredentials"
            tabCredentials.Size = New System.Drawing.Size(618, 455)
            tabCredentials.Text = "Environment && Credentials"
            ' 
            ' envTabControl
            ' 
            envTabControl.Dock = System.Windows.Forms.DockStyle.Fill
            envTabControl.Location = New System.Drawing.Point(0, 0)
            envTabControl.Name = "envTabControl"
            envTabControl.SelectedTabPage = tabProd
            envTabControl.Size = New System.Drawing.Size(618, 455)
            envTabControl.TabIndex = 0
            envTabControl.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {tabProd, tabPreprod})
            ' 
            ' tabPreprod
            ' 
            tabPreprod.Controls.Add(lblPreprodClientId)
            tabPreprod.Controls.Add(txtPreprodClientId)
            tabPreprod.Controls.Add(lblPreprodClientSecret)
            tabPreprod.Controls.Add(txtPreprodClientSecret)
            tabPreprod.Controls.Add(lblPreprodBaseUrl)
            tabPreprod.Controls.Add(txtPreprodBaseUrl)
            tabPreprod.Controls.Add(lblPreprodIdentityUrl)
            tabPreprod.Controls.Add(txtPreprodIdentityUrl)
            tabPreprod.Name = "tabPreprod"
            tabPreprod.Padding = New System.Windows.Forms.Padding(10)
            tabPreprod.Size = New System.Drawing.Size(616, 430)
            tabPreprod.Text = "Preproduction"
            ' 
            ' lblPreprodClientId
            ' 
            lblPreprodClientId.Location = New System.Drawing.Point(15, 15)
            lblPreprodClientId.Name = "lblPreprodClientId"
            lblPreprodClientId.Size = New System.Drawing.Size(45, 13)
            lblPreprodClientId.TabIndex = 0
            lblPreprodClientId.Text = "Client ID:"
            ' 
            ' txtPreprodClientId
            ' 
            txtPreprodClientId.Location = New System.Drawing.Point(15, 33)
            txtPreprodClientId.Name = "txtPreprodClientId"
            txtPreprodClientId.Size = New System.Drawing.Size(560, 20)
            txtPreprodClientId.TabIndex = 1
            ' 
            ' lblPreprodClientSecret
            ' 
            lblPreprodClientSecret.Location = New System.Drawing.Point(15, 65)
            lblPreprodClientSecret.Name = "lblPreprodClientSecret"
            lblPreprodClientSecret.Size = New System.Drawing.Size(65, 13)
            lblPreprodClientSecret.TabIndex = 2
            lblPreprodClientSecret.Text = "Client Secret:"
            ' 
            ' txtPreprodClientSecret
            ' 
            txtPreprodClientSecret.Location = New System.Drawing.Point(15, 83)
            txtPreprodClientSecret.Name = "txtPreprodClientSecret"
            txtPreprodClientSecret.Properties.PasswordChar = "*"c
            txtPreprodClientSecret.Size = New System.Drawing.Size(560, 20)
            txtPreprodClientSecret.TabIndex = 3
            ' 
            ' lblPreprodBaseUrl
            ' 
            lblPreprodBaseUrl.Location = New System.Drawing.Point(15, 115)
            lblPreprodBaseUrl.Name = "lblPreprodBaseUrl"
            lblPreprodBaseUrl.Size = New System.Drawing.Size(69, 13)
            lblPreprodBaseUrl.TabIndex = 4
            lblPreprodBaseUrl.Text = "API Base URL:"
            ' 
            ' txtPreprodBaseUrl
            ' 
            txtPreprodBaseUrl.Location = New System.Drawing.Point(15, 133)
            txtPreprodBaseUrl.Name = "txtPreprodBaseUrl"
            txtPreprodBaseUrl.Size = New System.Drawing.Size(560, 20)
            txtPreprodBaseUrl.TabIndex = 5
            ' 
            ' lblPreprodIdentityUrl
            ' 
            lblPreprodIdentityUrl.Location = New System.Drawing.Point(15, 165)
            lblPreprodIdentityUrl.Name = "lblPreprodIdentityUrl"
            lblPreprodIdentityUrl.Size = New System.Drawing.Size(104, 13)
            lblPreprodIdentityUrl.TabIndex = 6
            lblPreprodIdentityUrl.Text = "Identity (Token) URL:"
            ' 
            ' txtPreprodIdentityUrl
            ' 
            txtPreprodIdentityUrl.Location = New System.Drawing.Point(15, 183)
            txtPreprodIdentityUrl.Name = "txtPreprodIdentityUrl"
            txtPreprodIdentityUrl.Size = New System.Drawing.Size(560, 20)
            txtPreprodIdentityUrl.TabIndex = 7
            ' 
            ' tabProd
            ' 
            tabProd.Controls.Add(lblProdClientId)
            tabProd.Controls.Add(txtProdClientId)
            tabProd.Controls.Add(lblProdClientSecret)
            tabProd.Controls.Add(txtProdClientSecret)
            tabProd.Controls.Add(lblProdBaseUrl)
            tabProd.Controls.Add(txtProdBaseUrl)
            tabProd.Controls.Add(lblProdIdentityUrl)
            tabProd.Controls.Add(txtProdIdentityUrl)
            tabProd.Name = "tabProd"
            tabProd.Padding = New System.Windows.Forms.Padding(10)
            tabProd.Size = New System.Drawing.Size(616, 430)
            tabProd.Text = "Production"
            ' 
            ' lblProdClientId
            ' 
            lblProdClientId.Location = New System.Drawing.Point(15, 15)
            lblProdClientId.Name = "lblProdClientId"
            lblProdClientId.Size = New System.Drawing.Size(45, 13)
            lblProdClientId.TabIndex = 0
            lblProdClientId.Text = "Client ID:"
            ' 
            ' txtProdClientId
            ' 
            txtProdClientId.Location = New System.Drawing.Point(15, 33)
            txtProdClientId.Name = "txtProdClientId"
            txtProdClientId.Size = New System.Drawing.Size(560, 20)
            txtProdClientId.TabIndex = 1
            ' 
            ' lblProdClientSecret
            ' 
            lblProdClientSecret.Location = New System.Drawing.Point(15, 65)
            lblProdClientSecret.Name = "lblProdClientSecret"
            lblProdClientSecret.Size = New System.Drawing.Size(65, 13)
            lblProdClientSecret.TabIndex = 2
            lblProdClientSecret.Text = "Client Secret:"
            ' 
            ' txtProdClientSecret
            ' 
            txtProdClientSecret.Location = New System.Drawing.Point(15, 83)
            txtProdClientSecret.Name = "txtProdClientSecret"
            txtProdClientSecret.Properties.PasswordChar = "*"c
            txtProdClientSecret.Size = New System.Drawing.Size(560, 20)
            txtProdClientSecret.TabIndex = 3
            ' 
            ' lblProdBaseUrl
            ' 
            lblProdBaseUrl.Location = New System.Drawing.Point(15, 115)
            lblProdBaseUrl.Name = "lblProdBaseUrl"
            lblProdBaseUrl.Size = New System.Drawing.Size(69, 13)
            lblProdBaseUrl.TabIndex = 4
            lblProdBaseUrl.Text = "API Base URL:"
            ' 
            ' txtProdBaseUrl
            ' 
            txtProdBaseUrl.Location = New System.Drawing.Point(15, 133)
            txtProdBaseUrl.Name = "txtProdBaseUrl"
            txtProdBaseUrl.Size = New System.Drawing.Size(560, 20)
            txtProdBaseUrl.TabIndex = 5
            ' 
            ' lblProdIdentityUrl
            ' 
            lblProdIdentityUrl.Location = New System.Drawing.Point(15, 165)
            lblProdIdentityUrl.Name = "lblProdIdentityUrl"
            lblProdIdentityUrl.Size = New System.Drawing.Size(104, 13)
            lblProdIdentityUrl.TabIndex = 6
            lblProdIdentityUrl.Text = "Identity (Token) URL:"
            ' 
            ' txtProdIdentityUrl
            ' 
            txtProdIdentityUrl.Location = New System.Drawing.Point(15, 183)
            txtProdIdentityUrl.Name = "txtProdIdentityUrl"
            txtProdIdentityUrl.Size = New System.Drawing.Size(560, 20)
            txtProdIdentityUrl.TabIndex = 7
            ' 
            ' tabTaxpayer
            ' 
            tabTaxpayer.Controls.Add(lblTaxpayerName)
            tabTaxpayer.Controls.Add(txtTaxpayerName)
            tabTaxpayer.Controls.Add(lblDefaultActivityCode)
            tabTaxpayer.Controls.Add(txtDefaultActivityCode)
            tabTaxpayer.Controls.Add(lblDefaultBranchId)
            tabTaxpayer.Controls.Add(txtDefaultBranchId)
            tabTaxpayer.Controls.Add(lblIssuerGovernate)
            tabTaxpayer.Controls.Add(txtIssuerGovernate)
            tabTaxpayer.Controls.Add(lblIssuerRegionCity)
            tabTaxpayer.Controls.Add(txtIssuerRegionCity)
            tabTaxpayer.Controls.Add(lblIssuerStreet)
            tabTaxpayer.Controls.Add(txtIssuerStreet)
            tabTaxpayer.Controls.Add(lblIssuerBuildingNumber)
            tabTaxpayer.Controls.Add(txtIssuerBuildingNumber)
            tabTaxpayer.Name = "tabTaxpayer"
            tabTaxpayer.Padding = New System.Windows.Forms.Padding(10)
            tabTaxpayer.Size = New System.Drawing.Size(618, 455)
            tabTaxpayer.Text = "Taxpayer Info"
            ' 
            ' lblTaxpayerName
            ' 
            lblTaxpayerName.Location = New System.Drawing.Point(15, 20)
            lblTaxpayerName.Name = "lblTaxpayerName"
            lblTaxpayerName.Size = New System.Drawing.Size(190, 13)
            lblTaxpayerName.TabIndex = 0
            lblTaxpayerName.Text = "Registered Company / Taxpayer Name:"
            ' 
            ' txtTaxpayerName
            ' 
            txtTaxpayerName.Location = New System.Drawing.Point(15, 40)
            txtTaxpayerName.Name = "txtTaxpayerName"
            txtTaxpayerName.Size = New System.Drawing.Size(560, 20)
            txtTaxpayerName.TabIndex = 1
            ' 
            ' lblDefaultActivityCode
            ' 
            lblDefaultActivityCode.Location = New System.Drawing.Point(15, 75)
            lblDefaultActivityCode.Name = "lblDefaultActivityCode"
            lblDefaultActivityCode.Size = New System.Drawing.Size(155, 13)
            lblDefaultActivityCode.TabIndex = 2
            lblDefaultActivityCode.Text = "Default Taxpayer Activity Code:"
            ' 
            ' txtDefaultActivityCode
            ' 
            txtDefaultActivityCode.Location = New System.Drawing.Point(15, 95)
            txtDefaultActivityCode.Name = "txtDefaultActivityCode"
            txtDefaultActivityCode.Size = New System.Drawing.Size(250, 20)
            txtDefaultActivityCode.TabIndex = 3
            ' 
            ' lblDefaultBranchId
            ' 
            lblDefaultBranchId.Location = New System.Drawing.Point(290, 75)
            lblDefaultBranchId.Name = "lblDefaultBranchId"
            lblDefaultBranchId.Size = New System.Drawing.Size(89, 13)
            lblDefaultBranchId.TabIndex = 4
            lblDefaultBranchId.Text = "Default Branch ID:"
            ' 
            ' txtDefaultBranchId
            ' 
            txtDefaultBranchId.Location = New System.Drawing.Point(290, 95)
            txtDefaultBranchId.Name = "txtDefaultBranchId"
            txtDefaultBranchId.Size = New System.Drawing.Size(150, 20)
            txtDefaultBranchId.TabIndex = 5
            ' 
            ' lblIssuerGovernate
            ' 
            lblIssuerGovernate.Location = New System.Drawing.Point(15, 130)
            lblIssuerGovernate.Name = "lblIssuerGovernate"
            lblIssuerGovernate.Size = New System.Drawing.Size(92, 13)
            lblIssuerGovernate.TabIndex = 6
            lblIssuerGovernate.Text = "Issuer Governate:"
            ' 
            ' txtIssuerGovernate
            ' 
            txtIssuerGovernate.Location = New System.Drawing.Point(15, 150)
            txtIssuerGovernate.Name = "txtIssuerGovernate"
            txtIssuerGovernate.Size = New System.Drawing.Size(250, 20)
            txtIssuerGovernate.TabIndex = 7
            ' 
            ' lblIssuerRegionCity
            ' 
            lblIssuerRegionCity.Location = New System.Drawing.Point(290, 130)
            lblIssuerRegionCity.Name = "lblIssuerRegionCity"
            lblIssuerRegionCity.Size = New System.Drawing.Size(100, 13)
            lblIssuerRegionCity.TabIndex = 8
            lblIssuerRegionCity.Text = "Issuer Region/City:"
            ' 
            ' txtIssuerRegionCity
            ' 
            txtIssuerRegionCity.Location = New System.Drawing.Point(290, 150)
            txtIssuerRegionCity.Name = "txtIssuerRegionCity"
            txtIssuerRegionCity.Size = New System.Drawing.Size(150, 20)
            txtIssuerRegionCity.TabIndex = 9
            ' 
            ' lblIssuerStreet
            ' 
            lblIssuerStreet.Location = New System.Drawing.Point(15, 185)
            lblIssuerStreet.Name = "lblIssuerStreet"
            lblIssuerStreet.Size = New System.Drawing.Size(70, 13)
            lblIssuerStreet.TabIndex = 10
            lblIssuerStreet.Text = "Issuer Street:"
            ' 
            ' txtIssuerStreet
            ' 
            txtIssuerStreet.Location = New System.Drawing.Point(15, 205)
            txtIssuerStreet.Name = "txtIssuerStreet"
            txtIssuerStreet.Size = New System.Drawing.Size(250, 20)
            txtIssuerStreet.TabIndex = 11
            ' 
            ' lblIssuerBuildingNumber
            ' 
            lblIssuerBuildingNumber.Location = New System.Drawing.Point(290, 185)
            lblIssuerBuildingNumber.Name = "lblIssuerBuildingNumber"
            lblIssuerBuildingNumber.Size = New System.Drawing.Size(119, 13)
            lblIssuerBuildingNumber.TabIndex = 12
            lblIssuerBuildingNumber.Text = "Issuer Building Number:"
            ' 
            ' txtIssuerBuildingNumber
            ' 
            txtIssuerBuildingNumber.Location = New System.Drawing.Point(290, 205)
            txtIssuerBuildingNumber.Name = "txtIssuerBuildingNumber"
            txtIssuerBuildingNumber.Size = New System.Drawing.Size(150, 20)
            txtIssuerBuildingNumber.TabIndex = 13
            ' 
            ' tabNotifications
            ' 
            tabNotifications.Controls.Add(chkNotifyValidation)
            tabNotifications.Controls.Add(chkNotifyIssuance)
            tabNotifications.Controls.Add(chkNotifyRejection)
            tabNotifications.Controls.Add(chkNotifyCancellation)
            tabNotifications.Controls.Add(lblPollingInterval)
            tabNotifications.Controls.Add(numPollingMinutes)
            tabNotifications.Controls.Add(chkDesktopToast)
            tabNotifications.Controls.Add(chkEmailNotifications)
            tabNotifications.Controls.Add(lblNotificationEmail)
            tabNotifications.Controls.Add(txtNotificationEmail)
            tabNotifications.Controls.Add(lblSmtpHost)
            tabNotifications.Controls.Add(txtSmtpHost)
            tabNotifications.Controls.Add(lblSmtpPort)
            tabNotifications.Controls.Add(numSmtpPort)
            tabNotifications.Controls.Add(lblSmtpUsername)
            tabNotifications.Controls.Add(txtSmtpUsername)
            tabNotifications.Controls.Add(lblSmtpPassword)
            tabNotifications.Controls.Add(txtSmtpPassword)
            tabNotifications.Controls.Add(chkSmtpSsl)
            tabNotifications.Name = "tabNotifications"
            tabNotifications.Padding = New System.Windows.Forms.Padding(10)
            tabNotifications.Size = New System.Drawing.Size(618, 455)
            tabNotifications.Text = "Notifications"
            ' 
            ' chkNotifyValidation
            ' 
            chkNotifyValidation.Location = New System.Drawing.Point(15, 15)
            chkNotifyValidation.Name = "chkNotifyValidation"
            chkNotifyValidation.Properties.Caption = "Notify on Validation"
            chkNotifyValidation.Size = New System.Drawing.Size(220, 20)
            chkNotifyValidation.TabIndex = 0
            ' 
            ' chkNotifyIssuance
            ' 
            chkNotifyIssuance.Location = New System.Drawing.Point(245, 15)
            chkNotifyIssuance.Name = "chkNotifyIssuance"
            chkNotifyIssuance.Properties.Caption = "Notify on Issuance"
            chkNotifyIssuance.Size = New System.Drawing.Size(220, 20)
            chkNotifyIssuance.TabIndex = 1
            ' 
            ' chkNotifyRejection
            ' 
            chkNotifyRejection.Location = New System.Drawing.Point(15, 40)
            chkNotifyRejection.Name = "chkNotifyRejection"
            chkNotifyRejection.Properties.Caption = "Notify on Rejection"
            chkNotifyRejection.Size = New System.Drawing.Size(220, 20)
            chkNotifyRejection.TabIndex = 2
            ' 
            ' chkNotifyCancellation
            ' 
            chkNotifyCancellation.Location = New System.Drawing.Point(245, 40)
            chkNotifyCancellation.Name = "chkNotifyCancellation"
            chkNotifyCancellation.Properties.Caption = "Notify on Cancellation"
            chkNotifyCancellation.Size = New System.Drawing.Size(220, 20)
            chkNotifyCancellation.TabIndex = 3
            ' 
            ' lblPollingInterval
            ' 
            lblPollingInterval.Location = New System.Drawing.Point(15, 75)
            lblPollingInterval.Name = "lblPollingInterval"
            lblPollingInterval.Size = New System.Drawing.Size(204, 13)
            lblPollingInterval.TabIndex = 4
            lblPollingInterval.Text = "Check for status updates every (minutes):"
            ' 
            ' numPollingMinutes
            ' 
            numPollingMinutes.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
            numPollingMinutes.Location = New System.Drawing.Point(260, 73)
            numPollingMinutes.Name = "numPollingMinutes"
            numPollingMinutes.Properties.DisplayFormat.FormatString = "N0"
            numPollingMinutes.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            numPollingMinutes.Properties.MaskSettings.Set("mask", "N0")
            numPollingMinutes.Properties.MaxValue = New Decimal(New Integer() {1440, 0, 0, 0})
            numPollingMinutes.Properties.MinValue = New Decimal(New Integer() {1, 0, 0, 0})
            numPollingMinutes.Size = New System.Drawing.Size(70, 20)
            numPollingMinutes.TabIndex = 5
            ' 
            ' chkDesktopToast
            ' 
            chkDesktopToast.Location = New System.Drawing.Point(15, 110)
            chkDesktopToast.Name = "chkDesktopToast"
            chkDesktopToast.Properties.Caption = "Show Windows desktop notifications"
            chkDesktopToast.Size = New System.Drawing.Size(300, 20)
            chkDesktopToast.TabIndex = 6
            ' 
            ' chkEmailNotifications
            ' 
            chkEmailNotifications.Location = New System.Drawing.Point(15, 140)
            chkEmailNotifications.Name = "chkEmailNotifications"
            chkEmailNotifications.Properties.Caption = "Send email notifications"
            chkEmailNotifications.Size = New System.Drawing.Size(300, 20)
            chkEmailNotifications.TabIndex = 7
            ' 
            ' lblNotificationEmail
            ' 
            lblNotificationEmail.Location = New System.Drawing.Point(15, 170)
            lblNotificationEmail.Name = "lblNotificationEmail"
            lblNotificationEmail.Size = New System.Drawing.Size(126, 13)
            lblNotificationEmail.TabIndex = 8
            lblNotificationEmail.Text = "Notification email address:"
            ' 
            ' txtNotificationEmail
            ' 
            txtNotificationEmail.Location = New System.Drawing.Point(15, 190)
            txtNotificationEmail.Name = "txtNotificationEmail"
            txtNotificationEmail.Size = New System.Drawing.Size(560, 20)
            txtNotificationEmail.TabIndex = 9
            ' 
            ' lblSmtpHost
            ' 
            lblSmtpHost.Location = New System.Drawing.Point(15, 225)
            lblSmtpHost.Name = "lblSmtpHost"
            lblSmtpHost.Size = New System.Drawing.Size(55, 13)
            lblSmtpHost.TabIndex = 10
            lblSmtpHost.Text = "SMTP Host:"
            ' 
            ' txtSmtpHost
            ' 
            txtSmtpHost.Location = New System.Drawing.Point(15, 245)
            txtSmtpHost.Name = "txtSmtpHost"
            txtSmtpHost.Size = New System.Drawing.Size(300, 20)
            txtSmtpHost.TabIndex = 11
            ' 
            ' lblSmtpPort
            ' 
            lblSmtpPort.Location = New System.Drawing.Point(330, 225)
            lblSmtpPort.Name = "lblSmtpPort"
            lblSmtpPort.Size = New System.Drawing.Size(24, 13)
            lblSmtpPort.TabIndex = 12
            lblSmtpPort.Text = "Port:"
            ' 
            ' numSmtpPort
            ' 
            numSmtpPort.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
            numSmtpPort.Location = New System.Drawing.Point(330, 245)
            numSmtpPort.Name = "numSmtpPort"
            numSmtpPort.Properties.DisplayFormat.FormatString = "N0"
            numSmtpPort.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            numSmtpPort.Properties.MaskSettings.Set("mask", "N0")
            numSmtpPort.Properties.MaxValue = New Decimal(New Integer() {65535, 0, 0, 0})
            numSmtpPort.Properties.MinValue = New Decimal(New Integer() {1, 0, 0, 0})
            numSmtpPort.Size = New System.Drawing.Size(80, 20)
            numSmtpPort.TabIndex = 13
            ' 
            ' lblSmtpUsername
            ' 
            lblSmtpUsername.Location = New System.Drawing.Point(15, 280)
            lblSmtpUsername.Name = "lblSmtpUsername"
            lblSmtpUsername.Size = New System.Drawing.Size(81, 13)
            lblSmtpUsername.TabIndex = 14
            lblSmtpUsername.Text = "SMTP Username:"
            ' 
            ' txtSmtpUsername
            ' 
            txtSmtpUsername.Location = New System.Drawing.Point(15, 300)
            txtSmtpUsername.Name = "txtSmtpUsername"
            txtSmtpUsername.Size = New System.Drawing.Size(300, 20)
            txtSmtpUsername.TabIndex = 15
            ' 
            ' lblSmtpPassword
            ' 
            lblSmtpPassword.Location = New System.Drawing.Point(330, 280)
            lblSmtpPassword.Name = "lblSmtpPassword"
            lblSmtpPassword.Size = New System.Drawing.Size(79, 13)
            lblSmtpPassword.TabIndex = 16
            lblSmtpPassword.Text = "SMTP Password:"
            ' 
            ' txtSmtpPassword
            ' 
            txtSmtpPassword.Location = New System.Drawing.Point(330, 300)
            txtSmtpPassword.Name = "txtSmtpPassword"
            txtSmtpPassword.Properties.PasswordChar = "*"c
            txtSmtpPassword.Size = New System.Drawing.Size(245, 20)
            txtSmtpPassword.TabIndex = 17
            ' 
            ' chkSmtpSsl
            ' 
            chkSmtpSsl.Location = New System.Drawing.Point(15, 335)
            chkSmtpSsl.Name = "chkSmtpSsl"
            chkSmtpSsl.Properties.Caption = "Use SSL/TLS"
            chkSmtpSsl.Size = New System.Drawing.Size(200, 20)
            chkSmtpSsl.TabIndex = 18
            ' 
            ' tabDatabase
            ' 
            tabDatabase.Controls.Add(chkDbUseSqlServer)
            tabDatabase.Controls.Add(chkDbUseLocalDb)
            tabDatabase.Controls.Add(gbDbSqlServer)
            tabDatabase.Controls.Add(gbDbLocalDb)
            tabDatabase.Controls.Add(btnTestConnection)
            tabDatabase.Controls.Add(lblDbStatus)
            tabDatabase.Name = "tabDatabase"
            tabDatabase.Padding = New System.Windows.Forms.Padding(10)
            tabDatabase.Size = New System.Drawing.Size(618, 455)
            tabDatabase.Text = "Database"
            ' 
            ' chkDbUseSqlServer
            ' 
            chkDbUseSqlServer.Location = New System.Drawing.Point(15, 15)
            chkDbUseSqlServer.Name = "chkDbUseSqlServer"
            chkDbUseSqlServer.Properties.Caption = "SQL Server"
            chkDbUseSqlServer.Properties.RadioGroupIndex = 1
            chkDbUseSqlServer.Size = New System.Drawing.Size(140, 20)
            chkDbUseSqlServer.TabIndex = 0
            chkDbUseSqlServer.TabStop = False
            ' 
            ' chkDbUseLocalDb
            ' 
            chkDbUseLocalDb.Location = New System.Drawing.Point(160, 15)
            chkDbUseLocalDb.Name = "chkDbUseLocalDb"
            chkDbUseLocalDb.Properties.Caption = "LocalDB"
            chkDbUseLocalDb.Properties.RadioGroupIndex = 1
            chkDbUseLocalDb.Size = New System.Drawing.Size(140, 20)
            chkDbUseLocalDb.TabIndex = 1
            chkDbUseLocalDb.TabStop = False
            ' 
            ' gbDbSqlServer
            ' 
            gbDbSqlServer.Controls.Add(lblDbServer)
            gbDbSqlServer.Controls.Add(txtDbServer)
            gbDbSqlServer.Controls.Add(lblDbPort)
            gbDbSqlServer.Controls.Add(txtDbPort)
            gbDbSqlServer.Controls.Add(lblDbUsername)
            gbDbSqlServer.Controls.Add(txtDbUsername)
            gbDbSqlServer.Controls.Add(lblDbPassword)
            gbDbSqlServer.Controls.Add(txtDbPassword)
            gbDbSqlServer.Location = New System.Drawing.Point(15, 45)
            gbDbSqlServer.Name = "gbDbSqlServer"
            gbDbSqlServer.Size = New System.Drawing.Size(590, 145)
            gbDbSqlServer.TabIndex = 2
            gbDbSqlServer.Text = "SQL Server Connection"
            ' 
            ' lblDbServer
            ' 
            lblDbServer.Location = New System.Drawing.Point(15, 30)
            lblDbServer.Name = "lblDbServer"
            lblDbServer.Size = New System.Drawing.Size(81, 13)
            lblDbServer.TabIndex = 0
            lblDbServer.Text = "Server IP / Host:"
            ' 
            ' txtDbServer
            ' 
            txtDbServer.Location = New System.Drawing.Point(130, 28)
            txtDbServer.Name = "txtDbServer"
            txtDbServer.Size = New System.Drawing.Size(280, 20)
            txtDbServer.TabIndex = 1
            ' 
            ' lblDbPort
            ' 
            lblDbPort.Location = New System.Drawing.Point(425, 30)
            lblDbPort.Name = "lblDbPort"
            lblDbPort.Size = New System.Drawing.Size(24, 13)
            lblDbPort.TabIndex = 2
            lblDbPort.Text = "Port:"
            ' 
            ' txtDbPort
            ' 
            txtDbPort.Location = New System.Drawing.Point(465, 28)
            txtDbPort.Name = "txtDbPort"
            txtDbPort.Size = New System.Drawing.Size(80, 20)
            txtDbPort.TabIndex = 3
            ' 
            ' lblDbUsername
            ' 
            lblDbUsername.Location = New System.Drawing.Point(15, 65)
            lblDbUsername.Name = "lblDbUsername"
            lblDbUsername.Size = New System.Drawing.Size(52, 13)
            lblDbUsername.TabIndex = 4
            lblDbUsername.Text = "Username:"
            ' 
            ' txtDbUsername
            ' 
            txtDbUsername.Location = New System.Drawing.Point(130, 63)
            txtDbUsername.Name = "txtDbUsername"
            txtDbUsername.Size = New System.Drawing.Size(280, 20)
            txtDbUsername.TabIndex = 5
            ' 
            ' lblDbPassword
            ' 
            lblDbPassword.Location = New System.Drawing.Point(15, 100)
            lblDbPassword.Name = "lblDbPassword"
            lblDbPassword.Size = New System.Drawing.Size(50, 13)
            lblDbPassword.TabIndex = 6
            lblDbPassword.Text = "Password:"
            ' 
            ' txtDbPassword
            ' 
            txtDbPassword.Location = New System.Drawing.Point(130, 98)
            txtDbPassword.Name = "txtDbPassword"
            txtDbPassword.Properties.PasswordChar = "*"c
            txtDbPassword.Size = New System.Drawing.Size(280, 20)
            txtDbPassword.TabIndex = 7
            ' 
            ' gbDbLocalDb
            ' 
            gbDbLocalDb.Controls.Add(lblDbLocalDbInfo)
            gbDbLocalDb.Location = New System.Drawing.Point(15, 45)
            gbDbLocalDb.Name = "gbDbLocalDb"
            gbDbLocalDb.Size = New System.Drawing.Size(590, 100)
            gbDbLocalDb.TabIndex = 3
            gbDbLocalDb.Text = "LocalDB"
            ' 
            ' lblDbLocalDbInfo
            ' 
            lblDbLocalDbInfo.Location = New System.Drawing.Point(15, 30)
            lblDbLocalDbInfo.Name = "lblDbLocalDbInfo"
            lblDbLocalDbInfo.Size = New System.Drawing.Size(588, 13)
            lblDbLocalDbInfo.TabIndex = 0
            lblDbLocalDbInfo.Text = "LocalDB will auto-create the database on the local (localdb)\MSSQLLocalDB instance. No additional configuration is needed."
            ' 
            ' btnTestConnection
            ' 
            btnTestConnection.Location = New System.Drawing.Point(15, 205)
            btnTestConnection.Name = "btnTestConnection"
            btnTestConnection.Size = New System.Drawing.Size(150, 30)
            btnTestConnection.TabIndex = 4
            btnTestConnection.Text = "Test Connection"
            ' 
            ' lblDbStatus
            ' 
            lblDbStatus.Location = New System.Drawing.Point(180, 210)
            lblDbStatus.Name = "lblDbStatus"
            lblDbStatus.Size = New System.Drawing.Size(0, 13)
            lblDbStatus.TabIndex = 5
            ' 
            ' tabSubscription
            ' 
            tabSubscription.Name = "tabSubscription"
            tabSubscription.Padding = New System.Windows.Forms.Padding(10)
            tabSubscription.Size = New System.Drawing.Size(618, 455)
            tabSubscription.Text = "Subscription"
            ' 
            ' gbSubInfo
            ' 
            gbSubInfo.Controls.Add(lblSubLastCheckLabel)
            gbSubInfo.Controls.Add(lblSubLastCheck)
            gbSubInfo.Controls.Add(lblSubStatusLabel)
            gbSubInfo.Controls.Add(lblSubStatus)
            gbSubInfo.Controls.Add(lblSubExpiryLabel)
            gbSubInfo.Controls.Add(lblSubExpiry)
            gbSubInfo.Controls.Add(lblSubPlanLabel)
            gbSubInfo.Controls.Add(lblSubPlan)
            gbSubInfo.Controls.Add(lblSubCustomerLabel)
            gbSubInfo.Controls.Add(lblSubCustomer)
            gbSubInfo.Controls.Add(lblSubKeyLabel)
            gbSubInfo.Controls.Add(lblSubKey)
            gbSubInfo.Location = New System.Drawing.Point(15, 15)
            gbSubInfo.Name = "gbSubInfo"
            gbSubInfo.Size = New System.Drawing.Size(575, 340)
            gbSubInfo.TabIndex = 0
            gbSubInfo.Text = "Subscription Information"
            ' 
            ' lblSubKeyLabel
            ' 
            lblSubKeyLabel.Location = New System.Drawing.Point(15, 30)
            lblSubKeyLabel.Name = "lblSubKeyLabel"
            lblSubKeyLabel.Size = New System.Drawing.Size(64, 13)
            lblSubKeyLabel.TabIndex = 0
            lblSubKeyLabel.Text = "License Key:"
            ' 
            ' lblSubKey
            ' 
            lblSubKey.Appearance.Font = New System.Drawing.Font("Consolas", 9.75F, Drawing.FontStyle.Regular, Drawing.GraphicsUnit.Point)
            lblSubKey.Appearance.Options.UseFont = True
            lblSubKey.Location = New System.Drawing.Point(130, 28)
            lblSubKey.Name = "lblSubKey"
            lblSubKey.Size = New System.Drawing.Size(0, 16)
            lblSubKey.TabIndex = 1
            ' 
            ' lblSubCustomerLabel
            ' 
            lblSubCustomerLabel.Location = New System.Drawing.Point(15, 60)
            lblSubCustomerLabel.Name = "lblSubCustomerLabel"
            lblSubCustomerLabel.Size = New System.Drawing.Size(58, 13)
            lblSubCustomerLabel.TabIndex = 2
            lblSubCustomerLabel.Text = "Customer:"
            ' 
            ' lblSubCustomer
            ' 
            lblSubCustomer.Location = New System.Drawing.Point(130, 58)
            lblSubCustomer.Name = "lblSubCustomer"
            lblSubCustomer.Size = New System.Drawing.Size(0, 13)
            lblSubCustomer.TabIndex = 3
            ' 
            ' lblSubPlanLabel
            ' 
            lblSubPlanLabel.Location = New System.Drawing.Point(15, 90)
            lblSubPlanLabel.Name = "lblSubPlanLabel"
            lblSubPlanLabel.Size = New System.Drawing.Size(26, 13)
            lblSubPlanLabel.TabIndex = 4
            lblSubPlanLabel.Text = "Plan:"
            ' 
            ' lblSubPlan
            ' 
            lblSubPlan.Appearance.Font = New System.Drawing.Font("Segoe UI", 9F, Drawing.FontStyle.Bold, Drawing.GraphicsUnit.Point)
            lblSubPlan.Appearance.Options.UseFont = True
            lblSubPlan.Location = New System.Drawing.Point(130, 88)
            lblSubPlan.Name = "lblSubPlan"
            lblSubPlan.Size = New System.Drawing.Size(0, 15)
            lblSubPlan.TabIndex = 5
            ' 
            ' lblSubExpiryLabel
            ' 
            lblSubExpiryLabel.Location = New System.Drawing.Point(15, 120)
            lblSubExpiryLabel.Name = "lblSubExpiryLabel"
            lblSubExpiryLabel.Size = New System.Drawing.Size(34, 13)
            lblSubExpiryLabel.TabIndex = 6
            lblSubExpiryLabel.Text = "Expiry:"
            ' 
            ' lblSubExpiry
            ' 
            lblSubExpiry.Location = New System.Drawing.Point(130, 118)
            lblSubExpiry.Name = "lblSubExpiry"
            lblSubExpiry.Size = New System.Drawing.Size(0, 13)
            lblSubExpiry.TabIndex = 7
            ' 
            ' lblSubStatusLabel
            ' 
            lblSubStatusLabel.Location = New System.Drawing.Point(15, 150)
            lblSubStatusLabel.Name = "lblSubStatusLabel"
            lblSubStatusLabel.Size = New System.Drawing.Size(39, 13)
            lblSubStatusLabel.TabIndex = 8
            lblSubStatusLabel.Text = "Status:"
            ' 
            ' lblSubStatus
            ' 
            lblSubStatus.Appearance.Font = New System.Drawing.Font("Segoe UI", 9F, Drawing.FontStyle.Bold, Drawing.GraphicsUnit.Point)
            lblSubStatus.Appearance.Options.UseFont = True
            lblSubStatus.Location = New System.Drawing.Point(130, 148)
            lblSubStatus.Name = "lblSubStatus"
            lblSubStatus.Size = New System.Drawing.Size(0, 15)
            lblSubStatus.TabIndex = 9
            ' 
            ' lblSubLastCheckLabel
            ' 
            lblSubLastCheckLabel.Location = New System.Drawing.Point(15, 180)
            lblSubLastCheckLabel.Name = "lblSubLastCheckLabel"
            lblSubLastCheckLabel.Size = New System.Drawing.Size(62, 13)
            lblSubLastCheckLabel.TabIndex = 10
            lblSubLastCheckLabel.Text = "Last Check:"
            ' 
            ' lblSubLastCheck
            ' 
            lblSubLastCheck.Location = New System.Drawing.Point(130, 178)
            lblSubLastCheck.Name = "lblSubLastCheck"
            lblSubLastCheck.Size = New System.Drawing.Size(0, 13)
            lblSubLastCheck.TabIndex = 11


            ' 
            ' btnSubRefresh
            ' 
            btnSubRefresh.Location = New System.Drawing.Point(25, 370)
            btnSubRefresh.Name = "btnSubRefresh"
            btnSubRefresh.Size = New System.Drawing.Size(140, 30)
            btnSubRefresh.TabIndex = 1
            btnSubRefresh.Text = "Refresh from Server"
            ' 
            ' btnSubActivate
            ' 
            btnSubActivate.Appearance.BackColor = Drawing.Color.FromArgb(CByte(20), CByte(110), CByte(70))
            btnSubActivate.Appearance.Font = New System.Drawing.Font("Segoe UI", 9F, Drawing.FontStyle.Bold)
            btnSubActivate.Appearance.ForeColor = Drawing.Color.White
            btnSubActivate.Appearance.Options.UseBackColor = True
            btnSubActivate.Appearance.Options.UseFont = True
            btnSubActivate.Appearance.Options.UseForeColor = True
            btnSubActivate.Location = New System.Drawing.Point(180, 370)
            btnSubActivate.Name = "btnSubActivate"
            btnSubActivate.Size = New System.Drawing.Size(170, 30)
            btnSubActivate.TabIndex = 2
            btnSubActivate.Text = "Activate / Change License"
            tabSubscription.Controls.Add(gbSubInfo)
            tabSubscription.Controls.Add(btnSubRefresh)
            tabSubscription.Controls.Add(btnSubActivate)
            ' 
            ' btnSave
            ' 
            btnSave.Appearance.BackColor = Drawing.Color.FromArgb(CByte(20), CByte(110), CByte(70))
            btnSave.Appearance.Font = New System.Drawing.Font("Segoe UI", 9.5F, Drawing.FontStyle.Bold)
            btnSave.Appearance.ForeColor = Drawing.Color.White
            btnSave.Appearance.Options.UseBackColor = True
            btnSave.Appearance.Options.UseFont = True
            btnSave.Appearance.Options.UseForeColor = True
            btnSave.Location = New System.Drawing.Point(432, 500)
            btnSave.Name = "btnSave"
            btnSave.Size = New System.Drawing.Size(100, 35)
            btnSave.TabIndex = 1
            btnSave.Text = "Save"
            ' 
            ' btnCancel
            ' 
            btnCancel.Location = New System.Drawing.Point(532, 500)
            btnCancel.Name = "btnCancel"
            btnCancel.Size = New System.Drawing.Size(100, 35)
            btnCancel.TabIndex = 2
            btnCancel.Text = "Cancel"
            ' 
            ' SettingsForm
            ' 
            AutoScaleDimensions = New System.Drawing.SizeF(96F, 96F)
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
            ClientSize = New System.Drawing.Size(648, 550)
            Controls.Add(tabControl)
            Controls.Add(btnSave)
            Controls.Add(btnCancel)
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            MaximizeBox = False
            Name = "SettingsForm"
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Text = "Settings"
            CType(tabControl, ComponentModel.ISupportInitialize).EndInit()
            tabControl.ResumeLayout(False)
            tabCertificate.ResumeLayout(False)
            tabCertificate.PerformLayout()
            CType(cmbCertificates.Properties, ComponentModel.ISupportInitialize).EndInit()
            tabCredentials.ResumeLayout(False)
            CType(envTabControl, ComponentModel.ISupportInitialize).EndInit()
            envTabControl.ResumeLayout(False)
            tabPreprod.ResumeLayout(False)
            tabPreprod.PerformLayout()
            CType(txtPreprodClientId.Properties, ComponentModel.ISupportInitialize).EndInit()
            CType(txtPreprodClientSecret.Properties, ComponentModel.ISupportInitialize).EndInit()
            CType(txtPreprodBaseUrl.Properties, ComponentModel.ISupportInitialize).EndInit()
            CType(txtPreprodIdentityUrl.Properties, ComponentModel.ISupportInitialize).EndInit()
            tabProd.ResumeLayout(False)
            tabProd.PerformLayout()
            CType(txtProdClientId.Properties, ComponentModel.ISupportInitialize).EndInit()
            CType(txtProdClientSecret.Properties, ComponentModel.ISupportInitialize).EndInit()
            CType(txtProdBaseUrl.Properties, ComponentModel.ISupportInitialize).EndInit()
            CType(txtProdIdentityUrl.Properties, ComponentModel.ISupportInitialize).EndInit()
            tabTaxpayer.ResumeLayout(False)
            tabTaxpayer.PerformLayout()
            CType(txtTaxpayerName.Properties, ComponentModel.ISupportInitialize).EndInit()
            CType(txtDefaultActivityCode.Properties, ComponentModel.ISupportInitialize).EndInit()
            CType(txtDefaultBranchId.Properties, ComponentModel.ISupportInitialize).EndInit()
            CType(txtIssuerGovernate.Properties, ComponentModel.ISupportInitialize).EndInit()
            CType(txtIssuerRegionCity.Properties, ComponentModel.ISupportInitialize).EndInit()
            CType(txtIssuerStreet.Properties, ComponentModel.ISupportInitialize).EndInit()
            CType(txtIssuerBuildingNumber.Properties, ComponentModel.ISupportInitialize).EndInit()
            tabNotifications.ResumeLayout(False)
            tabNotifications.PerformLayout()
            CType(chkNotifyValidation.Properties, ComponentModel.ISupportInitialize).EndInit()
            CType(chkNotifyIssuance.Properties, ComponentModel.ISupportInitialize).EndInit()
            CType(chkNotifyRejection.Properties, ComponentModel.ISupportInitialize).EndInit()
            CType(chkNotifyCancellation.Properties, ComponentModel.ISupportInitialize).EndInit()
            CType(numPollingMinutes.Properties, ComponentModel.ISupportInitialize).EndInit()
            CType(chkDesktopToast.Properties, ComponentModel.ISupportInitialize).EndInit()
            CType(chkEmailNotifications.Properties, ComponentModel.ISupportInitialize).EndInit()
            CType(txtNotificationEmail.Properties, ComponentModel.ISupportInitialize).EndInit()
            CType(txtSmtpHost.Properties, ComponentModel.ISupportInitialize).EndInit()
            CType(numSmtpPort.Properties, ComponentModel.ISupportInitialize).EndInit()
            CType(txtSmtpUsername.Properties, ComponentModel.ISupportInitialize).EndInit()
            CType(txtSmtpPassword.Properties, ComponentModel.ISupportInitialize).EndInit()
            CType(chkSmtpSsl.Properties, ComponentModel.ISupportInitialize).EndInit()
            tabDatabase.ResumeLayout(False)
            tabDatabase.PerformLayout()
            CType(chkDbUseSqlServer.Properties, ComponentModel.ISupportInitialize).EndInit()
            CType(chkDbUseLocalDb.Properties, ComponentModel.ISupportInitialize).EndInit()
            CType(gbDbSqlServer, ComponentModel.ISupportInitialize).EndInit()
            gbDbSqlServer.ResumeLayout(False)
            gbDbSqlServer.PerformLayout()
            CType(txtDbServer.Properties, ComponentModel.ISupportInitialize).EndInit()
            CType(txtDbPort.Properties, ComponentModel.ISupportInitialize).EndInit()
            CType(txtDbUsername.Properties, ComponentModel.ISupportInitialize).EndInit()
            CType(txtDbPassword.Properties, ComponentModel.ISupportInitialize).EndInit()
            CType(gbDbLocalDb, ComponentModel.ISupportInitialize).EndInit()
            gbDbLocalDb.ResumeLayout(False)
            gbDbLocalDb.PerformLayout()
            CType(gbSubInfo, ComponentModel.ISupportInitialize).EndInit()
            gbSubInfo.ResumeLayout(False)
            gbSubInfo.PerformLayout()
            tabSubscription.ResumeLayout(False)
            tabSubscription.PerformLayout()
            ResumeLayout(False)
        End Sub

        Friend WithEvents tabControl As DevExpress.XtraTab.XtraTabControl
        Friend WithEvents tabCredentials As DevExpress.XtraTab.XtraTabPage
        Friend WithEvents tabCertificate As DevExpress.XtraTab.XtraTabPage
        Friend WithEvents tabTaxpayer As DevExpress.XtraTab.XtraTabPage
        Friend WithEvents tabNotifications As DevExpress.XtraTab.XtraTabPage
        Friend WithEvents tabDatabase As DevExpress.XtraTab.XtraTabPage
        Friend WithEvents btnSave As DevExpress.XtraEditors.SimpleButton
        Friend WithEvents btnCancel As DevExpress.XtraEditors.SimpleButton

        Friend WithEvents envTabControl As DevExpress.XtraTab.XtraTabControl
        Friend WithEvents tabPreprod As DevExpress.XtraTab.XtraTabPage
        Friend WithEvents tabProd As DevExpress.XtraTab.XtraTabPage
        Friend WithEvents lblPreprodClientId As DevExpress.XtraEditors.LabelControl
        Friend WithEvents txtPreprodClientId As DevExpress.XtraEditors.TextEdit
        Friend WithEvents lblPreprodClientSecret As DevExpress.XtraEditors.LabelControl
        Friend WithEvents txtPreprodClientSecret As DevExpress.XtraEditors.TextEdit
        Friend WithEvents lblPreprodBaseUrl As DevExpress.XtraEditors.LabelControl
        Friend WithEvents txtPreprodBaseUrl As DevExpress.XtraEditors.TextEdit
        Friend WithEvents lblPreprodIdentityUrl As DevExpress.XtraEditors.LabelControl
        Friend WithEvents txtPreprodIdentityUrl As DevExpress.XtraEditors.TextEdit
        Friend WithEvents lblProdClientId As DevExpress.XtraEditors.LabelControl
        Friend WithEvents txtProdClientId As DevExpress.XtraEditors.TextEdit
        Friend WithEvents lblProdClientSecret As DevExpress.XtraEditors.LabelControl
        Friend WithEvents txtProdClientSecret As DevExpress.XtraEditors.TextEdit
        Friend WithEvents lblProdBaseUrl As DevExpress.XtraEditors.LabelControl
        Friend WithEvents txtProdBaseUrl As DevExpress.XtraEditors.TextEdit
        Friend WithEvents lblProdIdentityUrl As DevExpress.XtraEditors.LabelControl
        Friend WithEvents txtProdIdentityUrl As DevExpress.XtraEditors.TextEdit

        Friend WithEvents lblCertInstructions As DevExpress.XtraEditors.LabelControl
        Friend WithEvents cmbCertificates As DevExpress.XtraEditors.ComboBoxEdit
        Friend WithEvents btnRefreshCertificates As DevExpress.XtraEditors.SimpleButton
        Friend WithEvents lblCertDetails As DevExpress.XtraEditors.LabelControl
        Friend WithEvents btnTestCertificate As DevExpress.XtraEditors.SimpleButton

        Friend WithEvents lblTaxpayerName As DevExpress.XtraEditors.LabelControl
        Friend WithEvents txtTaxpayerName As DevExpress.XtraEditors.TextEdit
        Friend WithEvents lblDefaultActivityCode As DevExpress.XtraEditors.LabelControl
        Friend WithEvents txtDefaultActivityCode As DevExpress.XtraEditors.TextEdit
        Friend WithEvents lblDefaultBranchId As DevExpress.XtraEditors.LabelControl
        Friend WithEvents txtDefaultBranchId As DevExpress.XtraEditors.TextEdit
        Friend WithEvents lblIssuerGovernate As DevExpress.XtraEditors.LabelControl
        Friend WithEvents txtIssuerGovernate As DevExpress.XtraEditors.TextEdit
        Friend WithEvents lblIssuerRegionCity As DevExpress.XtraEditors.LabelControl
        Friend WithEvents txtIssuerRegionCity As DevExpress.XtraEditors.TextEdit
        Friend WithEvents lblIssuerStreet As DevExpress.XtraEditors.LabelControl
        Friend WithEvents txtIssuerStreet As DevExpress.XtraEditors.TextEdit
        Friend WithEvents lblIssuerBuildingNumber As DevExpress.XtraEditors.LabelControl
        Friend WithEvents txtIssuerBuildingNumber As DevExpress.XtraEditors.TextEdit

        Friend WithEvents chkNotifyValidation As DevExpress.XtraEditors.CheckEdit
        Friend WithEvents chkNotifyIssuance As DevExpress.XtraEditors.CheckEdit
        Friend WithEvents chkNotifyRejection As DevExpress.XtraEditors.CheckEdit
        Friend WithEvents chkNotifyCancellation As DevExpress.XtraEditors.CheckEdit
        Friend WithEvents lblPollingInterval As DevExpress.XtraEditors.LabelControl
        Friend WithEvents numPollingMinutes As DevExpress.XtraEditors.SpinEdit
        Friend WithEvents chkDesktopToast As DevExpress.XtraEditors.CheckEdit
        Friend WithEvents chkEmailNotifications As DevExpress.XtraEditors.CheckEdit
        Friend WithEvents lblNotificationEmail As DevExpress.XtraEditors.LabelControl
        Friend WithEvents txtNotificationEmail As DevExpress.XtraEditors.TextEdit
        Friend WithEvents lblSmtpHost As DevExpress.XtraEditors.LabelControl
        Friend WithEvents txtSmtpHost As DevExpress.XtraEditors.TextEdit
        Friend WithEvents lblSmtpPort As DevExpress.XtraEditors.LabelControl
        Friend WithEvents numSmtpPort As DevExpress.XtraEditors.SpinEdit
        Friend WithEvents lblSmtpUsername As DevExpress.XtraEditors.LabelControl
        Friend WithEvents txtSmtpUsername As DevExpress.XtraEditors.TextEdit
        Friend WithEvents lblSmtpPassword As DevExpress.XtraEditors.LabelControl
        Friend WithEvents txtSmtpPassword As DevExpress.XtraEditors.TextEdit
        Friend WithEvents chkSmtpSsl As DevExpress.XtraEditors.CheckEdit

        Friend WithEvents chkDbUseSqlServer As DevExpress.XtraEditors.CheckEdit
        Friend WithEvents chkDbUseLocalDb As DevExpress.XtraEditors.CheckEdit
        Friend WithEvents gbDbSqlServer As DevExpress.XtraEditors.GroupControl
        Friend WithEvents lblDbServer As DevExpress.XtraEditors.LabelControl
        Friend WithEvents txtDbServer As DevExpress.XtraEditors.TextEdit
        Friend WithEvents lblDbPort As DevExpress.XtraEditors.LabelControl
        Friend WithEvents txtDbPort As DevExpress.XtraEditors.TextEdit
        Friend WithEvents lblDbUsername As DevExpress.XtraEditors.LabelControl
        Friend WithEvents txtDbUsername As DevExpress.XtraEditors.TextEdit
        Friend WithEvents lblDbPassword As DevExpress.XtraEditors.LabelControl
        Friend WithEvents txtDbPassword As DevExpress.XtraEditors.TextEdit
        Friend WithEvents gbDbLocalDb As DevExpress.XtraEditors.GroupControl
        Friend WithEvents lblDbLocalDbInfo As DevExpress.XtraEditors.LabelControl
        Friend WithEvents btnTestConnection As DevExpress.XtraEditors.SimpleButton
        Friend WithEvents lblDbStatus As DevExpress.XtraEditors.LabelControl

        Friend WithEvents tabSubscription As DevExpress.XtraTab.XtraTabPage
        Friend WithEvents gbSubInfo As DevExpress.XtraEditors.GroupControl
        Friend WithEvents lblSubKeyLabel As DevExpress.XtraEditors.LabelControl
        Friend WithEvents lblSubKey As DevExpress.XtraEditors.LabelControl
        Friend WithEvents lblSubCustomerLabel As DevExpress.XtraEditors.LabelControl
        Friend WithEvents lblSubCustomer As DevExpress.XtraEditors.LabelControl
        Friend WithEvents lblSubPlanLabel As DevExpress.XtraEditors.LabelControl
        Friend WithEvents lblSubPlan As DevExpress.XtraEditors.LabelControl
        Friend WithEvents lblSubExpiryLabel As DevExpress.XtraEditors.LabelControl
        Friend WithEvents lblSubExpiry As DevExpress.XtraEditors.LabelControl
        Friend WithEvents lblSubStatusLabel As DevExpress.XtraEditors.LabelControl
        Friend WithEvents lblSubStatus As DevExpress.XtraEditors.LabelControl
        Friend WithEvents lblSubLastCheckLabel As DevExpress.XtraEditors.LabelControl
        Friend WithEvents lblSubLastCheck As DevExpress.XtraEditors.LabelControl
        Friend WithEvents lblSubServerUrlLabel As DevExpress.XtraEditors.LabelControl
        Friend WithEvents lblSubServerUrl As DevExpress.XtraEditors.LabelControl
        Friend WithEvents btnSubRefresh As DevExpress.XtraEditors.SimpleButton
        Friend WithEvents btnSubActivate As DevExpress.XtraEditors.SimpleButton
    End Class
End Namespace
