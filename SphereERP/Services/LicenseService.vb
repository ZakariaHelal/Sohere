Option Strict On
Imports System.IO
Imports System.Security.Cryptography
Imports System.Text
Imports Newtonsoft.Json

Namespace Services

    Public Class LicenseValidationResult
        Public Property IsValid As Boolean
        Public Property ExpiryDate As Date?
        Public Property Message As String
        Public Property CustomerName As String
        Public Property Plan As String
    End Class

    Public Class CachedLicense
        Public Property LicenseKey As String
        Public Property HwId As String
        Public Property ExpiryDate As Date
        Public Property CustomerName As String
        Public Property Plan As String
        Public Property LastCheckUtc As Date
        Public Property GraceDays As Integer = 7
    End Class

    Public Class LicenseService
        Private Const LIC_CACHE_FILE As String = "license.cache"

        Private ReadOnly _licenseKey As String
        Private ReadOnly _serverUrl As String
        Private ReadOnly _hwId As String
        Private _cached As CachedLicense

        Public Sub New(licenseKey As String, Optional serverUrl As String = Nothing)
            _licenseKey = licenseKey
            Dim url = If(String.IsNullOrWhiteSpace(serverUrl), "https://www.alhayat-eg.com/api/v1/license.php", serverUrl)
            ' Migrate old placeholder URL if still present in saved settings
            If url = "https://your-license-server.com/api/v1/license" Then
                url = "https://www.alhayat-eg.com/api/v1/license.php"
            End If
            _serverUrl = url
            _hwId = GetHardwareId()
        End Sub

        Public ReadOnly Property LicenseKey As String
            Get
                Return _licenseKey
            End Get
        End Property

        Public ReadOnly Property HardwareId As String
            Get
                Return _hwId
            End Get
        End Property

        ''' <summary>
        ''' Quick synchronous check from cache only (no network). Use at startup
        ''' to avoid blocking on the UI thread while waiting for the server.
        ''' Returns Nothing if no cache exists (user needs to activate).
        ''' </summary>
        Public Function QuickValidateFromCache() As LicenseValidationResult
            Return ValidateFromCache()
        End Function

        Public Function GetCachedLicenseData() As CachedLicense
            Return LoadCache()
        End Function

        Public Async Function ValidateAsync() As Task(Of LicenseValidationResult)
            Try
                Dim result = Await CheckWithServerAsync()
                If result IsNot Nothing Then
                    If result.IsValid AndAlso result.ExpiryDate.HasValue Then
                        SaveCache(New CachedLicense() With {
                            .LicenseKey = _licenseKey,
                            .HwId = _hwId,
                            .ExpiryDate = result.ExpiryDate.Value,
                            .CustomerName = result.CustomerName,
                            .Plan = result.Plan,
                            .LastCheckUtc = DateTime.UtcNow
                        })
                    End If
                    Return result
                End If
            Catch ex As Exception
            End Try

            Return ValidateFromCache()
        End Function

        Private Async Function CheckWithServerAsync() As Task(Of LicenseValidationResult)
            Using client As New Net.Http.HttpClient()
                client.Timeout = TimeSpan.FromSeconds(10)

                Dim payload As New Dictionary(Of String, String) From {
                    {"license_key", _licenseKey},
                    {"hwid", _hwId},
                    {"app_version", GetAppVersion()}
                }
                Dim content As New Net.Http.FormUrlEncodedContent(payload)
                Dim response = Await client.PostAsync(_serverUrl, content)

                If Not response.IsSuccessStatusCode Then
                    Return New LicenseValidationResult() With {
                        .IsValid = False,
                        .Message = "License server returned error " & response.StatusCode.ToString()
                    }
                End If

                Dim json = Await response.Content.ReadAsStringAsync()
                Return JsonConvert.DeserializeObject(Of LicenseValidationResult)(json)
            End Using
        End Function

        Private Function ValidateFromCache() As LicenseValidationResult
            _cached = LoadCache()

            If _cached Is Nothing Then
                Return New LicenseValidationResult() With {
                    .IsValid = False,
                    .Message = "No valid license found. Please activate the application."
                }
            End If

            If _cached.LicenseKey <> _licenseKey OrElse _cached.HwId <> _hwId Then
                Return New LicenseValidationResult() With {
                    .IsValid = False,
                    .Message = "License does not match this machine."
                }
            End If

            Dim graceEnd = _cached.ExpiryDate.AddDays(_cached.GraceDays)
            If DateTime.UtcNow > graceEnd Then
                Return New LicenseValidationResult() With {
                    .IsValid = False,
                    .Message = "Your subscription has expired. Please renew your license.",
                    .ExpiryDate = _cached.ExpiryDate,
                    .CustomerName = _cached.CustomerName
                }
            End If

            Dim daysLeft = (_cached.ExpiryDate - DateTime.UtcNow).TotalDays
            Dim msg As String
            If daysLeft <= 0 Then
                msg = $"Subscription expired on {_cached.ExpiryDate:yyyy-MM-dd}. Grace period ends {graceEnd:yyyy-MM-dd}."
            Else
                msg = $"License valid until {_cached.ExpiryDate:yyyy-MM-dd} ({Math.Ceiling(daysLeft)} days remaining)."
            End If

            Return New LicenseValidationResult() With {
                .IsValid = daysLeft > 0 OrElse DateTime.UtcNow <= graceEnd,
                .ExpiryDate = _cached.ExpiryDate,
                .CustomerName = _cached.CustomerName,
                .Message = msg
            }
        End Function

        Public Shared Function GetHardwareId() As String
            Try
                Dim parts As New List(Of String)
                parts.Add(Environment.MachineName)
                parts.Add(Environment.UserName)
                parts.Add(Environment.ProcessorCount.ToString())
                parts.Add(Environment.OSVersion.VersionString)

                Dim machineIdFile = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                    "ETAInvoicing", "machine.id")

                If File.Exists(machineIdFile) Then
                    parts.Add(File.ReadAllText(machineIdFile).Trim())
                Else
                    Dim guid As String = System.Guid.NewGuid().ToString("N")
                    Try
                        Dim dir As String = Path.GetDirectoryName(machineIdFile)
                        If dir IsNot Nothing Then Directory.CreateDirectory(dir)
                        File.WriteAllText(machineIdFile, guid)
                    Catch
                    End Try
                    parts.Add(guid)
                End If

                Dim raw = String.Join("|", parts.Where(Function(p) p.Length > 0))
                Using sha = SHA256.Create()
                    Dim hash = sha.ComputeHash(Encoding.UTF8.GetBytes(raw))
                    Return Convert.ToHexString(hash).ToLowerInvariant()
                End Using
            Catch
                Return Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(Environment.MachineName & "|" & Environment.UserName))).ToLowerInvariant()
            End Try
        End Function

        Private Shared Function GetAppVersion() As String
            Try
                Dim v = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version
                Return If(v Is Nothing, "1.0.0.0", v.ToString())
            Catch
                Return "1.0.0.0"
            End Try
        End Function

        Private Shared ReadOnly CacheFolder As String =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "ETAInvoicing")

        Private Shared ReadOnly CacheFilePath As String =
            Path.Combine(CacheFolder, LIC_CACHE_FILE)

        Private Sub SaveCache(lic As CachedLicense)
            Try
                If Not Directory.Exists(CacheFolder) Then
                    Directory.CreateDirectory(CacheFolder)
                End If
                Dim json = JsonConvert.SerializeObject(lic, Formatting.None)
                Dim encrypted = ProtectedData.Protect(Encoding.UTF8.GetBytes(json), Nothing, DataProtectionScope.CurrentUser)
                File.WriteAllBytes(CacheFilePath, encrypted)
            Catch
            End Try
        End Sub

        Private Function LoadCache() As CachedLicense
            Try
                If Not File.Exists(CacheFilePath) Then Return Nothing
                Dim encrypted = File.ReadAllBytes(CacheFilePath)
                Dim json = Encoding.UTF8.GetString(ProtectedData.Unprotect(encrypted, Nothing, DataProtectionScope.CurrentUser))
                Return JsonConvert.DeserializeObject(Of CachedLicense)(json)
            Catch
                Return Nothing
            End Try
        End Function

        Public Shared Sub ClearCache()
            Try
                If File.Exists(CacheFilePath) Then File.Delete(CacheFilePath)
            Catch
            End Try
        End Sub

    End Class

End Namespace
