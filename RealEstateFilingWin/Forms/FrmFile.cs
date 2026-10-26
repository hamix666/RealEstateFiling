

using Common;
using DevExpress.Utils.DirectXPaint.Svg;
using Guna.UI2.WinForms;
using RealEstateFiling.Services;
using RealEstateFilingWin.DTO;
using RealEstateFilingWin.Models.FIL;
using RealEstateFilingWin.Models.GNR;
using RealEstateFilingWin.Services.FIL;
using RealEstateFilingWin.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace RealEstateFilingWin.Forms
{
    public partial class FrmFile : FrmBase
    {
        public event Action DataSaved;
        public Owners _PEntity = new Owners();
        private BindingList<OwnerDet> _lstOwnerDet = new BindingList<OwnerDet>();
        private long _id = 0;
        public string mobile="";
        private OwnersBL _ownersBL = new OwnersBL();

        public FrmFile(long ID)
        {
            InitializeComponent();
            _id = ID;
        }

        public FrmFile()
        {
            InitializeComponent();
        }

        private async void FrmFile_Load(object sender, EventArgs e)
        {
            grdDet.DataSource = _lstOwnerDet; // BindingList برای grid
            await LoadFormDataAsync();
            await SetUserAccsess();
            if (_PEntity.Ejare)
            {
                lblPrice1.Text = "مبلغ اجاره";
                lblPrice2.Text = "مبلغ رهن";
            }
            else
            {
                lblPrice1.Text = "مبلغ متری";
                lblPrice2.Text = "مبلغ کل";
            }
        }
        private async Task  SetUserAccsess()
        {
            if (ClsBaseSetting.UserID != 1)
            {
                var userAccsesses = ClsBaseSetting.UserIn.UserAccsessList.ToList();
                if (!userAccsesses.Where(x => x.AccessID == 2).Any())
                {
                    rdoPishForosh.Enabled = false;
                    
                }
                
                if (!userAccsesses.Where(x => x.AccessID == 1).Any())
                {
                    rdoForosh.Enabled = false;
                    
                    
                }

                if (!userAccsesses.Where(x => x.AccessID == 3).Any())
                {
                    rdoEjare.Enabled = false;
                   
                }
                if (_id == 0)
                {
                    if(ClsBaseSetting.UserID!=1 && ClsBaseSetting.UserIn.Description_Latin.ToInt32OrDefault() != 0)
                    {
                        if (ClsBaseSetting.UserIn.Description_Latin.ToInt32OrDefault() == 1)
                            rdoForosh.Checked = true;
                        else
                            rdoEjare.Checked = true;
                    }
                    else
                    {
                        
                    }
                }
                //if (rdoForosh.Enabled)
                //{
                //    rdoForosh.Checked=true;
                //}
                //else if (rdoEjare.Enabled)
                //{
                //    rdoEjare.Checked = true;
                //}
                //else
                //{
                //    rdoPishForosh.Checked = true;
                //}
                lblPrice1.Text = rdoEjare.Checked ? "مبلغ اجاره" : "قیمت متری";
                lblPrice2.Text = rdoEjare.Checked ? "مبلغ رهن" : "قیمت کل";
            }
        }

        private async Task LoadFormDataAsync()
        {
            // دیتای lookup ها و owner
            List<FileType> fileTypes;
            List<SanadStatus> sanadStatus;
            List<Views> views;
            List<Models.GNR.Region> regions;
            List<FileStatus> fileStatus;
            Owners owner;

            using (var db = new RealEstateContext())
            {
                db.Configuration.LazyLoadingEnabled = false;
                if (ClsBaseSetting.UserID != 1)
                {
                    var _filetype = ClsBaseSetting.UserIn.UserAccsessList.Where(w => w.AccessID >= 4 && w.AccessID <= 7).Select(x => x.AccessID - 3).ToList();
                    fileTypes = await db.FileType_List.Where(x => _filetype.Contains(x.ID)).AsNoTracking().ToListAsync();
                }
                else
                {
                    fileTypes = await db.FileType_List.AsNoTracking().ToListAsync();
                }
                    sanadStatus = await db.SanadStatus_List.AsNoTracking().ToListAsync();
                views = await db.Views_List.AsNoTracking().ToListAsync();
                regions = await db.Region_List.AsNoTracking().ToListAsync();
                fileStatus = await db.FileStatus_List.AsNoTracking().ToListAsync();

                if (_id != 0)
                {
                    owner = await db.Owners_List
                                    .AsNoTracking()
                                    .Include(o => o.OwnerDetsList.Select(d => d.OwnerDet_Kabinet))
                                    .Include(o => o.OwnerDetsList.Select(d => d.OwnerDet_ServiceWc))
                                    .Include(o => o.OwnerDetsList.Select(d => d.OwnerDet_Kafposh))
                                    .FirstOrDefaultAsync(o => o.ID == _id);
                }
                else owner = null;
            }

            // --- Assign دیتا به UI ---
            cmbFiletypeID.DataSource = fileTypes;
            cmbFiletypeID.DisplayMember = "Name";
            cmbFiletypeID.ValueMember = "ID";
            cmbFiletypeID.SelectedIndex = -1;

            cmbSanadStatusID.DataSource = sanadStatus;
            cmbSanadStatusID.DisplayMember = "Name";
            cmbSanadStatusID.ValueMember = "ID";

            cmbViewID.DataSource = views;
            cmbViewID.DisplayMember = "Name";
            cmbViewID.ValueMember = "ID";

            cmbRegionID.DataSource = regions;
            cmbRegionID.DisplayMember = "Name";
            cmbRegionID.ValueMember = "ID";
            cmbRegionID.SelectedIndex = -1;

            cmbFileStatusID.DataSource = fileStatus;
            cmbFileStatusID.DisplayMember = "Name";
            cmbFileStatusID.ValueMember = "ID";
            cmbFileStatusID.SelectedIndex = -1;

            cmbSenbana.DataSource = new EnumValue().Senbana;
            cmbSenbana.SelectedIndex = -1;

            if (owner != null)
            {
                _PEntity = owner;

                // Add owner details به BindingList یکجا
                _lstOwnerDet.RaiseListChangedEvents = false;
                _lstOwnerDet.Clear();
                foreach (var det in owner.OwnerDetsList)
                    _lstOwnerDet.Add(det);
                _lstOwnerDet.RaiseListChangedEvents = true;
                _lstOwnerDet.ResetBindings();

                grdDet.MoveFirst();

                if (_lstOwnerDet.Count > 0)
                {
                    txtPrice1.Text = _lstOwnerDet[0].Price1.ToString("N0");
                    txtPrice2.Text = _lstOwnerDet[0].Price2.ToString("N0");
                }

                label1.Text = $"مالک : {owner.MName} _____ کد : {owner.Code}";
                EfFormBinder.ModelToControls(owner, this);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_lstOwnerDet.Count == 0)
            {
                FrmMsgBox.Show("اطلاعات مربوط به واحد را تکمیل کنید", "خطا", MsgBoxType.Error);
                return;
            }
            if (Ejbari() == false)
                return;
            

            using (var db = new RealEstateContext())
            {
                var owner = new Owners();
                EfFormBinder.ControlsToModel(owner, this);
                if(txtMasterID.Text.Length>0)
                owner.MasterID = Guid.Parse(txtMasterID.Text);
                //if (owner.ID == 0)
                //{
                //    foreach (var item in _lstOwnerDet)
                //    {
                //        item.OwnerDet_Kabinet = null;
                //        item.OwnerDet_Kafposh = null;
                //        item.OwnerDet_ServiceWc= null;
                //        item.OwnerMasterID = owner.MasterID;
                //    }
                //}
                foreach (var item in _lstOwnerDet)
                {
                    item.OwnerDet_Kabinet = null;
                    item.OwnerDet_Kafposh = null;
                    item.OwnerDet_ServiceWc = null;
                    item.OwnerMasterID = owner.MasterID;
                }
                _lstOwnerDet.ToList().ForEach(x => x.OwnerMasterID = owner.MasterID);
                
                if (owner.ID == 0 || owner.ID == null)
                {
                    owner.Code = _ownersBL.GetLastFreeCode();
                    owner.BranchID = ClsBaseSetting.CurrentBranchID;
                    _lstOwnerDet.ToList().ForEach(x => x.BranchID = ClsBaseSetting.CurrentBranchID);
                }
                else
                {
                    var olde = db.Owners_List.Where(x => x.ID == owner.ID).FirstOrDefault();
                    owner.BranchID = olde.BranchID;
                    _lstOwnerDet.ToList().ForEach(x => x.BranchID = olde.BranchID);
                }
                owner.OwnerDetsList = _lstOwnerDet.ToList();

                EfFormBinder.SaveFull(db, owner);
                var idToRemove = owner.ID.ToString();
                var remove=db.CustomerReadOwner_List.Where(x => x.OwnerID == owner.ID).ToList();
                if(remove !=null && remove.Count() > 0)
                {
                    db.CustomerReadOwner_List.RemoveRange(remove);
                    db.SaveChanges();
                }


//                db.Database.ExecuteSqlCommand(@"
//    UPDATE CUS.Customer
//    SET Description_Latin =
//        LTRIM(RTRIM(
//            REPLACE(
//                REPLACE(
//                    REPLACE(
//                        REPLACE(',' + Description_Latin + ',', ',' + @p0 + ',', ','),
//                    ',,', ','), 
//                ',,', ','), 
//            ',', '')
//        ))
//    WHERE ',' + Description_Latin + ',' LIKE '%,' + @p0 + ',%'
//", idToRemove);


            }

            FrmMsgBox.Show("✅ ذخیره کامل شد (همراه با جزئیات)", "ذخیره", MsgBoxType.Info);
            DataSaved?.Invoke();
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e) => this.Close();
        private void FrmFile_FormClosed(object sender, FormClosedEventArgs e) => EfFormBinder.ClearCacheForForm(this);

        private void rdoPishForosh_CheckedChanged(object sender, EventArgs e)
        {
            txtDatePishForoshTahvil.Enabled = rdoPishForosh.Checked;
            if (!rdoPishForosh.Checked) txtDatePishForoshTahvil.Clear();
        }

        private void rdoEjare_CheckedChanged(object sender, EventArgs e)
        {
            lblPrice1.Text = rdoEjare.Checked ? "مبلغ اجاره" : "قیمت متری";
            lblPrice2.Text = rdoEjare.Checked ? "مبلغ رهن" : "قیمت کل";
        }

        private void btnDetNew_Click(object sender, EventArgs e)
        {
            OwnerDet ownerDet = new OwnerDet { OwnerID = txtID.Text.ToInt64OrDefault() };
            var _type = rdoPishForosh.Checked || rdoForosh.Checked ? true : false;
            int _masahat = 0;
            if (cmbFiletypeID.SelectedValue.ToInt64OrDefault() == 3)
            {
                if (string.IsNullOrEmpty(txtMasahat.Text))
                {
                    FrmMsgBox.Show("مساحت را وارد کنید");
                    return;
                }
                _masahat = txtMasahat.Text.ToInt32OrDefault2();
            }
            FrmFileDet frmFileDet = new FrmFileDet(cmbFiletypeID.Text, true,ownerDet, txtMName.Text, _type,_masahat);
            if (frmFileDet.ShowDialog() == DialogResult.OK)
            {
                frmFileDet._ownerDet.CreationDate = DateTime.Now;
                _lstOwnerDet.Add(frmFileDet._ownerDet);
            }
        }

        private void grdDet_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                var ownerDet = grdDet.CurrentRow?.DataRow as OwnerDet;
                if (ownerDet != null)
                {
                    txtPrice1.Text = ownerDet.Price1.ToString("N0");
                    txtPrice2.Text = ownerDet.Price2.ToString("N0");
                }
            }
        }

        private void btnDetEdit_Click(object sender, EventArgs e)
        {
            if (grdDet.CurrentRow?.DataRow == null)
            {
                FrmMsgBox.Show("برای انجام عملیات یک ردیف را انتخاب کنید.", "هشدار", MsgBoxType.Info);
                return;
            }
            var _type = rdoPishForosh.Checked || rdoForosh.Checked ? true : false;
            var ownerDet = (OwnerDet)grdDet.CurrentRow.DataRow;
            ownerDet.OwnerID = txtID.Text.ToInt64OrDefault();
            int _masahat = 0;
            if (cmbFiletypeID.SelectedValue.ToInt64OrDefault() == 3)
            {
                if (string.IsNullOrEmpty(txtMasahat.Text))
                {
                    FrmMsgBox.Show("مساحت را وارد کنید");
                    return;
                }
                _masahat = txtMasahat.Text.ToInt32OrDefault2();
            }
            FrmFileDet frmFileDet = new FrmFileDet(cmbFiletypeID.Text, false,ownerDet, txtMName.Text,_type,_masahat);
            if (frmFileDet.ShowDialog() == DialogResult.OK)
            {
                int index = _lstOwnerDet.IndexOf(ownerDet);
                if (index >= 0) _lstOwnerDet[index] = frmFileDet._ownerDet;
                else _lstOwnerDet.Add(frmFileDet._ownerDet);
            }
        }

        private void btnDetDelete_Click(object sender, EventArgs e)
        {
            if (grdDet.CurrentRow?.DataRow == null)
            {
                FrmMsgBox.Show("لطفاً یک ردیف برای حذف انتخاب کنید.", "هشدار", MsgBoxType.Info);
                return;
            }

            if (FrmMsgBox.Show("آیا از حذف این مورد مطمئن هستید؟", "تأیید حذف", MsgBoxType.Question, MessageBoxButtons.YesNo) == DialogResult.No)
                return;

            var selectedDet = (OwnerDet)grdDet.CurrentRow.DataRow;
            _lstOwnerDet.Remove(selectedDet);
        }

        private void btnClose_Click_1(object sender, EventArgs e)
        {
            this.Close();
            
        }

        private void btnMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            
        }

        private void btnMelkMoshabeh_Click(object sender, EventArgs e)
        {
            using (var db = new RealEstateContext())
            {
                var a = db.Owners_List.Where(x => x.MTel1 == txtMTel1.Text || x.MTel2 == txtMTel2.Text).ToList();
                if (db.Owners_List.Where(x => x.MTel1==txtMTel1.Text || x.MTel2==txtMTel2.Text).Any())
                {
                    if(FrmMsgBox.Show("این شماره موبایل قبلا ثبت شده است آیا نمایش داده شود؟.", "هشدار", MsgBoxType.Question, MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        mobile = a.Select(x=>x.MTel1).FirstOrDefault();
                        DataSaved?.Invoke();
                        this.btnMin.PerformClick();
                    }
                    
                }
            }
            
            
        }
        int counterror = 0;
        public bool Ejbari()
        {
            errorEjbari.Clear();
            
            counterror = 0;
            CheckControl(cmbFiletypeID);
            CheckControl(cmbRegionID);
            CheckControl(txtMName);
            CheckControl(txtMTel1);
            CheckControl(txtAddress);
            CheckControl(txtPlak);
            CheckControl(cmbSenbana);
            CheckControl(cmbFileStatusID);

            // فرض کنید نام پنل شما 'panel1' است
            var jahatcheckboxes = paneljahat.Controls.OfType<Guna2CheckBox>().ToList();
            var joziyatcheckboxes = paneljoziyat.Controls.OfType<Guna2CheckBox>().ToList();

            int jahatcheckedCount = jahatcheckboxes.Count(cb => cb.Checked);

            if (jahatcheckedCount == 0)
            {
                errorEjbari.SetError(paneljahat, "یک مورد انتخاب شود");
                errorEjbari.SetIconAlignment(paneljahat, ErrorIconAlignment.MiddleLeft);
                paneljahat.BackColor = Color.LightSalmon;
                counterror++;
            }
            else
            {
                paneljahat.BackColor = Color.Transparent;
            }

            int joziyatcheckedCount = joziyatcheckboxes.Count(cb => cb.Checked);

            if (joziyatcheckedCount < 2)
            {
                errorEjbari.SetError(paneljoziyat, "دو مورد انتخاب شود");
                errorEjbari.SetIconAlignment(paneljoziyat, ErrorIconAlignment.MiddleLeft);
                paneljoziyat.BackColor = Color.LightSalmon;
                counterror++;
            }
            else
            {
                paneljoziyat.BackColor = Color.Transparent;
            }
            if (counterror != 0)
                return false;
            else
                return true;
        }

        public void CheckControl(Control _control)
        {
            if(_control is Guna2ComboBox)
            {
                var _c = (Guna2ComboBox)_control;
                if (_c.SelectedIndex == -1)
                {
                    errorEjbari.SetError(_c, "اجباری");
                    _c.BackColor = Color.DarkRed;
                    counterror++;
                }
                else
                    _c.BackColor = Color.Gainsboro;


            }
            else if(_control is Guna2TextBox)
            {
                var _c = (Guna2TextBox)_control;
                if (string.IsNullOrEmpty(_c.Text))
                {
                    errorEjbari.SetError(_c, "اجباری");
                    _c.BackColor = Color.DarkRed;
                    counterror++;
                }
                else if(_c.Name== "txtMTel1" && _c.Text.Length != 11)
                {
                    errorEjbari.SetError(_c, "حتما باید 11 رقم باشد");
                    _c.BackColor = Color.DarkRed;
                    counterror++;
                }
               
                else
                    _c.BackColor = Color.Gainsboro;
            }
        }
    }
}
