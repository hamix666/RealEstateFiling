namespace WindowsFormsApp1
{
    partial class Form1
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
            this.label1 = new System.Windows.Forms.Label();
            this.timesystem = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.timeglobal = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.difrent = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(581, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "سیستم";
            // 
            // timesystem
            // 
            this.timesystem.AutoSize = true;
            this.timesystem.Location = new System.Drawing.Point(473, 41);
            this.timesystem.Name = "timesystem";
            this.timesystem.Size = new System.Drawing.Size(35, 13);
            this.timesystem.TabIndex = 0;
            this.timesystem.Text = "label1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(581, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "جهانی";
            // 
            // timeglobal
            // 
            this.timeglobal.AutoSize = true;
            this.timeglobal.Location = new System.Drawing.Point(473, 80);
            this.timeglobal.Name = "timeglobal";
            this.timeglobal.Size = new System.Drawing.Size(35, 13);
            this.timeglobal.TabIndex = 0;
            this.timeglobal.Text = "label1";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(581, 122);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "اختلاف";
            // 
            // difrent
            // 
            this.difrent.AutoSize = true;
            this.difrent.Location = new System.Drawing.Point(473, 122);
            this.difrent.Name = "difrent";
            this.difrent.Size = new System.Drawing.Size(35, 13);
            this.difrent.TabIndex = 0;
            this.difrent.Text = "label1";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(142, 80);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(670, 236);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.difrent);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.timeglobal);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.timesystem);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label timesystem;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label timeglobal;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label difrent;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button button1;
    }
}

