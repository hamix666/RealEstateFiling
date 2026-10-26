using BasicComponent;
using Common;
using DevExpress.XtraEditors.Filtering.Controls;
using RealEstateFiling.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RealEstateFilingWin.Forms
{
    public partial class FrmReminder : RealEstateFilingWin.Forms.FrmBase
    {
        Guid? _remindeid;
        public FrmReminder(Guid? Reminderid)
        {
            InitializeComponent();
            _remindeid = Reminderid;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnnim_Click(object sender, EventArgs e)
        {
            txtTime.Text = DateTime.Now.AddMinutes(30).GetTime();
        }

        private void btnyek_Click(object sender, EventArgs e)
        {
            txtTime.Text = DateTime.Now.AddHours(1).GetTime();
        }

        private void btndow_Click(object sender, EventArgs e)
        {
            txtTime.Text = DateTime.Now.AddHours(2).GetTime();
        }

        private void btnse_Click(object sender, EventArgs e)
        {
            txtTime.Text = DateTime.Now.AddHours(3).GetTime();
        }

        private void btnchahar_Click(object sender, EventArgs e)
        {
            txtTime.Text = DateTime.Now.AddHours(4).GetTime();
        }

        private void txtTime_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtTime.Text.Length < 5)
            {
                FrmMsgBox.Show("فرمت زمان وارد شده نامعتبر است.", "خطا", MsgBoxType.Error);
                txtTime.Text = "  :";
                txtTime.Focus();
                txtTime.SelectAll();
                return;
            }
            if (clsDateStart.Text == "")
            {
                FrmMsgBox.Show("فرمت تاریخ وارد شده نامعتبر است.", "خطا", MsgBoxType.Error);
                clsDateStart.Focus();
                return;
            }
            if (String.IsNullOrEmpty(txtDescription.Text))
            {
                FrmMsgBox.Show("متن یادآوری را وارد کنید", "خطا", MsgBoxType.Error);
                txtDescription.Focus();
                return;
            }

            using (var db = new RealEstateContext())
            {
                TimeSpan timeSpan;
                DateTime dateTime = DateTime.Now;
                dateTime = clsDateStart.Text.ToMiladiDate().Add(TimeSpan.Parse(txtTime.Text));
                if (dateTime <= DateTime.Now)
                {
                    FrmMsgBox.Show("زمان و تاریخ نمیتواند  برای گذشته باشد", "خطا", MsgBoxType.Error);
                    return;
                }
                if (_remindeid != null)
                {
                    var query = db.UserReminder_List.Where(x => x.ID == _remindeid).FirstOrDefault();
                    query.DateReminder = dateTime;
                    query.ShamsiDateReminder = dateTime.ToShamsiDate_Time();
                    query.TextReminder = txtDescription.Text;
                    query.UserID = ClsBaseSetting.UserID;

                }
                else
                {
                    db.UserReminder_List.Add(new Models.MNG.UserReminder()
                    {
                        ID=Guid.NewGuid(),
                        DateReminder = dateTime,
                        ShamsiDateReminder = dateTime.ToShamsiDate_Time(),
                        TextReminder = txtDescription.Text,
                        UserID = ClsBaseSetting.UserID

                    });
                }

                db.SaveChanges();
                FrmMsgBox.Show("باآوری با موفقیت ثبت گردید", "ثبت", MsgBoxType.Info);
                var today = DateTime.Today;

                var _list = db.UserReminder_List
     .Where(x => x.UserID == ClsBaseSetting.UserID )
     .ToList();
                ClsBaseSetting.UserReminderList = _list;
                DialogResult = DialogResult.OK;
            }

        }

        private void txtTime_TypeValidationCompleted(object sender, TypeValidationEventArgs e)
        {
            if (txtTime.Text == "  :")
            {
                return;
            }
            if (e.IsValidInput)
            {
                // 1️⃣ تبدیل ورودی به DateTime
                DateTime timeValue;
                if (DateTime.TryParse(txtTime.Text, out timeValue))
                {
                    // 2️⃣ اعمال عملیات
                    // در اینجا می‌توانید عملیات مورد نظر را انجام دهید
                    // مثلاً:
                    // - ذخیره timeValue در یک متغیر یا پایگاه داده
                    // - نمایش timeValue در یک کنترل دیگر
                    // - انجام محاسبات بر اساس timeValue

                    //MessageBox.Show($"زمان معتبر وارد شده: {timeValue}", "اعتبارسنجی موفق");
                }
                else
                {
                    FrmMsgBox.Show("فرمت زمان وارد شده نامعتبر است.", "خطا", MsgBoxType.Error);
                    txtTime.Text = "  :";
                    txtTime.Focus();
                    txtTime.SelectAll();
                }
            }
            else
            {
                // 3️⃣ نمایش پیام خطا
                FrmMsgBox.Show("فرمت زمان وارد شده نامعتبر است.", "خطا", MsgBoxType.Error);
                txtTime.Text = "  :";
                txtTime.Focus();
                txtTime.SelectAll();
            }
        }

        private void btnDeleteReminder_Click(object sender, EventArgs e)
        {
            if (FrmMsgBox.Show("آیا از حذف اطمینان دارید؟ ", "سوال", MsgBoxType.Question, MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                using (var db = new RealEstateContext())
                {
                    var query = db.UserReminder_List.Where(x => x.ID == _remindeid).FirstOrDefault();
                    db.UserReminder_List.Remove(query);
                    db.SaveChanges();
                    var today = DateTime.Today;

                    var _list = db.UserReminder_List
         .Where(x => x.UserID == ClsBaseSetting.UserID )
         .ToList();
                    ClsBaseSetting.UserReminderList = _list; 
                    DialogResult= DialogResult.OK;
                }
                
            }

        }

        private void FrmReminder_Load(object sender, EventArgs e)
        {
            if (_remindeid != null)
            {
                btnDeleteReminder.Visible = true;
                using (var db=new RealEstateContext())
                {
                    var query=db.UserReminder_List.Where(x=>x.ID== _remindeid).FirstOrDefault();
                    clsDateStart.Text =query.DateReminder.ToShamsiDate();
                    txtTime.Text=query.DateReminder.GetTime();
                    txtDescription.Text = query.TextReminder;
                    
                }
                
            }
        }

        private void btntime_Click(object sender, EventArgs e)
        {
            txtTime.Text = DateTime.Now.GetTime();
        }
    }
}
