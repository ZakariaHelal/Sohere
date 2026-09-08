Option Strict On
Imports System.Windows.Forms
Imports DevExpress.XtraEditors
Imports SphereERP.Data
Imports SphereERP.Models

Namespace Forms

    Public Class ReceiverEditorDialog

        Private Shared ReadOnly _countryOptions As String() = {
            "EG - مصر", "SA - السعودية", "AE - الإمارات",
            "KW - الكويت", "QA - قطر", "OM - عمان", "BH - البحرين",
            "JO - الأردن", "LB - لبنان", "IQ - العراق",
            "US - الولايات المتحدة", "GB - المملكة المتحدة", "DE - ألمانيا",
            "FR - فرنسا", "IT - إيطاليا", "ES - إسبانيا", "NL - هولندا",
            "BE - بلجيكا", "CH - سويسرا", "SE - السويد", "NO - النرويج",
            "DK - الدنمارك", "PL - بولندا", "TR - تركيا", "RU - روسيا",
            "CN - الصين", "JP - اليابان", "KR - كوريا الجنوبية", "IN - الهند",
            "AU - أستراليا", "BR - البرازيل", "ZA - جنوب أفريقيا", "NG - نيجيريا",
            "MA - المغرب", "DZ - الجزائر", "TN - تونس", "LY - ليبيا",
            "SD - السودان", "KE - كينيا", "ET - إثيوبيا",
            "SG - سنغافورة", "MY - ماليزيا", "ID - إندونيسيا"
        }

        Private Shared ReadOnly _governorates As String() = {
            "الإسكندرية", "أسوان", "أسيوط", "البحيرة", "بني سويف",
            "القاهرة", "الدقهلية", "دمياط", "الفيوم", "الغربية",
            "الجيزة", "الإسماعيلية", "كفر الشيخ", "الأقصر", "مطروح",
            "المنيا", "المنوفية", "الوادي الجديد", "شمال سيناء",
            "بورسعيد", "القليوبية", "قنا", "البحر الأحمر", "الشرقية",
            "سوهاج", "جنوب سيناء", "السويس"
        }

        Private ReadOnly _repo As New ReceiverRepository()
        Private _editId As Integer? = Nothing

        Public Sub New(Optional receiverId As Integer? = Nothing)
            InitializeComponent()
            PopulateCountries()
            PopulateGovernorates()
            _editId = receiverId
            If receiverId.HasValue Then
                Me.Text = "Edit Receiver"
                LoadReceiver(receiverId.Value)
            Else
                Me.Text = "New Receiver"
            End If
        End Sub

        Private Sub PopulateCountries()
            cmbCountry.Properties.Items.Clear()
            cmbCountry.Properties.Items.AddRange(_countryOptions)
            cmbCountry.SelectedIndex = 0 ' EG - Egypt
        End Sub

        Private Sub PopulateGovernorates()
            cmbGovernate.Properties.Items.Clear()
            cmbGovernate.Properties.Items.AddRange(_governorates)
        End Sub

        Private Sub LoadReceiver(id As Integer)
            Dim rec = _repo.GetById(id)
            If rec Is Nothing Then Return
            txtRin.Text = rec.Rin
            txtName.Text = rec.Name
            Dim countryCode = rec.Country
            For Each item In cmbCountry.Properties.Items
                If Convert.ToString(item).StartsWith(countryCode + " - ") Then
                    cmbCountry.SelectedItem = item
                    Exit For
                End If
            Next
            If cmbGovernate.Properties.Items.Contains(rec.Governate) Then
                cmbGovernate.SelectedItem = rec.Governate
            ElseIf Not String.IsNullOrWhiteSpace(rec.Governate) Then
                cmbGovernate.SelectedItem = rec.Governate
            End If
            txtCity.Text = rec.RegionCity
            txtStreet.Text = rec.Street
            txtBuilding.Text = rec.BuildingNumber
            txtRin.Properties.ReadOnly = True
        End Sub

        Private Function ValidateInput() As Boolean
            If String.IsNullOrWhiteSpace(txtRin.Text) Then
                XtraMessageBox.Show("RIN / National ID is required.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtRin.Focus()
                Return False
            End If
            If String.IsNullOrWhiteSpace(txtName.Text) Then
                XtraMessageBox.Show("Receiver name is required.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtName.Focus()
                Return False
            End If
            Return True
        End Function

        Private Function CountryCodeValue() As String
            Dim sel = Convert.ToString(cmbCountry.SelectedItem)
            If sel.Contains(" - ") Then Return sel.Split(New String() {" - "}, StringSplitOptions.None)(0)
            Return "EG"
        End Function

        Private Function GovernateValue() As String
            Dim sel = Convert.ToString(cmbGovernate.SelectedItem)
            Return If(String.IsNullOrWhiteSpace(sel), "", sel)
        End Function

        Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
            If Not ValidateInput() Then Return

            If _editId.HasValue Then
                _repo.Update(New ReceiverModel With {
                    .Id = _editId.Value,
                    .Rin = txtRin.Text.Trim(),
                    .Name = txtName.Text.Trim(),
                    .Country = CountryCodeValue(),
                    .Governate = GovernateValue(),
                    .RegionCity = txtCity.Text.Trim(),
                    .Street = txtStreet.Text.Trim(),
                    .BuildingNumber = txtBuilding.Text.Trim()
                })
                XtraMessageBox.Show("Receiver updated successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.DialogResult = DialogResult.OK
                Me.Close()
            Else
                Dim result = _repo.Insert(New ReceiverModel With {
                    .Rin = txtRin.Text.Trim(),
                    .Name = txtName.Text.Trim(),
                    .Country = CountryCodeValue(),
                    .Governate = GovernateValue(),
                    .RegionCity = txtCity.Text.Trim(),
                    .Street = txtStreet.Text.Trim(),
                    .BuildingNumber = txtBuilding.Text.Trim()
                })

                If result.isDuplicate Then
                    XtraMessageBox.Show("A receiver with this RIN already exists.", "Duplicate", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Else
                    XtraMessageBox.Show("Receiver created successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Me.DialogResult = DialogResult.OK
                    Me.Close()
                End If
            End If
        End Sub

        Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
            Me.DialogResult = DialogResult.Cancel
            Me.Close()
        End Sub

    End Class

End Namespace
