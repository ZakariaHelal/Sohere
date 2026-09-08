Option Strict On
Imports System.Security.Cryptography
Imports System.Security.Cryptography.Pkcs
Imports System.Security.Cryptography.X509Certificates
Imports System.Text

Namespace Services

    ''' <summary>
    ''' Handles digital signing of ETA documents using a certificate stored in the
    ''' Windows certificate store. USB tokens / HSMs (e.g. eToken, SafeNet, ePass)
    ''' expose their certificates through the CryptoAPI/CNG smart card minidriver,
    ''' so once the token's driver is installed the certificate appears in the
    ''' "Personal" (My) store and can be used here exactly like a software cert.
    ''' Signing a CAdES-BES message will trigger the token's PIN prompt natively.
    ''' </summary>
    Public Class CertificateSigningService

        ''' <summary>
        ''' Lists all certificates in the given store that have a private key
        ''' and are suitable for signing (Digital Signature key usage), including
        ''' those backed by hardware tokens.
        ''' </summary>
        Public Function ListSigningCertificates(Optional storeName As StoreName = StoreName.My) As List(Of X509Certificate2)
            Dim results As New List(Of X509Certificate2)()

            Using store As New X509Store(storeName, StoreLocation.CurrentUser)
                store.Open(OpenFlags.ReadOnly)
                For Each cert As X509Certificate2 In store.Certificates
                    If cert.HasPrivateKey AndAlso cert.NotAfter > DateTime.Now Then
                        results.Add(cert)
                    End If
                Next
            End Using

            ' Also check LocalMachine store in case the token driver registers there
            Try
                Using store As New X509Store(storeName, StoreLocation.LocalMachine)
                    store.Open(OpenFlags.ReadOnly)
                    For Each cert As X509Certificate2 In store.Certificates
                        If cert.HasPrivateKey AndAlso cert.NotAfter > DateTime.Now AndAlso
                           Not results.Any(Function(c) c.Thumbprint = cert.Thumbprint) Then
                            results.Add(cert)
                        End If
                    Next
                End Using
            Catch
                ' LocalMachine store may require elevated permissions; ignore if inaccessible.
            End Try

            Return results
        End Function

        ''' <summary>Finds a certificate by thumbprint across CurrentUser/LocalMachine "My" stores.</summary>
        Public Function FindByThumbprint(thumbprint As String, Optional storeName As StoreName = StoreName.My) As X509Certificate2
            If String.IsNullOrWhiteSpace(thumbprint) Then Return Nothing

            Dim normalized As String = thumbprint.Replace(" ", "").ToUpperInvariant()

            For Each location As StoreLocation In {StoreLocation.CurrentUser, StoreLocation.LocalMachine}
                Try
                    Using store As New X509Store(storeName, location)
                        store.Open(OpenFlags.ReadOnly)
                        Dim found = store.Certificates.Find(X509FindType.FindByThumbprint, normalized, False)
                        If found.Count > 0 Then Return found(0)
                    End Using
                Catch
                    ' continue to next location
                End Try
            Next

            Return Nothing
        End Function

        ''' <summary>
        ''' Produces a detached CAdES-BES (CMS/PKCS#7 with signing-certificate attribute) signature
        ''' over the given content bytes, as required by ETA for invoice/credit/debit note submission.
        ''' This will prompt for the USB token PIN via the CSP/minidriver if the certificate's
        ''' private key resides on hardware.
        ''' </summary>
        ''' <param name="contentBytes">Canonicalized document bytes to sign (typically UTF-8 JSON or the ETA-specified serialization).</param>
        ''' <param name="signingCert">Certificate with private key (from token or store).</param>
        ''' <returns>Base64-encoded detached CMS signature suitable for the document's "signatures" array.</returns>
        Public Function SignCadesBes(contentBytes As Byte(), signingCert As X509Certificate2) As String
            If signingCert Is Nothing Then
                Throw New InvalidOperationException("No signing certificate was provided. Configure your USB token certificate in Settings.")
            End If
            If Not signingCert.HasPrivateKey Then
                Throw New InvalidOperationException("The selected certificate does not have an accessible private key. Ensure the USB token is inserted and its driver is installed.")
            End If

            Try
                ' ContentInfo wraps the raw bytes; detached = true means the signature does not embed the payload.
                Dim contentInfo As New ContentInfo(contentBytes)
                Dim signedCms As New SignedCms(contentInfo, detached:=True)

                Dim signer As New CmsSigner(SubjectIdentifierType.IssuerAndSerialNumber, signingCert) With {
                    .DigestAlgorithm = New Oid("2.16.840.1.101.3.4.2.1") ' SHA-256
                }

                ' CAdES-BES requires the ESS signing-certificate-v2 attribute binding the cert to the signature.
                Dim signingCertAttr = BuildSigningCertificateV2Attribute(signingCert)
                signer.SignedAttributes.Add(signingCertAttr)

                ' Include the full chain so the receiving party can validate without separate lookup.
                signer.IncludeOption = X509IncludeOption.WholeChain

                ' This call invokes the CSP/KSP for the certificate's private key.
                ' For a USB token, Windows will show the PIN prompt dialog here.
                signedCms.ComputeSignature(signer, silent:=False)

                Dim signatureBytes As Byte() = signedCms.Encode()
                Return Convert.ToBase64String(signatureBytes)

            Catch ex As CryptographicException
                Throw New ApplicationException(
                    "Signing failed. Please verify the USB token is connected, the correct PIN was entered, " &
                    "and the certificate driver is properly installed. Details: " & ex.Message, ex)
            End Try
        End Function

        ''' <summary>
        ''' Builds the ESS SigningCertificateV2 signed attribute (RFC 5035), required for CAdES-BES,
        ''' binding the certificate's hash to the signature so it cannot be substituted.
        ''' </summary>
        Private Function BuildSigningCertificateV2Attribute(cert As X509Certificate2) As CryptographicAttributeObject
            Using sha256 As SHA256 = SHA256.Create()
                Dim certHash As Byte() = sha256.ComputeHash(cert.RawData)

                ' Manually build the minimal ASN.1 structure for SigningCertificateV2:
                ' SigningCertificateV2 ::= SEQUENCE { certs SEQUENCE OF ESSCertIDv2 }
                ' ESSCertIDv2 ::= SEQUENCE { hashAlgorithm AlgorithmIdentifier DEFAULT {sha256}, certHash OCTET STRING }
                Dim hashOctetString = AsnEncodeOctetString(certHash)
                Dim essCertIdV2 = AsnEncodeSequence(hashOctetString)
                Dim certsSeq = AsnEncodeSequence(AsnEncodeSequence(essCertIdV2))
                Dim signingCertificateV2 = AsnEncodeSequence(certsSeq)

                Dim oid As New Oid("1.2.840.113549.1.9.16.2.47") ' id-aa-signingCertificateV2
                Dim attr As New AsnEncodedData(oid, signingCertificateV2)
                Return New CryptographicAttributeObject(oid, New AsnEncodedDataCollection(attr))
            End Using
        End Function

        Private Function AsnEncodeOctetString(content As Byte()) As Byte()
            Return AsnEncodeTlv(&H4, content)
        End Function

        Private Function AsnEncodeSequence(content As Byte()) As Byte()
            Return AsnEncodeTlv(&H30, content)
        End Function

        Private Function AsnEncodeTlv(tag As Byte, content As Byte()) As Byte()
            Dim lengthBytes As Byte()
            If content.Length < 128 Then
                lengthBytes = {CByte(content.Length)}
            Else
                Dim lenBytesRaw = BitConverter.GetBytes(content.Length)
                Array.Reverse(lenBytesRaw)
                Dim trimmed = lenBytesRaw.SkipWhile(Function(b) b = 0).ToArray()
                If trimmed.Length = 0 Then trimmed = {0}
                lengthBytes = New Byte() {CByte(&H80 Or trimmed.Length)}.Concat(trimmed).ToArray()
            End If

            Return New Byte() {tag}.Concat(lengthBytes).Concat(content).ToArray()
        End Function

        ''' <summary>
        ''' Quick check whether the certificate's private key is accessible right now
        ''' (e.g. token is inserted), without prompting for a PIN.
        ''' </summary>
        Public Function IsCertificateAccessible(cert As X509Certificate2) As Boolean
            If cert Is Nothing Then Return False
            Try
                Return cert.HasPrivateKey
            Catch
                Return False
            End Try
        End Function

    End Class

End Namespace
