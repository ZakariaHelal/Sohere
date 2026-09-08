Namespace Forms
    Partial Class LoginForm
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
            pnlMain = New DevExpress.XtraEditors.PanelControl()
            lblTitle = New DevExpress.XtraEditors.LabelControl()
            lblSubtitle = New DevExpress.XtraEditors.LabelControl()
            grpEnvironment = New DevExpress.XtraEditors.GroupControl()
            rbPreproduction = New DevExpress.XtraEditors.CheckEdit()
            rbProduction = New DevExpress.XtraEditors.CheckEdit()
            lblClientId = New DevExpress.XtraEditors.LabelControl()
            txtClientId = New DevExpress.XtraEditors.TextEdit()
            lblClientSecret = New DevExpress.XtraEditors.LabelControl()
            txtClientSecret = New DevExpress.XtraEditors.TextEdit()
            lblTaxpayerRIN = New DevExpress.XtraEditors.LabelControl()
            txtTaxpayerRIN = New DevExpress.XtraEditors.TextEdit()
            lnkSaveCredentials = New DevExpress.XtraEditors.CheckEdit()
            btnLogin = New DevExpress.XtraEditors.SimpleButton()
            btnSettings = New DevExpress.XtraEditors.SimpleButton()
            lblStatus = New DevExpress.XtraEditors.LabelControl()
            progressBar = New DevExpress.XtraEditors.ProgressBarControl()
            CType(pnlMain, ComponentModel.ISupportInitialize).BeginInit()
            pnlMain.SuspendLayout()
            CType(grpEnvironment, ComponentModel.ISupportInitialize).BeginInit()
            grpEnvironment.SuspendLayout()
            CType(rbPreproduction.Properties, ComponentModel.ISupportInitialize).BeginInit()
            CType(rbProduction.Properties, ComponentModel.ISupportInitialize).BeginInit()
            CType(txtClientId.Properties, ComponentModel.ISupportInitialize).BeginInit()
            CType(txtClientSecret.Properties, ComponentModel.ISupportInitialize).BeginInit()
            CType(txtTaxpayerRIN.Properties, ComponentModel.ISupportInitialize).BeginInit()
            CType(lnkSaveCredentials.Properties, ComponentModel.ISupportInitialize).BeginInit()
            CType(progressBar.Properties, ComponentModel.ISupportInitialize).BeginInit()
            SuspendLayout()
            ' 
            ' pnlMain
            ' 
            pnlMain.Appearance.BackColor = Drawing.Color.White
            pnlMain.Appearance.Options.UseBackColor = True
            pnlMain.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
            pnlMain.Controls.Add(lblTitle)
            pnlMain.Controls.Add(lblSubtitle)
            pnlMain.Controls.Add(grpEnvironment)
            pnlMain.Controls.Add(lblClientId)
            pnlMain.Controls.Add(txtClientId)
            pnlMain.Controls.Add(lblClientSecret)
            pnlMain.Controls.Add(txtClientSecret)
            pnlMain.Controls.Add(lblTaxpayerRIN)
            pnlMain.Controls.Add(txtTaxpayerRIN)
            pnlMain.Controls.Add(lnkSaveCredentials)
            pnlMain.Controls.Add(btnLogin)
            pnlMain.Controls.Add(btnSettings)
            pnlMain.Controls.Add(lblStatus)
            pnlMain.Controls.Add(progressBar)
            pnlMain.Location = New System.Drawing.Point(0, 0)
            pnlMain.Name = "pnlMain"
            pnlMain.Size = New System.Drawing.Size(480, 560)
            pnlMain.TabIndex = 0
            ' 
            ' lblTitle
            ' 
            lblTitle.Appearance.Font = New System.Drawing.Font("Segoe UI", 18F, Drawing.FontStyle.Bold)
            lblTitle.Appearance.ForeColor = Drawing.Color.FromArgb(CByte(20), CByte(60), CByte(110))
            lblTitle.Appearance.Options.UseFont = True
            lblTitle.Appearance.Options.UseForeColor = True
            lblTitle.Location = New System.Drawing.Point(40, 30)
            lblTitle.Name = "lblTitle"
            lblTitle.Size = New System.Drawing.Size(230, 32)
            lblTitle.TabIndex = 0
            lblTitle.Text = "Sphere ERP Desktop"
            ' 
            ' lblSubtitle
            ' 
            lblSubtitle.Appearance.Font = New System.Drawing.Font("Segoe UI", 9.75F)
            lblSubtitle.Appearance.ForeColor = Drawing.Color.Gray
            lblSubtitle.Appearance.Options.UseFont = True
            lblSubtitle.Appearance.Options.UseForeColor = True
            lblSubtitle.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical
            lblSubtitle.Location = New System.Drawing.Point(42, 65)
            lblSubtitle.Name = "lblSubtitle"
            lblSubtitle.Size = New System.Drawing.Size(300, 17)
            lblSubtitle.TabIndex = 1
            lblSubtitle.Text = "Sign in to the Egyptian Tax Authority e-Invoicing API"
            ' 
            ' grpEnvironment
            ' 
            grpEnvironment.Controls.Add(rbPreproduction)
            grpEnvironment.Controls.Add(rbProduction)
            grpEnvironment.Location = New System.Drawing.Point(40, 110)
            grpEnvironment.Name = "grpEnvironment"
            grpEnvironment.Size = New System.Drawing.Size(400, 55)
            grpEnvironment.TabIndex = 1
            grpEnvironment.Text = "Environment"
            ' 
            ' rbPreproduction
            ' 
            rbPreproduction.EditValue = True
            rbPreproduction.Location = New System.Drawing.Point(227, 26)
            rbPreproduction.Name = "rbPreproduction"
            rbPreproduction.Properties.Caption = "Preproduction (Testing)"
            rbPreproduction.Size = New System.Drawing.Size(75, 20)
            rbPreproduction.TabIndex = 0
            ' 
            ' rbProduction
            ' 
            rbProduction.Location = New System.Drawing.Point(18, 26)
            rbProduction.Name = "rbProduction"
            rbProduction.Properties.Appearance.ForeColor = Drawing.Color.FromArgb(CByte(150), CByte(30), CByte(30))
            rbProduction.Properties.Appearance.Options.UseForeColor = True
            rbProduction.Properties.Caption = "Production (Live)"
            rbProduction.Size = New System.Drawing.Size(75, 20)
            rbProduction.TabIndex = 1
            ' 
            ' lblClientId
            ' 
            lblClientId.Appearance.Font = New System.Drawing.Font("Segoe UI", 9F)
            lblClientId.Appearance.Options.UseFont = True
            lblClientId.Location = New System.Drawing.Point(40, 180)
            lblClientId.Name = "lblClientId"
            lblClientId.Size = New System.Drawing.Size(48, 15)
            lblClientId.TabIndex = 2
            lblClientId.Text = "Client ID:"
            ' 
            ' txtClientId
            ' 
            txtClientId.Location = New System.Drawing.Point(40, 200)
            txtClientId.Name = "txtClientId"
            txtClientId.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI", 9.5F)
            txtClientId.Properties.Appearance.Options.UseFont = True
            txtClientId.Size = New System.Drawing.Size(400, 24)
            txtClientId.TabIndex = 2
            ' 
            ' lblClientSecret
            ' 
            lblClientSecret.Appearance.Font = New System.Drawing.Font("Segoe UI", 9F)
            lblClientSecret.Appearance.Options.UseFont = True
            lblClientSecret.Location = New System.Drawing.Point(40, 235)
            lblClientSecret.Name = "lblClientSecret"
            lblClientSecret.Size = New System.Drawing.Size(69, 15)
            lblClientSecret.TabIndex = 3
            lblClientSecret.Text = "Client Secret:"
            ' 
            ' txtClientSecret
            ' 
            txtClientSecret.Location = New System.Drawing.Point(40, 255)
            txtClientSecret.Name = "txtClientSecret"
            txtClientSecret.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI", 9.5F)
            txtClientSecret.Properties.Appearance.Options.UseFont = True
            txtClientSecret.Properties.PasswordChar = "*"c
            txtClientSecret.Size = New System.Drawing.Size(400, 24)
            txtClientSecret.TabIndex = 3
            ' 
            ' lblTaxpayerRIN
            ' 
            lblTaxpayerRIN.Appearance.Font = New System.Drawing.Font("Segoe UI", 9F)
            lblTaxpayerRIN.Appearance.Options.UseFont = True
            lblTaxpayerRIN.Location = New System.Drawing.Point(40, 290)
            lblTaxpayerRIN.Name = "lblTaxpayerRIN"
            lblTaxpayerRIN.Size = New System.Drawing.Size(214, 15)
            lblTaxpayerRIN.TabIndex = 4
            lblTaxpayerRIN.Text = "Taxpayer Tax Registration Number (RIN):"
            ' 
            ' txtTaxpayerRIN
            ' 
            txtTaxpayerRIN.Location = New System.Drawing.Point(40, 310)
            txtTaxpayerRIN.Name = "txtTaxpayerRIN"
            txtTaxpayerRIN.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI", 9.5F)
            txtTaxpayerRIN.Properties.Appearance.Options.UseFont = True
            txtTaxpayerRIN.Size = New System.Drawing.Size(400, 24)
            txtTaxpayerRIN.TabIndex = 4
            ' 
            ' lnkSaveCredentials
            ' 
            lnkSaveCredentials.EditValue = True
            lnkSaveCredentials.Location = New System.Drawing.Point(40, 345)
            lnkSaveCredentials.Name = "lnkSaveCredentials"
            lnkSaveCredentials.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI", 8.75F)
            lnkSaveCredentials.Properties.Appearance.Options.UseFont = True
            lnkSaveCredentials.Properties.Caption = "Remember these credentials on this computer (encrypted)"
            lnkSaveCredentials.Size = New System.Drawing.Size(75, 20)
            lnkSaveCredentials.TabIndex = 5
            ' 
            ' btnLogin
            ' 
            btnLogin.Appearance.BackColor = Drawing.Color.FromArgb(CByte(20), CByte(110), CByte(70))
            btnLogin.Appearance.Font = New System.Drawing.Font("Segoe UI", 10F, Drawing.FontStyle.Bold)
            btnLogin.Appearance.ForeColor = Drawing.Color.White
            btnLogin.Appearance.Options.UseBackColor = True
            btnLogin.Appearance.Options.UseFont = True
            btnLogin.Appearance.Options.UseForeColor = True
            btnLogin.Location = New System.Drawing.Point(40, 385)
            btnLogin.Name = "btnLogin"
            btnLogin.Size = New System.Drawing.Size(190, 40)
            btnLogin.TabIndex = 6
            btnLogin.Text = "Sign In"
            ' 
            ' btnSettings
            ' 
            btnSettings.Appearance.Font = New System.Drawing.Font("Segoe UI", 9.5F)
            btnSettings.Appearance.Options.UseFont = True
            btnSettings.Location = New System.Drawing.Point(250, 385)
            btnSettings.Name = "btnSettings"
            btnSettings.Size = New System.Drawing.Size(190, 40)
            btnSettings.TabIndex = 7
            btnSettings.Text = "Advanced Settings..."
            ' 
            ' lblStatus
            ' 
            lblStatus.Appearance.Font = New System.Drawing.Font("Segoe UI", 9F)
            lblStatus.Appearance.ForeColor = Drawing.Color.DarkRed
            lblStatus.Appearance.Options.UseFont = True
            lblStatus.Appearance.Options.UseForeColor = True
            lblStatus.Location = New System.Drawing.Point(40, 440)
            lblStatus.MaximumSize = New System.Drawing.Size(400, 0)
            lblStatus.Name = "lblStatus"
            lblStatus.Size = New System.Drawing.Size(0, 15)
            lblStatus.TabIndex = 8
            ' 
            ' progressBar
            ' 
            progressBar.Location = New System.Drawing.Point(40, 470)
            progressBar.Name = "progressBar"
            progressBar.Size = New System.Drawing.Size(400, 8)
            progressBar.TabIndex = 9
            progressBar.Visible = False
            ' 
            ' LoginForm
            ' 
            AutoScaleDimensions = New System.Drawing.SizeF(96F, 96F)
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
            ClientSize = New System.Drawing.Size(480, 560)
            Controls.Add(pnlMain)
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            MaximizeBox = False
            Name = "LoginForm"
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Text = "Sphere ERP Desktop - Sign In"
            CType(pnlMain, ComponentModel.ISupportInitialize).EndInit()
            pnlMain.ResumeLayout(False)
            pnlMain.PerformLayout()
            CType(grpEnvironment, ComponentModel.ISupportInitialize).EndInit()
            grpEnvironment.ResumeLayout(False)
            CType(rbPreproduction.Properties, ComponentModel.ISupportInitialize).EndInit()
            CType(rbProduction.Properties, ComponentModel.ISupportInitialize).EndInit()
            CType(txtClientId.Properties, ComponentModel.ISupportInitialize).EndInit()
            CType(txtClientSecret.Properties, ComponentModel.ISupportInitialize).EndInit()
            CType(txtTaxpayerRIN.Properties, ComponentModel.ISupportInitialize).EndInit()
            CType(lnkSaveCredentials.Properties, ComponentModel.ISupportInitialize).EndInit()
            CType(progressBar.Properties, ComponentModel.ISupportInitialize).EndInit()
            ResumeLayout(False)
        End Sub

        Friend WithEvents pnlMain As DevExpress.XtraEditors.PanelControl
        Friend WithEvents lblTitle As DevExpress.XtraEditors.LabelControl
        Friend WithEvents lblSubtitle As DevExpress.XtraEditors.LabelControl
        Friend WithEvents grpEnvironment As DevExpress.XtraEditors.GroupControl
        Friend WithEvents rbPreproduction As DevExpress.XtraEditors.CheckEdit
        Friend WithEvents rbProduction As DevExpress.XtraEditors.CheckEdit
        Friend WithEvents lblClientId As DevExpress.XtraEditors.LabelControl
        Friend WithEvents txtClientId As DevExpress.XtraEditors.TextEdit
        Friend WithEvents lblClientSecret As DevExpress.XtraEditors.LabelControl
        Friend WithEvents txtClientSecret As DevExpress.XtraEditors.TextEdit
        Friend WithEvents lblTaxpayerRIN As DevExpress.XtraEditors.LabelControl
        Friend WithEvents txtTaxpayerRIN As DevExpress.XtraEditors.TextEdit
        Friend WithEvents lnkSaveCredentials As DevExpress.XtraEditors.CheckEdit
        Friend WithEvents btnLogin As DevExpress.XtraEditors.SimpleButton
        Friend WithEvents btnSettings As DevExpress.XtraEditors.SimpleButton
        Friend WithEvents lblStatus As DevExpress.XtraEditors.LabelControl
        Friend WithEvents progressBar As DevExpress.XtraEditors.ProgressBarControl
    End Class
End Namespace
