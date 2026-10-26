

using RealEstateFiling.Services;
using RealEstateFilingWin.Models.GNR;
using RealEstateFilingWin.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using RealEstateFilingWin.Models.MNG;
using Common;

namespace RealEstateFilingWin.Forms
{
    public partial class FrmUserManage : FrmBase
    {
        string _connection;
        // مدل کمکی برای CheckedListBox
        public class chkItem
        {
            public long Id { get; set; }
            public string Name { get; set; }

            public override string ToString() => Name;
        }

        private bool isEditMode = false; // حالت جدید یا ویرایش
        private long currentUserId = 0;

        public FrmUserManage(string _str)
        {
            InitializeComponent();
            _connection = _str;
        }

        private void FrmUserManage_Load(object sender, EventArgs e)
        {
            Dictionary<int, string> list = new Dictionary<int, string>();
            list.Add(1, "فروش");
            list.Add(2, "رهن و اجاره");

            cmbType.DataSource = new BindingSource(list, null);
            cmbType.DisplayMember = "Value";
            cmbType.ValueMember = "Key";

            LoadUsers();
            LoadAccessLists();
        }

        // --------------- بارگذاری کاربران ----------------
        private void LoadUsers()
        {
            using (var db = new RealEstateContext(_connection))
            {
                grdUser.DataSource = db.Users_List.Where(x=>x.ID!=1).ToList();
            }
        }

        // --------------- بارگذاری چک‌لیست‌ها ----------------
        private void LoadAccessLists()
        {
            using (var db = new RealEstateContext(_connection))
            {
                var _lstAcces = db.AccsessName_List.ToList();

                foreach (var item in _lstAcces)
                {
                    if (item.ID >= 0 && item.ID < 4)
                        chklistMomeleType.Items.Add(new chkItem { Id = item.ID, Name = item.Name }, false);
                    else if (item.ID > 3 && item.ID < 8)
                        chkListMelkType.Items.Add(new chkItem { Id = item.ID, Name = item.Name }, false);
                    else
                        chkListAccsess.Items.Add(new chkItem { Id = item.ID, Name = item.Name }, false);
                }
            }
        }

        // --------------- وقتی رکورد تغییر کرد ----------------
        private void grdUser_CurrentCellChanged(object sender, EventArgs e)
        {
            //if (grdUser.CurrentRow == null) return;

            //var userId = grdUser.CurrentRow.Cells["ID"].Value.ToInt64OrDefault();
            //if (userId <= 0) return;

            //currentUserId = userId;
            //isEditMode = true;

            //using (var db = new RealEstateContext())
            //{
            //    var user = db.Users_List.FirstOrDefault(u => u.ID == userId);
            //    if (user == null) return;

            //    txtUserName.Text = user.Username;
            //    txtName.Text = user.Name;
            //    txtCodeMeli.Text = user.CodeMeli;
            //    txtMobile.Text = user.Mobile;
            //    txtPhone.Text = user.PhonNumber;
            //    txtAddress.Text = user.Address;
            //    cmbType.SelectedValue = user.Description_Latin?.ToInt32OrDefault();

            //    ClearAllChecked(chklistMomeleType);
            //    ClearAllChecked(chkListMelkType);
            //    ClearAllChecked(chkListAccsess);

            //    // گرفتن دسترسی‌های کاربر از جدول UserAccsess
            //    var accessList = db.UserAccsess_List
            //        .Where(x => x.UserID == userId)
            //        .Select(x => x.AccessID)
            //        .ToList();

            //    SetCheckedItems(chklistMomeleType, accessList);
            //    SetCheckedItems(chkListMelkType, accessList);
            //    SetCheckedItems(chkListAccsess, accessList);
            //}
        }

        // ----------------- تابع تنظیم تیک‌ها -----------------
        private void SetCheckedItems(CheckedListBox listBox, List<long> accessIds)
        {
            for (int i = 0; i < listBox.Items.Count; i++)
            {
                var item = (chkItem)listBox.Items[i];
                listBox.SetItemChecked(i, accessIds.Contains(item.Id));
            }
        }

