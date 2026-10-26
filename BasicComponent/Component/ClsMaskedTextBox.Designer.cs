namespace BasicComponent
{
    partial class ClsMaskedTextBox : DevComponents.DotNetBar.Controls.MaskedTextBoxAdv
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // ClsMaskedTextBox
            // 
            this.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.BackgroundStyle.Class = "TextBoxBorder";
            this.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ButtonClear.Symbol = "";
            this.ButtonClear.Visible = true;
            this.ButtonCustom.Symbol = "";
            this.ButtonCustom.Visible = true;
            this.ButtonCustom2.Visible = true;
            this.CutCopyMaskFormat = System.Windows.Forms.MaskFormat.IncludeLiterals;
            this.Font = new System.Drawing.Font("Vazirmatn", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Default;
            this.Location = new System.Drawing.Point(216, 201);
            this.Mask = "0000/00/00";
            this.Name = "maskedTextBoxAdv1";
            this.Size = new System.Drawing.Size(150, 27);
            this.TabIndex = 14;
            this.Text = "____/__/__";
            this.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.TextMaskFormat = System.Windows.Forms.MaskFormat.IncludePromptAndLiterals;
            this.ValidatingType = typeof(System.DateTime);
            this.ButtonClearClick += new System.ComponentModel.CancelEventHandler(this.ClsMaskedTextBox_ButtonClearClick);
            this.ButtonCustomClick += new System.EventHandler(this.ClsMaskedTextBox_ButtonCustomClick);
            this.ButtonCustom2Click += new System.EventHandler(this.ClsMaskedTextBox_ButtonCustom2Click);
            this.TypeValidationCompleted += new System.Windows.Forms.TypeValidationEventHandler(this.ClsMaskedTextBox_TypeValidationCompleted);
            this.TextChanged += new System.EventHandler(this.ClsMaskedTextBox_TextChanged);
            this.Click += new System.EventHandler(this.ClsMaskedTextBox_Click);
            this.Enter += new System.EventHandler(this.ClsMaskedTextBox_Enter);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ClsMaskedTextBox_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.ClsMaskedTextBox_KeyUp);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ClsMaskedTextBox_MouseDown);
            this.MouseLeave += new System.EventHandler(this.ClsMaskedTextBox_MouseLeave);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}
