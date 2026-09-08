Option Strict On
Imports System.Net.Http
Imports System.Net.Http.Headers
Imports System.Text
Imports Newtonsoft.Json
Imports SphereERP.Models

Namespace Services

    ''' <summary>
    ''' Client for the ETA e-Invoicing API (https://sdk.invoicing.eta.gov.eg/einvoicingapi/).
    ''' Handles OAuth2 client-credentials token acquisition/renewal and the document
    ''' submission, search, and status-change endpoints for both Preproduction and
    ''' Production environments.
    ''' </summary>
    Public Class EtaApiClient
        Implements IDisposable

        Private ReadOnly _httpClient As HttpClient
        Private ReadOnly _settings As AppSettings
        Private ReadOnly _logger As Data.AppLogRepository
        Private _currentToken As TokenResponse

        Public Sub New(settings As AppSettings)
            _settings = settings
            _logger = New Data.AppLogRepository()
            _httpClient = New HttpClient() With {
                .Timeout = TimeSpan.FromSeconds(90)
            }
            _currentToken = Nothing
        End Sub

        ''' <summary>True if a non-expired access token is currently held.</summary>
        Public ReadOnly Property HasValidToken As Boolean
            Get
                Return _currentToken IsNot Nothing AndAlso Not _currentToken.IsExpired
            End Get
        End Property

        ''' <summary>The currently cached token, if any.</summary>
        Public ReadOnly Property CurrentToken As TokenResponse
            Get
                Return _currentToken
            End Get
        End Property

        ''' <summary>
        ''' Acquires (or renews) an OAuth2 access token from the active environment's
        ''' identity server using the client-credentials grant.
        ''' </summary>
        Public Async Function LoginAsync() As Task(Of TokenResponse)
            Dim clientId = _settings.ActiveClientId
            Dim clientSecret = _settings.ActiveClientSecret

            If String.IsNullOrWhiteSpace(clientId) OrElse String.IsNullOrWhiteSpace(clientSecret) Then
                Throw New InvalidOperationException("Client ID / Client Secret are not configured for the selected environment. Please configure them in Settings.")
            End If

            Dim tokenEndpoint = $"{_settings.ActiveIdentityUrl.TrimEnd("/"c)}/connect/token"

            Using request As New HttpRequestMessage(HttpMethod.Post, tokenEndpoint)
                Dim formData As New List(Of KeyValuePair(Of String, String))() From {
                    New KeyValuePair(Of String, String)("grant_type", "client_credentials"),
                    New KeyValuePair(Of String, String)("client_id", clientId),
                    New KeyValuePair(Of String, String)("client_secret", clientSecret),
                    New KeyValuePair(Of String, String)("scope", "InvoicingAPI")
                }
                request.Content = New FormUrlEncodedContent(formData)

                Try
                    Dim response = Await _httpClient.SendAsync(request).ConfigureAwait(False)
                    Dim body = Await response.Content.ReadAsStringAsync().ConfigureAwait(False)

                    If Not response.IsSuccessStatusCode Then
                        _logger.Error("EtaApiClient.LoginAsync", $"Token request failed ({CInt(response.StatusCode)}): {body}")
                        Throw New ApplicationException($"Login failed ({CInt(response.StatusCode)}): {ExtractErrorMessage(body)}")
                    End If

                    Dim token = JsonConvert.DeserializeObject(Of TokenResponse)(body)
                    token.AcquiredAtUtc = DateTime.UtcNow
                    _currentToken = token

                    _logger.Info("EtaApiClient.LoginAsync", $"Token acquired successfully, expires in {token.ExpiresIn}s ({_settings.Environment})")
                    Return token

                Catch ex As HttpRequestException
                    _logger.Error("EtaApiClient.LoginAsync", "Network error: " & ex.Message)
                    Throw New ApplicationException("Could not reach the ETA identity server. Check your internet connection and the environment URL in Settings.", ex)
                End Try
            End Using
        End Function

        ''' <summary>Ensures a valid token is held, renewing it if expired or absent.</summary>
        Public Async Function EnsureTokenAsync() As Task
            If Not HasValidToken Then
                Await LoginAsync().ConfigureAwait(False)
            End If
        End Function

        ''' <summary>
        ''' Submits one or more signed documents (invoices/credit notes/debit notes) to the
        ''' POST /api/v1.0/documentsubmissions endpoint.
        ''' </summary>
        Public Async Function SubmitDocumentsAsync(documents As List(Of EInvoiceDocument)) As Task(Of SubmissionResponse)
            Await EnsureTokenAsync().ConfigureAwait(False)

            Dim url = $"{_settings.ActiveApiBaseUrl.TrimEnd("/"c)}/api/v1.0/documentsubmissions"
            Dim payload = New With {.documents = documents}
            Dim json = JsonConvert.SerializeObject(payload, New JsonSerializerSettings With {
                .NullValueHandling = NullValueHandling.Ignore,
                .DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                .DateFormatString = "yyyy-MM-ddTHH:mm:ssZ"
            })

            Using request As New HttpRequestMessage(HttpMethod.Post, url)
                request.Headers.Authorization = New AuthenticationHeaderValue("Bearer", _currentToken.AccessToken)
                request.Content = New StringContent(json, Encoding.UTF8, "application/json")

                Dim response = Await _httpClient.SendAsync(request).ConfigureAwait(False)
                Dim body = Await response.Content.ReadAsStringAsync().ConfigureAwait(False)

                If response.StatusCode = Net.HttpStatusCode.Unauthorized Then
                    ' Token may have just expired; retry once after renewing.
                    Await LoginAsync().ConfigureAwait(False)
                    Return Await SubmitDocumentsAsync(documents).ConfigureAwait(False)
                End If

                If Not response.IsSuccessStatusCode Then
                    _logger.Error("EtaApiClient.SubmitDocumentsAsync", $"Submission failed ({CInt(response.StatusCode)}): {body}")
                    Throw New EtaApiException($"Document submission failed ({CInt(response.StatusCode)}).", body)
                End If

                Dim result = JsonConvert.DeserializeObject(Of SubmissionResponse)(body)
                _logger.Info("EtaApiClient.SubmitDocumentsAsync", $"Submitted {documents.Count} document(s); accepted={result.AcceptedDocuments.Count}, rejected={result.RejectedDocuments.Count}")
                Return result
            End Using
        End Function

        ''' <summary>Retrieves the current status/details of a single document by its UUID.</summary>
        Public Async Function GetDocumentDetailsAsync(uuid As String) As Task(Of DocumentSearchItem)
            Dim body = Await GetDocumentDetailsRawJsonAsync(uuid).ConfigureAwait(False)
            Return JsonConvert.DeserializeObject(Of DocumentSearchItem)(body)
        End Function

        ''' <summary>Fetches the full document JSON from the portal details endpoint as a raw string.</summary>
        Public Async Function GetDocumentDetailsRawJsonAsync(uuid As String) As Task(Of String)
            Await EnsureTokenAsync().ConfigureAwait(False)
            Dim url = $"{_settings.ActiveApiBaseUrl.TrimEnd("/"c)}/api/v1.0/documents/{uuid}/details"

            Using request As New HttpRequestMessage(HttpMethod.Get, url)
                request.Headers.Authorization = New AuthenticationHeaderValue("Bearer", _currentToken.AccessToken)
                Dim response = Await _httpClient.SendAsync(request).ConfigureAwait(False)
                Dim body = Await response.Content.ReadAsStringAsync().ConfigureAwait(False)

                If Not response.IsSuccessStatusCode Then
                    Throw New EtaApiException($"Failed to retrieve document details ({CInt(response.StatusCode)}).", body)
                End If

                Return body
            End Using
        End Function

        ''' <summary>
        ''' Searches submitted documents on the portal via GET /api/v1.0/documents/search
        ''' with optional filters; supports continuation token for paging.
        ''' </summary>
        Public Async Function SearchDocumentsAsync(Optional dateFrom As Date? = Nothing,
                                                     Optional dateTo As Date? = Nothing,
                                                     Optional status As String = Nothing,
                                                     Optional documentType As String = Nothing,
                                                     Optional continuationToken As String = Nothing,
                                                     Optional pageSize As Integer = 100) As Task(Of DocumentSearchResult)
            Await EnsureTokenAsync().ConfigureAwait(False)

            Dim queryParams As New List(Of String)()
            If dateFrom.HasValue Then queryParams.Add($"submissionDateFrom={dateFrom.Value:yyyy-MM-dd}")
            If dateTo.HasValue Then queryParams.Add($"submissionDateTo={dateTo.Value:yyyy-MM-dd}")
            If Not String.IsNullOrWhiteSpace(status) Then queryParams.Add($"status={Uri.EscapeDataString(status)}")
            If Not String.IsNullOrWhiteSpace(documentType) Then queryParams.Add($"documentType={Uri.EscapeDataString(documentType)}")
            queryParams.Add($"pageSize={pageSize}")

            Dim url = $"{_settings.ActiveApiBaseUrl.TrimEnd("/"c)}/api/v1.0/documents/search?{String.Join("&", queryParams)}"

            Using request As New HttpRequestMessage(HttpMethod.Get, url)
                request.Headers.Authorization = New AuthenticationHeaderValue("Bearer", _currentToken.AccessToken)
                If Not String.IsNullOrWhiteSpace(continuationToken) Then
                    request.Headers.Add("Continuation-Token", continuationToken)
                End If

                Dim response = Await _httpClient.SendAsync(request).ConfigureAwait(False)
                Dim body = Await response.Content.ReadAsStringAsync().ConfigureAwait(False)

                If Not response.IsSuccessStatusCode Then
                    Throw New EtaApiException($"Document search failed ({CInt(response.StatusCode)}).", body)
                End If

                Return JsonConvert.DeserializeObject(Of DocumentSearchResult)(body)
            End Using
        End Function

        ''' <summary>
        ''' Changes a document's state to Cancelled (issuer-side, within 24h+grace window) or
        ''' Rejected (receiver-side) via POST /api/v1.0/documents/{uuid}/state.
        ''' </summary>
        Public Async Function ChangeDocumentStateAsync(uuid As String, newStatus As String, reason As String) As Task(Of Boolean)
            Await EnsureTokenAsync().ConfigureAwait(False)
            Dim url = $"{_settings.ActiveApiBaseUrl.TrimEnd("/"c)}/api/v1.0/documents/{uuid}/state"

            Dim payload As New DocumentStateChangeRequest With {
                .Status = newStatus,
                .Reason = reason
            }
            Dim json = JsonConvert.SerializeObject(payload)

            Using request As New HttpRequestMessage(HttpMethod.Put, url)
                request.Headers.Authorization = New AuthenticationHeaderValue("Bearer", _currentToken.AccessToken)
                request.Content = New StringContent(json, Encoding.UTF8, "application/json")

                Dim response = Await _httpClient.SendAsync(request).ConfigureAwait(False)
                Dim body = Await response.Content.ReadAsStringAsync().ConfigureAwait(False)

                If Not response.IsSuccessStatusCode Then
                    _logger.Error("EtaApiClient.ChangeDocumentStateAsync", $"State change to '{newStatus}' failed for {uuid}: {body}")
                    Throw New EtaApiException($"Failed to set document status to '{newStatus}' ({CInt(response.StatusCode)}).", body)
                End If

                _logger.Info("EtaApiClient.ChangeDocumentStateAsync", $"Document {uuid} status changed to '{newStatus}'. Reason: {reason}")
                Return True
            End Using
        End Function

        ''' <summary>Downloads the printable PDF representation of a document.</summary>
        Public Async Function DownloadDocumentPdfAsync(uuid As String) As Task(Of Byte())
            Await EnsureTokenAsync().ConfigureAwait(False)
            Dim url = $"{_settings.ActiveApiBaseUrl.TrimEnd("/"c)}/api/v1.0/documents/{uuid}/pdf"

            Using request As New HttpRequestMessage(HttpMethod.Get, url)
                request.Headers.Authorization = New AuthenticationHeaderValue("Bearer", _currentToken.AccessToken)
                request.Headers.Accept.Add(New MediaTypeWithQualityHeaderValue("application/pdf"))

                Dim response = Await _httpClient.SendAsync(request).ConfigureAwait(False)

                If Not response.IsSuccessStatusCode Then
                    Dim body = Await response.Content.ReadAsStringAsync().ConfigureAwait(False)
                    Throw New EtaApiException($"Failed to download PDF ({CInt(response.StatusCode)}).", body)
                End If

                Return Await response.Content.ReadAsByteArrayAsync().ConfigureAwait(False)
            End Using
        End Function

        ''' <summary>
        ''' Retrieves the taxpayer's registered EGS (item) codes from the ETA code-list service,
        ''' if/when exposed by the SDK's reference data endpoints.
        ''' </summary>
        Public Async Function GetRegisteredItemCodesAsync() As Task(Of List(Of EgsCodeModel))
            Await EnsureTokenAsync().ConfigureAwait(False)
            Dim url = $"{_settings.ActiveApiBaseUrl.TrimEnd("/"c)}/api/v1.0/itemcodes"

            Using request As New HttpRequestMessage(HttpMethod.Get, url)
                request.Headers.Authorization = New AuthenticationHeaderValue("Bearer", _currentToken.AccessToken)

                Dim response = Await _httpClient.SendAsync(request).ConfigureAwait(False)
                Dim body = Await response.Content.ReadAsStringAsync().ConfigureAwait(False)

                If Not response.IsSuccessStatusCode Then
                    Throw New EtaApiException($"Failed to retrieve item codes ({CInt(response.StatusCode)}). This endpoint may not be available for your taxpayer profile - use the manual EGS Code Library instead.", body)
                End If

                Dim raw = JsonConvert.DeserializeObject(Of List(Of Dictionary(Of String, Object)))(body)
                Dim list As New List(Of EgsCodeModel)()
                For Each item In raw
                    list.Add(New EgsCodeModel With {
                        .EgsCode = If(item.ContainsKey("egsCode"), item("egsCode")?.ToString(), ""),
                        .Description = If(item.ContainsKey("description"), item("description")?.ToString(), ""),
                        .UnitType = If(item.ContainsKey("unitType"), item("unitType")?.ToString(), "EA")
                    })
                Next
                Return list
            End Using
        End Function

        Private Function ExtractErrorMessage(body As String) As String
            Try
                Dim parsed = JsonConvert.DeserializeObject(Of Dictionary(Of String, Object))(body)
                If parsed IsNot Nothing AndAlso parsed.ContainsKey("error_description") Then
                    Return parsed("error_description")?.ToString()
                End If
                If parsed IsNot Nothing AndAlso parsed.ContainsKey("error") Then
                    Return parsed("error")?.ToString()
                End If
            Catch
            End Try
            Return body
        End Function

        Public Sub Dispose() Implements IDisposable.Dispose
            _httpClient?.Dispose()
        End Sub

    End Class

    ''' <summary>Exception carrying the raw ETA API error response body for diagnostics.</summary>
    Public Class EtaApiException
        Inherits ApplicationException

        Public Property ResponseBody As String

        Public Sub New(message As String, responseBody As String)
            MyBase.New(message)
            Me.ResponseBody = responseBody
        End Sub
    End Class

End Namespace
