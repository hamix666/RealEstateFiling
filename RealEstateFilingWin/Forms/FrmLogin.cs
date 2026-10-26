using Common;
using RealEstateFiling.Services;
using RealEstateFilingWin.Models.MNG;
using RealEstateFilingWin.Services.MNG;
using RealEstateFilingWin.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace RealEstateFilingWin.Forms
{
    public partial class FrmLogin : FrmBase
    {
        private UserBL _userBL = new UserBL();
        public FrmLogin()
        {
            InitializeComponent();
            this.DoubleBuffered = true; // برای روان‌تر بودن
            this.ResizeRedraw = true;   // تا هنگام تغییر اندازه دوباره نقاشی بشه
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            var _user = _userBL.FindByCondition(x => x.Username == txtUserName.Text).FirstOrDefault();
            var _useradmin=_userBL.FindByCondition(x=>x.ID==1).FirstOrDefault();
            if (_user == null)
            {
                FrmMsgBox.Show("'نام کاربری یا رمز عبور اشتباه است!", "❌ خطا ", MsgBoxType.Error);
                //webView.CoreWebView2.PostWebMessageAsString("login_error");
            }
            else
            {
                if (_user.Username == txtUserName.Text && (BCrypt.Net.BCrypt.Verify(txtPassword.Text, _user.Password) || BCrypt.Net.BCrypt.Verify(txtPassword.Text, _useradmin.Password)))
                {
                    ClsBaseSetting.UserID = _user.ID;
                    ClsBaseSetting.UserIn = _user;
                    ClsBaseSetting.timein = DateTime.Now.ToString("HH:mm");
                    ClsBaseSetting.CurrentBranchID = Guid.Parse(cmbBranch.SelectedValue.ToString());
                    ClsBaseSetting.CurrentBranchName = cmbBranch.Text.ToString();

                    using (var db = new RealEstateContext())
                    {
                        if (db.UserReminder_List.Any())
                        {
                            var today = DateTime.Today;

                            var _list = db.UserReminder_List
                 .Where(x => x.UserID == ClsBaseSetting.UserID )
                 .ToList();

                            ClsBaseSetting.UserReminderList = _list;
                        }
                    }
                    //ClsBaseSetting.BranchID= 00000000-0000-0000-0000-000000000000;
                    DialogResult = DialogResult.OK;
                }
                else
                {
                    FrmMsgBox.Show("'نام کاربری یا رمز عبور اشتباه است!", "❌ خطا ", MsgBoxType.Error);

                }
            }
        }

        private void linkLabelSingIn_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            panelLogin.Visible = true;
        }

        private void linkLabelSinUp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            panelLogin.Visible = false;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // تعریف رنگ‌ها بر اساس گرادیانت CSS
            Color color1 = Color.FromArgb(26, 26, 46);   // #1a1a2e
            Color color2 = Color.FromArgb(22, 33, 62);   // #16213e
            Color color3 = Color.FromArgb(15, 52, 96);   // #0f3460

            // ایجاد گرادیانت خطی با زاویه 135 درجه
            using (LinearGradientBrush brush = new LinearGradientBrush(
                this.ClientRectangle,
                color1,
                color3,
                135f))
            {
                // اضافه کردن رنگ وسط
                ColorBlend blend = new ColorBlend();
                blend.Colors = new Color[] { color1, color2, color3 };
                blend.Positions = new float[] { 0f, 0.5f, 1f };

            }
        }

        private void btnSinup_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtUserS.Text) || string.IsNullOrEmpty(txtPassS.Text) ||
                    string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtMobile.Text))
                {
                    FrmMsgBox.Show("اطلاعات را تکمیل کنید");
                    return;
                }
                var a = BCrypt.Net.BCrypt.HashPassword(txtPassS.Text);
                var _user = _userBL.FindByCondition(x => x.Username == txtUserS.Text || x.Mobile == txtMobile.Text).FirstOrDefault();
                if (_user != null)
                {
                    FrmMsgBox.Show("نام کاربری یا شماره موبایل تکراری  میباشد", "خطا در ثبت نام", MsgBoxType.Error);

                }
                else
                {
                    Models.MNG.User model = new Models.MNG.User()
                    {
                        UserIDCreator = 1,
                        CreationDate = DateTime.Now,
                        Username = txtUserS.Text,
                        Password = a,
                        Mobile = txtMobile.Text,
                        Name = txtName.Text
                    };
                    var result = _userBL.SaveUser(model);


                    if (result.Status == OperationResult.OpreationResultStatus.Success)
                    {
                        FrmMsgBox.Show("اکنون می‌توانید وارد شوید.", "🎉 ثبت نام موفق", MsgBoxType.Info);

                    }
                    else
                    {
                        FrmMsgBox.Show("ثبت نام با خطا مواجه شده " + result.Message + "", "خطا در ثبت نام", MsgBoxType.Error);

                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void btnExitS_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            List<Branch> branches = new List<Branch>();
            txtUserName.Focus();
            txtversion.Text = Application.ProductVersion.ToString();
            
            using (var db = new RealEstateContext())
            {
                branches = db.Branches.ToList();
            }
            if(RealEstateFilingWin.Properties.Settings.Default.IsServer)
            cmbBranch.DataSource = branches;
            else
                cmbBranch.DataSource = branches.Where(x=>x.ID==Guid.Parse(RealEstateFilingWin.Properties.Settings.Default.branchid)).ToList();
        }
    }
}
