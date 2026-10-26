using RealEstateFilingWin.Helpers;
using System;
using System.Data.SqlClient;


namespace Common
{
    public class ClsConnection
    {
        #region Property & Variable
        private static ClsConnection _ConsString = null;

        private String _String = null;
        
        public static string ConString
        {
            get
            {
                if (_ConsString == null)
                {
                    _ConsString = new ClsConnection { _String = ClsConnection.Connect() };
                    return _ConsString._String;
                }
                else
                    return _ConsString._String;
            }
        }

        #endregion

        #region Constructor
        private ClsConnection() { }

        #endregion

        #region  Method

        public static string Connect()
        {
            string aesKey = AESKeyManager.EnsureKeyExists();
            string decryptedPassword = "123"; // پیش‌فرض

            try
            {
                // سعی کن پسورد رمزگذاری‌شده رو بخونی
                string encryptedPackage = PasswordStore.LoadEncryptedPackage();
                if (!string.IsNullOrEmpty(encryptedPackage))
                {
                    decryptedPassword = SqlPasswordCrypto.DecryptWithRawKey(encryptedPackage, aesKey);
                }
            }
            catch
            {
                // در صورتی که فایل خراب یا خالیه، پسورد پیش‌فرض رو بذار
                decryptedPassword = "123";
            }

            SqlConnectionStringBuilder sqlBuilder = new SqlConnectionStringBuilder();
            ClsBaseSetting.ServerName = RealEstateFilingWin.Properties.Settings.Default.SvrName;
            if (ClsBaseSetting.ServerName == null)
            {
                sqlBuilder.DataSource = @"DESKTOP-49ANEBF\HAMIDSQL";
                sqlBuilder.InitialCatalog = "RealEstateFilingDB";
                sqlBuilder.UserID = "sa";
                sqlBuilder.Password = decryptedPassword;
                //if (Snfo.Spc.u2 == "")
                //    sqlBuilder.Password = NovinCommon.ClsBaseSetting.CP("R9O0HKmbPtpoh4+l3imT95x3N3a62Otsov2o378gjdc=");;
                //else
                //    sqlBuilder.Password = Snfo.Spc.u2;
                //if (ClsBaseSetting.SqlPaswword == null)
                //    sqlBuilder.Password = NovinCommon.ClsBaseSetting.CP("R9O0HKmbPtpoh4+l3imT95x3N3a62Otsov2o378gjdc="); 
                //else
                //    sqlBuilder.Password = ClsBaseSetting.SqlPaswword;

                sqlBuilder.MultipleActiveResultSets = true;
                sqlBuilder.PersistSecurityInfo = false;
                sqlBuilder["Connect Timeout"] = 0;
                sqlBuilder.Pooling = false;
            }
            else
            {
                sqlBuilder.DataSource = Common.ClsBaseSetting.ServerName;

                sqlBuilder.InitialCatalog = "RealEstateFilingDB";

                sqlBuilder.UserID = "sa";
                sqlBuilder.Password = decryptedPassword;

                sqlBuilder.MultipleActiveResultSets = true;
                sqlBuilder.PersistSecurityInfo = false;
                sqlBuilder["Connect Timeout"] = 0;
                sqlBuilder.Pooling = false;
            }

            return sqlBuilder.ConnectionString;
        }
        public static string Connectcheck()
        {
            string aesKey = AESKeyManager.EnsureKeyExists();
            string decryptedPassword = "123"; // پیش‌فرض

            try
            {
                // سعی کن پسورد رمزگذاری‌شده رو بخونی
                string encryptedPackage = PasswordStore.LoadEncryptedPackage();
                if (!string.IsNullOrEmpty(encryptedPackage))
                {
                    decryptedPassword = SqlPasswordCrypto.DecryptWithRawKey(encryptedPackage, aesKey);
                }
            }
            catch
            {
                // در صورتی که فایل خراب یا خالیه، پسورد پیش‌فرض رو بذار
                decryptedPassword = "123";
            }

            SqlConnectionStringBuilder sqlBuilder = new SqlConnectionStringBuilder();
            ClsBaseSetting.ServerName = RealEstateFilingWin.Properties.Settings.Default.SvrName;
            if (ClsBaseSetting.ServerName == null)
            {
                sqlBuilder.DataSource = @"DESKTOP-49ANEBF\HAMIDSQL";
                sqlBuilder.InitialCatalog = "master";
                sqlBuilder.UserID = "sa";
                sqlBuilder.Password = decryptedPassword;
                //if (Snfo.Spc.u2 == "")
                //    sqlBuilder.Password = NovinCommon.ClsBaseSetting.CP("R9O0HKmbPtpoh4+l3imT95x3N3a62Otsov2o378gjdc=");;
                //else
                //    sqlBuilder.Password = Snfo.Spc.u2;
                //if (ClsBaseSetting.SqlPaswword == null)
                //    sqlBuilder.Password = NovinCommon.ClsBaseSetting.CP("R9O0HKmbPtpoh4+l3imT95x3N3a62Otsov2o378gjdc="); 
                //else
                //    sqlBuilder.Password = ClsBaseSetting.SqlPaswword;

                sqlBuilder.MultipleActiveResultSets = true;
                sqlBuilder.PersistSecurityInfo = false;
                sqlBuilder["Connect Timeout"] = 0;
                sqlBuilder.Pooling = false;
            }
            else
            {
                sqlBuilder.DataSource = Common.ClsBaseSetting.ServerName;

                sqlBuilder.InitialCatalog = "master";

                sqlBuilder.UserID = "sa";
                sqlBuilder.Password = decryptedPassword;

                sqlBuilder.MultipleActiveResultSets = true;
                sqlBuilder.PersistSecurityInfo = false;
                sqlBuilder["Connect Timeout"] = 0;
                sqlBuilder.Pooling = false;
            }

            return sqlBuilder.ConnectionString;
        }

        public static bool TestConnection(string connectionString)
        {
            try
            {

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        #endregion
    }
}
