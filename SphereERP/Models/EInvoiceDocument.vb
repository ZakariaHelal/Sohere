Option Strict On
Imports Newtonsoft.Json

Namespace Models

    ''' <summary>
    ''' Document types as defined by ETA e-Invoicing/e-Receipt SDK.
    ''' Use .ToString().ToLowerInvariant() for submission to match the required lowercase values.
    ''' </summary>
    Public Enum DocumentTypeEnum
        I = 0     ' Invoice       → "i"
        C = 1     ' Credit Note   → "c"
        D = 2     ' Debit Note    → "d"
        EI = 3    ' Export Invoice → "ei"
        EC = 4    ' Export Credit Note → "ec"
        ED = 5    ' Export Debit Note  → "ed"
    End Enum

    ''' <summary>
    ''' Document status values as returned/used by ETA portal.
    ''' </summary>
    Public Enum DocumentStatusEnum
        Valid
        Invalid
        Submitted
        Rejected
        Cancelled
    End Enum

    ''' <summary>
    ''' ETA activity codes are numeric ISIC-based codes (e.g. "0111", "4711", "6201").
    ''' See https://sdk.invoicing.eta.gov.eg/codes/activity-types/ for the full list.
    ''' Set via AppSettings.DefaultActivityCode.
    ''' </summary>
    ''' <remarks>
    ''' Previous versions of this codebase defined a B2B/B2C/B2G enum here, but ETA
    ''' requires numeric activity codes from the code table linked above.
    ''' </remarks>

    ''' <summary>
    ''' Represents one issuer or receiver party.
    ''' </summary>
    Public Class PartyModel
        <JsonProperty("type")>
        Public Property Type As String ' B, P, F (Business, Person, Foreign)

        <JsonProperty("id")>
        Public Property Id As String ' Tax registration number / National ID

        <JsonProperty("name")>
        Public Property Name As String

        <JsonProperty("address")>
        Public Property Address As AddressModel

        Public Sub New()
            Address = New AddressModel()
            Type = "B"
            Id = ""
            Name = ""
        End Sub
    End Class

    Public Class AddressModel
        <JsonProperty("branchID")>
        Public Property BranchID As String

        <JsonProperty("country")>
        Public Property Country As String

        <JsonProperty("governate")>
        Public Property Governate As String

        <JsonProperty("regionCity")>
        Public Property RegionCity As String

        <JsonProperty("street")>
        Public Property Street As String

        <JsonProperty("buildingNumber")>
        Public Property BuildingNumber As String

        <JsonProperty("postalCode")>
        Public Property PostalCode As String

        <JsonProperty("floor")>
        Public Property Floor As String

        <JsonProperty("room")>
        Public Property Room As String

        <JsonProperty("landmark")>
        Public Property Landmark As String

        <JsonProperty("additionalInformation")>
        Public Property AdditionalInformation As String

        Public Sub New()
            BranchID = "0"
            Country = "EG"
            Governate = ""
            RegionCity = ""
            Street = ""
            BuildingNumber = ""
            PostalCode = Nothing
            Floor = Nothing
            Room = Nothing
            Landmark = Nothing
            AdditionalInformation = Nothing
        End Sub

        Public Function ShouldSerializePostalCode() As Boolean
            Return Not String.IsNullOrEmpty(PostalCode)
        End Function
        Public Function ShouldSerializeFloor() As Boolean
            Return Not String.IsNullOrEmpty(Floor)
        End Function
        Public Function ShouldSerializeRoom() As Boolean
            Return Not String.IsNullOrEmpty(Room)
        End Function
        Public Function ShouldSerializeLandmark() As Boolean
            Return Not String.IsNullOrEmpty(Landmark)
        End Function
        Public Function ShouldSerializeAdditionalInformation() As Boolean
            Return Not String.IsNullOrEmpty(AdditionalInformation)
        End Function
    End Class

    ''' <summary>
    ''' Document-level tax total (only taxType + amount per ETA Invoice v1.0 schema).
    ''' </summary>
    Public Class TaxTotalModel
        <JsonProperty("taxType")>
        Public Property TaxType As String

        <JsonProperty("amount")>
        Public Property Amount As Decimal
    End Class

    ''' <summary>
    ''' One tax applied to a line item (T1 VAT, T2 Table Tax, etc.)
    ''' </summary>
    Public Class TaxItemModel
        <JsonProperty("taxType")>
        Public Property TaxType As String ' e.g. T1, T2, T3...

        <JsonProperty("amount", NullValueHandling:=NullValueHandling.Ignore)>
        Public Property Amount As Decimal

        <JsonProperty("subType")>
        Public Property SubType As String

        <JsonProperty("rate", NullValueHandling:=NullValueHandling.Ignore)>
        Public Property Rate As Decimal

        Public Sub New()
            TaxType = "T1"
            SubType = ""
            Amount = 0D
            Rate = 0D
        End Sub

        ''' <summary>Returns the default subtype for a given tax type per ETA code tables.</summary>
        Public Shared Function DefaultSubType(taxType As String) As String
            Select Case taxType
                Case "T1" : Return "V009"
                Case "T2" : Return "Tbl01"
                Case "T3" : Return "Tbl02"
                Case "T4" : Return "W004"
                Case "T5" : Return "ST01"
                Case "T6" : Return "ST02"
                Case "T7" : Return "Ent01"
                Case "T8" : Return "RD01"
                Case "T9" : Return "SC01"
                Case "T10" : Return "Mn01"
                Case "T11" : Return "MI01"
                Case "T12" : Return "OF01"
                Case "T13" : Return "ST03"
                Case "T14" : Return "ST04"
                Case "T15" : Return "Ent03"
                Case "T16" : Return "RD03"
                Case "T17" : Return "SC03"
                Case "T18" : Return "Mn03"
                Case "T19" : Return "MI03"
                Case "T20" : Return "OF03"
                Case Else : Return ""
            End Select
        End Function
    End Class

    ''' <summary>
    ''' One discount or additional value at the line level.
    ''' </summary>
    Public Class CommercialDiscountModel
        <JsonProperty("amount", NullValueHandling:=NullValueHandling.Ignore)>
        Public Property Amount As Decimal

        <JsonIgnore>
        Public Property Description As String

        <JsonProperty("rate", NullValueHandling:=NullValueHandling.Ignore)>
        Public Property Rate As Decimal

        Public Sub New()
            Amount = 0D
            Description = ""
            Rate = 0D
        End Sub
    End Class

    ''' <summary>
    ''' Represents a single invoice line item.
    ''' </summary>
    Public Class InvoiceLineModel
        <JsonProperty("description")>
        Public Property Description As String

        <JsonProperty("itemType")>
        Public Property ItemType As String ' GS1, EGS, GPC

        <JsonProperty("itemCode")>
        Public Property ItemCode As String ' EGS code

        <JsonProperty("unitType")>
        Public Property UnitType As String ' EA, KGM, etc.

        <JsonProperty("quantity", NullValueHandling:=NullValueHandling.Ignore)>
        Public Property Quantity As Decimal

        <JsonProperty("internalCode")>
        Public Property InternalCode As String

        <JsonProperty("salesTotal", NullValueHandling:=NullValueHandling.Ignore)>
        Public Property SalesTotal As Decimal

        <JsonProperty("total", NullValueHandling:=NullValueHandling.Ignore)>
        Public Property Total As Decimal

        <JsonProperty("valueDifference", NullValueHandling:=NullValueHandling.Ignore)>
        Public Property ValueDifference As Decimal

        <JsonProperty("totalTaxableFees", NullValueHandling:=NullValueHandling.Ignore)>
        Public Property TotalTaxableFees As Decimal

        <JsonProperty("netTotal", NullValueHandling:=NullValueHandling.Ignore)>
        Public Property NetTotal As Decimal

        <JsonProperty("itemsDiscount", NullValueHandling:=NullValueHandling.Ignore)>
        Public Property ItemsDiscount As Decimal

        <JsonProperty("unitValue")>
        Public Property UnitValue As UnitValueModel

        <JsonProperty("discount")>
        Public Property Discount As CommercialDiscountModel

        <JsonProperty("taxableItems")>
        Public Property TaxableItems As List(Of TaxItemModel)

        <JsonIgnore>
        Public ReadOnly Property UnitPriceAmount As Decimal
            Get
                Return If(UnitValue Is Nothing, 0D, UnitValue.AmountEGP)
            End Get
        End Property

        <JsonIgnore>
        Public ReadOnly Property DiscountAmount As Decimal
            Get
                Return If(Discount Is Nothing, 0D, Discount.Amount)
            End Get
        End Property

        <JsonIgnore>
        Public ReadOnly Property TaxAmount As Decimal
            Get
                If TaxableItems Is Nothing Then Return 0D
                Return TaxableItems.Sum(Function(t) t.Amount)
            End Get
        End Property

        Public Sub New()
            Description = ""
            ItemType = "EGS"
            ItemCode = ""
            UnitType = "EA"
            Quantity = 1D
            InternalCode = ""
            SalesTotal = 0D
            Total = 0D
            ValueDifference = 0D
            TotalTaxableFees = 0D
            NetTotal = 0D
            ItemsDiscount = 0D
            UnitValue = New UnitValueModel()
            Discount = New CommercialDiscountModel()
            TaxableItems = New List(Of TaxItemModel)()
        End Sub

        Public Function ShouldSerializeValueDifference() As Boolean
            Return ValueDifference <> 0D
        End Function
        Public Function ShouldSerializeTotalTaxableFees() As Boolean
            Return TotalTaxableFees <> 0D
        End Function
        Public Function ShouldSerializeItemsDiscount() As Boolean
            Return ItemsDiscount <> 0D
        End Function
        Public Function ShouldSerializeInternalCode() As Boolean
            Return Not String.IsNullOrEmpty(InternalCode)
        End Function
        Public Function ShouldSerializeDiscount() As Boolean
            Return Discount IsNot Nothing
        End Function

        ''' <summary>
        ''' Recalculates Total/Net/Sales values based on UnitValue, Quantity, Discount and Taxes.
        ''' </summary>
        Public Sub Recalculate()
            Dim gross As Decimal = UnitValue.Amount * Quantity
            SalesTotal = Math.Round(gross, 5)
            Dim discAmt As Decimal = Discount.Amount
            If Discount.Rate > 0 Then
                discAmt = Math.Round(gross * (Discount.Rate / 100D), 5)
                Discount.Amount = discAmt
            End If
            ItemsDiscount = discAmt
            NetTotal = Math.Round(SalesTotal - ItemsDiscount + ValueDifference, 5)

            Dim taxTotal As Decimal = 0D
            For Each t In TaxableItems
                If t.Rate > 0 Then
                    t.Amount = Math.Round(NetTotal * (t.Rate / 100D), 5)
                End If
                If t.TaxType = "T1" Then
                    taxTotal += t.Amount
                ElseIf t.TaxType.StartsWith("T") AndAlso t.TaxType <> "T1" Then
                    ' Table tax / other taxes added on top
                    taxTotal += t.Amount
                End If
            Next

            Total = Math.Round(NetTotal + taxTotal + TotalTaxableFees, 5)
        End Sub
    End Class

    Public Class UnitValueModel
        <JsonProperty("currencySold")>
        Public Property CurrencySold As String

        <JsonProperty("amountEGP", NullValueHandling:=NullValueHandling.Ignore)>
        Public Property AmountEGP As Decimal

        <JsonProperty("amountSold", NullValueHandling:=NullValueHandling.Ignore)>
        Public Property AmountSold As Decimal

        <JsonProperty("currencyExchangeRate", NullValueHandling:=NullValueHandling.Ignore)>
        Public Property CurrencyExchangeRate As Decimal

        ''' <summary>Convenience: the value used in calculations (EGP).</summary>
        <JsonIgnore>
        Public Property Amount As Decimal
            Get
                Return AmountEGP
            End Get
            Set(value As Decimal)
                AmountEGP = value
            End Set
        End Property

        Public Sub New()
            CurrencySold = "EGP"
            AmountEGP = 0D
            AmountSold = 0D
            CurrencyExchangeRate = 1D
        End Sub

        Public Function ShouldSerializeAmountSold() As Boolean
            Return AmountSold <> 0D
        End Function
        Public Function ShouldSerializeCurrencyExchangeRate() As Boolean
            Return CurrencyExchangeRate <> 1D
        End Function
    End Class

    ''' <summary>
    ''' Reference to a previous document (used for Credit/Debit notes).
    ''' </summary>
    Public Class DocumentReferenceModel
        <JsonProperty("uuid")>
        Public Property Uuid As String

        <JsonProperty("internalId")>
        Public Property InternalId As String

        <JsonProperty("issueDate")>
        Public Property IssueDate As Date?

        Public Sub New()
            Uuid = ""
            InternalId = ""
            IssueDate = Nothing
        End Sub
    End Class

    Public Class SignatureModel
        <JsonProperty("type")>
        Public Property Type As String

        <JsonProperty("value")>
        Public Property Value As String

        Public Sub New()
            Type = "I"
            Value = ""
        End Sub
    End Class

    ''' <summary>
    ''' Full ETA document (invoice / credit note / debit note) as submitted to the API.
    ''' </summary>
    Public Class EInvoiceDocument
        <JsonProperty("issuer")>
        Public Property Issuer As PartyModel

        <JsonProperty("receiver")>
        Public Property Receiver As PartyModel

        <JsonProperty("documentType")>
        Public Property DocumentType As String ' I, C, D

        <JsonProperty("documentTypeVersion")>
        Public Property DocumentTypeVersion As String

        <JsonProperty("dateTimeIssued")>
        Public Property DateTimeIssued As DateTime

        <JsonProperty("taxpayerActivityCode")>
        Public Property TaxpayerActivityCode As String

        <JsonProperty("internalID")>
        Public Property InternalID As String

        <JsonProperty("purchaseOrderReference")>
        Public Property PurchaseOrderReference As String

        <JsonProperty("purchaseOrderDescription")>
        Public Property PurchaseOrderDescription As String

        <JsonProperty("salesOrderReference")>
        Public Property SalesOrderReference As String

        <JsonProperty("salesOrderDescription")>
        Public Property SalesOrderDescription As String

        <JsonProperty("proformaInvoiceNumber")>
        Public Property ProformaInvoiceNumber As String

        <JsonProperty("payment")>
        Public Property Payment As PaymentModel

        Public Function ShouldSerializePayment() As Boolean
            Return Payment IsNot Nothing AndAlso Payment.HasBankDetails
        End Function

        <JsonProperty("delivery")>
        Public Property Delivery As DeliveryModel

        <JsonProperty("invoiceLines")>
        Public Property InvoiceLines As List(Of InvoiceLineModel)

        <JsonProperty("totalSalesAmount", NullValueHandling:=NullValueHandling.Ignore)>
        Public Property TotalSalesAmount As Decimal

        <JsonProperty("totalDiscountAmount", NullValueHandling:=NullValueHandling.Ignore)>
        Public Property TotalDiscountAmount As Decimal

        <JsonProperty("netAmount", NullValueHandling:=NullValueHandling.Ignore)>
        Public Property NetAmount As Decimal

        <JsonProperty("taxTotals")>
        Public Property TaxTotals As List(Of TaxTotalModel)

        <JsonProperty("totalAmount", NullValueHandling:=NullValueHandling.Ignore)>
        Public Property TotalAmount As Decimal

        <JsonProperty("extraDiscountAmount", NullValueHandling:=NullValueHandling.Ignore)>
        Public Property ExtraDiscountAmount As Decimal

        <JsonProperty("totalItemsDiscountAmount", NullValueHandling:=NullValueHandling.Ignore)>
        Public Property TotalItemsDiscountAmount As Decimal

        <JsonProperty("references", NullValueHandling:=NullValueHandling.Ignore)>
        Public Property References As List(Of String)

        <JsonProperty("signatures")>
        Public Property Signatures As List(Of SignatureModel)

        Public Sub New()
            Issuer = New PartyModel()
            Receiver = New PartyModel()
            DocumentType = "I"
            DocumentTypeVersion = "1.0"
            DateTimeIssued = New DateTime(DateTime.UtcNow.Ticks - (DateTime.UtcNow.Ticks Mod TimeSpan.TicksPerSecond), DateTimeKind.Utc)
            TaxpayerActivityCode = ""
            InternalID = ""
            PurchaseOrderReference = Nothing
            PurchaseOrderDescription = Nothing
            SalesOrderReference = Nothing
            SalesOrderDescription = Nothing
            ProformaInvoiceNumber = Nothing
            Payment = New PaymentModel()
            Delivery = New DeliveryModel()
            InvoiceLines = New List(Of InvoiceLineModel)()
            TaxTotals = New List(Of TaxTotalModel)()
            Signatures = New List(Of SignatureModel)()
            References = New List(Of String)()
            TotalSalesAmount = 0D
            TotalDiscountAmount = 0D
            NetAmount = 0D
            TotalAmount = 0D
            ExtraDiscountAmount = 0D
            TotalItemsDiscountAmount = 0D
        End Sub

        Public Function ShouldSerializePurchaseOrderReference() As Boolean
            Return Not String.IsNullOrEmpty(PurchaseOrderReference)
        End Function
        Public Function ShouldSerializePurchaseOrderDescription() As Boolean
            Return Not String.IsNullOrEmpty(PurchaseOrderDescription)
        End Function
        Public Function ShouldSerializeSalesOrderReference() As Boolean
            Return Not String.IsNullOrEmpty(SalesOrderReference)
        End Function
        Public Function ShouldSerializeSalesOrderDescription() As Boolean
            Return Not String.IsNullOrEmpty(SalesOrderDescription)
        End Function
        Public Function ShouldSerializeProformaInvoiceNumber() As Boolean
            Return Not String.IsNullOrEmpty(ProformaInvoiceNumber)
        End Function
        Public Function ShouldSerializeReferences() As Boolean
            Return References IsNot Nothing AndAlso References.Count > 0
        End Function
        Public Function ShouldSerializeDelivery() As Boolean
            Return Delivery IsNot Nothing AndAlso
                   (Not String.IsNullOrEmpty(Delivery.Approach) OrElse
                    Not String.IsNullOrEmpty(Delivery.Packaging) OrElse
                    Delivery.DateValidity.HasValue OrElse
                    Not String.IsNullOrEmpty(Delivery.ExportPort) OrElse
                    Delivery.GrossWeight > 0D OrElse
                    Delivery.NetWeight > 0D OrElse
                    Not String.IsNullOrEmpty(Delivery.Terms) OrElse
                    Not String.IsNullOrEmpty(Delivery.CountryOfOrigin))
        End Function

        ''' <summary>
        ''' Recalculates document-level totals from line items.
        ''' </summary>
        Public Sub RecalculateTotals()
            For Each line In InvoiceLines
                line.Recalculate()
            Next

            TotalSalesAmount = Math.Round(InvoiceLines.Sum(Function(l) l.SalesTotal), 5)
            TotalItemsDiscountAmount = Math.Round(InvoiceLines.Sum(Function(l) l.ItemsDiscount), 5)
            TotalDiscountAmount = Math.Round(TotalItemsDiscountAmount + ExtraDiscountAmount, 5)
            NetAmount = Math.Round(InvoiceLines.Sum(Function(l) l.NetTotal), 5)

            ' Aggregate taxes by (TaxType, SubType) — use explicit key strings to avoid
            ' anonymous-type equality issues with SelectMany/GroupBy.
            Dim grouped = InvoiceLines.SelectMany(Function(l) l.TaxableItems) _
                .GroupBy(Function(t) t.TaxType & "|" & If(t.SubType, "")) _
                .Select(Function(g)
                            Dim parts = g.Key.Split("|"c)
                            Return New TaxTotalModel With {
                                .TaxType = parts(0),
                                .Amount = Math.Round(g.Sum(Function(t) t.Amount), 5)
                            }
                        End Function).ToList()
            TaxTotals = grouped

            Dim taxesTotal As Decimal = TaxTotals.Sum(Function(t) t.Amount)
            TotalAmount = Math.Round(NetAmount + taxesTotal + InvoiceLines.Sum(Function(l) l.TotalTaxableFees) - ExtraDiscountAmount, 5)
        End Sub
    End Class

    Public Class PaymentModel
        <JsonProperty("bankName")>
        Public Property BankName As String

        <JsonProperty("bankAddress")>
        Public Property BankAddress As String

        <JsonProperty("bankAccountNo")>
        Public Property BankAccountNo As String

        <JsonProperty("bankAccountIBAN")>
        Public Property BankAccountIBAN As String

        <JsonProperty("swiftCode")>
        Public Property SwiftCode As String

        <JsonProperty("terms")>
        Public Property Terms As String

        <JsonIgnore>
        Public Property PaymentMethod As String ' C=Cash, V=Visa, etc. — kept for internal use but not in ETA Invoice Payment schema

        Public Function HasBankDetails() As Boolean
            Return Not String.IsNullOrEmpty(BankName) OrElse
                   Not String.IsNullOrEmpty(BankAddress) OrElse
                   Not String.IsNullOrEmpty(BankAccountNo) OrElse
                   Not String.IsNullOrEmpty(BankAccountIBAN) OrElse
                   Not String.IsNullOrEmpty(SwiftCode) OrElse
                   Not String.IsNullOrEmpty(Terms)
        End Function

        Public Sub New()
            BankName = Nothing
            BankAddress = Nothing
            BankAccountNo = Nothing
            BankAccountIBAN = Nothing
            SwiftCode = Nothing
            Terms = Nothing
            PaymentMethod = "C"
        End Sub

        Public Function ShouldSerializeBankName() As Boolean
            Return Not String.IsNullOrEmpty(BankName)
        End Function
        Public Function ShouldSerializeBankAddress() As Boolean
            Return Not String.IsNullOrEmpty(BankAddress)
        End Function
        Public Function ShouldSerializeBankAccountNo() As Boolean
            Return Not String.IsNullOrEmpty(BankAccountNo)
        End Function
        Public Function ShouldSerializeBankAccountIBAN() As Boolean
            Return Not String.IsNullOrEmpty(BankAccountIBAN)
        End Function
        Public Function ShouldSerializeSwiftCode() As Boolean
            Return Not String.IsNullOrEmpty(SwiftCode)
        End Function
        Public Function ShouldSerializeTerms() As Boolean
            Return Not String.IsNullOrEmpty(Terms)
        End Function
    End Class

    Public Class DeliveryModel
        <JsonProperty("approach")>
        Public Property Approach As String

        <JsonProperty("packaging")>
        Public Property Packaging As String

        <JsonProperty("dateValidity")>
        Public Property DateValidity As Date?

        <JsonProperty("exportPort")>
        Public Property ExportPort As String

        <JsonProperty("grossWeight")>
        Public Property GrossWeight As Decimal

        <JsonProperty("netWeight")>
        Public Property NetWeight As Decimal

        <JsonProperty("terms")>
        Public Property Terms As String

        <JsonProperty("countryOfOrigin")>
        Public Property CountryOfOrigin As String

        Public Sub New()
            Approach = Nothing
            Packaging = Nothing
            DateValidity = Nothing
            ExportPort = Nothing
            GrossWeight = 0D
            NetWeight = 0D
            Terms = Nothing
            CountryOfOrigin = Nothing
        End Sub

        Public Function ShouldSerializeApproach() As Boolean
            Return Not String.IsNullOrEmpty(Approach)
        End Function
        Public Function ShouldSerializePackaging() As Boolean
            Return Not String.IsNullOrEmpty(Packaging)
        End Function
        Public Function ShouldSerializeExportPort() As Boolean
            Return Not String.IsNullOrEmpty(ExportPort)
        End Function
        Public Function ShouldSerializeGrossWeight() As Boolean
            Return GrossWeight > 0D
        End Function
        Public Function ShouldSerializeNetWeight() As Boolean
            Return NetWeight > 0D
        End Function
        Public Function ShouldSerializeTerms() As Boolean
            Return Not String.IsNullOrEmpty(Terms)
        End Function
        Public Function ShouldSerializeCountryOfOrigin() As Boolean
            Return Not String.IsNullOrEmpty(CountryOfOrigin)
        End Function
    End Class

End Namespace
