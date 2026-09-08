Option Strict On
Imports System.IO
Imports System.Linq
Imports System.Windows.Forms
Imports System.ComponentModel
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Base
Imports SphereERP.Models
Imports SphereERP.Services
Imports SphereERP.Data
Imports SphereERP.[Global]

Namespace Forms

    Public Class BatchImportForm

        Private ReadOnly _importService As New ExcelImportService()
        Private ReadOnly _batchRepo As New BatchImportRepository()
        Private _groups As New List(Of ImportedInvoiceGroup)()
        Private _loadedFilePath As String = ""

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub btnDownloadTemplate_Click(sender As Object, e As EventArgs) Handles btnDownloadTemplate.Click
            Using saveDialog As New SaveFileDialog With {
                .Filter = "Excel Workbook (*.xlsx)|*.xlsx",
                .FileName = "ETA_Invoice_Import_Template.xlsx"
            }
                If saveDialog.ShowDialog(Me) <> DialogResult.OK Then Return
                Try
                    _importService.GenerateTemplate(saveDialog.FileName)
                    MessageBox.Show("Template saved to:" & Environment.NewLine & saveDialog.FileName, "Template Saved", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Catch ex As Exception
                    MessageBox.Show("Failed to create template: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End Using
        End Sub

        Private Sub btnChooseFile_Click(sender As Object, e As EventArgs) Handles btnChooseFile.Click
            Using openDialog As New OpenFileDialog With {
                .Filter = "Excel/CSV files (*.xlsx;*.csv)|*.xlsx;*.csv|All files (*.*)|*.*"
            }
                If openDialog.ShowDialog(Me) <> DialogResult.OK Then Return

                Try
                    txtFilePath.Text = openDialog.FileName
                    _loadedFilePath = openDialog.FileName

                    If Path.GetExtension(openDialog.FileName).Equals(".csv", StringComparison.OrdinalIgnoreCase) Then
                        _groups = _importService.ParseCsvFile(openDialog.FileName)
                    Else
                        _groups = _importService.ParseExcelFile(openDialog.FileName)
                    End If

                    BindPreviewGrid()
                    lblStatus.Text = ""
                Catch ex As Exception
                    MessageBox.Show("Failed to read file: " & ex.Message, "Import Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End Using
        End Sub

        Private _detailView As DevExpress.XtraGrid.Views.Grid.GridView

        Private Sub BindPreviewGrid()
            gridViewPreview.OptionsBehavior.AutoPopulateColumns = False
            gridViewPreview.Columns.Clear()
            gridViewPreview.ColumnPanelRowHeight = 50
            gridViewPreview.OptionsView.ShowFooter = True
            gridViewPreview.OptionsView.ShowDetailButtons = True
            AddHandler gridViewPreview.MasterRowEmpty, AddressOf gridViewPreview_MasterRowEmpty
            AddHandler gridViewPreview.MasterRowGetRelationCount, AddressOf gridViewPreview_MasterRowGetRelationCount
            AddHandler gridViewPreview.MasterRowGetRelationName, AddressOf gridViewPreview_MasterRowGetRelationName
            AddHandler gridViewPreview.MasterRowGetChildList, AddressOf gridViewPreview_MasterRowGetChildList

            Dim colInternalId = gridViewPreview.Columns.AddField("InternalId")
            colInternalId.Caption = "Internal ID"
            colInternalId.VisibleIndex = 0
            colInternalId.Summary.Add(DevExpress.Data.SummaryItemType.Count, "InternalId")
            colInternalId.SummaryItem.DisplayFormat = "{0}"
            AlignHeader(colInternalId)

            Dim colType = gridViewPreview.Columns.AddField("DocumentType")
            colType.Caption = "Type"
            colType.VisibleIndex = 1
            AlignHeader(colType)
            AlignCellCenter(colType)

            Dim colReceiver = gridViewPreview.Columns.AddField("ReceiverName")
            colReceiver.Caption = "Receiver"
            colReceiver.VisibleIndex = 2
            AlignHeader(colReceiver)

            Dim colLines = gridViewPreview.Columns.AddField("LineCount")
            colLines.Caption = "Lines"
            colLines.VisibleIndex = 3
            AlignHeader(colLines)
            AlignCellCenter(colLines)

            Dim colNetAmount = gridViewPreview.Columns.AddField("NetAmount")
            colNetAmount.Caption = "Net Amount"
            colNetAmount.VisibleIndex = 4
            colNetAmount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            colNetAmount.DisplayFormat.FormatString = "N2"
            colNetAmount.Summary.Add(DevExpress.Data.SummaryItemType.Sum, "NetAmount")
            colNetAmount.SummaryItem.DisplayFormat = "{0:N2}"
            AlignHeader(colNetAmount)
            AlignCellCenter(colNetAmount)

            Dim colTotalAmount = gridViewPreview.Columns.AddField("TotalAmount")
            colTotalAmount.Caption = "Total Amount"
            colTotalAmount.VisibleIndex = 5
            colTotalAmount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            colTotalAmount.DisplayFormat.FormatString = "N2"
            colTotalAmount.Summary.Add(DevExpress.Data.SummaryItemType.Sum, "TotalAmount")
            colTotalAmount.SummaryItem.DisplayFormat = "{0:N2}"
            AlignHeader(colTotalAmount)
            AlignCellCenter(colTotalAmount)

            Dim colIsValid = gridViewPreview.Columns.AddField("IsValid")
            colIsValid.Caption = "Valid?"
            colIsValid.VisibleIndex = 6
            AlignHeader(colIsValid)

            gridPreview.DataSource = _groups
            gridViewPreview.BestFitColumns()

            SetupDetailView()

            Dim validCount = _groups.Where(Function(g) g.IsValid).Count()
            Dim totalAmount = _groups.Sum(Function(g) g.TotalAmount)
            lblSummary.Text = $"{_groups.Count} invoice(s) found — {validCount} valid, {_groups.Count - validCount} with issues. Total: {totalAmount:N2} EGP"

            btnSubmitAll.Enabled = (validCount > 0)
        End Sub

        Private Sub SetupDetailView()
            If _detailView IsNot Nothing Then
                gridPreview.ViewCollection.Remove(_detailView)
                _detailView.Dispose()
            End If

            _detailView = New DevExpress.XtraGrid.Views.Grid.GridView()
            AddHandler _detailView.CustomColumnDisplayText, AddressOf DetailView_CustomColumnDisplayText

            CType(_detailView, ISupportInitialize).BeginInit()
            _detailView.GridControl = gridPreview
            _detailView.Name = "gridViewDetailLines"
            _detailView.OptionsBehavior.Editable = False
            _detailView.OptionsView.ShowGroupPanel = False
            _detailView.OptionsView.ShowFooter = True
            _detailView.OptionsBehavior.AutoPopulateColumns = False
            gridPreview.ViewCollection.Add(_detailView)
            CType(_detailView, ISupportInitialize).EndInit()

            _detailView.Columns.Clear()
            Dim colDesc = _detailView.Columns.AddField("Description")
            colDesc.Caption = "Description"
            colDesc.VisibleIndex = 0

            Dim colCode = _detailView.Columns.AddField("ItemCode")
            colCode.Caption = "Item Code"
            colCode.VisibleIndex = 1

            Dim colItemType = _detailView.Columns.AddField("ItemType")
            colItemType.Caption = "Item Type"
            colItemType.VisibleIndex = 2

            Dim colUnitType = _detailView.Columns.AddField("UnitType")
            colUnitType.Caption = "Unit Type"
            colUnitType.VisibleIndex = 3

            Dim colQty = _detailView.Columns.AddField("Quantity")
            colQty.Caption = "Quantity"
            colQty.VisibleIndex = 4

            Dim colPrice = _detailView.Columns.AddField("UnitPrice")
            colPrice.Caption = "Unit Price"
            colPrice.VisibleIndex = 5

            Dim colDisc = _detailView.Columns.AddField("DiscountAmount")
            colDisc.Caption = "Discount"
            colDisc.VisibleIndex = 6

            Dim colSales = _detailView.Columns.AddField("SalesTotal")
            colSales.Caption = "Sales Total"
            colSales.VisibleIndex = 7

            Dim colNet = _detailView.Columns.AddField("NetTotal")
            colNet.Caption = "Net Total"
            colNet.VisibleIndex = 8

            Dim colTaxType = _detailView.Columns.AddField("TaxType")
            colTaxType.Caption = "Tax Type"
            colTaxType.VisibleIndex = 9

            Dim colTaxRate = _detailView.Columns.AddField("TaxRate")
            colTaxRate.Caption = "Tax Rate"
            colTaxRate.VisibleIndex = 10

            Dim colTaxAmt = _detailView.Columns.AddField("TaxAmount")
            colTaxAmt.Caption = "Tax Amount"
            colTaxAmt.VisibleIndex = 11

            Dim colTotal = _detailView.Columns.AddField("Total")
            colTotal.Caption = "Total"
            colTotal.VisibleIndex = 12

            FormatLineItemGridView(_detailView)

            For Each col As DevExpress.XtraGrid.Columns.GridColumn In _detailView.Columns
                Select Case col.FieldName
                    Case "DiscountAmount", "SalesTotal", "NetTotal", "TaxAmount", "Total"
                        col.Summary.Add(DevExpress.Data.SummaryItemType.Sum, col.FieldName)
                        col.SummaryItem.DisplayFormat = "{0:N2}"
                End Select
            Next
        End Sub

        Private Sub DetailView_CustomColumnDisplayText(sender As Object, e As DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs)
            If e.Column.FieldName = "TaxType" Then
                Dim line = TryCast(_detailView.GetRow(e.ListSourceRowIndex), ImportedInvoiceLine)
                If line IsNot Nothing Then
                    e.DisplayText = If(String.IsNullOrWhiteSpace(line.TaxType), "T1", line.TaxType)
                End If
            End If
        End Sub

        Private Sub gridViewPreview_MasterRowEmpty(sender As Object, e As DevExpress.XtraGrid.Views.Grid.MasterRowEmptyEventArgs)
            Dim group = TryCast(gridViewPreview.GetRow(e.RowHandle), ImportedInvoiceGroup)
            e.IsEmpty = (group Is Nothing OrElse group.Lines Is Nothing OrElse group.Lines.Count = 0)
        End Sub

        Private Sub gridViewPreview_MasterRowGetRelationCount(sender As Object, e As DevExpress.XtraGrid.Views.Grid.MasterRowGetRelationCountEventArgs)
            e.RelationCount = 1
        End Sub

        Private Sub gridViewPreview_MasterRowGetRelationName(sender As Object, e As DevExpress.XtraGrid.Views.Grid.MasterRowGetRelationNameEventArgs)
            e.RelationName = "Lines"
        End Sub

        Private Sub gridViewPreview_MasterRowGetChildList(sender As Object, e As DevExpress.XtraGrid.Views.Grid.MasterRowGetChildListEventArgs)
            Dim group = TryCast(gridViewPreview.GetRow(e.RowHandle), ImportedInvoiceGroup)
            e.ChildList = group?.Lines
        End Sub

        Private Sub gridViewPreview_DoubleClick(sender As Object, e As EventArgs) Handles gridViewPreview.DoubleClick
            Dim rowHandle = gridViewPreview.FocusedRowHandle
            If rowHandle < 0 OrElse rowHandle >= _groups.Count Then Return
            ShowBreakupDialog(_groups(rowHandle))
        End Sub

        Private Sub gridViewPreview_RowCellStyle(sender As Object, e As RowCellStyleEventArgs) Handles gridViewPreview.RowCellStyle
            Dim group = TryCast(gridViewPreview.GetRow(e.RowHandle), ImportedInvoiceGroup)
            If group IsNot Nothing Then
                If Not group.IsValid Then
                    e.Appearance.ForeColor = System.Drawing.Color.DarkRed
                Else
                    e.Appearance.ForeColor = System.Drawing.Color.DarkGreen
                End If
            End If
        End Sub

        Private Sub gridViewPreview_CustomColumnDisplayText(sender As Object, e As CustomColumnDisplayTextEventArgs) Handles gridViewPreview.CustomColumnDisplayText
            If e.Column.FieldName = "IsValid" Then
                Dim group = TryCast(gridViewPreview.GetRow(e.ListSourceRowIndex), ImportedInvoiceGroup)
                If group IsNot Nothing Then
                    If group.IsValid Then
                        e.DisplayText = "Yes"
                    Else
                        e.DisplayText = $"No ({group.ValidationErrors.Count} issue(s))"
                    End If
                End If
            End If
        End Sub

        Private Shared Sub AlignHeader(col As DevExpress.XtraGrid.Columns.GridColumn)
            col.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            col.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        End Sub

        Private Shared Sub AlignCellCenter(col As DevExpress.XtraGrid.Columns.GridColumn)
            col.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            col.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        End Sub

        Private Shared Sub SetN2Format(col As DevExpress.XtraGrid.Columns.GridColumn)
            col.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            col.DisplayFormat.FormatString = "N2"
        End Sub

        Private Shared Sub FormatLineItemGridView(gv As DevExpress.XtraGrid.Views.Grid.GridView)
            gv.OptionsBehavior.Editable = False
            gv.OptionsView.ShowGroupPanel = False
            gv.OptionsBehavior.AutoPopulateColumns = False
            gv.ColumnPanelRowHeight = 20

            For Each col As DevExpress.XtraGrid.Columns.GridColumn In gv.Columns
                AlignHeader(col)
                If col.FieldName <> "Description" AndAlso col.FieldName <> "ItemCode" Then
                    AlignCellCenter(col)
                End If
                Select Case col.FieldName
                    Case "UnitPrice", "DiscountAmount", "SalesTotal", "NetTotal", "TaxAmount", "Total"
                        SetN2Format(col)
                End Select
            Next

            gv.BestFitColumns()
        End Sub

        Private Sub ShowBreakupDialog(group As ImportedInvoiceGroup)
            Using dlg As New DevExpress.XtraEditors.XtraForm() With {
                .Text = $"Line Item Breakup - {group.InternalId}",
                .Width = 1600,
                .Height = 500,
                .StartPosition = FormStartPosition.CenterParent,
                .FormBorderStyle = FormBorderStyle.Sizable,
                .MaximizeBox = False,
                .MinimizeBox = False
            }
                Dim grid As New DevExpress.XtraGrid.GridControl() With {
                    .Dock = DockStyle.Fill,
                    .DataSource = group.Lines
                }
                Dim gridView As New DevExpress.XtraGrid.Views.Grid.GridView()
                CType(grid, System.ComponentModel.ISupportInitialize).BeginInit()
                CType(gridView, System.ComponentModel.ISupportInitialize).BeginInit()

                grid.MainView = gridView
                grid.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {gridView})

                gridView.GridControl = grid
                gridView.Name = "gridViewBreakup"
                gridView.OptionsBehavior.Editable = False
                gridView.OptionsView.ShowGroupPanel = False
                gridView.OptionsBehavior.AutoPopulateColumns = False

                Dim colDesc = gridView.Columns.AddField("Description")
                colDesc.Caption = "Description"
                colDesc.VisibleIndex = 0

                Dim colCode = gridView.Columns.AddField("ItemCode")
                colCode.Caption = "Item Code"
                colCode.VisibleIndex = 1

                Dim colItemType = gridView.Columns.AddField("ItemType")
                colItemType.Caption = "Item Type"
                colItemType.VisibleIndex = 2

                Dim colUnitType = gridView.Columns.AddField("UnitType")
                colUnitType.Caption = "Unit Type"
                colUnitType.VisibleIndex = 3

                Dim colQty = gridView.Columns.AddField("Quantity")
                colQty.Caption = "Quantity"
                colQty.VisibleIndex = 4

                Dim colPrice = gridView.Columns.AddField("UnitPrice")
                colPrice.Caption = "Unit Price"
                colPrice.VisibleIndex = 5

                Dim colDiscount = gridView.Columns.AddField("DiscountAmount")
                colDiscount.Caption = "Discount"
                colDiscount.VisibleIndex = 6

                Dim colTaxType = gridView.Columns.AddField("TaxType")
                colTaxType.Caption = "Tax Type"
                colTaxType.VisibleIndex = 7

                Dim colTaxRate = gridView.Columns.AddField("TaxRate")
                colTaxRate.Caption = "Tax Rate"
                colTaxRate.VisibleIndex = 8

                Dim colSales = gridView.Columns.AddField("SalesTotal")
                colSales.Caption = "Sales Total"
                colSales.VisibleIndex = 9

                Dim colNet = gridView.Columns.AddField("NetTotal")
                colNet.Caption = "Net Total"
                colNet.VisibleIndex = 10

                Dim colTaxAmt = gridView.Columns.AddField("TaxAmount")
                colTaxAmt.Caption = "Tax Amount"
                colTaxAmt.VisibleIndex = 11

                Dim colTotal = gridView.Columns.AddField("Total")
                colTotal.Caption = "Total"
                colTotal.VisibleIndex = 12

                FormatLineItemGridView(gridView)
                gridView.OptionsView.ShowFooter = True
                For Each col As DevExpress.XtraGrid.Columns.GridColumn In gridView.Columns
                    Select Case col.FieldName
                        Case "DiscountAmount", "SalesTotal", "NetTotal", "TaxAmount", "Total"
                            col.Summary.Add(DevExpress.Data.SummaryItemType.Sum, col.FieldName)
                            col.SummaryItem.DisplayFormat = "{0:N2}"
                    End Select
                Next

                CType(gridView, System.ComponentModel.ISupportInitialize).EndInit()
                CType(grid, System.ComponentModel.ISupportInitialize).EndInit()

                Dim pnlBottom As New DevExpress.XtraEditors.PanelControl() With {
                    .Dock = DockStyle.Bottom,
                    .Height = 100,
                    .BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
                }
                Dim lblIssues As New DevExpress.XtraEditors.LabelControl()
                lblIssues.Dock = DockStyle.Fill
                lblIssues.AutoSize = False
                lblIssues.Appearance.ForeColor = System.Drawing.Color.DarkRed
                lblIssues.Padding = New System.Windows.Forms.Padding(8)
                If group.ValidationErrors.Count > 0 Then
                    lblIssues.Text = "Issues:" & Environment.NewLine & String.Join(Environment.NewLine, group.ValidationErrors)
                Else
                    lblIssues.Text = "No validation issues."
                    lblIssues.Appearance.ForeColor = System.Drawing.Color.DarkGreen
                End If
                pnlBottom.Controls.Add(lblIssues)

                dlg.Controls.Add(grid)
                dlg.Controls.Add(pnlBottom)
                dlg.ShowDialog(Me)
            End Using
        End Sub

        Private Async Sub btnSubmitAll_Click(sender As Object, e As EventArgs) Handles btnSubmitAll.Click
            Dim validGroups = _groups.Where(Function(g) g.IsValid).ToList()
            If validGroups.Count = 0 Then Return

            Dim confirmMsg = $"This will sign and submit {validGroups.Count} document(s) to the {AppState.CurrentSettings.Environment} environment. Continue?"
            If MessageBox.Show(confirmMsg, "Confirm Bulk Submission", MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> DialogResult.Yes Then
                Return
            End If

            btnSubmitAll.Enabled = False
            btnChooseFile.Enabled = False
            progressBar.Properties.Maximum = validGroups.Count
            progressBar.Position = 0

            Dim settings = AppState.CurrentSettings
            Dim issuer As New PartyModel With {.Type = "B", .Id = settings.TaxpayerRIN, .Name = settings.TaxpayerName}
            issuer.Address.BranchID = If(String.IsNullOrWhiteSpace(settings.DefaultBranchId), "0", settings.DefaultBranchId)
            issuer.Address.Governate = settings.IssuerGovernate
            issuer.Address.RegionCity = settings.IssuerRegionCity
            issuer.Address.Street = settings.IssuerStreet
            issuer.Address.BuildingNumber = settings.IssuerBuildingNumber

            Dim batchId = _batchRepo.InsertBatch(Path.GetFileName(_loadedFilePath), settings.Environment.ToString())
            Dim succeeded = 0
            Dim failed = 0

            For Each group In validGroups
                lblStatus.ForeColor = System.Drawing.Color.DarkSlateGray
                lblStatus.Text = $"Submitting {group.InternalId}... (USB token PIN prompt may appear)"

                Dim rawLinesJson = Newtonsoft.Json.JsonConvert.SerializeObject(group.Lines)
                Dim itemId = _batchRepo.InsertItem(batchId, group.InternalId, group.LineCount, group.TotalAmount, "Pending", rawLinesJson)

                Try
                    Dim doc = _importService.ConvertToDocument(group, issuer, settings.DefaultActivityCode)
                    Dim result = Await AppState.SubmissionService.SubmitSingleAsync(doc).ConfigureAwait(True)

                    If result.Success Then
                        _batchRepo.UpdateItemResult(itemId, result.LocalDocumentId, "Submitted", Nothing)
                        succeeded += 1
                    Else
                        Dim errMsg = If(result.ValidationErrors.Count > 0, String.Join("; ", result.ValidationErrors), result.ErrorMessage)
                        _batchRepo.UpdateItemResult(itemId, Nothing, "Failed", errMsg)
                        failed += 1
                    End If
                Catch ex As Exception
                    _batchRepo.UpdateItemResult(itemId, Nothing, "Failed", ex.Message)
                    failed += 1
                End Try

                progressBar.Position = Math.Min(progressBar.Properties.Maximum, progressBar.Position + 1)
            Next

            _batchRepo.UpdateBatchSummary(batchId, validGroups.Count, succeeded, failed)

            lblStatus.ForeColor = If(failed = 0, System.Drawing.Color.DarkGreen, System.Drawing.Color.DarkOrange)
            lblStatus.Text = $"Batch complete: {succeeded} succeeded, {failed} failed out of {validGroups.Count}."

            btnSubmitAll.Enabled = True
            btnChooseFile.Enabled = True
        End Sub

    End Class

End Namespace
