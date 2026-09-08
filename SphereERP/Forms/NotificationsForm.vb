Option Strict On
Imports System.Windows.Forms
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Views.Grid
Imports SphereERP.Data

Namespace Forms

    Public Class NotificationsForm

        Private ReadOnly _repo As New NotificationRepository()
        Private _allNotifications As New List(Of NotificationRow)()

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub NotificationsForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            cmbEventTypeFilter.Properties.Items.Clear()
            cmbEventTypeFilter.Properties.Items.AddRange(New Object() {"(All)", "Validation", "Issuance", "Rejection", "Cancellation"})
            cmbEventTypeFilter.SelectedIndex = 0

            RefreshList()
        End Sub

        Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
            RefreshList()
        End Sub

        Private Sub chkUnreadOnly_CheckedChanged(sender As Object, e As EventArgs) Handles chkUnreadOnly.CheckedChanged
            RefreshList()
        End Sub

        Private Sub cmbEventTypeFilter_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbEventTypeFilter.SelectedIndexChanged
            ApplyFilterAndBind()
        End Sub

        Private Sub RefreshList()
            Try
                _allNotifications = _repo.GetRecent(unreadOnly:=chkUnreadOnly.Checked, top:=300)
                ApplyFilterAndBind()
            Catch ex As Exception
                lblStatus.Appearance.ForeColor = System.Drawing.Color.DarkRed
                lblStatus.Text = "Failed to load notifications: " & ex.Message
            End Try
        End Sub

        Private Sub ApplyFilterAndBind()
            Dim filtered = _allNotifications
            If cmbEventTypeFilter.SelectedIndex > 0 Then
                Dim selectedType = Convert.ToString(cmbEventTypeFilter.SelectedItem)
                filtered = _allNotifications.Where(Function(n) String.Equals(n.EventType, selectedType, StringComparison.OrdinalIgnoreCase)).ToList()
            End If

            GridViewNotifications.Columns.Clear()

            With GridViewNotifications.Columns.AddField("CreatedAtUtc")
                .Caption = "Date/Time"
                .VisibleIndex = 0
            End With
            With GridViewNotifications.Columns.AddField("EventType")
                .Caption = "Event"
                .VisibleIndex = 1
            End With
            With GridViewNotifications.Columns.AddField("InternalId")
                .Caption = "Internal ID"
                .VisibleIndex = 2
            End With
            With GridViewNotifications.Columns.AddField("Message")
                .Caption = "Message"
                .VisibleIndex = 3
            End With

            Dim repCheck As New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
            With GridViewNotifications.Columns.AddField("IsRead")
                .Caption = "Read"
                .ColumnEdit = repCheck
                .VisibleIndex = 4
            End With

            gridNotifications.DataSource = filtered

            Dim unreadCount = _repo.GetUnreadCount()
            lblStatus.Appearance.ForeColor = System.Drawing.Color.DimGray
            lblStatus.Text = $"{filtered.Count} notification(s) shown. {unreadCount} unread overall."
        End Sub

        Private Sub GridViewNotifications_RowStyle(sender As Object, e As RowStyleEventArgs) Handles GridViewNotifications.RowStyle
            Dim n = TryCast(GridViewNotifications.GetRow(e.RowHandle), NotificationRow)
            If n IsNot Nothing Then
                If Not n.IsRead Then
                    e.Appearance.Font = New System.Drawing.Font(GridViewNotifications.Appearance.Row.Font, System.Drawing.FontStyle.Bold)
                End If
                Select Case n.EventType
                    Case "Rejection"
                        e.Appearance.ForeColor = System.Drawing.Color.DarkRed
                    Case "Cancellation"
                        e.Appearance.ForeColor = System.Drawing.Color.DarkOrange
                    Case "Issuance"
                        e.Appearance.ForeColor = System.Drawing.Color.DarkGreen
                End Select
            End If
        End Sub

        Private Sub btnMarkAllRead_Click(sender As Object, e As EventArgs) Handles btnMarkAllRead.Click
            Try
                _repo.MarkAllRead()
                RefreshList()
                lblStatus.Appearance.ForeColor = System.Drawing.Color.DarkGreen
                lblStatus.Text = "All notifications marked as read."
            Catch ex As Exception
                lblStatus.Appearance.ForeColor = System.Drawing.Color.DarkRed
                lblStatus.Text = "Failed to mark notifications as read: " & ex.Message
            End Try
        End Sub

    End Class

End Namespace
