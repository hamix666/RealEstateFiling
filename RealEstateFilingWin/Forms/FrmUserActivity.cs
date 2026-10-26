using BasicComponent;
using Common;
using RealEstateFiling.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RealEstateFilingWin.Forms
{
    public partial class FrmUserActivity : RealEstateFilingWin.Forms.FrmBase
    {
        long _UserActivityID=0;
        public FrmUserActivity()
        {
            InitializeComponent();
            txtDate.Text = DateTime.Now.ToShamsiDate();
        }
        public FrmUserActivity(long userActivityID)
        {
                InitializeComponent();
            _UserActivityID = userActivityID;
            
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text) && string.IsNullOrEmpty(txtSubject.Text) && string.IsNullOrEmpty(txtDescription.Text))
            {
                return;
            }
            using (var db = new RealEstateContext())
            {
                if (_UserActivityID==0)
                {
                    db.UserActivity_List.Add(new Models.MNG.UserActivity()
                    {
                        CreationDate = DateTime.Now,
                        Description = txtDescription.Text,
                        NameCustomer = txtName.Text,
                        Subject = txtSubject.Text,
                        UserIDCreator = ClsBaseSetting.UserID
                    });

                }
                else
                {
                    var query = db.UserActivity_List.Find(_UserActivityID);
                    query.NameCustomer = txtName.Text;
                    query.Description= txtDescription.Text;
                    query.Subject=txtSubject.Text;
                    query.ModificationDate= DateTime.Now;
                    query.UserIDModifier = ClsBaseSetting.UserID;
                }
                db.SaveChanges();
                DialogResult = DialogResult.OK;
            }

        }

        private void FrmUserActivity_Load(object sender, EventArgs e)
        {
            if (_UserActivityID != 0)
            {
                using (var db=new RealEstateContext())
                {
                    var query = db.UserActivity_List.Where(x => x.ID == _UserActivityID).FirstOrDefault();
                    if (query.CreationDate.Date != DateTime.Now.Date)
                    {
                        btnSave.Enabled = false;
                    }
                    txtName.Text = query.NameCustomer;
                    txtSubject.Text = query.Subject;
                    txtDescription.Text = query.Description;
                    txtDate.Text = query.CreationDate.ToShamsiDate();
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
