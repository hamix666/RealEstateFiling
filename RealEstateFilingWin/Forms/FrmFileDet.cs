//using Guna.UI2.WinForms;
//using RealEstateFiling.Services;
//using RealEstateFilingWin.DTO;
//using RealEstateFilingWin.Models.FIL;
//using RealEstateFilingWin.Utility;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Drawing;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Forms;

//namespace RealEstateFilingWin.Forms
//{
//    public partial class FrmFileDet : FrmBase
//    {
//        long _id;
//        string _name;
//        public Models.FIL.OwnerDet _ownerDet;
//        bool _type;
//        private bool _isFormatting = false;
//        RealEstateContext db = new RealEstateContext();
//        public FrmFileDet(Models.FIL.OwnerDet ownerDet, string name, bool type)
//        {
//            InitializeComponent();
//            _ownerDet = ownerDet;
//            _name = name;
//            _type = type;
//        }

//        private void FrmFileDet_Load(object sender, EventArgs e)
//        {
//            lblDet.Text = "اطلاعات مربوط به فایل : " + _name;
//            FillCmb();
//            if (_ownerDet.ID != 0)
//            {

//                //_ownerDet = EfFormBinder.Load<OwnerDet>(db, _ownerDet.ID, this);

//                txtID.Text = _ownerDet.ID.ToString();
//                txtOwnerID.Text = _ownerDet.OwnerID.ToString();
//                txtZirbana.Text = _ownerDet.Zirbana.ToString();
//                cmbTabaghe.SelectedValue = _ownerDet.Tabaghe == null ? 0 : _ownerDet.Tabaghe;
//                cmbKhab.SelectedValue = _ownerDet.Khab == null ? 0 : _ownerDet.Khab; ;
//                cmbParking.SelectedValue = _ownerDet.Parking == null ? 0 : _ownerDet.Parking; ;
//                chkBalkon.Checked = _ownerDet.Balkon;
//                chkAnbari.Checked = _ownerDet.Anbari;
//                chkAsansor.Checked = _ownerDet.Asansor;
//                cmbKafposhID.SelectedValue = _ownerDet.KafposhID;
//                cmbServiceWcID.SelectedValue = _ownerDet.ServiceWcID;
//                cmbKabinetID.SelectedValue = _ownerDet.KabinetID;
//                txtPrice1.Text = _ownerDet.Price1.ToString("N0");
//                txtPrice2.Text = _ownerDet.Price2.ToString("N0");

//            }
//            else
//            {
//                txtID.Text = "0";
//                txtOwnerID.Text = _ownerDet.OwnerID.ToString();
//                txtZirbana.Text = _ownerDet.Zirbana.ToString();
//                cmbTabaghe.SelectedValue = _ownerDet.Tabaghe == null ? 0 : _ownerDet.Tabaghe;
//                cmbKhab.SelectedValue = 0;
//                cmbParking.SelectedValue = 0;
//                chkBalkon.Checked = _ownerDet.Balkon;
//                chkAnbari.Checked = _ownerDet.Anbari;
//                chkAsansor.Checked = _ownerDet.Asansor;
//                cmbKafposhID.SelectedValue = 0;
//                cmbServiceWcID.SelectedValue = 0;
//                cmbKabinetID.SelectedValue = 0;
//                txtPrice1.Text = _ownerDet.Price1.ToString("N0");
//                txtPrice2.Text = _ownerDet.Price2.ToString("N0");
//                txtID.Text = "0";

//            }
//            if (!_type)
//            {
//                lblPrice1.Text = "مبلغ رهن";
//                lblPrice2.Text = "مبلغ اجاره";
//            }

//        }

//        private void FillCmb()
//        {

//            cmbKabinetID.DataSource = db.Kabinet_List.ToList();
//            cmbKafposhID.DataSource = db.Kafposh_List.ToList();
//            cmbServiceWcID.DataSource = db.ServiceWc_List.ToList();


//            var enums = new EnumValue();
//            cmbTabaghe.DataSource = enums.Tabaghe;
//            cmbKhab.DataSource = enums.Khab;
//            cmbParking.DataSource = enums.Parking;
//        }

//        private void btnClose_Click(object sender, EventArgs e)
//        {
//            DialogResult = DialogResult.Cancel;
//        }

