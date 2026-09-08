Option Strict On
Imports System.Windows.Forms
Imports System.Drawing
Imports DevExpress.XtraEditors

Namespace Forms

    Partial Class LicenseActivationForm
        Inherits XtraForm

        Private WithEvents txtLicenseKey As TextEdit
        Private WithEvents btnActivate As SimpleButton
        Private WithEvents btnBuyNow As SimpleButton
        Private WithEvents btnOfflineActivation As SimpleButton
        Private lblStatus As LabelControl
        Private lblHwid As LabelControl
        Private lblTitle As LabelControl

        Private Sub InitializeComponent()
            Me.Text = "License Activation"
            Me.Width = 520
            Me.Height = 400
            Me.StartPosition = FormStartPosition.CenterScreen
            Me.MaximizeBox = False
            Me.FormBorderStyle = FormBorderStyle.FixedDialog

            lblTitle = New LabelControl()
            lblTitle.Text = "Activate ETA Invoicing"
            lblTitle.Left = 24
            lblTitle.Top = 20
            lblTitle.Width = 460
            lblTitle.Appearance.Font = New Font("Segoe UI", 16.0!, FontStyle.Bold)
            lblTitle.Appearance.ForeColor = Color.FromArgb(44, 62, 80)

            Dim lblKey As New LabelControl()
            lblKey.Text = "License Key:"
            lblKey.Left = 24
            lblKey.Top = 70
            lblKey.Width = 100

            txtLicenseKey = New TextEdit()
            txtLicenseKey.Left = 24
            txtLicenseKey.Top = 92
            txtLicenseKey.Width = 460
            txtLicenseKey.Properties.NullText = "XXXX-XXXX-XXXX-XXXX"

            lblHwid = New LabelControl()
            lblHwid.Left = 24
            lblHwid.Top = 130
            lblHwid.Width = 460
            lblHwid.Appearance.ForeColor = Color.Gray

            btnActivate = New SimpleButton()
            btnActivate.Text = "Activate"
            btnActivate.Left = 24
            btnActivate.Top = 170
            btnActivate.Width = 140

            btnBuyNow = New SimpleButton()
            btnBuyNow.Text = "Purchase License"
            btnBuyNow.Left = 180
            btnBuyNow.Top = 170
            btnBuyNow.Width = 140

            btnOfflineActivation = New SimpleButton()
            btnOfflineActivation.Text = "Offline Activation"
            btnOfflineActivation.Left = 336
            btnOfflineActivation.Top = 170
            btnOfflineActivation.Width = 148

            lblStatus = New LabelControl()
            lblStatus.Left = 24
            lblStatus.Top = 210
            lblStatus.Width = 460
            lblStatus.Appearance.ForeColor = Color.Red

            Controls.Add(lblTitle)
            Controls.Add(lblKey)
            Controls.Add(txtLicenseKey)
            Controls.Add(lblHwid)
            Controls.Add(btnActivate)
            Controls.Add(btnBuyNow)
            Controls.Add(btnOfflineActivation)
            Controls.Add(lblStatus)
        End Sub

    End Class

End Namespace
