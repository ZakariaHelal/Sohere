Option Strict On
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Views.Grid
Imports System.Windows.Forms
Imports SphereERP.Models
Imports SphereERP.Data
Imports SphereERP.[Global]
Imports OfficeOpenXml

Namespace Forms

    Public Class EgsCodeLibraryForm

        Private ReadOnly _repo As New EgsCodeRepository()
        Private _items As New List(Of EgsCodeModel)()
        Private _selectedOriginalCode As String = ""

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub EgsCodeLibraryForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            cmbDefaultTaxType.Properties.Items.Clear()
            cmbDefaultTaxType.Properties.Items.AddRange({"T1", "T2", "T3", "T4", "T5", "T6", "T7", "T8", "T9", "T10", "T11", "T12", "T13", "T14", "T15", "T16", "T17", "T18", "T19", "T20"})
            cmbDefaultTaxType.SelectedItem = "T1"
            numDefaultTaxRate.Value = 14

            LoadAll()
        End Sub

        Private Sub LoadAll()
            _items = _repo.GetAll(activeOnly:=False)
            BindGrid()
        End Sub

        Private Sub BindGrid()
            gridViewCodes.Columns.Clear()

            Dim col1 = gridViewCodes.Columns.AddField("EgsCode")
            col1.Caption = "EGS Code"
            col1.VisibleIndex = 0

            Dim col2 = gridViewCodes.Columns.AddField("Description")
            col2.Caption = "Description"
            col2.VisibleIndex = 1

            Dim col3 = gridViewCodes.Columns.AddField("UnitType")
            col3.Caption = "Unit Type"
            col3.VisibleIndex = 2

            Dim col4 = gridViewCodes.Columns.AddField("DefaultPrice")
            col4.Caption = "Default Price"
            col4.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            col4.DisplayFormat.FormatString = "N2"
            col4.VisibleIndex = 3

            Dim col5 = gridViewCodes.Columns.AddField("DefaultTaxType")
            col5.Caption = "Tax Type"
            col5.VisibleIndex = 4

            Dim col6 = gridViewCodes.Columns.AddField("DefaultTaxRate")
            col6.Caption = "Tax Rate"
            col6.VisibleIndex = 5

            Dim col7 = gridViewCodes.Columns.AddField("Category")
            col7.Caption = "Category"
            col7.VisibleIndex = 6

            Dim col8 = gridViewCodes.Columns.AddField("IsActive")
            col8.Caption = "Active"
            col8.VisibleIndex = 7
            Dim riCheck As New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
            col8.ColumnEdit = riCheck

            gridCodes.DataSource = _items
            gridViewCodes.BestFitColumns()

            lblStatus.Appearance.ForeColor = System.Drawing.Color.DimGray
            lblStatus.Text = $"{_items.Count} item(s) in library."
        End Sub

        Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
            LoadAll()
        End Sub

        Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
            If String.IsNullOrWhiteSpace(txtSearch.Text) Then
                LoadAll()
                Return
            End If
            _items = _repo.Search(txtSearch.Text.Trim())
            BindGrid()
        End Sub

        Private Sub gridViewCodes_FocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles gridViewCodes.FocusedRowChanged
            If e.FocusedRowHandle < 0 Then Return
            Dim item = TryCast(gridViewCodes.GetFocusedRow(), EgsCodeModel)
            If item Is Nothing Then Return

            _selectedOriginalCode = item.EgsCode
            txtEgsCode.Text = item.EgsCode
            txtDescription.Text = item.Description
            txtUnitType.Text = item.UnitType
            numDefaultPrice.Value = Math.Max(0, item.DefaultPrice)
            cmbDefaultTaxType.SelectedItem = If(cmbDefaultTaxType.Properties.Items.Contains(item.DefaultTaxType), item.DefaultTaxType, "T1")
            numDefaultTaxRate.Value = Math.Max(0, Math.Min(100, item.DefaultTaxRate))
            txtCategory.Text = item.Category
            chkIsActive.Checked = item.IsActive
        End Sub

        Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
            ClearEditor()
        End Sub

        Private Sub ClearEditor()
            _selectedOriginalCode = ""
            txtEgsCode.Text = ""
            txtDescription.Text = ""
            txtUnitType.Text = "EA"
            numDefaultPrice.Value = 0
            cmbDefaultTaxType.SelectedItem = "T1"
            numDefaultTaxRate.Value = 14
            txtCategory.Text = ""
            chkIsActive.Checked = True
        End Sub

        Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
            If String.IsNullOrWhiteSpace(txtEgsCode.Text) Then
                MessageBox.Show("EGS Code is required.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If
            If String.IsNullOrWhiteSpace(txtDescription.Text) Then
                MessageBox.Show("Description is required.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Try
                Dim item As New EgsCodeModel With {
                    .EgsCode = txtEgsCode.Text.Trim(),
                    .Description = txtDescription.Text.Trim(),
                    .UnitType = If(String.IsNullOrWhiteSpace(txtUnitType.Text), "EA", txtUnitType.Text.Trim()),
                    .DefaultPrice = numDefaultPrice.Value,
                    .DefaultTaxType = Convert.ToString(cmbDefaultTaxType.SelectedItem),
                    .DefaultTaxRate = numDefaultTaxRate.Value,
                    .Category = txtCategory.Text.Trim(),
                    .IsActive = chkIsActive.Checked,
                    .CreatedAtUtc = DateTime.UtcNow
                }

                If Not String.Equals(_selectedOriginalCode, item.EgsCode, StringComparison.OrdinalIgnoreCase) Then
                    Dim existing = _repo.GetAll().FirstOrDefault(Function(x) String.Equals(x.EgsCode, item.EgsCode, StringComparison.OrdinalIgnoreCase))
                    If existing IsNot Nothing Then
                        MessageBox.Show($"EGS code '{item.EgsCode}' already exists in the library.", "Duplicate Code", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Return
                    End If
                End If

                If Not String.IsNullOrWhiteSpace(_selectedOriginalCode) AndAlso Not String.Equals(_selectedOriginalCode, item.EgsCode, StringComparison.OrdinalIgnoreCase) Then
                    _repo.Delete(_selectedOriginalCode)
                End If

                _repo.Upsert(item)
                lblStatus.Appearance.ForeColor = System.Drawing.Color.DarkGreen
                lblStatus.Text = $"Saved item '{item.EgsCode}'."
                ClearEditor()
                LoadAll()
            Catch ex As Exception
                lblStatus.Appearance.ForeColor = System.Drawing.Color.DarkRed
                lblStatus.Text = "Failed to save: " & ex.Message
            End Try
        End Sub

        Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
            If String.IsNullOrWhiteSpace(_selectedOriginalCode) Then
                MessageBox.Show("Select an item from the grid first.", "Delete Item", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            If MessageBox.Show($"Delete EGS code '{_selectedOriginalCode}' from your library?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) <> DialogResult.Yes Then
                Return
            End If

            Try
                _repo.Delete(_selectedOriginalCode)
                lblStatus.Appearance.ForeColor = System.Drawing.Color.DarkGreen
                lblStatus.Text = $"Deleted item '{_selectedOriginalCode}'."
                ClearEditor()
                LoadAll()
            Catch ex As Exception
                lblStatus.Appearance.ForeColor = System.Drawing.Color.DarkRed
                lblStatus.Text = "Failed to delete: " & ex.Message
            End Try
        End Sub

        Private Sub btnDownloadTemplate_Click(sender As Object, e As EventArgs) Handles btnDownloadTemplate.Click
            Using saveDialog As New SaveFileDialog With {
                .Filter = "Excel Workbook (*.xlsx)|*.xlsx",
                .FileName = "EGS_Codes_Template.xlsx"
            }
                If saveDialog.ShowDialog(Me) <> DialogResult.OK Then Return
                Try
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial
                    Using package As New ExcelPackage()
                        Dim sheet = package.Workbook.Worksheets.Add("EGS Codes")
                        sheet.Cells(1, 1).Value = "EGS Code"
                        sheet.Cells(1, 2).Value = "Description"
                        sheet.Cells(1, 3).Value = "Unit Type"
                        sheet.Cells(1, 4).Value = "Default Price"
                        sheet.Cells(1, 5).Value = "Default Tax Type"
                        sheet.Cells(1, 6).Value = "Default Tax Rate"
                        sheet.Cells(1, 7).Value = "Category"
                        Using rng = sheet.Cells(1, 1, 1, 7)
                            rng.Style.Font.Bold = True
                        End Using
                        sheet.Cells(2, 1).Value = "EGS-001"
                        sheet.Cells(2, 2).Value = "Example item"
                        sheet.Cells(2, 3).Value = "EA"
                        sheet.Cells(2, 4).Value = 100
                        sheet.Cells(2, 5).Value = "T1"
                        sheet.Cells(2, 6).Value = 14
                        sheet.Cells(2, 7).Value = "General"
                        sheet.Cells.AutoFitColumns()
                        package.SaveAs(New System.IO.FileInfo(saveDialog.FileName))
                    End Using
                    lblStatus.Appearance.ForeColor = System.Drawing.Color.DarkGreen
                    lblStatus.Text = "Template saved to " & saveDialog.FileName
                Catch ex As Exception
                    lblStatus.Appearance.ForeColor = System.Drawing.Color.DarkRed
                    lblStatus.Text = "Failed to save template: " & ex.Message
                End Try
            End Using
        End Sub

        Private Sub btnImportFromExcel_Click(sender As Object, e As EventArgs) Handles btnImportFromExcel.Click
            Using openDialog As New OpenFileDialog With {
                .Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*",
                .Title = "Import EGS Codes from Excel"
            }
                If openDialog.ShowDialog(Me) <> DialogResult.OK Then Return

                Dim filePath = openDialog.FileName
                lblStatus.Appearance.ForeColor = System.Drawing.Color.DarkSlateGray
                lblStatus.Text = "Reading file..."

                Try
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial
                    Dim importedCount = 0
                    Dim skipCount = 0
                    Dim errorCount = 0
                    Dim existingCodes = New HashSet(Of String)(_repo.GetAll().Select(Function(x) x.EgsCode), StringComparer.OrdinalIgnoreCase)

                    Using package As New ExcelPackage(New System.IO.FileInfo(filePath))
                        Dim sheet = package.Workbook.Worksheets(0)
                        If sheet.Dimension Is Nothing Then
                            lblStatus.Appearance.ForeColor = System.Drawing.Color.DarkOrange
                            lblStatus.Text = "The selected file is empty."
                            Return
                        End If

                        Dim colCount = sheet.Dimension.End.Column
                        Dim rowCount = sheet.Dimension.End.Row
                        If rowCount < 2 Then
                            lblStatus.Appearance.ForeColor = System.Drawing.Color.DarkOrange
                            lblStatus.Text = "No data rows found (header only)."
                            Return
                        End If

                        ' Build column mapping from header row
                        Dim colIndex As New Dictionary(Of String, Integer)(StringComparer.OrdinalIgnoreCase)
                        For c = 1 To colCount
                            Dim header = Convert.ToString(sheet.Cells(1, c).Value)
                            If Not String.IsNullOrWhiteSpace(header) Then
                                colIndex(header.Trim()) = c
                            End If
                        Next

                        For r = 2 To rowCount
                            Try
                                Dim egsCode = Convert.ToString(sheet.Cells(r, GetCol(colIndex, "EGS Code", 1)).Value)
                                If String.IsNullOrWhiteSpace(egsCode) Then Continue For
                                Dim code = egsCode.Trim()
                                If existingCodes.Contains(code) Then
                                    skipCount += 1
                                    Continue For
                                End If

                                Dim item As New EgsCodeModel()
                                item.EgsCode = code
                                item.Description = Convert.ToString(sheet.Cells(r, GetCol(colIndex, "Description", 2)).Value)
                                item.UnitType = Convert.ToString(sheet.Cells(r, GetCol(colIndex, "Unit Type", 3)).Value)
                                If String.IsNullOrWhiteSpace(item.UnitType) Then item.UnitType = "EA"

                                Dim priceVal = sheet.Cells(r, GetCol(colIndex, "Default Price", 4)).Value
                                If priceVal IsNot Nothing Then
                                    Decimal.TryParse(Convert.ToString(priceVal), item.DefaultPrice)
                                End If

                                Dim taxType = Convert.ToString(sheet.Cells(r, GetCol(colIndex, "Default Tax Type", 5)).Value)
                                If Not String.IsNullOrWhiteSpace(taxType) Then item.DefaultTaxType = taxType.Trim()

                                Dim rateVal = sheet.Cells(r, GetCol(colIndex, "Default Tax Rate", 6)).Value
                                If rateVal IsNot Nothing Then
                                    Decimal.TryParse(Convert.ToString(rateVal), item.DefaultTaxRate)
                                End If

                                item.Category = Convert.ToString(sheet.Cells(r, GetCol(colIndex, "Category", 7)).Value)
                                item.IsActive = True

                                _repo.Upsert(item)
                                importedCount += 1
                            Catch
                                errorCount += 1
                            End Try
                        Next
                    End Using

                    If importedCount > 0 Then
                        Dim parts As New List(Of String) From {$"Imported {importedCount} code(s)"}
                        If skipCount > 0 Then parts.Add($"{skipCount} duplicate(s) skipped")
                        If errorCount > 0 Then parts.Add($"{errorCount} row(s) had errors")
                        lblStatus.Appearance.ForeColor = System.Drawing.Color.DarkGreen
                        lblStatus.Text = String.Join(" — ", parts) & "."
                        LoadAll()
                    Else
                        lblStatus.Appearance.ForeColor = System.Drawing.Color.DarkOrange
                        lblStatus.Text = "No new codes imported." &
                            If(skipCount > 0, $" {skipCount} duplicate(s) skipped.", "") &
                            If(errorCount > 0, $" {errorCount} row(s) had errors.", "")
                    End If

                Catch ex As Exception
                    lblStatus.Appearance.ForeColor = System.Drawing.Color.DarkRed
                    lblStatus.Text = "Failed to import: " & ex.Message
                End Try
            End Using
        End Sub

        Private Shared Function GetCol(mapping As Dictionary(Of String, Integer), key As String, fallback As Integer) As Integer
            If mapping.ContainsKey(key) Then Return mapping(key)
            Return fallback
        End Function

        Private Async Sub btnImportFromPortal_Click(sender As Object, e As EventArgs) Handles btnImportFromPortal.Click
            btnImportFromPortal.Enabled = False
            lblStatus.Appearance.ForeColor = System.Drawing.Color.DarkSlateGray
            lblStatus.Text = "Contacting ETA portal for registered item codes..."

            Try
                If Not AppState.ApiClient.HasValidToken Then
                    Await AppState.ApiClient.LoginAsync().ConfigureAwait(True)
                End If

                Dim portalCodes = Await AppState.ApiClient.GetRegisteredItemCodesAsync().ConfigureAwait(True)

                If portalCodes Is Nothing OrElse portalCodes.Count = 0 Then
                    lblStatus.Appearance.ForeColor = System.Drawing.Color.DarkOrange
                    lblStatus.Text = "No registered item codes were returned by the portal. You can continue maintaining codes manually below."
                    Return
                End If

                Dim importedCount = 0
                For Each code In portalCodes
                    _repo.Upsert(code)
                    importedCount += 1
                Next

                lblStatus.Appearance.ForeColor = System.Drawing.Color.DarkGreen
                lblStatus.Text = $"Imported/merged {importedCount} item code(s) from the portal."
                LoadAll()

            Catch ex As Exception
                lblStatus.Appearance.ForeColor = System.Drawing.Color.DarkOrange
                lblStatus.Text = "Could not retrieve codes from the portal (the ETA environment may not expose this endpoint). " &
                                  "You can keep maintaining your EGS code library manually below. Details: " & ex.Message
            Finally
                btnImportFromPortal.Enabled = True
            End Try
        End Sub

    End Class

End Namespace
