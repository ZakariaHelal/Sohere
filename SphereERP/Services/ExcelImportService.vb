Option Strict On
Imports System.Globalization
Imports System.IO
Imports OfficeOpenXml
Imports SphereERP.Models

Namespace Services

    ''' <summary>
    ''' Represents one invoice header parsed from the Excel/CSV import, together with
    ''' its line items, for preview (totals) and drill-down (breakup) before submission.
    ''' </summary>
    Public Class ImportedInvoiceGroup
        Public Property InternalId As String
        Public Property ReceiverType As String
        Public Property ReceiverId As String
        Public Property ReceiverName As String
        Public Property ReceiverGovernate As String
        Public Property ReceiverRegionCity As String
        Public Property ReceiverStreet As String
        Public Property ReceiverBuildingNumber As String
        Public Property ReceiverCountry As String
        Public Property DocumentType As String ' I, C, D
        Public Property DateTimeIssued As DateTime
        Public Property PaymentMethod As String
        Public Property ReferenceUuid As String ' for credit/debit notes
        Public Property Lines As List(Of ImportedInvoiceLine)
        Public Property ValidationErrors As List(Of String)

        Public ReadOnly Property TotalAmount As Decimal
            Get
                Return Math.Round(Lines.Sum(Function(l) l.Total), 2)
            End Get
        End Property

        Public ReadOnly Property NetAmount As Decimal
            Get
                Return Math.Round(Lines.Sum(Function(l) l.NetTotal), 2)
            End Get
        End Property

        Public ReadOnly Property LineCount As Integer
            Get
                Return Lines.Count
            End Get
        End Property

        Public ReadOnly Property IsValid As Boolean
            Get
                Return ValidationErrors.Count = 0
            End Get
        End Property

        Public Sub New()
            InternalId = ""
            ReceiverType = "B"
            ReceiverId = ""
            ReceiverName = ""
            ReceiverGovernate = ""
            ReceiverRegionCity = ""
            ReceiverStreet = ""
            ReceiverBuildingNumber = ""
            ReceiverCountry = "EG"
            DocumentType = "I"
            DateTimeIssued = DateTime.UtcNow
            PaymentMethod = "C"
            ReferenceUuid = ""
            Lines = New List(Of ImportedInvoiceLine)()
            ValidationErrors = New List(Of String)()
        End Sub
    End Class

    Public Class ImportedInvoiceLine
        Public Property Description As String
        Public Property ItemCode As String
        Public Property ItemType As String
        Public Property UnitType As String
        Public Property Quantity As Decimal
        Public Property UnitPrice As Decimal
        Public Property DiscountAmount As Decimal
        Public Property TaxType As String
        Public Property TaxSubType As String
        Public Property TaxRate As Decimal
        Public Property CurrencySold As String
        Public Property AmountSold As Decimal
        Public Property CurrencyExchangeRate As Decimal

        Public ReadOnly Property SalesTotal As Decimal
            Get
                Return Math.Round(Quantity * UnitPrice, 2)
            End Get
        End Property

        Public ReadOnly Property NetTotal As Decimal
            Get
                Return Math.Round(SalesTotal - DiscountAmount, 2)
            End Get
        End Property

        Public ReadOnly Property TaxAmount As Decimal
            Get
                Return Math.Round(NetTotal * (TaxRate / 100D), 2)
            End Get
        End Property

        Public ReadOnly Property Total As Decimal
            Get
                Return Math.Round(NetTotal + TaxAmount, 2)
            End Get
        End Property

        Public Sub New()
            Description = ""
            ItemCode = ""
            ItemType = "EGS"
            UnitType = "EA"
            Quantity = 1D
            UnitPrice = 0D
            DiscountAmount = 0D
            TaxType = "T1"
            TaxSubType = ""
            TaxRate = 14D
            CurrencySold = "EGP"
            AmountSold = 0D
            CurrencyExchangeRate = 0D
        End Sub
    End Class

    ''' <summary>
    ''' Parses an Excel (.xlsx) or CSV file of invoice lines into grouped ImportedInvoiceGroup
    ''' objects (one per InternalId), ready for preview and submission. Expects a flat
    ''' one-row-per-line-item layout with a header row matching ExpectedColumns.
    ''' </summary>
    Public Class ExcelImportService

        Public Shared ReadOnly ExpectedColumns As String() = {
            "InternalId", "DocumentType", "DateTimeIssued",
            "ReceiverType", "ReceiverId", "ReceiverName",
            "ReceiverGovernate", "ReceiverRegionCity", "ReceiverStreet", "ReceiverBuildingNumber", "ReceiverCountry",
            "PaymentMethod", "ReferenceUuid",
            "ItemDescription", "ItemCode", "ItemType", "UnitType",
            "Quantity", "UnitPrice", "DiscountAmount", "TaxType", "TaxSubType", "TaxRate",
            "CurrencySold", "AmountSold", "CurrencyExchangeRate"
        }

        Public Sub New()
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial
        End Sub

        ''' <summary>
        ''' Generates a blank template workbook with the expected headers and one example row,
        ''' so users can fill it in correctly.
        ''' </summary>
        Public Sub GenerateTemplate(outputPath As String)
            Using package As New ExcelPackage()
                Dim sheet = package.Workbook.Worksheets.Add("Invoices")
                For i = 0 To ExpectedColumns.Length - 1
                    sheet.Cells(1, i + 1).Value = ExpectedColumns(i)
                    sheet.Cells(1, i + 1).Style.Font.Bold = True
                Next

                Dim exampleRow As Object() = {
                    "INV-0001", "I", DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ"),
                    "B", "200000000", "Sample Customer Co.",
                    "Cairo", "Nasr City", "10 Abbas El Akkad St", "10", "EG",
                    "C", "",
                    "Office Chair", "EG-1001", "EGS", "EA",
                    "2", "1500", "0", "T1", "V009", "14",
                    "EGP", "", ""
                }
                For i = 0 To exampleRow.Length - 1
                    sheet.Cells(2, i + 1).Value = exampleRow(i)
                Next

                sheet.Cells.AutoFitColumns()
                package.SaveAs(New FileInfo(outputPath))
            End Using
        End Sub

        ''' <summary>
        ''' Reads the given .xlsx file and groups rows by InternalId into invoice groups
        ''' with line-item breakup, applying basic per-row validation.
        ''' </summary>
        Public Function ParseExcelFile(filePath As String) As List(Of ImportedInvoiceGroup)
            Using package As New ExcelPackage(New FileInfo(filePath))
                Dim sheet = package.Workbook.Worksheets(0)
                Return ParseWorksheet(sheet)
            End Using
        End Function

        ''' <summary>Reads a CSV file with the same expected column layout.</summary>
        Public Function ParseCsvFile(filePath As String) As List(Of ImportedInvoiceGroup)
            Dim lines = File.ReadAllLines(filePath)
            If lines.Length < 2 Then Return New List(Of ImportedInvoiceGroup)()

            Dim headers = SplitCsvLine(lines(0))
            Dim columnIndex As New Dictionary(Of String, Integer)(StringComparer.OrdinalIgnoreCase)
            For i = 0 To headers.Length - 1
                columnIndex(headers(i).Trim()) = i
            Next

            Dim groups As New Dictionary(Of String, ImportedInvoiceGroup)(StringComparer.OrdinalIgnoreCase)
            Dim order As New List(Of String)()

            For rowIdx = 1 To lines.Length - 1
                If String.IsNullOrWhiteSpace(lines(rowIdx)) Then Continue For
                Dim cells = SplitCsvLine(lines(rowIdx))
                ProcessRow(cells, columnIndex, groups, order, rowIdx + 1)
            Next

            Return order.Select(Function(k) groups(k)).ToList()
        End Function

        Private Function ParseWorksheet(sheet As ExcelWorksheet) As List(Of ImportedInvoiceGroup)
            Dim columnIndex As New Dictionary(Of String, Integer)(StringComparer.OrdinalIgnoreCase)
            Dim colCount = sheet.Dimension?.End.Column
            If colCount Is Nothing Then Return New List(Of ImportedInvoiceGroup)()

            For c = 1 To colCount.Value
                Dim header = Convert.ToString(sheet.Cells(1, c).Value)
                If Not String.IsNullOrWhiteSpace(header) Then
                    columnIndex(header.Trim()) = c
                End If
            Next

            Dim groups As New Dictionary(Of String, ImportedInvoiceGroup)(StringComparer.OrdinalIgnoreCase)
            Dim order As New List(Of String)()

            Dim rowCount = sheet.Dimension.End.Row
            For r = 2 To rowCount
                Dim cells(colCount.Value - 1) As String
                For c = 1 To colCount.Value
                    cells(c - 1) = Convert.ToString(sheet.Cells(r, c).Value)
                Next
                ProcessRow(cells, columnIndex, groups, order, r)
            Next

            Return order.Select(Function(k) groups(k)).ToList()
        End Function

        Private Sub ProcessRow(cells As String(), columnIndex As Dictionary(Of String, Integer),
                                groups As Dictionary(Of String, ImportedInvoiceGroup), order As List(Of String), rowNumber As Integer)

            Dim GetVal = Function(colName As String) As String
                              If Not columnIndex.ContainsKey(colName) Then Return ""
                              Dim idx = columnIndex(colName) - 1
                              If idx < 0 OrElse idx >= cells.Length Then Return ""
                              Return If(cells(idx), "").Trim()
                          End Function

            Dim internalId = GetVal("InternalId")
            If String.IsNullOrWhiteSpace(internalId) Then Return ' skip blank rows

            If Not groups.ContainsKey(internalId) Then
                Dim grp As New ImportedInvoiceGroup With {
                    .InternalId = internalId,
                    .DocumentType = If(String.IsNullOrWhiteSpace(GetVal("DocumentType")), "I", GetVal("DocumentType").ToUpperInvariant()),
                    .ReceiverType = If(String.IsNullOrWhiteSpace(GetVal("ReceiverType")), "B", GetVal("ReceiverType").ToUpperInvariant()),
                    .ReceiverId = GetVal("ReceiverId"),
                    .ReceiverName = GetVal("ReceiverName"),
                    .ReceiverGovernate = GetVal("ReceiverGovernate"),
                    .ReceiverRegionCity = GetVal("ReceiverRegionCity"),
                    .ReceiverStreet = GetVal("ReceiverStreet"),
                    .ReceiverBuildingNumber = GetVal("ReceiverBuildingNumber"),
                    .ReceiverCountry = If(String.IsNullOrWhiteSpace(GetVal("ReceiverCountry")), "EG", GetVal("ReceiverCountry").ToUpperInvariant()),
                    .PaymentMethod = If(String.IsNullOrWhiteSpace(GetVal("PaymentMethod")), "C", GetVal("PaymentMethod").ToUpperInvariant()),
                    .ReferenceUuid = GetVal("ReferenceUuid")
                }

                Dim dateStr = GetVal("DateTimeIssued")
                Dim parsedDate As DateTime
                If DateTime.TryParse(dateStr, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal Or DateTimeStyles.AdjustToUniversal, parsedDate) Then
                    grp.DateTimeIssued = parsedDate
                Else
                    grp.DateTimeIssued = DateTime.UtcNow
                End If

                groups(internalId) = grp
                order.Add(internalId)
            End If

            Dim group = groups(internalId)

            Dim line As New ImportedInvoiceLine With {
                .Description = GetVal("ItemDescription"),
                .ItemCode = GetVal("ItemCode"),
                .ItemType = If(String.IsNullOrWhiteSpace(GetVal("ItemType")), "EGS", GetVal("ItemType").ToUpperInvariant()),
                .UnitType = If(String.IsNullOrWhiteSpace(GetVal("UnitType")), "EA", GetVal("UnitType").ToUpperInvariant()),
                .TaxType = If(String.IsNullOrWhiteSpace(GetVal("TaxType")), "T1", GetVal("TaxType").ToUpperInvariant()),
                .TaxSubType = GetVal("TaxSubType")
            }

            Dim qty As Decimal, price As Decimal, disc As Decimal, rate As Decimal
            Dim amtSold As Decimal, exchRate As Decimal
            Decimal.TryParse(GetVal("Quantity"), qty)
            Decimal.TryParse(GetVal("UnitPrice"), price)
            Decimal.TryParse(GetVal("DiscountAmount"), disc)
            Decimal.TryParse(GetVal("TaxRate"), rate)
            Decimal.TryParse(GetVal("AmountSold"), amtSold)
            Decimal.TryParse(GetVal("CurrencyExchangeRate"), exchRate)

            line.Quantity = If(qty > 0, qty, 1D)
            line.UnitPrice = price
            line.DiscountAmount = disc
            line.TaxRate = rate
            line.CurrencySold = If(String.IsNullOrWhiteSpace(GetVal("CurrencySold")), "EGP", GetVal("CurrencySold").ToUpperInvariant())
            line.AmountSold = amtSold
            line.CurrencyExchangeRate = exchRate

            ' Row-level validation
            If String.IsNullOrWhiteSpace(line.Description) Then
                group.ValidationErrors.Add($"Row {rowNumber}: item description is missing.")
            End If
            If String.IsNullOrWhiteSpace(line.ItemCode) Then
                group.ValidationErrors.Add($"Row {rowNumber}: item code (EGS) is missing.")
            End If
            If line.Quantity <= 0 Then
                group.ValidationErrors.Add($"Row {rowNumber}: quantity must be greater than zero.")
            End If
            If line.UnitPrice < 0 Then
                group.ValidationErrors.Add($"Row {rowNumber}: unit price cannot be negative.")
            End If

            group.Lines.Add(line)
        End Sub

        ''' <summary>Minimal CSV line splitter handling quoted fields with embedded commas.</summary>
        Private Function SplitCsvLine(line As String) As String()
            Dim result As New List(Of String)()
            Dim sb As New Text.StringBuilder()
            Dim inQuotes = False

            For Each ch In line
                If ch = """"c Then
                    inQuotes = Not inQuotes
                ElseIf ch = ","c AndAlso Not inQuotes Then
                    result.Add(sb.ToString())
                    sb.Clear()
                Else
                    sb.Append(ch)
                End If
            Next
            result.Add(sb.ToString())
            Return result.ToArray()
        End Function

        ''' <summary>Converts an ImportedInvoiceGroup (with its raw lines) into a full EInvoiceDocument using the given EGS code library for lookups, ready for validation/signing/submission.</summary>
        Public Function ConvertToDocument(group As ImportedInvoiceGroup, issuer As PartyModel, taxpayerActivityCode As String) As EInvoiceDocument
            Dim doc As New EInvoiceDocument With {
                .Issuer = issuer,
                .DocumentType = group.DocumentType,
                .InternalID = group.InternalId,
                .DateTimeIssued = group.DateTimeIssued,
                .TaxpayerActivityCode = taxpayerActivityCode
            }

            doc.Receiver = New PartyModel With {
                .Type = group.ReceiverType,
                .Id = group.ReceiverId,
                .Name = group.ReceiverName
            }
            doc.Receiver.Address.Country = group.ReceiverCountry
            doc.Receiver.Address.Governate = group.ReceiverGovernate
            doc.Receiver.Address.RegionCity = group.ReceiverRegionCity
            doc.Receiver.Address.Street = group.ReceiverStreet
            doc.Receiver.Address.BuildingNumber = group.ReceiverBuildingNumber

            doc.Payment.PaymentMethod = group.PaymentMethod

            If (group.DocumentType = "C" OrElse group.DocumentType = "D") AndAlso Not String.IsNullOrWhiteSpace(group.ReferenceUuid) Then
                doc.References = New List(Of String) From {group.ReferenceUuid}
            End If

            For Each importedLine In group.Lines
                Dim docLine As New InvoiceLineModel With {
                    .Description = importedLine.Description,
                    .ItemCode = importedLine.ItemCode,
                    .ItemType = importedLine.ItemType,
                    .UnitType = importedLine.UnitType,
                    .Quantity = importedLine.Quantity
                }
                If importedLine.CurrencySold = "EGP" OrElse String.IsNullOrWhiteSpace(importedLine.CurrencySold) Then
                    docLine.UnitValue.AmountEGP = importedLine.UnitPrice
                    docLine.UnitValue.CurrencySold = "EGP"
                Else
                    docLine.UnitValue.CurrencySold = importedLine.CurrencySold
                    docLine.UnitValue.AmountSold = If(importedLine.AmountSold <> 0D, importedLine.AmountSold, importedLine.UnitPrice)
                    docLine.UnitValue.CurrencyExchangeRate = If(importedLine.CurrencyExchangeRate <> 0D, importedLine.CurrencyExchangeRate, 1D)
                    docLine.UnitValue.AmountEGP = Math.Round(docLine.UnitValue.AmountSold * docLine.UnitValue.CurrencyExchangeRate, 5)
                End If
                docLine.Discount.Amount = importedLine.DiscountAmount
                Dim subType = If(String.IsNullOrWhiteSpace(importedLine.TaxSubType),
                    TaxItemModel.DefaultSubType(importedLine.TaxType),
                    importedLine.TaxSubType)
                docLine.TaxableItems.Add(New TaxItemModel With {
                    .TaxType = importedLine.TaxType,
                    .SubType = subType,
                    .Rate = importedLine.TaxRate
                })
                doc.InvoiceLines.Add(docLine)
            Next

            doc.RecalculateTotals()
            Return doc
        End Function

    End Class

End Namespace
