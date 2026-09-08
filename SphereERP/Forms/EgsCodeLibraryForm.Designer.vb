Namespace Forms
    Partial Class EgsCodeLibraryForm
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
            numDefaultPrice = New DevExpress.XtraEditors.SpinEdit()
            cmbDefaultTaxType = New DevExpress.XtraEditors.ComboBoxEdit()
            numDefaultTaxRate = New DevExpress.XtraEditors.SpinEdit()
            btnNew = New DevExpress.XtraEditors.SimpleButton()
            chkIsActive = New DevExpress.XtraEditors.CheckEdit()
            btnSave = New DevExpress.XtraEditors.SimpleButton()
            txtCategory = New DevExpress.XtraEditors.TextEdit()
            btnDelete = New DevExpress.XtraEditors.SimpleButton()
            txtEgsCode = New DevExpress.XtraEditors.TextEdit()
            txtDescription = New DevExpress.XtraEditors.TextEdit()
            btnImportFromPortal = New DevExpress.XtraEditors.SimpleButton()
            btnImportFromExcel = New DevExpress.XtraEditors.SimpleButton()
            btnDownloadTemplate = New DevExpress.XtraEditors.SimpleButton()
            btnRefresh = New DevExpress.XtraEditors.SimpleButton()
            txtSearch = New DevExpress.XtraEditors.TextEdit()
            lblPageSubtitle = New DevExpress.XtraEditors.LabelControl()
            btnSearch = New DevExpress.XtraEditors.SimpleButton()
            gridCodes = New DevExpress.XtraGrid.GridControl()
            gridViewCodes = New DevExpress.XtraGrid.Views.Grid.GridView()
            txtUnitType = New DevExpress.XtraEditors.TextEdit()
            Root = New DevExpress.XtraLayout.LayoutControlGroup()
            LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
            LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
            LayoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem()
            EmptySpaceItem2 = New DevExpress.XtraLayout.EmptySpaceItem()
            EmptySpaceItem3 = New DevExpress.XtraLayout.EmptySpaceItem()
            LayoutControlItem5 = New DevExpress.XtraLayout.LayoutControlItem()
            SimpleSeparator1 = New DevExpress.XtraLayout.SimpleSeparator()
            EmptySpaceItem5 = New DevExpress.XtraLayout.EmptySpaceItem()
            EmptySpaceItem4 = New DevExpress.XtraLayout.EmptySpaceItem()
            LayoutControlItem6 = New DevExpress.XtraLayout.LayoutControlItem()
            LayoutControlItem7 = New DevExpress.XtraLayout.LayoutControlItem()
            LayoutControlItem9 = New DevExpress.XtraLayout.LayoutControlItem()
            LayoutControlItem10 = New DevExpress.XtraLayout.LayoutControlItem()
            SimpleSeparator2 = New DevExpress.XtraLayout.SimpleSeparator()
            SimpleLabelItem1 = New DevExpress.XtraLayout.SimpleLabelItem()
            EmptySpaceItem1 = New DevExpress.XtraLayout.EmptySpaceItem()
            LayoutControlItem8 = New DevExpress.XtraLayout.LayoutControlItem()
            SimpleSeparator3 = New DevExpress.XtraLayout.SimpleSeparator()
            EmptySpaceItem6 = New DevExpress.XtraLayout.EmptySpaceItem()
            EmptySpaceItem8 = New DevExpress.XtraLayout.EmptySpaceItem()
            EmptySpaceItem9 = New DevExpress.XtraLayout.EmptySpaceItem()
            LayoutControlItem11 = New DevExpress.XtraLayout.LayoutControlItem()
            LayoutControlItem12 = New DevExpress.XtraLayout.LayoutControlItem()
            SimpleSeparator4 = New DevExpress.XtraLayout.SimpleSeparator()
            LayoutControlItem13 = New DevExpress.XtraLayout.LayoutControlItem()
            LayoutControlItem14 = New DevExpress.XtraLayout.LayoutControlItem()
            LayoutControlItem15 = New DevExpress.XtraLayout.LayoutControlItem()
            LayoutControlItem16 = New DevExpress.XtraLayout.LayoutControlItem()
            LayoutControlItem17 = New DevExpress.XtraLayout.LayoutControlItem()
            LayoutControlItem18 = New DevExpress.XtraLayout.LayoutControlItem()
            EmptySpaceItem10 = New DevExpress.XtraLayout.EmptySpaceItem()
            EmptySpaceItem11 = New DevExpress.XtraLayout.EmptySpaceItem()
            LayoutControlItem19 = New DevExpress.XtraLayout.LayoutControlItem()
            LayoutControlItem20 = New DevExpress.XtraLayout.LayoutControlItem()
            EmptySpaceItem12 = New DevExpress.XtraLayout.EmptySpaceItem()
            EmptySpaceItem13 = New DevExpress.XtraLayout.EmptySpaceItem()
            LayoutControlItem21 = New DevExpress.XtraLayout.LayoutControlItem()
            lblStatus = New DevExpress.XtraEditors.LabelControl()
            CType(LayoutControl1, ComponentModel.ISupportInitialize).BeginInit()
            LayoutControl1.SuspendLayout()
            CType(numDefaultPrice.Properties, ComponentModel.ISupportInitialize).BeginInit()
            CType(cmbDefaultTaxType.Properties, ComponentModel.ISupportInitialize).BeginInit()
            CType(numDefaultTaxRate.Properties, ComponentModel.ISupportInitialize).BeginInit()
            CType(chkIsActive.Properties, ComponentModel.ISupportInitialize).BeginInit()
            CType(txtCategory.Properties, ComponentModel.ISupportInitialize).BeginInit()
            CType(txtEgsCode.Properties, ComponentModel.ISupportInitialize).BeginInit()
            CType(txtDescription.Properties, ComponentModel.ISupportInitialize).BeginInit()
            CType(txtSearch.Properties, ComponentModel.ISupportInitialize).BeginInit()
            CType(gridCodes, ComponentModel.ISupportInitialize).BeginInit()
            CType(gridViewCodes, ComponentModel.ISupportInitialize).BeginInit()
            CType(txtUnitType.Properties, ComponentModel.ISupportInitialize).BeginInit()
            CType(Root, ComponentModel.ISupportInitialize).BeginInit()
            CType(LayoutControlItem1, ComponentModel.ISupportInitialize).BeginInit()
            CType(LayoutControlItem3, ComponentModel.ISupportInitialize).BeginInit()
            CType(LayoutControlItem4, ComponentModel.ISupportInitialize).BeginInit()
            CType(EmptySpaceItem2, ComponentModel.ISupportInitialize).BeginInit()
            CType(EmptySpaceItem3, ComponentModel.ISupportInitialize).BeginInit()
            CType(LayoutControlItem5, ComponentModel.ISupportInitialize).BeginInit()
            CType(SimpleSeparator1, ComponentModel.ISupportInitialize).BeginInit()
            CType(EmptySpaceItem5, ComponentModel.ISupportInitialize).BeginInit()
            CType(EmptySpaceItem4, ComponentModel.ISupportInitialize).BeginInit()
            CType(LayoutControlItem6, ComponentModel.ISupportInitialize).BeginInit()
            CType(LayoutControlItem7, ComponentModel.ISupportInitialize).BeginInit()
            CType(LayoutControlItem9, ComponentModel.ISupportInitialize).BeginInit()
            CType(LayoutControlItem10, ComponentModel.ISupportInitialize).BeginInit()
            CType(SimpleSeparator2, ComponentModel.ISupportInitialize).BeginInit()
            CType(SimpleLabelItem1, ComponentModel.ISupportInitialize).BeginInit()
            CType(EmptySpaceItem1, ComponentModel.ISupportInitialize).BeginInit()
            CType(LayoutControlItem8, ComponentModel.ISupportInitialize).BeginInit()
            CType(SimpleSeparator3, ComponentModel.ISupportInitialize).BeginInit()
            CType(EmptySpaceItem6, ComponentModel.ISupportInitialize).BeginInit()
            CType(EmptySpaceItem8, ComponentModel.ISupportInitialize).BeginInit()
            CType(EmptySpaceItem9, ComponentModel.ISupportInitialize).BeginInit()
            CType(LayoutControlItem11, ComponentModel.ISupportInitialize).BeginInit()
            CType(LayoutControlItem12, ComponentModel.ISupportInitialize).BeginInit()
            CType(SimpleSeparator4, ComponentModel.ISupportInitialize).BeginInit()
            CType(LayoutControlItem13, ComponentModel.ISupportInitialize).BeginInit()
            CType(LayoutControlItem14, ComponentModel.ISupportInitialize).BeginInit()
            CType(LayoutControlItem15, ComponentModel.ISupportInitialize).BeginInit()
            CType(LayoutControlItem16, ComponentModel.ISupportInitialize).BeginInit()
            CType(LayoutControlItem17, ComponentModel.ISupportInitialize).BeginInit()
            CType(LayoutControlItem18, ComponentModel.ISupportInitialize).BeginInit()
            CType(EmptySpaceItem10, ComponentModel.ISupportInitialize).BeginInit()
            CType(EmptySpaceItem11, ComponentModel.ISupportInitialize).BeginInit()
            CType(LayoutControlItem19, ComponentModel.ISupportInitialize).BeginInit()
            CType(LayoutControlItem20, ComponentModel.ISupportInitialize).BeginInit()
            CType(EmptySpaceItem12, ComponentModel.ISupportInitialize).BeginInit()
            CType(EmptySpaceItem13, ComponentModel.ISupportInitialize).BeginInit()
            CType(LayoutControlItem21, ComponentModel.ISupportInitialize).BeginInit()
            SuspendLayout()
            ' 
            ' lblPageTitle
            ' 
            lblPageTitle.Appearance.Font = New System.Drawing.Font("Segoe UI", 15F, Drawing.FontStyle.Bold)
            lblPageTitle.Appearance.Options.UseFont = True
            lblPageTitle.Location = New System.Drawing.Point(12, 12)
            lblPageTitle.Name = "lblPageTitle"
            lblPageTitle.Size = New System.Drawing.Size(161, 28)
            lblPageTitle.StyleController = LayoutControl1
            lblPageTitle.TabIndex = 1
            lblPageTitle.Text = "EGS Code Library"
            ' 
            ' LayoutControl1
            ' 
            LayoutControl1.Controls.Add(numDefaultPrice)
            LayoutControl1.Controls.Add(cmbDefaultTaxType)
            LayoutControl1.Controls.Add(numDefaultTaxRate)
            LayoutControl1.Controls.Add(btnNew)
            LayoutControl1.Controls.Add(chkIsActive)
            LayoutControl1.Controls.Add(btnSave)
            LayoutControl1.Controls.Add(txtCategory)
            LayoutControl1.Controls.Add(btnDelete)
            LayoutControl1.Controls.Add(txtEgsCode)
            LayoutControl1.Controls.Add(txtDescription)
            LayoutControl1.Controls.Add(btnImportFromPortal)
            LayoutControl1.Controls.Add(btnImportFromExcel)
            LayoutControl1.Controls.Add(btnDownloadTemplate)
            LayoutControl1.Controls.Add(btnRefresh)
            LayoutControl1.Controls.Add(txtSearch)
            LayoutControl1.Controls.Add(lblPageSubtitle)
            LayoutControl1.Controls.Add(btnSearch)
            LayoutControl1.Controls.Add(lblPageTitle)
            LayoutControl1.Controls.Add(gridCodes)
            LayoutControl1.Controls.Add(txtUnitType)
            LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
            LayoutControl1.Location = New System.Drawing.Point(0, 0)
            LayoutControl1.Name = "LayoutControl1"
            LayoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(939, 632, 650, 400)
            LayoutControl1.Root = Root
            LayoutControl1.Size = New System.Drawing.Size(1421, 600)
            LayoutControl1.TabIndex = 9
            LayoutControl1.Text = "LayoutControl1"
            ' 
            ' numDefaultPrice
            ' 
            numDefaultPrice.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
            numDefaultPrice.Location = New System.Drawing.Point(1062, 465)
            numDefaultPrice.Name = "numDefaultPrice"
            numDefaultPrice.Properties.DisplayFormat.FormatString = "N2"
            numDefaultPrice.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            numDefaultPrice.Properties.MaskSettings.Set("mask", "N2")
            numDefaultPrice.Properties.MaxValue = New Decimal(New Integer() {999999, 0, 0, 0})
            numDefaultPrice.Size = New System.Drawing.Size(80, 20)
            numDefaultPrice.StyleController = LayoutControl1
            numDefaultPrice.TabIndex = 12
            ' 
            ' cmbDefaultTaxType
            ' 
            cmbDefaultTaxType.Location = New System.Drawing.Point(1146, 465)
            cmbDefaultTaxType.Name = "cmbDefaultTaxType"
            cmbDefaultTaxType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
            cmbDefaultTaxType.Size = New System.Drawing.Size(91, 20)
            cmbDefaultTaxType.StyleController = LayoutControl1
            cmbDefaultTaxType.TabIndex = 13
            ' 
            ' numDefaultTaxRate
            ' 
            numDefaultTaxRate.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
            numDefaultTaxRate.Location = New System.Drawing.Point(1241, 465)
            numDefaultTaxRate.Name = "numDefaultTaxRate"
            numDefaultTaxRate.Properties.DisplayFormat.FormatString = "N2"
            numDefaultTaxRate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            numDefaultTaxRate.Properties.MaskSettings.Set("mask", "N2")
            numDefaultTaxRate.Properties.MaxValue = New Decimal(New Integer() {100, 0, 0, 0})
            numDefaultTaxRate.Size = New System.Drawing.Size(105, 20)
            numDefaultTaxRate.StyleController = LayoutControl1
            numDefaultTaxRate.TabIndex = 14
            ' 
            ' btnNew
            ' 
            btnNew.Location = New System.Drawing.Point(1025, 539)
            btnNew.Name = "btnNew"
            btnNew.Size = New System.Drawing.Size(117, 49)
            btnNew.StyleController = LayoutControl1
            btnNew.TabIndex = 16
            btnNew.Text = "Clear / New"
            ' 
            ' chkIsActive
            ' 
            chkIsActive.Location = New System.Drawing.Point(496, 449)
            chkIsActive.Name = "chkIsActive"
            chkIsActive.Properties.Caption = "Active"
            chkIsActive.Size = New System.Drawing.Size(53, 20)
            chkIsActive.StyleController = LayoutControl1
            chkIsActive.TabIndex = 9
            ' 
            ' btnSave
            ' 
            btnSave.Appearance.BackColor = Drawing.Color.FromArgb(CByte(20), CByte(110), CByte(70))
            btnSave.Appearance.ForeColor = Drawing.Color.White
            btnSave.Appearance.Options.UseBackColor = True
            btnSave.Appearance.Options.UseForeColor = True
            btnSave.Location = New System.Drawing.Point(1156, 539)
            btnSave.Name = "btnSave"
            btnSave.Size = New System.Drawing.Size(114, 49)
            btnSave.StyleController = LayoutControl1
            btnSave.TabIndex = 17
            btnSave.Text = "Save Item"
            ' 
            ' txtCategory
            ' 
            txtCategory.Location = New System.Drawing.Point(12, 505)
            txtCategory.Name = "txtCategory"
            txtCategory.Size = New System.Drawing.Size(480, 20)
            txtCategory.StyleController = LayoutControl1
            txtCategory.TabIndex = 15
            ' 
            ' btnDelete
            ' 
            btnDelete.Appearance.BackColor = Drawing.Color.FromArgb(CByte(150), CByte(30), CByte(30))
            btnDelete.Appearance.ForeColor = Drawing.Color.White
            btnDelete.Appearance.Options.UseBackColor = True
            btnDelete.Appearance.Options.UseForeColor = True
            btnDelete.Location = New System.Drawing.Point(1284, 539)
            btnDelete.Name = "btnDelete"
            btnDelete.Size = New System.Drawing.Size(125, 49)
            btnDelete.StyleController = LayoutControl1
            btnDelete.TabIndex = 18
            btnDelete.Text = "Delete Item"
            ' 
            ' txtEgsCode
            ' 
            txtEgsCode.Location = New System.Drawing.Point(12, 465)
            txtEgsCode.Name = "txtEgsCode"
            txtEgsCode.Size = New System.Drawing.Size(480, 20)
            txtEgsCode.StyleController = LayoutControl1
            txtEgsCode.TabIndex = 8
            ' 
            ' txtDescription
            ' 
            txtDescription.Location = New System.Drawing.Point(553, 465)
            txtDescription.Name = "txtDescription"
            txtDescription.Size = New System.Drawing.Size(430, 20)
            txtDescription.StyleController = LayoutControl1
            txtDescription.TabIndex = 10
            ' 
            ' btnImportFromPortal
            ' 
            btnImportFromPortal.Anchor = System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right
            btnImportFromPortal.Location = New System.Drawing.Point(649, 62)
            btnImportFromPortal.Name = "btnImportFromPortal"
            btnImportFromPortal.Size = New System.Drawing.Size(173, 73)
            btnImportFromPortal.StyleController = LayoutControl1
            btnImportFromPortal.TabIndex = 2
            btnImportFromPortal.Text = "Import Codes from Portal"
            ' 
            ' btnImportFromExcel
            ' 
            btnImportFromExcel.Location = New System.Drawing.Point(496, 80)
            btnImportFromExcel.Name = "btnImportFromExcel"
            btnImportFromExcel.Size = New System.Drawing.Size(127, 55)
            btnImportFromExcel.StyleController = LayoutControl1
            btnImportFromExcel.TabIndex = 4
            btnImportFromExcel.Text = "Import Codes from Excel"
            ' 
            ' btnDownloadTemplate
            ' 
            btnDownloadTemplate.Location = New System.Drawing.Point(359, 80)
            btnDownloadTemplate.Name = "btnDownloadTemplate"
            btnDownloadTemplate.Size = New System.Drawing.Size(133, 55)
            btnDownloadTemplate.StyleController = LayoutControl1
            btnDownloadTemplate.TabIndex = 3
            btnDownloadTemplate.Text = "Download Template"
            ' 
            ' btnRefresh
            ' 
            btnRefresh.Location = New System.Drawing.Point(143, 86)
            btnRefresh.Name = "btnRefresh"
            btnRefresh.Size = New System.Drawing.Size(117, 39)
            btnRefresh.StyleController = LayoutControl1
            btnRefresh.TabIndex = 6
            btnRefresh.Text = "Refresh"
            ' 
            ' txtSearch
            ' 
            txtSearch.Location = New System.Drawing.Point(12, 62)
            txtSearch.Name = "txtSearch"
            txtSearch.Properties.NullValuePrompt = "Search code or description..."
            txtSearch.Size = New System.Drawing.Size(248, 20)
            txtSearch.StyleController = LayoutControl1
            txtSearch.TabIndex = 0
            ' 
            ' lblPageSubtitle
            ' 
            lblPageSubtitle.Appearance.ForeColor = Drawing.Color.Gray
            lblPageSubtitle.Appearance.Options.UseForeColor = True
            lblPageSubtitle.Location = New System.Drawing.Point(12, 44)
            lblPageSubtitle.Name = "lblPageSubtitle"
            lblPageSubtitle.Size = New System.Drawing.Size(405, 13)
            lblPageSubtitle.StyleController = LayoutControl1
            lblPageSubtitle.TabIndex = 1
            lblPageSubtitle.Text = "Maintain your own item codes, or import codes already registered on the ETA portal."
            ' 
            ' btnSearch
            ' 
            btnSearch.Location = New System.Drawing.Point(25, 86)
            btnSearch.Name = "btnSearch"
            btnSearch.Size = New System.Drawing.Size(114, 39)
            btnSearch.StyleController = LayoutControl1
            btnSearch.TabIndex = 5
            btnSearch.Text = "Search"
            ' 
            ' gridCodes
            ' 
            gridCodes.Anchor = System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right
            gridCodes.Location = New System.Drawing.Point(12, 139)
            gridCodes.MainView = gridViewCodes
            gridCodes.Name = "gridCodes"
            gridCodes.Size = New System.Drawing.Size(1397, 305)
            gridCodes.TabIndex = 7
            gridCodes.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {gridViewCodes})
            ' 
            ' gridViewCodes
            ' 
            gridViewCodes.Appearance.HeaderPanel.Options.UseTextOptions = True
            gridViewCodes.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            gridViewCodes.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
            gridViewCodes.GridControl = gridCodes
            gridViewCodes.Name = "gridViewCodes"
            gridViewCodes.OptionsBehavior.Editable = False
            gridViewCodes.OptionsSelection.MultiSelect = True
            gridViewCodes.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect
            gridViewCodes.OptionsView.EnableAppearanceOddRow = True
            gridViewCodes.OptionsView.ShowGroupPanel = False
            ' 
            ' txtUnitType
            ' 
            txtUnitType.Location = New System.Drawing.Point(988, 465)
            txtUnitType.Name = "txtUnitType"
            txtUnitType.Size = New System.Drawing.Size(70, 20)
            txtUnitType.StyleController = LayoutControl1
            txtUnitType.TabIndex = 11
            ' 
            ' Root
            ' 
            Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True
            Root.GroupBordersVisible = False
            Root.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {LayoutControlItem1, LayoutControlItem3, LayoutControlItem4, EmptySpaceItem2, EmptySpaceItem3, LayoutControlItem5, SimpleSeparator1, EmptySpaceItem5, EmptySpaceItem4, LayoutControlItem6, LayoutControlItem7, LayoutControlItem9, LayoutControlItem10, SimpleSeparator2, SimpleLabelItem1, EmptySpaceItem1, LayoutControlItem8, SimpleSeparator3, EmptySpaceItem6, EmptySpaceItem8, EmptySpaceItem9, LayoutControlItem11, LayoutControlItem12, SimpleSeparator4, LayoutControlItem13, LayoutControlItem14, LayoutControlItem15, LayoutControlItem16, LayoutControlItem17, LayoutControlItem18, EmptySpaceItem10, EmptySpaceItem11, LayoutControlItem19, LayoutControlItem20, EmptySpaceItem12, EmptySpaceItem13, LayoutControlItem21})
            Root.Name = "Root"
            Root.Size = New System.Drawing.Size(1421, 600)
            Root.TextVisible = False
            ' 
            ' LayoutControlItem1
            ' 
            LayoutControlItem1.Control = gridCodes
            LayoutControlItem1.Location = New System.Drawing.Point(0, 127)
            LayoutControlItem1.Name = "LayoutControlItem1"
            LayoutControlItem1.Size = New System.Drawing.Size(1401, 309)
            LayoutControlItem1.TextVisible = False
            ' 
            ' LayoutControlItem3
            ' 
            LayoutControlItem3.Control = lblPageTitle
            LayoutControlItem3.Location = New System.Drawing.Point(0, 0)
            LayoutControlItem3.Name = "LayoutControlItem3"
            LayoutControlItem3.Size = New System.Drawing.Size(1401, 32)
            LayoutControlItem3.TextVisible = False
            ' 
            ' LayoutControlItem4
            ' 
            LayoutControlItem4.Control = lblPageSubtitle
            LayoutControlItem4.Location = New System.Drawing.Point(0, 32)
            LayoutControlItem4.Name = "LayoutControlItem4"
            LayoutControlItem4.Size = New System.Drawing.Size(1401, 17)
            LayoutControlItem4.TextVisible = False
            ' 
            ' EmptySpaceItem2
            ' 
            EmptySpaceItem2.Location = New System.Drawing.Point(814, 50)
            EmptySpaceItem2.Name = "EmptySpaceItem2"
            EmptySpaceItem2.Size = New System.Drawing.Size(587, 77)
            ' 
            ' EmptySpaceItem3
            ' 
            EmptySpaceItem3.Location = New System.Drawing.Point(252, 50)
            EmptySpaceItem3.MaxSize = New System.Drawing.Size(95, 0)
            EmptySpaceItem3.MinSize = New System.Drawing.Size(95, 10)
            EmptySpaceItem3.Name = "EmptySpaceItem3"
            EmptySpaceItem3.Size = New System.Drawing.Size(95, 77)
            EmptySpaceItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
            ' 
            ' LayoutControlItem5
            ' 
            LayoutControlItem5.Control = txtSearch
            LayoutControlItem5.Location = New System.Drawing.Point(0, 50)
            LayoutControlItem5.MaxSize = New System.Drawing.Size(252, 24)
            LayoutControlItem5.MinSize = New System.Drawing.Size(252, 24)
            LayoutControlItem5.Name = "LayoutControlItem5"
            LayoutControlItem5.Size = New System.Drawing.Size(252, 24)
            LayoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
            LayoutControlItem5.TextVisible = False
            ' 
            ' SimpleSeparator1
            ' 
            SimpleSeparator1.Location = New System.Drawing.Point(0, 49)
            SimpleSeparator1.Name = "SimpleSeparator1"
            SimpleSeparator1.Size = New System.Drawing.Size(1401, 1)
            ' 
            ' EmptySpaceItem5
            ' 
            EmptySpaceItem5.Location = New System.Drawing.Point(0, 74)
            EmptySpaceItem5.MaxSize = New System.Drawing.Size(0, 43)
            EmptySpaceItem5.MinSize = New System.Drawing.Size(10, 43)
            EmptySpaceItem5.Name = "EmptySpaceItem5"
            EmptySpaceItem5.Size = New System.Drawing.Size(13, 43)
            EmptySpaceItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
            ' 
            ' EmptySpaceItem4
            ' 
            EmptySpaceItem4.Location = New System.Drawing.Point(0, 117)
            EmptySpaceItem4.Name = "EmptySpaceItem4"
            EmptySpaceItem4.Size = New System.Drawing.Size(252, 10)
            ' 
            ' LayoutControlItem6
            ' 
            LayoutControlItem6.Control = btnSearch
            LayoutControlItem6.Location = New System.Drawing.Point(13, 74)
            LayoutControlItem6.MaxSize = New System.Drawing.Size(118, 43)
            LayoutControlItem6.MinSize = New System.Drawing.Size(118, 43)
            LayoutControlItem6.Name = "LayoutControlItem6"
            LayoutControlItem6.Size = New System.Drawing.Size(118, 43)
            LayoutControlItem6.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
            LayoutControlItem6.TextVisible = False
            ' 
            ' LayoutControlItem7
            ' 
            LayoutControlItem7.Control = btnRefresh
            LayoutControlItem7.Location = New System.Drawing.Point(131, 74)
            LayoutControlItem7.MaxSize = New System.Drawing.Size(121, 43)
            LayoutControlItem7.MinSize = New System.Drawing.Size(121, 43)
            LayoutControlItem7.Name = "LayoutControlItem7"
            LayoutControlItem7.Size = New System.Drawing.Size(121, 43)
            LayoutControlItem7.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
            LayoutControlItem7.TextVisible = False
            ' 
            ' LayoutControlItem9
            ' 
            LayoutControlItem9.Control = btnDownloadTemplate
            LayoutControlItem9.Location = New System.Drawing.Point(347, 68)
            LayoutControlItem9.MaxSize = New System.Drawing.Size(137, 59)
            LayoutControlItem9.MinSize = New System.Drawing.Size(137, 59)
            LayoutControlItem9.Name = "LayoutControlItem9"
            LayoutControlItem9.Size = New System.Drawing.Size(137, 59)
            LayoutControlItem9.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
            LayoutControlItem9.TextVisible = False
            ' 
            ' LayoutControlItem10
            ' 
            LayoutControlItem10.Control = btnImportFromExcel
            LayoutControlItem10.Location = New System.Drawing.Point(484, 68)
            LayoutControlItem10.MaxSize = New System.Drawing.Size(131, 59)
            LayoutControlItem10.MinSize = New System.Drawing.Size(131, 59)
            LayoutControlItem10.Name = "LayoutControlItem10"
            LayoutControlItem10.Size = New System.Drawing.Size(131, 59)
            LayoutControlItem10.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
            LayoutControlItem10.TextVisible = False
            ' 
            ' SimpleSeparator2
            ' 
            SimpleSeparator2.Location = New System.Drawing.Point(347, 50)
            SimpleSeparator2.Name = "SimpleSeparator2"
            SimpleSeparator2.Size = New System.Drawing.Size(268, 1)
            ' 
            ' SimpleLabelItem1
            ' 
            SimpleLabelItem1.AppearanceItemCaption.Font = New System.Drawing.Font("Tahoma", 8.25F, Drawing.FontStyle.Bold)
            SimpleLabelItem1.AppearanceItemCaption.ForeColor = Drawing.Color.Blue
            SimpleLabelItem1.AppearanceItemCaption.Options.UseFont = True
            SimpleLabelItem1.AppearanceItemCaption.Options.UseForeColor = True
            SimpleLabelItem1.Location = New System.Drawing.Point(347, 51)
            SimpleLabelItem1.Name = "SimpleLabelItem1"
            SimpleLabelItem1.Size = New System.Drawing.Size(268, 17)
            SimpleLabelItem1.Text = "Batch Codes Import"
            SimpleLabelItem1.TextSize = New System.Drawing.Size(112, 13)
            ' 
            ' EmptySpaceItem1
            ' 
            EmptySpaceItem1.Location = New System.Drawing.Point(615, 50)
            EmptySpaceItem1.MaxSize = New System.Drawing.Size(22, 0)
            EmptySpaceItem1.MinSize = New System.Drawing.Size(22, 10)
            EmptySpaceItem1.Name = "EmptySpaceItem1"
            EmptySpaceItem1.Size = New System.Drawing.Size(22, 77)
            EmptySpaceItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
            ' 
            ' LayoutControlItem8
            ' 
            LayoutControlItem8.Control = btnImportFromPortal
            LayoutControlItem8.Location = New System.Drawing.Point(637, 50)
            LayoutControlItem8.MaxSize = New System.Drawing.Size(177, 77)
            LayoutControlItem8.MinSize = New System.Drawing.Size(177, 77)
            LayoutControlItem8.Name = "LayoutControlItem8"
            LayoutControlItem8.Size = New System.Drawing.Size(177, 77)
            LayoutControlItem8.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
            LayoutControlItem8.TextVisible = False
            ' 
            ' SimpleSeparator3
            ' 
            SimpleSeparator3.Location = New System.Drawing.Point(0, 436)
            SimpleSeparator3.Name = "SimpleSeparator3"
            SimpleSeparator3.Size = New System.Drawing.Size(1401, 1)
            ' 
            ' EmptySpaceItem6
            ' 
            EmptySpaceItem6.Location = New System.Drawing.Point(1338, 437)
            EmptySpaceItem6.Name = "EmptySpaceItem6"
            EmptySpaceItem6.Size = New System.Drawing.Size(63, 90)
            ' 
            ' EmptySpaceItem8
            ' 
            EmptySpaceItem8.Location = New System.Drawing.Point(0, 517)
            EmptySpaceItem8.MaxSize = New System.Drawing.Size(484, 0)
            EmptySpaceItem8.MinSize = New System.Drawing.Size(484, 10)
            EmptySpaceItem8.Name = "EmptySpaceItem8"
            EmptySpaceItem8.Size = New System.Drawing.Size(484, 10)
            EmptySpaceItem8.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
            ' 
            ' EmptySpaceItem9
            ' 
            EmptySpaceItem9.Location = New System.Drawing.Point(541, 477)
            EmptySpaceItem9.MaxSize = New System.Drawing.Size(434, 0)
            EmptySpaceItem9.MinSize = New System.Drawing.Size(434, 10)
            EmptySpaceItem9.Name = "EmptySpaceItem9"
            EmptySpaceItem9.Size = New System.Drawing.Size(434, 50)
            EmptySpaceItem9.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
            ' 
            ' LayoutControlItem11
            ' 
            LayoutControlItem11.Control = txtEgsCode
            LayoutControlItem11.Location = New System.Drawing.Point(0, 437)
            LayoutControlItem11.Name = "LayoutControlItem11"
            LayoutControlItem11.Size = New System.Drawing.Size(484, 40)
            LayoutControlItem11.Text = "EGS Code:"
            LayoutControlItem11.TextLocation = DevExpress.Utils.Locations.Top
            LayoutControlItem11.TextSize = New System.Drawing.Size(112, 13)
            ' 
            ' LayoutControlItem12
            ' 
            LayoutControlItem12.Control = txtDescription
            LayoutControlItem12.Location = New System.Drawing.Point(541, 437)
            LayoutControlItem12.Name = "LayoutControlItem12"
            LayoutControlItem12.Size = New System.Drawing.Size(434, 40)
            LayoutControlItem12.Text = "Description:"
            LayoutControlItem12.TextLocation = DevExpress.Utils.Locations.Top
            LayoutControlItem12.TextSize = New System.Drawing.Size(112, 13)
            ' 
            ' SimpleSeparator4
            ' 
            SimpleSeparator4.Location = New System.Drawing.Point(975, 437)
            SimpleSeparator4.Name = "SimpleSeparator4"
            SimpleSeparator4.Size = New System.Drawing.Size(1, 90)
            ' 
            ' LayoutControlItem13
            ' 
            LayoutControlItem13.Control = txtUnitType
            LayoutControlItem13.Location = New System.Drawing.Point(976, 437)
            LayoutControlItem13.MaxSize = New System.Drawing.Size(74, 40)
            LayoutControlItem13.MinSize = New System.Drawing.Size(74, 40)
            LayoutControlItem13.Name = "LayoutControlItem13"
            LayoutControlItem13.Size = New System.Drawing.Size(74, 90)
            LayoutControlItem13.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
            LayoutControlItem13.Text = "Unit Type:"
            LayoutControlItem13.TextLocation = DevExpress.Utils.Locations.Top
            LayoutControlItem13.TextSize = New System.Drawing.Size(112, 13)
            ' 
            ' LayoutControlItem14
            ' 
            LayoutControlItem14.Control = numDefaultPrice
            LayoutControlItem14.Location = New System.Drawing.Point(1050, 437)
            LayoutControlItem14.MaxSize = New System.Drawing.Size(84, 40)
            LayoutControlItem14.MinSize = New System.Drawing.Size(84, 40)
            LayoutControlItem14.Name = "LayoutControlItem14"
            LayoutControlItem14.Size = New System.Drawing.Size(84, 90)
            LayoutControlItem14.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
            LayoutControlItem14.Text = "Default Price:"
            LayoutControlItem14.TextLocation = DevExpress.Utils.Locations.Top
            LayoutControlItem14.TextSize = New System.Drawing.Size(112, 13)
            ' 
            ' LayoutControlItem15
            ' 
            LayoutControlItem15.Control = cmbDefaultTaxType
            LayoutControlItem15.Location = New System.Drawing.Point(1134, 437)
            LayoutControlItem15.MaxSize = New System.Drawing.Size(95, 40)
            LayoutControlItem15.MinSize = New System.Drawing.Size(95, 40)
            LayoutControlItem15.Name = "LayoutControlItem15"
            LayoutControlItem15.Size = New System.Drawing.Size(95, 90)
            LayoutControlItem15.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
            LayoutControlItem15.Text = "Tax Type:"
            LayoutControlItem15.TextLocation = DevExpress.Utils.Locations.Top
            LayoutControlItem15.TextSize = New System.Drawing.Size(112, 13)
            ' 
            ' LayoutControlItem16
            ' 
            LayoutControlItem16.Control = numDefaultTaxRate
            LayoutControlItem16.Location = New System.Drawing.Point(1229, 437)
            LayoutControlItem16.MaxSize = New System.Drawing.Size(109, 40)
            LayoutControlItem16.MinSize = New System.Drawing.Size(109, 40)
            LayoutControlItem16.Name = "LayoutControlItem16"
            LayoutControlItem16.Size = New System.Drawing.Size(109, 90)
            LayoutControlItem16.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
            LayoutControlItem16.Text = "Tax Rate (%):"
            LayoutControlItem16.TextLocation = DevExpress.Utils.Locations.Top
            LayoutControlItem16.TextSize = New System.Drawing.Size(112, 13)
            ' 
            ' LayoutControlItem17
            ' 
            LayoutControlItem17.Control = txtCategory
            LayoutControlItem17.Location = New System.Drawing.Point(0, 477)
            LayoutControlItem17.Name = "LayoutControlItem17"
            LayoutControlItem17.Size = New System.Drawing.Size(484, 40)
            LayoutControlItem17.Text = "Category:"
            LayoutControlItem17.TextLocation = DevExpress.Utils.Locations.Top
            LayoutControlItem17.TextSize = New System.Drawing.Size(112, 13)
            ' 
            ' LayoutControlItem18
            ' 
            LayoutControlItem18.Control = chkIsActive
            LayoutControlItem18.Location = New System.Drawing.Point(484, 437)
            LayoutControlItem18.MaxSize = New System.Drawing.Size(57, 24)
            LayoutControlItem18.MinSize = New System.Drawing.Size(57, 24)
            LayoutControlItem18.Name = "LayoutControlItem18"
            LayoutControlItem18.Size = New System.Drawing.Size(57, 90)
            LayoutControlItem18.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
            LayoutControlItem18.TextVisible = False
            ' 
            ' EmptySpaceItem10
            ' 
            EmptySpaceItem10.Location = New System.Drawing.Point(340, 527)
            EmptySpaceItem10.Name = "EmptySpaceItem10"
            EmptySpaceItem10.Size = New System.Drawing.Size(673, 53)
            ' 
            ' EmptySpaceItem11
            ' 
            EmptySpaceItem11.Location = New System.Drawing.Point(0, 527)
            EmptySpaceItem11.MaxSize = New System.Drawing.Size(0, 53)
            EmptySpaceItem11.MinSize = New System.Drawing.Size(10, 53)
            EmptySpaceItem11.Name = "EmptySpaceItem11"
            EmptySpaceItem11.Size = New System.Drawing.Size(340, 53)
            EmptySpaceItem11.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
            ' 
            ' LayoutControlItem19
            ' 
            LayoutControlItem19.Control = btnDelete
            LayoutControlItem19.Location = New System.Drawing.Point(1272, 527)
            LayoutControlItem19.MaxSize = New System.Drawing.Size(129, 53)
            LayoutControlItem19.MinSize = New System.Drawing.Size(129, 53)
            LayoutControlItem19.Name = "LayoutControlItem19"
            LayoutControlItem19.Size = New System.Drawing.Size(129, 53)
            LayoutControlItem19.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
            LayoutControlItem19.TextVisible = False
            ' 
            ' LayoutControlItem20
            ' 
            LayoutControlItem20.Control = btnSave
            LayoutControlItem20.Location = New System.Drawing.Point(1144, 527)
            LayoutControlItem20.MaxSize = New System.Drawing.Size(118, 53)
            LayoutControlItem20.MinSize = New System.Drawing.Size(118, 53)
            LayoutControlItem20.Name = "LayoutControlItem20"
            LayoutControlItem20.Size = New System.Drawing.Size(118, 53)
            LayoutControlItem20.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
            LayoutControlItem20.TextVisible = False
            ' 
            ' EmptySpaceItem12
            ' 
            EmptySpaceItem12.Location = New System.Drawing.Point(1262, 527)
            EmptySpaceItem12.MaxSize = New System.Drawing.Size(10, 0)
            EmptySpaceItem12.MinSize = New System.Drawing.Size(10, 10)
            EmptySpaceItem12.Name = "EmptySpaceItem12"
            EmptySpaceItem12.Size = New System.Drawing.Size(10, 53)
            EmptySpaceItem12.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
            ' 
            ' EmptySpaceItem13
            ' 
            EmptySpaceItem13.Location = New System.Drawing.Point(1134, 527)
            EmptySpaceItem13.MaxSize = New System.Drawing.Size(10, 0)
            EmptySpaceItem13.MinSize = New System.Drawing.Size(10, 10)
            EmptySpaceItem13.Name = "EmptySpaceItem13"
            EmptySpaceItem13.Size = New System.Drawing.Size(10, 53)
            EmptySpaceItem13.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
            ' 
            ' LayoutControlItem21
            ' 
            LayoutControlItem21.Control = btnNew
            LayoutControlItem21.Location = New System.Drawing.Point(1013, 527)
            LayoutControlItem21.MaxSize = New System.Drawing.Size(121, 53)
            LayoutControlItem21.MinSize = New System.Drawing.Size(121, 53)
            LayoutControlItem21.Name = "LayoutControlItem21"
            LayoutControlItem21.Size = New System.Drawing.Size(121, 53)
            LayoutControlItem21.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
            LayoutControlItem21.TextVisible = False
            ' 
            ' lblStatus
            ' 
            lblStatus.Anchor = System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right
            lblStatus.Appearance.ForeColor = Drawing.Color.DarkRed
            lblStatus.Appearance.Options.UseForeColor = True
            lblStatus.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
            lblStatus.Location = New System.Drawing.Point(20, 545)
            lblStatus.Name = "lblStatus"
            lblStatus.Size = New System.Drawing.Size(1331, 30)
            lblStatus.TabIndex = 8
            ' 
            ' EgsCodeLibraryForm
            ' 
            AutoScaleDimensions = New System.Drawing.SizeF(96F, 96F)
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
            ClientSize = New System.Drawing.Size(1421, 600)
            Controls.Add(LayoutControl1)
            Controls.Add(lblStatus)
            Name = "EgsCodeLibraryForm"
            Text = "EGS Code Library"
            CType(LayoutControl1, ComponentModel.ISupportInitialize).EndInit()
            LayoutControl1.ResumeLayout(False)
            CType(numDefaultPrice.Properties, ComponentModel.ISupportInitialize).EndInit()
            CType(cmbDefaultTaxType.Properties, ComponentModel.ISupportInitialize).EndInit()
            CType(numDefaultTaxRate.Properties, ComponentModel.ISupportInitialize).EndInit()
            CType(chkIsActive.Properties, ComponentModel.ISupportInitialize).EndInit()
            CType(txtCategory.Properties, ComponentModel.ISupportInitialize).EndInit()
            CType(txtEgsCode.Properties, ComponentModel.ISupportInitialize).EndInit()
            CType(txtDescription.Properties, ComponentModel.ISupportInitialize).EndInit()
            CType(txtSearch.Properties, ComponentModel.ISupportInitialize).EndInit()
            CType(gridCodes, ComponentModel.ISupportInitialize).EndInit()
            CType(gridViewCodes, ComponentModel.ISupportInitialize).EndInit()
            CType(txtUnitType.Properties, ComponentModel.ISupportInitialize).EndInit()
            CType(Root, ComponentModel.ISupportInitialize).EndInit()
            CType(LayoutControlItem1, ComponentModel.ISupportInitialize).EndInit()
            CType(LayoutControlItem3, ComponentModel.ISupportInitialize).EndInit()
            CType(LayoutControlItem4, ComponentModel.ISupportInitialize).EndInit()
            CType(EmptySpaceItem2, ComponentModel.ISupportInitialize).EndInit()
            CType(EmptySpaceItem3, ComponentModel.ISupportInitialize).EndInit()
            CType(LayoutControlItem5, ComponentModel.ISupportInitialize).EndInit()
            CType(SimpleSeparator1, ComponentModel.ISupportInitialize).EndInit()
            CType(EmptySpaceItem5, ComponentModel.ISupportInitialize).EndInit()
            CType(EmptySpaceItem4, ComponentModel.ISupportInitialize).EndInit()
            CType(LayoutControlItem6, ComponentModel.ISupportInitialize).EndInit()
            CType(LayoutControlItem7, ComponentModel.ISupportInitialize).EndInit()
            CType(LayoutControlItem9, ComponentModel.ISupportInitialize).EndInit()
            CType(LayoutControlItem10, ComponentModel.ISupportInitialize).EndInit()
            CType(SimpleSeparator2, ComponentModel.ISupportInitialize).EndInit()
            CType(SimpleLabelItem1, ComponentModel.ISupportInitialize).EndInit()
            CType(EmptySpaceItem1, ComponentModel.ISupportInitialize).EndInit()
            CType(LayoutControlItem8, ComponentModel.ISupportInitialize).EndInit()
            CType(SimpleSeparator3, ComponentModel.ISupportInitialize).EndInit()
            CType(EmptySpaceItem6, ComponentModel.ISupportInitialize).EndInit()
            CType(EmptySpaceItem8, ComponentModel.ISupportInitialize).EndInit()
            CType(EmptySpaceItem9, ComponentModel.ISupportInitialize).EndInit()
            CType(LayoutControlItem11, ComponentModel.ISupportInitialize).EndInit()
            CType(LayoutControlItem12, ComponentModel.ISupportInitialize).EndInit()
            CType(SimpleSeparator4, ComponentModel.ISupportInitialize).EndInit()
            CType(LayoutControlItem13, ComponentModel.ISupportInitialize).EndInit()
            CType(LayoutControlItem14, ComponentModel.ISupportInitialize).EndInit()
            CType(LayoutControlItem15, ComponentModel.ISupportInitialize).EndInit()
            CType(LayoutControlItem16, ComponentModel.ISupportInitialize).EndInit()
            CType(LayoutControlItem17, ComponentModel.ISupportInitialize).EndInit()
            CType(LayoutControlItem18, ComponentModel.ISupportInitialize).EndInit()
            CType(EmptySpaceItem10, ComponentModel.ISupportInitialize).EndInit()
            CType(EmptySpaceItem11, ComponentModel.ISupportInitialize).EndInit()
            CType(LayoutControlItem19, ComponentModel.ISupportInitialize).EndInit()
            CType(LayoutControlItem20, ComponentModel.ISupportInitialize).EndInit()
            CType(EmptySpaceItem12, ComponentModel.ISupportInitialize).EndInit()
            CType(EmptySpaceItem13, ComponentModel.ISupportInitialize).EndInit()
            CType(LayoutControlItem21, ComponentModel.ISupportInitialize).EndInit()
            ResumeLayout(False)
        End Sub

        Friend WithEvents lblPageTitle As DevExpress.XtraEditors.LabelControl
        Friend WithEvents lblPageSubtitle As DevExpress.XtraEditors.LabelControl
        Friend WithEvents txtSearch As DevExpress.XtraEditors.TextEdit
        Friend WithEvents btnSearch As DevExpress.XtraEditors.SimpleButton
        Friend WithEvents btnRefresh As DevExpress.XtraEditors.SimpleButton
        Friend WithEvents btnImportFromPortal As DevExpress.XtraEditors.SimpleButton
        Friend WithEvents btnImportFromExcel As DevExpress.XtraEditors.SimpleButton
        Friend WithEvents btnDownloadTemplate As DevExpress.XtraEditors.SimpleButton
        Friend WithEvents gridCodes As DevExpress.XtraGrid.GridControl
        Friend WithEvents gridViewCodes As DevExpress.XtraGrid.Views.Grid.GridView
        Friend WithEvents txtEgsCode As DevExpress.XtraEditors.TextEdit
        Friend WithEvents txtDescription As DevExpress.XtraEditors.TextEdit
        Friend WithEvents txtUnitType As DevExpress.XtraEditors.TextEdit
        Friend WithEvents numDefaultPrice As DevExpress.XtraEditors.SpinEdit
        Friend WithEvents cmbDefaultTaxType As DevExpress.XtraEditors.ComboBoxEdit
        Friend WithEvents numDefaultTaxRate As DevExpress.XtraEditors.SpinEdit
        Friend WithEvents txtCategory As DevExpress.XtraEditors.TextEdit
        Friend WithEvents chkIsActive As DevExpress.XtraEditors.CheckEdit
        Friend WithEvents btnNew As DevExpress.XtraEditors.SimpleButton
        Friend WithEvents btnSave As DevExpress.XtraEditors.SimpleButton
        Friend WithEvents btnDelete As DevExpress.XtraEditors.SimpleButton
        Friend WithEvents lblStatus As DevExpress.XtraEditors.LabelControl
        Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
        Friend WithEvents Root As DevExpress.XtraLayout.LayoutControlGroup
        Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
        Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
        Friend WithEvents LayoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem
        Friend WithEvents EmptySpaceItem2 As DevExpress.XtraLayout.EmptySpaceItem
        Friend WithEvents EmptySpaceItem3 As DevExpress.XtraLayout.EmptySpaceItem
        Friend WithEvents LayoutControlItem5 As DevExpress.XtraLayout.LayoutControlItem
        Friend WithEvents SimpleSeparator1 As DevExpress.XtraLayout.SimpleSeparator
        Friend WithEvents EmptySpaceItem5 As DevExpress.XtraLayout.EmptySpaceItem
        Friend WithEvents EmptySpaceItem4 As DevExpress.XtraLayout.EmptySpaceItem
        Friend WithEvents LayoutControlItem6 As DevExpress.XtraLayout.LayoutControlItem
        Friend WithEvents LayoutControlItem7 As DevExpress.XtraLayout.LayoutControlItem
        Friend WithEvents LayoutControlItem9 As DevExpress.XtraLayout.LayoutControlItem
        Friend WithEvents LayoutControlItem10 As DevExpress.XtraLayout.LayoutControlItem
        Friend WithEvents SimpleSeparator2 As DevExpress.XtraLayout.SimpleSeparator
        Friend WithEvents SimpleLabelItem1 As DevExpress.XtraLayout.SimpleLabelItem
        Friend WithEvents EmptySpaceItem1 As DevExpress.XtraLayout.EmptySpaceItem
        Friend WithEvents LayoutControlItem8 As DevExpress.XtraLayout.LayoutControlItem
        Friend WithEvents SimpleSeparator3 As DevExpress.XtraLayout.SimpleSeparator
        Friend WithEvents EmptySpaceItem6 As DevExpress.XtraLayout.EmptySpaceItem
        Friend WithEvents EmptySpaceItem8 As DevExpress.XtraLayout.EmptySpaceItem
        Friend WithEvents EmptySpaceItem9 As DevExpress.XtraLayout.EmptySpaceItem
        Friend WithEvents LayoutControlItem11 As DevExpress.XtraLayout.LayoutControlItem
        Friend WithEvents LayoutControlItem12 As DevExpress.XtraLayout.LayoutControlItem
        Friend WithEvents SimpleSeparator4 As DevExpress.XtraLayout.SimpleSeparator
        Friend WithEvents LayoutControlItem13 As DevExpress.XtraLayout.LayoutControlItem
        Friend WithEvents LayoutControlItem14 As DevExpress.XtraLayout.LayoutControlItem
        Friend WithEvents LayoutControlItem15 As DevExpress.XtraLayout.LayoutControlItem
        Friend WithEvents LayoutControlItem16 As DevExpress.XtraLayout.LayoutControlItem
        Friend WithEvents LayoutControlItem17 As DevExpress.XtraLayout.LayoutControlItem
        Friend WithEvents LayoutControlItem18 As DevExpress.XtraLayout.LayoutControlItem
        Friend WithEvents EmptySpaceItem10 As DevExpress.XtraLayout.EmptySpaceItem
        Friend WithEvents EmptySpaceItem11 As DevExpress.XtraLayout.EmptySpaceItem
        Friend WithEvents LayoutControlItem19 As DevExpress.XtraLayout.LayoutControlItem
        Friend WithEvents LayoutControlItem20 As DevExpress.XtraLayout.LayoutControlItem
        Friend WithEvents EmptySpaceItem12 As DevExpress.XtraLayout.EmptySpaceItem
        Friend WithEvents EmptySpaceItem13 As DevExpress.XtraLayout.EmptySpaceItem
        Friend WithEvents LayoutControlItem21 As DevExpress.XtraLayout.LayoutControlItem
    End Class
End Namespace
