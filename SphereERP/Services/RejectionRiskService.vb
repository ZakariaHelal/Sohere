Option Strict On
Imports System.Security.Cryptography.X509Certificates
Imports System.Text.RegularExpressions
Imports Newtonsoft.Json
Imports SphereERP.Models
Imports SphereERP.Data

Namespace Services

    Public Enum RiskSeverity
        Info
        Warning
        Blocker
    End Enum

    ''' <summary>One finding produced by the ETA rejection-risk assessment.</summary>
    Public Class RiskFinding
        Public Property Severity As RiskSeverity
        Public Property Rule As String
        Public Property Message As String

        Public ReadOnly Property SeverityDisplay As String
            Get
                Select Case Severity
                    Case RiskSeverity.Blocker : Return "BLOCKER"
                    Case RiskSeverity.Warning : Return "Warning"
                    Case Else : Return "Info"
                End Select
            End Get
        End Property
    End Class

    ''' <summary>
    ''' Full result of the pre-submission assessment, including an estimated probability
    ''' that ETA rejects the document and a qualitative risk level.
    ''' </summary>
    Public Class RiskReport

        Public Sub New(items As List(Of RiskFinding))
            Findings = If(items, New List(Of RiskFinding)())
        End Sub

        Public Property Findings As New List(Of RiskFinding)()

        Public ReadOnly Property BlockerCount As Integer
            Get
                Return Findings.Where(Function(f) f.Severity = RiskSeverity.Blocker).Count()
            End Get
        End Property

        Public ReadOnly Property WarningCount As Integer
            Get
                Return Findings.Where(Function(f) f.Severity = RiskSeverity.Warning).Count()
            End Get
        End Property

        Public ReadOnly Property InfoCount As Integer
            Get
                Return Findings.Where(Function(f) f.Severity = RiskSeverity.Info).Count()
            End Get
        End Property

        ''' <summary>Rough estimate (0-97%) derived from findings. Blockers dominate; warnings nudge upward.</summary>
        Public ReadOnly Property EstimatedRejectionProbability As Integer
            Get
                Dim p As Integer = 2 + (BlockerCount * 28) + (WarningCount * 6)
                Return Math.Min(p, 97)
            End Get
        End Property

        ''' <summary>Low / Medium / High — High whenever at least one blocker exists.</summary>
        Public ReadOnly Property RiskLevel As String
            Get
                If BlockerCount > 0 Then Return "High"
                If EstimatedRejectionProbability >= 15 Then Return "Medium"
                Return "Low"
            End Get
        End Property

        Public ReadOnly Property Summary As String
            Get
                Return $"Risk level: {RiskLevel}   |   Estimated rejection probability: ~{EstimatedRejectionProbability}%   |   " &
                       $"{BlockerCount} blocker(s), {WarningCount} warning(s), {InfoCount} info"
            End Get
        End Property

    End Class

    ''' <summary>
    ''' Pre-submission assessment of how likely ETA is to reject a document, based on the
    ''' published ETA e-Invoicing SDK requirements (sdk.invoicing.eta.gov.eg):
    ''' same-day issuance rule, unique internal IDs per issuer, registered receiver RINs,
    ''' line/tax recalculation rules (5-decimal rounding), credit/debit reference rules,
    ''' currency/exchange-rate rules, issuer address schema minimums, and signing-certificate readiness.
    ''' Unlike DocumentValidationService (schema-level pass/fail), this service also produces
    ''' warnings for conditions that are known to cause rejections or manual reviews.
    ''' </summary>
    Public Class RejectionRiskService

        Private ReadOnly _validator As New DocumentValidationService()
        Private ReadOnly _docRepo As New DocumentRepository()
        Private ReadOnly _receiverRepo As New ReceiverRepository()
        Private ReadOnly _signer As New CertificateSigningService()

        Private Const VatStandardRate As Decimal = 14D
        Private Const Tolerance As Decimal = 0.01D

        Public Function Assess(doc As EInvoiceDocument, settings As AppSettings) As RiskReport
            Dim findings As New List(Of RiskFinding)()

            ' 1) Schema-level rules shared with the hard validator.
            SafeCheck(findings, "schema", "schema validation",
              Sub()
                  For Each msg In _validator.Validate(doc)
                      findings.Add(New RiskFinding With {.Severity = RiskSeverity.Blocker, .Rule = "schema", .Message = msg})
                  Next
              End Sub)

            ' Assess what will actually reach ETA: totals/line math are recalculated just before submission.
            Dim effectiveDoc As EInvoiceDocument = doc
            SafeCheck(findings, "totals-recalculation", "totals recalculation",
              Sub()
                  effectiveDoc = CloneWithRecalculatedTotals(doc)
              End Sub)
            If effectiveDoc Is Nothing Then effectiveDoc = doc

            SafeCheck(findings, "same-day-submission", "same-day issuance rule", Sub() CheckSameDayIssuance(effectiveDoc, findings))
            SafeCheck(findings, "duplicate-internal-id", "duplicate internal ID", Sub() CheckDuplicateInternalId(effectiveDoc, settings, findings))
            SafeCheck(findings, "receiver", "receiver checks", Sub() CheckReceiverRules(effectiveDoc, findings))
            SafeCheck(findings, "issuer-address", "issuer address schema", Sub() CheckIssuerAddressSchema(doc, findings))
            SafeCheck(findings, "activity-code", "activity code format", Sub() CheckActivityCodeFormat(doc, findings))
            SafeCheck(findings, "lines-taxes", "line and tax recalculation rules", Sub() CheckLineAndTaxRules(effectiveDoc, doc, findings))
            SafeCheck(findings, "document-totals", "document totals consistency", Sub() CheckDocumentTotals(doc, effectiveDoc, findings))
            SafeCheck(findings, "references", "credit/debit reference rules", Sub() CheckReferenceRules(effectiveDoc, settings, findings))
            SafeCheck(findings, "currency", "currency rules", Sub() CheckCurrencyRules(effectiveDoc, findings))
            SafeCheck(findings, "certificate", "signing certificate readiness", Sub() CheckSigningReadiness(doc, settings, findings))

            findings.Sort(Function(a, b) CInt(b.Severity).CompareTo(CInt(a.Severity)))
            Return New RiskReport(findings)
        End Function

        ''' <summary>
        ''' Runs one check in isolation. A failure inside a check must never abort the whole
        ''' assessment - it is reported as a warning row instead.
        ''' </summary>
        Private Shared Sub SafeCheck(findings As List(Of RiskFinding), rule As String, checkName As String, action As Action)
            Try
                action()
            Catch ex As Exception
                findings.Add(New RiskFinding With {
                    .Severity = RiskSeverity.Warning, .Rule = rule & "-check-error",
                    .Message = $"The '{checkName}' check could not be completed ({ex.GetType().Name}: {ex.Message}). Verify this aspect manually before submitting."})
            End Try
        End Sub

        ''' <summary>Returns a deep clone of the document after RecalculateTotals() - i.e. exactly what is submitted.</summary>
        Private Function CloneWithRecalculatedTotals(doc As EInvoiceDocument) As EInvoiceDocument
            Dim clone = JsonConvert.DeserializeObject(Of EInvoiceDocument)(JsonConvert.SerializeObject(doc))
            clone.RecalculateTotals()
            Return clone
        End Function

        ''' <summary>ETA same-day rule: the issue date must fall on the same calendar day as submission (UTC).</summary>
        Private Sub CheckSameDayIssuance(doc As EInvoiceDocument, findings As List(Of RiskFinding))
            Dim utcNow = DateTime.UtcNow

            If doc.DateTimeIssued > utcNow.AddMinutes(10) Then
                findings.Add(New RiskFinding With {
                    .Severity = RiskSeverity.Blocker, .Rule = "issue-date-future",
                    .Message = "Issue date/time is in the future. ETA rejects documents issued ahead of submission time."})
            ElseIf doc.DateTimeIssued.Date <> utcNow.Date Then
                findings.Add(New RiskFinding With {
                    .Severity = RiskSeverity.Blocker, .Rule = "same-day-submission",
                    .Message = $"Issue date ({doc.DateTimeIssued:yyyy-MM-dd}) is not today (UTC). ETA requires invoices to be submitted on their issue day; older documents are rejected."})
            ElseIf (utcNow - doc.DateTimeIssued).TotalHours > 12 Then
                findings.Add(New RiskFinding With {
                    .Severity = RiskSeverity.Warning, .Rule = "same-day-submission",
                    .Message = "Document was issued more than 12 hours ago. Submitting close to midnight risks crossing the same-day boundary."})
            End If
        End Sub

        ''' <summary>ETA rejects submissions whose internal ID was already used by the same issuer.</summary>
        Private Sub CheckDuplicateInternalId(doc As EInvoiceDocument, settings As AppSettings, findings As List(Of RiskFinding))
            If String.IsNullOrWhiteSpace(doc.InternalID) OrElse settings Is Nothing Then Return

            Dim envName = settings.Environment.ToString()
            If _docRepo.InternalIdExists(doc.InternalID, doc.Issuer?.Id, envName) Then
                findings.Add(New RiskFinding With {
                    .Severity = RiskSeverity.Blocker, .Rule = "duplicate-internal-id",
                    .Message = $"Internal ID '{doc.InternalID}' was already used for this taxpayer in the {envName} environment. ETA enforces unique internal IDs per issuer and rejects duplicates."})
            End If
        End Sub

        ''' <summary>B2B receiver must have a valid RIN; name should match the registered receiver record.</summary>
        Private Sub CheckReceiverRules(doc As EInvoiceDocument, findings As List(Of RiskFinding))
            If doc.Receiver Is Nothing Then Return

            If doc.Receiver.Type = "B" Then
                If Not String.IsNullOrWhiteSpace(doc.Issuer?.Id) AndAlso
                   doc.Receiver.Id = doc.Issuer.Id Then
                    findings.Add(New RiskFinding With {
                        .Severity = RiskSeverity.Warning, .Rule = "receiver-equals-issuer",
                        .Message = "Receiver RIN equals the issuer RIN (self-invoice). Verify this is intentional."})
                End If

                Dim registered = _receiverRepo.GetByRin(If(doc.Receiver.Id, ""))
                If registered Is Nothing Then
                    findings.Add(New RiskFinding With {
                        .Severity = RiskSeverity.Warning, .Rule = "receiver-not-registered-locally",
                        .Message = "Receiver RIN is not in your saved receivers list. If this RIN is not registered/active on the ETA portal, the document will be rejected (error 4042-receiver not found). Double-check before submitting."})
                ElseIf Not NamesMatch(registered.Name, doc.Receiver.Name) Then
                    findings.Add(New RiskFinding With {
                        .Severity = RiskSeverity.Warning, .Rule = "receiver-name-mismatch",
                        .Message = $"Receiver name '{doc.Receiver.Name}' differs from the registered name '{registered.Name}'. Mismatched legal names are a common cause of B2B rejection."})
                End If
            ElseIf doc.Receiver.Type = "P" AndAlso Not String.IsNullOrWhiteSpace(doc.Receiver.Id) Then
                Dim digitsOnly = doc.Receiver.Id.All(Function(c) Char.IsDigit(c))
                If digitsOnly AndAlso doc.Receiver.Id.Length <> 14 Then
                    findings.Add(New RiskFinding With {
                        .Severity = RiskSeverity.Warning, .Rule = "receiver-national-id-format",
                        .Message = $"Natural-person receiver ID should be a 14-digit national ID (got {doc.Receiver.Id.Length}). Incorrect IDs can invalidate B2C documents."})
                End If
            End If
        End Sub

        ''' <summary>Issuer address fields regionCity/street/buildingNumber are schema-required (minLength 1).</summary>
        Private Sub CheckIssuerAddressSchema(doc As EInvoiceDocument, findings As List(Of RiskFinding))
            Dim addr = doc.Issuer?.Address
            If addr Is Nothing Then Return

            If String.IsNullOrWhiteSpace(addr.RegionCity) Then
                findings.Add(New RiskFinding With {
                    .Severity = RiskSeverity.Blocker, .Rule = "issuer-address-regioncity",
                    .Message = "Issuer regionCity is empty. The Invoice v1.0 schema requires it for the issuer; configure it in Settings."})
            End If
            If String.IsNullOrWhiteSpace(addr.Street) Then
                findings.Add(New RiskFinding With {
                    .Severity = RiskSeverity.Blocker, .Rule = "issuer-address-street",
                    .Message = "Issuer street is empty. The Invoice v1.0 schema requires it for the issuer; configure it in Settings."})
            End If
            If String.IsNullOrWhiteSpace(addr.BuildingNumber) Then
                findings.Add(New RiskFinding With {
                    .Severity = RiskSeverity.Blocker, .Rule = "issuer-address-buildingnumber",
                    .Message = "Issuer buildingNumber is empty. The Invoice v1.0 schema requires it for the issuer; configure it in Settings."})
            End If
        End Sub

        ''' <summary>ETA activity codes are numeric ISIC-based codes (4-5 digits).</summary>
        Private Sub CheckActivityCodeFormat(doc As EInvoiceDocument, findings As List(Of RiskFinding))
            Dim code = doc.TaxpayerActivityCode
            If String.IsNullOrWhiteSpace(code) Then Return ' covered by schema validation

            If Not Regex.IsMatch(code.Trim(), "^\d{4,5}$") Then
                findings.Add(New RiskFinding With {
                    .Severity = RiskSeverity.Blocker, .Rule = "activity-code-format",
                    .Message = $"Taxpayer activity code '{code}' is not a valid numeric ISIC activity code. ETA validates the code against your registered activities and rejects mismatches."})
            End If
        End Sub

        ''' <summary>
        ''' Line-level ETA recalculation rules. Compares the recalculated (submitted) values with the
        ''' entered ones and flags tax amounts that cannot be reproduced from the rate.
        ''' </summary>
        Private Sub CheckLineAndTaxRules(effectiveDoc As EInvoiceDocument, originalDoc As EInvoiceDocument, findings As List(Of RiskFinding))
            For i = 0 To effectiveDoc.InvoiceLines.Count - 1
                Dim line = effectiveDoc.InvoiceLines(i)
                Dim label = $"Line {i + 1}"

                If line.NetTotal < 0 Then
                    findings.Add(New RiskFinding With {
                        .Severity = RiskSeverity.Blocker, .Rule = "line-negative-net",
                        .Message = $"{label}: net total is negative ({line.NetTotal:N2}). ETA rejects negative line amounts; use a Credit Note for reductions."})
                End If

                For Each tax In line.TaxableItems
                    If String.IsNullOrWhiteSpace(tax.SubType) Then
                        findings.Add(New RiskFinding With {
                            .Severity = RiskSeverity.Blocker, .Rule = "tax-subtype-required",
                            .Message = $"{label}: tax {tax.TaxType} has no subType. The schema requires subType (e.g. V009 for T1); empty subtypes are rejected."})
                    End If

                    If tax.Rate = 0D AndAlso tax.Amount <> 0D Then
                        findings.Add(New RiskFinding With {
                            .Severity = RiskSeverity.Blocker, .Rule = "tax-amount-without-rate",
                            .Message = $"{label}: tax {tax.TaxType} has amount {tax.Amount:N2} but rate 0%. ETA recomputes tax amounts from the rate; this mismatch causes rejection."})
                    End If

                    If tax.Rate > 0 Then
                        Dim expected = Math.Round(line.NetTotal * (tax.Rate / 100D), 5)
                        If Math.Abs(expected - tax.Amount) > Tolerance Then
                            findings.Add(New RiskFinding With {
                                .Severity = RiskSeverity.Blocker, .Rule = "tax-amount-mismatch",
                                .Message = $"{label}: tax {tax.TaxType} amount {tax.Amount:N5} does not match rate {tax.Rate}% of net {line.NetTotal:N5} (expected {expected:N5}). ETA rejects inconsistent tax amounts."})
                        End If
                    End If

                    If tax.TaxType = "T1" AndAlso tax.Rate <> 0D AndAlso tax.Rate <> VatStandardRate Then
                        findings.Add(New RiskFinding With {
                            .Severity = RiskSeverity.Warning, .Rule = "vat-non-standard-rate",
                            .Message = $"{label}: VAT rate {tax.Rate}% is not the Egyptian standard rate of {VatStandardRate:0}%. Confirm the item is subject to a special rate; otherwise use 14% or 0% (exempt)."})
                    End If
                Next
            Next

            ' Transparency: report entered values that will silently change during recalculation.
            For i = 0 To Math.Min(originalDoc.InvoiceLines.Count, effectiveDoc.InvoiceLines.Count) - 1
                Dim before = originalDoc.InvoiceLines(i)
                Dim after = effectiveDoc.InvoiceLines(i)
                Dim label = $"Line {i + 1}"

                If Math.Abs(before.SalesTotal - after.SalesTotal) > Tolerance OrElse
                   Math.Abs(before.NetTotal - after.NetTotal) > Tolerance OrElse
                   Math.Abs(before.Total - after.Total) > Tolerance Then
                    findings.Add(New RiskFinding With {
                        .Severity = RiskSeverity.Info, .Rule = "line-values-recalculated",
                        .Message = $"{label}: sales/net/total values differ from the entered amounts and will be recalculated automatically at submission (sales {after.SalesTotal:N2}, net {after.NetTotal:N2}, total {after.Total:N2})."})
                End If
            Next
        End Sub

        ''' <summary>Cross-checks document totals against the recalculated values that are actually sent.</summary>
        Private Sub CheckDocumentTotals(originalDoc As EInvoiceDocument, effectiveDoc As EInvoiceDocument, findings As List(Of RiskFinding))
            If effectiveDoc.TotalAmount <= 0 Then
                findings.Add(New RiskFinding With {
                    .Severity = RiskSeverity.Blocker, .Rule = "total-not-positive",
                    .Message = $"Grand total is {effectiveDoc.TotalAmount:N2}. ETA only accepts documents with a positive grand total."})
            End If

            If effectiveDoc.ExtraDiscountAmount > effectiveDoc.NetAmount Then
                findings.Add(New RiskFinding With {
                    .Severity = RiskSeverity.Blocker, .Rule = "extra-discount-exceeds-net",
                    .Message = "Extra discount exceeds the document net amount; resulting totals are invalid and will be rejected."})
            End If

            If Math.Abs(originalDoc.TotalAmount - effectiveDoc.TotalAmount) > Tolerance OrElse
               Math.Abs(originalDoc.NetAmount - effectiveDoc.NetAmount) > Tolerance Then
                findings.Add(New RiskFinding With {
                    .Severity = RiskSeverity.Info, .Rule = "totals-recalculated",
                    .Message = $"Document totals were adjusted by recalculation before submission (net {effectiveDoc.NetAmount:N2}, total {effectiveDoc.TotalAmount:N2})."})
            End If
        End Sub

        ''' <summary>
        ''' Credit/debit notes must reference an existing valid document of the matching type,
        ''' for the same issuer/receiver pair, without exceeding the referenced amount.
        ''' </summary>
        Private Sub CheckReferenceRules(doc As EInvoiceDocument, settings As AppSettings, findings As List(Of RiskFinding))
            Dim dt = If(doc.DocumentType, "").ToUpperInvariant()
            Dim isNote = dt = "C" OrElse dt = "D" OrElse dt = "EC" OrElse dt = "ED"
            If Not isNote Then Return

            If doc.References Is Nothing OrElse doc.References.Count = 0 OrElse
               doc.References.All(Function(r) String.IsNullOrWhiteSpace(r)) Then
                findings.Add(New RiskFinding With {
                    .Severity = RiskSeverity.Blocker, .Rule = "reference-required",
                    .Message = $"{GetDocTypeLabel(dt)} without a reference UUID. ETA rejects credit/debit notes that do not reference the original document."})
                Return
            End If

            For Each refUuid In doc.References
                If String.IsNullOrWhiteSpace(refUuid) Then Continue For

                Dim trimmed = refUuid.Trim()
                Dim parsedUuid As Guid
                If Not Guid.TryParse(trimmed, parsedUuid) Then
                    findings.Add(New RiskFinding With {
                        .Severity = RiskSeverity.Blocker, .Rule = "reference-uuid-format",
                        .Message = $"Reference '{trimmed}' is not a valid UUID. References must be the UUID of the original document."})
                    Continue For
                End If

                Dim original = _docRepo.GetByUuid(trimmed)
                If original Is Nothing Then
                    findings.Add(New RiskFinding With {
                        .Severity = RiskSeverity.Warning, .Rule = "reference-not-found-locally",
                        .Message = $"Referenced UUID {trimmed} was not found in the local history. If it does not exist on the ETA portal either, the note will be rejected."})
                    Continue For
                End If

                If settings IsNot Nothing AndAlso settings.Environment.ToString() <> original.Environment Then
                    findings.Add(New RiskFinding With {
                        .Severity = RiskSeverity.Blocker, .Rule = "reference-environment-mismatch",
                        .Message = $"Referenced document {trimmed} belongs to the {original.Environment} environment but this note targets {settings.Environment}. Cross-environment references are rejected."})
                End If

                If Not String.Equals(original.Status, "Valid", StringComparison.OrdinalIgnoreCase) AndAlso
                   Not String.Equals(original.Status, "Submitted", StringComparison.OrdinalIgnoreCase) Then
                    findings.Add(New RiskFinding With {
                        .Severity = RiskSeverity.Blocker, .Rule = "reference-status-invalid",
                        .Message = $"Referenced document (internal ID {original.InternalId}) has status '{original.Status}'. Only Valid documents may be referenced."})
                End If

                If dt = "C" OrElse dt = "D" Then
                    If original.DocumentType.ToUpperInvariant() <> "I" Then
                        findings.Add(New RiskFinding With {
                            .Severity = RiskSeverity.Blocker, .Rule = "reference-type-mismatch",
                            .Message = $"{GetDocTypeLabel(dt)} references a {original.DocumentTypeDisplay}; domestic notes must reference an invoice (I)."})
                    End If
                Else
                    If original.DocumentType.ToUpperInvariant() <> "EI" Then
                        findings.Add(New RiskFinding With {
                            .Severity = RiskSeverity.Blocker, .Rule = "reference-type-mismatch",
                            .Message = $"{GetDocTypeLabel(dt)} references a {original.DocumentTypeDisplay}; export notes must reference an export invoice (EI)."})
                    End If
                End If

                If Not String.IsNullOrWhiteSpace(original.ReceiverRIN) AndAlso
                   Not String.IsNullOrWhiteSpace(doc.Receiver?.Id) AndAlso
                   original.ReceiverRIN <> doc.Receiver.Id Then
                    findings.Add(New RiskFinding With {
                        .Severity = RiskSeverity.Blocker, .Rule = "reference-party-mismatch",
                        .Message = "Referenced document was issued to a different receiver. Notes must reference a document for the same buyer."})
                End If

                If Math.Abs(doc.TotalAmount) > Math.Abs(original.TotalAmount) + Tolerance Then
                    findings.Add(New RiskFinding With {
                        .Severity = RiskSeverity.Blocker, .Rule = "credit-exceeds-original",
                        .Message = $"Note total {doc.TotalAmount:N2} exceeds the referenced document total {original.TotalAmount:N2}. Amounts above the original value are rejected."})
                End If
            Next
        End Sub

        ''' <summary>Foreign currencies need a positive exchange rate and the foreign amount populated.</summary>
        Private Sub CheckCurrencyRules(doc As EInvoiceDocument, findings As List(Of RiskFinding))
            For i = 0 To doc.InvoiceLines.Count - 1
                Dim line = doc.InvoiceLines(i)
                Dim label = $"Line {i + 1}"
                If line.UnitValue Is Nothing Then Continue For

                Dim cur = If(line.UnitValue.CurrencySold, "")
                If cur.Length <> 3 OrElse Not cur.All(AddressOf Char.IsLetter) Then
                    findings.Add(New RiskFinding With {
                        .Severity = RiskSeverity.Blocker, .Rule = "currency-code-invalid",
                        .Message = $"{label}: currencySold '{cur}' is not a valid ISO 4217 code."})
                    Continue For
                End If

                If cur <> "EGP" Then
                    If line.UnitValue.CurrencyExchangeRate <= 0D Then
                        findings.Add(New RiskFinding With {
                            .Severity = RiskSeverity.Blocker, .Rule = "exchange-rate-required",
                            .Message = $"{label}: foreign currency {cur} needs a positive currencyExchangeRate."})
                    End If
                    If line.UnitValue.AmountSold = 0D Then
                        findings.Add(New RiskFinding With {
                            .Severity = RiskSeverity.Warning, .Rule = "amount-sold-missing",
                            .Message = $"{label}: amountSold is zero while currency is {cur}. Foreign-currency lines should carry the original amount."})
                    End If
                End If
            Next
        End Sub

        ''' <summary>Without a usable, unexpired signing certificate linked to the token, submission fails outright.</summary>
        Private Sub CheckSigningReadiness(doc As EInvoiceDocument, settings As AppSettings, findings As List(Of RiskFinding))
            If settings Is Nothing Then Return

            If String.IsNullOrWhiteSpace(settings.SigningCertThumbprint) Then
                findings.Add(New RiskFinding With {
                    .Severity = RiskSeverity.Blocker, .Rule = "certificate-configured",
                    .Message = "No signing certificate thumbprint configured (Settings > Digital Certificate). Submission cannot proceed."})
                Return
            End If

            Dim cert = _signer.FindByThumbprint(settings.SigningCertThumbprint, StoreName.My)
            If cert Is Nothing Then
                findings.Add(New RiskFinding With {
                    .Severity = RiskSeverity.Blocker, .Rule = "certificate-found",
                    .Message = "The configured signing certificate could not be found. Connect the USB token and re-select the certificate in Settings."})
                Return
            End If

            If cert.NotAfter < DateTime.Now Then
                findings.Add(New RiskFinding With {
                    .Severity = RiskSeverity.Blocker, .Rule = "certificate-expired",
                    .Message = $"The signing certificate expired on {cert.NotAfter:yyyy-MM-dd}. ETA rejects signatures from expired certificates."})
            ElseIf cert.NotBefore > DateTime.Now Then
                findings.Add(New RiskFinding With {
                    .Severity = RiskSeverity.Blocker, .Rule = "certificate-not-yet-valid",
                    .Message = $"The signing certificate is not valid before {cert.NotBefore:yyyy-MM-dd}."})
            End If

            If Not cert.HasPrivateKey Then
                findings.Add(New RiskFinding With {
                    .Severity = RiskSeverity.Blocker, .Rule = "certificate-private-key",
                    .Message = "The selected certificate has no accessible private key (token locked or driver issue); signing would fail."})
            End If

            Dim rinOrNameInSubject = cert.Subject.Contains(If(doc.Issuer?.Id, "")) OrElse
                                     cert.Subject.Contains(If(doc.Issuer?.Name, ""))
            If doc.Issuer IsNot Nothing AndAlso Not rinOrNameInSubject Then
                findings.Add(New RiskFinding With {
                    .Severity = RiskSeverity.Warning, .Rule = "certificate-owner-mismatch",
                    .Message = "The certificate subject does not obviously contain the taxpayer RIN or name. Signatures must come from a certificate issued to this taxpayer; verify in Settings > Test Sign."})
            End If
        End Sub

        Private Shared Function NamesMatch(a As String, b As String) As Boolean
            Dim normalize = Function(s As String) Regex.Replace(If(s, ""), "\s+", " ").Trim().ToUpperInvariant()
            Return normalize(a) = normalize(b)
        End Function

        Private Shared Function GetDocTypeLabel(dt As String) As String
            Select Case dt.ToUpperInvariant()
                Case "C" : Return "Credit Note"
                Case "D" : Return "Debit Note"
                Case "EC" : Return "Export Credit Note"
                Case "ED" : Return "Export Debit Note"
                Case Else : Return "Note"
            End Select
        End Function

    End Class

End Namespace
