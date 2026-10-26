using Common;
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
    public partial class FrmReminderList : RealEstateFilingWin.Forms.FrmBase
    {
        public FrmReminderList()
        {
            InitializeComponent();
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
            FrmReminder frmReminder=new FrmReminder(null);
            if (frmReminder.ShowDialog() == DialogResult.OK)
            {
                grdUserReminder.DataSource = null;
                grdUserReminder.DataSource = ClsBaseSetting.UserReminderList.OrderBy(x => x.DateReminder).ToList();
            }

        }

        private void FrmReminderList_Load(object sender, EventArgs e)
        {
            grdUserReminder.DataSource = ClsBaseSetting.UserReminderList?.OrderBy(x => x.DateReminder).ToList();
        }

        private void grdUserReminder_DoubleClick(object sender, EventArgs e)
        {
            Guid _reminderid = Guid.Parse(grdUserReminder.CurrentRow.Cells["ID"].Value.ToString());
            FrmReminder frmReminder = new FrmReminder(_reminderid);
            if (frmReminder.ShowDialog() == DialogResult.OK)
            {
                grdUserReminder.DataSource = null;
                grdUserReminder.DataSource = ClsBaseSetting.UserReminderList.OrderBy(x=>x.DateReminder).ToList();
            }

        }
    }
}
