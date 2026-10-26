namespace RealEstateFilingWin.Forms
{
    partial class FrmFileDet
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
            this.guna2DragControl1 = new Guna.UI2.WinForms.Guna2DragControl(this.components);
            this.panelTop = new Guna.UI2.WinForms.Guna2Panel();
            this.btnMin = new Guna.UI2.WinForms.Guna2CircleButton();
            this.btnClose = new Guna.UI2.WinForms.Guna2CircleButton();
            this.lblDet = new System.Windows.Forms.Label();
            this.txtZirbana = new Guna.UI2.WinForms.Guna2TextBox();
            this.cmbKafposhID = new Guna.UI2.WinForms.Guna2ComboBox();
            this.cmbServiceWcID = new Guna.UI2.WinForms.Guna2ComboBox();
            this.cmbKabinetID = new Guna.UI2.WinForms.Guna2ComboBox();
            this.cmbParking = new Guna.UI2.WinForms.Guna2ComboBox();
            this.cmbKhab = new Guna.UI2.WinForms.Guna2ComboBox();
            this.cmbTabaghe = new Guna.UI2.WinForms.Guna2ComboBox();
            this.btnCalc = new Guna.UI2.WinForms.Guna2Button();
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.cmbAsansor = new Guna.UI2.WinForms.Guna2ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btnCancel = new DevComponents.DotNetBar.ButtonX();
            this.txtOwnerMasterID = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtMasterID = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtOwnerID = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtID = new Guna.UI2.WinForms.Guna2TextBox();
            this.btnSave = new DevComponents.DotNetBar.ButtonX();
            this.txtPrice2 = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtPrice1 = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblPrice2 = new System.Windows.Forms.Label();
            this.lblPrice1 = new System.Windows.Forms.Label();
            this.chkAsansor = new Guna.UI2.WinForms.Guna2CheckBox();
            this.chkBalkon = new Guna.UI2.WinForms.Guna2CheckBox();
            this.chkAnbari = new Guna.UI2.WinForms.Guna2CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.guna2Elipse1 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.errorEjbari = new System.Windows.Forms.ErrorProvider(this.components);
            this.panelTop.SuspendLayout();
            this.guna2Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorEjbari)).BeginInit();
            this.SuspendLayout();
            // 
            // guna2DragControl1
            // 
            this.guna2DragControl1.DockIndicatorTransparencyValue = 0.6D;
            this.guna2DragControl1.TargetControl = this.panelTop;
            this.guna2DragControl1.UseTransparentDrag = true;
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.Color.DimGray;
            this.panelTop.Controls.Add(this.btnMin);
            this.panelTop.Controls.Add(this.btnClose);
            this.panelTop.Controls.Add(this.lblDet);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(852, 39);
            this.panelTop.TabIndex = 200;
            // 
            // btnMin
            // 
            this.btnMin.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnMin.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnMin.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnMin.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnMin.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnMin.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.btnMin.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMin.ForeColor = System.Drawing.Color.White;
            this.btnMin.Location = new System.Drawing.Point(40, 0);
            this.btnMin.Name = "btnMin";
            this.btnMin.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.btnMin.Size = new System.Drawing.Size(40, 39);
            this.btnMin.TabIndex = 201;
            this.btnMin.Text = "-";
            this.btnMin.Click += new System.EventHandler(this.btnMin_Click);
            // 
            // btnClose
            // 
            this.btnClose.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnClose.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnClose.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnClose.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnClose.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnClose.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(0, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.btnClose.Size = new System.Drawing.Size(40, 39);
            this.btnClose.TabIndex = 200;
            this.btnClose.Text = "X";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblDet
            // 
            this.lblDet.AutoSize = true;
            this.lblDet.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblDet.Font = new System.Drawing.Font("Vazirmatn", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDet.ForeColor = System.Drawing.Color.White;
            this.lblDet.Location = new System.Drawing.Point(673, 0);
            this.lblDet.Name = "lblDet";
            this.lblDet.Size = new System.Drawing.Size(179, 32);
            this.lblDet.TabIndex = 0;
            this.lblDet.Text = "آژانس مسکن میرزایی";
            // 
            // txtZirbana
            // 
            this.txtZirbana.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtZirbana.BorderRadius = 5;
            this.txtZirbana.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtZirbana.DefaultText = "";
            this.txtZirbana.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtZirbana.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtZirbana.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtZirbana.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtZirbana.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtZirbana.FocusedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.txtZirbana.Font = new System.Drawing.Font("Vazirmatn", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtZirbana.ForeColor = System.Drawing.Color.Black;
            this.txtZirbana.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtZirbana.Location = new System.Drawing.Point(445, 122);
            this.txtZirbana.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtZirbana.Name = "txtZirbana";
            this.txtZirbana.PasswordChar = '\0';
            this.txtZirbana.PlaceholderText = "";
            this.txtZirbana.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtZirbana.SelectedText = "";
            this.txtZirbana.Size = new System.Drawing.Size(147, 36);
            this.txtZirbana.TabIndex = 1;
            // 
            // cmbKafposhID
            // 
            this.cmbKafposhID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbKafposhID.BackColor = System.Drawing.Color.Transparent;
            this.cmbKafposhID.BorderRadius = 5;
            this.cmbKafposhID.DisplayMember = "Name";
            this.cmbKafposhID.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbKafposhID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbKafposhID.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbKafposhID.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbKafposhID.FocusedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.cmbKafposhID.Font = new System.Drawing.Font("Vazirmatn", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbKafposhID.ForeColor = System.Drawing.Color.Black;
            this.cmbKafposhID.ItemHeight = 30;
            this.cmbKafposhID.Location = new System.Drawing.Point(245, 188);
            this.cmbKafposhID.Name = "cmbKafposhID";
            this.cmbKafposhID.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.cmbKafposhID.Size = new System.Drawing.Size(114, 36);
            this.cmbKafposhID.TabIndex = 6;
            this.cmbKafposhID.ValueMember = "ID";
            // 
            // cmbServiceWcID
            // 
            this.cmbServiceWcID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbServiceWcID.BackColor = System.Drawing.Color.Transparent;
            this.cmbServiceWcID.BorderRadius = 5;
            this.cmbServiceWcID.DisplayMember = "Name";
            this.cmbServiceWcID.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbServiceWcID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbServiceWcID.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbServiceWcID.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbServiceWcID.FocusedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.cmbServiceWcID.Font = new System.Drawing.Font("Vazirmatn", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbServiceWcID.ForeColor = System.Drawing.Color.Black;
            this.cmbServiceWcID.ItemHeight = 30;
            this.cmbServiceWcID.Location = new System.Drawing.Point(445, 188);
            this.cmbServiceWcID.Name = "cmbServiceWcID";
            this.cmbServiceWcID.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.cmbServiceWcID.Size = new System.Drawing.Size(147, 36);
            this.cmbServiceWcID.TabIndex = 5;
            this.cmbServiceWcID.ValueMember = "ID";
            // 
            // cmbKabinetID
            // 
            this.cmbKabinetID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbKabinetID.BackColor = System.Drawing.Color.Transparent;
            this.cmbKabinetID.BorderRadius = 5;
            this.cmbKabinetID.DisplayMember = "Name";
            this.cmbKabinetID.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbKabinetID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbKabinetID.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbKabinetID.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbKabinetID.FocusedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.cmbKabinetID.Font = new System.Drawing.Font("Vazirmatn", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbKabinetID.ForeColor = System.Drawing.Color.Black;
            this.cmbKabinetID.ItemHeight = 30;
            this.cmbKabinetID.Location = new System.Drawing.Point(666, 188);
            this.cmbKabinetID.Name = "cmbKabinetID";
            this.cmbKabinetID.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.cmbKabinetID.Size = new System.Drawing.Size(114, 36);
            this.cmbKabinetID.TabIndex = 4;
            this.cmbKabinetID.ValueMember = "ID";
            // 
            // cmbParking
            // 
            this.cmbParking.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbParking.BackColor = System.Drawing.Color.Transparent;
            this.cmbParking.BorderRadius = 5;
            this.cmbParking.DisplayMember = "Name";
            this.cmbParking.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbParking.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbParking.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbParking.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbParking.FocusedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.cmbParking.Font = new System.Drawing.Font("Vazirmatn", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbParking.ForeColor = System.Drawing.Color.Black;
            this.cmbParking.ItemHeight = 30;
            this.cmbParking.Location = new System.Drawing.Point(34, 122);
            this.cmbParking.Name = "cmbParking";
            this.cmbParking.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.cmbParking.Size = new System.Drawing.Size(126, 36);
            this.cmbParking.TabIndex = 3;
            this.cmbParking.ValueMember = "ID";
            // 
            // cmbKhab
            // 
            this.cmbKhab.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbKhab.BackColor = System.Drawing.Color.Transparent;
            this.cmbKhab.BorderRadius = 5;
            this.cmbKhab.DisplayMember = "Name";
            this.cmbKhab.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbKhab.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbKhab.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbKhab.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbKhab.FocusedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.cmbKhab.Font = new System.Drawing.Font("Vazirmatn", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbKhab.ForeColor = System.Drawing.Color.Black;
            this.cmbKhab.ItemHeight = 30;
            this.cmbKhab.Location = new System.Drawing.Point(244, 122);
            this.cmbKhab.Name = "cmbKhab";
            this.cmbKhab.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.cmbKhab.Size = new System.Drawing.Size(114, 36);
            this.cmbKhab.TabIndex = 2;
            this.cmbKhab.ValueMember = "ID";
            // 
            // cmbTabaghe
            // 
            this.cmbTabaghe.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbTabaghe.BackColor = System.Drawing.Color.Transparent;
            this.cmbTabaghe.BorderRadius = 5;
            this.cmbTabaghe.DisplayMember = "Name";
            this.cmbTabaghe.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbTabaghe.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTabaghe.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbTabaghe.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbTabaghe.FocusedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.cmbTabaghe.Font = new System.Drawing.Font("Vazirmatn", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbTabaghe.ForeColor = System.Drawing.Color.Black;
            this.cmbTabaghe.ItemHeight = 30;
            this.cmbTabaghe.Location = new System.Drawing.Point(666, 122);
            this.cmbTabaghe.Name = "cmbTabaghe";
            this.cmbTabaghe.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.cmbTabaghe.Size = new System.Drawing.Size(114, 36);
            this.cmbTabaghe.TabIndex = 0;
            this.cmbTabaghe.ValueMember = "ID";
            // 
            // btnCalc
            // 
            this.btnCalc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCalc.Animated = true;
            this.btnCalc.AnimatedGIF = true;
            this.btnCalc.BorderRadius = 15;
            this.btnCalc.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnCalc.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnCalc.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnCalc.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnCalc.FillColor = System.Drawing.Color.Teal;
            this.btnCalc.Font = new System.Drawing.Font("Vazirmatn", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCalc.ForeColor = System.Drawing.Color.White;
            this.btnCalc.Location = new System.Drawing.Point(44, 299);
            this.btnCalc.Name = "btnCalc";
            this.btnCalc.Size = new System.Drawing.Size(110, 36);
            this.btnCalc.TabIndex = 12;
            this.btnCalc.Text = "محاسبه";
            this.btnCalc.Click += new System.EventHandler(this.btnCalc_Click);
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.BackColor = System.Drawing.Color.Silver;
            this.guna2Panel1.Controls.Add(this.btnCancel);
            this.guna2Panel1.Controls.Add(this.txtOwnerMasterID);
            this.guna2Panel1.Controls.Add(this.txtMasterID);
            this.guna2Panel1.Controls.Add(this.txtOwnerID);
            this.guna2Panel1.Controls.Add(this.txtID);
            this.guna2Panel1.Controls.Add(this.btnSave);
            this.guna2Panel1.Controls.Add(this.chkAsansor);
            this.guna2Panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.guna2Panel1.Location = new System.Drawing.Point(0, 39);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(852, 68);
            this.guna2Panel1.TabIndex = 14;
            // 
            // cmbAsansor
            // 
            this.cmbAsansor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbAsansor.BackColor = System.Drawing.Color.Transparent;
            this.cmbAsansor.BorderRadius = 5;
            this.cmbAsansor.DisplayMember = "Name";
            this.cmbAsansor.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbAsansor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAsansor.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbAsansor.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbAsansor.FocusedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.cmbAsansor.Font = new System.Drawing.Font("Vazirmatn", 12F);
            this.cmbAsansor.ForeColor = System.Drawing.Color.Black;
            this.cmbAsansor.ItemHeight = 30;
            this.cmbAsansor.Items.AddRange(new object[] {
            "ندارد",
            "دارد"});
            this.cmbAsansor.Location = new System.Drawing.Point(46, 190);
            this.cmbAsansor.Name = "cmbAsansor";
            this.cmbAsansor.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.cmbAsansor.Size = new System.Drawing.Size(114, 36);
            this.cmbAsansor.TabIndex = 7;
            this.cmbAsansor.ValueMember = "ID";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Vazirmatn", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(166, 192);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(64, 27);
            this.label7.TabIndex = 208;
            this.label7.Text = "آسانسور";
            // 
            // btnCancel
            // 
            this.btnCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnCancel.Font = new System.Drawing.Font("Vazirmatn", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btnCancel.ImageTextSpacing = 5;
            this.btnCancel.Location = new System.Drawing.Point(672, 0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor(2, 6, 6, 2);
            this.btnCancel.Size = new System.Drawing.Size(90, 68);
            this.btnCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.OfficeXP;
            this.btnCancel.Symbol = "";
            this.btnCancel.SymbolColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.btnCancel.SymbolSize = 15F;
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "انصراف";
            this.btnCancel.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // txtOwnerMasterID
            // 
            this.txtOwnerMasterID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOwnerMasterID.BorderRadius = 15;
            this.txtOwnerMasterID.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtOwnerMasterID.DefaultText = "";
            this.txtOwnerMasterID.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtOwnerMasterID.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtOwnerMasterID.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtOwnerMasterID.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtOwnerMasterID.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtOwnerMasterID.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOwnerMasterID.ForeColor = System.Drawing.Color.Black;
            this.txtOwnerMasterID.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtOwnerMasterID.Location = new System.Drawing.Point(312, 6);
            this.txtOwnerMasterID.Name = "txtOwnerMasterID";
            this.txtOwnerMasterID.PasswordChar = '\0';
            this.txtOwnerMasterID.PlaceholderText = "";
            this.txtOwnerMasterID.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtOwnerMasterID.SelectedText = "";
            this.txtOwnerMasterID.Size = new System.Drawing.Size(46, 36);
            this.txtOwnerMasterID.TabIndex = 200;
            this.txtOwnerMasterID.Visible = false;
            // 
            // txtMasterID
            // 
            this.txtMasterID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMasterID.BorderRadius = 15;
            this.txtMasterID.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtMasterID.DefaultText = "";
            this.txtMasterID.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtMasterID.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtMasterID.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtMasterID.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtMasterID.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtMasterID.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMasterID.ForeColor = System.Drawing.Color.Black;
            this.txtMasterID.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtMasterID.Location = new System.Drawing.Point(377, 5);
            this.txtMasterID.Name = "txtMasterID";
            this.txtMasterID.PasswordChar = '\0';
            this.txtMasterID.PlaceholderText = "";
            this.txtMasterID.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtMasterID.SelectedText = "";
            this.txtMasterID.Size = new System.Drawing.Size(46, 36);
            this.txtMasterID.TabIndex = 200;
            this.txtMasterID.Visible = false;
            // 
            // txtOwnerID
            // 
            this.txtOwnerID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOwnerID.BorderRadius = 15;
            this.txtOwnerID.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtOwnerID.DefaultText = "";
            this.txtOwnerID.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtOwnerID.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtOwnerID.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtOwnerID.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtOwnerID.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtOwnerID.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOwnerID.ForeColor = System.Drawing.Color.Black;
            this.txtOwnerID.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtOwnerID.Location = new System.Drawing.Point(466, 4);
            this.txtOwnerID.Name = "txtOwnerID";
            this.txtOwnerID.PasswordChar = '\0';
            this.txtOwnerID.PlaceholderText = "";
            this.txtOwnerID.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtOwnerID.SelectedText = "";
            this.txtOwnerID.Size = new System.Drawing.Size(46, 36);
            this.txtOwnerID.TabIndex = 200;
            this.txtOwnerID.Visible = false;
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
            this.txtID.Location = new System.Drawing.Point(531, 3);
            this.txtID.Name = "txtID";
            this.txtID.PasswordChar = '\0';
            this.txtID.PlaceholderText = "";
            this.txtID.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtID.SelectedText = "";
            this.txtID.Size = new System.Drawing.Size(46, 36);
            this.txtID.TabIndex = 200;
            this.txtID.Visible = false;
            // 
            // btnSave
            // 
            this.btnSave.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSave.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSave.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSave.Font = new System.Drawing.Font("Vazirmatn", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btnSave.ImageTextSpacing = 5;
            this.btnSave.Location = new System.Drawing.Point(762, 0);
            this.btnSave.Name = "btnSave";
            this.btnSave.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor(2, 6, 6, 2);
            this.btnSave.Size = new System.Drawing.Size(90, 68);
            this.btnSave.Style = DevComponents.DotNetBar.eDotNetBarStyle.OfficeXP;
            this.btnSave.Symbol = "";
            this.btnSave.SymbolColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.btnSave.SymbolSize = 15F;
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "ذخیره";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtPrice2
            // 
            this.txtPrice2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPrice2.BorderRadius = 5;
            this.txtPrice2.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtPrice2.DefaultText = "";
            this.txtPrice2.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtPrice2.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtPrice2.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtPrice2.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtPrice2.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtPrice2.FocusedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.txtPrice2.Font = new System.Drawing.Font("Vazirmatn", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPrice2.ForeColor = System.Drawing.Color.Black;
            this.txtPrice2.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtPrice2.Location = new System.Drawing.Point(162, 299);
            this.txtPrice2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtPrice2.Name = "txtPrice2";
            this.txtPrice2.PasswordChar = '\0';
            this.txtPrice2.PlaceholderText = "";
            this.txtPrice2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtPrice2.SelectedText = "";
            this.txtPrice2.Size = new System.Drawing.Size(158, 36);
            this.txtPrice2.TabIndex = 11;
            this.txtPrice2.TextChanged += new System.EventHandler(this.txtPrice2_TextChanged);
            this.txtPrice2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPrice2_KeyPress);
            this.txtPrice2.Leave += new System.EventHandler(this.txtPrice2_Leave);
            // 
            // txtPrice1
            // 
            this.txtPrice1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPrice1.BorderRadius = 5;
            this.txtPrice1.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtPrice1.DefaultText = "";
            this.txtPrice1.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtPrice1.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtPrice1.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtPrice1.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtPrice1.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtPrice1.FocusedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.txtPrice1.Font = new System.Drawing.Font("Vazirmatn", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPrice1.ForeColor = System.Drawing.Color.Black;
            this.txtPrice1.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtPrice1.Location = new System.Drawing.Point(548, 299);
            this.txtPrice1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtPrice1.Name = "txtPrice1";
            this.txtPrice1.PasswordChar = '\0';
            this.txtPrice1.PlaceholderText = "";
            this.txtPrice1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtPrice1.SelectedText = "";
            this.txtPrice1.Size = new System.Drawing.Size(158, 36);
            this.txtPrice1.TabIndex = 10;
            this.txtPrice1.TextChanged += new System.EventHandler(this.txtPrice1_TextChanged);
            this.txtPrice1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPrice1_KeyPress);
            this.txtPrice1.Leave += new System.EventHandler(this.txtPrice1_Leave);
            // 
            // lblPrice2
            // 
            this.lblPrice2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPrice2.AutoSize = true;
            this.lblPrice2.Font = new System.Drawing.Font("Vazirmatn", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrice2.Location = new System.Drawing.Point(326, 301);
            this.lblPrice2.Name = "lblPrice2";
            this.lblPrice2.Size = new System.Drawing.Size(71, 27);
            this.lblPrice2.TabIndex = 200;
            this.lblPrice2.Text = "قیمت کل";
            // 
            // lblPrice1
            // 
            this.lblPrice1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPrice1.AutoSize = true;
            this.lblPrice1.Font = new System.Drawing.Font("Vazirmatn", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrice1.Location = new System.Drawing.Point(712, 301);
            this.lblPrice1.Name = "lblPrice1";
            this.lblPrice1.Size = new System.Drawing.Size(85, 27);
            this.lblPrice1.TabIndex = 200;
            this.lblPrice1.Text = "قیمت متری";
            // 
            // chkAsansor
            // 
            this.chkAsansor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkAsansor.AutoSize = true;
            this.chkAsansor.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.chkAsansor.CheckedState.BorderRadius = 0;
            this.chkAsansor.CheckedState.BorderThickness = 0;
            this.chkAsansor.CheckedState.FillColor = System.Drawing.Color.Black;
            this.chkAsansor.Font = new System.Drawing.Font("Vazirmatn", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAsansor.Location = new System.Drawing.Point(133, 11);
            this.chkAsansor.Name = "chkAsansor";
            this.chkAsansor.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chkAsansor.Size = new System.Drawing.Size(83, 31);
            this.chkAsansor.TabIndex = 10;
            this.chkAsansor.Text = "آسانسور";
            this.chkAsansor.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.chkAsansor.UncheckedState.BorderRadius = 0;
            this.chkAsansor.UncheckedState.BorderThickness = 0;
            this.chkAsansor.UncheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.chkAsansor.Visible = false;
            // 
            // chkBalkon
            // 
            this.chkBalkon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkBalkon.AutoSize = true;
            this.chkBalkon.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.chkBalkon.CheckedState.BorderRadius = 0;
            this.chkBalkon.CheckedState.BorderThickness = 0;
            this.chkBalkon.CheckedState.FillColor = System.Drawing.Color.Black;
            this.chkBalkon.Font = new System.Drawing.Font("Vazirmatn", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkBalkon.Location = new System.Drawing.Point(46, 243);
            this.chkBalkon.Name = "chkBalkon";
            this.chkBalkon.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chkBalkon.Size = new System.Drawing.Size(65, 31);
            this.chkBalkon.TabIndex = 9;
            this.chkBalkon.Text = "بالکن";
            this.chkBalkon.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.chkBalkon.UncheckedState.BorderRadius = 0;
            this.chkBalkon.UncheckedState.BorderThickness = 0;
            this.chkBalkon.UncheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            // 
            // chkAnbari
            // 
            this.chkAnbari.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkAnbari.AutoSize = true;
            this.chkAnbari.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.chkAnbari.CheckedState.BorderRadius = 0;
            this.chkAnbari.CheckedState.BorderThickness = 0;
            this.chkAnbari.CheckedState.FillColor = System.Drawing.Color.Black;
            this.chkAnbari.Font = new System.Drawing.Font("Vazirmatn", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAnbari.Location = new System.Drawing.Point(133, 243);
            this.chkAnbari.Name = "chkAnbari";
            this.chkAnbari.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chkAnbari.Size = new System.Drawing.Size(66, 31);
            this.chkAnbari.TabIndex = 8;
            this.chkAnbari.Text = "انباری";
            this.chkAnbari.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.chkAnbari.UncheckedState.BorderRadius = 0;
            this.chkAnbari.UncheckedState.BorderThickness = 0;
            this.chkAnbari.UncheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Vazirmatn", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(365, 190);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(60, 27);
            this.label6.TabIndex = 200;
            this.label6.Text = "کفپوش";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.Font = new System.Drawing.Font("Vazirmatn", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(598, 172);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(86, 68);
            this.label5.TabIndex = 200;
            this.label5.Text = "سرویس بهداشتی";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Vazirmatn", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(786, 190);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 27);
            this.label4.TabIndex = 200;
            this.label4.Text = "کابینت";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Vazirmatn", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(166, 124);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 27);
            this.label3.TabIndex = 200;
            this.label3.Text = "پارکینگ";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Vazirmatn", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(373, 124);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 27);
            this.label2.TabIndex = 200;
            this.label2.Text = "خواب";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Vazirmatn", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(605, 124);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 27);
            this.label1.TabIndex = 200;
            this.label1.Text = "زیربنا";
            // 
            // label15
            // 
            this.label15.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Vazirmatn", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(786, 124);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(44, 27);
            this.label15.TabIndex = 200;
            this.label15.Text = "طبقه";
            // 
            // guna2Elipse1
            // 
            this.guna2Elipse1.TargetControl = this;
            // 
            // errorEjbari
            // 
            this.errorEjbari.ContainerControl = this;
            // 
            // FrmFileDet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.ClientSize = new System.Drawing.Size(852, 349);
            this.Controls.Add(this.cmbAsansor);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtZirbana);
            this.Controls.Add(this.cmbKafposhID);
            this.Controls.Add(this.cmbServiceWcID);
            this.Controls.Add(this.cmbKabinetID);
            this.Controls.Add(this.cmbParking);
            this.Controls.Add(this.cmbKhab);
            this.Controls.Add(this.cmbTabaghe);
            this.Controls.Add(this.btnCalc);
            this.Controls.Add(this.guna2Panel1);
            this.Controls.Add(this.txtPrice2);
            this.Controls.Add(this.txtPrice1);
            this.Controls.Add(this.lblPrice2);
            this.Controls.Add(this.lblPrice1);
            this.Controls.Add(this.chkBalkon);
            this.Controls.Add(this.chkAnbari);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.panelTop);
            this.Font = new System.Drawing.Font("Vazirmatn", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmFileDet";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmFileDet";
            this.Load += new System.EventHandler(this.FrmFileDet_Load);
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.guna2Panel1.ResumeLayout(false);
            this.guna2Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorEjbari)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Guna.UI2.WinForms.Guna2DragControl guna2DragControl1;
        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse1;
        private Guna.UI2.WinForms.Guna2Panel panelTop;
        private Guna.UI2.WinForms.Guna2CircleButton btnClose;
        private System.Windows.Forms.Label lblDet;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private Guna.UI2.WinForms.Guna2CheckBox chkAsansor;
        private Guna.UI2.WinForms.Guna2CheckBox chkBalkon;
        private Guna.UI2.WinForms.Guna2CheckBox chkAnbari;
        private Guna.UI2.WinForms.Guna2TextBox txtPrice2;
        private Guna.UI2.WinForms.Guna2TextBox txtPrice1;
        private System.Windows.Forms.Label lblPrice2;
        private System.Windows.Forms.Label lblPrice1;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private Guna.UI2.WinForms.Guna2TextBox txtID;
        private Guna.UI2.WinForms.Guna2TextBox txtOwnerID;
        private Guna.UI2.WinForms.Guna2Button btnCalc;
        private DevComponents.DotNetBar.ButtonX btnCancel;
        private DevComponents.DotNetBar.ButtonX btnSave;
        private Guna.UI2.WinForms.Guna2ComboBox cmbTabaghe;
        private Guna.UI2.WinForms.Guna2ComboBox cmbKafposhID;
        private Guna.UI2.WinForms.Guna2ComboBox cmbServiceWcID;
        private Guna.UI2.WinForms.Guna2ComboBox cmbKabinetID;
        private Guna.UI2.WinForms.Guna2ComboBox cmbParking;
        private Guna.UI2.WinForms.Guna2ComboBox cmbKhab;
        private Guna.UI2.WinForms.Guna2TextBox txtZirbana;
        private Guna.UI2.WinForms.Guna2CircleButton btnMin;
        private Guna.UI2.WinForms.Guna2TextBox txtOwnerMasterID;
        private Guna.UI2.WinForms.Guna2TextBox txtMasterID;
        private System.Windows.Forms.ErrorProvider errorEjbari;
        private Guna.UI2.WinForms.Guna2ComboBox cmbAsansor;
        private System.Windows.Forms.Label label7;
    }
}