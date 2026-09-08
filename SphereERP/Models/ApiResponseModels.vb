Option Strict On
Imports Newtonsoft.Json

Namespace Models

    ''' <summary>
    ''' Result of OAuth2 client-credentials token request to ETA identity server.
    ''' </summary>
    Public Class TokenResponse
        <JsonProperty("access_token")>
        Public Property AccessToken As String

        <JsonProperty("token_type")>
        Public Property TokenType As String

        <JsonProperty("expires_in")>
        Public Property ExpiresIn As Integer

        <JsonIgnore>
        Public Property AcquiredAtUtc As DateTime

        <JsonIgnore>
        Public ReadOnly Property ExpiresAtUtc As DateTime
            Get
                Return AcquiredAtUtc.AddSeconds(ExpiresIn)
            End Get
        End Property

        <JsonIgnore>
        Public ReadOnly Property IsExpired As Boolean
            Get
                Return DateTime.UtcNow >= ExpiresAtUtc.AddSeconds(-30)
            End Get
        End Property

        Public Sub New()
            AccessToken = ""
            TokenType = "Bearer"
            ExpiresIn = 0
            AcquiredAtUtc = DateTime.UtcNow
        End Sub
    End Class

    ''' <summary>
    ''' Result of POST /api/v1/documentsubmissions
    ''' </summary>
    Public Class SubmissionResponse
        <JsonProperty("submissionId")>
        Public Property SubmissionId As Long?

        <JsonProperty("acceptedDocuments")>
        Public Property AcceptedDocuments As List(Of AcceptedDocumentModel)

        <JsonProperty("rejectedDocuments")>
        Public Property RejectedDocuments As List(Of RejectedDocumentModel)

        Public Sub New()
            SubmissionId = 0
            AcceptedDocuments = New List(Of AcceptedDocumentModel)()
            RejectedDocuments = New List(Of RejectedDocumentModel)()
        End Sub
    End Class

    Public Class AcceptedDocumentModel
        <JsonProperty("uuid")>
        Public Property Uuid As String

        <JsonProperty("submissionUUID")>
        Public Property SubmissionUuid As String

        <JsonProperty("longId")>
        Public Property LongId As String

        <JsonProperty("internalId")>
        Public Property InternalId As String

        <JsonProperty("hashKey")>
        Public Property HashKey As String

        Public Sub New()
            Uuid = ""
            SubmissionUuid = ""
            LongId = ""
            InternalId = ""
            HashKey = ""
        End Sub
    End Class

    Public Class RejectedDocumentModel
        <JsonProperty("internalId")>
        Public Property InternalId As String

        <JsonProperty("error")>
        Public Property [Error] As RejectionErrorModel

        Public Sub New()
            InternalId = ""
            [Error] = New RejectionErrorModel()
        End Sub
    End Class

    Public Class RejectionErrorModel
        <JsonProperty("code")>
        Public Property Code As String

        <JsonProperty("message")>
        Public Property Message As String

        <JsonProperty("target")>
        Public Property Target As String

        <JsonProperty("details")>
        Public Property Details As List(Of RejectionErrorDetailModel)

        Public Sub New()
            Code = ""
            Message = ""
            Target = ""
            Details = New List(Of RejectionErrorDetailModel)()
        End Sub
    End Class

    Public Class RejectionErrorDetailModel
        <JsonProperty("code")>
        Public Property Code As String

        <JsonProperty("message")>
        Public Property Message As String

        <JsonProperty("target")>
        Public Property Target As String

        Public Sub New()
            Code = ""
            Message = ""
            Target = ""
        End Sub
    End Class

    ''' <summary>
    ''' Single document item as returned by GET /api/v1.0/documents/search or recent documents.
    ''' </summary>
    Public Class DocumentSearchItem
        <JsonProperty("uuid")>
        Public Property Uuid As String

        <JsonProperty("submissionUUID")>
        Public Property SubmissionUuid As String

        <JsonProperty("longId")>
        Public Property LongId As String

        <JsonProperty("internalId")>
        Public Property InternalId As String

        <JsonProperty("typeName")>
        Public Property TypeName As String ' I, C, D

        <JsonProperty("typeVersionName")>
        Public Property TypeVersionName As String

        <JsonProperty("issuerId")>
        Public Property IssuerId As String

        <JsonProperty("issuerName")>
        Public Property IssuerName As String

        <JsonProperty("receiverId")>
        Public Property ReceiverId As String

        <JsonProperty("receiverName")>
        Public Property ReceiverName As String

        <JsonProperty("dateTimeIssued")>
        Public Property DateTimeIssued As DateTime?

        <JsonProperty("dateTimeReceived")>
        Public Property DateTimeReceived As DateTime?

        <JsonProperty("totalAmount")>
        Public Property TotalAmount As Decimal

        <JsonProperty("totalDiscount")>
        Public Property TotalDiscount As Decimal

        <JsonProperty("netAmount")>
        Public Property NetAmount As Decimal

        <JsonProperty("total")>
        Public Property Total As Decimal

        <JsonProperty("status")>
        Public Property Status As String ' Valid, Invalid, Cancelled, Rejected, Submitted

        <JsonProperty("documentStatusReason")>
        Public Property DocumentStatusReason As String

        <JsonProperty("currency")>
        Public Property Currency As String

        Public Sub New()
            Uuid = ""
            SubmissionUuid = ""
            LongId = ""
            InternalId = ""
            TypeName = ""
            TypeVersionName = ""
            IssuerId = ""
            IssuerName = ""
            ReceiverId = ""
            ReceiverName = ""
            Status = ""
            DocumentStatusReason = ""
            Currency = "EGP"
        End Sub
    End Class

    Public Class DocumentSearchResult
        <JsonProperty("result")>
        Public Property Result As List(Of DocumentSearchItem)

        <JsonProperty("metadata")>
        Public Property Metadata As SearchMetadata

        Public Sub New()
            Result = New List(Of DocumentSearchItem)()
            Metadata = New SearchMetadata()
        End Sub
    End Class

    Public Class SearchMetadata
        <JsonProperty("continuationToken")>
        Public Property ContinuationToken As String

        <JsonProperty("returnedCount")>
        Public Property ReturnedCount As Integer

        <JsonProperty("totalCount")>
        Public Property TotalCount As Integer

        Public Sub New()
            ContinuationToken = ""
            ReturnedCount = 0
            TotalCount = 0
        End Sub
    End Class

    ''' <summary>
    ''' Request body for cancel/reject document state-change call.
    ''' </summary>
    Public Class DocumentStateChangeRequest
        <JsonProperty("status")>
        Public Property Status As String ' "cancelled" or "rejected"

        <JsonProperty("reason")>
        Public Property Reason As String

        Public Sub New()
            Status = ""
            Reason = ""
        End Sub
    End Class

    ''' <summary>
    ''' Represents an Item / EGS code as listed in the ETA Code Lists portal.
    ''' </summary>
    Public Class EgsCodeModel
        Public Property EgsCode As String
        Public Property Description As String
        Public Property UnitType As String
        Public Property DefaultPrice As Decimal
        Public Property DefaultTaxType As String
        Public Property DefaultTaxRate As Decimal
        Public Property Category As String
        Public Property IsActive As Boolean
        Public Property CreatedAtUtc As DateTime

        Public Sub New()
            EgsCode = ""
            Description = ""
            UnitType = "EA"
            DefaultPrice = 0D
            DefaultTaxType = "T1"
            DefaultTaxRate = 14D
            Category = ""
            IsActive = True
            CreatedAtUtc = DateTime.UtcNow
        End Sub
    End Class

End Namespace
