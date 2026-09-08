Option Strict On
Imports System.IO
Imports PdfSharpCore.Drawing
Imports PdfSharpCore.Pdf
Imports SphereERP.Models
Imports QRCoder

Namespace Services

    ''' <summary>
    ''' Generates a printable PDF representation of an e-Invoice/Credit/Debit Note document,
    ''' used as a fallback/local printout and for documents already downloaded from ETA.
    ''' If the portal-issued PDF was downloaded via the API, prefer that as the official copy;
    ''' this generator is for local preview/printing before or independent of that download.
    ''' </summary>
    Public Class PdfGenerationService

        ''' <summary>Generates a PDF for the given document and saves it to outputPath.</summary>
        Public Sub GenerateDocumentPdf(doc As EInvoiceDocument, outputPath As String, Optional uuid As String = "", Optional longId As String = "")
            Using pdfDoc As New PdfDocument()
                Dim page = pdfDoc.AddPage()
                page.Size = PdfSharpCore.PageSize.A4
                Dim gfx = XGraphics.FromPdfPage(page)

                Dim fontTitle = New XFont("Arial", 16, XFontStyle.Bold)
                Dim fontHeader = New XFont("Arial", 10, XFontStyle.Bold)
                Dim fontNormal = New XFont("Arial", 9, XFontStyle.Regular)
                Dim fontSmall = New XFont("Arial", 7, XFontStyle.Regular)

                Dim margin As Double = 30
                Dim y As Double = margin
                Dim pageWidth = page.Width.Point - 2 * margin

                Dim typeLabel = GetDocumentTypeLabel(doc.DocumentType)
                gfx.DrawString($"{typeLabel} - {doc.InternalID}", fontTitle, XBrushes.Black, New XRect(margin, y, pageWidth, 24), XStringFormats.TopLeft)
                y += 28

                If Not String.IsNullOrWhiteSpace(uuid) Then
                    gfx.DrawString($"UUID: {uuid}", fontSmall, XBrushes.DarkSlateGray, New XRect(margin, y, pageWidth, 12), XStringFormats.TopLeft)
                    y += 12
                End If
                gfx.DrawString($"Issue Date: {doc.DateTimeIssued:yyyy-MM-dd HH:mm}", fontNormal, XBrushes.Black, New XRect(margin, y, pageWidth, 14), XStringFormats.TopLeft)
                y += 20

                ' Issuer / Receiver boxes
                Dim colWidth = pageWidth / 2 - 5
                DrawPartyBox(gfx, "Issuer (Seller)", doc.Issuer, margin, y, colWidth, fontHeader, fontNormal)
                DrawPartyBox(gfx, "Receiver (Buyer)", doc.Receiver, margin + colWidth + 10, y, colWidth, fontHeader, fontNormal)
                y += 95

                ' QR Code with key document info (UUID/InternalId), per ETA invoice printout convention.
                If Not String.IsNullOrWhiteSpace(uuid) Then
                    Dim qrPayload = $"InternalID:{doc.InternalID}|UUID:{uuid}|Total:{doc.TotalAmount:N2}|Date:{doc.DateTimeIssued:yyyy-MM-dd}"
                    Dim qrBytes = GenerateQrCodeBytes(qrPayload)
                    Using qrImage = XImage.FromStream(Function() New MemoryStream(qrBytes))
                        gfx.DrawImage(qrImage, page.Width.Point - margin - 80, margin, 80, 80)
                    End Using
                End If

                y += 10
                gfx.DrawLine(XPens.Gray, margin, y, page.Width.Point - margin, y)
                y += 8

                ' Line items table header
                Dim colDesc As Double = margin
                Dim colQty As Double = margin + pageWidth * 0.40
                Dim colPrice As Double = margin + pageWidth * 0.52
                Dim colDisc As Double = margin + pageWidth * 0.64
                Dim colTax As Double = margin + pageWidth * 0.76
                Dim colTotal As Double = margin + pageWidth * 0.88

                gfx.DrawString("Description", fontHeader, XBrushes.Black, New XPoint(colDesc, y))
                gfx.DrawString("Qty", fontHeader, XBrushes.Black, New XPoint(colQty, y))
                gfx.DrawString("Price", fontHeader, XBrushes.Black, New XPoint(colPrice, y))
                gfx.DrawString("Disc.", fontHeader, XBrushes.Black, New XPoint(colDisc, y))
                gfx.DrawString("Tax", fontHeader, XBrushes.Black, New XPoint(colTax, y))
                gfx.DrawString("Total", fontHeader, XBrushes.Black, New XPoint(colTotal, y))
                y += 14
                gfx.DrawLine(XPens.LightGray, margin, y, page.Width.Point - margin, y)
                y += 6

                For Each line In doc.InvoiceLines
                    If y > page.Height.Point - 150 Then
                        page = pdfDoc.AddPage()
                        gfx = XGraphics.FromPdfPage(page)
                        y = margin
                    End If

                    Dim descText = TruncateText(line.Description, 45)
                    gfx.DrawString(descText, fontNormal, XBrushes.Black, New XPoint(colDesc, y))
                    gfx.DrawString(line.Quantity.ToString("N2"), fontNormal, XBrushes.Black, New XPoint(colQty, y))
                    gfx.DrawString(line.UnitValue.AmountEGP.ToString("N2"), fontNormal, XBrushes.Black, New XPoint(colPrice, y))
                    gfx.DrawString(line.ItemsDiscount.ToString("N2"), fontNormal, XBrushes.Black, New XPoint(colDisc, y))
                    Dim taxStr = String.Join(",", line.TaxableItems.Select(Function(t) $"{t.Rate}%"))
                    gfx.DrawString(taxStr, fontNormal, XBrushes.Black, New XPoint(colTax, y))
                    gfx.DrawString(line.Total.ToString("N2"), fontNormal, XBrushes.Black, New XPoint(colTotal, y))
                    y += 14
                Next

                y += 10
                gfx.DrawLine(XPens.Gray, margin, y, page.Width.Point - margin, y)
                y += 10

                DrawTotalsBlock(gfx, doc, margin, y, pageWidth, fontHeader, fontNormal)

                pdfDoc.Save(outputPath)
            End Using
        End Sub

        Private Sub DrawPartyBox(gfx As XGraphics, title As String, party As PartyModel, x As Double, y As Double, width As Double, fontHeader As XFont, fontNormal As XFont)
            gfx.DrawRectangle(XPens.LightGray, x, y, width, 90)
            gfx.DrawString(title, fontHeader, XBrushes.Black, New XRect(x + 5, y + 5, width - 10, 14), XStringFormats.TopLeft)
            gfx.DrawString(party.Name, fontNormal, XBrushes.Black, New XRect(x + 5, y + 22, width - 10, 14), XStringFormats.TopLeft)
            gfx.DrawString($"RIN/ID: {party.Id}", fontNormal, XBrushes.Black, New XRect(x + 5, y + 36, width - 10, 14), XStringFormats.TopLeft)
            Dim addr = $"{party.Address.Street}, {party.Address.RegionCity}, {party.Address.Governate}"
            gfx.DrawString(TruncateText(addr, 60), fontNormal, XBrushes.Black, New XRect(x + 5, y + 50, width - 10, 28), XStringFormats.TopLeft)
        End Sub

        Private Sub DrawTotalsBlock(gfx As XGraphics, doc As EInvoiceDocument, margin As Double, y As Double, pageWidth As Double, fontHeader As XFont, fontNormal As XFont)
            Dim labelX = margin + pageWidth * 0.65
            Dim valueX = margin + pageWidth * 0.85

            gfx.DrawString("Total Sales:", fontNormal, XBrushes.Black, New XPoint(labelX, y))
            gfx.DrawString(doc.TotalSalesAmount.ToString("N2") & " EGP", fontNormal, XBrushes.Black, New XPoint(valueX, y))
            y += 14

            gfx.DrawString("Total Discount:", fontNormal, XBrushes.Black, New XPoint(labelX, y))
            gfx.DrawString(doc.TotalDiscountAmount.ToString("N2") & " EGP", fontNormal, XBrushes.Black, New XPoint(valueX, y))
            y += 14

            gfx.DrawString("Net Amount:", fontNormal, XBrushes.Black, New XPoint(labelX, y))
            gfx.DrawString(doc.NetAmount.ToString("N2") & " EGP", fontNormal, XBrushes.Black, New XPoint(valueX, y))
            y += 14

            For Each tax In doc.TaxTotals
                gfx.DrawString($"{tax.TaxType}:", fontNormal, XBrushes.Black, New XPoint(labelX, y))
                gfx.DrawString(tax.Amount.ToString("N2") & " EGP", fontNormal, XBrushes.Black, New XPoint(valueX, y))
                y += 14
            Next

            gfx.DrawString("Total Amount:", fontHeader, XBrushes.Black, New XPoint(labelX, y))
            gfx.DrawString(doc.TotalAmount.ToString("N2") & " EGP", fontHeader, XBrushes.Black, New XPoint(valueX, y))
        End Sub

        Private Function GetDocumentTypeLabel(documentType As String) As String
            Select Case documentType.ToUpperInvariant()
                Case "I" : Return "TAX INVOICE"
                Case "C" : Return "CREDIT NOTE"
                Case "D" : Return "DEBIT NOTE"
                Case "EI" : Return "EXPORT INVOICE"
                Case "EC" : Return "EXPORT CREDIT NOTE"
                Case "ED" : Return "EXPORT DEBIT NOTE"
                Case Else : Return "DOCUMENT"
            End Select
        End Function

        Private Function TruncateText(text As String, maxLen As Integer) As String
            If String.IsNullOrEmpty(text) Then Return ""
            If text.Length <= maxLen Then Return text
            Return text.Substring(0, maxLen - 1) & "…"
        End Function

        Private Function GenerateQrCodeBytes(payload As String) As Byte()
            Using qrGenerator As New QRCodeGenerator()
                Dim qrData = qrGenerator.CreateQrCode(payload, QRCodeGenerator.ECCLevel.Q)
                Using qrCode As New PngByteQRCode(qrData)
                    Return qrCode.GetGraphic(10)
                End Using
            End Using
        End Function

        ''' <summary>Saves a portal-downloaded PDF byte array (from the ETA API) directly to disk.</summary>
        Public Sub SavePortalPdf(pdfBytes As Byte(), outputPath As String)
            File.WriteAllBytes(outputPath, pdfBytes)
        End Sub

    End Class

End Namespace
