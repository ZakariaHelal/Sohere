Option Strict On
Imports SphereERP.Data
Imports SphereERP.Models

Namespace Services

    ''' <summary>
    ''' Retrieves documents from the ETA portal search API and stores them in dbo.Documents.
    ''' </summary>
    Public Class PortalDocumentSyncService

        Private ReadOnly _settings As AppSettings
        Private ReadOnly _apiClient As EtaApiClient
        Private ReadOnly _docRepo As New DocumentRepository()
        Private ReadOnly _logger As New AppLogRepository()

        Public Sub New(settings As AppSettings, apiClient As EtaApiClient)
            _settings = settings
            _apiClient = apiClient
        End Sub

        Public Class SyncResult
            Public Property RetrievedCount As Integer
            Public Property InsertedCount As Integer
            Public Property SkippedCount As Integer
            Public Property SentCount As Integer
            Public Property ReceivedCount As Integer

            Public Sub New()
                RetrievedCount = 0
                InsertedCount = 0
                SkippedCount = 0
                SentCount = 0
                ReceivedCount = 0
            End Sub

            Public ReadOnly Property DirectionSummary As String
                Get
                    Dim parts As New List(Of String)
                    If SentCount > 0 Then parts.Add($"{SentCount} sent")
                    If ReceivedCount > 0 Then parts.Add($"{ReceivedCount} received")
                    Return If(parts.Count > 0, String.Join(", ", parts), "direction unknown")
                End Get
            End Property
        End Class

        Public Async Function SyncAllDocumentsAsync(Optional dateFrom As Date? = Nothing,
                                                     Optional dateTo As Date? = Nothing,
                                                     Optional pageSize As Integer = 100,
                                                     Optional onProgress As IProgress(Of Integer) = Nothing) As Task(Of SyncResult)
            Dim result As New SyncResult()
            Dim continuationToken As String = Nothing
            Dim pageNumber = 0
            Dim totalCount As Integer = 0

            Do
                pageNumber += 1
                Dim page = Await _apiClient.SearchDocumentsAsync(
                    dateFrom:=dateFrom,
                    dateTo:=dateTo,
                    continuationToken:=continuationToken,
                    pageSize:=pageSize).ConfigureAwait(False)

                If page Is Nothing OrElse page.Result Is Nothing OrElse page.Result.Count = 0 Then
                    Exit Do
                End If

                If totalCount = 0 AndAlso page.Metadata IsNot Nothing AndAlso page.Metadata.TotalCount > 0 Then
                    totalCount = page.Metadata.TotalCount
                End If

                For Each item In page.Result
                    result.RetrievedCount += 1
                    Dim action = _docRepo.UpsertPortalDocument(item, _settings.Environment.ToString())
                    If action = PortalDocumentSyncAction.Inserted Then
                        result.InsertedCount += 1
                    Else
                        result.SkippedCount += 1
                    End If

                    If String.Equals(item.IssuerId, _settings.TaxpayerRIN, StringComparison.OrdinalIgnoreCase) Then
                        result.SentCount += 1
                    ElseIf String.Equals(item.ReceiverId, _settings.TaxpayerRIN, StringComparison.OrdinalIgnoreCase) Then
                        result.ReceivedCount += 1
                    End If

                    If onProgress IsNot Nothing AndAlso totalCount > 0 Then
                        Dim pct = Math.Min(CInt(result.RetrievedCount * 100 / totalCount), 100)
                        onProgress.Report(pct)
                    End If
                Next

                continuationToken = If(page.Metadata IsNot Nothing, page.Metadata.ContinuationToken, "")

                If String.IsNullOrWhiteSpace(continuationToken) OrElse page.Result.Count < pageSize Then
                    Exit Do
                End If
            Loop

            _logger.Info("PortalDocumentSyncService.SyncAllDocumentsAsync",
                         $"Synced {result.RetrievedCount} portal document(s); inserted={result.InsertedCount}, skipped={result.SkippedCount}. Direction: {result.DirectionSummary}.")

            Return result
        End Function

    End Class

End Namespace
