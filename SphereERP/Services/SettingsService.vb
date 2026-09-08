Option Strict On
Imports System.IO
Imports System.Security.Cryptography
Imports System.Text
Imports Newtonsoft.Json
Imports SphereERP.Models

Namespace Services

    ''' <summary>
    ''' Loads/saves AppSettings to a JSON file in %AppData%, encrypting
    ''' sensitive fields (client secrets, SMTP password) using Windows DPAPI
    ''' so they can only be decrypted by the same Windows user account.
    ''' </summary>
    Public Class SettingsService

        Private Shared ReadOnly SettingsFolder As String =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "ETAInvoicing")

        Private Shared ReadOnly SettingsFilePath As String =
            Path.Combine(SettingsFolder, "settings.json")

        Private Const ENC_PREFIX As String = "ENC:"

        ''' <summary>
        ''' Loads settings from disk. Returns a new default AppSettings if no file exists.
        ''' </summary>
        Public Shared Function Load() As AppSettings
            Try
                If Not File.Exists(SettingsFilePath) Then
                    Return New AppSettings()
                End If

                Dim json As String = File.ReadAllText(SettingsFilePath, Encoding.UTF8)
                Dim settings As AppSettings = JsonConvert.DeserializeObject(Of AppSettings)(json)
                If settings Is Nothing Then Return New AppSettings()

                ' Decrypt sensitive fields
                settings.PreprodClientSecret = DecryptIfNeeded(settings.PreprodClientSecret)
                settings.ProdClientSecret = DecryptIfNeeded(settings.ProdClientSecret)
                settings.SmtpPassword = DecryptIfNeeded(settings.SmtpPassword)
                settings.DbPassword = DecryptIfNeeded(settings.DbPassword)
                settings.DatabaseConnectionString = DecryptIfNeeded(settings.DatabaseConnectionString)
                settings.LicenseKey = DecryptIfNeeded(settings.LicenseKey)
                If String.IsNullOrWhiteSpace(settings.DatabaseConnectionString) Then
                    settings.DatabaseConnectionString = Data.DbConnectionFactory.DefaultConnectionString
                End If

                Return settings
            Catch ex As Exception
                Throw New ApplicationException("Failed to load settings: " & ex.Message, ex)
            End Try
        End Function

        ''' <summary>
        ''' Saves settings to disk, encrypting sensitive fields with DPAPI.
        ''' </summary>
        Public Shared Sub Save(settings As AppSettings)
            Try
                If Not Directory.Exists(SettingsFolder) Then
                    Directory.CreateDirectory(SettingsFolder)
                End If

                ' Clone via serialization so we don't mutate the live object's plaintext values
                Dim json As String = JsonConvert.SerializeObject(settings, Formatting.Indented)
                Dim toSave As AppSettings = JsonConvert.DeserializeObject(Of AppSettings)(json)

                toSave.PreprodClientSecret = EncryptIfNeeded(settings.PreprodClientSecret)
                toSave.ProdClientSecret = EncryptIfNeeded(settings.ProdClientSecret)
                toSave.SmtpPassword = EncryptIfNeeded(settings.SmtpPassword)
                toSave.DbPassword = EncryptIfNeeded(settings.DbPassword)
                toSave.DatabaseConnectionString = EncryptIfNeeded(settings.DatabaseConnectionString)
                toSave.LicenseKey = EncryptIfNeeded(settings.LicenseKey)

                Dim outJson As String = JsonConvert.SerializeObject(toSave, Formatting.Indented)
                File.WriteAllText(SettingsFilePath, outJson, Encoding.UTF8)
            Catch ex As Exception
                Throw New ApplicationException("Failed to save settings: " & ex.Message, ex)
            End Try
        End Sub

        Private Shared Function EncryptIfNeeded(plainValue As String) As String
            If String.IsNullOrEmpty(plainValue) Then Return plainValue
            If plainValue.StartsWith(ENC_PREFIX) Then Return plainValue ' already encrypted

            Dim plainBytes As Byte() = Encoding.UTF8.GetBytes(plainValue)
            Dim encryptedBytes As Byte() = ProtectedData.Protect(plainBytes, Nothing, DataProtectionScope.CurrentUser)
            Return ENC_PREFIX & Convert.ToBase64String(encryptedBytes)
        End Function

        Private Shared Function DecryptIfNeeded(storedValue As String) As String
            If String.IsNullOrEmpty(storedValue) Then Return storedValue
            If Not storedValue.StartsWith(ENC_PREFIX) Then Return storedValue ' legacy/plain value

            Try
                Dim base64 As String = storedValue.Substring(ENC_PREFIX.Length)
                Dim encryptedBytes As Byte() = Convert.FromBase64String(base64)
                Dim plainBytes As Byte() = ProtectedData.Unprotect(encryptedBytes, Nothing, DataProtectionScope.CurrentUser)
                Return Encoding.UTF8.GetString(plainBytes)
            Catch
                ' Could not decrypt (different user/machine) - treat as empty
                Return ""
            End Try
        End Function

    End Class

End Namespace
