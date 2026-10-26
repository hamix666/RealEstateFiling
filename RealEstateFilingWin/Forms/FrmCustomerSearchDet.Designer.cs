namespace RealEstateFilingWin.Forms
{
    partial class FrmCustomerSearchDet
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCustomerSearchDet));
            Janus.Windows.GridEX.GridEXLayout grdCustomerSearch_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            Janus.Windows.GridEX.GridEXLayout grdCustomerDelete_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            this.guna2DragControl1 = new Guna.UI2.WinForms.Guna2DragControl(this.components);
            this.guna2DragControl2 = new Guna.UI2.WinForms.Guna2DragControl(this.components);
            this.guna2Elipse2 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.tabItemMalek = new DevComponents.DotNetBar.TabItem(this.components);
            this.tabControl1 = new DevComponents.DotNetBar.TabControl();
            this.tabControlPanel1 = new DevComponents.DotNetBar.TabControlPanel();
            this.grdCustomerSearch = new BasicComponent.ClsGrid(this.components);
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.btnCancel = new DevComponents.DotNetBar.ButtonX();
            this.btnEdit = new DevComponents.DotNetBar.ButtonX();
            this.btnSave = new DevComponents.DotNetBar.ButtonX();
            this.txtID = new Guna.UI2.WinForms.Guna2TextBox();
            this.tabItem1 = new DevComponents.DotNetBar.TabItem(this.components);
            this.tabControlPanel2 = new DevComponents.DotNetBar.TabControlPanel();
            this.grdCustomerDelete = new BasicComponent.ClsGrid(this.components);
            this.panelRecordGrid = new Guna.UI2.WinForms.Guna2Panel();
            this.panelRecordGridM = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.lblSumRecord = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panelRecordGridR = new System.Windows.Forms.Panel();
            this.btnCustomer = new DevComponents.DotNetBar.ButtonX();
            this.btnView = new DevComponents.DotNetBar.ButtonX();
            this.panelRecordGridL = new System.Windows.Forms.Panel();
            this.lblTo = new System.Windows.Forms.Label();
            this.btnP = new DevComponents.DotNetBar.ButtonX();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblfrom = new System.Windows.Forms.Label();
            this.btnN = new DevComponents.DotNetBar.ButtonX();
            this.panelSearch = new Guna.UI2.WinForms.Guna2Panel();
            this.btnreturn = new DevComponents.DotNetBar.ButtonX();
            this.tabItem2 = new DevComponents.DotNetBar.TabItem(this.components);
            this.guna2Elipse1 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.tabControl1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabControlPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdCustomerSearch)).BeginInit();
            this.guna2Panel1.SuspendLayout();
            this.tabControlPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdCustomerDelete)).BeginInit();
            this.panelRecordGrid.SuspendLayout();
            this.panelRecordGridM.SuspendLayout();
            this.panelRecordGridR.SuspendLayout();
            this.panelRecordGridL.SuspendLayout();
            this.panelSearch.SuspendLayout();
            this.SuspendLayout();
            // 
            // guna2DragControl1
            // 
            this.guna2DragControl1.DockIndicatorTransparencyValue = 0.6D;
            this.guna2DragControl1.UseTransparentDrag = true;
            // 
            // guna2DragControl2
            // 
            this.guna2DragControl2.DockIndicatorTransparencyValue = 0.6D;
            this.guna2DragControl2.UseTransparentDrag = true;
            // 
            // tabItemMalek
            // 
            this.tabItemMalek.Name = "tabItemMalek";
            this.tabItemMalek.Text = "مالک";
            // 
            // tabControl1
            // 
            this.tabControl1.CanReorderTabs = true;
            this.tabControl1.Controls.Add(this.tabControlPanel1);
            this.tabControl1.Controls.Add(this.tabControlPanel2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedTabFont = new System.Drawing.Font("Vazirmatn", 9.75F, System.Drawing.FontStyle.Bold);
            this.tabControl1.SelectedTabIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1018, 450);
            this.tabControl1.Style = DevComponents.DotNetBar.eTabStripStyle.SimulatedTheme;
            this.tabControl1.TabIndex = 0;
            this.tabControl1.TabLayoutType = DevComponents.DotNetBar.eTabLayoutType.FixedWithNavigationBox;
            this.tabControl1.Tabs.Add(this.tabItem1);
            this.tabControl1.Tabs.Add(this.tabItem2);
            this.tabControl1.Text = "tabControl1";
            // 
            // tabControlPanel1
            // 
            this.tabControlPanel1.Controls.Add(this.grdCustomerSearch);
            this.tabControlPanel1.Controls.Add(this.guna2Panel1);
            this.tabControlPanel1.DisabledBackColor = System.Drawing.Color.Empty;
            this.tabControlPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlPanel1.Location = new System.Drawing.Point(0, 34);
            this.tabControlPanel1.Name = "tabControlPanel1";
            this.tabControlPanel1.Padding = new System.Windows.Forms.Padding(1);
            this.tabControlPanel1.Size = new System.Drawing.Size(1018, 416);
            this.tabControlPanel1.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(242)))), ((int)(((byte)(232)))));
            this.tabControlPanel1.Style.BackColor2.Color = System.Drawing.Color.White;
            this.tabControlPanel1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.tabControlPanel1.Style.BorderColor.Color = System.Drawing.Color.FromArgb(((int)(((byte)(172)))), ((int)(((byte)(168)))), ((int)(((byte)(153)))));
            this.tabControlPanel1.Style.BorderSide = ((DevComponents.DotNetBar.eBorderSide)(((DevComponents.DotNetBar.eBorderSide.Left | DevComponents.DotNetBar.eBorderSide.Right) 
            | DevComponents.DotNetBar.eBorderSide.Bottom)));
            this.tabControlPanel1.Style.GradientAngle = 90;
            this.tabControlPanel1.TabIndex = 1;
            this.tabControlPanel1.TabItem = this.tabItem1;
            // 
            // grdCustomerSearch
            // 
            this.grdCustomerSearch.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False;
            this.grdCustomerSearch.AlternatingColors = true;
            this.grdCustomerSearch.BLObjectName = null;
            this.grdCustomerSearch.BuiltInTextsData = resources.GetString("grdCustomerSearch.BuiltInTextsData");
            this.grdCustomerSearch.ColumnAutoResize = true;
            this.grdCustomerSearch.ColumnAutoSizeMode = Janus.Windows.GridEX.ColumnAutoSizeMode.DiaplayedCells;
            this.grdCustomerSearch.ContextMenuS = 0;
            this.grdCustomerSearch.CopyAndPaste = false;
            this.grdCustomerSearch.DefaultFilterRowComparison = Janus.Windows.GridEX.FilterConditionOperator.Contains;
            grdCustomerSearch_DesignTimeLayout.LayoutString = resources.GetString("grdCustomerSearch_DesignTimeLayout.LayoutString");
            this.grdCustomerSearch.DesignTimeLayout = grdCustomerSearch_DesignTimeLayout;
            this.grdCustomerSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdCustomerSearch.FieldName = null;
            this.grdCustomerSearch.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
            this.grdCustomerSearch.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown;
            this.grdCustomerSearch.FocusCellFormatStyle.BackColor = System.Drawing.Color.LemonChiffon;
            this.grdCustomerSearch.Font = new System.Drawing.Font("Vazirmatn", 12F);
            this.grdCustomerSearch.GetMethodName = null;
            this.grdCustomerSearch.GridLineStyle = Janus.Windows.GridEX.GridLineStyle.Solid;
            this.grdCustomerSearch.GroupByBoxVisible = false;
            this.grdCustomerSearch.HeaderFormatStyle.Font = new System.Drawing.Font("Vazirmatn", 9.75F, System.Drawing.FontStyle.Bold);
            this.grdCustomerSearch.IsRequierd = false;
            this.grdCustomerSearch.Location = new System.Drawing.Point(1, 70);
            this.grdCustomerSearch.LockStatus = BasicComponent.ClsInterface.EnumLockStatus.ReadOnly;
            this.grdCustomerSearch.Name = "grdCustomerSearch";
            this.grdCustomerSearch.PrecisionNumber = 0;
            this.grdCustomerSearch.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.grdCustomerSearch.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.Default;
            this.grdCustomerSearch.SelectedFormatStyle.BackColor = System.Drawing.Color.LemonChiffon;
            this.grdCustomerSearch.Size = new System.Drawing.Size(1016, 345);
            this.grdCustomerSearch.TabIndex = 24;
            this.grdCustomerSearch.UpdateMode = Janus.Windows.GridEX.UpdateMode.CellUpdate;
            this.grdCustomerSearch.ValidateType = BasicComponent.ClsInterface.ValidateType.IsNullOrEmpty;
            this.grdCustomerSearch.ViewStatus = BasicComponent.ClsInterface.EnumViewStatus.ViewMode;
            this.grdCustomerSearch.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2010;
            this.grdCustomerSearch.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.grdCustomerSearch_MouseDoubleClick);
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.BackColor = System.Drawing.Color.Gainsboro;
            this.guna2Panel1.Controls.Add(this.btnCancel);
            this.guna2Panel1.Controls.Add(this.btnEdit);
            this.guna2Panel1.Controls.Add(this.btnSave);
            this.guna2Panel1.Controls.Add(this.txtID);
            this.guna2Panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.guna2Panel1.Font = new System.Drawing.Font("Vazirmatn", 12F, System.Drawing.FontStyle.Bold);
            this.guna2Panel1.Location = new System.Drawing.Point(1, 1);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Padding = new System.Windows.Forms.Padding(5);
            this.guna2Panel1.Size = new System.Drawing.Size(1016, 69);
            this.guna2Panel1.TabIndex = 23;
            // 
            // btnCancel
            // 
            this.btnCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnCancel.Font = new System.Drawing.Font("Vazirmatn", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btnCancel.ImageTextSpacing = 5;
            this.btnCancel.Location = new System.Drawing.Point(705, 5);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor(2, 6, 6, 2);
            this.btnCancel.Size = new System.Drawing.Size(105, 59);
            this.btnCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.OfficeXP;
            this.btnCancel.Symbol = "";
            this.btnCancel.SymbolColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.btnCancel.SymbolSize = 15F;
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "حذف مشتری";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnEdit.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnEdit.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnEdit.Font = new System.Drawing.Font("Vazirmatn", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEdit.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btnEdit.ImageTextSpacing = 5;
            this.btnEdit.Location = new System.Drawing.Point(810, 5);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor(2, 6, 6, 2);
            this.btnEdit.Size = new System.Drawing.Size(105, 59);
            this.btnEdit.Style = DevComponents.DotNetBar.eDotNetBarStyle.OfficeXP;
            this.btnEdit.Symbol = "";
            this.btnEdit.SymbolColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.btnEdit.SymbolSize = 15F;
            this.btnEdit.TabIndex = 5;
            this.btnEdit.Text = "ویرایش  مشتری";
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnSave
            // 
            this.btnSave.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSave.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSave.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSave.Font = new System.Drawing.Font("Vazirmatn", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btnSave.ImageTextSpacing = 5;
            this.btnSave.Location = new System.Drawing.Point(915, 5);
            this.btnSave.Name = "btnSave";
            this.btnSave.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor(2, 6, 6, 2);
            this.btnSave.Size = new System.Drawing.Size(96, 59);
            this.btnSave.Style = DevComponents.DotNetBar.eDotNetBarStyle.OfficeXP;
            this.btnSave.Symbol = "";
            this.btnSave.SymbolColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.btnSave.SymbolSize = 15F;
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "مشاهده نتایج";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtID
            // 
            this.txtID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtID.BorderRadius = 15;
            this.txtID.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtID.DefaultText = "";
            this.txtID.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtID.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtID.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtID.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtID.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtID.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtID.ForeColor = System.Drawing.Color.Black;
            this.txtID.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtID.Location = new System.Drawing.Point(64, 8);
            this.txtID.Name = "txtID";
            this.txtID.PasswordChar = '\0';
            this.txtID.PlaceholderText = "";
            this.txtID.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtID.SelectedText = "";
            this.txtID.Size = new System.Drawing.Size(64, 36);
            this.txtID.TabIndex = 4;
            this.txtID.Visible = false;
            // 
            // tabItem1
            // 
            this.tabItem1.AttachedControl = this.tabControlPanel1;
            this.tabItem1.Name = "tabItem1";
            this.tabItem1.Text = "لیست ذخیره جستجو";
            // 
            // tabControlPanel2
            // 
            this.tabControlPanel2.Controls.Add(this.grdCustomerDelete);
            this.tabControlPanel2.Controls.Add(this.panelRecordGrid);
            this.tabControlPanel2.Controls.Add(this.panelSearch);
            this.tabControlPanel2.DisabledBackColor = System.Drawing.Color.Empty;
            this.tabControlPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlPanel2.Location = new System.Drawing.Point(0, 34);
            this.tabControlPanel2.Name = "tabControlPanel2";
            this.tabControlPanel2.Padding = new System.Windows.Forms.Padding(1);
            this.tabControlPanel2.Size = new System.Drawing.Size(1018, 416);
            this.tabControlPanel2.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(242)))), ((int)(((byte)(232)))));
            this.tabControlPanel2.Style.BackColor2.Color = System.Drawing.Color.White;
            this.tabControlPanel2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.tabControlPanel2.Style.BorderColor.Color = System.Drawing.Color.FromArgb(((int)(((byte)(172)))), ((int)(((byte)(168)))), ((int)(((byte)(153)))));
            this.tabControlPanel2.Style.BorderSide = ((DevComponents.DotNetBar.eBorderSide)(((DevComponents.DotNetBar.eBorderSide.Left | DevComponents.DotNetBar.eBorderSide.Right) 
            | DevComponents.DotNetBar.eBorderSide.Bottom)));
            this.tabControlPanel2.Style.GradientAngle = 90;
            this.tabControlPanel2.TabIndex = 5;
            this.tabControlPanel2.TabItem = this.tabItem2;
            // 
            // grdCustomerDelete
            // 
            this.grdCustomerDelete.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False;
            this.grdCustomerDelete.AlternatingColors = true;
            this.grdCustomerDelete.BLObjectName = null;
            this.grdCustomerDelete.BuiltInTextsData = resources.GetString("grdCustomerDelete.BuiltInTextsData");
            this.grdCustomerDelete.ColumnAutoResize = true;
            this.grdCustomerDelete.ColumnAutoSizeMode = Janus.Windows.GridEX.ColumnAutoSizeMode.DiaplayedCells;
            this.grdCustomerDelete.ContextMenuS = 0;
            this.grdCustomerDelete.CopyAndPaste = false;
            this.grdCustomerDelete.DefaultFilterRowComparison = Janus.Windows.GridEX.FilterConditionOperator.Contains;
            grdCustomerDelete_DesignTimeLayout.LayoutString = resources.GetString("grdCustomerDelete_DesignTimeLayout.LayoutString");
            this.grdCustomerDelete.DesignTimeLayout = grdCustomerDelete_DesignTimeLayout;
            this.grdCustomerDelete.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdCustomerDelete.FieldName = null;
            this.grdCustomerDelete.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
            this.grdCustomerDelete.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown;
            this.grdCustomerDelete.FocusCellFormatStyle.BackColor = System.Drawing.Color.LemonChiffon;
            this.grdCustomerDelete.Font = new System.Drawing.Font("Vazirmatn", 12F);
            this.grdCustomerDelete.GetMethodName = null;
            this.grdCustomerDelete.GridLineStyle = Janus.Windows.GridEX.GridLineStyle.Solid;
            this.grdCustomerDelete.GroupByBoxVisible = false;
            this.grdCustomerDelete.HeaderFormatStyle.Font = new System.Drawing.Font("Vazirmatn", 9.75F, System.Drawing.FontStyle.Bold);
            this.grdCustomerDelete.IsRequierd = false;
            this.grdCustomerDelete.Location = new System.Drawing.Point(1, 71);
            this.grdCustomerDelete.LockStatus = BasicComponent.ClsInterface.EnumLockStatus.ReadOnly;
            this.grdCustomerDelete.Name = "grdCustomerDelete";
            this.grdCustomerDelete.PrecisionNumber = 0;
            this.grdCustomerDelete.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.grdCustomerDelete.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.Default;
            this.grdCustomerDelete.SelectedFormatStyle.BackColor = System.Drawing.Color.LemonChiffon;
            this.grdCustomerDelete.Size = new System.Drawing.Size(1016, 295);
            this.grdCustomerDelete.TabIndex = 25;
            this.grdCustomerDelete.UpdateMode = Janus.Windows.GridEX.UpdateMode.CellUpdate;
            this.grdCustomerDelete.ValidateType = BasicComponent.ClsInterface.ValidateType.IsNullOrEmpty;
            this.grdCustomerDelete.ViewStatus = BasicComponent.ClsInterface.EnumViewStatus.ViewMode;
            this.grdCustomerDelete.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2010;
            // 
            // panelRecordGrid
            // 
            this.panelRecordGrid.BackColor = System.Drawing.Color.LightGray;
            this.panelRecordGrid.Controls.Add(this.panelRecordGridM);
            this.panelRecordGrid.Controls.Add(this.panelRecordGridR);
            this.panelRecordGrid.Controls.Add(this.panelRecordGridL);
            this.panelRecordGrid.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelRecordGrid.Location = new System.Drawing.Point(1, 366);
            this.panelRecordGrid.Margin = new System.Windows.Forms.Padding(4);
            this.panelRecordGrid.Name = "panelRecordGrid";
            this.panelRecordGrid.Size = new System.Drawing.Size(1016, 49);
            this.panelRecordGrid.TabIndex = 5;
            this.panelRecordGrid.Visible = false;
            // 
            // panelRecordGridM
            // 
            this.panelRecordGridM.Controls.Add(this.label6);
            this.panelRecordGridM.Controls.Add(this.lblSumRecord);
            this.panelRecordGridM.Controls.Add(this.label3);
            this.panelRecordGridM.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRecordGridM.Location = new System.Drawing.Point(257, 0);
            this.panelRecordGridM.Margin = new System.Windows.Forms.Padding(4);
            this.panelRecordGridM.Name = "panelRecordGridM";
            this.panelRecordGridM.Size = new System.Drawing.Size(462, 49);
            this.panelRecordGridM.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(2, 11);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(85, 22);
            this.label6.TabIndex = 5;
            this.label6.Text = "رکورد یافت شد";
            // 
            // lblSumRecord
            // 
            this.lblSumRecord.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSumRecord.AutoSize = true;
            this.lblSumRecord.ForeColor = System.Drawing.Color.Black;
            this.lblSumRecord.Location = new System.Drawing.Point(130, 11);
            this.lblSumRecord.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSumRecord.Name = "lblSumRecord";
            this.lblSumRecord.Size = new System.Drawing.Size(16, 22);
            this.lblSumRecord.TabIndex = 4;
            this.lblSumRecord.Text = "0";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(166, 11);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 22);
            this.label3.TabIndex = 3;
            this.label3.Text = "تعداد";
            // 
            // panelRecordGridR
            // 
            this.panelRecordGridR.Controls.Add(this.btnCustomer);
            this.panelRecordGridR.Controls.Add(this.btnView);
            this.panelRecordGridR.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelRecordGridR.Location = new System.Drawing.Point(719, 0);
            this.panelRecordGridR.Margin = new System.Windows.Forms.Padding(4);
            this.panelRecordGridR.Name = "panelRecordGridR";
            this.panelRecordGridR.Size = new System.Drawing.Size(297, 49);
            this.panelRecordGridR.TabIndex = 7;
            // 
            // btnCustomer
            // 
            this.btnCustomer.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCustomer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCustomer.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnCustomer.Location = new System.Drawing.Point(6, 6);
            this.btnCustomer.Margin = new System.Windows.Forms.Padding(4);
            this.btnCustomer.Name = "btnCustomer";
            this.btnCustomer.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor(2, 6, 6, 2);
            this.btnCustomer.Size = new System.Drawing.Size(181, 37);
            this.btnCustomer.Style = DevComponents.DotNetBar.eDotNetBarStyle.OfficeXP;
            this.btnCustomer.Symbol = "";
            this.btnCustomer.SymbolColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.btnCustomer.SymbolSize = 12F;
            this.btnCustomer.TabIndex = 1;
            this.btnCustomer.Text = "مشاهده مشتری";
            // 
            // btnView
            // 
            this.btnView.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnView.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnView.Location = new System.Drawing.Point(195, 6);
            this.btnView.Margin = new System.Windows.Forms.Padding(4);
            this.btnView.Name = "btnView";
            this.btnView.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor(2, 6, 6, 2);
            this.btnView.Size = new System.Drawing.Size(96, 37);
            this.btnView.Style = DevComponents.DotNetBar.eDotNetBarStyle.OfficeXP;
            this.btnView.Symbol = "";
            this.btnView.SymbolColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.btnView.SymbolSize = 12F;
            this.btnView.TabIndex = 0;
            this.btnView.Text = "نمایش";
            // 
            // panelRecordGridL
            // 
            this.panelRecordGridL.Controls.Add(this.lblTo);
            this.panelRecordGridL.Controls.Add(this.btnP);
            this.panelRecordGridL.Controls.Add(this.label4);
            this.panelRecordGridL.Controls.Add(this.label2);
            this.panelRecordGridL.Controls.Add(this.lblfrom);
            this.panelRecordGridL.Controls.Add(this.btnN);
            this.panelRecordGridL.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelRecordGridL.Location = new System.Drawing.Point(0, 0);
            this.panelRecordGridL.Margin = new System.Windows.Forms.Padding(4);
            this.panelRecordGridL.Name = "panelRecordGridL";
            this.panelRecordGridL.Size = new System.Drawing.Size(257, 49);
            this.panelRecordGridL.TabIndex = 6;
            // 
            // lblTo
            // 
            this.lblTo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTo.AutoSize = true;
            this.lblTo.ForeColor = System.Drawing.Color.Black;
            this.lblTo.Location = new System.Drawing.Point(10, 11);
            this.lblTo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(14, 22);
            this.lblTo.TabIndex = 2;
            this.lblTo.Text = "1";
            // 
            // btnP
            // 
            this.btnP.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnP.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnP.Location = new System.Drawing.Point(197, 11);
            this.btnP.Margin = new System.Windows.Forms.Padding(4);
            this.btnP.Name = "btnP";
            this.btnP.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor(2, 6, 6, 2);
            this.btnP.Size = new System.Drawing.Size(35, 28);
            this.btnP.Style = DevComponents.DotNetBar.eDotNetBarStyle.OfficeXP;
            this.btnP.Symbol = "58133";
            this.btnP.SymbolColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.btnP.SymbolSet = DevComponents.DotNetBar.eSymbolSet.Material;
            this.btnP.SymbolSize = 20F;
            this.btnP.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(36, 11);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(18, 22);
            this.label4.TabIndex = 2;
            this.label4.Text = "از";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(93, 11);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 22);
            this.label2.TabIndex = 2;
            this.label2.Text = "صفحه";
            // 
            // lblfrom
            // 
            this.lblfrom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblfrom.AutoSize = true;
            this.lblfrom.ForeColor = System.Drawing.Color.Black;
            this.lblfrom.Location = new System.Drawing.Point(66, 11);
            this.lblfrom.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblfrom.Name = "lblfrom";
            this.lblfrom.Size = new System.Drawing.Size(16, 22);
            this.lblfrom.TabIndex = 2;
            this.lblfrom.Text = "0";
            // 
            // btnN
            // 
            this.btnN.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnN.BackColor = System.Drawing.Color.Transparent;
            this.btnN.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnN.Location = new System.Drawing.Point(156, 11);
            this.btnN.Margin = new System.Windows.Forms.Padding(4);
            this.btnN.Name = "btnN";
            this.btnN.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor(2, 6, 6, 2);
            this.btnN.Size = new System.Drawing.Size(33, 28);
            this.btnN.Style = DevComponents.DotNetBar.eDotNetBarStyle.OfficeXP;
            this.btnN.Symbol = "58132";
            this.btnN.SymbolColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.btnN.SymbolSet = DevComponents.DotNetBar.eSymbolSet.Material;
            this.btnN.SymbolSize = 20F;
            this.btnN.TabIndex = 3;
            // 
            // panelSearch
            // 
            this.panelSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panelSearch.Controls.Add(this.btnreturn);
            this.panelSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSearch.Font = new System.Drawing.Font("Vazirmatn", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panelSearch.Location = new System.Drawing.Point(1, 1);
            this.panelSearch.Margin = new System.Windows.Forms.Padding(4);
            this.panelSearch.Name = "panelSearch";
            this.panelSearch.Size = new System.Drawing.Size(1016, 70);
            this.panelSearch.TabIndex = 4;
            // 
            // btnreturn
            // 
            this.btnreturn.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnreturn.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnreturn.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnreturn.Font = new System.Drawing.Font("Vazirmatn", 12F);
            this.btnreturn.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btnreturn.ImageTextSpacing = 5;
            this.btnreturn.Location = new System.Drawing.Point(926, 0);
            this.btnreturn.Name = "btnreturn";
            this.btnreturn.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor(2, 6, 6, 2);
            this.btnreturn.Size = new System.Drawing.Size(90, 70);
            this.btnreturn.Style = DevComponents.DotNetBar.eDotNetBarStyle.OfficeXP;
            this.btnreturn.Symbol = "";
            this.btnreturn.SymbolColor = System.Drawing.Color.Olive;
            this.btnreturn.SymbolSize = 15F;
            this.btnreturn.TabIndex = 12;
            this.btnreturn.Text = "احیای مشتری";
            this.btnreturn.Click += new System.EventHandler(this.btnreturn_Click);
            // 
            // tabItem2
            // 
            this.tabItem2.AttachedControl = this.tabControlPanel2;
            this.tabItem2.Name = "tabItem2";
            this.tabItem2.Text = "مشتری های بایگانی شده";
            // 
            // guna2Elipse1
            // 
            this.guna2Elipse1.TargetControl = this;
            // 
            // FrmCustomerSearchDet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1018, 450);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmCustomerSearchDet";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.Text = "FrmCustomerSearchDet";
            this.Load += new System.EventHandler(this.FrmCustomerSearchDet_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tabControl1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabControlPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdCustomerSearch)).EndInit();
            this.guna2Panel1.ResumeLayout(false);
            this.tabControlPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdCustomerDelete)).EndInit();
            this.panelRecordGrid.ResumeLayout(false);
            this.panelRecordGridM.ResumeLayout(false);
            this.panelRecordGridM.PerformLayout();
            this.panelRecordGridR.ResumeLayout(false);
            this.panelRecordGridL.ResumeLayout(false);
            this.panelRecordGridL.PerformLayout();
            this.panelSearch.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse1;
        private Guna.UI2.WinForms.Guna2DragControl guna2DragControl1;
        private DevComponents.DotNetBar.TabControl tabControl1;
        private DevComponents.DotNetBar.TabControlPanel tabControlPanel1;
        private DevComponents.DotNetBar.TabItem tabItem1;
        private DevComponents.DotNetBar.TabControlPanel tabControlPanel2;
        private DevComponents.DotNetBar.TabItem tabItem2;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private DevComponents.DotNetBar.ButtonX btnCancel;
        private DevComponents.DotNetBar.ButtonX btnSave;
        private Guna.UI2.WinForms.Guna2TextBox txtID;
        private Guna.UI2.WinForms.Guna2DragControl guna2DragControl2;
        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse2;
        private BasicComponent.ClsGrid grdCustomerSearch;
        private Guna.UI2.WinForms.Guna2Panel panelRecordGrid;
        private System.Windows.Forms.Panel panelRecordGridM;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblSumRecord;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panelRecordGridR;
        private DevComponents.DotNetBar.ButtonX btnCustomer;
        private DevComponents.DotNetBar.ButtonX btnView;
        private System.Windows.Forms.Panel panelRecordGridL;
        private System.Windows.Forms.Label lblTo;
        private DevComponents.DotNetBar.ButtonX btnP;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblfrom;
        private DevComponents.DotNetBar.ButtonX btnN;
        private DevComponents.DotNetBar.TabItem tabItemMalek;
        private DevComponents.DotNetBar.ButtonX btnEdit;
        private Guna.UI2.WinForms.Guna2Panel panelSearch;
        private DevComponents.DotNetBar.ButtonX btnreturn;
        private BasicComponent.ClsGrid grdCustomerDelete;
    }
}