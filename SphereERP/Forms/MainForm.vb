Option Strict On
Imports System.Windows.Forms
Imports DevExpress.XtraEditors
Imports SphereERP.Models
Imports SphereERP.Services
Imports SphereERP.[Global]

Namespace Forms

    Public Class MainForm

        Private _activeChildForm As Form
        Private WithEvents _licenseTimer As Timer

        Public Sub New()
            InitializeComponent()
            _licenseTimer = New Timer(components) With {.Interval = 1800000}
        End Sub

        Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            _licenseTimer.Start()
            FireAndForgetLicenseCheck()

            UpdateEnvironmentBadge()

            notifyIcon.Icon = System.Drawing.SystemIcons.Information
            notifyIcon.Visible = True
            notifyIcon.Text = "ETA Invoicing Desktop"

            AppState.NotificationService = New NotificationService(AppState.CurrentSettings, AppState.SubmissionService, notifyIcon)
            AddHandler AppState.NotificationService.NewNotification, AddressOf OnNewNotification
            AppState.NotificationService.Start()

            NavigateTo(New DashboardForm())
        End Sub

        Private Async Sub _licenseTimer_Tick(sender As Object, e As EventArgs) Handles _licenseTimer.Tick
            Await CheckLicenseAsync()
        End Sub

        Private Async Sub FireAndForgetLicenseCheck()
            Await CheckLicenseAsync()
        End Sub

        Private Async Function CheckLicenseAsync() As Task
            Dim key = AppState.CurrentSettings?.LicenseKey
            If String.IsNullOrWhiteSpace(key) Then Return

            Try
                Dim service As New LicenseService(key, AppState.CurrentSettings.LicenseServerUrl)
                Dim result = Await service.ValidateAsync()

                If result IsNot Nothing AndAlso Not result.IsValid Then
                    Me.BeginInvoke(Sub()
                        Dim msg = result.Message & vbCrLf & vbCrLf & "The application will be locked until the license is renewed."
                        XtraMessageBox.Show(msg, "License Expired", MessageBoxButtons.OK, MessageBoxIcon.Warning)

                        AppState.IsLoggedIn = False
                        AppState.NotificationService?.Stop()
                        Me.Close()
                    End Sub)
                End If
            Catch
            End Try
        End Function

        Private Sub UpdateEnvironmentBadge()
            Dim isProd = AppState.CurrentSettings.Environment = ApiEnvironment.Production
            lblEnvironmentBadge.Text = If(isProd, "PRODUCTION (LIVE)", "PREPRODUCTION")
            lblEnvironmentBadge.ForeColor = If(isProd, System.Drawing.Color.OrangeRed, System.Drawing.Color.LightGreen)
            lblStatusEnvironment.Text = $"Environment: {AppState.CurrentSettings.Environment} | RIN: {AppState.CurrentSettings.TaxpayerRIN}"
        End Sub

        Private Sub OnNewNotification(eventType As String, message As String)
            If Me.InvokeRequired Then
                Me.BeginInvoke(New Action(Sub() lblStatusText.Text = message))
            Else
                lblStatusText.Text = message
            End If
        End Sub

        Public Sub NavigateTo(childForm As Form)
            If _activeChildForm IsNot Nothing Then
                pnlContent.Controls.Remove(_activeChildForm)
                _activeChildForm.Dispose()
            End If

            childForm.TopLevel = False
            childForm.FormBorderStyle = FormBorderStyle.None
            childForm.Dock = DockStyle.Fill
            pnlContent.Controls.Add(childForm)
            childForm.Show()
            _activeChildForm = childForm

            lblStatusText.Text = "Ready"
        End Sub

        Private Sub btnNavDashboard_Click(sender As Object, e As EventArgs) Handles btnNavDashboard.Click
            NavigateTo(New DashboardForm())
        End Sub

        Private Sub btnNavNewDocument_Click(sender As Object, e As EventArgs) Handles btnNavNewDocument.Click
            NavigateTo(New InvoiceEntryForm())
        End Sub

        Private Sub btnNavBatchImport_Click(sender As Object, e As EventArgs) Handles btnNavBatchImport.Click
            NavigateTo(New BatchImportForm())
        End Sub

        Private Sub btnNavSearch_Click(sender As Object, e As EventArgs) Handles btnNavSearch.Click
            NavigateTo(New DocumentSearchForm())
        End Sub

        Private Sub btnNavEgsCodes_Click(sender As Object, e As EventArgs) Handles btnNavEgsCodes.Click
            NavigateTo(New EgsCodeLibraryForm())
        End Sub

        Private Sub btnNavNotifications_Click(sender As Object, e As EventArgs) Handles btnNavNotifications.Click
            NavigateTo(New NotificationsForm())
        End Sub

        Private Sub btnNavSettings_Click(sender As Object, e As EventArgs) Handles btnNavSettings.Click
            Using settingsForm As New SettingsForm()
                If settingsForm.ShowDialog(Me) = DialogResult.OK Then
                    UpdateEnvironmentBadge()
                    AppState.NotificationService?.RestartWithCurrentSettings()
                End If
            End Using
        End Sub

        Private Sub btnHelp_Click(sender As Object, e As EventArgs) Handles btnHelp.Click
            Dim manualPath = System.IO.Path.Combine(Application.StartupPath, "USER_MANUAL.md")
            If Not System.IO.File.Exists(manualPath) Then
                MessageBox.Show("User manual file not found.", "Help", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            Dim markdown = System.IO.File.ReadAllText(manualPath)
            Dim html = MarkdownToHtml(markdown)

            Using dlg As New DevExpress.XtraEditors.XtraForm() With {
                .Text = "User Manual",
                .Width = 950,
                .Height = 750,
                .StartPosition = FormStartPosition.CenterParent
            }
                Dim tb As New DevExpress.XtraEditors.PanelControl()
                tb.Dock = DockStyle.Top
                tb.Height = 44
                tb.Padding = New Padding(8, 6, 8, 6)

                Dim btnPdf As New DevExpress.XtraEditors.SimpleButton()
                btnPdf.Text = "Print / Save as PDF"
                btnPdf.Dock = DockStyle.Right
                btnPdf.Width = 140

                Dim lblTitle As New DevExpress.XtraEditors.LabelControl()
                lblTitle.Text = "User Manual"
                lblTitle.Appearance.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold)
                lblTitle.Appearance.ForeColor = System.Drawing.Color.FromArgb(44, 62, 80)
                lblTitle.Dock = DockStyle.Left
                lblTitle.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical

                tb.Controls.Add(btnPdf)
                tb.Controls.Add(lblTitle)

                Dim wb As New System.Windows.Forms.WebBrowser()
                wb.Dock = DockStyle.Fill
                wb.WebBrowserShortcutsEnabled = True
                wb.AllowWebBrowserDrop = False
                wb.IsWebBrowserContextMenuEnabled = False
                wb.DocumentText = html

                AddHandler btnPdf.Click, Sub()
                    wb.ShowPrintDialog()
                End Sub

                dlg.Controls.Add(wb)
                dlg.Controls.Add(tb)
                dlg.ShowDialog(Me)
            End Using
        End Sub

        Private Shared Function MarkdownToHtml(md As String) As String
            Dim lines = md.Split({vbCrLf, vbLf}, StringSplitOptions.None)
            Dim sb As New System.Text.StringBuilder()

            sb.AppendLine("<!DOCTYPE html><html><head><meta charset='utf-8'><meta http-equiv='X-UA-Compatible' content='IE=edge'>")
            sb.AppendLine("<style>")
            sb.AppendLine("body{font-family:'Segoe UI',Tahoma,sans-serif;font-size:14px;color:#1a1a1a;max-width:960px;margin:30px auto;padding:0 40px;line-height:1.6}")
            sb.AppendLine("h1{color:#2c3e50;border-bottom:2px solid #3498db;padding-bottom:8px;margin-top:32px;font-size:26px}")
            sb.AppendLine("h2{color:#2c3e50;border-bottom:1px solid #bdc3c7;padding-bottom:4px;margin-top:28px;font-size:22px}")
            sb.AppendLine("h3{color:#34495e;margin-top:22px;font-size:18px}")
            sb.AppendLine("h4{color:#34495e;margin-top:18px;font-size:16px}")
            sb.AppendLine("p{margin:10px 0}")
            sb.AppendLine("table{border-collapse:collapse;width:100%;margin:14px 0}")
            sb.AppendLine("th,td{border:1px solid #ccc;padding:8px 12px;text-align:left;vertical-align:top}")
            sb.AppendLine("th{background:#f2f6fa;font-weight:600}")
            sb.AppendLine("tr:nth-child(even){background:#f9f9f9}")
            sb.AppendLine("code{background:#f0f0f0;padding:2px 6px;border-radius:3px;font-size:13px;font-family:Cascadia Code,Consolas,monospace}")
            sb.AppendLine("pre{background:#f5f5f5;padding:12px 16px;border-radius:5px;border:1px solid #ddd;overflow-x:auto}")
            sb.AppendLine("pre code{background:none;padding:0;border-radius:0}")
            sb.AppendLine("hr{border:none;border-top:1px solid #ddd;margin:28px 0}")
            sb.AppendLine("ul,ol{margin:8px 0;padding-left:26px}")
            sb.AppendLine("li{margin:4px 0}")
            sb.AppendLine("strong{font-weight:600}")
            sb.AppendLine("a{color:#2980b9;text-decoration:none}")
            sb.AppendLine("a:hover{text-decoration:underline}")
            sb.AppendLine("</style>")
            sb.AppendLine("<script>")
            sb.AppendLine("document.onclick=function(e){")
            sb.AppendLine("e=e||window.event;var t=e.target||e.srcElement;")
            sb.AppendLine("while(t&&t.tagName!='A')t=t.parentNode;")
            sb.AppendLine("if(!t||!t.hash||t.hash.charAt(0)!='#')return;")
            sb.AppendLine("var id=t.hash.substring(1),el=document.getElementById(id);")
            sb.AppendLine("if(el){el.scrollIntoView(true);if(e.preventDefault)e.preventDefault();else e.returnValue=false;return;}")
            sb.AppendLine("var lt=t.textContent||t.innerText;lt=lt.replace(/^\\s+|\\s+$/g,'');")
            sb.AppendLine("var h=['h1','h2','h3','h4','h5','h6'];")
            sb.AppendLine("for(var hi=0;hi<h.length;hi++){")
            sb.AppendLine("var nodes=document.getElementsByTagName(h[hi]);")
            sb.AppendLine("for(var i=0;i<nodes.length;i++){")
            sb.AppendLine("var ht=nodes[i].textContent||nodes[i].innerText;")
            sb.AppendLine("if(ht.indexOf(lt)>=0||lt.indexOf(ht)>=0){")
            sb.AppendLine("nodes[i].scrollIntoView(true);if(e.preventDefault)e.preventDefault();else e.returnValue=false;return;")
            sb.AppendLine("}}}}</script>")
            sb.AppendLine("</head><body>")

            Dim inTable = False
            Dim inList = False
            Dim inCodeBlock = False

            For i = 0 To lines.Length - 1
                Dim raw = lines(i)
                Dim trimmed = raw.Trim()

                ' fenced code block
                If trimmed.StartsWith("```") Then
                    If inCodeBlock Then
                        sb.AppendLine("</code></pre>")
                        inCodeBlock = False
                    Else
                        sb.AppendLine("<pre><code>")
                        inCodeBlock = True
                    End If
                    Continue For
                End If

                If inCodeBlock Then
                    sb.AppendLine(EscapeHtml(raw))
                    Continue For
                End If

                ' blank line
                If String.IsNullOrWhiteSpace(raw) Then
                    If inTable Then
                        sb.AppendLine("</table>")
                        inTable = False
                    End If
                    If inList Then
                        sb.AppendLine("</ul>")
                        inList = False
                    End If
                    Continue For
                End If

                ' horizontal rule
                If trimmed = "---" OrElse trimmed = "***" Then
                    sb.AppendLine("<hr>")
                    Continue For
                End If

                ' heading
                If trimmed.StartsWith("###### ") Then
                    Dim hText = trimmed.Substring(7)
                    sb.AppendLine("<h6 id=""" & HeadingToId(hText) & """>" & RenderInline(hText) & "</h6>")
                    Continue For
                End If
                If trimmed.StartsWith("##### ") Then
                    Dim hText = trimmed.Substring(6)
                    sb.AppendLine("<h5 id=""" & HeadingToId(hText) & """>" & RenderInline(hText) & "</h5>")
                    Continue For
                End If
                If trimmed.StartsWith("#### ") Then
                    Dim hText = trimmed.Substring(5)
                    sb.AppendLine("<h4 id=""" & HeadingToId(hText) & """>" & RenderInline(hText) & "</h4>")
                    Continue For
                End If
                If trimmed.StartsWith("### ") Then
                    Dim hText = trimmed.Substring(4)
                    sb.AppendLine("<h3 id=""" & HeadingToId(hText) & """>" & RenderInline(hText) & "</h3>")
                    Continue For
                End If
                If trimmed.StartsWith("## ") Then
                    Dim hText = trimmed.Substring(3)
                    sb.AppendLine("<h2 id=""" & HeadingToId(hText) & """>" & RenderInline(hText) & "</h2>")
                    Continue For
                End If
                If trimmed.StartsWith("# ") Then
                    Dim hText = trimmed.Substring(2)
                    sb.AppendLine("<h1 id=""" & HeadingToId(hText) & """>" & RenderInline(hText) & "</h1>")
                    Continue For
                End If

                ' table row
                If trimmed.StartsWith("|") AndAlso trimmed.EndsWith("|") Then
                    Dim cols = trimmed.Trim({"|"c}).Split("|"c)
                    Dim isHeader = i + 1 < lines.Length AndAlso lines(i + 1).Trim().Replace(" ", "").StartsWith("|") AndAlso lines(i + 1).Trim().Replace(" ", "").Replace("|", "").Replace("-", "").Trim().Length = 0

                    If Not inTable Then
                        sb.AppendLine("<table>")
                        inTable = True
                    End If

                    If isHeader Then
                        sb.AppendLine("<thead><tr>")
                        For Each col In cols
                            sb.AppendLine("<th>" & RenderInline(col.Trim()) & "</th>")
                        Next
                        sb.AppendLine("</tr></thead><tbody>")
                        i += 1 ' skip separator row
                    Else
                        sb.AppendLine("<tr>")
                        For Each col In cols
                            sb.AppendLine("<td>" & RenderInline(col.Trim()) & "</td>")
                        Next
                        sb.AppendLine("</tr>")
                    End If
                    Continue For
                End If

                ' close table if we were in one but now have non-table content
                If inTable Then
                    sb.AppendLine("</table>")
                    inTable = False
                End If

                ' list item
                If trimmed.StartsWith("- ") OrElse trimmed.StartsWith("* ") OrElse trimmed.StartsWith("+ ") Then
                    If Not inList Then
                        sb.AppendLine("<ul>")
                        inList = True
                    End If
                    Dim item = trimmed.Substring(2)
                    ' check for sub-list (indented)
                    sb.AppendLine("<li>" & RenderInline(item) & "</li>")
                    Continue For
                End If

                If Char.IsDigit(trimmed.Chars(0)) AndAlso trimmed.Contains(". ") Then
                    If Not inList Then
                        sb.AppendLine("<ol>")
                        inList = True
                    End If
                    Dim idx = trimmed.IndexOf(". ")
                    Dim item = trimmed.Substring(idx + 2)
                    sb.AppendLine("<li>" & RenderInline(item) & "</li>")
                    Continue For
                End If

                If inList Then
                    sb.AppendLine("</ul>")
                    inList = False
                End If

                ' regular paragraph
                sb.AppendLine("<p>" & RenderInline(trimmed) & "</p>")
            Next

            If inCodeBlock Then sb.AppendLine("</code></pre>")
            If inTable Then sb.AppendLine("</table>")
            If inList Then sb.AppendLine("</ul>")

            sb.AppendLine("</body></html>")
            Return sb.ToString()
        End Function

        Private Shared Function HeadingToId(heading As String) As String
            Dim id = heading.ToLowerInvariant()
            id = System.Text.RegularExpressions.Regex.Replace(id, "[^a-z0-9\s-]", "")
            id = System.Text.RegularExpressions.Regex.Replace(id.Trim(), "\s+", "-")
            id = System.Text.RegularExpressions.Regex.Replace(id, "-+", "-")
            Return id
        End Function

        Private Shared Function RenderInline(text As String) As String
            Dim result = EscapeHtml(text)
            ' **bold**
            result = System.Text.RegularExpressions.Regex.Replace(result, "\*\*(.+?)\*\*", "<strong>$1</strong>")
            ' *italic*
            result = System.Text.RegularExpressions.Regex.Replace(result, "\*(.+?)\*", "<em>$1</em>")
            ' `inline code`
            result = System.Text.RegularExpressions.Regex.Replace(result, "`(.+?)`", "<code>$1</code>")
            ' [text](url)
            result = System.Text.RegularExpressions.Regex.Replace(result, "\[(.+?)\]\((.+?)\)", "<a href=""$2"">$1</a>")
            Return result
        End Function

        Private Shared Function EscapeHtml(text As String) As String
            Return text.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;")
        End Function

        Private Sub btnLogout_Click(sender As Object, e As EventArgs) Handles btnLogout.Click
            Dim confirm = MessageBox.Show("Are you sure you want to sign out?", "Sign Out", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If confirm = DialogResult.Yes Then
                AppState.IsLoggedIn = False
                AppState.NotificationService?.Stop()
                Me.Close()
            End If
        End Sub

        Private Sub MainForm_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
            notifyIcon.Visible = False
            AppState.NotificationService?.Dispose()
        End Sub

    End Class

End Namespace
