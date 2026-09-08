Option Strict On
Imports DevExpress.XtraEditors

Namespace Forms
    Partial Class ReceiverSelectorForm
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
            gridReceivers = New DevExpress.XtraGrid.GridControl()
            gridViewReceivers = New DevExpress.XtraGrid.Views.Grid.GridView()
            btnSelect = New SimpleButton()
            btnAddNew = New SimpleButton()
            btnEdit = New SimpleButton()
            btnCancel = New SimpleButton()
            lblSearch = New LabelControl()
            txtSearch = New TextEdit()
            CType(gridReceivers, ComponentModel.ISupportInitialize).BeginInit()
            CType(gridViewReceivers, ComponentModel.ISupportInitialize).BeginInit()
            CType(txtSearch.Properties, ComponentModel.ISupportInitialize).BeginInit()
            SuspendLayout()

            ' gridReceivers
            gridReceivers.Anchor = System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right
            gridReceivers.Location = New System.Drawing.Point(12, 42)
            gridReceivers.MainView = gridViewReceivers
            gridReceivers.Name = "gridReceivers"
            gridReceivers.Size = New System.Drawing.Size(776, 368)
            gridReceivers.TabIndex = 0
            gridReceivers.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {gridViewReceivers})

            ' gridViewReceivers
            gridViewReceivers.GridControl = gridReceivers
            gridViewReceivers.Name = "gridViewReceivers"
            gridViewReceivers.OptionsBehavior.Editable = False
            gridViewReceivers.OptionsSelection.EnableAppearanceFocusedCell = False
            gridViewReceivers.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
            gridViewReceivers.OptionsView.ShowGroupPanel = False

            ' btnSelect
            btnSelect.Anchor = System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right
            btnSelect.Location = New System.Drawing.Point(632, 416)
            btnSelect.Name = "btnSelect"
            btnSelect.Size = New System.Drawing.Size(75, 30)
            btnSelect.TabIndex = 1
            btnSelect.Text = "Select"
            AddHandler btnSelect.Click, AddressOf btnSelect_Click

            ' btnAddNew
            btnAddNew.Anchor = System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left
            btnAddNew.Location = New System.Drawing.Point(12, 416)
            btnAddNew.Name = "btnAddNew"
            btnAddNew.Size = New System.Drawing.Size(90, 30)
            btnAddNew.TabIndex = 2
            btnAddNew.Text = "Add New"
            AddHandler btnAddNew.Click, AddressOf btnAddNew_Click

            ' btnEdit
            btnEdit.Anchor = System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left
            btnEdit.Location = New System.Drawing.Point(108, 416)
            btnEdit.Name = "btnEdit"
            btnEdit.Size = New System.Drawing.Size(75, 30)
            btnEdit.TabIndex = 3
            btnEdit.Text = "Edit"
            AddHandler btnEdit.Click, AddressOf btnEdit_Click

            ' btnCancel
            btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right
            btnCancel.Location = New System.Drawing.Point(713, 416)
            btnCancel.Name = "btnCancel"
            btnCancel.Size = New System.Drawing.Size(75, 30)
            btnCancel.TabIndex = 4
            btnCancel.Text = "Cancel"
            AddHandler btnCancel.Click, AddressOf btnCancel_Click

            ' lblSearch
            lblSearch.Location = New System.Drawing.Point(12, 14)
            lblSearch.Name = "lblSearch"
            lblSearch.Size = New System.Drawing.Size(38, 13)
            lblSearch.TabIndex = 5
            lblSearch.Text = "Search:"

            ' txtSearch
            txtSearch.Anchor = System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right
            txtSearch.Location = New System.Drawing.Point(56, 11)
            txtSearch.Name = "txtSearch"
            txtSearch.Size = New System.Drawing.Size(732, 20)
            txtSearch.TabIndex = 6
            AddHandler txtSearch.EditValueChanged, AddressOf txtSearch_EditValueChanged

            ' ReceiverSelectorForm
            AutoScaleDimensions = New System.Drawing.SizeF(96F, 96F)
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
            ClientSize = New System.Drawing.Size(800, 458)
            Controls.Add(txtSearch)
            Controls.Add(lblSearch)
            Controls.Add(btnCancel)
            Controls.Add(btnEdit)
            Controls.Add(btnAddNew)
            Controls.Add(btnSelect)
            Controls.Add(gridReceivers)
            Name = "ReceiverSelectorForm"
            Text = "Select Receiver"
            AddHandler Load, AddressOf ReceiverSelectorForm_Load
            AddHandler gridViewReceivers.DoubleClick, AddressOf gridViewReceivers_DoubleClick
            CType(gridReceivers, ComponentModel.ISupportInitialize).EndInit()
            CType(gridViewReceivers, ComponentModel.ISupportInitialize).EndInit()
            CType(txtSearch.Properties, ComponentModel.ISupportInitialize).EndInit()
            ResumeLayout(False)
            PerformLayout()
        End Sub

        Friend WithEvents gridReceivers As DevExpress.XtraGrid.GridControl
        Friend WithEvents gridViewReceivers As DevExpress.XtraGrid.Views.Grid.GridView
        Friend WithEvents btnSelect As SimpleButton
        Friend WithEvents btnAddNew As SimpleButton
        Friend WithEvents btnEdit As SimpleButton
        Friend WithEvents btnCancel As SimpleButton
        Friend WithEvents lblSearch As LabelControl
        Friend WithEvents txtSearch As TextEdit
    End Class
End Namespace
