Option Strict On
Imports Microsoft.Data.SqlClient
Imports SphereERP.Models
Imports Newtonsoft.Json

Namespace Data

    ''' <summary>
    ''' Data access for the Documents table: local cache of submitted
    ''' invoices/credit notes/debit notes, their status and full JSON payloads.
    ''' </summary>
    Public Enum PortalDocumentSyncAction
        Inserted
        AlreadyExists
    End Enum

    Public Class DocumentRepository

        ''' <summary>Inserts a new local document record (status = Draft or Submitted) and returns its Id.</summary>
        Public Function Insert(doc As EInvoiceDocument, environmentName As String, status As String, documentJson As String) As Integer
            Using conn = DbConnectionFactory.CreateOpenConnection()
                Dim sql As String = "
                    INSERT INTO dbo.Documents
                        (InternalId, DocumentType, IssuerName, IssuerRIN, ReceiverName, ReceiverRIN,
                         DateTimeIssued, TotalAmount, NetAmount, TaxAmount, Currency, Environment, Status, DocumentJson)
                    OUTPUT INSERTED.Id
                    VALUES
                        (@InternalId, @DocumentType, @IssuerName, @IssuerRIN, @ReceiverName, @ReceiverRIN,
                         @DateTimeIssued, @TotalAmount, @NetAmount, @TaxAmount, @Currency, @Environment, @Status, @DocumentJson);"

                Using cmd As New SqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@InternalId", doc.InternalID)
                    cmd.Parameters.AddWithValue("@DocumentType", doc.DocumentType)
                    cmd.Parameters.AddWithValue("@IssuerName", doc.Issuer.Name)
                    cmd.Parameters.AddWithValue("@IssuerRIN", doc.Issuer.Id)
                    cmd.Parameters.AddWithValue("@ReceiverName", If(doc.Receiver.Name, ""))
                    cmd.Parameters.AddWithValue("@ReceiverRIN", If(doc.Receiver.Id, ""))
                    cmd.Parameters.AddWithValue("@DateTimeIssued", doc.DateTimeIssued)
                    cmd.Parameters.AddWithValue("@TotalAmount", doc.TotalAmount)
                    cmd.Parameters.AddWithValue("@NetAmount", doc.NetAmount)
                    cmd.Parameters.AddWithValue("@TaxAmount", doc.TaxTotals.Sum(Function(t) t.Amount))
                    cmd.Parameters.AddWithValue("@Currency", "EGP")
                    cmd.Parameters.AddWithValue("@Environment", environmentName)
                    cmd.Parameters.AddWithValue("@Status", status)
                    cmd.Parameters.AddWithValue("@DocumentJson", documentJson)

                    Return CInt(cmd.ExecuteScalar())
                End Using
            End Using
        End Function

        ''' <summary>Updates document status, response JSON and ETA identifiers after a submission/status check.</summary>
        Public Sub UpdateSubmissionResult(documentId As Integer, status As String, statusReason As String,
                                           uuid As String, submissionUuid As String, longId As String, responseJson As String)
            Using conn = DbConnectionFactory.CreateOpenConnection()
                Dim sql As String = "
                    UPDATE dbo.Documents SET
                        Status = @Status,
                        StatusReason = @StatusReason,
                        Uuid = @Uuid,
                        SubmissionUuid = @SubmissionUuid,
                        LongId = @LongId,
                        ResponseJson = @ResponseJson,
                        UpdatedAtUtc = SYSUTCDATETIME(),
                        LastStatusCheckUtc = SYSUTCDATETIME()
                    WHERE Id = @Id;"

                Using cmd As New SqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@Status", status)
                    cmd.Parameters.AddWithValue("@StatusReason", If(statusReason, CType(DBNull.Value, Object)))
                    cmd.Parameters.AddWithValue("@Uuid", If(uuid, CType(DBNull.Value, Object)))
                    cmd.Parameters.AddWithValue("@SubmissionUuid", If(submissionUuid, CType(DBNull.Value, Object)))
                    cmd.Parameters.AddWithValue("@LongId", If(longId, CType(DBNull.Value, Object)))
                    cmd.Parameters.AddWithValue("@ResponseJson", If(responseJson, CType(DBNull.Value, Object)))
                    cmd.Parameters.AddWithValue("@Id", documentId)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        End Sub

        ''' <summary>Updates only status + reason (used by status polling and cancel/reject).</summary>
        Public Sub UpdateStatus(documentId As Integer, status As String, statusReason As String, Optional responseJson As String = Nothing)
            Using conn = DbConnectionFactory.CreateOpenConnection()
                Dim sql As String = "
                    UPDATE dbo.Documents SET
                        Status = @Status,
                        StatusReason = @StatusReason,
                        ResponseJson = @ResponseJson,
                        UpdatedAtUtc = SYSUTCDATETIME(),
                        LastStatusCheckUtc = SYSUTCDATETIME()
                    WHERE Id = @Id;"
                Using cmd As New SqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@Status", status)
                    cmd.Parameters.AddWithValue("@StatusReason", If(statusReason, CType(DBNull.Value, Object)))
                    cmd.Parameters.AddWithValue("@ResponseJson", If(responseJson, CType(DBNull.Value, Object)))
                    cmd.Parameters.AddWithValue("@Id", documentId)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        End Sub

        ''' <summary>
        ''' Inserts a document from the ETA portal search API if it does not already exist.
        ''' Uses UUID as the primary match key when available; otherwise falls back to InternalId.
        ''' If the document already exists, it is skipped entirely (no update).
        ''' </summary>
        Public Function UpsertPortalDocument(item As DocumentSearchItem, environmentName As String) As PortalDocumentSyncAction
            Using conn = DbConnectionFactory.CreateOpenConnection()
                Dim existingId As Integer? = FindExistingPortalDocumentId(conn, item, environmentName)

                If existingId.HasValue Then
                    Return PortalDocumentSyncAction.AlreadyExists
                End If

                Dim responseJson = JsonConvert.SerializeObject(item, Formatting.Indented)
                Dim status = If(String.IsNullOrWhiteSpace(item.Status), "Submitted", item.Status)
                Dim documentType = NormalizeDocumentType(item.TypeName)

                Dim insertSql As String = "
                    INSERT INTO dbo.Documents
                        (InternalId, DocumentType, Uuid, SubmissionUuid, LongId,
                         IssuerName, IssuerRIN, ReceiverName, ReceiverRIN,
                         DateTimeIssued, TotalAmount, NetAmount, TaxAmount, Currency,
                         Environment, Status, StatusReason, DocumentJson, ResponseJson, LastStatusCheckUtc)
                    VALUES
                        (@InternalId, @DocumentType, @Uuid, @SubmissionUuid, @LongId,
                         @IssuerName, @IssuerRIN, @ReceiverName, @ReceiverRIN,
                         @DateTimeIssued, @TotalAmount, @NetAmount, @TaxAmount, @Currency,
                         @Environment, @Status, @StatusReason, @DocumentJson, @ResponseJson, SYSUTCDATETIME());"

                Using cmd As New SqlCommand(insertSql, conn)
                    AddPortalDocumentParameters(cmd, item, environmentName, status, documentType, responseJson)
                    cmd.Parameters.AddWithValue("@DocumentJson", responseJson)
                    cmd.ExecuteNonQuery()
                End Using

                Return PortalDocumentSyncAction.Inserted
            End Using
        End Function

        Private Function FindExistingPortalDocumentId(conn As SqlConnection, item As DocumentSearchItem, environmentName As String) As Integer?
            If Not String.IsNullOrWhiteSpace(item.Uuid) Then
                Using cmd As New SqlCommand("SELECT TOP (1) Id FROM dbo.Documents WHERE Uuid = @Uuid AND Environment = @Environment", conn)
                    cmd.Parameters.AddWithValue("@Uuid", item.Uuid)
                    cmd.Parameters.AddWithValue("@Environment", environmentName)
                    Dim result = cmd.ExecuteScalar()
                    If result IsNot Nothing AndAlso result IsNot DBNull.Value Then Return CInt(result)
                End Using
            End If

            If Not String.IsNullOrWhiteSpace(item.InternalId) Then
                Using cmd As New SqlCommand("SELECT TOP (1) Id FROM dbo.Documents WHERE InternalId = @InternalId AND Environment = @Environment ORDER BY Id DESC", conn)
                    cmd.Parameters.AddWithValue("@InternalId", item.InternalId)
                    cmd.Parameters.AddWithValue("@Environment", environmentName)
                    Dim result = cmd.ExecuteScalar()
                    If result IsNot Nothing AndAlso result IsNot DBNull.Value Then Return CInt(result)
                End Using
            End If

            Return Nothing
        End Function

        Private Sub AddPortalDocumentParameters(cmd As SqlCommand, item As DocumentSearchItem, environmentName As String,
                                                status As String, documentType As String, responseJson As String)
            cmd.Parameters.AddWithValue("@InternalId", If(item.InternalId, ""))
            cmd.Parameters.AddWithValue("@DocumentType", documentType)
            cmd.Parameters.AddWithValue("@Uuid", If(String.IsNullOrWhiteSpace(item.Uuid), CType(DBNull.Value, Object), item.Uuid))
            cmd.Parameters.AddWithValue("@SubmissionUuid", If(String.IsNullOrWhiteSpace(item.SubmissionUuid), CType(DBNull.Value, Object), item.SubmissionUuid))
            cmd.Parameters.AddWithValue("@LongId", If(String.IsNullOrWhiteSpace(item.LongId), CType(DBNull.Value, Object), item.LongId))
            cmd.Parameters.AddWithValue("@IssuerName", If(item.IssuerName, ""))
            cmd.Parameters.AddWithValue("@IssuerRIN", If(item.IssuerId, ""))
            cmd.Parameters.AddWithValue("@ReceiverName", If(item.ReceiverName, ""))
            cmd.Parameters.AddWithValue("@ReceiverRIN", If(item.ReceiverId, ""))
            cmd.Parameters.AddWithValue("@DateTimeIssued", If(item.DateTimeIssued.HasValue, CType(item.DateTimeIssued.Value, Object), DBNull.Value))
            cmd.Parameters.AddWithValue("@TotalAmount", item.Total)
            cmd.Parameters.AddWithValue("@NetAmount", item.NetAmount)
            cmd.Parameters.AddWithValue("@TaxAmount", Math.Max(0D, item.Total - item.NetAmount))
            cmd.Parameters.AddWithValue("@Currency", If(String.IsNullOrWhiteSpace(item.Currency), "EGP", item.Currency))
            cmd.Parameters.AddWithValue("@Environment", environmentName)
            cmd.Parameters.AddWithValue("@Status", status)
            cmd.Parameters.AddWithValue("@StatusReason", If(String.IsNullOrWhiteSpace(item.DocumentStatusReason), CType(DBNull.Value, Object), item.DocumentStatusReason))
            cmd.Parameters.AddWithValue("@ResponseJson", responseJson)
        End Sub

        Private Function NormalizeDocumentType(typeName As String) As String
            If String.IsNullOrWhiteSpace(typeName) Then Return "I"
            Dim value = typeName.Trim()

            Select Case value.ToUpperInvariant()
                Case "I", "INVOICE"
                    Return "I"
                Case "C", "CREDIT", "CREDIT NOTE", "CREDITNOTE"
                    Return "C"
                Case "D", "DEBIT", "DEBIT NOTE", "DEBITNOTE"
                    Return "D"
                Case "EI", "EXPORT INVOICE", "EXPORTINVOICE"
                    Return "EI"
                Case "EC", "EXPORT CREDIT NOTE", "EXPORT CREDITNOTE"
                    Return "EC"
                Case "ED", "EXPORT DEBIT NOTE", "EXPORT DEBITNOTE"
                    Return "ED"
                Case Else
                    Return value
            End Select
        End Function

        ''' <summary>Searches local documents with optional filters. Returns a lightweight result set for the grid.</summary>
        Public Function Search(Optional internalId As String = Nothing,
                                Optional documentType As String = Nothing,
                                Optional status As String = Nothing,
                                Optional dateFrom As Date? = Nothing,
                                Optional dateTo As Date? = Nothing,
                                Optional receiverName As String = Nothing,
                                Optional issuerName As String = Nothing,
                                Optional environmentName As String = Nothing) As List(Of DocumentRow)

            Dim results As New List(Of DocumentRow)()

            Using conn = DbConnectionFactory.CreateOpenConnection()
                Dim sql As String = "
                    SELECT Id, InternalId, DocumentType, Uuid, SubmissionUuid, LongId,
                           IssuerName, IssuerRIN, ReceiverName, ReceiverRIN, DateTimeIssued,
                           TotalAmount, NetAmount, TaxAmount, Currency, Environment, Status,
                           StatusReason, CreatedAtUtc, UpdatedAtUtc
                    FROM dbo.Documents
                    WHERE 1 = 1"

                If Not String.IsNullOrWhiteSpace(internalId) Then sql &= " AND InternalId LIKE @InternalId"
                If Not String.IsNullOrWhiteSpace(documentType) Then sql &= " AND DocumentType = @DocumentType"
                If Not String.IsNullOrWhiteSpace(status) Then sql &= " AND Status = @Status"
                If dateFrom.HasValue Then sql &= " AND DateTimeIssued >= @DateFrom"
                If dateTo.HasValue Then sql &= " AND DateTimeIssued <= @DateTo"
                If Not String.IsNullOrWhiteSpace(receiverName) Then sql &= " AND ReceiverName LIKE @ReceiverName"
                If Not String.IsNullOrWhiteSpace(issuerName) Then sql &= " AND IssuerName LIKE @IssuerName"
                If Not String.IsNullOrWhiteSpace(environmentName) Then sql &= " AND Environment = @Environment"

                sql &= " ORDER BY CreatedAtUtc DESC"

                Using cmd As New SqlCommand(sql, conn)
                    If Not String.IsNullOrWhiteSpace(internalId) Then cmd.Parameters.AddWithValue("@InternalId", "%" & internalId & "%")
                    If Not String.IsNullOrWhiteSpace(documentType) Then cmd.Parameters.AddWithValue("@DocumentType", documentType)
                    If Not String.IsNullOrWhiteSpace(status) Then cmd.Parameters.AddWithValue("@Status", status)
                    If dateFrom.HasValue Then cmd.Parameters.AddWithValue("@DateFrom", dateFrom.Value.Date)
                    If dateTo.HasValue Then cmd.Parameters.AddWithValue("@DateTo", dateTo.Value.Date.AddDays(1).AddSeconds(-1))
                    If Not String.IsNullOrWhiteSpace(receiverName) Then cmd.Parameters.AddWithValue("@ReceiverName", "%" & receiverName & "%")
                    If Not String.IsNullOrWhiteSpace(issuerName) Then cmd.Parameters.AddWithValue("@IssuerName", "%" & issuerName & "%")
                    If Not String.IsNullOrWhiteSpace(environmentName) Then cmd.Parameters.AddWithValue("@Environment", environmentName)

                    Using reader = cmd.ExecuteReader()
                        While reader.Read()
                            results.Add(New DocumentRow With {
                                .Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                .InternalId = reader.GetString(reader.GetOrdinal("InternalId")),
                                .DocumentType = reader.GetString(reader.GetOrdinal("DocumentType")),
                                .Uuid = GetNullableString(reader, "Uuid"),
                                .SubmissionUuid = GetNullableString(reader, "SubmissionUuid"),
                                .LongId = GetNullableString(reader, "LongId"),
                                .IssuerName = GetNullableString(reader, "IssuerName"),
                                .IssuerRIN = GetNullableString(reader, "IssuerRIN"),
                                .ReceiverName = GetNullableString(reader, "ReceiverName"),
                                .ReceiverRIN = GetNullableString(reader, "ReceiverRIN"),
                                .DateTimeIssued = If(reader.IsDBNull(reader.GetOrdinal("DateTimeIssued")), CType(Nothing, Date?), reader.GetDateTime(reader.GetOrdinal("DateTimeIssued"))),
                                .TotalAmount = If(reader.IsDBNull(reader.GetOrdinal("TotalAmount")), 0D, reader.GetDecimal(reader.GetOrdinal("TotalAmount"))),
                                .NetAmount = If(reader.IsDBNull(reader.GetOrdinal("NetAmount")), 0D, reader.GetDecimal(reader.GetOrdinal("NetAmount"))),
                                .TaxAmount = If(reader.IsDBNull(reader.GetOrdinal("TaxAmount")), 0D, reader.GetDecimal(reader.GetOrdinal("TaxAmount"))),
                                .Currency = GetNullableString(reader, "Currency"),
                                .Environment = reader.GetString(reader.GetOrdinal("Environment")),
                                .Status = reader.GetString(reader.GetOrdinal("Status")),
                                .StatusReason = GetNullableString(reader, "StatusReason"),
                                .CreatedAtUtc = reader.GetDateTime(reader.GetOrdinal("CreatedAtUtc")),
                                .UpdatedAtUtc = reader.GetDateTime(reader.GetOrdinal("UpdatedAtUtc"))
                            })
                        End While
                    End Using
                End Using
            End Using

            Return results
        End Function

        ''' <summary>Retrieves the full stored document JSON for re-display / PDF generation.</summary>
        Public Function GetDocumentJson(documentId As Integer) As String
            Using conn = DbConnectionFactory.CreateOpenConnection()
                Using cmd As New SqlCommand("SELECT DocumentJson FROM dbo.Documents WHERE Id = @Id", conn)
                    cmd.Parameters.AddWithValue("@Id", documentId)
                    Dim result = cmd.ExecuteScalar()
                    Return If(result Is Nothing, "", CStr(result))
                End Using
            End Using
        End Function

        ''' <summary>Retrieves invoice line items for a document by parsing its stored JSON.</summary>
        Public Function GetDocumentLineItems(documentId As Integer) As List(Of InvoiceLineModel)
            Dim json = GetDocumentJson(documentId)
            If String.IsNullOrWhiteSpace(json) Then Return New List(Of InvoiceLineModel)()
            Try
                Dim doc = JsonConvert.DeserializeObject(Of EInvoiceDocument)(json)
                Return If(doc?.InvoiceLines, New List(Of InvoiceLineModel)())
            Catch
                Return New List(Of InvoiceLineModel)()
            End Try
        End Function

        ''' <summary>Replaces the stored document JSON with new content (e.g. after fetching full details from portal).</summary>
        Public Sub UpdateDocumentJson(documentId As Integer, json As String)
            Using conn = DbConnectionFactory.CreateOpenConnection()
                Using cmd As New SqlCommand("UPDATE dbo.Documents SET DocumentJson = @DocumentJson WHERE Id = @Id", conn)
                    cmd.Parameters.AddWithValue("@DocumentJson", json)
                    cmd.Parameters.AddWithValue("@Id", documentId)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        End Sub

        ''' <summary>Retrieves a single document row by Id.</summary>
        Public Function GetById(documentId As Integer) As DocumentRow
            Dim rows = Search()
            Return rows.FirstOrDefault(Function(r) r.Id = documentId)
        End Function

        ''' <summary>
        ''' Determines whether the given internal ID was already used for this issuer in
        ''' this environment. ETA enforces unique internal IDs per issuer and rejects duplicates.
        ''' </summary>
        Public Function InternalIdExists(internalId As String, issuerRin As String, environmentName As String) As Boolean
            If String.IsNullOrWhiteSpace(internalId) Then Return False

            Using conn = DbConnectionFactory.CreateOpenConnection()
                Using cmd As New SqlCommand(
                    "SELECT COUNT(1) FROM dbo.Documents " &
                    "WHERE InternalId = @InternalId AND IssuerRIN = @IssuerRIN AND Environment = @Environment", conn)

                    cmd.Parameters.AddWithValue("@InternalId", internalId)
                    cmd.Parameters.AddWithValue("@IssuerRIN", If(issuerRin, ""))
                    cmd.Parameters.AddWithValue("@Environment", environmentName)
                    Return CInt(cmd.ExecuteScalar()) > 0
                End Using
            End Using
        End Function

        ''' <summary>Finds a locally cached document by its ETA UUID (most recent match), or Nothing.</summary>
        Public Function GetByUuid(uuid As String) As DocumentRow
            If String.IsNullOrWhiteSpace(uuid) Then Return Nothing

            Using conn = DbConnectionFactory.CreateOpenConnection()
                Using cmd As New SqlCommand("SELECT TOP (1) Id FROM dbo.Documents WHERE Uuid = @Uuid ORDER BY Id DESC", conn)
                    cmd.Parameters.AddWithValue("@Uuid", uuid)
                    Dim result = cmd.ExecuteScalar()
                    If result Is Nothing OrElse result Is DBNull.Value Then Return Nothing
                    Return GetById(CInt(result))
                End Using
            End Using
        End Function

        Private Function GetNullableString(reader As SqlDataReader, columnName As String) As String
            Dim ordinal = reader.GetOrdinal(columnName)
            If reader.IsDBNull(ordinal) Then Return ""
            Return reader.GetString(ordinal)
        End Function

    End Class

    ''' <summary>Lightweight row used to populate document search/list grids.</summary>
    Public Class DocumentRow
        Public Property Id As Integer
        Public Property InternalId As String
        Public Property DocumentType As String
        Public Property Uuid As String
        Public Property SubmissionUuid As String
        Public Property LongId As String
        Public Property IssuerName As String
        Public Property IssuerRIN As String
        Public Property ReceiverName As String
        Public Property ReceiverRIN As String
        Public Property DateTimeIssued As Date?
        Public Property TotalAmount As Decimal
        Public Property NetAmount As Decimal
        Public Property TaxAmount As Decimal
        Public Property Currency As String
        Public Property Environment As String
        Public Property Status As String
        Public Property StatusReason As String
        Public Property CreatedAtUtc As DateTime
        Public Property UpdatedAtUtc As DateTime
        Public Property Direction As String

        ''' <summary>Friendly display name for the document type.</summary>
        Public ReadOnly Property DocumentTypeDisplay As String
            Get
                Select Case DocumentType.ToUpperInvariant()
                    Case "I" : Return "Invoice"
                    Case "C" : Return "Credit Note"
                    Case "D" : Return "Debit Note"
                    Case "EI" : Return "Export Invoice"
                    Case "EC" : Return "Export Credit Note"
                    Case "ED" : Return "Export Debit Note"
                    Case Else : Return DocumentType
                End Select
            End Get
        End Property
    End Class

End Namespace
