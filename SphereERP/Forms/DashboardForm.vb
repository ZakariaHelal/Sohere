Option Strict On
Imports System.Linq
Imports System.Windows.Forms
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Views.Grid
Imports SphereERP.Data
Imports SphereERP.[Global]

Namespace Forms

    Public Class DashboardForm

        Private ReadOnly _docRepo As New DocumentRepository()

        Private _lblTotalDocsCount, _lblValidCount, _lblSubmittedCount, _lblRejectedCount, _lblCancelledCount, _lblTotalAmountCount As LabelControl

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub DashboardForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            dtDateFrom.EditValue = GetFirsttDayOfYear(Today)
            dtDateTo.EditValue = GetLastDayOfMonth(Today)
            cmbDirection.SelectedIndex = 0
            BuildCardLabels()
            RefreshData()
        End Sub

        Private Sub BuildCardLabels()
            _lblTotalDocsCount = AddCardLabels(cardTotalDocs, "Total Documents")
            _lblValidCount = AddCardLabels(cardValid, "Valid")
            _lblSubmittedCount = AddCardLabels(cardSubmitted, "Pending/Submitted")
            _lblRejectedCount = AddCardLabels(cardRejected, "Rejected/Invalid")
            _lblCancelledCount = AddCardLabels(cardCancelled, "Cancelled")
            _lblTotalAmountCount = AddCardLabels(cardTotalAmount, "Total Amount (EGP)", isAmount:=True)
        End Sub

        Private Function AddCardLabels(card As PanelControl, title As String, Optional isAmount As Boolean = False) As LabelControl
            Dim lblCount As New LabelControl With {
                .Font = New System.Drawing.Font("Segoe UI", If(isAmount, 18.0!, 26.0!), System.Drawing.FontStyle.Bold),
                .ForeColor = System.Drawing.Color.White,
                .Location = New System.Drawing.Point(12, 10),
                .Size = New System.Drawing.Size(card.Width - 24, If(isAmount, 36, 46)),
                .Text = "0"
            }
            Dim lblTitle As New LabelControl With {
                .Font = New System.Drawing.Font("Segoe UI", 9.0!),
                .ForeColor = System.Drawing.Color.FromArgb(230, 230, 230),
                .Location = New System.Drawing.Point(12, If(isAmount, 50, 62)),
                .Size = New System.Drawing.Size(card.Width - 24, 36),
                .Text = title
            }
            card.Controls.Add(lblCount)
            card.Controls.Add(lblTitle)
            Return lblCount
        End Function

        Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
            RefreshData()
        End Sub

        Private Sub RefreshData()
            Try
                Dim allDocs = _docRepo.Search(
                    dateFrom:=dtDateFrom.DateTime.Date,
                    dateTo:=dtDateTo.DateTime.Date)

                Dim myRin = If(AppState.CurrentSettings?.TaxpayerRIN, "")

                Select Case Convert.ToString(cmbDirection.SelectedItem)
                    Case "Sent (Issued by me)"
                        allDocs = allDocs.Where(Function(d) String.Equals(d.IssuerRIN, myRin, StringComparison.OrdinalIgnoreCase)).ToList()
                    Case "Received (Issued to me)"
                        allDocs = allDocs.Where(Function(d) String.Equals(d.ReceiverRIN, myRin, StringComparison.OrdinalIgnoreCase)).ToList()
                End Select

                Dim totalCount = allDocs.Count
                Dim validCount = allDocs.Where(Function(d) d.Status = "Valid").Count()
                Dim submittedCount = allDocs.Where(Function(d) d.Status = "Submitted").Count()
                Dim rejectedCount = allDocs.Where(Function(d) d.Status = "Rejected" OrElse d.Status = "Invalid" OrElse d.Status = "Failed").Count()
                Dim cancelledCount = allDocs.Where(Function(d) d.Status = "Cancelled").Count()
                Dim totalAmount = allDocs.Where(Function(d) d.Status = "Valid").Sum(Function(d) d.TotalAmount)

                _lblTotalDocsCount.Text = totalCount.ToString("N0")
                _lblValidCount.Text = validCount.ToString("N0")
                _lblSubmittedCount.Text = submittedCount.ToString("N0")
                _lblRejectedCount.Text = rejectedCount.ToString("N0")
                _lblCancelledCount.Text = cancelledCount.ToString("N0")
                _lblTotalAmountCount.Text = totalAmount.ToString("N0")

                Dim recentDocs = allDocs.OrderByDescending(Function(d) d.CreatedAtUtc).Take(50).ToList()

                For Each d In recentDocs
                    If String.Equals(d.IssuerRIN, myRin, StringComparison.OrdinalIgnoreCase) Then
                        d.Direction = "Sent"
                    ElseIf String.Equals(d.ReceiverRIN, myRin, StringComparison.OrdinalIgnoreCase) Then
                        d.Direction = "Received"
                    Else
                        d.Direction = ""
                    End If
                Next

                BindRecentDocuments(recentDocs)

                Dim viewLabel = Convert.ToString(cmbDirection.SelectedItem)
                If viewLabel = "(All)" Then viewLabel = "all"
                lblPageSubtitle.Text = $"Showing {viewLabel} documents from {dtDateFrom.DateTime:yyyy-MM-dd} to {dtDateTo.DateTime:yyyy-MM-dd}"

            Catch ex As Exception
                MessageBox.Show("Failed to load dashboard data: " & ex.Message, "Dashboard", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Private Sub BindRecentDocuments(rows As List(Of DocumentRow))
            gridViewRecentDocs.Columns.Clear()

            gridViewRecentDocs.ColumnPanelRowHeight = 50

            Dim col1 = gridViewRecentDocs.Columns.AddField("InternalId")
            col1.VisibleIndex = 0
            col1.Caption = "Internal ID"
            col1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            col1.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center

            Dim col2 = gridViewRecentDocs.Columns.AddField("Direction")
            col2.VisibleIndex = 1
            col2.Caption = "Direction"
            col2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            col2.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
            col2.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center

            Dim col3 = gridViewRecentDocs.Columns.AddField("DocumentTypeDisplay")
            col3.VisibleIndex = 2
            col3.Caption = "Type"
            col3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            col3.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
            col3.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center

            Dim col4 = gridViewRecentDocs.Columns.AddField("IssuerName")
            col4.VisibleIndex = 3
            col4.Caption = "Issuer"
            col4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            col4.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center

            Dim col5 = gridViewRecentDocs.Columns.AddField("ReceiverName")
            col5.VisibleIndex = 4
            col5.Caption = "Receiver"
            col5.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            col5.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center

            Dim col7 = gridViewRecentDocs.Columns.AddField("DateTimeIssued")
            col7.VisibleIndex = 6
            col7.Caption = "Issue Date"
            col7.SortOrder = DevExpress.Data.ColumnSortOrder.Descending
            col7.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            col7.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
            col7.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center

            Dim col8 = gridViewRecentDocs.Columns.AddField("TotalAmount")
            col8.VisibleIndex = 7
            col8.Caption = "Total (EGP)"
            col8.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
            col8.DisplayFormat.FormatString = "N2"
            col8.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            col8.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
            col8.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            col8.Summary.Add(DevExpress.Data.SummaryItemType.Sum, "TotalAmount")
            col8.SummaryItem.DisplayFormat = "{0:N2}"

            Dim col9 = gridViewRecentDocs.Columns.AddField("Status")
            col9.VisibleIndex = 8
            col9.Caption = "Status"
            col9.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            col9.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
            col9.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center

            gridRecentDocs.DataSource = rows
            gridViewRecentDocs.BestFitColumns()
            gridViewRecentDocs.RefreshData()
        End Sub


    End Class

End Namespace
