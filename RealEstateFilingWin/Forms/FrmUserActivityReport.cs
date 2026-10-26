using Common;
using RealEstateFiling.Services;
using RealEstateFilingWin.DTO;
using RealEstateFilingWin.Utility;
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
    public partial class FrmUserActivityReport : FrmBase
    {
        public FrmUserActivityReport()
        {
            InitializeComponent();
        }

        private void grdCustomerSearch_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var _useractivityid = grdUserActivityReport.CurrentRow.Cells["ID"].Value.ToInt64OrDefault();
            FrmUserActivity frmUserActivity=new FrmUserActivity(_useractivityid);
            if(frmUserActivity.ShowDialog() == DialogResult.OK)
            {
                FillData();
            }
        }

        private void FrmUserActivityReport_Load(object sender, EventArgs e)
        {
            txtStartDate.Text=DateTime.Now.ToShamsiDate();
            txtEndDate.Text=DateTime.Now.ToShamsiDate();
            using (var db=new RealEstateContext())
            {
               
                var userlist = db.Users_List.ToList();
                cmbUser.DataSource = userlist;
                cmbUser.DisplayMember = "Name";
                cmbUser.ValueMember = "ID";
                if (ClsBaseSetting.UserID != 1)
                {
                    cmbUser.SelectedValue = ClsBaseSetting.UserID;
                    cmbUser.Enabled = false;
                }
            }
            FillData();
        }
        public void FillData()
        {
            var userId = cmbUser.SelectedValue.ToInt64OrDefault();
            var name = txtName.Text?.Trim();
            var subject = txtSubject.Text?.Trim();
            var des = txtDes.Text?.Trim();
            var startDate = txtStartDate.Text.ToMiladiDate().TimeStartWith0000();
            var endDate = txtEndDate.Text.ToMiladiDate().TimeEndWith2359();

            using (var db = new RealEstateContext())
            {
                var query = db.UserActivity_List.AsQueryable();

                if (userId > 0)
                    query = query.Where(x => x.UserIDCreator == userId);

                if (!string.IsNullOrWhiteSpace(name))
                    query = query.Where(x => x.NameCustomer.Contains(name));

                if (!string.IsNullOrWhiteSpace(subject))
                    query = query.Where(x => x.Subject.Contains(subject));

                if (!string.IsNullOrWhiteSpace(des))
                    query = query.Where(x => x.Description.Contains(des));

                if (startDate != null)
                    query = query.Where(x => x.CreationDate >= startDate);

                if (endDate != null)
                    query = query.Where(x => x.CreationDate <= endDate);

                grdUserActivityReport.DataSource = query.ToList().OrderByDescending(d => d.CreationDate).Select(x => new UserActivityDTO()
                {
                    CreationDate = x.CreationDate.ToShamsiDate(),
                    NameCustomer = x.NameCustomer,
                    Subject = x.Subject,
                    ID = x.ID,
                    Description = x.Description
                }).ToList();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            FrmUserActivity frmUserActivity = new FrmUserActivity();
            if(frmUserActivity.ShowDialog() == DialogResult.OK)
            {
                FillData();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            FillData();
        }

        private void btnretrundatestart_Click(object sender, EventArgs e)
        {
            txtStartDate.Text = DateTime.Now.ToShamsiDate();
            
        }

        private void btnretrundateend_Click(object sender, EventArgs e)
        {
            txtEndDate.Text = DateTime.Now.ToShamsiDate();
        }
    }
}
