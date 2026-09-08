Namespace Forms
    Partial Class NotificationsForm
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
            Me.lblPageTitle = New DevExpress.XtraEditors.LabelControl()
            Me.lblPageSubtitle = New DevExpress.XtraEditors.LabelControl()
            Me.lblEventTypeFilter = New DevExpress.XtraEditors.LabelControl()
            Me.cmbEventTypeFilter = New DevExpress.XtraEditors.ComboBoxEdit()
            Me.chkUnreadOnly = New DevExpress.XtraEditors.CheckEdit()
            Me.btnRefresh = New DevExpress.XtraEditors.SimpleButton()
            Me.btnMarkAllRead = New DevExpress.XtraEditors.SimpleButton()
            Me.gridNotifications = New DevExpress.XtraGrid.GridControl()
            Me.GridViewNotifications = New DevExpress.XtraGrid.Views.Grid.GridView()
            Me.lblStatus = New DevExpress.XtraEditors.LabelControl()
            CType(Me.cmbEventTypeFilter.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.chkUnreadOnly.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.gridNotifications, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.GridViewNotifications, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'lblPageTitle
            '
            Me.lblPageTitle.AutoSize = True
            Me.lblPageTitle.Appearance.Font = New System.Drawing.Font("Segoe UI", 15.0!, System.Drawing.FontStyle.Bold)
            Me.lblPageTitle.Location = New System.Drawing.Point(20, 15)
            Me.lblPageTitle.Text = "Notifications"
            '
            'lblPageSubtitle
            '
            Me.lblPageSubtitle.AutoSize = True
            Me.lblPageSubtitle.Appearance.ForeColor = System.Drawing.Color.Gray
            Me.lblPageSubtitle.Location = New System.Drawing.Point(22, 48)
            Me.lblPageSubtitle.Text = "Validation, issuance, rejection, and cancellation events for your submitted documents."
            '
            'lblEventTypeFilter
            '
            Me.lblEventTypeFilter.AutoSize = True
            Me.lblEventTypeFilter.Location = New System.Drawing.Point(20, 85)
            Me.lblEventTypeFilter.Text = "Event Type:"
            '
            'cmbEventTypeFilter
            '
            Me.cmbEventTypeFilter.Location = New System.Drawing.Point(20, 104)
            Me.cmbEventTypeFilter.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
            Me.cmbEventTypeFilter.Size = New System.Drawing.Size(180, 22)
            '
            'chkUnreadOnly
            '
            Me.chkUnreadOnly.AutoSize = True
            Me.chkUnreadOnly.Location = New System.Drawing.Point(220, 107)
            Me.chkUnreadOnly.Text = "Unread only"
            '
            'btnRefresh
            '
            Me.btnRefresh.Location = New System.Drawing.Point(360, 103)
            Me.btnRefresh.Size = New System.Drawing.Size(100, 28)
            Me.btnRefresh.Text = "Refresh"
            '
            'btnMarkAllRead
            '
            Me.btnMarkAllRead.Location = New System.Drawing.Point(880, 103)
            Me.btnMarkAllRead.Size = New System.Drawing.Size(140, 28)
            Me.btnMarkAllRead.Text = "Mark All Read"
            Me.btnMarkAllRead.Anchor = CType(System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right, System.Windows.Forms.AnchorStyles)
            '
            'gridNotifications
            '
            Me.gridNotifications.Location = New System.Drawing.Point(20, 145)
            Me.gridNotifications.MainView = Me.GridViewNotifications
            Me.gridNotifications.Size = New System.Drawing.Size(1000, 400)
            Me.gridNotifications.TabIndex = 0
            Me.gridNotifications.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridViewNotifications})
            Me.gridNotifications.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            '
            'GridViewNotifications
            '
            Me.GridViewNotifications.GridControl = Me.gridNotifications
            Me.GridViewNotifications.Name = "GridViewNotifications"
            Me.GridViewNotifications.OptionsBehavior.Editable = False
            Me.GridViewNotifications.OptionsView.ShowGroupPanel = False
            '
            'lblStatus
            '
            Me.lblStatus.AutoSize = False
            Me.lblStatus.Location = New System.Drawing.Point(20, 555)
            Me.lblStatus.Size = New System.Drawing.Size(1000, 30)
            Me.lblStatus.Appearance.ForeColor = System.Drawing.Color.DimGray
            Me.lblStatus.Anchor = CType((((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right)), System.Windows.Forms.AnchorStyles)
            '
            'NotificationsForm
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
            Me.BackColor = System.Drawing.Color.FromArgb(245, 246, 248)
            Me.ClientSize = New System.Drawing.Size(1090, 600)
            Me.Controls.Add(Me.lblPageTitle)
            Me.Controls.Add(Me.lblPageSubtitle)
            Me.Controls.Add(Me.lblEventTypeFilter)
            Me.Controls.Add(Me.cmbEventTypeFilter)
            Me.Controls.Add(Me.chkUnreadOnly)
            Me.Controls.Add(Me.btnRefresh)
            Me.Controls.Add(Me.btnMarkAllRead)
            Me.Controls.Add(Me.gridNotifications)
            Me.Controls.Add(Me.lblStatus)
            Me.Text = "Notifications"
            CType(Me.cmbEventTypeFilter.Properties, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.chkUnreadOnly.Properties, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.gridNotifications, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.GridViewNotifications, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)
            Me.PerformLayout()
        End Sub

        Friend WithEvents lblPageTitle As DevExpress.XtraEditors.LabelControl
        Friend WithEvents lblPageSubtitle As DevExpress.XtraEditors.LabelControl
        Friend WithEvents lblEventTypeFilter As DevExpress.XtraEditors.LabelControl
        Friend WithEvents cmbEventTypeFilter As DevExpress.XtraEditors.ComboBoxEdit
        Friend WithEvents chkUnreadOnly As DevExpress.XtraEditors.CheckEdit
        Friend WithEvents btnRefresh As DevExpress.XtraEditors.SimpleButton
        Friend WithEvents btnMarkAllRead As DevExpress.XtraEditors.SimpleButton
        Friend WithEvents gridNotifications As DevExpress.XtraGrid.GridControl
        Friend WithEvents GridViewNotifications As DevExpress.XtraGrid.Views.Grid.GridView
        Friend WithEvents lblStatus As DevExpress.XtraEditors.LabelControl
    End Class
End Namespace
