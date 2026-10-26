using Common;
using RealEstateFilingWin.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RealEstateFilingWin.Forms
{
    public partial class FrmSqlPassword : Form
    {
        private string aesKey;

        public FrmSqlPassword()
        {
            InitializeComponent();
            // تولید یا خواندن کلید AES به صورت خودکار
            aesKey = AESKeyManager.EnsureKeyExists();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string sqlPassword = txtPassword.Text.Trim();
            if (string.IsNullOrEmpty(sqlPassword))
            {
                FrmMsgBox.Show("لطفاً پسورد SQL را وارد کنید.");
                return;
            }

            string package = SqlPasswordCrypto.EncryptWithRawKey(sqlPassword, aesKey);
            PasswordStore.SaveEncryptedPackage(package);
            FrmMsgBox.Show("پسورد ذخیره شد.");

        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            string package = PasswordStore.LoadEncryptedPackage();
            aesKey = AESKeyManager.EnsureKeyExists();
            if (string.IsNullOrEmpty(package))
            {
                FrmMsgBox.Show("پسورد ذخیره نشده است!");
                return;
            }

            string plainPassword = SqlPasswordCrypto.DecryptWithRawKey(package, aesKey);

            var builder = new SqlConnectionStringBuilder
            {

                DataSource = Common.ClsBaseSetting.ServerName,

                InitialCatalog = "master",

                UserID = "sa",
                Password = plainPassword,

                MultipleActiveResultSets = true,
                PersistSecurityInfo = false,
                Pooling = false,

                //DataSource = ClsBaseSetting.ServerName,
                //    InitialCatalog = "master",
                //    UserID = "sa",
                //    Password = plainPassword,
                //    Encrypt = true,
                //    TrustServerCertificate = false
            };

            try
            {
                using (var conn = new SqlConnection(builder.ConnectionString))
                {
                    conn.Open();
                    FrmMsgBox.Show("اتصال موفق!");
                }
            }
            catch (Exception ex)
            {
                FrmMsgBox.Show("اتصال ناموفق: " + ex.Message);
            }
        }
    }
}
