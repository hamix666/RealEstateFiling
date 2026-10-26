using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RealEstateFilingWin.Forms
{
    public partial class FrmBase : Form
    {
        public FrmBase()
        {
            InitializeComponent();
            if (!DesignMode) // ← فقط در زمان اجرا آیکن ست شود
            {
                try
                {
                    string iconPath = Path.Combine(Application.StartupPath, "Resources", "logo1.ico");
                    if (File.Exists(iconPath))
                        this.Icon = new Icon(iconPath);
                }
                catch (Exception ex)
                {
                    // خطا رو لاگ کن ولی کاری نکن که دیزاینر خراب بشه
                    Console.WriteLine("Icon load error: " + ex.Message);
                }
            }
            this.KeyPreview = true;  // اجازه میده فرم کلیدها را قبل از کنترل‌ها بگیره
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape && this.Name!="FrmMain")
            {
                this.Close();
                return true; // کلید پردازش شد
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

    }
}
