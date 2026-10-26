using Common;
using RealEstateFiling.Services;
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
    public partial class FrmPassword : Form
    {
        public FrmPassword()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtpassolse.Text))
            {
                FrmMsgBox.Show("رمز قبلی را وارد کنید");
                return;
            }

            if (string.IsNullOrEmpty(txtPassword.Text))
            {
                FrmMsgBox.Show("رمز جدید را وارد کنید");
                return;
            }

            using (var db = new RealEstateContext())
            {
                var user = db.Users_List
                             .Where(x => x.ID == ClsBaseSetting.UserID)
                             .FirstOrDefault();

                if (user == null)
                {
                    FrmMsgBox.Show("کاربر یافت نشد");
                    return;
                }

                // 🔥 بررسی درستی پسورد قبلی
                bool isOldPassCorrect = BCrypt.Net.BCrypt.Verify(
                    txtpassolse.Text.Trim(),
                    user.Password
                );

                if (!isOldPassCorrect)
                {
                    FrmMsgBox.Show("رمز قبلی اشتباه است");
                    return;
                }

                // 🔥 ذخیره پسورد جدید
                user.Password = BCrypt.Net.BCrypt.HashPassword(txtPassword.Text.Trim());
                db.SaveChanges();

                FrmMsgBox.Show("رمز با موفقیت تغییر کرد");
                this.Close();
            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
