using Common;
using Microsoft.Web.WebView2.WinForms;
using RealEstateFilingWin.Services.MNG;
using RealEstateFilingWin.Utility;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;


namespace RealEstateFilingWin.Forms
{
    public partial class LoginForm : Form
    {
        private WebView2 webView;
        private UserBL _userBL=new UserBL();
        public LoginForm()
        {
            InitializeComponent();
            InitializeWebView();
        }

        private async void InitializeWebView()
        {
            webView = new WebView2 { Dock = DockStyle.Fill };
            this.Controls.Add(webView);

            await webView.EnsureCoreWebView2Async();
            webView.DefaultBackgroundColor = System.Drawing.Color.FromArgb(26, 26, 46); // رنگ پس‌زمینه

            string htmlPath = Path.Combine(Application.StartupPath, "login", "index.html");
            webView.Source = new Uri(htmlPath);
            webView.CoreWebView2.Settings.IsStatusBarEnabled = false;

            // دریافت پیام JS
            webView.WebMessageReceived += (s, e) =>
            {
                try
                {
                    string json = e.TryGetWebMessageAsString();
                    using (var doc = System.Text.Json.JsonDocument.Parse(json))
                    {
                        var root = doc.RootElement;
                        string action = root.GetProperty("action").GetString();
                        string user = root.GetProperty("username").GetString();
                        string pass = root.GetProperty("password").GetString();
                        

                        if (action == "login")
                        {
                            
                            var _user = _userBL.FindByCondition(x => x.Username == user).FirstOrDefault();
                            if (_user == null)
                            {
                                FrmMsgBox.Show("'نام کاربری یا رمز عبور اشتباه است!", "❌ خطا ", MsgBoxType.Error);
                                //webView.CoreWebView2.PostWebMessageAsString("login_error");
                            }
                            else
                            {
                                if(_user.Username==user && BCrypt.Net.BCrypt.Verify(pass, _user.Password))
                                {
                                    ClsBaseSetting.UserID=_user.ID;
                                    ClsBaseSetting.UserIn = _user;
                                    ClsBaseSetting.timein = DateTime.Now.ToString("HH:mm");
                                    DialogResult = DialogResult.OK;
                                }
                                else
                                {
                                    FrmMsgBox.Show("'نام کاربری یا رمز عبور اشتباه است!", "❌ خطا ", MsgBoxType.Error);
                                    //webView.CoreWebView2.PostWebMessageAsString("login_error");
                                }
                            }

                            //// 🔹 بررسی کاربر
                            //if (user == "admin" && pass == "1234")
                            //    webView.CoreWebView2.PostWebMessageAsString("login_success");
                            //else
                            //    webView.CoreWebView2.PostWebMessageAsString("login_error");
                        }
                        else if (action == "register")
                        {
                            string mobile = root.GetProperty("mobile").GetString();
                            string name = root.GetProperty("name").GetString();
                            var a = BCrypt.Net.BCrypt.HashPassword(pass);
                            var _user = _userBL.FindByCondition(x => x.Username == user || x.Mobile==mobile).FirstOrDefault();
                            if(_user != null)
                            {
                                FrmMsgBox.Show("نام کاربری یا شماره موبایل تکراری  میباشد", "خطا در ثبت نام", MsgBoxType.Error);
                                //webView.CoreWebView2.PostWebMessageAsString("register_exists");
                            }
                            else
                            {
                                Models.MNG.User model = new Models.MNG.User()
                                {
                                    UserIDCreator = 1,
                                    CreationDate = DateTime.Now,
                                    Username = user,
                                    Password = a,
                                    Mobile = mobile,
                                    Name = name
                                } ;
                                var result = _userBL.SaveUser(model);
                                //var result= _userBL.Create(new Models.MNG.User()
                                //{
                                //    UserIDCreator = 1,
                                //    CreationDate = DateTime.Now,
                                //    Username = user,
                                //    Password = a,
                                //    Mobile=mobile,
                                //    Name=name
                                //}) ;

                                if(result.Status==OperationResult.OpreationResultStatus.Success)
                                {
                                    FrmMsgBox.Show("اکنون می‌توانید وارد شوید.", "🎉 ثبت نام موفق", MsgBoxType.Info);
                                    //webView.CoreWebView2.PostWebMessageAsString("register_success");
                                    
                                }
                                else
                                {
                                    FrmMsgBox.Show("ثبت نام با خطا مواجه شده "+result.Message+"", "خطا در ثبت نام", MsgBoxType.Error);

                                    //webView.CoreWebView2.PostWebMessageAsString("register_error");
                                }
                            }
                            //// 🔹 شبیه‌سازی ذخیره کاربر
                            //if (user == "admin")
                            //    webView.CoreWebView2.PostWebMessageAsString("register_exists");
                            //else
                            //    webView.CoreWebView2.PostWebMessageAsString("register_success");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            };


        }


        private void WebView_WebMessageReceived(object sender, Microsoft.Web.WebView2.Core.CoreWebView2WebMessageReceivedEventArgs e)
        {
            try
            {
                using (System.Text.Json.JsonDocument doc = System.Text.Json.JsonDocument.Parse(e.WebMessageAsJson))
                {
                    var root = doc.RootElement;

                    string user = root.GetProperty("username").GetString();
                    string pass = root.GetProperty("password").GetString();

                    if (user == "admin" && pass == "1234")
                    {
                        webView.CoreWebView2.ExecuteScriptAsync("Swal.fire('✅ ورود موفق', 'به برنامه خوش آمدید!', 'success');");
                        DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        webView.CoreWebView2.ExecuteScriptAsync("Swal.fire('❌ خطا', 'نام کاربری یا رمز عبور اشتباه است!', 'error');");
                        MessageBox.Show("❌ خطا', 'نام کاربری یا رمز عبور اشتباه است!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("خطا در پردازش پیام: " + ex.Message);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
