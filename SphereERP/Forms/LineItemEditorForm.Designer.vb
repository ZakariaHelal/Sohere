Namespace Forms
    Partial Class LineItemEditorForm
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
            lblDescription = New DevExpress.XtraEditors.LabelControl()
            txtDescription = New DevExpress.XtraEditors.TextEdit()
            lblItemCode = New DevExpress.XtraEditors.LabelControl()
            txtItemCode = New DevExpress.XtraEditors.TextEdit()
            lblItemType = New DevExpress.XtraEditors.LabelControl()
            cmbItemType = New DevExpress.XtraEditors.ComboBoxEdit()
            lblUnitType = New DevExpress.XtraEditors.LabelControl()
            txtUnitType = New DevExpress.XtraEditors.TextEdit()
            lblQuantity = New DevExpress.XtraEditors.LabelControl()
            numQuantity = New DevExpress.XtraEditors.SpinEdit()
            lblUnitPrice = New DevExpress.XtraEditors.LabelControl()
            numUnitPrice = New DevExpress.XtraEditors.SpinEdit()
            lblDiscount = New DevExpress.XtraEditors.LabelControl()
            numDiscount = New DevExpress.XtraEditors.SpinEdit()
            lblTaxType = New DevExpress.XtraEditors.LabelControl()
            cmbTaxType = New DevExpress.XtraEditors.ComboBoxEdit()
            lblTaxSubType = New DevExpress.XtraEditors.LabelControl()
            cmbTaxSubType = New DevExpress.XtraEditors.ComboBoxEdit()
            lblTaxRate = New DevExpress.XtraEditors.LabelControl()
            numTaxRate = New DevExpress.XtraEditors.SpinEdit()
            lblComputedTotal = New DevExpress.XtraEditors.LabelControl()
            btnOK = New DevExpress.XtraEditors.SimpleButton()
            btnCancel = New DevExpress.XtraEditors.SimpleButton()
            CType(txtDescription.Properties, ComponentModel.ISupportInitialize).BeginInit()
            CType(txtItemCode.Properties, ComponentModel.ISupportInitialize).BeginInit()
            CType(cmbItemType.Properties, ComponentModel.ISupportInitialize).BeginInit()
            CType(txtUnitType.Properties, ComponentModel.ISupportInitialize).BeginInit()
            CType(numQuantity.Properties, ComponentModel.ISupportInitialize).BeginInit()
            CType(numUnitPrice.Properties, ComponentModel.ISupportInitialize).BeginInit()
            CType(numDiscount.Properties, ComponentModel.ISupportInitialize).BeginInit()
            CType(cmbTaxType.Properties, ComponentModel.ISupportInitialize).BeginInit()
            CType(cmbTaxSubType.Properties, ComponentModel.ISupportInitialize).BeginInit()
            CType(numTaxRate.Properties, ComponentModel.ISupportInitialize).BeginInit()
            SuspendLayout()
            ' 
            ' lblDescription
            ' 
            lblDescription.Location = New System.Drawing.Point(15, 15)
            lblDescription.Name = "lblDescription"
            lblDescription.Size = New System.Drawing.Size(57, 13)
            lblDescription.TabIndex = 0
            lblDescription.Text = "Description:"
            ' 
            ' txtDescription
            ' 
            txtDescription.Location = New System.Drawing.Point(15, 35)
            txtDescription.Name = "txtDescription"
            txtDescription.Size = New System.Drawing.Size(420, 20)
            txtDescription.TabIndex = 1
            ' 
            ' lblItemCode
            ' 
            lblItemCode.Location = New System.Drawing.Point(15, 70)
            lblItemCode.Name = "lblItemCode"
            lblItemCode.Size = New System.Drawing.Size(107, 13)
            lblItemCode.TabIndex = 2
            lblItemCode.Text = "Item Code (EGS/GS1):"
            ' 
            ' txtItemCode
            ' 
            txtItemCode.Location = New System.Drawing.Point(15, 90)
            txtItemCode.Name = "txtItemCode"
            txtItemCode.Size = New System.Drawing.Size(200, 20)
            txtItemCode.TabIndex = 3
            ' 
            ' lblItemType
            ' 
            lblItemType.Location = New System.Drawing.Point(235, 70)
            lblItemType.Name = "lblItemType"
            lblItemType.Size = New System.Drawing.Size(53, 13)
            lblItemType.TabIndex = 4
            lblItemType.Text = "Item Type:"
            ' 
            ' cmbItemType
            ' 
            cmbItemType.Location = New System.Drawing.Point(235, 90)
            cmbItemType.Name = "cmbItemType"
            cmbItemType.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
            cmbItemType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
            cmbItemType.Size = New System.Drawing.Size(95, 20)
            cmbItemType.TabIndex = 5
            ' 
            ' lblUnitType
            ' 
            lblUnitType.Location = New System.Drawing.Point(345, 70)
            lblUnitType.Name = "lblUnitType"
            lblUnitType.Size = New System.Drawing.Size(50, 13)
            lblUnitType.TabIndex = 6
            lblUnitType.Text = "Unit Type:"
            ' 
            ' txtUnitType
            ' 
            txtUnitType.Location = New System.Drawing.Point(345, 90)
            txtUnitType.Name = "txtUnitType"
            txtUnitType.Size = New System.Drawing.Size(90, 20)
            txtUnitType.TabIndex = 7
            ' 
            ' lblQuantity
            ' 
            lblQuantity.Location = New System.Drawing.Point(15, 125)
            lblQuantity.Name = "lblQuantity"
            lblQuantity.Size = New System.Drawing.Size(46, 13)
            lblQuantity.TabIndex = 8
            lblQuantity.Text = "Quantity:"
            ' 
            ' numQuantity
            ' 
            numQuantity.EditValue = New Decimal(New Integer() {1, 0, 0, 0})
            numQuantity.Location = New System.Drawing.Point(15, 145)
            numQuantity.Name = "numQuantity"
            numQuantity.Properties.DisplayFormat.FormatString = "N3"
            numQuantity.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            numQuantity.Properties.MaskSettings.Set("mask", "N3")
            numQuantity.Properties.MaxValue = New Decimal(New Integer() {1000000, 0, 0, 0})
            numQuantity.Properties.MinValue = New Decimal(New Integer() {1, 0, 0, 196608})
            numQuantity.Size = New System.Drawing.Size(120, 20)
            numQuantity.TabIndex = 9
            ' 
            ' lblUnitPrice
            ' 
            lblUnitPrice.Location = New System.Drawing.Point(150, 125)
            lblUnitPrice.Name = "lblUnitPrice"
            lblUnitPrice.Size = New System.Drawing.Size(79, 13)
            lblUnitPrice.TabIndex = 10
            lblUnitPrice.Text = "Unit Price (EGP):"
            ' 
            ' numUnitPrice
            ' 
            numUnitPrice.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
            numUnitPrice.Location = New System.Drawing.Point(150, 145)
            numUnitPrice.Name = "numUnitPrice"
            numUnitPrice.Properties.DisplayFormat.FormatString = "N2"
            numUnitPrice.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            numUnitPrice.Properties.MaskSettings.Set("mask", "N2")
            numUnitPrice.Properties.MaxValue = New Decimal(New Integer() {100000000, 0, 0, 0})
            numUnitPrice.Size = New System.Drawing.Size(120, 20)
            numUnitPrice.TabIndex = 11
            ' 
            ' lblDiscount
            ' 
            lblDiscount.Location = New System.Drawing.Point(285, 125)
            lblDiscount.Name = "lblDiscount"
            lblDiscount.Size = New System.Drawing.Size(85, 13)
            lblDiscount.TabIndex = 12
            lblDiscount.Text = "Discount Amount:"
            ' 
            ' numDiscount
            ' 
            numDiscount.EditValue = New Decimal(New Integer() {0, 0, 0, 0})
            numDiscount.Location = New System.Drawing.Point(285, 145)
            numDiscount.Name = "numDiscount"
            numDiscount.Properties.DisplayFormat.FormatString = "N2"
            numDiscount.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            numDiscount.Properties.MaskSettings.Set("mask", "N2")
            numDiscount.Properties.MaxValue = New Decimal(New Integer() {100000000, 0, 0, 0})
            numDiscount.Size = New System.Drawing.Size(120, 20)
            numDiscount.TabIndex = 13
            ' 
            ' lblTaxType
            ' 
            lblTaxType.Location = New System.Drawing.Point(15, 180)
            lblTaxType.Name = "lblTaxType"
            lblTaxType.Size = New System.Drawing.Size(49, 13)
            lblTaxType.TabIndex = 14
            lblTaxType.Text = "Tax Type:"
            ' 
            ' cmbTaxType
            ' 
            cmbTaxType.Location = New System.Drawing.Point(15, 200)
            cmbTaxType.Name = "cmbTaxType"
            cmbTaxType.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
            cmbTaxType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
            cmbTaxType.Size = New System.Drawing.Size(120, 20)
            cmbTaxType.TabIndex = 15
            ' 
            ' lblTaxSubType
            ' 
            lblTaxSubType.Location = New System.Drawing.Point(150, 180)
            lblTaxSubType.Name = "lblTaxSubType"
            lblTaxSubType.Size = New System.Drawing.Size(67, 13)
            lblTaxSubType.TabIndex = 16
            lblTaxSubType.Text = "Tax SubType:"
            ' 
            ' cmbTaxSubType
            ' 
            cmbTaxSubType.Location = New System.Drawing.Point(150, 200)
            cmbTaxSubType.Name = "cmbTaxSubType"
            cmbTaxSubType.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton()})
            cmbTaxSubType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
            cmbTaxSubType.Size = New System.Drawing.Size(160, 20)
            cmbTaxSubType.TabIndex = 17
            ' 
            ' lblTaxRate
            ' 
            lblTaxRate.Location = New System.Drawing.Point(325, 180)
            lblTaxRate.Name = "lblTaxRate"
            lblTaxRate.Size = New System.Drawing.Size(70, 13)
            lblTaxRate.TabIndex = 18
            lblTaxRate.Text = "Tax Rate (%):"
            ' 
            ' numTaxRate
            ' 
            numTaxRate.EditValue = New Decimal(New Integer() {14, 0, 0, 0})
            numTaxRate.Location = New System.Drawing.Point(325, 200)
            numTaxRate.Name = "numTaxRate"
            numTaxRate.Properties.DisplayFormat.FormatString = "N2"
            numTaxRate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            numTaxRate.Properties.MaskSettings.Set("mask", "N2")
            numTaxRate.Properties.MaxValue = New Decimal(New Integer() {100, 0, 0, 0})
            numTaxRate.Size = New System.Drawing.Size(120, 20)
            numTaxRate.TabIndex = 19
            ' 
            ' lblComputedTotal
            ' 
            lblComputedTotal.Appearance.Font = New System.Drawing.Font("Segoe UI", 10F, Drawing.FontStyle.Bold)
            lblComputedTotal.Appearance.Options.UseFont = True
            lblComputedTotal.Location = New System.Drawing.Point(15, 240)
            lblComputedTotal.Name = "lblComputedTotal"
            lblComputedTotal.Size = New System.Drawing.Size(123, 17)
            lblComputedTotal.TabIndex = 18
            lblComputedTotal.Text = "Line Total: 0.00 EGP"
            ' 
            ' btnOK
            ' 
            btnOK.Appearance.BackColor = Drawing.Color.FromArgb(CByte(20), CByte(110), CByte(70))
            btnOK.Appearance.Font = New System.Drawing.Font("Segoe UI", 9.5F, Drawing.FontStyle.Bold)
            btnOK.Appearance.ForeColor = Drawing.Color.White
            btnOK.Appearance.Options.UseBackColor = True
            btnOK.Appearance.Options.UseFont = True
            btnOK.Appearance.Options.UseForeColor = True
            btnOK.Location = New System.Drawing.Point(245, 275)
            btnOK.Name = "btnOK"
            btnOK.Size = New System.Drawing.Size(95, 32)
            btnOK.TabIndex = 19
            btnOK.Text = "OK"
            ' 
            ' btnCancel
            ' 
            btnCancel.Location = New System.Drawing.Point(350, 275)
            btnCancel.Name = "btnCancel"
            btnCancel.Size = New System.Drawing.Size(95, 32)
            btnCancel.TabIndex = 20
            btnCancel.Text = "Cancel"
            ' 
            ' LineItemEditorForm
            ' 
            AutoScaleDimensions = New System.Drawing.SizeF(96F, 96F)
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
            ClientSize = New System.Drawing.Size(480, 322)
            Controls.Add(lblDescription)
            Controls.Add(txtDescription)
            Controls.Add(lblItemCode)
            Controls.Add(txtItemCode)
            Controls.Add(lblItemType)
            Controls.Add(cmbItemType)
            Controls.Add(lblUnitType)
            Controls.Add(txtUnitType)
            Controls.Add(lblQuantity)
            Controls.Add(numQuantity)
            Controls.Add(lblUnitPrice)
            Controls.Add(numUnitPrice)
            Controls.Add(lblDiscount)
            Controls.Add(numDiscount)
            Controls.Add(lblTaxType)
            Controls.Add(cmbTaxType)
            Controls.Add(lblTaxSubType)
            Controls.Add(cmbTaxSubType)
            Controls.Add(lblTaxRate)
            Controls.Add(numTaxRate)
            Controls.Add(lblComputedTotal)
            Controls.Add(btnOK)
            Controls.Add(btnCancel)
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            MaximizeBox = False
            MinimizeBox = False
            Name = "LineItemEditorForm"
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Text = "Line Item"
            CType(txtDescription.Properties, ComponentModel.ISupportInitialize).EndInit()
            CType(txtItemCode.Properties, ComponentModel.ISupportInitialize).EndInit()
            CType(cmbItemType.Properties, ComponentModel.ISupportInitialize).EndInit()
            CType(txtUnitType.Properties, ComponentModel.ISupportInitialize).EndInit()
            CType(numQuantity.Properties, ComponentModel.ISupportInitialize).EndInit()
            CType(numUnitPrice.Properties, ComponentModel.ISupportInitialize).EndInit()
            CType(numDiscount.Properties, ComponentModel.ISupportInitialize).EndInit()
            CType(cmbTaxType.Properties, ComponentModel.ISupportInitialize).EndInit()
            CType(cmbTaxSubType.Properties, ComponentModel.ISupportInitialize).EndInit()
            CType(numTaxRate.Properties, ComponentModel.ISupportInitialize).EndInit()
            ResumeLayout(False)
            PerformLayout()
        End Sub

        Friend WithEvents lblDescription As DevExpress.XtraEditors.LabelControl
        Friend WithEvents txtDescription As DevExpress.XtraEditors.TextEdit
        Friend WithEvents lblItemCode As DevExpress.XtraEditors.LabelControl
        Friend WithEvents txtItemCode As DevExpress.XtraEditors.TextEdit
        Friend WithEvents lblItemType As DevExpress.XtraEditors.LabelControl
        Friend WithEvents cmbItemType As DevExpress.XtraEditors.ComboBoxEdit
        Friend WithEvents lblUnitType As DevExpress.XtraEditors.LabelControl
        Friend WithEvents txtUnitType As DevExpress.XtraEditors.TextEdit
        Friend WithEvents lblQuantity As DevExpress.XtraEditors.LabelControl
        Friend WithEvents numQuantity As DevExpress.XtraEditors.SpinEdit
        Friend WithEvents lblUnitPrice As DevExpress.XtraEditors.LabelControl
        Friend WithEvents numUnitPrice As DevExpress.XtraEditors.SpinEdit
        Friend WithEvents lblDiscount As DevExpress.XtraEditors.LabelControl
        Friend WithEvents numDiscount As DevExpress.XtraEditors.SpinEdit
        Friend WithEvents lblTaxType As DevExpress.XtraEditors.LabelControl
        Friend WithEvents cmbTaxType As DevExpress.XtraEditors.ComboBoxEdit
        Friend WithEvents lblTaxSubType As DevExpress.XtraEditors.LabelControl
        Friend WithEvents cmbTaxSubType As DevExpress.XtraEditors.ComboBoxEdit
        Friend WithEvents lblTaxRate As DevExpress.XtraEditors.LabelControl
        Friend WithEvents numTaxRate As DevExpress.XtraEditors.SpinEdit
        Friend WithEvents lblComputedTotal As DevExpress.XtraEditors.LabelControl
        Friend WithEvents btnOK As DevExpress.XtraEditors.SimpleButton
        Friend WithEvents btnCancel As DevExpress.XtraEditors.SimpleButton
    End Class
End Namespace
