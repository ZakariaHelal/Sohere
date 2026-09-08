Namespace Forms
    Partial Class MainForm
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
            components = New ComponentModel.Container()
            pnlSidebar = New DevExpress.XtraEditors.PanelControl()
            lblAppTitle = New DevExpress.XtraEditors.LabelControl()
            lblEnvironmentBadge = New DevExpress.XtraEditors.LabelControl()
            btnNavDashboard = New DevExpress.XtraEditors.SimpleButton()
            btnNavNewDocument = New DevExpress.XtraEditors.SimpleButton()
            btnNavBatchImport = New DevExpress.XtraEditors.SimpleButton()
            btnNavSearch = New DevExpress.XtraEditors.SimpleButton()
            btnNavEgsCodes = New DevExpress.XtraEditors.SimpleButton()
            btnNavNotifications = New DevExpress.XtraEditors.SimpleButton()
            btnNavSettings = New DevExpress.XtraEditors.SimpleButton()
            btnHelp = New DevExpress.XtraEditors.SimpleButton()
            btnLogout = New DevExpress.XtraEditors.SimpleButton()
            pnlContent = New DevExpress.XtraEditors.PanelControl()
            statusStrip = New System.Windows.Forms.StatusStrip()
            lblStatusText = New System.Windows.Forms.ToolStripStatusLabel()
            lblStatusEnvironment = New System.Windows.Forms.ToolStripStatusLabel()
            notifyIcon = New System.Windows.Forms.NotifyIcon(components)
            CType(pnlSidebar, ComponentModel.ISupportInitialize).BeginInit()
            pnlSidebar.SuspendLayout()
            CType(pnlContent, ComponentModel.ISupportInitialize).BeginInit()
            statusStrip.SuspendLayout()
            SuspendLayout()
            ' 
            ' pnlSidebar
            ' 
            pnlSidebar.Appearance.BackColor = Drawing.Color.FromArgb(CByte(25), CByte(40), CByte(60))
            pnlSidebar.Appearance.Options.UseBackColor = True
            pnlSidebar.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
            pnlSidebar.Controls.Add(lblAppTitle)
            pnlSidebar.Controls.Add(lblEnvironmentBadge)
            pnlSidebar.Controls.Add(btnNavDashboard)
            pnlSidebar.Controls.Add(btnNavNewDocument)
            pnlSidebar.Controls.Add(btnNavBatchImport)
            pnlSidebar.Controls.Add(btnNavSearch)
            pnlSidebar.Controls.Add(btnNavEgsCodes)
            pnlSidebar.Controls.Add(btnNavNotifications)
            pnlSidebar.Controls.Add(btnNavSettings)
            pnlSidebar.Controls.Add(btnHelp)
            pnlSidebar.Controls.Add(btnLogout)
            pnlSidebar.Dock = System.Windows.Forms.DockStyle.Left
            pnlSidebar.Location = New System.Drawing.Point(0, 0)
            pnlSidebar.Name = "pnlSidebar"
            pnlSidebar.Size = New System.Drawing.Size(220, 578)
            pnlSidebar.TabIndex = 1
            ' 
            ' lblAppTitle
            ' 
            lblAppTitle.Appearance.Font = New System.Drawing.Font("Segoe UI", 13F, Drawing.FontStyle.Bold)
            lblAppTitle.Appearance.ForeColor = Drawing.Color.White
            lblAppTitle.Appearance.Options.UseFont = True
            lblAppTitle.Appearance.Options.UseForeColor = True
            lblAppTitle.Location = New System.Drawing.Point(15, 20)
            lblAppTitle.Name = "lblAppTitle"
            lblAppTitle.Size = New System.Drawing.Size(91, 23)
            lblAppTitle.TabIndex = 0
            lblAppTitle.Text = "Sphere ERP"
            ' 
            ' lblEnvironmentBadge
            ' 
            lblEnvironmentBadge.Appearance.Font = New System.Drawing.Font("Segoe UI", 8.5F, Drawing.FontStyle.Bold)
            lblEnvironmentBadge.Appearance.ForeColor = Drawing.Color.Orange
            lblEnvironmentBadge.Appearance.Options.UseFont = True
            lblEnvironmentBadge.Appearance.Options.UseForeColor = True
            lblEnvironmentBadge.Location = New System.Drawing.Point(15, 52)
            lblEnvironmentBadge.Name = "lblEnvironmentBadge"
            lblEnvironmentBadge.Size = New System.Drawing.Size(91, 13)
            lblEnvironmentBadge.TabIndex = 1
            lblEnvironmentBadge.Text = "PREPRODUCTION"
            ' 
            ' btnNavDashboard
            ' 
            SetupNavButton(btnNavDashboard, "Dashboard", 90)
            btnNavDashboard.TabIndex = 2
            ' 
            ' btnNavNewDocument
            ' 
            SetupNavButton(btnNavNewDocument, "New Document", 135)
            btnNavNewDocument.TabIndex = 3
            ' 
            ' btnNavBatchImport
            ' 
            SetupNavButton(btnNavBatchImport, "Batch Import", 180)
            btnNavBatchImport.TabIndex = 4
            ' 
            ' btnNavSearch
            ' 
            SetupNavButton(btnNavSearch, "Search", 225)
            btnNavSearch.TabIndex = 5
            ' 
            ' btnNavEgsCodes
            ' 
            SetupNavButton(btnNavEgsCodes, "EGS Codes", 270)
            btnNavEgsCodes.TabIndex = 6
            ' 
            ' btnNavNotifications
            ' 
            SetupNavButton(btnNavNotifications, "Notifications", 315)
            btnNavNotifications.TabIndex = 7
            ' 
            ' btnNavSettings
            ' 
            SetupNavButton(btnNavSettings, "Settings", 360)
            btnNavSettings.TabIndex = 8
            ' 
            ' btnHelp
            ' 
            SetupNavButton(btnHelp, "Help / Manual", 410)
            btnHelp.TabIndex = 9
            ' 
            ' btnLogout
            ' 
            btnLogout.Anchor = System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left
            btnLogout.Appearance.Font = New System.Drawing.Font("Segoe UI", 9.5F)
            btnLogout.Appearance.ForeColor = Drawing.Color.LightSalmon
            btnLogout.Appearance.Options.UseFont = True
            btnLogout.Appearance.Options.UseForeColor = True
            btnLogout.Appearance.Options.UseTextOptions = True
            btnLogout.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
            btnLogout.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
            btnLogout.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
            btnLogout.Location = New System.Drawing.Point(10, 998)
            btnLogout.Name = "btnLogout"
            btnLogout.Size = New System.Drawing.Size(200, 38)
            btnLogout.TabIndex = 10
            btnLogout.Text = "Sign Out"
            ' 
            ' pnlContent
            ' 
            pnlContent.Appearance.BackColor = Drawing.Color.FromArgb(CByte(245), CByte(246), CByte(248))
            pnlContent.Appearance.Options.UseBackColor = True
            pnlContent.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
            pnlContent.Dock = System.Windows.Forms.DockStyle.Fill
            pnlContent.Location = New System.Drawing.Point(220, 0)
            pnlContent.Name = "pnlContent"
            pnlContent.Padding = New System.Windows.Forms.Padding(20)
            pnlContent.Size = New System.Drawing.Size(880, 578)
            pnlContent.TabIndex = 0
            ' 
            ' statusStrip
            ' 
            statusStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {lblStatusText, lblStatusEnvironment})
            statusStrip.Location = New System.Drawing.Point(0, 578)
            statusStrip.Name = "statusStrip"
            statusStrip.Size = New System.Drawing.Size(1100, 22)
            statusStrip.TabIndex = 2
            ' 
            ' lblStatusText
            ' 
            lblStatusText.Name = "lblStatusText"
            lblStatusText.Size = New System.Drawing.Size(1085, 17)
            lblStatusText.Spring = True
            lblStatusText.Text = "Ready"
            lblStatusText.TextAlign = Drawing.ContentAlignment.MiddleLeft
            ' 
            ' lblStatusEnvironment
            ' 
            lblStatusEnvironment.Name = "lblStatusEnvironment"
            lblStatusEnvironment.Size = New System.Drawing.Size(0, 17)
            ' 
            ' MainForm
            ' 
            AutoScaleDimensions = New System.Drawing.SizeF(96F, 96F)
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
            ClientSize = New System.Drawing.Size(1100, 600)
            Controls.Add(pnlContent)
            Controls.Add(pnlSidebar)
            Controls.Add(statusStrip)
            MinimumSize = New System.Drawing.Size(1000, 600)
            Name = "MainForm"
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Text = "Sphere ERP Desktop"
            WindowState = System.Windows.Forms.FormWindowState.Maximized
            CType(pnlSidebar, ComponentModel.ISupportInitialize).EndInit()
            pnlSidebar.ResumeLayout(False)
            pnlSidebar.PerformLayout()
            CType(pnlContent, ComponentModel.ISupportInitialize).EndInit()
            statusStrip.ResumeLayout(False)
            statusStrip.PerformLayout()
            ResumeLayout(False)
            PerformLayout()
        End Sub

        Private Sub SetupNavButton(btn As DevExpress.XtraEditors.SimpleButton, text As String, top As Integer)
            btn.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
            btn.Appearance.Font = New System.Drawing.Font("Segoe UI", 9.5!)
            btn.Appearance.ForeColor = System.Drawing.Color.White
            btn.Appearance.Options.UseFont = True
            btn.Appearance.Options.UseForeColor = True
            btn.Location = New System.Drawing.Point(10, top)
            btn.Size = New System.Drawing.Size(200, 38)
            btn.Text = "   " & text
            btn.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
            btn.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        End Sub

        Friend WithEvents pnlSidebar As DevExpress.XtraEditors.PanelControl
        Friend WithEvents lblAppTitle As DevExpress.XtraEditors.LabelControl
        Friend WithEvents lblEnvironmentBadge As DevExpress.XtraEditors.LabelControl
        Friend WithEvents btnNavDashboard As DevExpress.XtraEditors.SimpleButton
        Friend WithEvents btnNavNewDocument As DevExpress.XtraEditors.SimpleButton
        Friend WithEvents btnNavBatchImport As DevExpress.XtraEditors.SimpleButton
        Friend WithEvents btnNavSearch As DevExpress.XtraEditors.SimpleButton
        Friend WithEvents btnNavEgsCodes As DevExpress.XtraEditors.SimpleButton
        Friend WithEvents btnNavNotifications As DevExpress.XtraEditors.SimpleButton
        Friend WithEvents btnNavSettings As DevExpress.XtraEditors.SimpleButton
        Friend WithEvents btnHelp As DevExpress.XtraEditors.SimpleButton
        Friend WithEvents btnLogout As DevExpress.XtraEditors.SimpleButton
        Friend WithEvents pnlContent As DevExpress.XtraEditors.PanelControl
        Friend WithEvents statusStrip As System.Windows.Forms.StatusStrip
        Friend WithEvents lblStatusText As System.Windows.Forms.ToolStripStatusLabel
        Friend WithEvents lblStatusEnvironment As System.Windows.Forms.ToolStripStatusLabel
        Friend WithEvents notifyIcon As System.Windows.Forms.NotifyIcon
    End Class
End Namespace
