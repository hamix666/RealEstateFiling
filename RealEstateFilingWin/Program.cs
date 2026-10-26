using RealEstateFiling.Services;
using RealEstateFilingWin.Forms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

using RealEstateFilingWin.Helpers;
using System.Data.SqlClient;
using Common;
using System.Globalization;
using System.Threading;
using RealEstateFilingWin.Services.Sync;
using System.Net.Sockets;




namespace RealEstateFilingWin
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                


                //if (DateTime.Now.Month != 11)
                //{
                //    return;
                //}
                // ✅ ست کردن Culture فارسی برای کل Thread
                CultureInfo persianCulture = new CultureInfo("fa-IR");
                Thread.CurrentThread.CurrentCulture = persianCulture;
                Thread.CurrentThread.CurrentUICulture = persianCulture;

                // ✅ ست کردن Globalization برای کنترل‌های دیتایی و فرم‌ها
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false); // false باعث می‌شود کنترل‌ها از Culture استفاده کنند


                // آیکن مشترک
                Icon appIcon = new Icon("Resources/logo1.ico");

                // رویداد ساخت فرم‌ها را هندل کن
                Application.ApplicationExit += (s, e) => appIcon?.Dispose();
                Application.OpenForms.Cast<Form>().ToList().ForEach(f => f.Icon = appIcon);


                // 🔹 تلاش برای اتصال به دیتابیس
                string connStr = ClsConnection.Connectcheck();
                bool canConnect = ClsConnection.TestConnection(connStr);

                // 🔹 اگر اتصال برقرار نبود، فرم تنظیم پسورد را نمایش بده
                if (!canConnect)
                {

                    FrmMsgBox.Show("اتصال به دیتابیس برقرار نشد. لطفاً تنظیمات را بررسی کنید.", "خطا", MsgBoxType.Error, MessageBoxButtons.OK);
                    var frmSql = new FrmSqlPassword();
                    frmSql.ShowDialog();

                    // بعد از ذخیره، مجدداً تلاش کن
                    connStr = ClsConnection.Connectcheck();
                    canConnect = ClsConnection.TestConnection(connStr);

                    if (!canConnect)
                    {
                        FrmMsgBox.Show("هنوز اتصال برقرار نشد. برنامه بسته می‌شود.", "خطا", MsgBoxType.Error, MessageBoxButtons.OK);
                        return;
                    }
                }



                // 👇 EF Warmup در پس‌زمینه
                Task.Run(() =>
                {
                    try
                    {
                        using (var db = new RealEstateContext())
                        {
                            // اجرای یک Query سبک برای کامپایل مدل
                            db.Database.Initialize(false);
                            db.FileType_List.FirstOrDefault();
                        }
                    }
                    catch { }
                });
                if (RealEstateFilingWin.Properties.Settings.Default.IsServer)
                {
                    //RealEstateContext aaaa = new RealEstateContext("Server=menubaan.ir; Database=softenia_amlak;user id=softenia_amlak; password=H@mid4850; Trusted_Connection=False; MultipleActiveResultSets=true;Encrypt=False");
                    //var a = aaaa.OwnerDets_List.FirstOrDefault();


                    RealEstateContext realEstateContext = new RealEstateContext();
                    var settings =realEstateContext.Settings.FirstOrDefault();

                    SyncInformation.Start();
                    if (settings.Active)
                    {
                        SyncService.Start();
                        FileSyncService.Start();
                    }
                }
                FrmLogin frmLogin = new FrmLogin();
                if (frmLogin.ShowDialog() == DialogResult.OK)
                    Application.Run(new FrmMain());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erroooor");
            }
        }
    }
}