        private void ClearAllChecked(CheckedListBox listBox)
        {
            for (int i = 0; i < listBox.Items.Count; i++)
                listBox.SetItemChecked(i, false);
        }

        // ----------------- ذخیره یا بروزرسانی -----------------
        private void btnSave_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            bool hasError = false;

            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                errorProvider1.SetError(txtName, "نام اجباری است");
                hasError = true;
            }

            if (string.IsNullOrWhiteSpace(txtUserName.Text))
            {
                errorProvider1.SetError(txtUserName, "نام کاربری اجباری است");
                hasError = true;
            }

            if (!isEditMode && string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                errorProvider1.SetError(txtPassword, "رمز عبور اجباری است");
                hasError = true;
            }

            // اگر خطا وجود داشت، ادامه نده
            if (hasError)
            {
                FrmMsgBox.Show("موارد اجباری را تکمیل کنید", "خطا", MsgBoxType.Error);
                return;
            }

            using (var db = new RealEstateContext(_connection))
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    User user;
                    if (isEditMode)
                    {
                        user = db.Users_List.FirstOrDefault(u => u.ID == currentUserId);
                        if (user == null) return;
                    }
                    else
                    {
                        user = new User();
                        db.Users_List.Add(user);
                    }

                    // اطلاعات پایه
                    user.Username = txtUserName.Text.Trim();
                    user.Name = txtName.Text.Trim();
                    user.CodeMeli = txtCodeMeli.Text.Trim();
                    user.Mobile = txtMobile.Text.Trim();
                    user.PhonNumber = txtPhone.Text.Trim();
                    user.Address = txtAddress.Text.Trim();
                    user.Description_Latin = cmbType.SelectedValue.ToString();
                    if (!isEditMode)
                    {
                        user.CreationDate = DateTime.Now;
                        user.UserIDCreator = ClsBaseSetting.UserID;
                    }
                    else
                    {
                        user.ModificationDate = DateTime.Now;
                        user.UserIDModifier = ClsBaseSetting.UserID;
                    }
                    if(txtPassword.Text.Length>0)
                        user.Password = BCrypt.Net.BCrypt.HashPassword(txtPassword.Text.Trim());
                    db.SaveChanges();

                    // --------- بهینه سازی دسترسی‌ها ---------
                    // 1. لیست AccessIDهایی که در چک‌لیست انتخاب شدند
                    var selectedAccessIds = new HashSet<long>();
                    AddCheckedItemsToSet(chklistMomeleType, selectedAccessIds);
                    AddCheckedItemsToSet(chkListMelkType, selectedAccessIds);
                    AddCheckedItemsToSet(chkListAccsess, selectedAccessIds);

                    // 2. دسترسی‌های فعلی کاربر از DB
                    var currentAccessIds = db.UserAccsess_List
                        .Where(x => x.UserID == user.ID)
                        .Select(x => x.AccessID)
                        .ToList();

                    // 3. اضافه کردن دسترسی‌های جدید
                    var toAdd = selectedAccessIds.Except(currentAccessIds);
                    foreach (var accId in toAdd)
                    {
                        db.UserAccsess_List.Add(new UserAccsess
                        {
                            UserID = user.ID,
                            AccessID = accId,
                            CreationDate=DateTime.Now,
                            UserIDCreator=ClsBaseSetting.UserID
                        });
                    }

                    // 4. حذف دسترسی‌هایی که دیگر انتخاب نشده‌اند
                    var toRemove = currentAccessIds.Except(selectedAccessIds).ToList();
                    if (toRemove.Any())
                    {
                        var itemsToRemove = db.UserAccsess_List
                            .Where(x => x.UserID == user.ID && toRemove.Contains(x.AccessID));
                        db.UserAccsess_List.RemoveRange(itemsToRemove);
                    }

