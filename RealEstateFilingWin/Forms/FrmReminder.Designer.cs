namespace RealEstateFilingWin.Forms
{
    partial class FrmReminder
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
            this.panelTop = new Guna.UI2.WinForms.Guna2Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.btnMin = new Guna.UI2.WinForms.Guna2CircleButton();
            this.btnClose = new Guna.UI2.WinForms.Guna2CircleButton();
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.btnDeleteReminder = new DevComponents.DotNetBar.ButtonX();
            this.btnCancel = new DevComponents.DotNetBar.ButtonX();
            this.btnSave = new DevComponents.DotNetBar.ButtonX();
            this.txtID = new Guna.UI2.WinForms.Guna2TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtDescription = new Guna.UI2.WinForms.Guna2TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.clsDateStart = new BasicComponent.ClsMaskedTextBox(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.txtTime = new System.Windows.Forms.MaskedTextBox();
            this.btntime = new DevComponents.DotNetBar.ButtonX();
            this.btnnim = new DevComponents.DotNetBar.ButtonItem();
            this.btnyek = new DevComponents.DotNetBar.ButtonItem();
            this.btndow = new DevComponents.DotNetBar.ButtonItem();
            this.btnse = new DevComponents.DotNetBar.ButtonItem();
            this.btnchahar = new DevComponents.DotNetBar.ButtonItem();
            this.panelTop.SuspendLayout();
            this.guna2Panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.Color.DimGray;
            this.panelTop.Controls.Add(this.label2);
            this.panelTop.Controls.Add(this.btnMin);
            this.panelTop.Controls.Add(this.btnClose);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(581, 35);
            this.panelTop.TabIndex = 221;
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Right;
            this.label2.Font = new System.Drawing.Font("Vazirmatn", 14F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(396, 0);
            this.label2.Name = "label2";
            this.label2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label2.Size = new System.Drawing.Size(185, 35);
            this.label2.TabIndex = 204;
            this.label2.Text = "ثبت و ویرایش یادآور";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
            this.btnMin.Location = new System.Drawing.Point(41, 0);
            this.btnMin.Name = "btnMin";
            this.btnMin.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.btnMin.Size = new System.Drawing.Size(40, 35);
            this.btnMin.TabIndex = 203;
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
            this.btnClose.Size = new System.Drawing.Size(41, 35);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "X";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.BackColor = System.Drawing.Color.Gainsboro;
            this.guna2Panel1.Controls.Add(this.btnDeleteReminder);
            this.guna2Panel1.Controls.Add(this.btnCancel);
            this.guna2Panel1.Controls.Add(this.btnSave);
            this.guna2Panel1.Controls.Add(this.txtID);
            this.guna2Panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.guna2Panel1.Font = new System.Drawing.Font("Vazirmatn", 12F, System.Drawing.FontStyle.Bold);
            this.guna2Panel1.Location = new System.Drawing.Point(0, 35);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Padding = new System.Windows.Forms.Padding(5);
            this.guna2Panel1.Size = new System.Drawing.Size(581, 69);
            this.guna2Panel1.TabIndex = 222;
            // 
            // btnDeleteReminder
            // 
            this.btnDeleteReminder.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnDeleteReminder.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnDeleteReminder.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnDeleteReminder.Font = new System.Drawing.Font("Vazirmatn", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeleteReminder.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btnDeleteReminder.ImageTextSpacing = 5;
            this.btnDeleteReminder.Location = new System.Drawing.Point(306, 5);
            this.btnDeleteReminder.Name = "btnDeleteReminder";
            this.btnDeleteReminder.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor(2, 6, 6, 2);
            this.btnDeleteReminder.Size = new System.Drawing.Size(90, 59);
            this.btnDeleteReminder.Style = DevComponents.DotNetBar.eDotNetBarStyle.OfficeXP;
            this.btnDeleteReminder.Symbol = "";
            this.btnDeleteReminder.SymbolColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.btnDeleteReminder.SymbolSize = 15F;
            this.btnDeleteReminder.TabIndex = 5;
            this.btnDeleteReminder.Text = "حذف یادآور";
            this.btnDeleteReminder.Visible = false;
            this.btnDeleteReminder.Click += new System.EventHandler(this.btnDeleteReminder_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnCancel.Font = new System.Drawing.Font("Vazirmatn", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btnCancel.ImageTextSpacing = 5;
            this.btnCancel.Location = new System.Drawing.Point(396, 5);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor(2, 6, 6, 2);
            this.btnCancel.Size = new System.Drawing.Size(90, 59);
            this.btnCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.OfficeXP;
            this.btnCancel.Symbol = "";
            this.btnCancel.SymbolColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.btnCancel.SymbolSize = 15F;
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "انصراف";
            // 
            // btnSave
            // 
            this.btnSave.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSave.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSave.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSave.Font = new System.Drawing.Font("Vazirmatn", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btnSave.ImageTextSpacing = 5;
            this.btnSave.Location = new System.Drawing.Point(486, 5);
            this.btnSave.Name = "btnSave";
            this.btnSave.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor(2, 6, 6, 2);
            this.btnSave.Size = new System.Drawing.Size(90, 59);
            this.btnSave.Style = DevComponents.DotNetBar.eDotNetBarStyle.OfficeXP;
            this.btnSave.Symbol = "";
            this.btnSave.SymbolColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.btnSave.SymbolSize = 15F;
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "ذخیره";
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
            this.txtID.Location = new System.Drawing.Point(-371, 8);
            this.txtID.Name = "txtID";
            this.txtID.PasswordChar = '\0';
            this.txtID.PlaceholderText = "";
            this.txtID.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtID.SelectedText = "";
            this.txtID.Size = new System.Drawing.Size(64, 36);
            this.txtID.TabIndex = 4;
            this.txtID.Visible = false;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Vazirmatn", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(496, 116);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 27);
            this.label4.TabIndex = 227;
            this.label4.Text = "تاریخ";
            // 
            // txtDescription
            // 
            this.txtDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDescription.BorderRadius = 5;
            this.txtDescription.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtDescription.DefaultText = "";
            this.txtDescription.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtDescription.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtDescription.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtDescription.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtDescription.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtDescription.FocusedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.txtDescription.Font = new System.Drawing.Font("Vazirmatn", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDescription.ForeColor = System.Drawing.Color.Black;
            this.txtDescription.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtDescription.Location = new System.Drawing.Point(12, 155);
            this.txtDescription.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.PasswordChar = '\0';
            this.txtDescription.PlaceholderText = "";
            this.txtDescription.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtDescription.SelectedText = "";
            this.txtDescription.Size = new System.Drawing.Size(466, 208);
            this.txtDescription.TabIndex = 220;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Vazirmatn", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(496, 160);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 27);
            this.label3.TabIndex = 223;
            this.label3.Text = "توضیحات";
            // 
            // clsDateStart
            // 
            this.clsDateStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.clsDateStart.BackgroundStyle.Class = "TextBoxBorder";
            this.clsDateStart.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.clsDateStart.BLObjectName = null;
            this.clsDateStart.ButtonClear.Symbol = "";
            this.clsDateStart.ButtonClear.Visible = true;
            this.clsDateStart.ButtonCustom.Symbol = "";
            this.clsDateStart.ButtonCustom.Visible = true;
            this.clsDateStart.ButtonCustom2.Symbol = "";
            this.clsDateStart.ButtonCustom2.Visible = true;
            this.clsDateStart.FieldName = null;
            this.clsDateStart.Font = new System.Drawing.Font("Vazirmatn", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clsDateStart.GetMethodName = null;
            this.clsDateStart.IsRequierd = false;
            this.clsDateStart.IsShamsi = false;
            this.clsDateStart.IsTime = false;
            this.clsDateStart.Location = new System.Drawing.Point(213, 117);
            this.clsDateStart.LockStatus = BasicComponent.ClsInterface.EnumLockStatus.EditAble;
            this.clsDateStart.Mask = "0000/00/00";
            this.clsDateStart.Name = "clsDateStart";
            this.clsDateStart.Size = new System.Drawing.Size(265, 31);
            this.clsDateStart.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.clsDateStart.TabIndex = 228;
            this.clsDateStart.Text = "";
            this.clsDateStart.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.clsDateStart.TextMaskFormat = System.Windows.Forms.MaskFormat.IncludePromptAndLiterals;
            this.clsDateStart.ValidateType = BasicComponent.ClsInterface.ValidateType.IsNullOrEmpty;
            this.clsDateStart.ValidatingType = typeof(System.DateTime);
            this.clsDateStart.ViewStatus = BasicComponent.ClsInterface.EnumViewStatus.EditMode;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Vazirmatn", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(164, 121);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 27);
            this.label1.TabIndex = 229;
            this.label1.Text = "زمان";
            // 
            // txtTime
            // 
            this.txtTime.Location = new System.Drawing.Point(49, 121);
            this.txtTime.Mask = "00:00";
            this.txtTime.Name = "txtTime";
            this.txtTime.Size = new System.Drawing.Size(109, 28);
            this.txtTime.TabIndex = 230;
            this.txtTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtTime.ValidatingType = typeof(System.DateTime);
            this.txtTime.MaskInputRejected += new System.Windows.Forms.MaskInputRejectedEventHandler(this.txtTime_MaskInputRejected);
            this.txtTime.TypeValidationCompleted += new System.Windows.Forms.TypeValidationEventHandler(this.txtTime_TypeValidationCompleted);
            // 
            // btntime
            // 
            this.btntime.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btntime.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btntime.Font = new System.Drawing.Font("Vazirmatn", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btntime.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btntime.ImageTextSpacing = 5;
            this.btntime.Location = new System.Drawing.Point(12, 121);
            this.btntime.Name = "btntime";
            this.btntime.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor(2, 6, 6, 2);
            this.btntime.Size = new System.Drawing.Size(31, 28);
            this.btntime.Style = DevComponents.DotNetBar.eDotNetBarStyle.OfficeXP;
            this.btntime.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btnnim,
            this.btnyek,
            this.btndow,
            this.btnse,
            this.btnchahar});
            this.btntime.Symbol = "57745";
            this.btntime.SymbolColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.btntime.SymbolSet = DevComponents.DotNetBar.eSymbolSet.Material;
            this.btntime.SymbolSize = 15F;
            this.btntime.TabIndex = 231;
            this.btntime.Click += new System.EventHandler(this.btntime_Click);
            // 
            // btnnim
            // 
            this.btnnim.GlobalItem = false;
            this.btnnim.Name = "btnnim";
            this.btnnim.Text = "نیم ساعت بعد";
            this.btnnim.Click += new System.EventHandler(this.btnnim_Click);
            // 
            // btnyek
            // 
            this.btnyek.GlobalItem = false;
            this.btnyek.Name = "btnyek";
            this.btnyek.Text = "یک ساعت بعد";
            this.btnyek.Click += new System.EventHandler(this.btnyek_Click);
            // 
            // btndow
            // 
            this.btndow.GlobalItem = false;
            this.btndow.Name = "btndow";
            this.btndow.Text = "دو ساعت بعد";
            this.btndow.Click += new System.EventHandler(this.btndow_Click);
            // 
            // btnse
            // 
            this.btnse.GlobalItem = false;
            this.btnse.Name = "btnse";
            this.btnse.Text = "سه ساعت بعد";
            this.btnse.Click += new System.EventHandler(this.btnse_Click);
            // 
            // btnchahar
            // 
            this.btnchahar.GlobalItem = false;
            this.btnchahar.Name = "btnchahar";
            this.btnchahar.Text = "چهار ساعت بعد";
            this.btnchahar.Click += new System.EventHandler(this.btnchahar_Click);
            // 
            // FrmReminder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 22F);
            this.ClientSize = new System.Drawing.Size(581, 382);
            this.Controls.Add(this.btntime);
            this.Controls.Add(this.txtTime);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.clsDateStart);
            this.Controls.Add(this.guna2Panel1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.panelTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmReminder";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.FrmReminder_Load);
            this.panelTop.ResumeLayout(false);
            this.guna2Panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel panelTop;
        private System.Windows.Forms.Label label2;
        private Guna.UI2.WinForms.Guna2CircleButton btnMin;
        private Guna.UI2.WinForms.Guna2CircleButton btnClose;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private DevComponents.DotNetBar.ButtonX btnCancel;
        private DevComponents.DotNetBar.ButtonX btnSave;
        private Guna.UI2.WinForms.Guna2TextBox txtID;
        private System.Windows.Forms.Label label4;
        private Guna.UI2.WinForms.Guna2TextBox txtDescription;
        private System.Windows.Forms.Label label3;
        private BasicComponent.ClsMaskedTextBox clsDateStart;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MaskedTextBox txtTime;
        private DevComponents.DotNetBar.ButtonX btntime;
        private DevComponents.DotNetBar.ButtonItem btnnim;
        private DevComponents.DotNetBar.ButtonItem btnyek;
        private DevComponents.DotNetBar.ButtonItem btndow;
        private DevComponents.DotNetBar.ButtonItem btnse;
        private DevComponents.DotNetBar.ButtonItem btnchahar;
        private DevComponents.DotNetBar.ButtonX btnDeleteReminder;
    }
}
