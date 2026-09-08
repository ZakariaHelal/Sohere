Option Strict On
Imports DevExpress.XtraEditors
Imports System.Windows.Forms
Imports SphereERP.Models

Namespace Forms

    Public Class LineItemEditorForm

        Private ReadOnly _validTaxTypes As String() = {"T1", "T2", "T3", "T4", "T5", "T6", "T7", "T8", "T9", "T10", "T11", "T12", "T13", "T14", "T15", "T16", "T17", "T18", "T19", "T20"}

        Private Shared ReadOnly _taxSubTypesByType As Dictionary(Of String, String()) =
            New Dictionary(Of String, String()) From {
                {"T1", {"V001", "V002", "V003", "V004", "V005", "V006", "V007", "V008", "V009", "V010"}},
                {"T2", {"Tbl01"}},
                {"T3", {"Tbl02"}},
                {"T4", {"W001", "W002", "W003", "W004", "W005", "W006", "W007", "W008", "W009", "W010", "W011", "W012", "W013", "W014", "W015", "W016"}},
                {"T5", {"ST01"}},
                {"T6", {"ST02"}},
                {"T7", {"Ent01", "Ent02"}},
                {"T8", {"RD01", "RD02"}},
                {"T9", {"SC01", "SC02"}},
                {"T10", {"Mn01", "Mn02"}},
                {"T11", {"MI01", "MI02"}},
                {"T12", {"OF01", "OF02"}},
                {"T13", {"ST03"}},
                {"T14", {"ST04"}},
                {"T15", {"Ent03", "Ent04"}},
                {"T16", {"RD03", "RD04"}},
                {"T17", {"SC03", "SC04"}},
                {"T18", {"Mn03", "Mn04"}},
                {"T19", {"MI03", "MI04"}},
                {"T20", {"OF03", "OF04"}}
            }

        Private Shared ReadOnly _taxTypeDescriptions As Dictionary(Of String, String) =
            New Dictionary(Of String, String) From {
                {"T1", "Value Added Tax"}, {"T2", "Table Tax (%)"}, {"T3", "Table Tax (Fixed)"},
                {"T4", "Withholding Tax"}, {"T5", "Stamping Tax (%)"}, {"T6", "Stamping Tax (Fixed)"},
                {"T7", "Entertainment Tax"}, {"T8", "Resource Dev. Fee"}, {"T9", "Service Charges"},
                {"T10", "Municipality Fees"}, {"T11", "Medical Insurance Fee"}, {"T12", "Other Fees"},
                {"T13", "Stamping Tax (%) (Non-tax.)"}, {"T14", "Stamping Tax (Fixed) (Non-tax.)"},
                {"T15", "Entertainment Tax (Non-tax.)"}, {"T16", "Resource Dev. Fee (Non-tax.)"},
                {"T17", "Service Charges (Non-tax.)"}, {"T18", "Municipality Fees (Non-tax.)"},
                {"T19", "Medical Ins. Fee (Non-tax.)"}, {"T20", "Other Fees (Non-tax.)"}
            }

        Private Shared ReadOnly _taxSubTypeDescriptions As Dictionary(Of String, String) =
            New Dictionary(Of String, String) From {
                {"V001", "Export"}, {"V002", "Export to Free Areas"}, {"V003", "Exempted"},
                {"V004", "Non-taxable"}, {"V005", "Diplomats Exemption"}, {"V006", "Defence Exemption"},
                {"V007", "Agreements Exemption"}, {"V008", "Special Exemptions"},
                {"V009", "General Item Sales"}, {"V010", "Other Rates"},
                {"Tbl01", "Table Tax (%)"}, {"Tbl02", "Table Tax (Fixed)"},
                {"W001", "Contracting"}, {"W002", "Supplies"}, {"W003", "Purchases"},
                {"W004", "Services"}, {"W005", "Cooperative Soc."}, {"W006", "Commission Agency"},
                {"W007", "Smoke & Cement Co."}, {"W008", "Petroleum & Telecom"},
                {"W009", "Export Subsidies"}, {"W010", "Professional Fees"},
                {"W011", "Commission & Brokerage"}, {"W012", "Hospitals"}, {"W013", "Royalties"},
                {"W014", "Customs Clearance"}, {"W015", "Exemption"}, {"W016", "Advance Payment"},
                {"ST01", "Stamping Tax (%)"}, {"ST02", "Stamping Tax (Fixed)"},
                {"Ent01", "Entertainment Tax (%)"}, {"Ent02", "Entertainment Tax (Fixed)"},
                {"RD01", "Resource Dev. Fee (%)"}, {"RD02", "Resource Dev. Fee (Fixed)"},
                {"SC01", "Service Charges (%)"}, {"SC02", "Service Charges (Fixed)"},
                {"Mn01", "Municipality Fees (%)"}, {"Mn02", "Municipality Fees (Fixed)"},
                {"MI01", "Medical Ins. Fee (%)"}, {"MI02", "Medical Ins. Fee (Fixed)"},
                {"OF01", "Other Fees (%)"}, {"OF02", "Other Fees (Fixed)"},
                {"ST03", "Stamping Tax (%) (Non-tax.)"}, {"ST04", "Stamping Tax (Fixed) (Non-tax.)"},
                {"Ent03", "Entertainment Tax (%) (Non-tax.)"}, {"Ent04", "Entertainment Tax (Fixed) (Non-tax.)"},
                {"RD03", "Resource Dev. Fee (%) (Non-tax.)"}, {"RD04", "Resource Dev. Fee (Fixed) (Non-tax.)"},
                {"SC03", "Service Charges (%) (Non-tax.)"}, {"SC04", "Service Charges (Fixed) (Non-tax.)"},
                {"Mn03", "Municipality Fees (%) (Non-tax.)"}, {"Mn04", "Municipality Fees (Fixed) (Non-tax.)"},
                {"MI03", "Medical Ins. Fee (%) (Non-tax.)"}, {"MI04", "Medical Ins. Fee (Fixed) (Non-tax.)"},
                {"OF03", "Other Fees (%) (Non-tax.)"}, {"OF04", "Other Fees (Fixed) (Non-tax.)"}
            }

        Public Property ResultLine As InvoiceLineModel

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Shared Function DisplayItem(code As String, description As String) As String
            Return $"{code} - {description}"
        End Function

        Private Shared Function CodeFromDisplay(display As String) As String
            If String.IsNullOrWhiteSpace(display) Then Return ""
            Dim idx = display.IndexOf(" - ", StringComparison.Ordinal)
            Return If(idx > 0, display.Substring(0, idx), display.Trim())
        End Function

        Private Shared Function FindDisplayByCode(items As Object, code As String) As String
            If String.IsNullOrWhiteSpace(code) Then Return Nothing
            For Each item In CType(items, System.Collections.IEnumerable)
                Dim s = Convert.ToString(item)
                If s IsNot Nothing AndAlso s.StartsWith(code, StringComparison.Ordinal) Then
                    Dim sep = s.IndexOf(" - ", StringComparison.Ordinal)
                    If sep = code.Length Then Return s
                End If
            Next
            Return Nothing
        End Function

        Public Sub LoadLine(line As InvoiceLineModel)
            cmbItemType.Properties.Items.Clear()
            cmbItemType.Properties.Items.AddRange({"EGS", "GS1"})
            cmbTaxType.Properties.Items.Clear()
            cmbTaxType.Properties.Items.AddRange(
                _validTaxTypes.Select(Function(t) DisplayItem(t, _taxTypeDescriptions(t))).ToArray())

            If line Is Nothing Then
                cmbItemType.SelectedItem = "EGS"
                txtUnitType.Text = "EA"
                cmbTaxType.SelectedItem = FindDisplayByCode(cmbTaxType.Properties.Items, "T1")
                PopulateSubTypes("T1")
                cmbTaxSubType.SelectedItem = FindDisplayByCode(cmbTaxSubType.Properties.Items, "V009")
                numTaxRate.EditValue = 14
                numQuantity.EditValue = 1
            Else
                txtDescription.Text = line.Description
                txtItemCode.Text = line.ItemCode
                cmbItemType.SelectedItem = If(cmbItemType.Properties.Items.Contains(line.ItemType), line.ItemType, "EGS")
                txtUnitType.Text = line.UnitType
                numQuantity.EditValue = If(line.Quantity > 0, line.Quantity, 1)
                numUnitPrice.EditValue = Math.Max(0, line.UnitValue.AmountEGP)
                numDiscount.EditValue = Math.Max(0, line.Discount.Amount)

                If line.TaxableItems IsNot Nothing AndAlso line.TaxableItems.Count > 0 Then
                    Dim firstTax = line.TaxableItems(0)
                    cmbTaxType.SelectedItem = FindDisplayByCode(cmbTaxType.Properties.Items, firstTax.TaxType)
                    If cmbTaxType.SelectedItem Is Nothing Then
                        cmbTaxType.SelectedItem = FindDisplayByCode(cmbTaxType.Properties.Items, "T1")
                    End If
                    Dim typeCode = CodeFromDisplay(Convert.ToString(cmbTaxType.SelectedItem))
                    PopulateSubTypes(typeCode)
                    If Not String.IsNullOrWhiteSpace(firstTax.SubType) Then
                        cmbTaxSubType.SelectedItem = FindDisplayByCode(cmbTaxSubType.Properties.Items, firstTax.SubType)
                    End If
                    numTaxRate.EditValue = Math.Max(0, Math.Min(100, firstTax.Rate))
                Else
                    cmbTaxType.SelectedItem = FindDisplayByCode(cmbTaxType.Properties.Items, "T1")
                    PopulateSubTypes("T1")
                    cmbTaxSubType.SelectedItem = FindDisplayByCode(cmbTaxSubType.Properties.Items, "V009")
                    numTaxRate.EditValue = 14
                End If
            End If

            UpdateComputedTotal()
        End Sub

        Private Sub cmbTaxType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbTaxType.SelectedIndexChanged
            Dim display = Convert.ToString(cmbTaxType.SelectedItem)
            If Not String.IsNullOrWhiteSpace(display) Then
                PopulateSubTypes(CodeFromDisplay(display))
            End If
        End Sub

        Private Sub PopulateSubTypes(taxType As String)
            cmbTaxSubType.Properties.Items.Clear()
            If _taxSubTypesByType.ContainsKey(taxType) AndAlso _taxSubTypeDescriptions.ContainsKey(_taxSubTypesByType(taxType)(0)) Then
                Dim items = _taxSubTypesByType(taxType) _
                    .Select(Function(s) DisplayItem(s, If(_taxSubTypeDescriptions.ContainsKey(s), _taxSubTypeDescriptions(s), s))) _
                    .ToArray()
                cmbTaxSubType.Properties.Items.AddRange(items)
                If cmbTaxSubType.Properties.Items.Count > 0 Then
                    cmbTaxSubType.SelectedItem = cmbTaxSubType.Properties.Items(0)
                End If
            End If
        End Sub

        Private Sub Recalc_Changed(sender As Object, e As EventArgs) Handles numQuantity.EditValueChanged, numUnitPrice.EditValueChanged, numDiscount.EditValueChanged, numTaxRate.EditValueChanged
            UpdateComputedTotal()
        End Sub

        Private Sub UpdateComputedTotal()
            Dim sales = CDec(numQuantity.Value) * CDec(numUnitPrice.Value)
            Dim net = sales - CDec(numDiscount.Value)
            Dim tax = net * (CDec(numTaxRate.Value) / 100D)
            Dim total = net + tax
            lblComputedTotal.Text = $"Line Total: {total:N2} EGP  (Net: {net:N2}, Tax: {tax:N2})"
        End Sub

        Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
            If String.IsNullOrWhiteSpace(txtDescription.Text) Then
                MessageBox.Show("Description is required.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If
            If String.IsNullOrWhiteSpace(txtItemCode.Text) Then
                MessageBox.Show("Item code is required.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim line As New InvoiceLineModel With {
                .Description = txtDescription.Text.Trim(),
                .ItemCode = txtItemCode.Text.Trim(),
                .ItemType = Convert.ToString(cmbItemType.SelectedItem),
                .UnitType = If(String.IsNullOrWhiteSpace(txtUnitType.Text), "EA", txtUnitType.Text.Trim()),
                .Quantity = CDec(numQuantity.EditValue)
            }
            line.UnitValue.AmountEGP = CDec(numUnitPrice.EditValue)
            line.UnitValue.CurrencySold = "EGP"
            line.Discount.Amount = CDec(numDiscount.EditValue)
            line.TaxableItems.Add(New TaxItemModel With {
                .TaxType = CodeFromDisplay(Convert.ToString(cmbTaxType.SelectedItem)),
                .SubType = CodeFromDisplay(Convert.ToString(cmbTaxSubType.SelectedItem)),
                .Rate = CDec(numTaxRate.EditValue)
            })
            line.Recalculate()

            ResultLine = line
            Me.DialogResult = DialogResult.OK
            Me.Close()
        End Sub

        Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
            Me.DialogResult = DialogResult.Cancel
            Me.Close()
        End Sub

    End Class

End Namespace
