Option Strict On
Imports System.Windows.Forms
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.Utils
Imports SphereERP.Data
Imports SphereERP.Models

Namespace Forms

    Public Class ReceiverSelectorForm

        Private ReadOnly _repo As New ReceiverRepository()
        Private _allReceivers As List(Of ReceiverModel) = New List(Of ReceiverModel)()

        Public ReadOnly Property SelectedReceiver As ReceiverModel
            Get
                Return TryCast(gridViewReceivers.GetFocusedRow(), ReceiverModel)
            End Get
        End Property

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub ReceiverSelectorForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            LoadData()
        End Sub

        Private Sub LoadData()
            _allReceivers = _repo.GetAll()
            BindGrid(_allReceivers)
        End Sub

        Private Sub BindGrid(list As List(Of ReceiverModel))
            gridViewReceivers.Columns.Clear()
            gridViewReceivers.ColumnPanelRowHeight = 50

            Dim col1 = gridViewReceivers.Columns.AddField("Rin")
            col1.VisibleIndex = 0
            col1.Caption = "RIN"
            col1.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center
            col1.AppearanceHeader.TextOptions.VAlignment = VertAlignment.Center

            Dim col2 = gridViewReceivers.Columns.AddField("Name")
            col2.VisibleIndex = 1
            col2.Caption = "Name"
            col2.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center
            col2.AppearanceHeader.TextOptions.VAlignment = VertAlignment.Center

            Dim col3 = gridViewReceivers.Columns.AddField("Country")
            col3.VisibleIndex = 2
            col3.Caption = "Country"
            col3.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center
            col3.AppearanceHeader.TextOptions.VAlignment = VertAlignment.Center

            Dim col4 = gridViewReceivers.Columns.AddField("Governate")
            col4.VisibleIndex = 3
            col4.Caption = "Governate"
            col4.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center
            col4.AppearanceHeader.TextOptions.VAlignment = VertAlignment.Center

            Dim col5 = gridViewReceivers.Columns.AddField("RegionCity")
            col5.VisibleIndex = 4
            col5.Caption = "Region / City"
            col5.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center
            col5.AppearanceHeader.TextOptions.VAlignment = VertAlignment.Center

            Dim col6 = gridViewReceivers.Columns.AddField("Street")
            col6.VisibleIndex = 5
            col6.Caption = "Street"
            col6.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center
            col6.AppearanceHeader.TextOptions.VAlignment = VertAlignment.Center

            Dim col7 = gridViewReceivers.Columns.AddField("BuildingNumber")
            col7.VisibleIndex = 6
            col7.Caption = "Building"
            col7.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center
            col7.AppearanceHeader.TextOptions.VAlignment = VertAlignment.Center
            col7.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center

            gridReceivers.DataSource = list
            if list.Count > 0 Then gridViewReceivers.MoveFirst()
        End Sub

        Private Sub DoSelect()
            If SelectedReceiver IsNot Nothing Then
                Me.DialogResult = DialogResult.OK
                Me.Close()
            Else
                XtraMessageBox.Show("Please select a receiver from the list.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End Sub

        Private Sub btnSelect_Click(sender As Object, e As EventArgs)
            DoSelect()
        End Sub

        Private Sub gridViewReceivers_DoubleClick(sender As Object, e As EventArgs)
            DoSelect()
        End Sub

        Private Sub btnAddNew_Click(sender As Object, e As EventArgs)
            Using dlg As New ReceiverEditorDialog()
                If dlg.ShowDialog(Me) = DialogResult.OK Then
                    LoadData()
                End If
            End Using
        End Sub

        Private Sub btnEdit_Click(sender As Object, e As EventArgs)
            Dim rec = SelectedReceiver
            If rec Is Nothing Then
                XtraMessageBox.Show("Please select a receiver to edit.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If
            Using dlg As New ReceiverEditorDialog(rec.Id)
                If dlg.ShowDialog(Me) = DialogResult.OK Then
                    LoadData()
                End If
            End Using
        End Sub

        Private Sub btnCancel_Click(sender As Object, e As EventArgs)
            Me.DialogResult = DialogResult.Cancel
            Me.Close()
        End Sub

        Private Sub txtSearch_EditValueChanged(sender As Object, e As EventArgs)
            Dim text = txtSearch.Text.Trim()
            If String.IsNullOrWhiteSpace(text) Then
                BindGrid(_allReceivers)
            Else
                BindGrid(_repo.Search(text))
            End If
        End Sub

    End Class

End Namespace
