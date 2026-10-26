using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RealEstateFilingWin.Forms
{
    public enum MsgBoxType
    {
        Info,
        Warning,
        Error,
        Question
    }
    public partial class FrmMsgBox : FrmBase
    {
        public DialogResult Result { get; private set; } = DialogResult.None;
        private System.Windows.Forms.Timer autoCloseTimer;
        private int autoCloseSeconds = 1; // زمان بسته شدن اتوماتیک (ثانیه)
        public FrmMsgBox(string message, string title = "پیام", MsgBoxType type = MsgBoxType.Info, MessageBoxButtons buttons = MessageBoxButtons.OK)
        {
            InitializeComponent();

            lblMessage.Text = message;
            this.Text = title;

            // ظاهر آیکون بر اساس نوع پیام
            switch (type)
            {
                case MsgBoxType.Info:
                    picIcon.Image = SystemIcons.Information.ToBitmap();
                    pnlTop.BackColor = Color.FromArgb(33, 150, 243); // آبی
                    break;
                case MsgBoxType.Warning:
                    picIcon.Image = SystemIcons.Warning.ToBitmap();
                    pnlTop.BackColor = Color.FromArgb(255, 152, 0); // نارنجی
                    break;
                case MsgBoxType.Error:
                    picIcon.Image = SystemIcons.Error.ToBitmap();
                    pnlTop.BackColor = Color.FromArgb(244, 67, 54); // قرمز
                    break;
                case MsgBoxType.Question:
                    picIcon.Image = SystemIcons.Question.ToBitmap();
                    pnlTop.BackColor = Color.FromArgb(76, 175, 80); // سبز
                    break;
            }

            // ایجاد دکمه‌ها
            CreateButtons(buttons);
            if (type == MsgBoxType.Info)
                StartAutoCloseTimer();
        }
        private void StartAutoCloseTimer()
        {
            autoCloseTimer = new System.Windows.Forms.Timer();
            autoCloseTimer.Interval = 1000; // هر 1 ثانیه
            autoCloseTimer.Tick += AutoCloseTimer_Tick;
            autoCloseTimer.Start();
        }
        private void CreateButtons(MessageBoxButtons buttons)
        {
            pnlButtons.Controls.Clear();

            void AddButton(string text, DialogResult result, Color color)
            {
                var btn = new Button
                {
                    Text = text,
                    DialogResult = result,
                    AutoSize = true,
                    FlatStyle = FlatStyle.Flat,
                    BackColor = color,
                    ForeColor = Color.White,
                    Font = new Font("Vazirmatn", 10, FontStyle.Bold),
                    Margin = new Padding(5),
                    Padding = new Padding(10, 5, 10, 10),
                };
                btn.FlatAppearance.BorderSize = 0;
                btn.Click += (s, e) => { this.Result = result; this.Close(); };
                pnlButtons.Controls.Add(btn);
            }

            if (buttons == MessageBoxButtons.OK)
            {
                AddButton("تأیید", DialogResult.OK, Color.FromArgb(33, 150, 243));
            }
            else if (buttons == MessageBoxButtons.OKCancel)
            {
                AddButton("تأیید", DialogResult.OK, Color.FromArgb(33, 150, 243));
                AddButton("انصراف", DialogResult.Cancel, Color.Gray);
            }
            else if (buttons == MessageBoxButtons.YesNo)
            {
                AddButton("بله", DialogResult.Yes, Color.FromArgb(76, 175, 80));
                AddButton("خیر", DialogResult.No, Color.FromArgb(244, 67, 54));
            }

            pnlButtons.FlowDirection = FlowDirection.RightToLeft;
        }

        public static DialogResult Show(string message, string title = "پیام", MsgBoxType type = MsgBoxType.Info, MessageBoxButtons buttons = MessageBoxButtons.OK)
        {
            using (var f = new FrmMsgBox(message, title, type, buttons))
            {
                f.StartPosition = FormStartPosition.CenterParent;
                f.ShowDialog();
                return f.Result;
            }
        }

        private void AutoCloseTimer_Tick(object sender, EventArgs e)
        {
            autoCloseSeconds--;

            if (autoCloseSeconds <= 0)
            {
                autoCloseTimer.Stop();
                this.Close();
            }
        }
    }
}
