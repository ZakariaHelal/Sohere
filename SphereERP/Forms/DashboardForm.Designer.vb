Namespace Forms
    Partial Class DashboardForm
        Inherits DevExpress.XtraEditors.XtraForm

        <System.Diagnostics.DebuggerNonUserCode()>
        Protected Overrides Sub Dispose(disposing As Boolean)
            Try
                If disposing AndAlso components IsNot Nothing Then
                    components.Dispose()
                End If
            Finally
                MyBase.Dispose(disposing)
            End Try
        End Sub

        Private components As System.ComponentModel.IContainer

        <System.Diagnostics.DebuggerStepThrough()>
        Private Sub InitializeComponent()
            lblPageTitle = New DevExpress.XtraEditors.LabelControl()
            LayoutControl1 = New DevExpress.XtraLayout.LayoutControl()
            lblPageSubtitle = New DevExpress.XtraEditors.LabelControl()
            cmbDirection = New DevExpress.XtraEditors.ComboBoxEdit()
            pnlKpiContainer = New DevExpress.XtraEditors.PanelControl()
            cardTotalDocs = New DevExpress.XtraEditors.PanelControl()
            cardValid = New DevExpress.XtraEditors.PanelControl()
            cardSubmitted = New DevExpress.XtraEditors.PanelControl()
            cardRejected = New DevExpress.XtraEditors.PanelControl()
            cardCancelled = New DevExpress.XtraEditors.PanelControl()
            cardTotalAmount = New DevExpress.XtraEditors.PanelControl()
            dtDateFrom = New DevExpress.XtraEditors.DateEdit()
            dtDateTo = New DevExpress.XtraEditors.DateEdit()
            lblRecentDocsHeader = New DevExpress.XtraEditors.LabelControl()
            btnRefresh = New DevExpress.XtraEditors.SimpleButton()
            gridRecentDocs = New DevExpress.XtraGrid.GridControl()
            gridViewRecentDocs = New DevExpress.XtraGrid.Views.Grid.GridView()
            Root = New DevExpress.XtraLayout.LayoutControlGroup()
            EmptySpaceItem3 = New DevExpress.XtraLayout.EmptySpaceItem()
            LayoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
            LayoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
            LayoutControlItem3 = New DevExpress.XtraLayout.LayoutControlItem()
            LayoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem()
            EmptySpaceItem5 = New DevExpress.XtraLayout.EmptySpaceItem()
            LayoutControlItem5 = New DevExpress.XtraLayout.LayoutControlItem()
            LayoutControlItem6 = New DevExpress.XtraLayout.LayoutControlItem()
            LayoutControlItem7 = New DevExpress.XtraLayout.LayoutControlItem()
            LayoutControlItem8 = New DevExpress.XtraLayout.LayoutControlItem()
            LayoutControlItem9 = New DevExpress.XtraLayout.LayoutControlItem()
            EmptySpaceItem2 = New DevExpress.XtraLayout.EmptySpaceItem()
            EmptySpaceItem6 = New DevExpress.XtraLayout.EmptySpaceItem()
            CType(LayoutControl1, ComponentModel.ISupportInitialize).BeginInit()
            LayoutControl1.SuspendLayout()
            CType(cmbDirection.Properties, ComponentModel.ISupportInitialize).BeginInit()
            CType(pnlKpiContainer, ComponentModel.ISupportInitialize).BeginInit()
            pnlKpiContainer.SuspendLayout()
            CType(cardTotalDocs, ComponentModel.ISupportInitialize).BeginInit()
            CType(cardValid, ComponentModel.ISupportInitialize).BeginInit()
            CType(cardSubmitted, ComponentModel.ISupportInitialize).BeginInit()
            CType(cardRejected, ComponentModel.ISupportInitialize).BeginInit()
            CType(cardCancelled, ComponentModel.ISupportInitialize).BeginInit()
            CType(cardTotalAmount, ComponentModel.ISupportInitialize).BeginInit()
            CType(dtDateFrom.Properties, ComponentModel.ISupportInitialize).BeginInit()
            CType(dtDateFrom.Properties.CalendarTimeProperties, ComponentModel.ISupportInitialize).BeginInit()
            CType(dtDateTo.Properties, ComponentModel.ISupportInitialize).BeginInit()
            CType(dtDateTo.Properties.CalendarTimeProperties, ComponentModel.ISupportInitialize).BeginInit()
            CType(gridRecentDocs, ComponentModel.ISupportInitialize).BeginInit()
            CType(gridViewRecentDocs, ComponentModel.ISupportInitialize).BeginInit()
            CType(Root, ComponentModel.ISupportInitialize).BeginInit()
            CType(EmptySpaceItem3, ComponentModel.ISupportInitialize).BeginInit()
            CType(LayoutControlItem1, ComponentModel.ISupportInitialize).BeginInit()
            CType(LayoutControlItem2, ComponentModel.ISupportInitialize).BeginInit()
            CType(LayoutControlItem3, ComponentModel.ISupportInitialize).BeginInit()
            CType(LayoutControlItem4, ComponentModel.ISupportInitialize).BeginInit()
            CType(EmptySpaceItem5, ComponentModel.ISupportInitialize).BeginInit()
            CType(LayoutControlItem5, ComponentModel.ISupportInitialize).BeginInit()
            CType(LayoutControlItem6, ComponentModel.ISupportInitialize).BeginInit()
            CType(LayoutControlItem7, ComponentModel.ISupportInitialize).BeginInit()
            CType(LayoutControlItem8, ComponentModel.ISupportInitialize).BeginInit()
            CType(LayoutControlItem9, ComponentModel.ISupportInitialize).BeginInit()
            CType(EmptySpaceItem2, ComponentModel.ISupportInitialize).BeginInit()
            CType(EmptySpaceItem6, ComponentModel.ISupportInitialize).BeginInit()
            SuspendLayout()
            ' 
            ' lblPageTitle
            ' 
            lblPageTitle.Appearance.Font = New System.Drawing.Font("Segoe UI", 16F, Drawing.FontStyle.Bold)
            lblPageTitle.Appearance.ForeColor = Drawing.Color.FromArgb(CByte(30), CByte(40), CByte(55))
            lblPageTitle.Appearance.Options.UseFont = True
            lblPageTitle.Appearance.Options.UseForeColor = True
            lblPageTitle.Location = New System.Drawing.Point(12, 12)
            lblPageTitle.Name = "lblPageTitle"
            lblPageTitle.Size = New System.Drawing.Size(204, 30)
            lblPageTitle.StyleController = LayoutControl1
            lblPageTitle.TabIndex = 0
            lblPageTitle.Text = "Invoicing Dashboard"
            ' 
            ' LayoutControl1
            ' 
            LayoutControl1.Controls.Add(lblPageSubtitle)
            LayoutControl1.Controls.Add(cmbDirection)
            LayoutControl1.Controls.Add(lblPageTitle)
            LayoutControl1.Controls.Add(pnlKpiContainer)
            LayoutControl1.Controls.Add(dtDateFrom)
            LayoutControl1.Controls.Add(dtDateTo)
            LayoutControl1.Controls.Add(lblRecentDocsHeader)
            LayoutControl1.Controls.Add(btnRefresh)
            LayoutControl1.Controls.Add(gridRecentDocs)
            LayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill
            LayoutControl1.Location = New System.Drawing.Point(0, 0)
            LayoutControl1.Name = "LayoutControl1"
            LayoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(468, 379, 650, 400)
            LayoutControl1.Root = Root
            LayoutControl1.Size = New System.Drawing.Size(1491, 600)
            LayoutControl1.TabIndex = 12
            LayoutControl1.Text = "LayoutControl1"
            ' 
            ' lblPageSubtitle
            ' 
            lblPageSubtitle.Appearance.Font = New System.Drawing.Font("Segoe UI", 9.5F)
            lblPageSubtitle.Appearance.ForeColor = Drawing.Color.Gray
            lblPageSubtitle.Appearance.Options.UseFont = True
            lblPageSubtitle.Appearance.Options.UseForeColor = True
            lblPageSubtitle.Location = New System.Drawing.Point(12, 46)
            lblPageSubtitle.Name = "lblPageSubtitle"
            lblPageSubtitle.Size = New System.Drawing.Size(209, 17)
            lblPageSubtitle.StyleController = LayoutControl1
            lblPageSubtitle.TabIndex = 1
            lblPageSubtitle.Text = "Overview of your e-invoicing activity"
            ' 
            ' cmbDirection
            ' 
            cmbDirection.Anchor = System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right
            cmbDirection.Location = New System.Drawing.Point(1194, 115)
            cmbDirection.Name = "cmbDirection"
            cmbDirection.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
            cmbDirection.Properties.Items.AddRange(New Object() {"(All)", "Sent (Issued by me)", "Received (Issued to me)"})
            cmbDirection.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
            cmbDirection.Size = New System.Drawing.Size(152, 20)
            cmbDirection.StyleController = LayoutControl1
            cmbDirection.TabIndex = 4
            ' 
            ' pnlKpiContainer
            ' 
            pnlKpiContainer.Anchor = System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right
            pnlKpiContainer.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
            pnlKpiContainer.Controls.Add(cardTotalDocs)
            pnlKpiContainer.Controls.Add(cardValid)
            pnlKpiContainer.Controls.Add(cardSubmitted)
            pnlKpiContainer.Controls.Add(cardRejected)
            pnlKpiContainer.Controls.Add(cardCancelled)
            pnlKpiContainer.Controls.Add(cardTotalAmount)
            pnlKpiContainer.Location = New System.Drawing.Point(12, 67)
            pnlKpiContainer.Name = "pnlKpiContainer"
            pnlKpiContainer.Size = New System.Drawing.Size(1124, 130)
            pnlKpiContainer.TabIndex = 9
            ' 
            ' cardTotalDocs
            ' 
            cardTotalDocs.Appearance.BackColor = Drawing.Color.FromArgb(CByte(45), CByte(90), CByte(160))
            cardTotalDocs.Appearance.Options.UseBackColor = True
            cardTotalDocs.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
            cardTotalDocs.Location = New System.Drawing.Point(0, 0)
            cardTotalDocs.Margin = New System.Windows.Forms.Padding(0, 0, 15, 15)
            cardTotalDocs.Name = "cardTotalDocs"
            cardTotalDocs.Size = New System.Drawing.Size(165, 110)
            cardTotalDocs.TabIndex = 0
            ' 
            ' cardValid
            ' 
            cardValid.Appearance.BackColor = Drawing.Color.FromArgb(CByte(35), CByte(130), CByte(80))
            cardValid.Appearance.Options.UseBackColor = True
            cardValid.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
            cardValid.Location = New System.Drawing.Point(180, 0)
            cardValid.Margin = New System.Windows.Forms.Padding(0, 0, 15, 15)
            cardValid.Name = "cardValid"
            cardValid.Size = New System.Drawing.Size(165, 110)
            cardValid.TabIndex = 1
            ' 
            ' cardSubmitted
            ' 
            cardSubmitted.Appearance.BackColor = Drawing.Color.FromArgb(CByte(190), CByte(140), CByte(30))
            cardSubmitted.Appearance.Options.UseBackColor = True
            cardSubmitted.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
            cardSubmitted.Location = New System.Drawing.Point(360, 0)
            cardSubmitted.Margin = New System.Windows.Forms.Padding(0, 0, 15, 15)
            cardSubmitted.Name = "cardSubmitted"
            cardSubmitted.Size = New System.Drawing.Size(165, 110)
            cardSubmitted.TabIndex = 2
            ' 
            ' cardRejected
            ' 
            cardRejected.Appearance.BackColor = Drawing.Color.FromArgb(CByte(170), CByte(50), CByte(50))
            cardRejected.Appearance.Options.UseBackColor = True
            cardRejected.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
            cardRejected.Location = New System.Drawing.Point(540, 0)
            cardRejected.Margin = New System.Windows.Forms.Padding(0, 0, 15, 15)
            cardRejected.Name = "cardRejected"
            cardRejected.Size = New System.Drawing.Size(165, 110)
            cardRejected.TabIndex = 3
            ' 
            ' cardCancelled
            ' 
            cardCancelled.Appearance.BackColor = Drawing.Color.FromArgb(CByte(110), CByte(110), CByte(110))
            cardCancelled.Appearance.Options.UseBackColor = True
            cardCancelled.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
            cardCancelled.Location = New System.Drawing.Point(720, 0)
            cardCancelled.Margin = New System.Windows.Forms.Padding(0, 0, 15, 15)
            cardCancelled.Name = "cardCancelled"
            cardCancelled.Size = New System.Drawing.Size(165, 110)
            cardCancelled.TabIndex = 4
            ' 
            ' cardTotalAmount
            ' 
            cardTotalAmount.Appearance.BackColor = Drawing.Color.FromArgb(CByte(70), CByte(50), CByte(140))
            cardTotalAmount.Appearance.Options.UseBackColor = True
            cardTotalAmount.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
            cardTotalAmount.Location = New System.Drawing.Point(900, 0)
            cardTotalAmount.Margin = New System.Windows.Forms.Padding(0, 0, 15, 15)
            cardTotalAmount.Name = "cardTotalAmount"
            cardTotalAmount.Size = New System.Drawing.Size(165, 110)
            cardTotalAmount.TabIndex = 5
            ' 
            ' dtDateFrom
            ' 
            dtDateFrom.Anchor = System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right
            dtDateFrom.EditValue = New Date(2026, 6, 20, 0, 0, 0, 0)
            dtDateFrom.Location = New System.Drawing.Point(1194, 67)
            dtDateFrom.Name = "dtDateFrom"
            dtDateFrom.Properties.Appearance.Options.UseTextOptions = True
            dtDateFrom.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            dtDateFrom.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
            dtDateFrom.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
            dtDateFrom.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
            dtDateFrom.Size = New System.Drawing.Size(152, 20)
            dtDateFrom.StyleController = LayoutControl1
            dtDateFrom.TabIndex = 0
            ' 
            ' dtDateTo
            ' 
            dtDateTo.Anchor = System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right
            dtDateTo.EditValue = New Date(2026, 6, 20, 0, 0, 0, 0)
            dtDateTo.Location = New System.Drawing.Point(1194, 91)
            dtDateTo.Name = "dtDateTo"
            dtDateTo.Properties.Appearance.Options.UseTextOptions = True
            dtDateTo.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            dtDateTo.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
            dtDateTo.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
            dtDateTo.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
            dtDateTo.Size = New System.Drawing.Size(152, 20)
            dtDateTo.StyleController = LayoutControl1
            dtDateTo.TabIndex = 3
            ' 
            ' lblRecentDocsHeader
            ' 
            lblRecentDocsHeader.Appearance.Font = New System.Drawing.Font("Segoe UI", 11F, Drawing.FontStyle.Bold)
            lblRecentDocsHeader.Appearance.Options.UseFont = True
            lblRecentDocsHeader.Location = New System.Drawing.Point(12, 201)
            lblRecentDocsHeader.Name = "lblRecentDocsHeader"
            lblRecentDocsHeader.Size = New System.Drawing.Size(132, 20)
            lblRecentDocsHeader.StyleController = LayoutControl1
            lblRecentDocsHeader.TabIndex = 1
            lblRecentDocsHeader.Text = "Recent Documents"
            ' 
            ' btnRefresh
            ' 
            btnRefresh.Anchor = System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right
            btnRefresh.Appearance.BackColor = Drawing.Color.FromArgb(CByte(20), CByte(110), CByte(70))
            btnRefresh.Appearance.ForeColor = Drawing.Color.White
            btnRefresh.Appearance.Options.UseBackColor = True
            btnRefresh.Appearance.Options.UseForeColor = True
            btnRefresh.Location = New System.Drawing.Point(1350, 67)
            btnRefresh.Name = "btnRefresh"
            btnRefresh.Size = New System.Drawing.Size(119, 61)
            btnRefresh.StyleController = LayoutControl1
            btnRefresh.TabIndex = 2
            btnRefresh.Text = "Refresh"
            ' 
            ' gridRecentDocs
            ' 
            gridRecentDocs.Anchor = System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right
            gridRecentDocs.Location = New System.Drawing.Point(12, 225)
            gridRecentDocs.MainView = gridViewRecentDocs
            gridRecentDocs.Name = "gridRecentDocs"
            gridRecentDocs.Size = New System.Drawing.Size(1467, 363)
            gridRecentDocs.TabIndex = 5
            gridRecentDocs.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {gridViewRecentDocs})
            ' 
            ' gridViewRecentDocs
            ' 
            gridViewRecentDocs.GridControl = gridRecentDocs
            gridViewRecentDocs.Name = "gridViewRecentDocs"
            gridViewRecentDocs.OptionsBehavior.Editable = False
            gridViewRecentDocs.OptionsView.EnableAppearanceEvenRow = True
            gridViewRecentDocs.OptionsView.ShowFooter = True
            gridViewRecentDocs.OptionsView.ShowGroupPanel = False
            ' 
            ' Root
            ' 
            Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True
            Root.GroupBordersVisible = False
            Root.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {EmptySpaceItem3, LayoutControlItem1, LayoutControlItem2, LayoutControlItem3, LayoutControlItem4, EmptySpaceItem5, LayoutControlItem9, EmptySpaceItem2, EmptySpaceItem6, LayoutControlItem6, LayoutControlItem7, LayoutControlItem8, LayoutControlItem5})
            Root.Name = "Root"
            Root.Size = New System.Drawing.Size(1491, 600)
            Root.TextVisible = False
            ' 
            ' EmptySpaceItem3
            ' 
            EmptySpaceItem3.Location = New System.Drawing.Point(136, 189)
            EmptySpaceItem3.MaxSize = New System.Drawing.Size(0, 24)
            EmptySpaceItem3.MinSize = New System.Drawing.Size(10, 24)
            EmptySpaceItem3.Name = "EmptySpaceItem3"
            EmptySpaceItem3.Size = New System.Drawing.Size(1335, 24)
            EmptySpaceItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
            ' 
            ' LayoutControlItem1
            ' 
            LayoutControlItem1.Control = lblRecentDocsHeader
            LayoutControlItem1.Location = New System.Drawing.Point(0, 189)
            LayoutControlItem1.Name = "LayoutControlItem1"
            LayoutControlItem1.Size = New System.Drawing.Size(136, 24)
            LayoutControlItem1.TextVisible = False
            ' 
            ' LayoutControlItem2
            ' 
            LayoutControlItem2.Control = gridRecentDocs
            LayoutControlItem2.Location = New System.Drawing.Point(0, 213)
            LayoutControlItem2.Name = "LayoutControlItem2"
            LayoutControlItem2.Size = New System.Drawing.Size(1471, 367)
            LayoutControlItem2.TextVisible = False
            ' 
            ' LayoutControlItem3
            ' 
            LayoutControlItem3.Control = lblPageTitle
            LayoutControlItem3.Location = New System.Drawing.Point(0, 0)
            LayoutControlItem3.Name = "LayoutControlItem3"
            LayoutControlItem3.Size = New System.Drawing.Size(208, 34)
            LayoutControlItem3.TextVisible = False
            ' 
            ' LayoutControlItem4
            ' 
            LayoutControlItem4.Control = lblPageSubtitle
            LayoutControlItem4.Location = New System.Drawing.Point(0, 34)
            LayoutControlItem4.Name = "LayoutControlItem4"
            LayoutControlItem4.Size = New System.Drawing.Size(1471, 21)
            LayoutControlItem4.TextVisible = False
            ' 
            ' EmptySpaceItem5
            ' 
            EmptySpaceItem5.Location = New System.Drawing.Point(208, 0)
            EmptySpaceItem5.Name = "EmptySpaceItem5"
            EmptySpaceItem5.Size = New System.Drawing.Size(1263, 34)
            ' 
            ' LayoutControlItem5
            ' 
            LayoutControlItem5.Control = btnRefresh
            LayoutControlItem5.Location = New System.Drawing.Point(1338, 55)
            LayoutControlItem5.MaxSize = New System.Drawing.Size(123, 65)
            LayoutControlItem5.MinSize = New System.Drawing.Size(123, 65)
            LayoutControlItem5.Name = "LayoutControlItem5"
            LayoutControlItem5.Size = New System.Drawing.Size(123, 134)
            LayoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
            LayoutControlItem5.TextVisible = False
            ' 
            ' LayoutControlItem6
            ' 
            LayoutControlItem6.Control = dtDateFrom
            LayoutControlItem6.Location = New System.Drawing.Point(1128, 55)
            LayoutControlItem6.MaxSize = New System.Drawing.Size(210, 24)
            LayoutControlItem6.MinSize = New System.Drawing.Size(210, 24)
            LayoutControlItem6.Name = "LayoutControlItem6"
            LayoutControlItem6.Size = New System.Drawing.Size(210, 24)
            LayoutControlItem6.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
            LayoutControlItem6.Text = "From"
            LayoutControlItem6.TextSize = New System.Drawing.Size(42, 13)
            ' 
            ' LayoutControlItem7
            ' 
            LayoutControlItem7.Control = dtDateTo
            LayoutControlItem7.Location = New System.Drawing.Point(1128, 79)
            LayoutControlItem7.Name = "LayoutControlItem7"
            LayoutControlItem7.Size = New System.Drawing.Size(210, 24)
            LayoutControlItem7.Text = "To"
            LayoutControlItem7.TextSize = New System.Drawing.Size(42, 13)
            ' 
            ' LayoutControlItem8
            ' 
            LayoutControlItem8.Control = cmbDirection
            LayoutControlItem8.Location = New System.Drawing.Point(1128, 103)
            LayoutControlItem8.Name = "LayoutControlItem8"
            LayoutControlItem8.Size = New System.Drawing.Size(210, 24)
            LayoutControlItem8.Text = "Direction"
            LayoutControlItem8.TextSize = New System.Drawing.Size(42, 13)
            ' 
            ' LayoutControlItem9
            ' 
            LayoutControlItem9.Control = pnlKpiContainer
            LayoutControlItem9.Location = New System.Drawing.Point(0, 55)
            LayoutControlItem9.MaxSize = New System.Drawing.Size(0, 134)
            LayoutControlItem9.MinSize = New System.Drawing.Size(5, 134)
            LayoutControlItem9.Name = "LayoutControlItem9"
            LayoutControlItem9.Size = New System.Drawing.Size(1128, 134)
            LayoutControlItem9.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
            LayoutControlItem9.TextVisible = False
            ' 
            ' EmptySpaceItem2
            ' 
            EmptySpaceItem2.Location = New System.Drawing.Point(1128, 127)
            EmptySpaceItem2.Name = "EmptySpaceItem2"
            EmptySpaceItem2.Size = New System.Drawing.Size(210, 62)
            ' 
            ' EmptySpaceItem6
            ' 
            EmptySpaceItem6.Location = New System.Drawing.Point(1461, 55)
            EmptySpaceItem6.Name = "EmptySpaceItem6"
            EmptySpaceItem6.Size = New System.Drawing.Size(10, 134)
            ' 
            ' DashboardForm
            ' 
            AutoScaleDimensions = New System.Drawing.SizeF(96F, 96F)
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
            ClientSize = New System.Drawing.Size(1491, 600)
            Controls.Add(LayoutControl1)
            Name = "DashboardForm"
            Text = "Dashboard"
            CType(LayoutControl1, ComponentModel.ISupportInitialize).EndInit()
            LayoutControl1.ResumeLayout(False)
            CType(cmbDirection.Properties, ComponentModel.ISupportInitialize).EndInit()
            CType(pnlKpiContainer, ComponentModel.ISupportInitialize).EndInit()
            pnlKpiContainer.ResumeLayout(False)
            CType(cardTotalDocs, ComponentModel.ISupportInitialize).EndInit()
            CType(cardValid, ComponentModel.ISupportInitialize).EndInit()
            CType(cardSubmitted, ComponentModel.ISupportInitialize).EndInit()
            CType(cardRejected, ComponentModel.ISupportInitialize).EndInit()
            CType(cardCancelled, ComponentModel.ISupportInitialize).EndInit()
            CType(cardTotalAmount, ComponentModel.ISupportInitialize).EndInit()
            CType(dtDateFrom.Properties.CalendarTimeProperties, ComponentModel.ISupportInitialize).EndInit()
            CType(dtDateFrom.Properties, ComponentModel.ISupportInitialize).EndInit()
            CType(dtDateTo.Properties.CalendarTimeProperties, ComponentModel.ISupportInitialize).EndInit()
            CType(dtDateTo.Properties, ComponentModel.ISupportInitialize).EndInit()
            CType(gridRecentDocs, ComponentModel.ISupportInitialize).EndInit()
            CType(gridViewRecentDocs, ComponentModel.ISupportInitialize).EndInit()
            CType(Root, ComponentModel.ISupportInitialize).EndInit()
            CType(EmptySpaceItem3, ComponentModel.ISupportInitialize).EndInit()
            CType(LayoutControlItem1, ComponentModel.ISupportInitialize).EndInit()
            CType(LayoutControlItem2, ComponentModel.ISupportInitialize).EndInit()
            CType(LayoutControlItem3, ComponentModel.ISupportInitialize).EndInit()
            CType(LayoutControlItem4, ComponentModel.ISupportInitialize).EndInit()
            CType(EmptySpaceItem5, ComponentModel.ISupportInitialize).EndInit()
            CType(LayoutControlItem5, ComponentModel.ISupportInitialize).EndInit()
            CType(LayoutControlItem6, ComponentModel.ISupportInitialize).EndInit()
            CType(LayoutControlItem7, ComponentModel.ISupportInitialize).EndInit()
            CType(LayoutControlItem8, ComponentModel.ISupportInitialize).EndInit()
            CType(LayoutControlItem9, ComponentModel.ISupportInitialize).EndInit()
            CType(EmptySpaceItem2, ComponentModel.ISupportInitialize).EndInit()
            CType(EmptySpaceItem6, ComponentModel.ISupportInitialize).EndInit()
            ResumeLayout(False)
        End Sub

        Friend WithEvents lblPageTitle As DevExpress.XtraEditors.LabelControl
        Friend WithEvents lblPageSubtitle As DevExpress.XtraEditors.LabelControl
        Friend WithEvents dtDateFrom As DevExpress.XtraEditors.DateEdit
        Friend WithEvents dtDateTo As DevExpress.XtraEditors.DateEdit
        Friend WithEvents cmbDirection As DevExpress.XtraEditors.ComboBoxEdit
        Friend WithEvents btnRefresh As DevExpress.XtraEditors.SimpleButton
        Friend WithEvents pnlKpiContainer As DevExpress.XtraEditors.PanelControl
        Friend WithEvents cardTotalDocs As DevExpress.XtraEditors.PanelControl
        Friend WithEvents cardValid As DevExpress.XtraEditors.PanelControl
        Friend WithEvents cardSubmitted As DevExpress.XtraEditors.PanelControl
        Friend WithEvents cardRejected As DevExpress.XtraEditors.PanelControl
        Friend WithEvents cardCancelled As DevExpress.XtraEditors.PanelControl
        Friend WithEvents cardTotalAmount As DevExpress.XtraEditors.PanelControl
        Friend WithEvents lblRecentDocsHeader As DevExpress.XtraEditors.LabelControl
        Friend WithEvents gridRecentDocs As DevExpress.XtraGrid.GridControl
        Friend WithEvents gridViewRecentDocs As DevExpress.XtraGrid.Views.Grid.GridView
        Friend WithEvents LayoutControl1 As DevExpress.XtraLayout.LayoutControl
        Friend WithEvents Root As DevExpress.XtraLayout.LayoutControlGroup
        Friend WithEvents EmptySpaceItem3 As DevExpress.XtraLayout.EmptySpaceItem
        Friend WithEvents LayoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem
        Friend WithEvents LayoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem
        Friend WithEvents LayoutControlItem3 As DevExpress.XtraLayout.LayoutControlItem
        Friend WithEvents LayoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem
        Friend WithEvents EmptySpaceItem5 As DevExpress.XtraLayout.EmptySpaceItem
        Friend WithEvents LayoutControlItem5 As DevExpress.XtraLayout.LayoutControlItem
        Friend WithEvents LayoutControlItem6 As DevExpress.XtraLayout.LayoutControlItem
        Friend WithEvents LayoutControlItem7 As DevExpress.XtraLayout.LayoutControlItem
        Friend WithEvents LayoutControlItem8 As DevExpress.XtraLayout.LayoutControlItem
        Friend WithEvents LayoutControlItem9 As DevExpress.XtraLayout.LayoutControlItem
        Friend WithEvents EmptySpaceItem2 As DevExpress.XtraLayout.EmptySpaceItem
        Friend WithEvents EmptySpaceItem6 As DevExpress.XtraLayout.EmptySpaceItem
    End Class
End Namespace