//        private void guna2TileButton2_Click(object sender, EventArgs e)
//        {
//            DialogResult = DialogResult.Cancel;
//        }

//        private void btnSave_Click(object sender, EventArgs e)
//        {
//            if (String.IsNullOrEmpty(txtPrice1.Text))
//            {
//                FrmMsgBox.Show("مبالغ را وارد کنید", "خطا", MsgBoxType.Error);
//                txtPrice1.Focus();
//                return;
//            }
//            if (_ownerDet == null) { _ownerDet = new OwnerDet(); }
//            _ownerDet.Zirbana = txtZirbana.Text.ToInt32OrDefault();
//            _ownerDet.Tabaghe = cmbTabaghe.SelectedValue.ToInt32OrDefault();
//            _ownerDet.Khab = cmbKhab.SelectedValue.ToInt32OrDefault();
//            _ownerDet.Parking = cmbParking.SelectedValue.ToInt32OrDefault();
//            _ownerDet.Balkon = chkBalkon.Checked;
//            _ownerDet.Anbari = chkAnbari.Checked;
//            _ownerDet.Asansor = chkAsansor.Checked;
//            _ownerDet.KafposhID = cmbKabinetID.SelectedValue.ToInt64OrDefault();
//            _ownerDet.ServiceWcID = cmbServiceWcID.SelectedValue.ToInt64OrDefault();
//            _ownerDet.KabinetID = cmbKabinetID.SelectedValue.ToInt64OrDefault();
//            _ownerDet.Price1 = txtPrice1.Text.ToDecimalOrDefault();
//            _ownerDet.Price2 = txtPrice2.Text.ToDecimalOrDefault();
//            _ownerDet.ID = txtID.Text.ToInt64OrDefault();
//            _ownerDet.OwnerID = txtOwnerID.Text.ToInt64OrDefault();
//            _ownerDet.OwnerDet_Kabinet = new Models.GNR.Kabinet(){ID = cmbKabinetID.SelectedValue.ToInt64OrDefault(),Name = cmbKabinetID.Text};
//            _ownerDet.OwnerDet_Kafposh = new Models.GNR.Kafposh() { ID = cmbKafposhID.SelectedValue.ToInt64OrDefault(), Name = cmbKafposhID.Text };
//            _ownerDet.OwnerDet_ServiceWc = new Models.GNR.ServiceWc() { ID = cmbServiceWcID.SelectedValue.ToInt64OrDefault(), Name = cmbServiceWcID.Text };

//            DialogResult = DialogResult.OK;
//        }

//        private void btnCalc_Click(object sender, EventArgs e)
//        {
//            if (_type)
//            {
//                if (txtPrice1.Text.ToDecimalOrDefault() > 0)
//                {
//                    txtPrice2.Text = (txtZirbana.Text.ToDecimalOrDefault() * txtPrice1.Text.ToDecimalOrDefault()).ToString("N0");
//                }
//                else
//                {
//                    txtPrice1.Text = (txtPrice2.Text.ToDecimalOrDefault() / txtZirbana.Text.ToDecimalOrDefault()).ToString("N0");
//                }
//            }
//        }

//        private void txtPrice1_TextChanged(object sender, EventArgs e)
//        {
//            FormatNumberTextBox(txtPrice1);
//        }

//        private void txtPrice2_TextChanged(object sender, EventArgs e)
//        {
//            FormatNumberTextBox(txtPrice2);
//        }
//        private void FormatNumberTextBox(Guna2TextBox txt)
//        {
//            if (_isFormatting) return;
//            _isFormatting = true;

//            // حذف جداکننده‌ها و فقط نگه‌داشتن عدد
//            string digits = new string(txt.Text.Where(char.IsDigit).ToArray());

//            if (string.IsNullOrEmpty(digits))
//            {
//                _isFormatting = false;
//                return;
//            }

//            // موقعیت فعلی کرسر را نگه می‌داریم
//            int selStart = txt.SelectionStart;
//            int digitsBeforeCursor = txt.Text.Take(selStart).Count(char.IsDigit);

//            // فرمت سه رقمی با ویرگول
//            string formatted = string.Format("{0:N0}", long.Parse(digits));

//            txt.Text = formatted;

