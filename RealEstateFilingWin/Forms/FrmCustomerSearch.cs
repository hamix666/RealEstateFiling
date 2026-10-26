using Common;
using Newtonsoft.Json;
using RealEstateFiling.Services;
using RealEstateFilingWin.DTO;
using RealEstateFilingWin.Models.CUS;
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
    public partial class FrmCustomerSearch : FrmBase
    {
        long _ID = 0;
        public long ID { get; }
        bool _IsEdit = false;
        public FileSearchFilterDto _FileSearchFilterDto { get; }
        public FrmCustomerSearch(FileSearchFilterDto fileSearchFilterDto)
        {
            InitializeComponent();
            _FileSearchFilterDto = fileSearchFilterDto;
        }
        public FrmCustomerSearch(long ID, FileSearchFilterDto fileSearchFilterDto, bool isEdit)
        {
            InitializeComponent();
            _ID = ID;
            _FileSearchFilterDto = fileSearchFilterDto;
            _IsEdit = isEdit;
        }



        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtTel1.Text.Length != 11)
            {
                FrmMsgBox.Show("شماره موبایل را درست وارد کنید", "", MsgBoxType.Error);
                return;
            }
            using (var db = new RealEstateContext())
            {
                var json = JsonConvert.SerializeObject(_FileSearchFilterDto); // Newtonsoft.Json
                var userFilter = db.Customer_List.FirstOrDefault(f => f.ID == _ID);

                if (userFilter == null)
                {
                    userFilter = new Customer
                    {
                        ID = _ID,
                        Title = txtTitle.Text,
                        FilterJson = json,
                        CreationDate = DateTime.Now,
                        Tel1 = txtTel1.Text,
                        Tel2 = txtTel2.Text,
                        Tel3 = txtTel3.Text,
                        Addres = txtAddress.Text,
                        Name = txtName.Text,
                        CountMember = txtCountMember.Text.ToInt32OrDefault(),
                        UserIDCreator = ClsBaseSetting.UserID

                    };
                    db.Customer_List.Add(userFilter);
                }
                else
                {
                    userFilter.ID = _ID;
                    userFilter.Title = txtTitle.Text;
                    userFilter.FilterJson = json;
                    userFilter.Tel1 = txtTel1.Text;
                    userFilter.Tel2 = txtTel2.Text;
                    userFilter.Tel3 = txtTel3.Text;
                    userFilter.Addres = txtAddress.Text;
                    userFilter.Name = txtName.Text;
                    userFilter.CountMember = txtCountMember.Text.ToInt32OrDefault();
                    userFilter.ModificationDate = DateTime.Now;
                    userFilter.UserIDModifier = ClsBaseSetting.UserID;
                }
                db.SaveChanges();
                DialogResult = DialogResult.OK;
            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtTel1_Leave(object sender, EventArgs e)
        {
            if (!_IsEdit)
                using (var db = new RealEstateContext())
                {
                    var _finde = db.Customer_List.Where(x => x.Tel1 == txtTel1.Text && x.UserIDCreator == ClsBaseSetting.UserID).FirstOrDefault();
                    if (_finde != null && FrmMsgBox.Show("یک مشتری با این شماره موبایل یافت شد آیا ویرایش شود ؟", "مشتری مشابه", MsgBoxType.Question, MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        _ID = _finde.ID;
                        txtTitle.Text = _finde.Title;
                        txtTel1.Text = _finde.Tel1;
                        txtTel2.Text = _finde.Tel1;
                        txtTel3.Text = _finde.Tel3;
                        txtAddress.Text = _finde.Addres;
                        txtName.Text = _finde.Name;
                        txtCountMember.Text = _finde.CountMember.ToString();

                    }
                }
        }

        private void FrmCustomerSearch_Load(object sender, EventArgs e)
        {
            txtTel1.Focus();
            if (_IsEdit)
            {
                using (var db = new RealEstateContext())
                {
                    var _finde = db.Customer_List.Where(x => x.ID == _ID && x.UserIDCreator == ClsBaseSetting.UserID).FirstOrDefault();

                    _ID = _finde.ID;
                    txtTitle.Text = _finde.Title;
                    txtTel1.Text = _finde.Tel1;
                    txtTel2.Text = _finde.Tel2;
                    txtTel3.Text = _finde.Tel3;
                    txtAddress.Text = _finde.Addres;
                    txtName.Text = _finde.Name;
                    txtCountMember.Text = _finde.CountMember.ToString();


                }
            }
        }
    }
}
