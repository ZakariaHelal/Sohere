Option Strict On
Imports System.Windows.Forms
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Views.Grid
Imports Newtonsoft.Json
Imports SphereERP.Data
Imports SphereERP.Models
Imports SphereERP.Services
Imports SphereERP.[Global]

Namespace Forms

    Public Class DocumentSearchForm

        Private ReadOnly _docRepo As New DocumentRepository()
        Private ReadOnly _pdfService As New PdfGenerationService()
        Private _results As List(Of DocumentRow) = New List(Of DocumentRow)()

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub DocumentSearchForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            cmbTypeFilter.Properties.Items.Clear()
            cmbTypeFilter.Properties.Items.AddRange(New Object() {"(All)", "I - Invoice", "C - Credit Note", "D - Debit Note", "EI - Export Invoice", "EC - Export Credit Note", "ED - Export Debit Note"})
            cmbTypeFilter.SelectedIndex = 0

            cmbStatusFilter.Properties.Items.Clear()
            cmbStatusFilter.Properties.Items.AddRange(New Object() {"(All)", "Valid", "Invalid", "Submitted", "Rejected", "Cancelled", "Failed"})
            cmbStatusFilter.SelectedIndex = 0

            dtDateFrom.EditValue = GetFirstDayOfMonth(Today) 'DateTime.Now.AddMonths(-1)
            dtDateTo.EditValue = GetLastDayOfMonth(Today) ' DateTime.Now

            cmbDirection.Properties.Items.Clear()
            cmbDirection.Properties.Items.AddRange(New Object() {"(All)", "Sent (Issued by me)", "Received (Issued to me)"})
            cmbDirection.SelectedIndex = 0

            RunSearch()
        End Sub

        Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
            RunSearch()
        End Sub

        Private Async Sub btnSyncPortal_Click(sender As Object, e As EventArgs) Handles btnSyncPortal.Click
            If AppState.PortalSyncService Is Nothing Then
                lblStatus.ForeColor = System.Drawing.Color.DarkRed
                lblStatus.Text = "Portal sync service is not initialized. Sign in again and retry."
                Return
            End If

            If MessageBox.Show(
                $"Retrieve documents from the ETA portal for {dtDateFrom.DateTime:yyyy-MM-dd} to {dtDateTo.DateTime:yyyy-MM-dd} and store them in the local Documents table?",
                "Sync from Portal",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) <> DialogResult.Yes Then
                Return
            End If

            If (dtDateTo.DateTime.Date - dtDateFrom.DateTime.Date).TotalDays > 30 Then
                MessageBox.Show("The ETA portal only allows syncing up to 30 days at a time. Please narrow your date range and try again.",
                                "Date Range Too Wide", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            SetSyncBusy(True)
            lblStatus.ForeColor = System.Drawing.Color.DimGray
            lblStatus.Text = "Retrieving documents from ETA portal..."

            Try
                progressSync.Value = 0
                Dim progressReporter = New Progress(Of Integer)(Sub(pct)
                                                                     progressSync.Value = pct
                                                                     lblStatus.Text = $"Syncing... {pct}% complete"
                                                                 End Sub)
                Dim syncResult = Await AppState.PortalSyncService.SyncAllDocumentsAsync(
                    dateFrom:=dtDateFrom.DateTime.Date,
                    dateTo:=dtDateTo.DateTime.Date,
                    onProgress:=progressReporter).ConfigureAwait(True)

                progressSync.Value = 100
                Dim syncMsg = $"Portal sync complete: {syncResult.RetrievedCount} retrieved ({syncResult.DirectionSummary}). {syncResult.InsertedCount} new, {syncResult.SkippedCount} already existed (skipped)."
                MessageBox.Show(syncMsg, "Sync Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)
                RunSearch()
            Catch ex As Exception
                lblStatus.ForeColor = System.Drawing.Color.DarkRed
                lblStatus.Text = "Portal sync failed: " & ex.Message
            Finally
                SetSyncBusy(False)
            End Try
        End Sub

        Private Sub SetSyncBusy(busy As Boolean)
            btnSyncPortal.Enabled = Not busy
            btnSearch.Enabled = Not busy
            Me.Cursor = If(busy, Cursors.WaitCursor, Cursors.Default)
            progressSync.Visible = busy
            If busy Then progressSync.BringToFront()
        End Sub

        Private Async Sub RunSearch()
            Try
                Dim typeFilter As String = Nothing
                If cmbTypeFilter.SelectedIndex > 0 Then
                    typeFilter = Convert.ToString(cmbTypeFilter.SelectedItem)
                    If typeFilter.Contains(" - ") Then typeFilter = typeFilter.Split(New String() {" - "}, StringSplitOptions.None)(0)
                End If

                Dim statusFilter As String = Nothing
                If cmbStatusFilter.SelectedIndex > 0 Then
                    statusFilter = Convert.ToString(cmbStatusFilter.SelectedItem)
                End If

                _results = _docRepo.Search(
                    internalId:=If(String.IsNullOrWhiteSpace(txtInternalIdFilter.Text), Nothing, txtInternalIdFilter.Text.Trim()),
                    documentType:=typeFilter,
                    status:=statusFilter,
                    dateFrom:=dtDateFrom.DateTime.Date,
                    dateTo:=dtDateTo.DateTime.Date,
                    receiverName:=If(String.IsNullOrWhiteSpace(txtReceiverFilter.Text), Nothing, txtReceiverFilter.Text.Trim()),
                    issuerName:=If(String.IsNullOrWhiteSpace(txtIssuerFilter.Text), Nothing, txtIssuerFilter.Text.Trim()))

                Dim myRin = If(AppState.CurrentSettings?.TaxpayerRIN, "")
                Select Case Convert.ToString(cmbDirection.SelectedItem)
                    Case "Sent (Issued by me)"
                        _results = _results.Where(Function(d) String.Equals(d.IssuerRIN, myRin, StringComparison.OrdinalIgnoreCase)).ToList()
                    Case "Received (Issued to me)"
                        _results = _results.Where(Function(d) String.Equals(d.ReceiverRIN, myRin, StringComparison.OrdinalIgnoreCase)).ToList()
                End Select

                For Each d In _results
                    If String.Equals(d.IssuerRIN, myRin, StringComparison.OrdinalIgnoreCase) Then
                        d.Direction = "Sent"
                    ElseIf String.Equals(d.ReceiverRIN, myRin, StringComparison.OrdinalIgnoreCase) Then
                        d.Direction = "Received"
                    Else
                        d.Direction = ""
                    End If
                Next

                BindResultsGrid()
                Await LoadDetailGridAsync().ConfigureAwait(True)
                lblStatus.Text = $"{_results.Count} document(s) found."
                lblStatus.ForeColor = System.Drawing.Color.DimGray
            Catch ex As Exception
                lblStatus.ForeColor = System.Drawing.Color.DarkRed
                lblStatus.Text = "Search failed: " & ex.Message
            End Try
        End Sub

        Private Sub BindResultsGrid()
            gridViewResults.Columns.Clear()
            gridViewResults.ColumnPanelRowHeight = 50

            Dim col1 = gridViewResults.Columns.AddField("InternalId")
            col1.VisibleIndex = 0
            col1.Caption = "Internal ID"
            col1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            col1.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center

            Dim col2 = gridViewResults.Columns.AddField("Direction")
            col2.VisibleIndex = 1
            col2.Caption = "Direction"
            col2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            col2.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
            col2.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center

            Dim col3 = gridViewResults.Columns.AddField("DocumentTypeDisplay")
            col3.VisibleIndex = 2
            col3.Caption = "Type"
            col3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            col3.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
            col3.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center

            Dim col4 = gridViewResults.Columns.AddField("ReceiverName")
            col4.VisibleIndex = 3
            col4.Caption = "Receiver"
            col4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            col4.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center

            Dim col5 = gridViewResults.Columns.AddField("IssuerName")
            col5.VisibleIndex = 4
            col5.Caption = "Issuer"
            col5.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            col5.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center

            Dim col7 = gridViewResults.Columns.AddField("DateTimeIssued")
            col7.VisibleIndex = 6
            col7.Caption = "Issue Date"
            col7.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            col7.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
            col7.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center

            Dim col8 = gridViewResults.Columns.AddField("TotalAmount")
            col8.VisibleIndex = 7
            col8.Caption = "Total (EGP)"
            col8.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            col8.DisplayFormat.FormatString = "N2"
            col8.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            col8.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
            col8.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            col8.Summary.Add(DevExpress.Data.SummaryItemType.Sum, "TotalAmount")
            col8.SummaryItem.DisplayFormat = "{0:N2}"

            Dim col9 = gridViewResults.Columns.AddField("Status")
            col9.VisibleIndex = 8
            col9.Caption = "Status"
            col9.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            col9.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
            col9.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center

            Dim col10 = gridViewResults.Columns.AddField("Uuid")
            col10.VisibleIndex = 9
            col8.Caption = "UUID"
            col8.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            col8.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center

            gridResults.DataSource = _results
            gridViewResults.BestFitColumns()
        End Sub

        Private Function GetSelectedDocument() As DocumentRow
            If gridViewResults.FocusedRowHandle < 0 Then Return Nothing
            Return TryCast(gridViewResults.GetFocusedRow(), DocumentRow)
        End Function

        Private Async Sub gridViewResults_FocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles gridViewResults.FocusedRowChanged
            Try
                Await LoadDetailGridAsync().ConfigureAwait(True)
            Catch ex As Exception
                lblDetailHeader.Text = $"Details (error: {ex.Message})"
            End Try
        End Sub

        Private Async Function LoadDetailGridAsync() As Task
            Try
                gridViewDetail.Columns.Clear()
                gridDetail.DataSource = Nothing

                Dim doc = GetSelectedDocument()
                If doc Is Nothing Then
                    lblDetailHeader.Text = "Line Items"
                    Return
                End If

                Dim json = _docRepo.GetDocumentJson(doc.Id)
                If String.IsNullOrWhiteSpace(json) Then
                    lblDetailHeader.Text = $"Line Items (no data for {doc.InternalId})"
                    Return
                End If

                If json.Contains("""invoiceLines""") Then
                    Try
                        Dim fullDoc = JsonConvert.DeserializeObject(Of EInvoiceDocument)(json, JsonHelper.GetLenientSettings())
                        Dim lines = fullDoc?.InvoiceLines
                        If lines IsNot Nothing AndAlso lines.Count > 0 Then
                            FixTaxableAmounts(lines)
                            BindInvoiceLines(lines, doc.InternalId)
                            Return
                        End If
                    Catch
                    End Try
                End If

                If Not String.IsNullOrWhiteSpace(doc.Uuid) AndAlso AppState.ApiClient IsNot Nothing Then
                    lblDetailHeader.Text = $"Fetching details for {doc.InternalId}..."
                    Dim portalJson As String = Nothing
                    Try
                        portalJson = Await AppState.ApiClient.GetDocumentDetailsRawJsonAsync(doc.Uuid).ConfigureAwait(True)
                    Catch
                    End Try

                    If Not String.IsNullOrWhiteSpace(portalJson) Then
                        If portalJson.Contains("""invoiceLines""") Then
                            Dim portalDoc As EInvoiceDocument = Nothing
                            Try
                                portalDoc = JsonConvert.DeserializeObject(Of EInvoiceDocument)(portalJson, JsonHelper.GetLenientSettings())
                            Catch
                            End Try
                            Dim portalLines = portalDoc?.InvoiceLines
                            If portalLines IsNot Nothing AndAlso portalLines.Count > 0 Then
                                FixTaxableAmounts(portalLines)
                                _docRepo.UpdateDocumentJson(doc.Id, portalJson)
                                BindInvoiceLines(portalLines, doc.InternalId)
                                Return
                            End If
                        End If
                        json = portalJson
                    End If
                End If

                Dim searchItem = JsonConvert.DeserializeObject(Of DocumentSearchItem)(json)
                If searchItem Is Nothing Then
                    lblDetailHeader.Text = $"Unable to parse document details for {doc.InternalId}"
                    Return
                End If
                Dim items As New List(Of DocumentSearchItem)()
                items.Add(searchItem)

                lblDetailHeader.Text = $"Document Details - {doc.InternalId}"

                gridViewDetail.ColumnPanelRowHeight = 50

                Dim c1 = gridViewDetail.Columns.AddField("Uuid")
                c1.VisibleIndex = 0
                c1.Caption = "UUID"
                AlignHeader(c1)

                Dim c2 = gridViewDetail.Columns.AddField("TypeName")
                c2.VisibleIndex = 1
                c2.Caption = "Type"
                AlignHeader(c2)
                AlignCellCenter(c2)

                Dim c3 = gridViewDetail.Columns.AddField("IssuerName")
                c3.VisibleIndex = 2
                c3.Caption = "Issuer"
                AlignHeader(c3)

                Dim c4 = gridViewDetail.Columns.AddField("ReceiverName")
                c4.VisibleIndex = 3
                c4.Caption = "Receiver"
                AlignHeader(c4)

                Dim c5 = gridViewDetail.Columns.AddField("DateTimeIssued")
                c5.VisibleIndex = 4
                c5.Caption = "Issue Date"
                AlignHeader(c5)
                AlignCellCenter(c5)
                c5.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime

                Dim c6 = gridViewDetail.Columns.AddField("Total")
                c6.VisibleIndex = 5
                c6.Caption = "Total"
                c6.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                c6.DisplayFormat.FormatString = "N2"
                AlignHeader(c6)
                AlignCellCenter(c6)

                Dim c7 = gridViewDetail.Columns.AddField("NetAmount")
                c7.VisibleIndex = 6
                c7.Caption = "Net"
                c7.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                c7.DisplayFormat.FormatString = "N2"
                AlignHeader(c7)
                AlignCellCenter(c7)

                Dim c8 = gridViewDetail.Columns.AddField("Status")
                c8.VisibleIndex = 7
                c8.Caption = "Status"
                AlignHeader(c8)
                AlignCellCenter(c8)

                gridDetail.DataSource = items
                gridViewDetail.BestFitColumns()
            Catch ex As Exception
                lblDetailHeader.Text = $"Details (error: {ex.Message})"
            End Try
        End Function

        Private Shared Sub AlignHeader(col As DevExpress.XtraGrid.Columns.GridColumn)
            col.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            col.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        End Sub

        Private Shared Sub AlignCellCenter(col As DevExpress.XtraGrid.Columns.GridColumn)
            col.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        End Sub

        Private Sub BindInvoiceLines(lines As List(Of InvoiceLineModel), internalId As String)
            lblDetailHeader.Text = $"Line Items ({lines.Count}) - {internalId}"
            gridViewDetail.ColumnPanelRowHeight = 50

            Dim col1 = gridViewDetail.Columns.AddField("Description")
            col1.VisibleIndex = 0
            col1.Caption = "Description"
            AlignHeader(col1)

            Dim col2 = gridViewDetail.Columns.AddField("ItemCode")
            col2.VisibleIndex = 1
            col2.Caption = "Item Code"
            AlignHeader(col2)

            Dim col3 = gridViewDetail.Columns.AddField("UnitType")
            col3.VisibleIndex = 2
            col3.Caption = "Unit"
            AlignHeader(col3)
            AlignCellCenter(col3)

            Dim col4 = gridViewDetail.Columns.AddField("Quantity")
            col4.VisibleIndex = 3
            col4.Caption = "Qty"
            col4.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            col4.DisplayFormat.FormatString = "N2"
            AlignHeader(col4)
            AlignCellCenter(col4)

            Dim col5 = gridViewDetail.Columns.AddField("UnitPriceAmount")
            col5.VisibleIndex = 4
            col5.Caption = "Unit Price"
            col5.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            col5.DisplayFormat.FormatString = "N2"
            AlignHeader(col5)
            AlignCellCenter(col5)

            Dim col6 = gridViewDetail.Columns.AddField("NetTotal")
            col6.VisibleIndex = 5
            col6.Caption = "Net Total"
            col6.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            col6.DisplayFormat.FormatString = "N2"
            AlignHeader(col6)
            AlignCellCenter(col6)

            Dim col7 = gridViewDetail.Columns.AddField("DiscountAmount")
            col7.VisibleIndex = 6
            col7.Caption = "Discount"
            col7.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            col7.DisplayFormat.FormatString = "N2"
            AlignHeader(col7)
            AlignCellCenter(col7)

            Dim col8 = gridViewDetail.Columns.AddField("TaxAmount")
            col8.VisibleIndex = 7
            col8.Caption = "Tax"
            col8.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            col8.DisplayFormat.FormatString = "N2"
            AlignHeader(col8)
            AlignCellCenter(col8)

            gridDetail.DataSource = lines
            gridViewDetail.BestFitColumns()
        End Sub

        Private Shared Sub FixTaxableAmounts(lines As List(Of InvoiceLineModel))
            For Each l In lines
                If l.TaxableItems IsNot Nothing AndAlso l.NetTotal > 0 Then
                    For Each t In l.TaxableItems
                        If t.Amount = 0D AndAlso t.Rate > 0 Then
                            t.Amount = Math.Round(l.NetTotal * (t.Rate / 100D), 5)
                        End If
                    Next
                End If
            Next
        End Sub

        Private Sub btnViewDetails_Click(sender As Object, e As EventArgs) Handles btnViewDetails.Click
            Dim doc = GetSelectedDocument()
            If doc Is Nothing Then
                MessageBox.Show("Select a document first.", "View Details", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            Dim json = _docRepo.GetDocumentJson(doc.Id)
            Using dlg As New DevExpress.XtraEditors.XtraForm() With {
                .Text = $"Document Details - {doc.InternalId}",
                .Width = 700,
                .Height = 600,
                .StartPosition = FormStartPosition.CenterParent
            }
                Dim txt As New DevExpress.XtraEditors.MemoEdit()
                txt.Dock = DockStyle.Fill
                txt.Properties.ReadOnly = True
                txt.Font = New System.Drawing.Font("Consolas", 9.0!)
                txt.Text = json
                dlg.Controls.Add(txt)
                dlg.ShowDialog(Me)
            End Using
        End Sub

        Private Async Sub btnDownloadPdf_Click(sender As Object, e As EventArgs) Handles btnDownloadPdf.Click
            Dim doc = GetSelectedDocument()
            If doc Is Nothing Then
                MessageBox.Show("Select a document first.", "Download PDF", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If
            If String.IsNullOrWhiteSpace(doc.Uuid) Then
                MessageBox.Show("This document has not been issued a UUID yet (not yet accepted by ETA).", "Download PDF", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Using saveDialog As New SaveFileDialog With {
                .Filter = "PDF files (*.pdf)|*.pdf",
                .FileName = $"{doc.InternalId}.pdf"
            }
                If saveDialog.ShowDialog(Me) <> DialogResult.OK Then Return

                Try
                    lblStatus.Text = "Downloading PDF..."
                    Dim pdfBytes = Await AppState.ApiClient.DownloadDocumentPdfAsync(doc.Uuid).ConfigureAwait(True)
                    _pdfService.SavePortalPdf(pdfBytes, saveDialog.FileName)
                    lblStatus.ForeColor = System.Drawing.Color.DarkGreen
                    lblStatus.Text = "PDF saved: " & saveDialog.FileName
                Catch ex As Exception
                    lblStatus.ForeColor = System.Drawing.Color.DarkRed
                    lblStatus.Text = "Failed to download PDF: " & ex.Message
                End Try
            End Using
        End Sub

        Private Async Sub btnCancelDocument_Click(sender As Object, e As EventArgs) Handles btnCancelDocument.Click
            Await ChangeStatus("cancelled")
        End Sub

        Private Async Sub btnRejectDocument_Click(sender As Object, e As EventArgs) Handles btnRejectDocument.Click
            Await ChangeStatus("rejected")
        End Sub

        Private Async Function ChangeStatus(targetStatus As String) As Task
            Dim doc = GetSelectedDocument()
            If doc Is Nothing Then
                MessageBox.Show("Select a document first.", "Change Status", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If
            If String.IsNullOrWhiteSpace(doc.Uuid) Then
                MessageBox.Show("This document has no UUID (not yet accepted by ETA) and cannot be cancelled/rejected via the portal.", "Change Status", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim reason = Microsoft.VisualBasic.Interaction.InputBox(
                $"Enter a reason for marking this document as '{targetStatus}':", "Reason Required", "")
            If String.IsNullOrWhiteSpace(reason) Then Return

            Dim verb = If(targetStatus = "cancelled", "cancel", "reject")
            If MessageBox.Show($"Are you sure you want to {verb} document {doc.InternalId}? This cannot be undone.",
                                "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) <> DialogResult.Yes Then
                Return
            End If

            Try
                lblStatus.Text = $"Updating status to {targetStatus}..."
                Await AppState.SubmissionService.ChangeStatusAsync(doc.Id, doc.Uuid, doc.InternalId, targetStatus, reason).ConfigureAwait(True)
                lblStatus.ForeColor = System.Drawing.Color.DarkGreen
                lblStatus.Text = $"Document {doc.InternalId} marked as {targetStatus}."
                RunSearch()
            Catch ex As Exception
                lblStatus.ForeColor = System.Drawing.Color.DarkRed
                lblStatus.Text = "Failed to update status: " & ex.Message
            End Try
        End Function

    End Class

    ''' <summary>Converts null JSON values to 0 for Decimal properties.</summary>
    Public Class NullToDecimalConverter
        Inherits JsonConverter(Of Decimal)

        Public Overrides Function ReadJson(reader As JsonReader, objectType As Type, existingValue As Decimal, hasExistingValue As Boolean, serializer As JsonSerializer) As Decimal
            If reader.TokenType = JsonToken.Null Then Return 0D
            If reader.TokenType = JsonToken.String AndAlso String.IsNullOrWhiteSpace(Convert.ToString(reader.Value)) Then Return 0D
            Return Convert.ToDecimal(reader.Value)
        End Function

        Public Overrides Sub WriteJson(writer As JsonWriter, value As Decimal, serializer As JsonSerializer)
            writer.WriteValue(value)
        End Sub
    End Class

    Friend Module JsonHelper
        Private _settings As JsonSerializerSettings = Nothing
        Private _lock As New Object()

        Public Function GetLenientSettings() As JsonSerializerSettings
            If _settings Is Nothing Then
                SyncLock _lock
                    If _settings Is Nothing Then
                        _settings = New JsonSerializerSettings()
                        _settings.Converters.Add(New NullToDecimalConverter())
                    End If
                End SyncLock
            End If
            Return _settings
        End Function
    End Module

End Namespace
