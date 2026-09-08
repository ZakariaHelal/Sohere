Option Strict On

Namespace Models

    Public Enum ApiEnvironment
        Preproduction
        Production
    End Enum

    ''' <summary>
    ''' Holds environment selection, client credentials, certificate selection and notification settings.
    ''' Persisted (encrypted) via SettingsService.
    ''' </summary>
    Public Class AppSettings
        Public Property Environment As ApiEnvironment

        ' Preproduction credentials
        Public Property PreprodClientId As String
        Public Property PreprodClientSecret As String
        Public Property PreprodBaseUrl As String
        Public Property PreprodIdentityUrl As String

        ' Production credentials
        Public Property ProdClientId As String
        Public Property ProdClientSecret As String
        Public Property ProdBaseUrl As String
        Public Property ProdIdentityUrl As String

        ' Taxpayer info
        Public Property TaxpayerRIN As String
        Public Property TaxpayerName As String
        Public Property DefaultActivityCode As String
        Public Property DefaultBranchId As String
        Public Property IssuerGovernate As String
        Public Property IssuerRegionCity As String
        Public Property IssuerStreet As String
        Public Property IssuerBuildingNumber As String

        ' Certificate (USB token / HSM) selection - stored as certificate thumbprint
        Public Property SigningCertThumbprint As String
        Public Property SigningCertStoreName As String ' "My" (Personal) typically

        ' Notification settings
        Public Property NotifyOnValidation As Boolean
        Public Property NotifyOnIssuance As Boolean
        Public Property NotifyOnRejection As Boolean
        Public Property NotifyOnCancellation As Boolean
        Public Property NotificationEmail As String
        Public Property NotificationPollingMinutes As Integer
        Public Property EnableDesktopToast As Boolean
        Public Property EnableEmailNotifications As Boolean
        Public Property SmtpHost As String
        Public Property SmtpPort As Integer
        Public Property SmtpUsername As String
        Public Property SmtpPassword As String
        Public Property SmtpUseSsl As Boolean

        ' License
        Public Property LicenseKey As String
        Public Property LicenseServerUrl As String

        ' Database — individual fields (UI-friendly)
        Public Property DbUseLocalDb As Boolean = False
        Public Property DbServer As String = "192.168.1.2"
        Public Property DbPort As Integer = 1433
        Public Property DbUsername As String = "za"
        Public Property DbPassword As String = "Alhayat2020"
        Public Property DbName As String = "ETAInvoicingDB"

        ' Legacy — still persisted for backward compat, rebuilt from individual fields on save
        Public Property DatabaseConnectionString As String

        Public Function BuildConnectionString() As String
            If DbUseLocalDb Then
                Return $"Server=(localdb)\MSSQLLocalDB;Database={DbName};Integrated Security=True;TrustServerCertificate=True;"
            Else
                Return $"Data Source={DbServer},{DbPort};Initial Catalog={DbName};User ID={DbUsername};Password={DbPassword};Trusted_Connection=False;TrustServerCertificate=True;"
            End If
        End Function

        Public Sub New()
            Environment = ApiEnvironment.Preproduction

            PreprodClientId = ""
            PreprodClientSecret = ""
            PreprodBaseUrl = "https://api.preprod.invoicing.eta.gov.eg"
            PreprodIdentityUrl = "https://id.preprod.eta.gov.eg"

            ProdClientId = ""
            ProdClientSecret = ""
            ProdBaseUrl = "https://api.invoicing.eta.gov.eg"
            ProdIdentityUrl = "https://id.eta.gov.eg"

            TaxpayerRIN = ""
            TaxpayerName = ""
            DefaultActivityCode = ""
            DefaultBranchId = "0"
            IssuerGovernate = ""
            IssuerRegionCity = ""
            IssuerStreet = ""
            IssuerBuildingNumber = ""

            SigningCertThumbprint = ""
            SigningCertStoreName = "My"

            NotifyOnValidation = True
            NotifyOnIssuance = True
            NotifyOnRejection = True
            NotifyOnCancellation = True
            NotificationEmail = ""
            NotificationPollingMinutes = 15
            EnableDesktopToast = True
            EnableEmailNotifications = False
            SmtpHost = ""
            SmtpPort = 587
            SmtpUsername = ""
            SmtpPassword = ""
            SmtpUseSsl = True

            LicenseKey = ""
            LicenseServerUrl = "https://www.alhayat-eg.com/api/v1/license.php"

            DatabaseConnectionString = ""
        End Sub

        ''' <summary>Current environment's API base URL.</summary>
        Public ReadOnly Property ActiveApiBaseUrl As String
            Get
                Return If(Environment = ApiEnvironment.Production, ProdBaseUrl, PreprodBaseUrl)
            End Get
        End Property

        ''' <summary>Current environment's identity (token) URL.</summary>
        Public ReadOnly Property ActiveIdentityUrl As String
            Get
                Return If(Environment = ApiEnvironment.Production, ProdIdentityUrl, PreprodIdentityUrl)
            End Get
        End Property

        ''' <summary>Current environment's client id.</summary>
        Public ReadOnly Property ActiveClientId As String
            Get
                Return If(Environment = ApiEnvironment.Production, ProdClientId, PreprodClientId)
            End Get
        End Property

        ''' <summary>Current environment's client secret.</summary>
        Public ReadOnly Property ActiveClientSecret As String
            Get
                Return If(Environment = ApiEnvironment.Production, ProdClientSecret, PreprodClientSecret)
            End Get
        End Property
    End Class

End Namespace
