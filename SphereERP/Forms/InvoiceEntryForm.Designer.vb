Namespace Forms
    Partial Class InvoiceEntryForm
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
            Dim SuperToolTip1 As DevExpress.Utils.SuperToolTip = New DevExpress.Utils.SuperToolTip()
            Dim ToolTipItem1 As DevExpress.Utils.ToolTipItem = New DevExpress.Utils.ToolTipItem()
            lblPageTitle = New DevExpress.XtraEditors.LabelControl()
            grpDocument = New DevExpress.XtraEditors.GroupControl()
            lblDocumentType = New DevExpress.XtraEditors.LabelControl()
            cmbDocumentType = New DevExpress.XtraEditors.ComboBoxEdit()
            lblInternalId = New DevExpress.XtraEditors.LabelControl()
            txtInternalId = New DevExpress.XtraEditors.TextEdit()
            lblDateIssued = New DevExpress.XtraEditors.LabelControl()
            dtDateIssued = New DevExpress.XtraEditors.DateEdit()
            lblPaymentMethod = New DevExpress.XtraEditors.LabelControl()
            cmbPaymentMethod = New DevExpress.XtraEditors.ComboBoxEdit()
            lblReferenceUuid = New DevExpress.XtraEditors.LabelControl()
            txtReferenceUuid = New DevExpress.XtraEditors.TextEdit()
            lblCurrency = New DevExpress.XtraEditors.LabelControl()
            cmbCurrency = New DevExpress.XtraEditors.ComboBoxEdit()
            lblCurrencyExchangeRate = New DevExpress.XtraEditors.LabelControl()
            txtCurrencyExchangeRate = New DevExpress.XtraEditors.TextEdit()
            grpReceiver = New DevExpress.XtraEditors.GroupControl()
            lblReceiverType = New DevExpress.XtraEditors.LabelControl()
            cmbReceiverType = New DevExpress.XtraEditors.ComboBoxEdit()
            lblReceiverId = New DevExpress.XtraEditors.LabelControl()
            txtReceiverId = New DevExpress.XtraEditors.TextEdit()
            lblReceiverName = New DevExpress.XtraEditors.LabelControl()
            txtReceiverName = New DevExpress.XtraEditors.TextEdit()
            lblReceiverGovernate = New DevExpress.XtraEditors.LabelControl()
            txtReceiverGovernate = New DevExpress.XtraEditors.TextEdit()
            lblReceiverCity = New DevExpress.XtraEditors.LabelControl()
            txtReceiverCity = New DevExpress.XtraEditors.TextEdit()
            lblReceiverStreet = New DevExpress.XtraEditors.LabelControl()
            txtReceiverStreet = New DevExpress.XtraEditors.TextEdit()
            lblReceiverBuilding = New DevExpress.XtraEditors.LabelControl()
            txtReceiverBuilding = New DevExpress.XtraEditors.TextEdit()
            lblReceiverCountry = New DevExpress.XtraEditors.LabelControl()
            cmbReceiverCountry = New DevExpress.XtraEditors.ComboBoxEdit()
            lblLineItemsHeader = New DevExpress.XtraEditors.LabelControl()
            btnAddLine = New DevExpress.XtraEditors.SimpleButton()
            btnEditLine = New DevExpress.XtraEditors.SimpleButton()
            btnRemoveLine = New DevExpress.XtraEditors.SimpleButton()
            btnPickFromEgs = New DevExpress.XtraEditors.SimpleButton()
            gridLines = New DevExpress.XtraGrid.GridControl()
            gridViewLines = New DevExpress.XtraGrid.Views.Grid.GridView()
            lblTotals = New DevExpress.XtraEditors.LabelControl()
            btnValidate = New DevExpress.XtraEditors.SimpleButton()
            btnCheckRejection = New DevExpress.XtraEditors.SimpleButton()
            btnSignAndSubmit = New DevExpress.XtraEditors.SimpleButton()
            btnDownloadPdf = New DevExpress.XtraEditors.SimpleButton()
            lblStatus = New DevExpress.XtraEditors.MemoEdit()
            CType(grpDocument, ComponentModel.ISupportInitialize).BeginInit()
            grpDocument.SuspendLayout()
            CType(cmbDocumentType.Properties, ComponentModel.ISupportInitialize).BeginInit()
            CType(txtInternalId.Properties, ComponentModel.ISupportInitialize).BeginInit()
            CType(dtDateIssued.Properties, ComponentModel.ISupportInitialize).BeginInit()
            CType(dtDateIssued.Properties.CalendarTimeProperties, ComponentModel.ISupportInitialize).BeginInit()
            CType(cmbPaymentMethod.Properties, ComponentModel.ISupportInitialize).BeginInit()
            CType(txtReferenceUuid.Properties, ComponentModel.ISupportInitialize).BeginInit()
            CType(cmbCurrency.Properties, ComponentModel.ISupportInitialize).BeginInit()
            CType(txtCurrencyExchangeRate.Properties, ComponentModel.ISupportInitialize).BeginInit()
            CType(grpReceiver, ComponentModel.ISupportInitialize).BeginInit()
            grpReceiver.SuspendLayout()
            CType(cmbReceiverType.Properties, ComponentModel.ISupportInitialize).BeginInit()
            CType(txtReceiverId.Properties, ComponentModel.ISupportInitialize).BeginInit()
            CType(txtReceiverName.Properties, ComponentModel.ISupportInitialize).BeginInit()
            CType(txtReceiverGovernate.Properties, ComponentModel.ISupportInitialize).BeginInit()
            CType(txtReceiverCity.Properties, ComponentModel.ISupportInitialize).BeginInit()
            CType(txtReceiverStreet.Properties, ComponentModel.ISupportInitialize).BeginInit()
            CType(txtReceiverBuilding.Properties, ComponentModel.ISupportInitialize).BeginInit()
            CType(cmbReceiverCountry.Properties, ComponentModel.ISupportInitialize).BeginInit()
            CType(gridLines, ComponentModel.ISupportInitialize).BeginInit()
            CType(gridViewLines, ComponentModel.ISupportInitialize).BeginInit()
            CType(lblStatus.Properties, ComponentModel.ISupportInitialize).BeginInit()
            SuspendLayout()
            ' 
            ' lblPageTitle
            ' 
            lblPageTitle.Appearance.Font = New System.Drawing.Font("Segoe UI", 15F, Drawing.FontStyle.Bold)
            lblPageTitle.Appearance.Options.UseFont = True
            lblPageTitle.Location = New System.Drawing.Point(20, 15)
            lblPageTitle.Name = "lblPageTitle"
            lblPageTitle.Size = New System.Drawing.Size(147, 28)
            lblPageTitle.TabIndex = 0
            lblPageTitle.Text = "New Document"
            ' 
            ' grpDocument
            ' 
            grpDocument.Controls.Add(lblDocumentType)
            grpDocument.Controls.Add(cmbDocumentType)
            grpDocument.Controls.Add(lblInternalId)
            grpDocument.Controls.Add(txtInternalId)
            grpDocument.Controls.Add(lblDateIssued)
            grpDocument.Controls.Add(dtDateIssued)
            grpDocument.Controls.Add(lblPaymentMethod)
            grpDocument.Controls.Add(cmbPaymentMethod)
            grpDocument.Controls.Add(lblReferenceUuid)
            grpDocument.Controls.Add(txtReferenceUuid)
            grpDocument.Controls.Add(lblCurrency)
            grpDocument.Controls.Add(cmbCurrency)
            grpDocument.Controls.Add(lblCurrencyExchangeRate)
            grpDocument.Controls.Add(txtCurrencyExchangeRate)
            grpDocument.Location = New System.Drawing.Point(20, 55)
            grpDocument.Name = "grpDocument"
            grpDocument.Size = New System.Drawing.Size(500, 215)
            grpDocument.TabIndex = 1
            grpDocument.Text = "Document Details"
            ' 
            ' lblDocumentType
            ' 
            lblDocumentType.Location = New System.Drawing.Point(15, 28)
            lblDocumentType.Name = "lblDocumentType"
            lblDocumentType.Size = New System.Drawing.Size(79, 13)
            lblDocumentType.TabIndex = 0
            lblDocumentType.Text = "Document Type:"
            ' 
            ' cmbDocumentType
            ' 
            cmbDocumentType.Location = New System.Drawing.Point(15, 47)
            cmbDocumentType.Name = "cmbDocumentType"
            cmbDocumentType.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
            cmbDocumentType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
            cmbDocumentType.Size = New System.Drawing.Size(220, 20)
            cmbDocumentType.TabIndex = 1
            ' 
            ' lblInternalId
            ' 
            lblInternalId.Location = New System.Drawing.Point(15, 75)
            lblInternalId.Name = "lblInternalId"
            lblInternalId.Size = New System.Drawing.Size(164, 13)
            lblInternalId.TabIndex = 2
            lblInternalId.Text = "Internal ID (your invoice number):"
            ' 
            ' txtInternalId
            ' 
            txtInternalId.Location = New System.Drawing.Point(15, 94)
            txtInternalId.Name = "txtInternalId"
            txtInternalId.Size = New System.Drawing.Size(220, 20)
            txtInternalId.TabIndex = 3
            ' 
            ' lblDateIssued
            ' 
            lblDateIssued.Location = New System.Drawing.Point(255, 75)
            lblDateIssued.Name = "lblDateIssued"
            lblDateIssued.Size = New System.Drawing.Size(82, 13)
            lblDateIssued.TabIndex = 4
            lblDateIssued.Text = "Issue Date/Time:"
            ' 
            ' dtDateIssued
            ' 
            dtDateIssued.EditValue = Nothing
            dtDateIssued.Location = New System.Drawing.Point(255, 94)
            dtDateIssued.Name = "dtDateIssued"
            dtDateIssued.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
            dtDateIssued.Properties.CalendarTimeEditing = DevExpress.Utils.DefaultBoolean.True
            dtDateIssued.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
            dtDateIssued.Properties.CalendarView = DevExpress.XtraEditors.Repository.CalendarView.Vista
            dtDateIssued.Properties.Mask.UseMaskAsDisplayFormat = True
            dtDateIssued.Properties.MaskSettings.Set("mask", "yyyy-MM-dd HH:mm")
            dtDateIssued.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.True
            dtDateIssued.Size = New System.Drawing.Size(220, 20)
            dtDateIssued.TabIndex = 5
            ' 
            ' lblPaymentMethod
            ' 
            lblPaymentMethod.Location = New System.Drawing.Point(15, 122)
            lblPaymentMethod.Name = "lblPaymentMethod"
            lblPaymentMethod.Size = New System.Drawing.Size(85, 13)
            lblPaymentMethod.TabIndex = 6
            lblPaymentMethod.Text = "Payment Method:"
            ' 
            ' cmbPaymentMethod
            ' 
            cmbPaymentMethod.Location = New System.Drawing.Point(15, 141)
            cmbPaymentMethod.Name = "cmbPaymentMethod"
            cmbPaymentMethod.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
            cmbPaymentMethod.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
            cmbPaymentMethod.Size = New System.Drawing.Size(220, 20)
            cmbPaymentMethod.TabIndex = 7
            ' 
            ' lblReferenceUuid
            ' 
            lblReferenceUuid.Location = New System.Drawing.Point(255, 122)
            lblReferenceUuid.Name = "lblReferenceUuid"
            lblReferenceUuid.Size = New System.Drawing.Size(120, 13)
            lblReferenceUuid.TabIndex = 8
            lblReferenceUuid.Text = "Reference Invoice UUID:"
            ' 
            ' txtReferenceUuid
            ' 
            txtReferenceUuid.Location = New System.Drawing.Point(255, 141)
            txtReferenceUuid.Name = "txtReferenceUuid"
            txtReferenceUuid.Size = New System.Drawing.Size(220, 20)
            txtReferenceUuid.TabIndex = 9
            ' 
            ' lblCurrency
            ' 
            lblCurrency.Location = New System.Drawing.Point(15, 169)
            lblCurrency.Name = "lblCurrency"
            lblCurrency.Size = New System.Drawing.Size(48, 13)
            lblCurrency.TabIndex = 10
            lblCurrency.Text = "Currency:"
            ' 
            ' cmbCurrency
            ' 
            cmbCurrency.Location = New System.Drawing.Point(15, 188)
            cmbCurrency.Name = "cmbCurrency"
            cmbCurrency.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
            cmbCurrency.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
            cmbCurrency.Size = New System.Drawing.Size(110, 20)
            cmbCurrency.TabIndex = 11
            ' 
            ' lblCurrencyExchangeRate
            ' 
            lblCurrencyExchangeRate.Location = New System.Drawing.Point(255, 169)
            lblCurrencyExchangeRate.Name = "lblCurrencyExchangeRate"
            lblCurrencyExchangeRate.Size = New System.Drawing.Size(77, 13)
            lblCurrencyExchangeRate.TabIndex = 12
            lblCurrencyExchangeRate.Text = "Exchange Rate:"
            ' 
            ' txtCurrencyExchangeRate
            ' 
            txtCurrencyExchangeRate.Location = New System.Drawing.Point(255, 188)
            txtCurrencyExchangeRate.Name = "txtCurrencyExchangeRate"
            txtCurrencyExchangeRate.Size = New System.Drawing.Size(110, 20)
            txtCurrencyExchangeRate.TabIndex = 13
            ' 
            ' grpReceiver
            ' 
            grpReceiver.Controls.Add(lblReceiverType)
            grpReceiver.Controls.Add(cmbReceiverType)
            grpReceiver.Controls.Add(lblReceiverId)
            grpReceiver.Controls.Add(txtReceiverId)
            grpReceiver.Controls.Add(lblReceiverName)
            grpReceiver.Controls.Add(txtReceiverName)
            grpReceiver.Controls.Add(lblReceiverGovernate)
            grpReceiver.Controls.Add(txtReceiverGovernate)
            grpReceiver.Controls.Add(lblReceiverCity)
            grpReceiver.Controls.Add(txtReceiverCity)
            grpReceiver.Controls.Add(lblReceiverStreet)
            grpReceiver.Controls.Add(txtReceiverStreet)
            grpReceiver.Controls.Add(lblReceiverBuilding)
            grpReceiver.Controls.Add(txtReceiverBuilding)
            grpReceiver.Controls.Add(lblReceiverCountry)
            grpReceiver.Controls.Add(cmbReceiverCountry)
            grpReceiver.Location = New System.Drawing.Point(559, 55)
            grpReceiver.Name = "grpReceiver"
            grpReceiver.Size = New System.Drawing.Size(530, 175)
            grpReceiver.TabIndex = 2
            grpReceiver.Text = "Receiver (Buyer)"
            ' 
            ' lblReceiverType
            ' 
            lblReceiverType.Location = New System.Drawing.Point(15, 28)
            lblReceiverType.Name = "lblReceiverType"
            lblReceiverType.Size = New System.Drawing.Size(28, 13)
            lblReceiverType.TabIndex = 0
            lblReceiverType.Text = "Type:"
            ' 
            ' cmbReceiverType
            ' 
            cmbReceiverType.Location = New System.Drawing.Point(15, 47)
            cmbReceiverType.Name = "cmbReceiverType"
            cmbReceiverType.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
            cmbReceiverType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
            cmbReceiverType.Size = New System.Drawing.Size(110, 20)
            cmbReceiverType.TabIndex = 1
            ' 
            ' lblReceiverId
            ' 
            lblReceiverId.Location = New System.Drawing.Point(140, 28)
            lblReceiverId.Name = "lblReceiverId"
            lblReceiverId.Size = New System.Drawing.Size(85, 13)
            lblReceiverId.TabIndex = 2
            lblReceiverId.Text = "RIN / National ID:"
            ' 
            ' txtReceiverId
            ' 
            txtReceiverId.Location = New System.Drawing.Point(140, 47)
            txtReceiverId.Name = "txtReceiverId"
            txtReceiverId.Size = New System.Drawing.Size(160, 20)
            ToolTipItem1.Text = "Double Click for Details"
            SuperToolTip1.Items.Add(ToolTipItem1)
            txtReceiverId.SuperTip = SuperToolTip1
            txtReceiverId.TabIndex = 3
            txtReceiverId.ToolTip = "Double Click for Details"
            ' 
            ' lblReceiverName
            ' 
            lblReceiverName.Location = New System.Drawing.Point(315, 28)
            lblReceiverName.Name = "lblReceiverName"
            lblReceiverName.Size = New System.Drawing.Size(31, 13)
            lblReceiverName.TabIndex = 4
            lblReceiverName.Text = "Name:"
            ' 
            ' txtReceiverName
            ' 
            txtReceiverName.Enabled = False
            txtReceiverName.Location = New System.Drawing.Point(315, 47)
            txtReceiverName.Name = "txtReceiverName"
            txtReceiverName.Size = New System.Drawing.Size(200, 20)
            txtReceiverName.TabIndex = 5
            ' 
            ' lblReceiverGovernate
            ' 
            lblReceiverGovernate.Location = New System.Drawing.Point(15, 80)
            lblReceiverGovernate.Name = "lblReceiverGovernate"
            lblReceiverGovernate.Size = New System.Drawing.Size(55, 13)
            lblReceiverGovernate.TabIndex = 6
            lblReceiverGovernate.Text = "Governate:"
            ' 
            ' txtReceiverGovernate
            ' 
            txtReceiverGovernate.Enabled = False
            txtReceiverGovernate.Location = New System.Drawing.Point(15, 99)
            txtReceiverGovernate.Name = "txtReceiverGovernate"
            txtReceiverGovernate.Size = New System.Drawing.Size(120, 20)
            txtReceiverGovernate.TabIndex = 7
            ' 
            ' lblReceiverCity
            ' 
            lblReceiverCity.Location = New System.Drawing.Point(145, 80)
            lblReceiverCity.Name = "lblReceiverCity"
            lblReceiverCity.Size = New System.Drawing.Size(60, 13)
            lblReceiverCity.TabIndex = 8
            lblReceiverCity.Text = "Region/City:"
            ' 
            ' txtReceiverCity
            ' 
            txtReceiverCity.Enabled = False
            txtReceiverCity.Location = New System.Drawing.Point(145, 99)
            txtReceiverCity.Name = "txtReceiverCity"
            txtReceiverCity.Size = New System.Drawing.Size(120, 20)
            txtReceiverCity.TabIndex = 9
            ' 
            ' lblReceiverStreet
            ' 
            lblReceiverStreet.Location = New System.Drawing.Point(275, 80)
            lblReceiverStreet.Name = "lblReceiverStreet"
            lblReceiverStreet.Size = New System.Drawing.Size(34, 13)
            lblReceiverStreet.TabIndex = 10
            lblReceiverStreet.Text = "Street:"
            ' 
            ' txtReceiverStreet
            ' 
            txtReceiverStreet.Enabled = False
            txtReceiverStreet.Location = New System.Drawing.Point(275, 99)
            txtReceiverStreet.Name = "txtReceiverStreet"
            txtReceiverStreet.Size = New System.Drawing.Size(160, 20)
            txtReceiverStreet.TabIndex = 11
            ' 
            ' lblReceiverBuilding
            ' 
            lblReceiverBuilding.Location = New System.Drawing.Point(445, 80)
            lblReceiverBuilding.Name = "lblReceiverBuilding"
            lblReceiverBuilding.Size = New System.Drawing.Size(35, 13)
            lblReceiverBuilding.TabIndex = 12
            lblReceiverBuilding.Text = "Bldg #:"
            ' 
            ' txtReceiverBuilding
            ' 
            txtReceiverBuilding.Enabled = False
            txtReceiverBuilding.Location = New System.Drawing.Point(445, 99)
            txtReceiverBuilding.Name = "txtReceiverBuilding"
            txtReceiverBuilding.Size = New System.Drawing.Size(70, 20)
            txtReceiverBuilding.TabIndex = 13
            ' 
            ' lblReceiverCountry
            ' 
            lblReceiverCountry.Location = New System.Drawing.Point(15, 128)
            lblReceiverCountry.Name = "lblReceiverCountry"
            lblReceiverCountry.Size = New System.Drawing.Size(43, 13)
            lblReceiverCountry.TabIndex = 14
            lblReceiverCountry.Text = "Country:"
            ' 
            ' cmbReceiverCountry
            ' 
            cmbReceiverCountry.Enabled = False
            cmbReceiverCountry.Location = New System.Drawing.Point(15, 147)
            cmbReceiverCountry.Name = "cmbReceiverCountry"
            cmbReceiverCountry.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
            cmbReceiverCountry.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
            cmbReceiverCountry.Size = New System.Drawing.Size(120, 20)
            cmbReceiverCountry.TabIndex = 15
            ' 
            ' lblLineItemsHeader
            ' 
            lblLineItemsHeader.Appearance.Font = New System.Drawing.Font("Segoe UI", 11F, Drawing.FontStyle.Bold)
            lblLineItemsHeader.Appearance.Options.UseFont = True
            lblLineItemsHeader.Location = New System.Drawing.Point(20, 286)
            lblLineItemsHeader.Name = "lblLineItemsHeader"
            lblLineItemsHeader.Size = New System.Drawing.Size(73, 20)
            lblLineItemsHeader.TabIndex = 3
            lblLineItemsHeader.Text = "Line Items"
            ' 
            ' btnAddLine
            ' 
            btnAddLine.Location = New System.Drawing.Point(740, 276)
            btnAddLine.Name = "btnAddLine"
            btnAddLine.Size = New System.Drawing.Size(90, 30)
            btnAddLine.TabIndex = 5
            btnAddLine.Text = "Add"
            ' 
            ' btnEditLine
            ' 
            btnEditLine.Location = New System.Drawing.Point(835, 276)
            btnEditLine.Name = "btnEditLine"
            btnEditLine.Size = New System.Drawing.Size(90, 30)
            btnEditLine.TabIndex = 6
            btnEditLine.Text = "Edit"
            ' 
            ' btnRemoveLine
            ' 
            btnRemoveLine.Location = New System.Drawing.Point(930, 276)
            btnRemoveLine.Name = "btnRemoveLine"
            btnRemoveLine.Size = New System.Drawing.Size(90, 30)
            btnRemoveLine.TabIndex = 7
            btnRemoveLine.Text = "Remove"
            ' 
            ' btnPickFromEgs
            ' 
            btnPickFromEgs.Location = New System.Drawing.Point(580, 276)
            btnPickFromEgs.Name = "btnPickFromEgs"
            btnPickFromEgs.Size = New System.Drawing.Size(150, 30)
            btnPickFromEgs.TabIndex = 4
            btnPickFromEgs.Text = "Add from EGS Library..."
            ' 
            ' gridLines
            ' 
            gridLines.Anchor = System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right
            gridLines.Location = New System.Drawing.Point(20, 313)
            gridLines.MainView = gridViewLines
            gridLines.Name = "gridLines"
            gridLines.Size = New System.Drawing.Size(1000, 236)
            gridLines.TabIndex = 8
            gridLines.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {gridViewLines})
            ' 
            ' gridViewLines
            ' 
            gridViewLines.GridControl = gridLines
            gridViewLines.Name = "gridViewLines"
            gridViewLines.OptionsBehavior.Editable = False
            gridViewLines.OptionsView.EnableAppearanceOddRow = True
            gridViewLines.OptionsView.ShowFooter = True
            gridViewLines.OptionsView.ShowGroupPanel = False
            ' 
            ' lblTotals
            ' 
            lblTotals.Anchor = System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left
            lblTotals.Appearance.Font = New System.Drawing.Font("Segoe UI", 11F, Drawing.FontStyle.Bold)
            lblTotals.Appearance.Options.UseFont = True
            lblTotals.Location = New System.Drawing.Point(20, 555)
            lblTotals.Name = "lblTotals"
            lblTotals.Size = New System.Drawing.Size(107, 20)
            lblTotals.TabIndex = 9
            lblTotals.Text = "Total: 0.00 EGP"
            ' 
            ' btnValidate
            ' 
            btnValidate.Anchor = System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right
            btnValidate.Location = New System.Drawing.Point(748, 555)
            btnValidate.Name = "btnValidate"
            btnValidate.Size = New System.Drawing.Size(110, 36)
            btnValidate.TabIndex = 10
            btnValidate.Text = "Validate"
            ' 
            ' btnCheckRejection
            ' 
            btnCheckRejection.Anchor = System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right
            btnCheckRejection.Location = New System.Drawing.Point(502, 555)
            btnCheckRejection.Name = "btnCheckRejection"
            btnCheckRejection.Size = New System.Drawing.Size(116, 36)
            btnCheckRejection.TabIndex = 14
            btnCheckRejection.Text = "Rejection Risk"
            ' 
            ' btnSignAndSubmit
            ' 
            btnSignAndSubmit.Anchor = System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right
            btnSignAndSubmit.Appearance.BackColor = Drawing.Color.FromArgb(CByte(20), CByte(110), CByte(70))
            btnSignAndSubmit.Appearance.Font = New System.Drawing.Font("Segoe UI", 9.5F, Drawing.FontStyle.Bold)
            btnSignAndSubmit.Appearance.ForeColor = Drawing.Color.White
            btnSignAndSubmit.Appearance.Options.UseBackColor = True
            btnSignAndSubmit.Appearance.Options.UseFont = True
            btnSignAndSubmit.Appearance.Options.UseForeColor = True
            btnSignAndSubmit.Location = New System.Drawing.Point(868, 555)
            btnSignAndSubmit.Name = "btnSignAndSubmit"
            btnSignAndSubmit.Size = New System.Drawing.Size(150, 36)
            btnSignAndSubmit.TabIndex = 12
            btnSignAndSubmit.Text = "Sign && Submit"
            ' 
            ' btnDownloadPdf
            ' 
            btnDownloadPdf.Anchor = System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right
            btnDownloadPdf.Enabled = False
            btnDownloadPdf.Location = New System.Drawing.Point(628, 555)
            btnDownloadPdf.Name = "btnDownloadPdf"
            btnDownloadPdf.Size = New System.Drawing.Size(110, 36)
            btnDownloadPdf.TabIndex = 11
            btnDownloadPdf.Text = "Print/PDF"
            ' 
            ' lblStatus
            ' 
            lblStatus.Anchor = System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right
            lblStatus.Location = New System.Drawing.Point(161, 554)
            lblStatus.Name = "lblStatus"
            lblStatus.Properties.Appearance.ForeColor = Drawing.Color.DarkRed
            lblStatus.Properties.Appearance.Options.UseForeColor = True
            lblStatus.Properties.ReadOnly = True
            lblStatus.Size = New System.Drawing.Size(331, 74)
            lblStatus.TabIndex = 13
            ' 
            ' InvoiceEntryForm
            ' 
            AutoScaleDimensions = New System.Drawing.SizeF(96F, 96F)
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
            ClientSize = New System.Drawing.Size(1090, 640)
            Controls.Add(lblPageTitle)
            Controls.Add(grpDocument)
            Controls.Add(grpReceiver)
            Controls.Add(lblLineItemsHeader)
            Controls.Add(btnPickFromEgs)
            Controls.Add(btnAddLine)
            Controls.Add(btnEditLine)
            Controls.Add(btnRemoveLine)
            Controls.Add(gridLines)
            Controls.Add(lblTotals)
            Controls.Add(btnValidate)
            Controls.Add(btnCheckRejection)
            Controls.Add(btnDownloadPdf)
            Controls.Add(btnSignAndSubmit)
            Controls.Add(lblStatus)
            Name = "InvoiceEntryForm"
            Text = "New Document"
            CType(grpDocument, ComponentModel.ISupportInitialize).EndInit()
            grpDocument.ResumeLayout(False)
            grpDocument.PerformLayout()
            CType(cmbDocumentType.Properties, ComponentModel.ISupportInitialize).EndInit()
            CType(txtInternalId.Properties, ComponentModel.ISupportInitialize).EndInit()
            CType(dtDateIssued.Properties.CalendarTimeProperties, ComponentModel.ISupportInitialize).EndInit()
            CType(dtDateIssued.Properties, ComponentModel.ISupportInitialize).EndInit()
            CType(cmbPaymentMethod.Properties, ComponentModel.ISupportInitialize).EndInit()
            CType(txtReferenceUuid.Properties, ComponentModel.ISupportInitialize).EndInit()
            CType(cmbCurrency.Properties, ComponentModel.ISupportInitialize).EndInit()
            CType(txtCurrencyExchangeRate.Properties, ComponentModel.ISupportInitialize).EndInit()
            CType(grpReceiver, ComponentModel.ISupportInitialize).EndInit()
            grpReceiver.ResumeLayout(False)
            grpReceiver.PerformLayout()
            CType(cmbReceiverType.Properties, ComponentModel.ISupportInitialize).EndInit()
            CType(txtReceiverId.Properties, ComponentModel.ISupportInitialize).EndInit()
            CType(txtReceiverName.Properties, ComponentModel.ISupportInitialize).EndInit()
            CType(txtReceiverGovernate.Properties, ComponentModel.ISupportInitialize).EndInit()
            CType(txtReceiverCity.Properties, ComponentModel.ISupportInitialize).EndInit()
            CType(txtReceiverStreet.Properties, ComponentModel.ISupportInitialize).EndInit()
            CType(txtReceiverBuilding.Properties, ComponentModel.ISupportInitialize).EndInit()
            CType(cmbReceiverCountry.Properties, ComponentModel.ISupportInitialize).EndInit()
            CType(gridLines, ComponentModel.ISupportInitialize).EndInit()
            CType(gridViewLines, ComponentModel.ISupportInitialize).EndInit()
            CType(lblStatus.Properties, ComponentModel.ISupportInitialize).EndInit()
            ResumeLayout(False)
            PerformLayout()
        End Sub

        Friend WithEvents lblPageTitle As DevExpress.XtraEditors.LabelControl
        Friend WithEvents grpDocument As DevExpress.XtraEditors.GroupControl
        Friend WithEvents lblDocumentType As DevExpress.XtraEditors.LabelControl
        Friend WithEvents cmbDocumentType As DevExpress.XtraEditors.ComboBoxEdit
        Friend WithEvents lblInternalId As DevExpress.XtraEditors.LabelControl
        Friend WithEvents txtInternalId As DevExpress.XtraEditors.TextEdit
        Friend WithEvents lblDateIssued As DevExpress.XtraEditors.LabelControl
        Friend WithEvents dtDateIssued As DevExpress.XtraEditors.DateEdit
        Friend WithEvents lblPaymentMethod As DevExpress.XtraEditors.LabelControl
        Friend WithEvents cmbPaymentMethod As DevExpress.XtraEditors.ComboBoxEdit
        Friend WithEvents lblReferenceUuid As DevExpress.XtraEditors.LabelControl
        Friend WithEvents txtReferenceUuid As DevExpress.XtraEditors.TextEdit
        Friend WithEvents lblCurrency As DevExpress.XtraEditors.LabelControl
        Friend WithEvents cmbCurrency As DevExpress.XtraEditors.ComboBoxEdit
        Friend WithEvents lblCurrencyExchangeRate As DevExpress.XtraEditors.LabelControl
        Friend WithEvents txtCurrencyExchangeRate As DevExpress.XtraEditors.TextEdit
        Friend WithEvents grpReceiver As DevExpress.XtraEditors.GroupControl
        Friend WithEvents lblReceiverType As DevExpress.XtraEditors.LabelControl
        Friend WithEvents cmbReceiverType As DevExpress.XtraEditors.ComboBoxEdit
        Friend WithEvents lblReceiverId As DevExpress.XtraEditors.LabelControl
        Friend WithEvents txtReceiverId As DevExpress.XtraEditors.TextEdit
        Friend WithEvents lblReceiverName As DevExpress.XtraEditors.LabelControl
        Friend WithEvents txtReceiverName As DevExpress.XtraEditors.TextEdit
        Friend WithEvents lblReceiverGovernate As DevExpress.XtraEditors.LabelControl
        Friend WithEvents txtReceiverGovernate As DevExpress.XtraEditors.TextEdit
        Friend WithEvents lblReceiverCity As DevExpress.XtraEditors.LabelControl
        Friend WithEvents txtReceiverCity As DevExpress.XtraEditors.TextEdit
        Friend WithEvents lblReceiverStreet As DevExpress.XtraEditors.LabelControl
        Friend WithEvents txtReceiverStreet As DevExpress.XtraEditors.TextEdit
        Friend WithEvents lblReceiverBuilding As DevExpress.XtraEditors.LabelControl
        Friend WithEvents txtReceiverBuilding As DevExpress.XtraEditors.TextEdit
        Friend WithEvents lblReceiverCountry As DevExpress.XtraEditors.LabelControl
        Friend WithEvents cmbReceiverCountry As DevExpress.XtraEditors.ComboBoxEdit
        Friend WithEvents lblLineItemsHeader As DevExpress.XtraEditors.LabelControl
        Friend WithEvents btnAddLine As DevExpress.XtraEditors.SimpleButton
        Friend WithEvents btnEditLine As DevExpress.XtraEditors.SimpleButton
        Friend WithEvents btnRemoveLine As DevExpress.XtraEditors.SimpleButton
        Friend WithEvents btnPickFromEgs As DevExpress.XtraEditors.SimpleButton
        Friend WithEvents gridLines As DevExpress.XtraGrid.GridControl
        Friend WithEvents gridViewLines As DevExpress.XtraGrid.Views.Grid.GridView
        Friend WithEvents lblTotals As DevExpress.XtraEditors.LabelControl
        Friend WithEvents btnValidate As DevExpress.XtraEditors.SimpleButton
        Friend WithEvents btnCheckRejection As DevExpress.XtraEditors.SimpleButton
        Friend WithEvents btnSignAndSubmit As DevExpress.XtraEditors.SimpleButton
        Friend WithEvents btnDownloadPdf As DevExpress.XtraEditors.SimpleButton
        Friend WithEvents lblStatus As DevExpress.XtraEditors.MemoEdit
    End Class
End Namespace
