Namespace Forms
    Partial Class BatchImportForm
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
            lblPageTitle = New DevExpress.XtraEditors.LabelControl()
            lblPageSubtitle = New DevExpress.XtraEditors.LabelControl()
            btnDownloadTemplate = New DevExpress.XtraEditors.SimpleButton()
            btnChooseFile = New DevExpress.XtraEditors.SimpleButton()
            txtFilePath = New DevExpress.XtraEditors.TextEdit()
            lblPreviewHeader = New DevExpress.XtraEditors.LabelControl()
            gridPreview = New DevExpress.XtraGrid.GridControl()
            gridViewPreview = New DevExpress.XtraGrid.Views.Grid.GridView()
            lblSummary = New DevExpress.XtraEditors.LabelControl()
            btnSubmitAll = New DevExpress.XtraEditors.SimpleButton()
            progressBar = New DevExpress.XtraEditors.ProgressBarControl()
            lblStatus = New DevExpress.XtraEditors.LabelControl()
            CType(txtFilePath.Properties, ComponentModel.ISupportInitialize).BeginInit()
            CType(gridPreview, ComponentModel.ISupportInitialize).BeginInit()
            CType(gridViewPreview, ComponentModel.ISupportInitialize).BeginInit()
            CType(progressBar.Properties, ComponentModel.ISupportInitialize).BeginInit()
            SuspendLayout()
            ' 
            ' lblPageTitle
            ' 
            lblPageTitle.Appearance.Font = New System.Drawing.Font("Segoe UI", 15F, Drawing.FontStyle.Bold)
            lblPageTitle.Appearance.Options.UseFont = True
            lblPageTitle.Location = New System.Drawing.Point(20, 15)
            lblPageTitle.Name = "lblPageTitle"
            lblPageTitle.Size = New System.Drawing.Size(216, 28)
            lblPageTitle.TabIndex = 0
            lblPageTitle.Text = "Import from Excel/CSV"
            ' 
            ' lblPageSubtitle
            ' 
            lblPageSubtitle.Appearance.ForeColor = Drawing.Color.Gray
            lblPageSubtitle.Appearance.Options.UseForeColor = True
            lblPageSubtitle.Location = New System.Drawing.Point(22, 48)
            lblPageSubtitle.Name = "lblPageSubtitle"
            lblPageSubtitle.Size = New System.Drawing.Size(347, 13)
            lblPageSubtitle.TabIndex = 1
            lblPageSubtitle.Text = "Upload a spreadsheet of invoice lines, review totals, then submit in bulk."
            ' 
            ' btnDownloadTemplate
            ' 
            btnDownloadTemplate.Location = New System.Drawing.Point(20, 80)
            btnDownloadTemplate.Name = "btnDownloadTemplate"
            btnDownloadTemplate.Size = New System.Drawing.Size(170, 32)
            btnDownloadTemplate.TabIndex = 2
            btnDownloadTemplate.Text = "Download Template"
            ' 
            ' btnChooseFile
            ' 
            btnChooseFile.Location = New System.Drawing.Point(200, 80)
            btnChooseFile.Name = "btnChooseFile"
            btnChooseFile.Size = New System.Drawing.Size(140, 32)
            btnChooseFile.TabIndex = 3
            btnChooseFile.Text = "Choose File..."
            ' 
            ' txtFilePath
            ' 
            txtFilePath.Anchor = System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right
            txtFilePath.Location = New System.Drawing.Point(350, 84)
            txtFilePath.Name = "txtFilePath"
            txtFilePath.Properties.ReadOnly = True
            txtFilePath.Size = New System.Drawing.Size(650, 20)
            txtFilePath.TabIndex = 4
            ' 
            ' lblPreviewHeader
            ' 
            lblPreviewHeader.Appearance.Font = New System.Drawing.Font("Segoe UI", 11F, Drawing.FontStyle.Bold)
            lblPreviewHeader.Appearance.Options.UseFont = True
            lblPreviewHeader.Location = New System.Drawing.Point(20, 125)
            lblPreviewHeader.Name = "lblPreviewHeader"
            lblPreviewHeader.Size = New System.Drawing.Size(530, 20)
            lblPreviewHeader.TabIndex = 5
            lblPreviewHeader.Text = "Preview (one row per invoice — double-click a row to see line item breakup)"
            ' 
            ' gridPreview
            ' 
            gridPreview.Anchor = System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right
            gridPreview.Location = New System.Drawing.Point(20, 155)
            gridPreview.MainView = gridViewPreview
            gridPreview.Name = "gridPreview"
            gridPreview.Size = New System.Drawing.Size(1000, 320)
            gridPreview.TabIndex = 6
            gridPreview.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {gridViewPreview})
            ' 
            ' gridViewPreview
            ' 
            gridViewPreview.Appearance.Empty.BackColor = Drawing.Color.White
            gridViewPreview.GridControl = gridPreview
            gridViewPreview.Name = "gridViewPreview"
            gridViewPreview.OptionsBehavior.Editable = False
            gridViewPreview.OptionsView.EnableAppearanceOddRow = True
            gridViewPreview.OptionsView.ShowGroupPanel = False
            gridViewPreview.OptionsView.ShowIndicator = False
            ' 
            ' lblSummary
            ' 
            lblSummary.Anchor = System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left
            lblSummary.Appearance.Font = New System.Drawing.Font("Segoe UI", 9.5F, Drawing.FontStyle.Bold)
            lblSummary.Appearance.Options.UseFont = True
            lblSummary.Location = New System.Drawing.Point(20, 485)
            lblSummary.Name = "lblSummary"
            lblSummary.Size = New System.Drawing.Size(92, 17)
            lblSummary.TabIndex = 7
            lblSummary.Text = "No file loaded."
            ' 
            ' btnSubmitAll
            ' 
            btnSubmitAll.Anchor = System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right
            btnSubmitAll.Appearance.BackColor = Drawing.Color.FromArgb(CByte(20), CByte(110), CByte(70))
            btnSubmitAll.Appearance.Font = New System.Drawing.Font("Segoe UI", 9.5F, Drawing.FontStyle.Bold)
            btnSubmitAll.Appearance.ForeColor = Drawing.Color.White
            btnSubmitAll.Appearance.Options.UseBackColor = True
            btnSubmitAll.Appearance.Options.UseFont = True
            btnSubmitAll.Appearance.Options.UseForeColor = True
            btnSubmitAll.Enabled = False
            btnSubmitAll.Location = New System.Drawing.Point(890, 505)
            btnSubmitAll.Name = "btnSubmitAll"
            btnSubmitAll.Size = New System.Drawing.Size(130, 36)
            btnSubmitAll.TabIndex = 10
            btnSubmitAll.Text = "Submit All Valid"
            ' 
            ' progressBar
            ' 
            progressBar.Anchor = System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right
            progressBar.Location = New System.Drawing.Point(20, 548)
            progressBar.Name = "progressBar"
            progressBar.Size = New System.Drawing.Size(1000, 8)
            progressBar.TabIndex = 9
            ' 
            ' lblStatus
            ' 
            lblStatus.Anchor = System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left
            lblStatus.Appearance.ForeColor = Drawing.Color.DarkRed
            lblStatus.Appearance.Options.UseForeColor = True
            lblStatus.Location = New System.Drawing.Point(20, 505)
            lblStatus.Name = "lblStatus"
            lblStatus.Size = New System.Drawing.Size(0, 13)
            lblStatus.TabIndex = 8
            ' 
            ' BatchImportForm
            ' 
            AutoScaleDimensions = New System.Drawing.SizeF(96F, 96F)
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
            ClientSize = New System.Drawing.Size(1090, 600)
            Controls.Add(lblPageTitle)
            Controls.Add(lblPageSubtitle)
            Controls.Add(btnDownloadTemplate)
            Controls.Add(btnChooseFile)
            Controls.Add(txtFilePath)
            Controls.Add(lblPreviewHeader)
            Controls.Add(gridPreview)
            Controls.Add(lblSummary)
            Controls.Add(lblStatus)
            Controls.Add(progressBar)
            Controls.Add(btnSubmitAll)
            Name = "BatchImportForm"
            Text = "Batch Import"
            CType(txtFilePath.Properties, ComponentModel.ISupportInitialize).EndInit()
            CType(gridPreview, ComponentModel.ISupportInitialize).EndInit()
            CType(gridViewPreview, ComponentModel.ISupportInitialize).EndInit()
            CType(progressBar.Properties, ComponentModel.ISupportInitialize).EndInit()
            ResumeLayout(False)
            PerformLayout()
        End Sub

        Friend WithEvents lblPageTitle As DevExpress.XtraEditors.LabelControl
        Friend WithEvents lblPageSubtitle As DevExpress.XtraEditors.LabelControl
        Friend WithEvents btnDownloadTemplate As DevExpress.XtraEditors.SimpleButton
        Friend WithEvents btnChooseFile As DevExpress.XtraEditors.SimpleButton
        Friend WithEvents txtFilePath As DevExpress.XtraEditors.TextEdit
        Friend WithEvents lblPreviewHeader As DevExpress.XtraEditors.LabelControl
        Friend WithEvents gridPreview As DevExpress.XtraGrid.GridControl
        Friend WithEvents gridViewPreview As DevExpress.XtraGrid.Views.Grid.GridView
        Friend WithEvents lblSummary As DevExpress.XtraEditors.LabelControl
        Friend WithEvents btnSubmitAll As DevExpress.XtraEditors.SimpleButton
        Friend WithEvents progressBar As DevExpress.XtraEditors.ProgressBarControl
        Friend WithEvents lblStatus As DevExpress.XtraEditors.LabelControl
    End Class
End Namespace
