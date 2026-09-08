Option Strict On
Imports System.Security.Cryptography.X509Certificates
Imports System.Text
Imports SphereERP.Models
Imports SphereERP.Data
Imports Newtonsoft.Json

Namespace Services

    ''' <summary>
    ''' High-level orchestration: validates a document, signs it with the configured
    ''' USB token/certificate, submits it to the ETA API, persists the result locally,
    ''' and raises notification log entries for validation/issuance/rejection events.
    ''' </summary>
    Public Class DocumentSubmissionService

        Private ReadOnly _settings As AppSettings
        Private ReadOnly _apiClient As EtaApiClient
        Private ReadOnly _validator As New DocumentValidationService()
        Private ReadOnly _signer As New CertificateSigningService()
        Private ReadOnly _docRepo As New DocumentRepository()
        Private ReadOnly _notifRepo As New NotificationRepository()
        Private ReadOnly _logger As New AppLogRepository()

        Public Sub New(settings As AppSettings, apiClient As EtaApiClient)
            _settings = settings
            _apiClient = apiClient
        End Sub

        ''' <summary>
        ''' Result of a single document submission attempt, suitable for displaying
        ''' to the user and for batch-import summary reporting.
        ''' </summary>
        Public Class SubmitResult
            Public Property Success As Boolean
            Public Property LocalDocumentId As Integer
            Public Property Uuid As String
            Public Property LongId As String
            Public Property ErrorMessage As String
            Public Property ValidationErrors As List(Of String)

            Public Sub New()
                Success = False
                LocalDocumentId = 0
                Uuid = ""
                LongId = ""
                ErrorMessage = ""
                ValidationErrors = New List(Of String)()
            End Sub
        End Class

        ''' <summary>
        ''' Validates, signs, and submits a single document. Saves it locally regardless
        ''' of outcome so it appears in document history (Draft/Failed/Submitted).
        ''' </summary>
        Public Async Function SubmitSingleAsync(doc As EInvoiceDocument) As Task(Of SubmitResult)
            Dim result As New SubmitResult()

            doc.RecalculateTotals()

            Dim validationErrors = _validator.Validate(doc)
            If validationErrors.Count > 0 Then
                result.ValidationErrors = validationErrors
                result.ErrorMessage = "Document failed validation. See details."
                Return result
            End If

            Dim cert As X509Certificate2 = _signer.FindByThumbprint(_settings.SigningCertThumbprint, StoreName.My)
            If cert Is Nothing Then
                result.ErrorMessage = "No signing certificate configured or the USB token certificate could not be found. Configure it in Settings > Certificate."
                Return result
            End If

            Dim documentJsonForStorage As String = ""
            Dim localId As Integer = 0

            Try
                ' Sign: canonical content to sign is the document JSON (without the signatures array populated).
                Dim contentToSign = BuildCanonicalContentForSigning(doc)
                Dim contentBytes = Encoding.UTF8.GetBytes(contentToSign)
                Dim signatureBase64 = _signer.SignCadesBes(contentBytes, cert)

                doc.Signatures = New List(Of SignatureModel) From {
                    New SignatureModel With {.Type = "I", .Value = signatureBase64}
                }

                documentJsonForStorage = JsonConvert.SerializeObject(doc, New JsonSerializerSettings With {
                    .Formatting = Formatting.Indented,
                    .NullValueHandling = NullValueHandling.Ignore,
                    .DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                    .DateFormatString = "yyyy-MM-ddTHH:mm:ssZ"
                })
                localId = _docRepo.Insert(doc, _settings.Environment.ToString(), "Submitted", documentJsonForStorage)
                result.LocalDocumentId = localId

            Catch ex As Exception
                _logger.Error("DocumentSubmissionService.SubmitSingleAsync", "Signing failed: " & ex.Message)
                result.ErrorMessage = "Signing failed: " & ex.Message
                Return result
            End Try

            Try
                Dim submission = Await _apiClient.SubmitDocumentsAsync(New List(Of EInvoiceDocument) From {doc}).ConfigureAwait(False)

                If submission.RejectedDocuments IsNot Nothing AndAlso submission.RejectedDocuments.Count > 0 Then
                    Dim rejected = submission.RejectedDocuments(0)
                    Dim err = rejected.Error
                    Dim msg = $"{err?.Code}: {err?.Message}"
                    If err?.Details IsNot Nothing AndAlso err.Details.Count > 0 Then
                        Dim detailStrs = err.Details.Select(Function(d) $"[{d.Code}] {d.Message} (target: {d.Target})")
                        msg &= " | " & String.Join("; ", detailStrs)
                    End If
                    _docRepo.UpdateStatus(localId, "Rejected", msg, JsonConvert.SerializeObject(submission))
                    _notifRepo.Insert(localId, doc.InternalID, "", "Rejection", msg)
                    result.ErrorMessage = "Document was rejected by ETA: " & msg
                    Return result
                End If

                If submission.AcceptedDocuments IsNot Nothing AndAlso submission.AcceptedDocuments.Count > 0 Then
                    Dim accepted = submission.AcceptedDocuments(0)
                    _docRepo.UpdateSubmissionResult(localId, "Valid", "Accepted by ETA",
                        accepted.Uuid, accepted.SubmissionUuid, accepted.LongId,
                        JsonConvert.SerializeObject(submission))

                    _notifRepo.Insert(localId, doc.InternalID, accepted.Uuid, "Validation", "Document validated successfully by ETA.")
                    _notifRepo.Insert(localId, doc.InternalID, accepted.Uuid, "Issuance", $"Document issued with UUID {accepted.Uuid}.")

                    result.Success = True
                    result.Uuid = accepted.Uuid
                    result.LongId = accepted.LongId
                    Return result
                End If

                ' Neither accepted nor rejected lists populated - treat as ambiguous/pending.
                _docRepo.UpdateStatus(localId, "Submitted", "Awaiting ETA processing")
                result.Success = True
                Return result

            Catch ex As EtaApiException
                _docRepo.UpdateStatus(localId, "Failed", ex.Message)
                _logger.Error("DocumentSubmissionService.SubmitSingleAsync", $"API error: {ex.Message} | Body: {ex.ResponseBody}")
                result.ErrorMessage = "Submission failed: " & ex.Message
                Return result
            Catch ex As Exception
                _docRepo.UpdateStatus(localId, "Failed", ex.Message)
                _logger.Error("DocumentSubmissionService.SubmitSingleAsync", "Unexpected error: " & ex.Message)
                result.ErrorMessage = "Submission failed: " & ex.Message
                Return result
            End Try
        End Function

        ''' <summary>
        ''' Builds the ETA-canonical serialization of the document used as input to the
        ''' digital signature, per ETA's "Document Serialization Approach" (signed content
        ''' is the document excluding the signatures array). This special canonical form
        ''' (uppercased property names, quoted values, repeated array-name prefixes)
        ''' guarantees ETA can reproduce the exact hash regardless of JSON whitespace.
        ''' </summary>
        Private Function BuildCanonicalContentForSigning(doc As EInvoiceDocument) As String
            ' Serialize with the exact same settings used to build the JSON that is
            ' submitted to ETA, so the canonicalized values match what ETA verifies.
            ' The signatures property is excluded entirely (not serialized as empty),
            ' because ETA strips the signatures array before re-deriving the hash.
            Dim json = JsonConvert.SerializeObject(doc, New JsonSerializerSettings With {
                .NullValueHandling = NullValueHandling.Ignore,
                .DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                .DateFormatString = "yyyy-MM-ddTHH:mm:ssZ",
                .ContractResolver = New CanonicalJsonResolver()
            })

            ' Load with FloatParseHandling.Decimal so numeric literals keep their exact
            ' scale, and DateParseHandling.None so ISO-8601 strings (e.g. dateTimeIssued)
            ' stay as verbatim string literals instead of being converted to Date tokens
            ' (which would otherwise be dropped by canonicalization).
            Using strReader As New IO.StringReader(json)
                Using jsonReader As New Newtonsoft.Json.JsonTextReader(strReader) With {
                    .FloatParseHandling = Newtonsoft.Json.FloatParseHandling.Decimal,
                    .DateParseHandling = Newtonsoft.Json.DateParseHandling.None
                }
                    Dim root = Newtonsoft.Json.Linq.JObject.Load(jsonReader)
                    Return CanonicalizeToken(root)
                End Using
            End Using
        End Function

        ''' <summary>
        ''' Newtonsoft contract resolver that omits the SignatureModel collection from
        ''' serialization, ensuring the signed content excludes the signatures array
        ''' (ETA strips it before verifying the CAdES-BES signature).
        ''' </summary>
        Private Class CanonicalJsonResolver
            Inherits Newtonsoft.Json.Serialization.DefaultContractResolver
            Protected Overrides Function CreateProperty(ByVal member As System.Reflection.MemberInfo,
                                                        ByVal memberSerialization As Newtonsoft.Json.MemberSerialization) As Newtonsoft.Json.Serialization.JsonProperty
                Dim jsonProperty = MyBase.CreateProperty(member, memberSerialization)
                If member.Name = "Signatures" OrElse jsonProperty.PropertyName = "signatures" Then
                    jsonProperty.Ignored = True
                End If
                Return jsonProperty
            End Function
        End Class

        ''' <summary>
        ''' Recursively serializes a JSON structure per the ETA canonicalization algorithm.
        ''' Property names become uppercased quoted tokens; simple values are quoted verbatim;
        ''' every array element is prefixed with its (uppercased) array property name.
        ''' </summary>
        Private Shared Function CanonicalizeToken(token As Newtonsoft.Json.Linq.JToken) As String
            Select Case token.Type
                Case Newtonsoft.Json.Linq.JTokenType.Object
                    Dim sb As New StringBuilder()
                    For Each prop In DirectCast(token, Newtonsoft.Json.Linq.JObject).Properties()
                        Dim propertyToken = """" & prop.Name.ToUpperInvariant() & """"
                        sb.Append(propertyToken)
                        If prop.Value.Type = Newtonsoft.Json.Linq.JTokenType.Array Then
                            For Each element In DirectCast(prop.Value, Newtonsoft.Json.Linq.JArray)
                                sb.Append(propertyToken)
                                sb.Append(CanonicalizeToken(element))
                            Next
                        Else
                            sb.Append(CanonicalizeToken(prop.Value))
                        End If
                    Next
                    Return sb.ToString()

                Case Newtonsoft.Json.Linq.JTokenType.Array
                    Dim sb As New StringBuilder()
                    For Each element In DirectCast(token, Newtonsoft.Json.Linq.JArray)
                        sb.Append(CanonicalizeToken(element))
                    Next
                    Return sb.ToString()

                Case Newtonsoft.Json.Linq.JTokenType.String
                    Return """" & Convert.ToString(DirectCast(token, Newtonsoft.Json.Linq.JValue).Value) & """"

                Case Newtonsoft.Json.Linq.JTokenType.Integer, Newtonsoft.Json.Linq.JTokenType.Float
                    Return """" & FormatNumberLiteral(DirectCast(token, Newtonsoft.Json.Linq.JValue)) & """"

                Case Newtonsoft.Json.Linq.JTokenType.Boolean
                    Return """" & DirectCast(token, Newtonsoft.Json.Linq.JValue).ToString().ToLowerInvariant() & """"

                Case Newtonsoft.Json.Linq.JTokenType.Null
                    Return """"""

                Case Else
                    Return ""
            End Select
        End Function

        ''' <summary>
        ''' Preserves the exact numeric literal as it appears in the input JSON so that
        ''' canonicalized output stays byte-identical to what ETA derives from the document.
        ''' Values were loaded with FloatParseHandling.Decimal, so converting via the typed
        ''' Decimal keeps their original scale (e.g. "93024.00" -> "93024.00").
        ''' </summary>
        Private Shared Function FormatNumberLiteral(value As Newtonsoft.Json.Linq.JValue) As String
            Dim dec As Decimal = Convert.ToDecimal(value.Value)
            Return dec.ToString(Globalization.CultureInfo.InvariantCulture)
        End Function

        ''' <summary>
        ''' Cancels (issuer-initiated) or rejects (receiver-initiated) a previously
        ''' submitted document on the portal and updates the local cache + notification log.
        ''' </summary>
        Public Async Function ChangeStatusAsync(localDocumentId As Integer, uuid As String, internalId As String,
                                                  newStatus As String, reason As String) As Task(Of Boolean)
            Try
                Await _apiClient.ChangeDocumentStateAsync(uuid, newStatus, reason).ConfigureAwait(False)

                Dim normalizedStatus = If(newStatus.Equals("cancelled", StringComparison.OrdinalIgnoreCase), "Cancelled", "Rejected")
                _docRepo.UpdateStatus(localDocumentId, normalizedStatus, reason)

                Dim eventType = If(normalizedStatus = "Cancelled", "Cancellation", "Rejection")
                _notifRepo.Insert(localDocumentId, internalId, uuid, eventType, $"Document {normalizedStatus.ToLowerInvariant()}: {reason}")

                Return True
            Catch ex As Exception
                _logger.Error("DocumentSubmissionService.ChangeStatusAsync", ex.Message)
                Throw
            End Try
        End Function

        ''' <summary>Polls the ETA portal for current status of locally-tracked submitted documents and updates local cache.</summary>
        Public Async Function RefreshDocumentStatusAsync(localDocumentId As Integer, uuid As String, internalId As String) As Task(Of String)
            If String.IsNullOrWhiteSpace(uuid) Then Return "Submitted"

            Try
                Dim details = Await _apiClient.GetDocumentDetailsAsync(uuid).ConfigureAwait(False)
                Dim normalized = _validator.NormalizeStatus(details.Status)

                Dim previousRows = _docRepo.Search(internalId:=internalId)
                Dim previousStatus = If(previousRows.Any(), previousRows.First().Status, "")

                _docRepo.UpdateStatus(localDocumentId, normalized, details.DocumentStatusReason)

                If Not String.Equals(previousStatus, normalized, StringComparison.OrdinalIgnoreCase) Then
                    Select Case normalized
                        Case "Valid"
                            _notifRepo.Insert(localDocumentId, internalId, uuid, "Validation", "Document status changed to Valid.")
                        Case "Invalid"
                            _notifRepo.Insert(localDocumentId, internalId, uuid, "Rejection", "Document status changed to Invalid.")
                        Case "Cancelled"
                            _notifRepo.Insert(localDocumentId, internalId, uuid, "Cancellation", "Document status changed to Cancelled.")
                        Case "Rejected"
                            _notifRepo.Insert(localDocumentId, internalId, uuid, "Rejection", "Document status changed to Rejected.")
                    End Select
                End If

                Return normalized
            Catch ex As Exception
                _logger.Warning("DocumentSubmissionService.RefreshDocumentStatusAsync", ex.Message)
                Return "Unknown"
            End Try
        End Function

    End Class

End Namespace