                    db.SaveChanges();
                    transaction.Commit();

                    FrmMsgBox.Show("اطلاعات با موفقیت ذخیره شد.", "پیام", MsgBoxType.Info);
                    LoadUsers();
                    btnNew_Click(sender, e  ); // فرم را ریست کن
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    FrmMsgBox.Show("خطا در ذخیره اطلاعات: " + ex.Message, "خطا",MsgBoxType.Error);
                }

                ClsBaseSetting.UserIn.UserAccsessList = db.UserAccsess_List.Where(x=>x.UserID==2).ToList();
            }
        }

        // افزودن AccessIDهای تیک‌خورده به یک مجموعه HashSet
        private void AddCheckedItemsToSet(CheckedListBox listBox, HashSet<long> set)
        {
            for (int i = 0; i < listBox.Items.Count; i++)
            {
                if (listBox.GetItemChecked(i))
                {
                    var item = (chkItem)listBox.Items[i];
                    set.Add(item.Id);
                }
            }
        }

        // ----------------- بستن فرم -----------------
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        // ----------------- دکمه جدید -----------------
        private void btnNew_Click(object sender, EventArgs e)
        {
            isEditMode = false;
            currentUserId = 0;

            txtUserName.Clear();
            txtName.Clear();
            txtCodeMeli.Clear();
            txtMobile.Clear();
            txtPhone.Clear();
            txtAddress.Clear();
            txtPassword.Clear();
            chkAllTypeMoamele.Checked = false;
            chkAccess.Checked = false;
            chkMelktype.Checked = false;

            ClearAllChecked(chklistMomeleType);
            ClearAllChecked(chkListMelkType);
            ClearAllChecked(chkListAccsess);
        }

        private void chkAllTypeMoamele_CheckedChanged(object sender, EventArgs e)
        {
            SetAllChecked(chklistMomeleType, chkAllTypeMoamele.Checked);
        }

        private void chkAccess_CheckedChanged(object sender, EventArgs e)
        {
            SetAllChecked(chkListAccsess, chkAccess.Checked);
        }

        private void chkMelktype_CheckedChanged(object sender, EventArgs e)
        {
            SetAllChecked(chkListMelkType, chkMelktype.Checked);
        }

        private void SetAllChecked(CheckedListBox listBox, bool isChecked)
        {
            for (int i = 0; i < listBox.Items.Count; i++)
                listBox.SetItemChecked(i, isChecked);
        }

        private void grdUser_MouseClick(object sender, MouseEventArgs e)
        {
            if (grdUser.CurrentRow == null ) return;

            var userId = grdUser.CurrentRow.Cells["ID"].Value.ToInt64OrDefault();
            if (userId <= 0) return;

            currentUserId = userId;
            isEditMode = true;

            using (var db = new RealEstateContext(_connection))
            {
                var user = db.Users_List.FirstOrDefault(u => u.ID == userId);
                if (user == null) return;

                txtUserName.Text = user.Username;
                txtName.Text = user.Name;
                txtCodeMeli.Text = user.CodeMeli;
                txtMobile.Text = user.Mobile;
                txtPhone.Text = user.PhonNumber;
                txtAddress.Text = user.Address;
                cmbType.SelectedValue = user.Description_Latin?.ToInt32OrDefault();

                ClearAllChecked(chklistMomeleType);
                ClearAllChecked(chkListMelkType);
                ClearAllChecked(chkListAccsess);

                // گرفتن دسترسی‌های کاربر از جدول UserAccsess
                var accessList = db.UserAccsess_List
                    .Where(x => x.UserID == userId)
                    .Select(x => x.AccessID)
                    .ToList();

                SetCheckedItems(chklistMomeleType, accessList);
                SetCheckedItems(chkListMelkType, accessList);
                SetCheckedItems(chkListAccsess, accessList);
            }
        }
    }
}
