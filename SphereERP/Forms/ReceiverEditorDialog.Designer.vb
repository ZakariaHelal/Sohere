Option Strict On
Imports DevExpress.XtraEditors

Namespace Forms
    Partial Class ReceiverEditorDialog
        Inherits XtraForm

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

        Private components As ComponentModel.IContainer

        <System.Diagnostics.DebuggerStepThrough()>
        Private Sub InitializeComponent()
            lblRin = New LabelControl()
            txtRin = New TextEdit()
            lblName = New LabelControl()
            txtName = New TextEdit()
            lblCountry = New LabelControl()
            cmbCountry = New ComboBoxEdit()
            lblGovernate = New LabelControl()
            cmbGovernate = New ComboBoxEdit()
            lblCity = New LabelControl()
            txtCity = New TextEdit()
            lblStreet = New LabelControl()
            txtStreet = New TextEdit()
            lblBuilding = New LabelControl()
            txtBuilding = New TextEdit()
            btnSave = New SimpleButton()
            btnCancel = New SimpleButton()
            CType(txtRin.Properties, ComponentModel.ISupportInitialize).BeginInit()
            CType(txtName.Properties, ComponentModel.ISupportInitialize).BeginInit()
            CType(cmbCountry.Properties, ComponentModel.ISupportInitialize).BeginInit()
            CType(cmbGovernate.Properties, ComponentModel.ISupportInitialize).BeginInit()
            CType(txtCity.Properties, ComponentModel.ISupportInitialize).BeginInit()
            CType(txtStreet.Properties, ComponentModel.ISupportInitialize).BeginInit()
            CType(txtBuilding.Properties, ComponentModel.ISupportInitialize).BeginInit()
            SuspendLayout()
            ' 
            ' lblRin
            ' 
            lblRin.Location = New System.Drawing.Point(12, 15)
            lblRin.Name = "lblRin"
            lblRin.Size = New System.Drawing.Size(85, 13)
            lblRin.TabIndex = 0
            lblRin.Text = "RIN / National ID:"
            ' 
            ' txtRin
            ' 
            txtRin.Location = New System.Drawing.Point(12, 34)
            txtRin.Name = "txtRin"
            txtRin.Size = New System.Drawing.Size(360, 20)
            txtRin.TabIndex = 1
            ' 
            ' lblName
            ' 
            lblName.Location = New System.Drawing.Point(12, 65)
            lblName.Name = "lblName"
            lblName.Size = New System.Drawing.Size(31, 13)
            lblName.TabIndex = 2
            lblName.Text = "Name:"
            ' 
            ' txtName
            ' 
            txtName.Location = New System.Drawing.Point(12, 84)
            txtName.Name = "txtName"
            txtName.Size = New System.Drawing.Size(360, 20)
            txtName.TabIndex = 3
            ' 
            ' lblCountry
            ' 
            lblCountry.Location = New System.Drawing.Point(12, 115)
            lblCountry.Name = "lblCountry"
            lblCountry.Size = New System.Drawing.Size(43, 13)
            lblCountry.TabIndex = 4
            lblCountry.Text = "Country:"
            ' 
            ' cmbCountry
            ' 
            cmbCountry.Location = New System.Drawing.Point(12, 134)
            cmbCountry.Name = "cmbCountry"
            cmbCountry.Properties.Buttons.AddRange(New Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
            cmbCountry.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
            cmbCountry.Size = New System.Drawing.Size(360, 20)
            cmbCountry.TabIndex = 5
            ' 
            ' lblGovernate
            ' 
            lblGovernate.Location = New System.Drawing.Point(12, 165)
            lblGovernate.Name = "lblGovernate"
            lblGovernate.Size = New System.Drawing.Size(55, 13)
            lblGovernate.TabIndex = 6
            lblGovernate.Text = "Governate:"
            ' 
            ' cmbGovernate
            ' 
            cmbGovernate.Location = New System.Drawing.Point(12, 184)
            cmbGovernate.Name = "cmbGovernate"
            cmbGovernate.Properties.Buttons.AddRange(New Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
            cmbGovernate.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
            cmbGovernate.Size = New System.Drawing.Size(360, 20)
            cmbGovernate.TabIndex = 7
            ' 
            ' lblCity
            ' 
            lblCity.Location = New System.Drawing.Point(12, 215)
            lblCity.Name = "lblCity"
            lblCity.Size = New System.Drawing.Size(66, 13)
            lblCity.TabIndex = 8
            lblCity.Text = "Region / City:"
            ' 
            ' txtCity
            ' 
            txtCity.Location = New System.Drawing.Point(12, 234)
            txtCity.Name = "txtCity"
            txtCity.Size = New System.Drawing.Size(360, 20)
            txtCity.TabIndex = 9
            ' 
            ' lblStreet
            ' 
            lblStreet.Location = New System.Drawing.Point(12, 265)
            lblStreet.Name = "lblStreet"
            lblStreet.Size = New System.Drawing.Size(34, 13)
            lblStreet.TabIndex = 10
            lblStreet.Text = "Street:"
            ' 
            ' txtStreet
            ' 
            txtStreet.Location = New System.Drawing.Point(12, 284)
            txtStreet.Name = "txtStreet"
            txtStreet.Size = New System.Drawing.Size(360, 20)
            txtStreet.TabIndex = 11
            ' 
            ' lblBuilding
            ' 
            lblBuilding.Location = New System.Drawing.Point(12, 319)
            lblBuilding.Name = "lblBuilding"
            lblBuilding.Size = New System.Drawing.Size(80, 13)
            lblBuilding.TabIndex = 12
            lblBuilding.Text = "Building Number:"
            ' 
            ' txtBuilding
            ' 
            txtBuilding.Location = New System.Drawing.Point(12, 341)
            txtBuilding.Name = "txtBuilding"
            txtBuilding.Size = New System.Drawing.Size(360, 20)
            txtBuilding.TabIndex = 13
            ' 
            ' btnSave
            ' 
            btnSave.Location = New System.Drawing.Point(216, 377)
            btnSave.Name = "btnSave"
            btnSave.Size = New System.Drawing.Size(75, 30)
            btnSave.TabIndex = 14
            btnSave.Text = "Save"
            ' 
            ' btnCancel
            ' 
            btnCancel.Location = New System.Drawing.Point(297, 377)
            btnCancel.Name = "btnCancel"
            btnCancel.Size = New System.Drawing.Size(75, 30)
            btnCancel.TabIndex = 15
            btnCancel.Text = "Cancel"
            ' 
            ' ReceiverEditorDialog
            ' 
            AutoScaleDimensions = New System.Drawing.SizeF(96F, 96F)
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
            ClientSize = New System.Drawing.Size(384, 428)
            Controls.Add(btnCancel)
            Controls.Add(btnSave)
            Controls.Add(txtBuilding)
            Controls.Add(lblBuilding)
            Controls.Add(txtStreet)
            Controls.Add(lblStreet)
            Controls.Add(txtCity)
            Controls.Add(lblCity)
            Controls.Add(cmbGovernate)
            Controls.Add(lblGovernate)
            Controls.Add(cmbCountry)
            Controls.Add(lblCountry)
            Controls.Add(txtName)
            Controls.Add(lblName)
            Controls.Add(txtRin)
            Controls.Add(lblRin)
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            IconOptions.ShowIcon = False
            MaximizeBox = False
            MinimizeBox = False
            Name = "ReceiverEditorDialog"
            ShowInTaskbar = False
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Text = "Receiver Details"
            CType(txtRin.Properties, ComponentModel.ISupportInitialize).EndInit()
            CType(txtName.Properties, ComponentModel.ISupportInitialize).EndInit()
            CType(cmbCountry.Properties, ComponentModel.ISupportInitialize).EndInit()
            CType(cmbGovernate.Properties, ComponentModel.ISupportInitialize).EndInit()
            CType(txtCity.Properties, ComponentModel.ISupportInitialize).EndInit()
            CType(txtStreet.Properties, ComponentModel.ISupportInitialize).EndInit()
            CType(txtBuilding.Properties, ComponentModel.ISupportInitialize).EndInit()
            ResumeLayout(False)
            PerformLayout()
        End Sub

        Friend WithEvents lblRin As LabelControl
        Friend WithEvents txtRin As TextEdit
        Friend WithEvents lblName As LabelControl
        Friend WithEvents txtName As TextEdit
        Friend WithEvents lblCountry As LabelControl
        Friend WithEvents cmbCountry As ComboBoxEdit
        Friend WithEvents lblGovernate As LabelControl
        Friend WithEvents cmbGovernate As ComboBoxEdit
        Friend WithEvents lblCity As LabelControl
        Friend WithEvents txtCity As TextEdit
        Friend WithEvents lblStreet As LabelControl
        Friend WithEvents txtStreet As TextEdit
        Friend WithEvents lblBuilding As LabelControl
        Friend WithEvents txtBuilding As TextEdit
        Friend WithEvents btnSave As SimpleButton
        Friend WithEvents btnCancel As SimpleButton
    End Class
End Namespace
