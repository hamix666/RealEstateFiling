using FluentFTP;
using RealEstateFiling.Services;
using RealEstateFilingWin.Models.FIL;
using RealEstateFilingWin.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FluentFTP;
using RealEstateFilingWin.Services.Sync;
using System.Windows.Forms;

namespace RealEstateFilingWin.Forms
{
    public partial class FrmSetting : FrmBase
    {
        bool _isEdit = false;
        bool _isEditBranch = false;
        long _IdType;
        Guid _IdBranch;
        string _connection;
        public FrmSetting(string _str)
        {
            InitializeComponent();
            _connection = _str;
        }

        private void grdType_MouseClick(object sender, MouseEventArgs e)
        {
            _isEdit = false;
            _IdType = grdType.CurrentRow.Cells["ID"].Value.ToInt64OrDefault();
            txtName.Text = grdType.CurrentRow.Cells["Name"].Value.ToStringOrDefult();
            _isEdit = true;
        }

        private void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtName.Clear();
            using (var db = new RealEstateContext(_connection))
            {
                _isEdit = false;
                switch (cmbType.SelectedIndex)
                {
                    case 0:

                        grdType.DataSource = db.Kabinet_List.OrderBy(o => o.ID).ToList(); break;
                    case 1:
                        grdType.DataSource = db.ServiceWc_List.OrderBy(o => o.ID).ToList(); break;
                    case 2:
                        grdType.DataSource = db.Kafposh_List.OrderBy(o => o.ID).ToList(); break;
                    case 3:
                        grdType.DataSource = db.Region_List.OrderBy(o => o.ID).ToList(); break;
                    default:
                        break;
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            _isEdit = false;
            _IdType = 0;
            txtName.Clear();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            if (txtName.Name?.Length == 0)
            {
                FrmMsgBox.Show("نام را وارد کنید");
                return;
            }
            using (var db = new RealEstateContext(_connection))
            {
                if (_isEdit)
                {
                    if (cmbType.SelectedIndex == 0)
                    {
                        var m = db.Kabinet_List.Where(x => x.ID == _IdType).FirstOrDefault();
                        m.Name = txtName.Text;

                    }
                    else if (cmbType.SelectedIndex == 1)
                    {
                        var m = db.ServiceWc_List.Where(x => x.ID == _IdType).FirstOrDefault();
                        m.Name = txtName.Text;
                    }
                    else if (cmbType.SelectedIndex == 2)
                    {
                        var m = db.Kafposh_List.Where(x => x.ID == _IdType).FirstOrDefault();
                        m.Name = txtName.Text;
                    }
                    else if (cmbType.SelectedIndex == 3)
                    {
                        var m = db.Region_List.Where(x => x.ID == _IdType).FirstOrDefault();
                        m.Name = txtName.Text;
                    }
                }
                else
                {
                    if (cmbType.SelectedIndex == 0)
                    {
                        db.Kabinet_List.Add(new Models.GNR.Kabinet()
                        {
                            Name = txtName.Text,
                        });

                    }
                    else if (cmbType.SelectedIndex == 1)
                    {
                        db.ServiceWc_List.Add(new Models.GNR.ServiceWc()
                        {
                            Name = txtName.Text,
                        });
                    }
                    else if (cmbType.SelectedIndex == 2)
                    {
                        db.Kafposh_List.Add(new Models.GNR.Kafposh()
                        {
                            Name = txtName.Text,
                        });
                    }
                    else if (cmbType.SelectedIndex == 3)
                    {
                        db.Region_List.Add(new Models.GNR.Region()
                        {
                            Name = txtName.Text,
                        });
                    }
                }
                db.SaveChanges();
                cmbType_SelectedIndexChanged(sender, e);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (_IdType == 0)
            {
                FrmMsgBox.Show("یک ردیف از جدول را انتخاب کنید");
                return;
            }

            string _err = "";
            using (var db = new RealEstateContext(_connection))
            {
                OwnerDet ownerDet = new OwnerDet();
                switch (cmbType.SelectedIndex)
                {
                    case 0:
                        ownerDet = db.OwnerDets_List.Include(w => w.OwnerDet_Kabinet).Where(x => x.KabinetID == _IdType).FirstOrDefault();
                        if (ownerDet != null)
                        {
                            FrmMsgBox.Show("امکان حذف کردن این مورد وجود ندارد چون استفاده شده است");
                            return;
                        }
                        db.Kabinet_List.Remove(db.Kabinet_List.Where(x => x.ID == _IdType).FirstOrDefault());
                        db.SaveChanges();
                        break;
                    case 1:
                        ownerDet = db.OwnerDets_List.Include(w => w.OwnerDet_ServiceWc).Where(x => x.ServiceWcID == _IdType).FirstOrDefault();
                        if (ownerDet != null)
                        {
                            FrmMsgBox.Show("امکان حذف کردن این مورد وجود ندارد چون استفاده شده است");
                            return;
                        }
                        db.ServiceWc_List.Remove(db.ServiceWc_List.Where(x => x.ID == _IdType).FirstOrDefault());
                        db.SaveChanges(); break;
                    case 2:
                        ownerDet = db.OwnerDets_List.Include(w => w.OwnerDet_Kafposh).Where(x => x.KafposhID == _IdType).FirstOrDefault();
                        if (ownerDet != null)
                        {
                            FrmMsgBox.Show("امکان حذف کردن این مورد وجود ندارد چون استفاده شده است");
                            return;
                        }
                        db.Kafposh_List.Remove(db.Kafposh_List.Where(x => x.ID == _IdType).FirstOrDefault());
                        db.SaveChanges(); break;
                    case 3:
                        var owners = db.Owners_List.Include(w => w.Owner_Region).Where(x => x.RegionID == _IdType).FirstOrDefault();
                        if (owners != null)
                        {
                            FrmMsgBox.Show("امکان حذف کردن این مورد وجود ندارد چون استفاده شده است");
                            return;
                        }
                        db.Region_List.Remove(db.Region_List.Where(x => x.ID == _IdType).FirstOrDefault());
                        db.SaveChanges(); break;
                    default:
                        break;
                }
            }
            SyncInformation.Start();
            cmbType_SelectedIndexChanged(sender, e);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnSaveSetting_Click(object sender, EventArgs e)
        {
            using (var db = new RealEstateContext(_connection))
            {
                var model = db.Users_List.Where(x => x.ID == 1).FirstOrDefault();
                model.Description_Latin = txtUrlFile.Text;
                var _setting = db.Settings.FirstOrDefault();
                _setting.ftpServer = txtftpServer.Text;
                _setting.ftpPort = txtftpPort.Text.ToInt32OrDefault();
                _setting.ftpUser = txtftpUser.Text;
                _setting.ftpPass = txtftpPass.Text;
                _setting.ftpRootPath = txtftpRootPath.Text;
                _setting.masterConnectionString = txtmasterConnectionString.Text;
                _setting.IntervalFileUpdate = txtIntervalFileUpdate.Text.ToInt32OrDefault();
                _setting.IntervalInfoUpdate = txtIntervalInfoUpdate.Text.ToInt32OrDefault();
                _setting.IntervalOwnerUpdate = txtIntervalOwnerUpdate.Text.ToInt32OrDefault();
                if(swichactive.Checked)
                    _setting.Active=true;
                else
                    _setting.Active=false;
                db.SaveChanges();
                FrmMsgBox.Show("دخیره شد");
            }
        }

        private void FrmSetting_Load(object sender, EventArgs e)
        {
            using (var db = new RealEstateContext(_connection))
            {
                var model = db.Users_List.Where(x => x.ID == 1).FirstOrDefault();
                var _setting = db.Settings.FirstOrDefault();
                txtftpServer.Text = _setting.ftpServer;
                txtftpPort.Text = _setting.ftpPort.ToString();
                txtftpUser.Text = _setting.ftpUser;
                txtftpPass.Text = _setting.ftpPass;
                txtftpRootPath.Text = _setting.ftpRootPath;
                txtmasterConnectionString.Text = _setting.masterConnectionString;
                txtIntervalFileUpdate.Text = _setting.IntervalFileUpdate.ToString();
                txtIntervalInfoUpdate.Text = _setting.IntervalInfoUpdate.ToString();
                txtIntervalOwnerUpdate.Text = _setting.IntervalOwnerUpdate.ToString();
                txtUrlFile.Text = model.Description_Latin;
                grdBranch.DataSource = db.Branches.ToList();
                if (_setting.Active)
                {
                    swichactive.Checked = true;
                    
                }
                else 
                {
                    swichactive.Checked = false;
                    
                }
            }
        }

        private void btncheckFtp_Click(object sender, EventArgs e)
        {
            try
            {


                using (var client = new FtpClient(txtftpServer.Text, txtftpUser.Text, txtftpPass.Text, txtftpPort.Text.ToInt32OrDefault()))
                {
                    client.ValidateCertificate += (control, ee) => ee.Accept = true;
                    client.Connect();

                    if (client.IsConnected)
                    {


                        // بررسی وجود مسیر Owners
                        if (client.DirectoryExists(txtftpRootPath.Text))
                        {
                            var listings = client.GetListing(txtftpRootPath.Text);

                        }
                        else
                        {

                            client.CreateDirectory(txtftpRootPath.Text);

                        }

                        client.Disconnect();
                        MessageBox.Show("ارتباط با موفقیت انجام شد");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ارتباط با خطا مواجه شد \n\n " + ex.Message);
            }
        }

        private void btncheckConnectionSql_Click(object sender, EventArgs e)
        {
            using (var db = new RealEstateContext(txtmasterConnectionString.Text))
            {
                try
                {
                    MessageBox.Show("ارتباط با دیتابیس آنلاین بدون مشکل برقرار است ✅");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ارتباط با دیتابیس آنلاین مشکل وجود دارد❌");

                }
            }
        }

        private void btnaddShobe_Click(object sender, EventArgs e)
        {
            _isEditBranch = false;
            _IdBranch = Guid.NewGuid();
            txtNameBranch.Clear();
            txtlocalRoot.Clear();
            txtbranchid.Clear();
            txtCode.Clear();
        }

        private void btnSaveShobe_Click(object sender, EventArgs e)
        {

            if (txtNameBranch.Text?.Length == 0)
            {
                FrmMsgBox.Show("نام شعبه را وارد کنید");
                return;
            }
            if (txtCode.Text?.Length == 0)
            {
                FrmMsgBox.Show("کد شعبه را وارد کنید");
                return;
            }
            using (var db = new RealEstateContext(_connection))
            {
                int _code = txtCode.Text.ToInt32OrDefault();
                var exist = db.Branches.Where(x => x.Code == _code).FirstOrDefault();
                if (_isEditBranch)
                {
                    var _data=db.Branches.Where(x => x.ID == _IdBranch).FirstOrDefault();

                    if(exist!=null && exist.ID!=_IdBranch)
                    {
                        FrmMsgBox.Show("کد شعبه تکراری میباشد");
                        return;
                    }
                    

                    _data.Name = txtNameBranch.Text;
                    _data.localRoot = txtlocalRoot.Text;
                    _data.Code = txtCode.Text.ToInt32OrDefault();

                }
                else
                {
                    if (exist != null )
                    {
                        FrmMsgBox.Show("کد شعبه تکراری میباشد");
                        return;
                    }
                    db.Branches.Add(new Models.MNG.Branch()
                    {
                        ID = Guid.NewGuid(),
                        IsActive = true,
                        localRoot=txtlocalRoot.Text,
                        Name = txtNameBranch.Text,
                        Code=txtCode.Text.ToInt32OrDefault()
                    }) ;
                }
                db.SaveChanges();
                grdBranch.DataSource = null;
                grdBranch.DataSource = db.Branches.ToList();
            }
        }

        private void grdBranch_MouseClick(object sender, MouseEventArgs e)
        {
            if (grdBranch.RecordCount == 0) return;
            _isEditBranch = false;
            _IdBranch = Guid.Parse(grdBranch.CurrentRow.Cells["ID"].Value.ToString());
            txtNameBranch.Text = grdBranch.CurrentRow.Cells["Name"].Value.ToStringOrDefult();
            txtlocalRoot.Text = grdBranch.CurrentRow.Cells["localRoot"].Value.ToStringOrDefult();
            txtbranchid.Text = grdBranch.CurrentRow.Cells["ID"].Value.ToString();
            txtCode.Text = grdBranch.CurrentRow.Cells["Code"].Value?.ToString();
            _isEditBranch = true;
        }

        private void FrmSetting_FormClosing(object sender, FormClosingEventArgs e)
        {
            SyncInformation.Start();
        }

        private void swichactive_CheckedChanged(object sender, EventArgs e)
        {
            if (swichactive.Checked)
            {
               
                lblActive.Text = "سیستم بروزرسانی فعال هست";
                lblActive.ForeColor = Color.DarkGreen;
            }
            else
            {
                
                lblActive.Text = "سیستم بروزرسانی غیر فعال هست";
                lblActive.ForeColor = Color.DarkRed;
            }
        }

        private void txtbranchid_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            txtbranchid.SelectAll();
            // کپی متن در کلیپ‌بورد
            Clipboard.SetText(txtbranchid.Text);
        }
    }
}
