Option Strict On
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.Utils
Imports System.Windows.Forms
Imports SphereERP.Models
Imports SphereERP.Services
Imports SphereERP.Data
Imports SphereERP.[Global]

Namespace Forms

    Public Class InvoiceEntryForm

        Private ReadOnly _lines As New List(Of InvoiceLineModel)()
        Private ReadOnly _egsRepo As New EgsCodeRepository()
        Private ReadOnly _pdfService As New PdfGenerationService()

        Private _lastSubmittedUuid As String = ""
        Private _lastInternalId As String = ""

        Private Class DocumentTypeOption
            Public Property Code As DocumentTypeEnum
            Public Property Description As String
            Public Overrides Function ToString() As String
                Return $"{Code.ToString().ToLowerInvariant()} - {Description}"
            End Function
        End Class

        Private Shared ReadOnly _documentTypeOptions As New List(Of DocumentTypeOption) From {
            New DocumentTypeOption With {.Code = DocumentTypeEnum.I, .Description = "Invoice"},
            New DocumentTypeOption With {.Code = DocumentTypeEnum.C, .Description = "Credit Note"},
            New DocumentTypeOption With {.Code = DocumentTypeEnum.D, .Description = "Debit Note"},
            New DocumentTypeOption With {.Code = DocumentTypeEnum.EI, .Description = "Export Invoice"},
            New DocumentTypeOption With {.Code = DocumentTypeEnum.EC, .Description = "Export Credit Note"},
            New DocumentTypeOption With {.Code = DocumentTypeEnum.ED, .Description = "Export Debit Note"}
        }

        Private Shared ReadOnly _currencyOptions As String() = {
            "EGP - Egyptian Pound", "USD - US Dollar", "EUR - Euro",
            "GBP - British Pound", "CHF - Swiss Franc", "SAR - Saudi Riyal",
            "AED - UAE Dirham", "KWD - Kuwaiti Dinar", "QAR - Qatari Riyal",
            "OMR - Omani Rial", "BHD - Bahraini Dinar", "JOD - Jordanian Dinar",
            "LBP - Lebanese Pound", "TRY - Turkish Lira", "CNY - Chinese Yuan",
            "JPY - Japanese Yen", "INR - Indian Rupee", "AUD - Australian Dollar",
            "CAD - Canadian Dollar", "NOK - Norwegian Krone", "SEK - Swedish Krona",
            "DKK - Danish Krone", "PLN - Polish Zloty", "ZAR - South African Rand"
        }

        Private Shared ReadOnly _countryOptions As String() = {
            "EG - Egypt", "SA - Saudi Arabia", "AE - United Arab Emirates",
            "KW - Kuwait", "QA - Qatar", "OM - Oman", "BH - Bahrain",
            "JO - Jordan", "LB - Lebanon", "IQ - Iraq",
            "US - United States", "GB - United Kingdom", "DE - Germany",
            "FR - France", "IT - Italy", "ES - Spain", "NL - Netherlands",
            "BE - Belgium", "CH - Switzerland", "SE - Sweden", "NO - Norway",
            "DK - Denmark", "PL - Poland", "TR - Turkey", "RU - Russia",
            "CN - China", "JP - Japan", "KR - South Korea", "IN - India",
            "AU - Australia", "BR - Brazil", "ZA - South Africa", "NG - Nigeria",
            "MA - Morocco", "DZ - Algeria", "TN - Tunisia", "LY - Libya",
            "SD - Sudan", "KE - Kenya", "ET - Ethiopia",
            "SG - Singapore", "MY - Malaysia", "ID - Indonesia"
        }

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub InvoiceEntryForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            cmbDocumentType.Properties.Items.Clear()
            For Each opt In _documentTypeOptions
                cmbDocumentType.Properties.Items.Add(opt)
            Next
            cmbDocumentType.SelectedIndex = 0

            cmbReceiverType.Properties.Items.Clear()
            cmbReceiverType.Properties.Items.AddRange(New Object() {"B - Business", "P - Natural Person", "F - Foreigner"})
            cmbReceiverType.SelectedIndex = 0

            cmbReceiverCountry.Properties.Items.Clear()
            cmbReceiverCountry.Properties.Items.AddRange(_countryOptions)
            cmbReceiverCountry.SelectedIndex = 0

            cmbCurrency.Properties.Items.Clear()
            cmbCurrency.Properties.Items.AddRange(_currencyOptions)
            cmbCurrency.SelectedIndex = 0

            cmbPaymentMethod.Properties.Items.Clear()
            cmbPaymentMethod.Properties.Items.AddRange(New Object() {"C - Cash", "V - Visa", "CC - Cash with Contractor", "VC - Visa with Contractor", "VO - Vouchers", "PR - Promotion", "GC - Gift Card", "P - Points", "O - Others"})
            cmbPaymentMethod.SelectedIndex = 0

            dtDateIssued.EditValue = DateTime.Now

            UpdateDocumentTypeState()

            AddHandler txtReceiverId.DoubleClick, AddressOf txtReceiverId_DoubleClick
            RefreshGrid()
        End Sub

        Private Sub cmbDocumentType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbDocumentType.SelectedIndexChanged
            UpdateDocumentTypeState()
        End Sub

        Private Sub UpdateDocumentTypeState()
            Dim opt = TryCast(cmbDocumentType.SelectedItem, DocumentTypeOption)
            If opt Is Nothing Then Return

            Dim typeName = opt.Description
            lblPageTitle.Text = $"New {typeName}"
            Me.Text = $"New {typeName}"

            Dim isNote = (opt.Code = DocumentTypeEnum.C OrElse opt.Code = DocumentTypeEnum.D OrElse
                          opt.Code = DocumentTypeEnum.EC OrElse opt.Code = DocumentTypeEnum.ED)
            lblReferenceUuid.Enabled = isNote
            txtReferenceUuid.Enabled = isNote
            If Not isNote Then txtReferenceUuid.Text = ""
        End Sub

        Private Sub cmbCurrency_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbCurrency.SelectedIndexChanged
            UpdateCurrencyState()
        End Sub

        Private Sub UpdateCurrencyState()
            Dim currency = Convert.ToString(cmbCurrency.SelectedItem)
            If currency.Contains(" - ") Then currency = currency.Split(New String() {" - "}, StringSplitOptions.None)(0)
            Dim isForeign = (currency <> "EGP")
            lblCurrencyExchangeRate.Visible = isForeign
            txtCurrencyExchangeRate.Visible = isForeign
        End Sub

        Private Sub txtReceiverId_DoubleClick(sender As Object, e As EventArgs)
            Using dlg As New ReceiverSelectorForm()
                If dlg.ShowDialog(Me) = DialogResult.OK Then
                    Dim rec = dlg.SelectedReceiver
                    If rec IsNot Nothing Then
                        txtReceiverId.Text = rec.Rin
                        txtReceiverName.Text = rec.Name
                        For Each item In cmbReceiverCountry.Properties.Items
                            If Convert.ToString(item).StartsWith(rec.Country + " - ") Then
                                cmbReceiverCountry.SelectedItem = item
                                Exit For
                            End If
                        Next
                        txtReceiverGovernate.Text = rec.Governate
                        txtReceiverCity.Text = rec.RegionCity
                        txtReceiverStreet.Text = rec.Street
                        txtReceiverBuilding.Text = rec.BuildingNumber
                    End If
                End If
            End Using
        End Sub

        Private Function GetTypeDisplayName() As String
            Dim opt = TryCast(cmbDocumentType.SelectedItem, DocumentTypeOption)
            Return If(opt IsNot Nothing, opt.Description, "Document")
        End Function

        Private Function GetSelectedDocumentTypeCode() As String
            Dim opt = TryCast(cmbDocumentType.SelectedItem, DocumentTypeOption)
            Return If(opt IsNot Nothing, opt.Code.ToString(), "I")
        End Function

        Private Function GetSelectedCurrencyCode() As String
            Dim currency = Convert.ToString(cmbCurrency.SelectedItem)
            If currency.Contains(" - ") Then currency = currency.Split(New String() {" - "}, StringSplitOptions.None)(0)
            Return currency
        End Function

        Private Function GetExchangeRate() As Decimal
            Dim rate As Decimal
            Decimal.TryParse(txtCurrencyExchangeRate.Text, rate)
            Return If(rate > 0, rate, 1D)
        End Function

        Private Sub ApplyCurrencyToLine(line As InvoiceLineModel)
            Dim currency = GetSelectedCurrencyCode()
            If currency = "EGP" OrElse String.IsNullOrWhiteSpace(currency) Then
                line.UnitValue.CurrencySold = "EGP"
            Else
                Dim rate = GetExchangeRate()
                line.UnitValue.CurrencySold = currency
                line.UnitValue.AmountSold = line.UnitValue.AmountEGP
                line.UnitValue.CurrencyExchangeRate = rate
                line.UnitValue.AmountEGP = Math.Round(line.UnitValue.AmountSold * rate, 5)
            End If
        End Sub

        Private Sub btnAddLine_Click(sender As Object, e As EventArgs) Handles btnAddLine.Click
            Using editor As New LineItemEditorForm()
                editor.LoadLine(Nothing)
                If editor.ShowDialog(Me) = DialogResult.OK Then
                    Dim line = editor.ResultLine
                    ApplyCurrencyToLine(line)
                    _lines.Add(line)
                    RefreshGrid()
                End If
            End Using
        End Sub

        Private Sub btnEditLine_Click(sender As Object, e As EventArgs) Handles btnEditLine.Click
            Dim idx = gridViewLines.FocusedRowHandle
            If idx < 0 OrElse idx >= _lines.Count Then
                MessageBox.Show("Select a line to edit.", "Edit Line", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            Using editor As New LineItemEditorForm()
                editor.LoadLine(_lines(idx))
                If editor.ShowDialog(Me) = DialogResult.OK Then
                    Dim line = editor.ResultLine
                    ApplyCurrencyToLine(line)
                    _lines(idx) = line
                    RefreshGrid()
                End If
            End Using
        End Sub

        Private Sub btnRemoveLine_Click(sender As Object, e As EventArgs) Handles btnRemoveLine.Click
            Dim idx = gridViewLines.FocusedRowHandle
            If idx < 0 OrElse idx >= _lines.Count Then
                MessageBox.Show("Select a line to remove.", "Remove Line", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If
            _lines.RemoveAt(idx)
            RefreshGrid()
        End Sub

        Private Sub btnPickFromEgs_Click(sender As Object, e As EventArgs) Handles btnPickFromEgs.Click
            Dim codes = _egsRepo.GetAll(activeOnly:=True)
            If codes.Count = 0 Then
                MessageBox.Show("No EGS codes found in your library. Add some via the EGS Code Library page first.", "EGS Code Library", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            Using picker As New System.Windows.Forms.Form() With {
                .Text = "Select EGS Item",
                .Width = 1500,
                .Height = 500,
                .StartPosition = FormStartPosition.CenterParent,
                .FormBorderStyle = FormBorderStyle.Sizable,
                .MaximizeBox = False,
                .MinimizeBox = False
            }
                Dim grid As New DevExpress.XtraGrid.GridControl() With {.Dock = DockStyle.Fill}
                Dim gridView As New DevExpress.XtraGrid.Views.Grid.GridView()
                CType(grid, System.ComponentModel.ISupportInitialize).BeginInit()
                CType(gridView, System.ComponentModel.ISupportInitialize).BeginInit()
                grid.MainView = gridView
                grid.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {gridView})
                gridView.GridControl = grid
                gridView.OptionsBehavior.Editable = False
                gridView.OptionsView.ShowGroupPanel = False
                gridView.OptionsBehavior.AutoPopulateColumns = True
                gridView.Appearance.HeaderPanel.Options.UseTextOptions = True
                gridView.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                gridView.Appearance.Row.Options.UseTextOptions = True
                'gridView.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                grid.DataSource = codes



                For Each col As DevExpress.XtraGrid.Columns.GridColumn In gridView.Columns
                    col.AppearanceHeader.Options.UseTextOptions = True
                    col.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                    col.AppearanceCell.Options.UseTextOptions = True
                    col.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                Next

                Dim egsCol = TryCast(gridView.Columns.ColumnByFieldName("EgsCode"), DevExpress.XtraGrid.Columns.GridColumn)
                If egsCol IsNot Nothing Then
                    egsCol.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                    egsCol.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
                End If

                gridView.OptionsView.ColumnAutoWidth = True
                gridView.BestFitColumns()

                Dim btnSelect As New DevExpress.XtraEditors.SimpleButton() With {
                    .Text = "Add Selected",
                    .Dock = DockStyle.Bottom,
                    .Height = 36
                }
                btnSelect.Appearance.BackColor = System.Drawing.Color.Green
                btnSelect.Appearance.ForeColor = System.Drawing.Color.White
                btnSelect.Appearance.Font = New System.Drawing.Font("Segoe UI", 9.5!, System.Drawing.FontStyle.Bold)
                btnSelect.Appearance.Options.UseFont = True
                btnSelect.Appearance.Options.UseBackColor = True
                btnSelect.Appearance.Options.UseForeColor = True

                AddHandler btnSelect.Click, Sub()
                                                Dim selected = TryCast(gridView.GetFocusedRow(), EgsCodeModel)
                                                If selected Is Nothing Then Return
                                                Dim line As New InvoiceLineModel With {
                                                     .Description = selected.Description,
                                                     .ItemCode = selected.EgsCode,
                                                     .ItemType = "EGS",
                                                     .UnitType = selected.UnitType,
                                                     .Quantity = 1
                                                 }
                                                Dim currency = Convert.ToString(cmbCurrency.SelectedItem)
                                                If currency.Contains(" - ") Then currency = currency.Split(New String() {" - "}, StringSplitOptions.None)(0)
                                                If currency = "EGP" Then
                                                    line.UnitValue.AmountEGP = selected.DefaultPrice
                                                    line.UnitValue.CurrencySold = "EGP"
                                                Else
                                                    line.UnitValue.CurrencySold = currency
                                                    line.UnitValue.AmountSold = selected.DefaultPrice
                                                    Dim exchRate As Decimal
                                                    Decimal.TryParse(txtCurrencyExchangeRate.Text, exchRate)
                                                    If exchRate <= 0 Then exchRate = 1D
                                                    line.UnitValue.CurrencyExchangeRate = exchRate
                                                    line.UnitValue.AmountEGP = Math.Round(selected.DefaultPrice * exchRate, 5)
                                                End If
                                                line.TaxableItems.Add(New TaxItemModel With {.TaxType = selected.DefaultTaxType, .SubType = TaxItemModel.DefaultSubType(selected.DefaultTaxType), .Rate = selected.DefaultTaxRate})
                                                line.Recalculate()
                                                _lines.Add(line)
                                                RefreshGrid()
                                                picker.DialogResult = DialogResult.OK
                                                picker.Close()
                                            End Sub

                CType(gridView, System.ComponentModel.ISupportInitialize).EndInit()
                CType(grid, System.ComponentModel.ISupportInitialize).EndInit()

                picker.Controls.Add(btnSelect)
                picker.Controls.Add(grid)
                grid.BringToFront()
                picker.ShowDialog(Me)
            End Using
        End Sub

        Private Sub RefreshGrid()
            gridViewLines.Columns.Clear()
            gridViewLines.ColumnPanelRowHeight = 50

            Dim colDesc = gridViewLines.Columns.AddField("Description")
            colDesc.Caption = "Description"
            colDesc.VisibleIndex = gridViewLines.Columns.Count - 1
            colDesc.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            colDesc.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center

            Dim colCode = gridViewLines.Columns.AddField("ItemCode")
            colCode.Caption = "Item Code"
            colCode.VisibleIndex = gridViewLines.Columns.Count - 1
            colCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            colCode.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center

            Dim colQty = gridViewLines.Columns.AddField("Quantity")
            colQty.Caption = "Qty"
            colQty.VisibleIndex = gridViewLines.Columns.Count - 1
            colQty.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            colQty.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
            colQty.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            colQty.Summary.Add(DevExpress.Data.SummaryItemType.Sum, "Quantity")
            colQty.SummaryItem.DisplayFormat = "{0:N2}"

            Dim colPrice = gridViewLines.Columns.AddField("UnitPriceAmount")
            colPrice.Caption = "Unit Price"
            colPrice.DisplayFormat.FormatType = FormatType.Numeric
            colPrice.DisplayFormat.FormatString = "N2"
            colPrice.VisibleIndex = gridViewLines.Columns.Count - 1
            colPrice.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            colPrice.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
            colPrice.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center

            Dim colDisc = gridViewLines.Columns.AddField("DiscountAmount")
            colDisc.Caption = "Discount"
            colDisc.DisplayFormat.FormatType = FormatType.Numeric
            colDisc.DisplayFormat.FormatString = "N2"
            colDisc.VisibleIndex = gridViewLines.Columns.Count - 1
            colDisc.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            colDisc.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
            colDisc.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            colDisc.Summary.Add(DevExpress.Data.SummaryItemType.Sum, "DiscountAmount")
            colDisc.SummaryItem.DisplayFormat = "{0:N2}"

            Dim colNet = gridViewLines.Columns.AddField("NetTotal")
            colNet.Caption = "Net"
            colNet.DisplayFormat.FormatType = FormatType.Numeric
            colNet.DisplayFormat.FormatString = "N2"
            colNet.VisibleIndex = gridViewLines.Columns.Count - 1
            colNet.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            colNet.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
            colNet.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            colNet.Summary.Add(DevExpress.Data.SummaryItemType.Sum, "NetTotal")
            colNet.SummaryItem.DisplayFormat = "{0:N2}"

            Dim colTax = gridViewLines.Columns.AddField("TaxAmount")
            colTax.Caption = "Tax"
            colTax.DisplayFormat.FormatType = FormatType.Numeric
            colTax.DisplayFormat.FormatString = "N2"
            colTax.VisibleIndex = gridViewLines.Columns.Count - 1
            colTax.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            colTax.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
            colTax.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            colTax.Summary.Add(DevExpress.Data.SummaryItemType.Sum, "TaxAmount")
            colTax.SummaryItem.DisplayFormat = "{0:N2}"

            Dim colTotal = gridViewLines.Columns.AddField("Total")
            colTotal.Caption = "Total"
            colTotal.DisplayFormat.FormatType = FormatType.Numeric
            colTotal.DisplayFormat.FormatString = "N2"
            colTotal.VisibleIndex = gridViewLines.Columns.Count - 1
            colTotal.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            colTotal.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
            colTotal.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            colTotal.Summary.Add(DevExpress.Data.SummaryItemType.Sum, "Total")
            colTotal.SummaryItem.DisplayFormat = "{0:N2}"

            gridViewLines.BestFitColumns()

            For Each l In _lines
                l.Recalculate()
            Next

            gridLines.DataSource = New List(Of InvoiceLineModel)(_lines)

            Dim totalAmount = _lines.Sum(Function(l) l.Total)
            Dim currencyCode = GetSelectedCurrencyCode()
            lblTotals.Text = $"Total: {totalAmount:N2} {currencyCode}   ({_lines.Count} line item(s))"
        End Sub

        Private Function BuildDocument() As EInvoiceDocument
            Dim settings = AppState.CurrentSettings
            If settings Is Nothing Then
                Throw New InvalidOperationException("Settings not loaded. Please log out and log back in.")
            End If

            Dim issuer As New PartyModel With {
                .Type = "B",
                .Id = settings.TaxpayerRIN,
                .Name = settings.TaxpayerName
            }
            issuer.Address.BranchID = If(String.IsNullOrWhiteSpace(settings.DefaultBranchId), "0", settings.DefaultBranchId)
            issuer.Address.Governate = settings.IssuerGovernate
            issuer.Address.RegionCity = settings.IssuerRegionCity
            issuer.Address.Street = settings.IssuerStreet
            issuer.Address.BuildingNumber = settings.IssuerBuildingNumber

            Dim docCode = GetSelectedDocumentTypeCode()
            Dim doc As New EInvoiceDocument With {
                .Issuer = issuer,
                .DocumentType = docCode.ToLowerInvariant(),
                .DocumentTypeVersion = "1.0",
                .InternalID = txtInternalId.Text.Trim(),
                .DateTimeIssued = dtDateIssued.DateTime.ToUniversalTime(),
                .TaxpayerActivityCode = If(String.IsNullOrWhiteSpace(settings.DefaultActivityCode), "", settings.DefaultActivityCode)
            }

            Dim receiverTypeCode = Convert.ToString(cmbReceiverType.SelectedItem).Substring(0, 1)
            doc.Receiver = New PartyModel With {
                .Type = receiverTypeCode,
                .Id = txtReceiverId.Text.Trim(),
                .Name = txtReceiverName.Text.Trim()
            }
            Dim receiverCountry = Convert.ToString(cmbReceiverCountry.SelectedItem)
            If receiverCountry.Contains(" - ") Then receiverCountry = receiverCountry.Split(New String() {" - "}, StringSplitOptions.None)(0)
            doc.Receiver.Address.Country = receiverCountry
            doc.Receiver.Address.Governate = txtReceiverGovernate.Text.Trim()
            doc.Receiver.Address.RegionCity = txtReceiverCity.Text.Trim()
            doc.Receiver.Address.Street = txtReceiverStreet.Text.Trim()
            doc.Receiver.Address.BuildingNumber = txtReceiverBuilding.Text.Trim()

            Dim paymentCode = Convert.ToString(cmbPaymentMethod.SelectedItem)
            If paymentCode.Contains(" - ") Then paymentCode = paymentCode.Split(New String() {" - "}, StringSplitOptions.None)(0)
            doc.Payment.PaymentMethod = paymentCode

            If (docCode = "C" OrElse docCode = "D" OrElse docCode = "EC" OrElse docCode = "ED") AndAlso Not String.IsNullOrWhiteSpace(txtReferenceUuid.Text) Then
                doc.References = New List(Of String) From {txtReferenceUuid.Text.Trim()}
            End If

            For Each l In _lines
                ApplyCurrencyToLine(l)
                doc.InvoiceLines.Add(l)
            Next

            doc.RecalculateTotals()
            Return doc
        End Function

        Private Sub btnValidate_Click(sender As Object, e As EventArgs) Handles btnValidate.Click
            Dim doc = BuildDocument()
            Dim validator As New DocumentValidationService()
            Dim errors = validator.Validate(doc)

            If errors.Count = 0 Then
                lblStatus.ForeColor = System.Drawing.Color.DarkGreen
                lblStatus.Text = "Document is valid and ready to sign and submit."
            Else
                lblStatus.ForeColor = System.Drawing.Color.DarkRed
                lblStatus.Text = "Validation issues:" & Environment.NewLine & String.Join(Environment.NewLine, errors)
            End If
        End Sub

        Private Sub btnCheckRejection_Click(sender As Object, e As EventArgs) Handles btnCheckRejection.Click
            Try
                Dim doc = BuildDocument()
                Dim service As New RejectionRiskService()
                Dim report = service.Assess(doc, AppState.CurrentSettings)
                lblStatus.ForeColor = If(report.RiskLevel = "High", System.Drawing.Color.Firebrick,
                                     If(report.RiskLevel = "Medium", System.Drawing.Color.DarkOrange, System.Drawing.Color.DarkGreen))
                lblStatus.Text = report.Summary
                Using dlg As New RejectionRiskDialog(report)
                    dlg.ShowDialog(Me)
                End Using
            Catch ex As Exception
                Dim appLog As New AppLogRepository()
                appLog.Error("InvoiceEntryForm.btnCheckRejection_Click", ex.ToString())
                Dim detail = ex.ToString()
                If detail.Length > 1200 Then detail = detail.Substring(0, 1200) & " ..."
                MessageBox.Show("Risk assessment failed: " & detail, "Rejection Risk", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End Try
        End Sub

        Private Async Sub btnSignAndSubmit_Click(sender As Object, e As EventArgs) Handles btnSignAndSubmit.Click
            Dim doc = BuildDocument()

            Dim confirmMsg = $"You are about to sign and submit this {GetTypeDisplayName()} to the " &
                              $"{AppState.CurrentSettings.Environment} environment. This action cannot be undone. Continue?"
            If MessageBox.Show(confirmMsg, "Confirm Submission", MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> DialogResult.Yes Then
                Return
            End If

            SetBusy(True)
            lblStatus.ForeColor = System.Drawing.Color.DarkSlateGray
            lblStatus.Text = "Signing document (check your USB token for a PIN prompt)..."

            Try
                Dim result = Await AppState.SubmissionService.SubmitSingleAsync(doc).ConfigureAwait(True)

                If result.ValidationErrors IsNot Nothing AndAlso result.ValidationErrors.Count > 0 Then
                    lblStatus.ForeColor = System.Drawing.Color.DarkRed
                    lblStatus.Text = "Validation issues:" & Environment.NewLine & String.Join(Environment.NewLine, result.ValidationErrors)
                    Return
                End If

                If result.Success Then
                    lblStatus.ForeColor = System.Drawing.Color.DarkGreen
                    lblStatus.Text = $"Submitted successfully. UUID: {result.Uuid}"
                    _lastSubmittedUuid = result.Uuid
                    _lastInternalId = doc.InternalID
                    btnDownloadPdf.Enabled = True
                Else
                    lblStatus.ForeColor = System.Drawing.Color.DarkRed
                    lblStatus.Text = result.ErrorMessage
                End If

            Catch ex As Exception
                lblStatus.ForeColor = System.Drawing.Color.DarkRed
                lblStatus.Text = "Unexpected error: " & ex.Message
            Finally
                SetBusy(False)
            End Try
        End Sub

        Private Async Sub btnDownloadPdf_Click(sender As Object, e As EventArgs) Handles btnDownloadPdf.Click
            If String.IsNullOrWhiteSpace(_lastSubmittedUuid) Then Return

            Using saveDialog As New SaveFileDialog With {
                .Filter = "PDF files (*.pdf)|*.pdf",
                .FileName = $"{_lastInternalId}.pdf"
            }
                If saveDialog.ShowDialog(Me) <> DialogResult.OK Then Return

                Try
                    lblStatus.Text = "Downloading PDF from portal..."
                    Dim pdfBytes = Await AppState.ApiClient.DownloadDocumentPdfAsync(_lastSubmittedUuid).ConfigureAwait(True)
                    _pdfService.SavePortalPdf(pdfBytes, saveDialog.FileName)
                    lblStatus.ForeColor = System.Drawing.Color.DarkGreen
                    lblStatus.Text = "PDF saved: " & saveDialog.FileName
                Catch ex As Exception
                    Try
                        Dim doc = BuildDocument()
                        _pdfService.GenerateDocumentPdf(doc, saveDialog.FileName, _lastSubmittedUuid)
                        lblStatus.ForeColor = System.Drawing.Color.DarkOrange
                        lblStatus.Text = "Portal PDF unavailable; generated a local printout instead: " & saveDialog.FileName
                    Catch ex2 As Exception
                        lblStatus.ForeColor = System.Drawing.Color.DarkRed
                        lblStatus.Text = "Failed to generate PDF: " & ex2.Message
                    End Try
                End Try
            End Using
        End Sub

        Private Sub SetBusy(busy As Boolean)
            btnValidate.Enabled = Not busy
            btnSignAndSubmit.Enabled = Not busy
            btnAddLine.Enabled = Not busy
            btnEditLine.Enabled = Not busy
            btnRemoveLine.Enabled = Not busy
            btnPickFromEgs.Enabled = Not busy
            Me.Cursor = If(busy, Cursors.WaitCursor, Cursors.Default)
        End Sub

    End Class

End Namespace
