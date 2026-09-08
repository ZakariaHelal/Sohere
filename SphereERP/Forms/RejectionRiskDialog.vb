Option Strict On
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.Utils
Imports System.ComponentModel
Imports System.Windows.Forms
Imports SphereERP.Services

Namespace Forms

    ''' <summary>
    ''' Displays the ETA rejection-risk assessment: overall verdict plus a grid of
    ''' findings (blockers / warnings / info) built at runtime, mirroring the
    ''' programmatic-dialog pattern used elsewhere in this codebase.
    ''' </summary>
    Public Class RejectionRiskDialog
        Inherits XtraForm

        Private ReadOnly _report As RiskReport

        Public Sub New(report As RiskReport)
            _report = report
            BuildUi()
        End Sub

        Private Sub BuildUi()
            Text = "ETA Rejection Risk Assessment"
            Width = 1000
            Height = 560
            StartPosition = FormStartPosition.CenterParent
            FormBorderStyle = FormBorderStyle.Sizable
            MaximizeBox = True
            MinimizeBox = False

            Dim verdictColor As System.Drawing.Color =
                If(_report.RiskLevel = "High", System.Drawing.Color.Firebrick,
                   If(_report.RiskLevel = "Medium", System.Drawing.Color.DarkOrange, System.Drawing.Color.ForestGreen))

            Dim lblVerdict As New LabelControl() With {
                .Text = $"{_report.RiskLevel} rejection risk - {_report.BlockerCount} blocker(s), {_report.WarningCount} warning(s)",
                .Dock = DockStyle.Top,
                .AutoSizeMode = LabelAutoSizeMode.None,
                .Height = 32
            }
            lblVerdict.Appearance.Font = New System.Drawing.Font("Segoe UI", 13.0!, System.Drawing.FontStyle.Bold)
            lblVerdict.Appearance.ForeColor = verdictColor
            lblVerdict.Appearance.Options.UseFont = True
            lblVerdict.Appearance.Options.UseForeColor = True
            lblVerdict.Padding = New Padding(12, 8, 0, 0)

            Dim lblSummary As New LabelControl() With {
                .Text = $"Estimated probability that ETA rejects this document: ~{_report.EstimatedRejectionProbability}%   |   Checks based on ETA e-Invoicing SDK requirements.",
                .Dock = DockStyle.Top,
                .Height = 24
            }
            lblSummary.Padding = New Padding(14, 2, 0, 0)

            Dim grid As New GridControl() With {.Dock = DockStyle.Fill}
            Dim view As New GridView()
            CType(grid, ISupportInitialize).BeginInit()
            CType(view, ISupportInitialize).BeginInit()

            grid.MainView = view
            grid.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {view})
            view.GridControl = grid
            view.OptionsBehavior.Editable = False
            view.OptionsView.ShowGroupPanel = False
            view.OptionsBehavior.AutoPopulateColumns = True
            view.RowHeight = 22

            AddHandler view.RowStyle, Sub(sender As Object, e As RowStyleEventArgs)
                                           If e.RowHandle < 0 Then Return
                                           Dim finding = TryCast(view.GetRow(e.RowHandle), RiskFinding)
                                           If finding Is Nothing Then Return
                                           Select Case finding.Severity
                                               Case RiskSeverity.Blocker
                                                   e.Appearance.BackColor = System.Drawing.Color.FromArgb(255, 235, 235)
                                                   e.Appearance.ForeColor = System.Drawing.Color.Firebrick
                                                   e.HighPriority = True
                                               Case RiskSeverity.Warning
                                                   e.Appearance.BackColor = System.Drawing.Color.FromArgb(255, 248, 231)
                                                   e.Appearance.ForeColor = System.Drawing.Color.FromArgb(140, 90, 0)
                                                   e.HighPriority = True
                                           End Select
                                       End Sub

            grid.DataSource = _report.Findings

            CType(view, ISupportInitialize).EndInit()
            CType(grid, ISupportInitialize).EndInit()

            For Each col As GridColumn In view.Columns
                col.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center
                col.AppearanceCell.TextOptions.VAlignment = VertAlignment.Center
            Next
            Dim sevCol = view.Columns.ColumnByFieldName("SeverityDisplay")
            If sevCol IsNot Nothing Then
                sevCol.Caption = "Severity"
                sevCol.VisibleIndex = 0
                sevCol.Width = 80
            End If
            Dim ruleCol = view.Columns.ColumnByFieldName("Rule")
            If ruleCol IsNot Nothing Then
                ruleCol.Caption = "ETA Rule"
                ruleCol.VisibleIndex = 1
                ruleCol.Width = 200
            End If
            Dim msgCol = view.Columns.ColumnByFieldName("Message")
            If msgCol IsNot Nothing Then
                msgCol.Caption = "Finding"
                msgCol.VisibleIndex = 2
            End If
            view.BestFitColumns()
            If msgCol IsNot Nothing AndAlso msgCol.Width < 400 Then msgCol.Width = 400

            Dim btnClose As New SimpleButton() With {
                .Text = "Close",
                .Dock = DockStyle.Bottom,
                .Height = 36
            }
            AddHandler btnClose.Click, Sub() Close()

            Controls.Add(grid)
            Controls.Add(lblSummary)
            Controls.Add(lblVerdict)
            Controls.Add(btnClose)
            grid.BringToFront()
        End Sub

    End Class

End Namespace
