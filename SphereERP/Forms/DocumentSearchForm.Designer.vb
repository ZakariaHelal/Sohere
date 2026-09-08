Namespace Forms
    Partial Class DocumentSearchForm
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
            LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
            txtInternalIdFilter = New DevExpress.XtraEditors.TextEdit()
            txtIssuerFilter = New DevExpress.XtraEditors.TextEdit()
            txtReceiverFilter = New DevExpress.XtraEditors.TextEdit()
            cmbTypeFilter = New DevExpress.XtraEditors.ComboBoxEdit()
            dtDateFrom = New DevExpress.XtraEditors.DateEdit()
            dtDateTo = New DevExpress.XtraEditors.DateEdit()
            cmbStatusFilter = New DevExpress.XtraEditors.ComboBoxEdit()
            btnDownloadPdf = New DevExpress.XtraEditors.SimpleButton()
            btnCancelDocument = New DevExpress.XtraEditors.SimpleButton()
            btnRejectDocument = New DevExpress.XtraEditors.SimpleButton()
            gridResults = New DevExpress.XtraGrid.GridControl()
            gridViewResults = New DevExpress.XtraGrid.Views.Grid.GridView()
            gridDetail = New DevExpress.XtraGrid.GridControl()
            gridViewDetail = New DevExpress.XtraGrid.Views.Grid.GridView()
            lblDetailHeader = New DevExpress.XtraEditors.LabelControl()
            btnSearch = New DevExpress.XtraEditors.SimpleButton()
            btnSyncPortal = New DevExpress.XtraEditors.SimpleButton()
            btnViewDetails = New DevExpress.XtraEditors.SimpleButton()
            cmbDirection = New DevExpress.XtraEditors.ComboBoxEdit()
            Root = New DevExpress.XtraLayout.LayoutControlGroup()
            EmptySpaceItem2 = New DevExpress.XtraLayout.EmptySpaceItem()
            LayoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem()
            LayoutControlItem5 = New DevExpress.XtraLayout.LayoutControlItem()
            EmptySpaceItem1 = New DevExpress.XtraLayout.EmptySpaceItem()
            LayoutControlItem6 = New DevExpress.XtraLayout.LayoutControlItem()
            SplitterItem1 = New DevExpress.XtraLayout.SplitterItem()
            EmptySpaceItem3 = New DevExpress.XtraLayout.EmptySpaceItem()
            LayoutControlItem9 = New DevExpress.XtraLayout.LayoutControlItem()
            LayoutControlItem10 = New DevExpress.XtraLayout.LayoutControlItem()
            LayoutControlItem12 = New DevExpress.XtraLayout.LayoutControlItem()
            LayoutControlItem13 = New DevExpress.XtraLayout.LayoutControlItem()
            LayoutControlItem15 = New DevExpress.XtraLayout.LayoutControlItem()
            LayoutControlItem16 = New DevExpress.XtraLayout.LayoutControlItem()
            EmptySpaceItem5 = New DevExpress.XtraLayout.EmptySpaceItem()
            EmptySpaceItem6 = New DevExpress.XtraLayout.EmptySpaceItem()
            LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
            LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
            LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
            LayoutControlItem8 = New DevExpress.XtraLayout.LayoutControlItem()
            EmptySpaceItem4 = New DevExpress.XtraLayout.EmptySpaceItem()
            LayoutControlItem7 = New DevExpress.XtraLayout.LayoutControlItem()
            EmptySpaceItem7 = New DevExpress.XtraLayout.EmptySpaceItem()
            EmptySpaceItem8 = New DevExpress.XtraLayout.EmptySpaceItem()
            LayoutControlItem18 = New DevExpress.XtraLayout.LayoutControlItem()
            LayoutControlItem11 = New DevExpress.XtraLayout.LayoutControlItem()
            LayoutControlItem14 = New DevExpress.XtraLayout.LayoutControlItem()
            LayoutControlItem17 = New DevExpress.XtraLayout.LayoutControlItem()
            lblStatus = New DevExpress.XtraEditors.LabelControl()
            progressSync = New System.Windows.Forms.ProgressBar()
            CType(LayoutControl1, ComponentModel.ISupportInitialize).BeginInit()
            LayoutControl1.SuspendLayout()
            CType(txtInternalIdFilter.Properties, ComponentModel.ISupportInitialize).BeginInit()
            CType(txtIssuerFilter.Properties, ComponentModel.ISupportInitialize).BeginInit()
            CType(txtReceiverFilter.Properties, ComponentModel.ISupportInitialize).BeginInit()
            CType(cmbTypeFilter.Properties, ComponentModel.ISupportInitialize).BeginInit()
            CType(dtDateFrom.Properties, ComponentModel.ISupportInitialize).BeginInit()
            CType(dtDateFrom.Properties.CalendarTimeProperties, ComponentModel.ISupportInitialize).BeginInit()
            CType(dtDateTo.Properties, ComponentModel.ISupportInitialize).BeginInit()
            CType(dtDateTo.Properties.CalendarTimeProperties, ComponentModel.ISupportInitialize).BeginInit()
            CType(cmbStatusFilter.Properties, ComponentModel.ISupportInitialize).BeginInit()
            CType(gridResults, ComponentModel.ISupportInitialize).BeginInit()
            CType(gridViewResults, ComponentModel.ISupportInitialize).BeginInit()
            CType(gridDetail, ComponentModel.ISupportInitialize).BeginInit()
            CType(gridViewDetail, ComponentModel.ISupportInitialize).BeginInit()
            CType(cmbDirection.Properties, ComponentModel.ISupportInitialize).BeginInit()
            CType(Root, ComponentModel.ISupportInitialize).BeginInit()
            CType(EmptySpaceItem2, ComponentModel.ISupportInitialize).BeginInit()
            CType(LayoutControlItem4, ComponentModel.ISupportInitialize).BeginInit()
            CType(LayoutControlItem5, ComponentModel.ISupportInitialize).BeginInit()
            CType(EmptySpaceItem1, ComponentModel.ISupportInitialize).BeginInit()
            CType(LayoutControlItem6, ComponentModel.ISupportInitialize).BeginInit()
            CType(SplitterItem1, ComponentModel.ISupportInitialize).BeginInit()
            CType(EmptySpaceItem3, ComponentModel.ISupportInitialize).BeginInit()
            CType(LayoutControlItem9, ComponentModel.ISupportInitialize).BeginInit()
            CType(LayoutControlItem10, ComponentModel.ISupportInitialize).BeginInit()
            CType(LayoutControlItem12, ComponentModel.ISupportInitialize).BeginInit()
            CType(LayoutControlItem13, ComponentModel.ISupportInitialize).BeginInit()
            CType(LayoutControlItem15, ComponentModel.ISupportInitialize).BeginInit()
            CType(LayoutControlItem16, ComponentModel.ISupportInitialize).BeginInit()
            CType(EmptySpaceItem5, ComponentModel.ISupportInitialize).BeginInit()
            CType(EmptySpaceItem6, ComponentModel.ISupportInitialize).BeginInit()
            CType(LayoutControlItem1, ComponentModel.ISupportInitialize).BeginInit()
            CType(LayoutControlItem2, ComponentModel.ISupportInitialize).BeginInit()
            CType(LayoutControlItem3, ComponentModel.ISupportInitialize).BeginInit()
            CType(LayoutControlItem8, ComponentModel.ISupportInitialize).BeginInit()
            CType(EmptySpaceItem4, ComponentModel.ISupportInitialize).BeginInit()
            CType(LayoutControlItem7, ComponentModel.ISupportInitialize).BeginInit()
            CType(EmptySpaceItem7, ComponentModel.ISupportInitialize).BeginInit()
            CType(EmptySpaceItem8, ComponentModel.ISupportInitialize).BeginInit()
            CType(LayoutControlItem18, ComponentModel.ISupportInitialize).BeginInit()
            CType(LayoutControlItem11, ComponentModel.ISupportInitialize).BeginInit()
            CType(LayoutControlItem14, ComponentModel.ISupportInitialize).BeginInit()
            CType(LayoutControlItem17, ComponentModel.ISupportInitialize).BeginInit()
            SuspendLayout()
            ' 
            ' lblPageTitle
            ' 
            lblPageTitle.Appearance.Font = New System.Drawing.Font("Segoe UI", 15F, Drawing.FontStyle.Bold)
            lblPageTitle.Appearance.Options.UseFont = True
            lblPageTitle.Location = New System.Drawing.Point(12, 12)
            lblPageTitle.Name = "lblPageTitle"
            lblPageTitle.Size = New System.Drawing.Size(176, 28)
            lblPageTitle.StyleController = LayoutControl1
            lblPageTitle.TabIndex = 1
            lblPageTitle.Text = "Search Documents"
            ' 
            ' LayoutControl1
            ' 
            LayoutControl1.Controls.Add(lblPageTitle)
            LayoutControl1.Controls.Add(txtInternalIdFilter)
            LayoutControl1.Controls.Add(txtIssuerFilter)
            LayoutControl1.Controls.Add(txtReceiverFilter)
            LayoutControl1.Controls.Add(cmbTypeFilter)
            LayoutControl1.Controls.Add(dtDateFrom)
            LayoutControl1.Controls.Add(dtDateTo)
            LayoutControl1.Controls.Add(cmbStatusFilter)
            LayoutControl1.Controls.Add(btnDownloadPdf)
            LayoutControl1.Controls.Add(btnCancelDocument)
            LayoutControl1.Controls.Add(btnRejectDocument)
            LayoutControl1.Controls.Add(gridResults)
            LayoutControl1.Controls.Add(gridDetail)
            LayoutControl1.Controls.Add(lblDetailHeader)
            LayoutControl1.Controls.Add(btnSearch)
            LayoutControl1.Controls.Add(btnSyncPortal)
            LayoutControl1.Controls.Add(btnViewDetails)
            LayoutControl1.Controls.Add(cmbDirection)
            LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
            LayoutControl1.Location = New System.Drawing.Point(0, 0)
            LayoutControl1.Name = "LayoutControl1"
            LayoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(1093, 319, 650, 400)
            LayoutControl1.Root = Root
            LayoutControl1.Size = New System.Drawing.Size(1202, 652)
            LayoutControl1.TabIndex = 25
            LayoutControl1.Text = "LayoutControl1"
            ' 
            ' txtInternalIdFilter
            ' 
            txtInternalIdFilter.Location = New System.Drawing.Point(96, 44)
            txtInternalIdFilter.Name = "txtInternalIdFilter"
            txtInternalIdFilter.Size = New System.Drawing.Size(146, 20)
            txtInternalIdFilter.StyleController = LayoutControl1
            txtInternalIdFilter.TabIndex = 0
            ' 
            ' txtIssuerFilter
            ' 
            txtIssuerFilter.Location = New System.Drawing.Point(330, 68)
            txtIssuerFilter.Name = "txtIssuerFilter"
            txtIssuerFilter.Size = New System.Drawing.Size(244, 20)
            txtIssuerFilter.StyleController = LayoutControl1
            txtIssuerFilter.TabIndex = 16
            ' 
            ' txtReceiverFilter
            ' 
            txtReceiverFilter.Location = New System.Drawing.Point(330, 44)
            txtReceiverFilter.Name = "txtReceiverFilter"
            txtReceiverFilter.Size = New System.Drawing.Size(244, 20)
            txtReceiverFilter.StyleController = LayoutControl1
            txtReceiverFilter.TabIndex = 2
            ' 
            ' cmbTypeFilter
            ' 
            cmbTypeFilter.Location = New System.Drawing.Point(662, 68)
            cmbTypeFilter.Name = "cmbTypeFilter"
            cmbTypeFilter.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
            cmbTypeFilter.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
            cmbTypeFilter.Size = New System.Drawing.Size(144, 20)
            cmbTypeFilter.StyleController = LayoutControl1
            cmbTypeFilter.TabIndex = 7
            ' 
            ' dtDateFrom
            ' 
            dtDateFrom.EditValue = New Date(2026, 6, 20, 0, 0, 0, 0)
            dtDateFrom.Location = New System.Drawing.Point(894, 44)
            dtDateFrom.Name = "dtDateFrom"
            dtDateFrom.Properties.Appearance.Options.UseTextOptions = True
            dtDateFrom.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            dtDateFrom.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
            dtDateFrom.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
            dtDateFrom.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
            dtDateFrom.Properties.Mask.EditMask = "dd/MM/yyyy"
            dtDateFrom.Properties.Mask.UseMaskAsDisplayFormat = True
            dtDateFrom.Size = New System.Drawing.Size(121, 20)
            dtDateFrom.StyleController = LayoutControl1
            dtDateFrom.TabIndex = 4
            ' 
            ' dtDateTo
            ' 
            dtDateTo.EditValue = New Date(2026, 6, 20, 0, 0, 0, 0)
            dtDateTo.Location = New System.Drawing.Point(894, 68)
            dtDateTo.Name = "dtDateTo"
            dtDateTo.Properties.Appearance.Options.UseTextOptions = True
            dtDateTo.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            dtDateTo.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
            dtDateTo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
            dtDateTo.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
            dtDateTo.Properties.Mask.EditMask = "dd/MM/yyyy"
            dtDateTo.Properties.Mask.UseMaskAsDisplayFormat = True
            dtDateTo.Size = New System.Drawing.Size(121, 20)
            dtDateTo.StyleController = LayoutControl1
            dtDateTo.TabIndex = 8
            ' 
            ' cmbStatusFilter
            ' 
            cmbStatusFilter.Location = New System.Drawing.Point(662, 44)
            cmbStatusFilter.Name = "cmbStatusFilter"
            cmbStatusFilter.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
            cmbStatusFilter.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
            cmbStatusFilter.Size = New System.Drawing.Size(144, 20)
            cmbStatusFilter.StyleController = LayoutControl1
            cmbStatusFilter.TabIndex = 3
            ' 
            ' btnDownloadPdf
            ' 
            btnDownloadPdf.Anchor = System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left
            btnDownloadPdf.Location = New System.Drawing.Point(12, 575)
            btnDownloadPdf.Name = "btnDownloadPdf"
            btnDownloadPdf.Size = New System.Drawing.Size(176, 65)
            btnDownloadPdf.StyleController = LayoutControl1
            btnDownloadPdf.TabIndex = 11
            btnDownloadPdf.Text = "Download PDF"
            ' 
            ' btnCancelDocument
            ' 
            btnCancelDocument.Anchor = System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left
            btnCancelDocument.Location = New System.Drawing.Point(445, 575)
            btnCancelDocument.Name = "btnCancelDocument"
            btnCancelDocument.Size = New System.Drawing.Size(154, 65)
            btnCancelDocument.StyleController = LayoutControl1
            btnCancelDocument.TabIndex = 12
            btnCancelDocument.Text = "Cancel Document"
            ' 
            ' btnRejectDocument
            ' 
            btnRejectDocument.Anchor = System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left
            btnRejectDocument.Location = New System.Drawing.Point(636, 575)
            btnRejectDocument.Name = "btnRejectDocument"
            btnRejectDocument.Size = New System.Drawing.Size(151, 65)
            btnRejectDocument.StyleController = LayoutControl1
            btnRejectDocument.TabIndex = 13
            btnRejectDocument.Text = "Reject Document"
            ' 
            ' gridResults
            ' 
            gridResults.Anchor = System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right
            gridResults.Location = New System.Drawing.Point(12, 92)
            gridResults.MainView = gridViewResults
            gridResults.Name = "gridResults"
            gridResults.Size = New System.Drawing.Size(1178, 219)
            gridResults.TabIndex = 9
            gridResults.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {gridViewResults})
            ' 
            ' gridViewResults
            ' 
            gridViewResults.GridControl = gridResults
            gridViewResults.Name = "gridViewResults"
            gridViewResults.OptionsBehavior.Editable = False
            gridViewResults.OptionsView.EnableAppearanceOddRow = True
            gridViewResults.OptionsView.ShowGroupPanel = False
            ' 
            ' gridDetail
            ' 
            gridDetail.Anchor = System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right
            gridDetail.Location = New System.Drawing.Point(12, 349)
            gridDetail.MainView = gridViewDetail
            gridDetail.Name = "gridDetail"
            gridDetail.Size = New System.Drawing.Size(1178, 222)
            gridDetail.TabIndex = 10
            gridDetail.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {gridViewDetail})
            ' 
            ' gridViewDetail
            ' 
            gridViewDetail.GridControl = gridDetail
            gridViewDetail.Name = "gridViewDetail"
            gridViewDetail.OptionsBehavior.Editable = False
            gridViewDetail.OptionsSelection.MultiSelect = True
            gridViewDetail.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect
            gridViewDetail.OptionsView.EnableAppearanceOddRow = True
            gridViewDetail.OptionsView.ShowGroupPanel = False
            ' 
            ' lblDetailHeader
            ' 
            lblDetailHeader.Appearance.Font = New System.Drawing.Font("Segoe UI", 11F, Drawing.FontStyle.Bold)
            lblDetailHeader.Appearance.Options.UseFont = True
            lblDetailHeader.Location = New System.Drawing.Point(12, 325)
            lblDetailHeader.Name = "lblDetailHeader"
            lblDetailHeader.Size = New System.Drawing.Size(73, 20)
            lblDetailHeader.StyleController = LayoutControl1
            lblDetailHeader.TabIndex = 1
            lblDetailHeader.Text = "Line Items"
            ' 
            ' btnSearch
            ' 
            btnSearch.Location = New System.Drawing.Point(1062, 44)
            btnSearch.Name = "btnSearch"
            btnSearch.Size = New System.Drawing.Size(102, 44)
            btnSearch.StyleController = LayoutControl1
            btnSearch.TabIndex = 5
            btnSearch.Text = "Search"
            ' 
            ' btnSyncPortal
            ' 
            btnSyncPortal.Anchor = System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left
            btnSyncPortal.Appearance.BackColor = Drawing.Color.FromArgb(CByte(20), CByte(110), CByte(70))
            btnSyncPortal.Appearance.Font = New System.Drawing.Font("Segoe UI", 9.5F, Drawing.FontStyle.Bold)
            btnSyncPortal.Appearance.ForeColor = Drawing.Color.White
            btnSyncPortal.Appearance.Options.UseBackColor = True
            btnSyncPortal.Appearance.Options.UseFont = True
            btnSyncPortal.Appearance.Options.UseForeColor = True
            btnSyncPortal.Location = New System.Drawing.Point(1024, 575)
            btnSyncPortal.Name = "btnSyncPortal"
            btnSyncPortal.Size = New System.Drawing.Size(166, 65)
            btnSyncPortal.StyleController = LayoutControl1
            btnSyncPortal.TabIndex = 14
            btnSyncPortal.Text = "Sync from Portal"
            ' 
            ' btnViewDetails
            ' 
            btnViewDetails.Anchor = System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left
            btnViewDetails.Location = New System.Drawing.Point(791, 575)
            btnViewDetails.Name = "btnViewDetails"
            btnViewDetails.Size = New System.Drawing.Size(109, 22)
            btnViewDetails.StyleController = LayoutControl1
            btnViewDetails.TabIndex = 15
            btnViewDetails.Text = "View Details"
            ' 
            ' cmbDirection
            ' 
            cmbDirection.Location = New System.Drawing.Point(96, 68)
            cmbDirection.Name = "cmbDirection"
            cmbDirection.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
            cmbDirection.Properties.Items.AddRange(New Object() {"(All)", "Sent (Issued by me)", "Received (Issued to me)"})
            cmbDirection.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
            cmbDirection.Size = New System.Drawing.Size(146, 20)
            cmbDirection.StyleController = LayoutControl1
            cmbDirection.TabIndex = 6
            ' 
            ' Root
            ' 
            Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True
            Root.GroupBordersVisible = False
            Root.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {EmptySpaceItem2, LayoutControlItem4, LayoutControlItem5, EmptySpaceItem1, LayoutControlItem6, SplitterItem1, EmptySpaceItem3, LayoutControlItem9, LayoutControlItem10, LayoutControlItem12, LayoutControlItem13, LayoutControlItem15, LayoutControlItem16, EmptySpaceItem5, EmptySpaceItem6, LayoutControlItem1, LayoutControlItem2, LayoutControlItem3, LayoutControlItem8, EmptySpaceItem4, LayoutControlItem7, EmptySpaceItem7, EmptySpaceItem8, LayoutControlItem18, LayoutControlItem11, LayoutControlItem14, LayoutControlItem17})
            Root.Name = "Root"
            Root.Size = New System.Drawing.Size(1202, 652)
            Root.TextVisible = False
            ' 
            ' EmptySpaceItem2
            ' 
            EmptySpaceItem2.Location = New System.Drawing.Point(1007, 32)
            EmptySpaceItem2.MaxSize = New System.Drawing.Size(43, 0)
            EmptySpaceItem2.MinSize = New System.Drawing.Size(43, 10)
            EmptySpaceItem2.Name = "EmptySpaceItem2"
            EmptySpaceItem2.Size = New System.Drawing.Size(43, 48)
            EmptySpaceItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
            ' 
            ' LayoutControlItem4
            ' 
            LayoutControlItem4.Control = gridResults
            LayoutControlItem4.Location = New System.Drawing.Point(0, 80)
            LayoutControlItem4.Name = "LayoutControlItem4"
            LayoutControlItem4.Size = New System.Drawing.Size(1182, 223)
            LayoutControlItem4.TextVisible = False
            ' 
            ' LayoutControlItem5
            ' 
            LayoutControlItem5.Control = gridDetail
            LayoutControlItem5.Location = New System.Drawing.Point(0, 337)
            LayoutControlItem5.Name = "LayoutControlItem5"
            LayoutControlItem5.Size = New System.Drawing.Size(1182, 226)
            LayoutControlItem5.TextVisible = False
            ' 
            ' EmptySpaceItem1
            ' 
            EmptySpaceItem1.Location = New System.Drawing.Point(77, 313)
            EmptySpaceItem1.Name = "EmptySpaceItem1"
            EmptySpaceItem1.Size = New System.Drawing.Size(1105, 24)
            ' 
            ' LayoutControlItem6
            ' 
            LayoutControlItem6.Control = lblDetailHeader
            LayoutControlItem6.Location = New System.Drawing.Point(0, 313)
            LayoutControlItem6.Name = "LayoutControlItem6"
            LayoutControlItem6.Size = New System.Drawing.Size(77, 24)
            LayoutControlItem6.TextVisible = False
            ' 
            ' SplitterItem1
            ' 
            SplitterItem1.Location = New System.Drawing.Point(0, 303)
            SplitterItem1.Name = "SplitterItem1"
            SplitterItem1.Size = New System.Drawing.Size(1182, 10)
            ' 
            ' EmptySpaceItem3
            ' 
            EmptySpaceItem3.Location = New System.Drawing.Point(180, 0)
            EmptySpaceItem3.Name = "EmptySpaceItem3"
            EmptySpaceItem3.Size = New System.Drawing.Size(1002, 32)
            ' 
            ' LayoutControlItem9
            ' 
            LayoutControlItem9.Control = lblPageTitle
            LayoutControlItem9.Location = New System.Drawing.Point(0, 0)
            LayoutControlItem9.Name = "LayoutControlItem9"
            LayoutControlItem9.Size = New System.Drawing.Size(180, 32)
            LayoutControlItem9.TextVisible = False
            ' 
            ' LayoutControlItem10
            ' 
            LayoutControlItem10.Control = txtInternalIdFilter
            LayoutControlItem10.Location = New System.Drawing.Point(0, 32)
            LayoutControlItem10.MaxSize = New System.Drawing.Size(234, 24)
            LayoutControlItem10.MinSize = New System.Drawing.Size(234, 24)
            LayoutControlItem10.Name = "LayoutControlItem10"
            LayoutControlItem10.Size = New System.Drawing.Size(234, 24)
            LayoutControlItem10.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
            LayoutControlItem10.Text = "Internal ID"
            LayoutControlItem10.TextSize = New System.Drawing.Size(72, 13)
            ' 
            ' LayoutControlItem12
            ' 
            LayoutControlItem12.Control = btnSearch
            LayoutControlItem12.Location = New System.Drawing.Point(1050, 32)
            LayoutControlItem12.MaxSize = New System.Drawing.Size(106, 48)
            LayoutControlItem12.MinSize = New System.Drawing.Size(106, 48)
            LayoutControlItem12.Name = "LayoutControlItem12"
            LayoutControlItem12.Size = New System.Drawing.Size(106, 48)
            LayoutControlItem12.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
            LayoutControlItem12.TextVisible = False
            ' 
            ' LayoutControlItem13
            ' 
            LayoutControlItem13.Control = txtReceiverFilter
            LayoutControlItem13.Location = New System.Drawing.Point(234, 32)
            LayoutControlItem13.MaxSize = New System.Drawing.Size(332, 24)
            LayoutControlItem13.MinSize = New System.Drawing.Size(332, 24)
            LayoutControlItem13.Name = "LayoutControlItem13"
            LayoutControlItem13.Size = New System.Drawing.Size(332, 24)
            LayoutControlItem13.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
            LayoutControlItem13.Text = "Reciever Name"
            LayoutControlItem13.TextSize = New System.Drawing.Size(72, 13)
            ' 
            ' LayoutControlItem15
            ' 
            LayoutControlItem15.Control = cmbStatusFilter
            LayoutControlItem15.Location = New System.Drawing.Point(566, 32)
            LayoutControlItem15.MaxSize = New System.Drawing.Size(232, 24)
            LayoutControlItem15.MinSize = New System.Drawing.Size(232, 24)
            LayoutControlItem15.Name = "LayoutControlItem15"
            LayoutControlItem15.Size = New System.Drawing.Size(232, 24)
            LayoutControlItem15.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
            LayoutControlItem15.Text = "Status"
            LayoutControlItem15.TextSize = New System.Drawing.Size(72, 13)
            ' 
            ' LayoutControlItem16
            ' 
            LayoutControlItem16.Control = dtDateFrom
            LayoutControlItem16.Location = New System.Drawing.Point(798, 32)
            LayoutControlItem16.MaxSize = New System.Drawing.Size(209, 24)
            LayoutControlItem16.MinSize = New System.Drawing.Size(209, 24)
            LayoutControlItem16.Name = "LayoutControlItem16"
            LayoutControlItem16.Size = New System.Drawing.Size(209, 24)
            LayoutControlItem16.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
            LayoutControlItem16.Text = "From"
            LayoutControlItem16.TextSize = New System.Drawing.Size(72, 13)
            ' 
            ' EmptySpaceItem5
            ' 
            EmptySpaceItem5.Location = New System.Drawing.Point(892, 563)
            EmptySpaceItem5.Name = "EmptySpaceItem5"
            EmptySpaceItem5.Size = New System.Drawing.Size(110, 69)
            ' 
            ' EmptySpaceItem6
            ' 
            EmptySpaceItem6.Location = New System.Drawing.Point(1002, 563)
            EmptySpaceItem6.MaxSize = New System.Drawing.Size(0, 69)
            EmptySpaceItem6.MinSize = New System.Drawing.Size(10, 69)
            EmptySpaceItem6.Name = "EmptySpaceItem6"
            EmptySpaceItem6.Size = New System.Drawing.Size(10, 69)
            EmptySpaceItem6.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
            ' 
            ' LayoutControlItem1
            ' 
            LayoutControlItem1.Control = btnDownloadPdf
            LayoutControlItem1.Location = New System.Drawing.Point(0, 563)
            LayoutControlItem1.MaxSize = New System.Drawing.Size(180, 69)
            LayoutControlItem1.MinSize = New System.Drawing.Size(180, 69)
            LayoutControlItem1.Name = "LayoutControlItem1"
            LayoutControlItem1.Size = New System.Drawing.Size(180, 69)
            LayoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
            LayoutControlItem1.TextVisible = False
            ' 
            ' LayoutControlItem2
            ' 
            LayoutControlItem2.Control = btnCancelDocument
            LayoutControlItem2.Location = New System.Drawing.Point(433, 563)
            LayoutControlItem2.MaxSize = New System.Drawing.Size(158, 69)
            LayoutControlItem2.MinSize = New System.Drawing.Size(158, 69)
            LayoutControlItem2.Name = "LayoutControlItem2"
            LayoutControlItem2.Size = New System.Drawing.Size(158, 69)
            LayoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
            LayoutControlItem2.TextVisible = False
            ' 
            ' LayoutControlItem3
            ' 
            LayoutControlItem3.Control = btnRejectDocument
            LayoutControlItem3.Location = New System.Drawing.Point(624, 563)
            LayoutControlItem3.MaxSize = New System.Drawing.Size(155, 69)
            LayoutControlItem3.MinSize = New System.Drawing.Size(155, 69)
            LayoutControlItem3.Name = "LayoutControlItem3"
            LayoutControlItem3.Size = New System.Drawing.Size(155, 69)
            LayoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
            LayoutControlItem3.TextVisible = False
            ' 
            ' LayoutControlItem8
            ' 
            LayoutControlItem8.Control = btnViewDetails
            LayoutControlItem8.Location = New System.Drawing.Point(779, 563)
            LayoutControlItem8.Name = "LayoutControlItem8"
            LayoutControlItem8.Size = New System.Drawing.Size(113, 69)
            LayoutControlItem8.TextVisible = False
            LayoutControlItem8.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            ' 
            ' EmptySpaceItem4
            ' 
            EmptySpaceItem4.Location = New System.Drawing.Point(1156, 32)
            EmptySpaceItem4.Name = "EmptySpaceItem4"
            EmptySpaceItem4.Size = New System.Drawing.Size(26, 48)
            ' 
            ' LayoutControlItem7
            ' 
            LayoutControlItem7.Control = btnSyncPortal
            LayoutControlItem7.Location = New System.Drawing.Point(1012, 563)
            LayoutControlItem7.MaxSize = New System.Drawing.Size(170, 69)
            LayoutControlItem7.MinSize = New System.Drawing.Size(170, 69)
            LayoutControlItem7.Name = "LayoutControlItem7"
            LayoutControlItem7.Size = New System.Drawing.Size(170, 69)
            LayoutControlItem7.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
            LayoutControlItem7.TextVisible = False
            ' 
            ' EmptySpaceItem7
            ' 
            EmptySpaceItem7.Location = New System.Drawing.Point(180, 563)
            EmptySpaceItem7.MaxSize = New System.Drawing.Size(253, 0)
            EmptySpaceItem7.MinSize = New System.Drawing.Size(253, 10)
            EmptySpaceItem7.Name = "EmptySpaceItem7"
            EmptySpaceItem7.Size = New System.Drawing.Size(253, 69)
            EmptySpaceItem7.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
            ' 
            ' EmptySpaceItem8
            ' 
            EmptySpaceItem8.Location = New System.Drawing.Point(591, 563)
            EmptySpaceItem8.MaxSize = New System.Drawing.Size(33, 0)
            EmptySpaceItem8.MinSize = New System.Drawing.Size(33, 10)
            EmptySpaceItem8.Name = "EmptySpaceItem8"
            EmptySpaceItem8.Size = New System.Drawing.Size(33, 69)
            EmptySpaceItem8.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
            ' 
            ' LayoutControlItem18
            ' 
            LayoutControlItem18.Control = txtIssuerFilter
            LayoutControlItem18.Location = New System.Drawing.Point(234, 56)
            LayoutControlItem18.Name = "LayoutControlItem18"
            LayoutControlItem18.Size = New System.Drawing.Size(332, 24)
            LayoutControlItem18.Text = "Issuer Name"
            LayoutControlItem18.TextSize = New System.Drawing.Size(72, 13)
            ' 
            ' LayoutControlItem11
            ' 
            LayoutControlItem11.Control = cmbDirection
            LayoutControlItem11.Location = New System.Drawing.Point(0, 56)
            LayoutControlItem11.Name = "LayoutControlItem11"
            LayoutControlItem11.Size = New System.Drawing.Size(234, 24)
            LayoutControlItem11.Text = "Direction"
            LayoutControlItem11.TextSize = New System.Drawing.Size(72, 13)
            ' 
            ' LayoutControlItem14
            ' 
            LayoutControlItem14.Control = cmbTypeFilter
            LayoutControlItem14.Location = New System.Drawing.Point(566, 56)
            LayoutControlItem14.Name = "LayoutControlItem14"
            LayoutControlItem14.Size = New System.Drawing.Size(232, 24)
            LayoutControlItem14.Text = "Type"
            LayoutControlItem14.TextSize = New System.Drawing.Size(72, 13)
            ' 
            ' LayoutControlItem17
            ' 
            LayoutControlItem17.Control = dtDateTo
            LayoutControlItem17.Location = New System.Drawing.Point(798, 56)
            LayoutControlItem17.Name = "LayoutControlItem17"
            LayoutControlItem17.Size = New System.Drawing.Size(209, 24)
            LayoutControlItem17.Text = "To"
            LayoutControlItem17.TextSize = New System.Drawing.Size(72, 13)
            ' 
            ' lblStatus
            ' 
            lblStatus.Anchor = System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right
            lblStatus.Appearance.ForeColor = Drawing.Color.DarkRed
            lblStatus.Appearance.Options.UseForeColor = True
            lblStatus.Location = New System.Drawing.Point(20, 587)
            lblStatus.Name = "lblStatus"
            lblStatus.Size = New System.Drawing.Size(0, 13)
            lblStatus.TabIndex = 24
            ' 
            ' progressSync
            ' 
            progressSync.Anchor = System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right
            progressSync.Location = New System.Drawing.Point(12, 545)
            progressSync.Name = "progressSync"
            progressSync.Size = New System.Drawing.Size(1178, 20)
            progressSync.Style = System.Windows.Forms.ProgressBarStyle.Continuous
            progressSync.TabIndex = 25
            progressSync.Visible = False
            ' 
            ' DocumentSearchForm
            ' 
            AutoScaleDimensions = New System.Drawing.SizeF(96F, 96F)
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
            ClientSize = New System.Drawing.Size(1202, 652)
            Controls.Add(LayoutControl1)
            Controls.Add(progressSync)
            Controls.Add(lblStatus)
            Name = "DocumentSearchForm"
            Text = "Search Documents"
            CType(LayoutControl1, ComponentModel.ISupportInitialize).EndInit()
            LayoutControl1.ResumeLayout(False)
            CType(txtInternalIdFilter.Properties, ComponentModel.ISupportInitialize).EndInit()
            CType(txtIssuerFilter.Properties, ComponentModel.ISupportInitialize).EndInit()
            CType(txtReceiverFilter.Properties, ComponentModel.ISupportInitialize).EndInit()
            CType(cmbTypeFilter.Properties, ComponentModel.ISupportInitialize).EndInit()
            CType(dtDateFrom.Properties.CalendarTimeProperties, ComponentModel.ISupportInitialize).EndInit()
            CType(dtDateFrom.Properties, ComponentModel.ISupportInitialize).EndInit()
            CType(dtDateTo.Properties.CalendarTimeProperties, ComponentModel.ISupportInitialize).EndInit()
            CType(dtDateTo.Properties, ComponentModel.ISupportInitialize).EndInit()
            CType(cmbStatusFilter.Properties, ComponentModel.ISupportInitialize).EndInit()
            CType(gridResults, ComponentModel.ISupportInitialize).EndInit()
            CType(gridViewResults, ComponentModel.ISupportInitialize).EndInit()
            CType(gridDetail, ComponentModel.ISupportInitialize).EndInit()
            CType(gridViewDetail, ComponentModel.ISupportInitialize).EndInit()
            CType(cmbDirection.Properties, ComponentModel.ISupportInitialize).EndInit()
            CType(Root, ComponentModel.ISupportInitialize).EndInit()
            CType(EmptySpaceItem2, ComponentModel.ISupportInitialize).EndInit()
            CType(LayoutControlItem4, ComponentModel.ISupportInitialize).EndInit()
            CType(LayoutControlItem5, ComponentModel.ISupportInitialize).EndInit()
            CType(EmptySpaceItem1, ComponentModel.ISupportInitialize).EndInit()
            CType(LayoutControlItem6, ComponentModel.ISupportInitialize).EndInit()
            CType(SplitterItem1, ComponentModel.ISupportInitialize).EndInit()
            CType(EmptySpaceItem3, ComponentModel.ISupportInitialize).EndInit()
            CType(LayoutControlItem9, ComponentModel.ISupportInitialize).EndInit()
            CType(LayoutControlItem10, ComponentModel.ISupportInitialize).EndInit()
            CType(LayoutControlItem12, ComponentModel.ISupportInitialize).EndInit()
            CType(LayoutControlItem13, ComponentModel.ISupportInitialize).EndInit()
            CType(LayoutControlItem15, ComponentModel.ISupportInitialize).EndInit()
            CType(LayoutControlItem16, ComponentModel.ISupportInitialize).EndInit()
            CType(EmptySpaceItem5, ComponentModel.ISupportInitialize).EndInit()
            CType(EmptySpaceItem6, ComponentModel.ISupportInitialize).EndInit()
            CType(LayoutControlItem1, ComponentModel.ISupportInitialize).EndInit()
            CType(LayoutControlItem2, ComponentModel.ISupportInitialize).EndInit()
            CType(LayoutControlItem3, ComponentModel.ISupportInitialize).EndInit()
            CType(LayoutControlItem8, ComponentModel.ISupportInitialize).EndInit()
            CType(EmptySpaceItem4, ComponentModel.ISupportInitialize).EndInit()
            CType(LayoutControlItem7, ComponentModel.ISupportInitialize).EndInit()
            CType(EmptySpaceItem7, ComponentModel.ISupportInitialize).EndInit()
            CType(EmptySpaceItem8, ComponentModel.ISupportInitialize).EndInit()
            CType(LayoutControlItem18, ComponentModel.ISupportInitialize).EndInit()
            CType(LayoutControlItem11, ComponentModel.ISupportInitialize).EndInit()
            CType(LayoutControlItem14, ComponentModel.ISupportInitialize).EndInit()
            CType(LayoutControlItem17, ComponentModel.ISupportInitialize).EndInit()
            ResumeLayout(False)
            PerformLayout()
        End Sub

        Friend WithEvents lblPageTitle As DevExpress.XtraEditors.LabelControl
        Friend WithEvents txtInternalIdFilter As DevExpress.XtraEditors.TextEdit
        Friend WithEvents txtIssuerFilter As DevExpress.XtraEditors.TextEdit
        Friend WithEvents txtReceiverFilter As DevExpress.XtraEditors.TextEdit
        Friend WithEvents cmbTypeFilter As DevExpress.XtraEditors.ComboBoxEdit
        Friend WithEvents cmbStatusFilter As DevExpress.XtraEditors.ComboBoxEdit
        Friend WithEvents dtDateFrom As DevExpress.XtraEditors.DateEdit
        Friend WithEvents dtDateTo As DevExpress.XtraEditors.DateEdit
        Friend WithEvents btnSearch As DevExpress.XtraEditors.SimpleButton
        Friend WithEvents cmbDirection As DevExpress.XtraEditors.ComboBoxEdit
        Friend WithEvents gridResults As DevExpress.XtraGrid.GridControl
        Friend WithEvents gridViewResults As DevExpress.XtraGrid.Views.Grid.GridView
        Friend WithEvents lblDetailHeader As DevExpress.XtraEditors.LabelControl
        Friend WithEvents gridDetail As DevExpress.XtraGrid.GridControl
        Friend WithEvents gridViewDetail As DevExpress.XtraGrid.Views.Grid.GridView
        Friend WithEvents btnViewDetails As DevExpress.XtraEditors.SimpleButton
        Friend WithEvents btnCancelDocument As DevExpress.XtraEditors.SimpleButton
        Friend WithEvents btnRejectDocument As DevExpress.XtraEditors.SimpleButton
        Friend WithEvents btnDownloadPdf As DevExpress.XtraEditors.SimpleButton
        Friend WithEvents btnSyncPortal As DevExpress.XtraEditors.SimpleButton
        Friend WithEvents lblStatus As DevExpress.XtraEditors.LabelControl
        Friend WithEvents progressSync As System.Windows.Forms.ProgressBar
        Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
        Friend WithEvents Root As DevExpress.XtraLayout.LayoutControlGroup
        Friend WithEvents EmptySpaceItem2 As DevExpress.XtraLayout.EmptySpaceItem
        Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
        Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
        Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
        Friend WithEvents LayoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem
        Friend WithEvents LayoutControlItem5 As DevExpress.XtraLayout.LayoutControlItem
        Friend WithEvents EmptySpaceItem1 As DevExpress.XtraLayout.EmptySpaceItem
        Friend WithEvents LayoutControlItem6 As DevExpress.XtraLayout.LayoutControlItem
        Friend WithEvents SplitterItem1 As DevExpress.XtraLayout.SplitterItem
        Friend WithEvents LayoutControlItem7 As DevExpress.XtraLayout.LayoutControlItem
        Friend WithEvents LayoutControlItem8 As DevExpress.XtraLayout.LayoutControlItem
        Friend WithEvents EmptySpaceItem3 As DevExpress.XtraLayout.EmptySpaceItem
        Friend WithEvents LayoutControlItem9 As DevExpress.XtraLayout.LayoutControlItem
        Friend WithEvents LayoutControlItem10 As DevExpress.XtraLayout.LayoutControlItem
        Friend WithEvents LayoutControlItem11 As DevExpress.XtraLayout.LayoutControlItem
        Friend WithEvents LayoutControlItem12 As DevExpress.XtraLayout.LayoutControlItem
        Friend WithEvents LayoutControlItem13 As DevExpress.XtraLayout.LayoutControlItem
        Friend WithEvents LayoutControlItem14 As DevExpress.XtraLayout.LayoutControlItem
        Friend WithEvents LayoutControlItem15 As DevExpress.XtraLayout.LayoutControlItem
        Friend WithEvents LayoutControlItem16 As DevExpress.XtraLayout.LayoutControlItem
        Friend WithEvents LayoutControlItem17 As DevExpress.XtraLayout.LayoutControlItem
        Friend WithEvents EmptySpaceItem5 As DevExpress.XtraLayout.EmptySpaceItem
        Friend WithEvents EmptySpaceItem6 As DevExpress.XtraLayout.EmptySpaceItem
        Friend WithEvents EmptySpaceItem4 As DevExpress.XtraLayout.EmptySpaceItem
        Friend WithEvents EmptySpaceItem7 As DevExpress.XtraLayout.EmptySpaceItem
        Friend WithEvents LayoutControlItem18 As DevExpress.XtraLayout.LayoutControlItem
        Friend WithEvents EmptySpaceItem8 As DevExpress.XtraLayout.EmptySpaceItem
    End Class
End Namespace