//            // بازیابی مکان‌نما بر اساس تعداد رقم قبل از کرسر
//            int pos = 0;
//            int digitCount = 0;
//            while (pos < txt.Text.Length && digitCount < digitsBeforeCursor)
//            {
//                if (char.IsDigit(txt.Text[pos])) digitCount++;
//                pos++;
//            }
//            txt.SelectionStart = pos;

//            _isFormatting = false;
//        }
//        private void txtPrice1_Leave(object sender, EventArgs e)
//        {
//            if (_type && txtPrice1.Text.ToDecimalOrDefault() > 0)
//            {
//                txtPrice2.Text = "0";
//                btnCalc.PerformClick();
//            }
//        }

//        private void txtPrice2_Leave(object sender, EventArgs e)
//        {
//            if (_type && txtPrice2.Text.ToDecimalOrDefault() > 0)
//            {
//                txtPrice1.Text = "0";
//                btnCalc.PerformClick();
//            }
//        }

//        private void txtPrice1_KeyDown(object sender, KeyEventArgs e)
//        {

//        }

//        private void txtPrice1_KeyPress(object sender, KeyPressEventArgs e)
//        {
//            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
//                e.Handled = true;
//        }

//        private void txtPrice2_KeyPress(object sender, KeyPressEventArgs e)
//        {
//            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
//                e.Handled = true;
//        }
//    }
//}


