Option Strict On
Imports SphereERP.Models
Imports SphereERP.Services

Namespace [Global]

    ''' <summary>
    ''' Holds application-wide singletons: current settings, the active API client,
    ''' and the notification service, so forms don't need to re-instantiate them.
    ''' Initialized once at startup (see Forms.LoginForm / ApplicationEvents) and
    ''' refreshed whenever Settings are saved or environment is switched.
    ''' </summary>
    Public Module AppState

        Public Property CurrentSettings As AppSettings
        Public Property ApiClient As EtaApiClient
        Public Property SubmissionService As DocumentSubmissionService
        Public Property PortalSyncService As PortalDocumentSyncService
        Public Property NotificationService As NotificationService
        Public Property IsLoggedIn As Boolean = False

        ''' <summary>Initializes or re-initializes the API client + submission service for the current settings.</summary>
        Public Sub InitializeServices()
            CurrentSettings = SettingsService.Load()
            Data.DbConnectionFactory.SetConnectionString(CurrentSettings.DatabaseConnectionString)
            Data.DbConnectionFactory.EnsureDatabaseCreated()
            ApiClient = New EtaApiClient(CurrentSettings)
            SubmissionService = New DocumentSubmissionService(CurrentSettings, ApiClient)
            PortalSyncService = New PortalDocumentSyncService(CurrentSettings, ApiClient)
        End Sub

        ''' <summary>Call after Settings are changed/saved so the API client picks up new credentials/environment.</summary>
        Public Sub ReinitializeApiClient()
            ApiClient?.Dispose()
            ApiClient = New EtaApiClient(CurrentSettings)
            SubmissionService = New DocumentSubmissionService(CurrentSettings, ApiClient)
            PortalSyncService = New PortalDocumentSyncService(CurrentSettings, ApiClient)
            IsLoggedIn = False
        End Sub

    End Module

End Namespace
