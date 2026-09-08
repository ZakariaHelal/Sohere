Option Strict On
Imports SphereERP.Models

Namespace Services

    ''' <summary>
    ''' Validates an EInvoiceDocument against ETA e-Invoicing business rules before
    ''' submission, covering document type/status compliance, required fields,
    ''' tax codes, and reference rules for Credit/Debit notes.
    ''' Mirrors the validation rules published in the ETA e-Invoicing SDK documentation.
    ''' </summary>
    Public Class DocumentValidationService

        ' Valid ETA tax type codes (T1=VAT, T2=Table Tax (percentage), T3=Table Tax (Fixed Amount),
        ' T4=Withholding Tax (WHT), T5=Stamping Tax (percentage), T6=Stamping Tax (amount), T7=Entertainment Tax, etc.)
        Private Shared ReadOnly ValidTaxTypes As HashSet(Of String) =
            New HashSet(Of String) From {"T1", "T2", "T3", "T4", "T5", "T6", "T7", "T8", "T9", "T10", "T11", "T12", "T13", "T14", "T15", "T16", "T17", "T18", "T19", "T20"}

        Private Shared ReadOnly ValidDocumentTypes As HashSet(Of String) = New HashSet(Of String) From {"I", "C", "D", "EI", "EC", "ED"}
        Private Shared ReadOnly ValidPartyTypes As HashSet(Of String) = New HashSet(Of String) From {"B", "P", "F"}
        Private Shared ReadOnly ValidPaymentMethods As HashSet(Of String) = New HashSet(Of String) From {"C", "V", "CC", "VC", "VO", "PR", "GC", "P", "O"}

        ' ETA unit type codes from https://sdk.invoicing.eta.gov.eg/codes/unit-types/
        Private Shared ReadOnly ValidUnitTypes As HashSet(Of String) =
            New HashSet(Of String) From {"2Z", "4K", "4O", "A87", "A93", "A94", "AMP", "ANN", "B22", "B49", "B75", "B78", "B84",
                "BAR", "BBL", "BG", "BO", "BOX", "C10", "C39", "C41", "C45", "C62", "CA", "CMK", "CMQ", "CMT", "CS", "CT", "CTL",
                "D10", "D33", "D41", "DAY", "DMT", "DRM", "EA", "FAR", "FOT", "FTK", "FTQ", "G42", "GL", "GLL", "GM", "GPT", "GRM",
                "H63", "HHP", "HLT", "HTZ", "HUR", "IE", "INH", "INK", "JOB", "KGM", "KHZ", "KMH", "KMK", "KMQ", "KMT", "KSM",
                "KVT", "KWT", "LB", "LTR", "LVL", "M", "MAN", "MAW", "MGM", "MHZ", "MIN", "MMK", "MMQ", "MMT", "MON", "MTK", "MTQ",
                "OHM", "ONZ", "PAL", "PF", "PK", "SK", "SMI", "ST", "TNE", "TON", "VLT", "WEE", "WTT", "X03", "YDQ", "YRD",
                "NMP", "5I", "AE", "B4", "BB", "BD", "BE", "BK", "BL", "CH", "CR", "DAA", "DTN", "DZN", "FP", "HMT", "INQ",
                "KG", "KTM", "LO", "MLT", "MT", "NA", "NAR", "NC", "NE", "NPL", "NV", "PA", "PG", "PL", "PR", "PT", "RL", "RO",
                "SET", "STK", "T3", "TC", "TK", "TN", "TTS", "UC", "VI", "VQ", "YDK", "Z3"}

        ' Return with No Reference Reasons from https://sdk.invoicing.eta.gov.eg/codes/return-with-no-reference-reasons-type/
        Private Shared ReadOnly ValidReturnWithNoReferenceReasons As HashSet(Of String) =
            New HashSet(Of String) From {"I", "B"}

        ''' <summary>
        ''' Runs all validation rules and returns a list of error messages.
        ''' An empty list means the document is valid and ready for signing/submission.
        ''' </summary>
        Public Function Validate(doc As EInvoiceDocument) As List(Of String)
            Dim errors As New List(Of String)()

            ValidateDocumentTypeAndVersion(doc, errors)
            ValidateIssuer(doc, errors)
            ValidateReceiver(doc, errors)
            ValidateDates(doc, errors)
            ValidateLines(doc, errors)
            ValidateTotals(doc, errors)
            ValidateReferencesForNotes(doc, errors)
            ValidatePayment(doc, errors)

            Return errors
        End Function

        Private Sub ValidateDocumentTypeAndVersion(doc As EInvoiceDocument, errors As List(Of String))
            If String.IsNullOrWhiteSpace(doc.DocumentType) OrElse Not ValidDocumentTypes.Contains(doc.DocumentType.ToUpperInvariant()) Then
                errors.Add("Document type must be one of: I (Invoice), C (Credit Note), D (Debit Note), EI (Export Invoice), EC (Export Credit Note), ED (Export Debit Note).")
            End If

            If String.IsNullOrWhiteSpace(doc.DocumentTypeVersion) Then
                errors.Add("Document type version is required (e.g. '1.0').")
            End If

            If String.IsNullOrWhiteSpace(doc.InternalID) Then
                errors.Add("Internal ID (your own invoice/document number) is required.")
            End If
        End Sub

        Private Sub ValidateIssuer(doc As EInvoiceDocument, errors As List(Of String))
            If doc.Issuer Is Nothing Then
                errors.Add("Issuer information is required.")
                Return
            End If

            If String.IsNullOrWhiteSpace(doc.Issuer.Id) Then
                errors.Add("Issuer Tax Registration Number (RIN) is required.")
            ElseIf Not IsValidEgyptianRin(doc.Issuer.Id) Then
                errors.Add("Issuer RIN must be a valid 9-digit Egyptian Tax Registration Number.")
            End If

            If String.IsNullOrWhiteSpace(doc.Issuer.Name) Then
                errors.Add("Issuer name is required.")
            End If

            If Not ValidPartyTypes.Contains(doc.Issuer.Type) Then
                errors.Add("Issuer type must be B (Business), P (Natural Person/Person), or F (Foreigner).")
            End If

            If doc.Issuer.Address Is Nothing OrElse String.IsNullOrWhiteSpace(doc.Issuer.Address.Governate) Then
                errors.Add("Issuer governate is required.")
            End If

            If String.IsNullOrWhiteSpace(doc.TaxpayerActivityCode) Then
                errors.Add("Taxpayer activity code is required for the issuer.")
            End If
        End Sub

        Private Sub ValidateReceiver(doc As EInvoiceDocument, errors As List(Of String))
            If doc.Receiver Is Nothing Then
                errors.Add("Receiver information is required (use type 'P' with a generic ID for retail/B2C if unknown).")
                Return
            End If

            If Not ValidPartyTypes.Contains(doc.Receiver.Type) Then
                errors.Add("Receiver type must be B (Business), P (Natural Person), or F (Foreigner).")
            End If

            ' B2B (type B) receivers must have an RIN; B2C (type P) and Export (type F) have relaxed ID rules.
            If doc.Receiver.Type = "B" Then
                If String.IsNullOrWhiteSpace(doc.Receiver.Id) Then
                    errors.Add("Receiver RIN is required for B2B (business) transactions.")
                ElseIf Not IsValidEgyptianRin(doc.Receiver.Id) Then
                    errors.Add("Receiver RIN must be a valid 9-digit Egyptian Tax Registration Number for B2B transactions.")
                End If
                If String.IsNullOrWhiteSpace(doc.Receiver.Name) Then
                    errors.Add("Receiver name is required for B2B transactions.")
                End If
            End If
        End Sub

        Private Sub ValidateDates(doc As EInvoiceDocument, errors As List(Of String))
            If doc.DateTimeIssued = Date.MinValue Then
                errors.Add("Document issue date/time is required.")
            ElseIf doc.DateTimeIssued > DateTime.Now.AddMinutes(10) Then
                errors.Add("Document issue date/time cannot be in the future.")
            ElseIf doc.DateTimeIssued < DateTime.Now.AddDays(-1) Then
                errors.Add("Document issue date/time must generally be within the same day of submission (per ETA same-day submission rule). Verify before proceeding.")
            End If
        End Sub

        Private Sub ValidateLines(doc As EInvoiceDocument, errors As List(Of String))
            If doc.InvoiceLines Is Nothing OrElse doc.InvoiceLines.Count = 0 Then
                errors.Add("At least one invoice line is required.")
                Return
            End If

            For i = 0 To doc.InvoiceLines.Count - 1
                Dim line = doc.InvoiceLines(i)
                Dim lineLabel = $"Line {i + 1}"

                If String.IsNullOrWhiteSpace(line.Description) Then
                    errors.Add($"{lineLabel}: description is required.")
                End If

                If String.IsNullOrWhiteSpace(line.ItemCode) Then
                    errors.Add($"{lineLabel}: item code (EGS/GS1/GPC) is required. Use the EGS Code Library or retrieve from the portal.")
                End If

                If String.IsNullOrWhiteSpace(line.ItemType) Then
                    errors.Add($"{lineLabel}: item type must be specified (EGS, GS1, or GPC).")
                End If

                If String.IsNullOrWhiteSpace(line.UnitType) Then
                    errors.Add($"{lineLabel}: unit type (e.g. EA, KGM) is required.")
                ElseIf Not ValidUnitTypes.Contains(line.UnitType) Then
                    errors.Add($"{lineLabel}: unit type '{line.UnitType}' is not a recognized ETA unit type code.")
                End If

                If line.Quantity <= 0 Then
                    errors.Add($"{lineLabel}: quantity must be greater than zero.")
                End If

                If line.UnitValue Is Nothing OrElse line.UnitValue.AmountEGP < 0 Then
                    errors.Add($"{lineLabel}: unit price (EGP) cannot be negative.")
                End If

                If line.TaxableItems Is Nothing OrElse line.TaxableItems.Count = 0 Then
                    errors.Add($"{lineLabel}: at least one tax item (e.g. T1/VAT) is required, even if rate is 0% for exempt items.")
                Else
                    For Each tax In line.TaxableItems
                        If Not ValidTaxTypes.Contains(tax.TaxType) Then
                            errors.Add($"{lineLabel}: tax type '{tax.TaxType}' is not a recognized ETA tax type code.")
                        End If
                        If tax.Rate < 0 OrElse tax.Rate > 100 Then
                            errors.Add($"{lineLabel}: tax rate for {tax.TaxType} must be between 0 and 100.")
                        End If
                    Next
                End If
            Next
        End Sub

        Private Sub ValidateTotals(doc As EInvoiceDocument, errors As List(Of String))
            If doc.InvoiceLines Is Nothing OrElse doc.InvoiceLines.Count = 0 Then Return

            Dim expectedNet = Math.Round(doc.InvoiceLines.Sum(Function(l) l.NetTotal), 2)
            Dim actualNet = Math.Round(doc.NetAmount, 2)
            If Math.Abs(expectedNet - actualNet) > 0.05D Then
                errors.Add($"Net amount mismatch: line items sum to {expectedNet:N2} but document net amount is {actualNet:N2}. Recalculate totals before submitting.")
            End If

            If doc.TotalAmount <= 0 Then
                errors.Add("Total amount must be greater than zero.")
            End If
        End Sub

        Private Sub ValidateReferencesForNotes(doc As EInvoiceDocument, errors As List(Of String))
            Dim dt = doc.DocumentType.ToUpperInvariant()
            If dt = "C" OrElse dt = "D" OrElse dt = "EC" OrElse dt = "ED" Then
                If doc.References Is Nothing OrElse doc.References.Count = 0 OrElse
                   doc.References.All(Function(r) String.IsNullOrWhiteSpace(r)) Then
                    Dim typeName = GetDocTypeLabel(dt)
                    errors.Add($"{typeName} must reference the original invoice's UUID in the References field.")
                End If
            End If
        End Sub

        Private Shared Function GetDocTypeLabel(dt As String) As String
            Select Case dt.ToUpperInvariant()
                Case "I" : Return "Invoice"
                Case "C" : Return "Credit Note"
                Case "D" : Return "Debit Note"
                Case "EI" : Return "Export Invoice"
                Case "EC" : Return "Export Credit Note"
                Case "ED" : Return "Export Debit Note"
                Case Else : Return "Document"
            End Select
        End Function

        Private Sub ValidatePayment(doc As EInvoiceDocument, errors As List(Of String))
            If doc.Payment IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(doc.Payment.PaymentMethod) Then
                If Not ValidPaymentMethods.Contains(doc.Payment.PaymentMethod) Then
                    errors.Add("Payment method must be one of: C (Cash), V (Visa), CC (Cash with contractor), VC (Visa with contractor), VO (Vouchers), PR (Promotion), GC (Gift Card), P (Points), O (Others).")
                End If
            End If
        End Sub

        ''' <summary>Basic structural check for a 9-digit Egyptian Tax Registration Number.</summary>
        Public Function IsValidEgyptianRin(rin As String) As Boolean
            If String.IsNullOrWhiteSpace(rin) Then Return False
            Dim trimmed = rin.Trim()
            Return trimmed.Length = 9 AndAlso trimmed.All(Function(ch) Char.IsDigit(ch))
        End Function

        ''' <summary>
        ''' Maps an ETA portal status string to the internal DocumentStatusEnum used for
        ''' grid coloring/filtering, ensuring consistent display across the app.
        ''' </summary>
        Public Function NormalizeStatus(portalStatus As String) As String
            If String.IsNullOrWhiteSpace(portalStatus) Then Return "Submitted"
            Select Case portalStatus.Trim().ToLowerInvariant()
                Case "valid" : Return "Valid"
                Case "invalid" : Return "Invalid"
                Case "cancelled", "canceled" : Return "Cancelled"
                Case "rejected" : Return "Rejected"
                Case "submitted" : Return "Submitted"
                Case Else : Return portalStatus
            End Select
        End Function

    End Class

End Namespace