using Guna.UI2.WinForms;
using RealEstateFiling.Services;
using RealEstateFilingWin.DTO;
using RealEstateFilingWin.Models.FIL;
using RealEstateFilingWin.Utility;
using System;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RealEstateFilingWin.Forms
{
    public partial class FrmFileDet : FrmBase
    {
        public OwnerDet _ownerDet;
        private string _name;
        private bool _type;
        private bool _formnew;
        private bool _isFormatting = false;
        int _masahat = 0;
        string _FiletypeName = "";

        public FrmFileDet(string FiletypeName,bool formnew,OwnerDet ownerDet, string name, bool type,int masahat=0)
        {
            InitializeComponent();
            _ownerDet = ownerDet ?? new OwnerDet();
            _name = name;
            _type = type;
            _masahat=masahat;
            _formnew = formnew;
            _FiletypeName= FiletypeName;
        }

        private void FrmFileDet_Load(object sender, EventArgs e)
        {
            lblDet.Text = "اطلاعات مربوط به فایل : " + _name;
            if(_masahat > 0)  lblDet.Text += " مساحت ملک برابر : " + _masahat;
            Task.Run(() =>
            {
                using (var db = new RealEstateContext())
                {
                    var kabinet = db.Kabinet_List.AsNoTracking().ToList();
                    var kafposh = db.Kafposh_List.AsNoTracking().ToList();
                    var service = db.ServiceWc_List.AsNoTracking().ToList();

                    this.Invoke(new Action(() =>
                    {
                        cmbKabinetID.DataSource = kabinet;
                        cmbKabinetID.DisplayMember = "Name";
                        cmbKabinetID.ValueMember = "ID";
                        cmbKabinetID.SelectedIndex = -1;

                        cmbKafposhID.DataSource = kafposh;
                        cmbKafposhID.DisplayMember = "Name";
                        cmbKafposhID.ValueMember = "ID";
                        cmbKafposhID.SelectedIndex = -1;

                        cmbServiceWcID.DataSource = service;
                        cmbServiceWcID.DisplayMember = "Name";
                        cmbServiceWcID.ValueMember = "ID";
                        cmbServiceWcID.SelectedIndex = -1;
                        var enums = new EnumValue();
                        cmbTabaghe.DataSource = enums.Tabaghe;
                        cmbTabaghe.SelectedIndex = -1;
                        
                        cmbKhab.DataSource = enums.Khab;
                        cmbKhab.SelectedIndex = -1;

                        cmbParking.DataSource = enums.Parking;
                        cmbParking.SelectedIndex = -1;
                    }));
                }
            BindOwnerDetToControls();
            });
        }


        private void BindOwnerDetToControls()
        {
            this.Invoke(new Action(() =>
            {
                txtID.Text = _ownerDet.ID.ToString();
                txtOwnerID.Text = _ownerDet.OwnerID.ToString();
                txtZirbana.Text = _ownerDet.Zirbana.ToString();
                chkBalkon.Checked = _ownerDet.Balkon;
                chkAnbari.Checked = _ownerDet.Anbari;
                chkAsansor.Checked = _ownerDet.Asansor;
                if(_ownerDet.Asansor)
                {
                    cmbAsansor.SelectedIndex = 1;
                }
                else
                {
                    cmbAsansor.SelectedIndex = 0;
                }
                if (_formnew)
                {
                    cmbAsansor.SelectedIndex = -1;
                }
                else
                {
                    cmbTabaghe.SelectedValue = _ownerDet.Tabaghe ?? 0;
                    cmbKhab.SelectedValue = _ownerDet.Khab ?? 2;
                    cmbParking.SelectedValue = _ownerDet.Parking ?? 1;
                    cmbKabinetID.SelectedValue = _ownerDet.KabinetID ?? 1;
                    cmbKafposhID.SelectedValue = _ownerDet.KafposhID ?? 1;
                    cmbServiceWcID.SelectedValue = _ownerDet.ServiceWcID ?? 1;
                }
                txtPrice1.Text = _ownerDet.Price1 == 0 ? "" : _ownerDet.Price1.ToString("N0");
                txtPrice2.Text = _ownerDet.Price2 == 0 ? "" : _ownerDet.Price2.ToString("N0");

                if (!_type)
                {
                    lblPrice1.Text = "مبلغ اجاره";
                    lblPrice2.Text = "مبلغ رهن";
                    if (_ownerDet.ID == 0)
                        txtPrice1.Text = "0";

                }
            }));
        }

        private void SaveOwnerDetFromControls()
        {
            _ownerDet.Zirbana = txtZirbana.Text.ToInt32OrDefault();
            _ownerDet.Tabaghe = cmbTabaghe.SelectedValue.ToInt32OrDefault();
            _ownerDet.Khab = cmbKhab.SelectedValue.ToInt32OrDefault();
            _ownerDet.Parking = cmbParking.SelectedValue.ToInt32OrDefault();
            _ownerDet.Balkon = chkBalkon.Checked;
            _ownerDet.Anbari = chkAnbari.Checked;
            //_ownerDet.Asansor = chkAsansor.Checked;
            _ownerDet.Asansor = cmbAsansor.SelectedIndex==0?false:true;
            _ownerDet.KafposhID = cmbKafposhID.SelectedValue.ToInt64OrDefault();
            _ownerDet.ServiceWcID = cmbServiceWcID.SelectedValue.ToInt64OrDefault();
            _ownerDet.KabinetID = cmbKabinetID.SelectedValue.ToInt64OrDefault();
            _ownerDet.Price1 = txtPrice1.Text.ToDecimalOrDefault();
            _ownerDet.Price2 = txtPrice2.Text.ToDecimalOrDefault();

            _ownerDet.OwnerDet_Kabinet = new Models.GNR.Kabinet { ID = cmbKabinetID.SelectedValue.ToInt64OrDefault(), Name = cmbKabinetID.Text };
            _ownerDet.OwnerDet_Kafposh = new Models.GNR.Kafposh { ID = cmbKafposhID.SelectedValue.ToInt64OrDefault(), Name = cmbKafposhID.Text };
            _ownerDet.OwnerDet_ServiceWc = new Models.GNR.ServiceWc { ID = cmbServiceWcID.SelectedValue.ToInt64OrDefault(), Name = cmbServiceWcID.Text };
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPrice1.Text))
            {
                FrmMsgBox.Show("مبالغ را وارد کنید", "خطا", MsgBoxType.Error);
                txtPrice1.Focus();
                return;
            }
            if (Ejbari() == false)
            {
                return;
            }
            SaveOwnerDetFromControls();
            DialogResult = DialogResult.OK;
        }

        private void btnClose_Click(object sender, EventArgs e) => DialogResult = DialogResult.Cancel;
        private void guna2TileButton2_Click(object sender, EventArgs e) => DialogResult = DialogResult.Cancel;

        #region Price Formatting
        private void txtPrice1_Leave(object sender, EventArgs e)
        {
            FormatNumberTextBox(txtPrice1);
            //if(_type)
            //btnCalc.PerformClick();
        }  


        private void txtPrice2_Leave(object sender, EventArgs e)
        {
            FormatNumberTextBox(txtPrice2);
            //if(_type)
            //btnCalc.PerformClick();
        }

        private void FormatNumberTextBox(Guna2TextBox txt)
        {
            if (_isFormatting) return;
            _isFormatting = true;

            string digits = new string(txt.Text.Where(char.IsDigit).ToArray());
            if (string.IsNullOrEmpty(digits)) { _isFormatting = false; return; }

            txt.Text = string.Format("{0:N0}", long.Parse(digits));
            _isFormatting = false;
        }

        private void btnCalc_Click(object sender, EventArgs e)
        {
            if (_masahat > 0)
            {
                decimal price1 = txtPrice1.Text.ToDecimalOrDefault();
                decimal zirbana = _masahat.ToDecimalOrDefault();

                if (price1 > 0) txtPrice2.Text = (_masahat * price1).ToString("N0");
                else txtPrice1.Text = (txtPrice2.Text.ToDecimalOrDefault() / _masahat).ToString("N0");
                return;
            }
            if (_type)
            {
                decimal price1 = txtPrice1.Text.ToDecimalOrDefault();
                decimal zirbana = txtZirbana.Text.ToDecimalOrDefault();

                if (price1 > 0) txtPrice2.Text = (zirbana * price1).ToString("N0");
                else txtPrice1.Text = (txtPrice2.Text.ToDecimalOrDefault() / zirbana).ToString("N0");
            }

        }
        #endregion

        private void txtPrice1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
            if(_type)
            txtPrice2.Text = "0";
        }

        private void txtPrice2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
            if(_type)
            txtPrice1.Text="0";
        }

        private void FormatNumberTextBoxRunTime(Guna2TextBox txt)
        {
            if (_isFormatting) return;
            _isFormatting = true;

            // حذف جداکننده‌ها و فقط نگه‌داشتن عدد
            string digits = new string(txt.Text.Where(char.IsDigit).ToArray());

            if (string.IsNullOrEmpty(digits))
            {
                _isFormatting = false;
                return;
            }

            // موقعیت فعلی کرسر را نگه می‌داریم
            int selStart = txt.SelectionStart;
            int digitsBeforeCursor = txt.Text.Take(selStart).Count(char.IsDigit);

            // فرمت سه رقمی با ویرگول
            string formatted = string.Format("{0:N0}", long.Parse(digits));

            txt.Text = formatted;

            // بازیابی مکان‌نما بر اساس تعداد رقم قبل از کرسر
            int pos = 0;
            int digitCount = 0;
            while (pos < txt.Text.Length && digitCount < digitsBeforeCursor)
            {
                if (char.IsDigit(txt.Text[pos])) digitCount++;
                pos++;
            }
            txt.SelectionStart = pos;

            _isFormatting = false;
        }

        private void txtPrice1_TextChanged(object sender, EventArgs e)
        {
            FormatNumberTextBoxRunTime(txtPrice1);
        }

        private void txtPrice2_TextChanged(object sender, EventArgs e)
        {
            FormatNumberTextBoxRunTime(txtPrice2);
        }

        private void btnMin_Click(object sender, EventArgs e)
        {
            this.WindowState=FormWindowState.Minimized;
            
        }
        int counterror = 0;
        public bool Ejbari()
        {
            errorEjbari.Clear();

            counterror = 0;

          
            CheckControl(cmbTabaghe);
            CheckControl(cmbKhab);
            CheckControl(cmbParking);
            CheckControl(cmbKafposhID);
            CheckControl(cmbServiceWcID);

            CheckControl(cmbKabinetID);
            CheckControl(cmbAsansor);
            CheckControl(txtPrice1);
            CheckControl(txtPrice2);
            CheckControl(txtZirbana);

            
            if (counterror != 0)
                return false;
            else
                return true;
        }

        public void CheckControl(Control _control)
        {
            if (_control is Guna2ComboBox)
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
            else if (_control is Guna2TextBox)
            {
                var _c = (Guna2TextBox)_control;
                if (string.IsNullOrEmpty(_c.Text))
                {
                    errorEjbari.SetError(_c, "اجباری");
                    _c.BackColor = Color.DarkRed;
                    counterror++;
                }
                else
                    _c.BackColor = Color.Gainsboro;
            }
        }
    }
}

