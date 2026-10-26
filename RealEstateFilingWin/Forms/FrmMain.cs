

using Common;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Controls;
using DevComponents.Editors;
using Guna.UI2.WinForms;
using Janus.Windows.EditControls;
using Janus.Windows.GridEX;
using Janus.Windows.UI.Tab;
using Microsoft.Web.WebView2.Core;
using PresentationControls;
using RealEstateFiling.Services;
using RealEstateFilingWin.DTO;
using RealEstateFilingWin.Models.FIL;
using RealEstateFilingWin.Services.FIL;
using RealEstateFilingWin.Services.Sync;
using RealEstateFilingWin.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Guna.UI2.Native.WinApi;

namespace RealEstateFilingWin.Forms
{
    public partial class FrmMain : FrmBase
    {
        private OwnersBL _ownersBL = new OwnersBL();
        int pageNumber = 1, pageSize = 100;

        private bool isAnimating = false;
        private bool isOpen = false;
        private int targetHeight = 0;
        private int targetWidth = 0;
        private Timer slideTimer;
        private DevComponents.DotNetBar.TabControlPanel currentPanel = null;
        PagedResult<FileListDto> _lstowner = new PagedResult<FileListDto>();
        List<GridFileListDto> _lstfinal = new List<GridFileListDto>();
        private bool _isFormatting = false;
        FrmCustomerSearchDet frmCustomerSearchDet = new FrmCustomerSearchDet();
        long _idCustomer = 0;
        bool _isCustomerView = false;
        bool _isPoshe = false;
        private Guna.UI2.WinForms.Guna2GradientButton _activeButton = null;
        private ListBox popupList;
        private Guna2TextBox activeTextbox;

        List<string> prices = new List<string>()
        {
            "100,000",
            "500,000",
            "1,000,000",
            "5,000,000",
            "10,000,000",
            "20,000,000",
            "30,000,000",
            "50,000,000",
            "100,000,000",
            "200,000,000",
            "500,000,000",
            "1,000,000,000",
        };


        public class chkItem
        {
            public long Id { get; set; }       // مثلاً 101
            public string Name { get; set; }  // مثلاً "تهران"

            public override string ToString()
            {
                // باعث میشه وقتی توی CheckedListBox نمایش داده میشه، فقط اسمش نشون داده بشه
                return Name;
            }
        }


        // Import تابع SetWindowPos از User32
        //[DllImport("user32.dll")]
        //private static extern bool SetWindowPos(
        //    IntPtr hWnd,
        //    IntPtr hWndInsertAfter,
        //    int X,
        //    int Y,
        //    int cx,
        //    int cy,
        //    uint uFlags);

        //private static readonly IntPtr HWND_TOP = new IntPtr(0);
        //private const UInt32 SWP_SHOWWINDOW = 0x0040;

        public FrmMain()
        {
            InitializeComponent();
            // انتخاب پیش‌فرض دکمه اول
            var first = btnSearch;
            if (first != null)
                ActivateButton(first);



        }


        private void ActivateButton(Guna.UI2.WinForms.Guna2GradientButton btn)
        {
            if (btn == null) return;

            if (btn.Name != "btnSetting" && btn.Name != "btnUserManage" && btn.Name != "btnFileForm")
            {
                // ریست دکمه قبلی
                if (_activeButton != null)
                {
                    _activeButton.FillColor = Color.FromArgb(0, 120, 215);
                    _activeButton.ForeColor = Color.White;
                    _activeButton.ImageSize = new Size(24, 24);
                }

                // فعال‌سازی دکمه جدید
                //btn.FillColor = Color.FromArgb(0, 120, 215); // آبی متریال
                btn.FillColor = Color.FromArgb(40, 40, 40); // آبی متریال
                btn.ForeColor = Color.White;
                btn.ImageSize = new Size(28, 28);

                _activeButton = btn;
            }
            //// حرکت دادن اسلاید پنل
            //MoveSlidePanel(btn);
        }

        private void MoveSlidePanel(Guna.UI2.WinForms.Guna2GradientButton btn)
        {
            int targetTop = btn.Top;

            Timer t = new Timer();
            t.Interval = 1;

            t.Tick += (s, e) =>
            {
                panelMenu.Top += (targetTop - panelMenu.Top) / 4;

                if (Math.Abs(panelMenu.Top - targetTop) < 2)
                {
                    panelMenu.Top = targetTop;
                    t.Stop();
                    t.Dispose();
                }
            };

            t.Start();
        }



        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {

            ApplyBorderlessTaskbarSafe();
            FillData();

            FillStatusbar();
            TabcontrolLoad();
            SetUserAccsess();
            btnCustomer.Symbol = "✅";
            CommonExtension.ApplyToAllControls(this);



        }

        private void SetUserAccsess()
        {
            if (ClsBaseSetting.UserID != 1)
            {
                var userAccsesses = ClsBaseSetting.UserIn.UserAccsessList.ToList();
                if (!userAccsesses.Where(x => x.AccessID == 10).Any())
                    btnPoshe.Visible = false;
                if (!userAccsesses.Where(x => x.AccessID == 11).Any())
                {

                    btnCustomerSearchDetView.Visible = false;
                    btnSaveSearch.Visible = false;
                }
                if (!userAccsesses.Where(x => x.AccessID == 12).Any())
                    btnFileForm.Visible = false;
                if (!userAccsesses.Where(x => x.AccessID == 13).Any())
                    btnUserManage.Visible = false;
                if (!userAccsesses.Where(x => x.AccessID == 14).Any())
                {
                    btnSetting.Visible = false;
                    btnBackup.Visible = false;
                }

                if (!userAccsesses.Where(x => x.AccessID == 1).Any())
                    chkForosh.Enabled = false;
                if (!userAccsesses.Where(x => x.AccessID == 2).Any())
                    chkPishForosh.Enabled = false;
                if (!userAccsesses.Where(x => x.AccessID == 3).Any())
                    chkEjare.Enabled = false;
                if (!userAccsesses.Where(x => x.AccessID == 4).Any())
                    chkAparteman.Enabled = false;
                if (!userAccsesses.Where(x => x.AccessID == 5).Any())
                    chkEdari.Enabled = false;
                if (!userAccsesses.Where(x => x.AccessID == 6).Any())
                    chkMostaghelat.Enabled = false;
                if (!userAccsesses.Where(x => x.AccessID == 7).Any())
                    chkMaghaze.Enabled = false;

            }
        }

        private void TabcontrolLoad()
        {
            popupList.Visible = false;
            tabControlSearch.Visible = false;
            tabControlSearch.Height = 0; // شروع از ارتفاع صفر

            slideTimer = new Timer();
            slideTimer.Interval = 10; // سرعت انیمیشن
            slideTimer.Tick += SlideTimer_Tick;

            // پنهان کردن همه تب‌ها در شروع
            foreach (var p in tabControlSearch.Controls.OfType<DevComponents.DotNetBar.TabControlPanel>())
                p.Visible = false;
        }

        private void FillStatusbar()
        {
            lbldate.Text = "امروز : " + DateTime.Now.ToShamsiDate();
            lblUser.Text = "کاربر : " + ClsBaseSetting.UserIn.Name + " // : " + ClsBaseSetting.CurrentBranchName;
            lblLoginTime.Text = "زمان ورود : " + ClsBaseSetting.timein;


        }

        private void ApplyBorderlessTaskbarSafe()
        {
            //this.FormBorderStyle = FormBorderStyle.None;
            //this.TopMost = false;
            //this.StartPosition = FormStartPosition.Manual;

            //// اندازه فرم = WorkingArea (بدون پوشاندن Taskbar)
            //Rectangle workArea = Screen.FromHandle(this.Handle).WorkingArea;
            //this.Location = workArea.Location;
            //this.Size = workArea.Size;

            //// استفاده از SetWindowPos برای قرار گرفتن دقیق فرم روی Desktop بدون پوشاندن Taskbar
            //SetWindowPos(this.Handle, HWND_TOP, workArea.X, workArea.Y, workArea.Width, workArea.Height, SWP_SHOWWINDOW);
            // استفاده از این روش به جای آن
            // ابتدا فرم رو نرمال کنیم
            this.WindowState = FormWindowState.Normal;

            // تنظیم bounds برای ماکسیمایز
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;

            // حالا ماکسیمایز کنیم
            this.WindowState = FormWindowState.Maximized;

            gmain.BackColor = ColorTranslator.FromHtml("#FFFFFF");
            gmain.AlternatingColors = true;
            //gmain.AlternatingRowFormatStyle.BackColor = ColorTranslator.FromHtml("#E3F2FD");
            //gmain.AlternatingRowFormatStyle.ForeColor = ColorTranslator.FromHtml("#263238");

            gmain.HeaderFormatStyle.BackColor = ColorTranslator.FromHtml("#0D47A1");
            gmain.HeaderFormatStyle.ForeColor = Color.Black;
            //grdmain.HeaderFormatStyle.Font = new Font("Vazirmatn", 12, FontStyle.Bold);

            gmain.SelectedFormatStyle.BackColor = ColorTranslator.FromHtml("#BBDEFB");
            gmain.SelectedFormatStyle.ForeColor = ColorTranslator.FromHtml("#0D47A1");
            gmain.RowFormatStyle.BackColor = ColorTranslator.FromHtml("#FFFFFF");
            gmain.RowFormatStyle.ForeColor = ColorTranslator.FromHtml("#263238");
            //grdmain.GridLines = Janus.Windows.GridEX.GridLines.Horizontal;
            gmain.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2010;








            foreach (Control ctrl in tabType.Controls)
            {
                if (ctrl is CheckBox cb)
                    cb.CheckedChanged += CheckBoxFileTypo_CheckedChanged;
            }
            foreach (Control ctrl in tabMoamele.Controls)
            {
                if (ctrl is CheckBox cb)
                    cb.CheckedChanged += CheckBoxMoamele_CheckedChanged;
            }
            foreach (Control ctrl in tabbiygani.Controls)
            {
                if (ctrl is CheckBox cb)
                    cb.CheckedChanged += CheckBoxBiygani_CheckedChanged;
            }

            foreach (Control c in panelMenu.Controls)
            {
                if (c is Guna.UI2.WinForms.Guna2GradientButton btn)
                {
                    btn.Click += MenuButton_Click;
                }
            }


            popupList = new ListBox();
            popupList.Visible = false;
            popupList.IntegralHeight = true;

            popupList.Click += PopupList_Click;

            this.Controls.Add(popupList);

            txtprice1_s.Enter += ShowPopupList;
            txtprice1_e.Enter += ShowPopupList;
            txtprice2_s.Enter += ShowPopupList;
            txtprice2_e.Enter += ShowPopupList;

            #region Tooltip

            CommonExtension.AddToolTip(label32, "اگر از این گزینه استفاده کنید باید به شرایط زیر توجه کنید \r\nاز عددی که مینویسید به بعد را با آسانسور و قبل آن را بدون آسانسور در نظر میگیرد \r\nهمچنین استفاده همزمان این گزینه با گزینه های طبقه و آسانسور امکانپذیر نمیباشد");
            CommonExtension.AddToolTip(btnasnsor, "اگر از این گزینه استفاده کنید باید به شرایط زیر توجه کنید \r\nاز عددی که مینویسید به بعد را با آسانسور و قبل آن را بدون آسانسور در نظر میگیرد \r\nهمچنین استفاده همزمان این گزینه با گزینه های طبقه و آسانسور امکانپذیر نمیباشد");
            #endregion
        }

        private void ShowPopupList(object sender, EventArgs e)
        {
            activeTextbox = sender as Guna2TextBox;

            // مقداردهی لیست
            popupList.Items.Clear();
            popupList.Items.AddRange(prices.ToArray());

            // جای دقیق زیر TextBox
            var location = activeTextbox.PointToScreen(new Point(0, activeTextbox.Height));
            location = this.PointToClient(location);

            popupList.Location = location;
            popupList.Width = activeTextbox.Width;
            popupList.Height = 120;

            popupList.Visible = true;
            popupList.BringToFront();
        }
        private void PopupList_Click(object sender, EventArgs e)
        {
            if (activeTextbox != null)
                activeTextbox.Text = popupList.SelectedItem.ToString();

            popupList.Visible = false;
        }
        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            popupList.Visible = false;
        }


        private void MenuButton_Click(object sender, EventArgs e)
        {
            var btn = sender as Guna.UI2.WinForms.Guna2GradientButton;
            ActivateButton(btn);
        }

        private void CheckBoxBiygani_CheckedChanged(object sender, EventArgs e)
        {
            // کنترل فرستنده رو بگیر
            CheckBox selectedCheckBox = sender as CheckBox;

            // فقط اگر چک‌باکس انتخاب شد (نه لغو انتخاب)
            if (selectedCheckBox.Checked)
            {
                // در تمام چک‌باکس‌های داخل پنل بگرد
                foreach (Control ctrl in tabbiygani.Controls)
                {
                    if (ctrl is CheckBox cb && cb != selectedCheckBox)
                    {
                        cb.Checked = false; // بقیه رو غیرفعال کن
                    }
                }
            }
        }

        private void btnFileForm_Click(object sender, EventArgs e)
        {
            FrmFile frmFile = new FrmFile();
            // 📡 ثبت هندلر برای بروزرسانی گرید بعد از ذخیره
            frmFile.DataSaved += () =>
            {
                if (frmFile.mobile != "")
                {
                    txtMTel1.Text = frmFile.mobile;
                    btnSearchMain.PerformClick();
                }

                FillData();
            };

            frmFile.Show();
        }

        private void grdmain_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (gmain.CurrentRow.RowType == RowType.Record)
                ViewFile();
        }

        private void ViewFile()
        {
            if (_lstowner.Items.Count() == 0)
                return;
            List<long> _lstid = _lstowner.Items.ToList().Select(w => w.ID).ToList();
            var _selectrow = gmain.CurrentRow.Position;
            var ID = gmain.CurrentRow.Cells["ID"].Value;
            FrmFileView frmFile = new FrmFileView(ID.ToInt64OrDefault(), _lstid);
            frmFile._customerid = _idCustomer;
            if (gmain.CurrentRow.Cells["Newrecord"].Value.ToInt32OrDefault() == 1) frmFile._SearchView = true;
            // 📡 ثبت هندلر برای بروزرسانی گرید بعد از ذخیره
            frmFile.DataSaved += () =>
            {

                if (_idCustomer == 0)
                    FillData();
                else
                    FillData(false);
                gmain.MoveToRowIndex(_selectrow);
            };

            frmFile.Show();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

            _isPoshe = false;
            try
            {
                frmCustomerSearchDet.Close();
                panelBody.Controls.Remove(frmCustomerSearchDet);

                panelSearch.Dock = DockStyle.Top;
                panelRecordGrid.Dock = DockStyle.Bottom;
                btnDeleteFilter.PerformClick();
            }
            catch (Exception)
            {


            }


            this.Refresh();
        }

        private void btnN_Click(object sender, EventArgs e)
        {
            pageNumber++;
            FillData();
        }

        private void btnP_Click(object sender, EventArgs e)
        {
            pageNumber--;
            FillData();
        }



        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToString("HH:mm:ss");
            if (ClsBaseSetting.UserReminderList != null)
            {
                var alarm = ClsBaseSetting.UserReminderList.Where(x => x.DateReminder <= DateTime.Now).ToList();
                if (alarm != null && alarm.Count > 0)
                {
                    FrmReminder frmReminder;
                    timer1.Stop();
                    foreach (var item in alarm)
                    {
                        frmReminder = new FrmReminder(item.ID);
                        frmReminder.ShowDialog();
                    }
                    timer1.Start();
                }
            }
        }

        private void tabControlSearch_TabIndexChanged(object sender, EventArgs e)
        {
            if (tabControlSearch.SelectedTabIndex == 0)
            {
                tabMalek.Visible = true;
            }
            else
            {
                tabMalek.Visible = false;
            }

            SetButtonSearch();
        }

        private void btnSearchMain_Click(object sender, EventArgs e)
        {
            pageNumber = 1;
            //tabMalek.Visible = false;
            popupList.Visible = false;
            tabControlSearch.Visible = false;
            _idCustomer = 0;
            StartSlide(false);
            gmain.Visible = true;
            _ownersBL = new OwnersBL();
            List<long> RegionItem = new List<long>();
            if (checkListRegion.CheckedItems.Count > 0)
                RegionItem = checkListRegion.CheckedItems.Cast<chkItem>().Select(x => x.Id).ToList();
            int archive = 0;
            if (chkIsDelete.Checked)
            { archive = 1; }
            else if (chkIsDelete_NotDelete.Checked) { archive = -1; }

            DateTime? _startdate = null;
            DateTime? _enddate = null;
            if (clsDateStart.Text != "____/__/__")
                _startdate = clsDateStart.Text.ToMiladiDate();
            if (clsDateEnd.Text != "____/__/__")
                _enddate = clsDateEnd.Text.ToMiladiDate();


            long _filetype = chkAparteman.Checked ? 1 : chkEdari.Checked ? 2 : chkMaghaze.Checked ? 4 : chkMostaghelat.Checked ? 3 : 0;
            int? tabaghe_az = null, tabaghe_ta = null;
            if (!string.IsNullOrEmpty(txttabaghe_az.Text)) tabaghe_az = txttabaghe_az.Text.ToInt32OrDefault();
            if (!string.IsNullOrEmpty(txttabaghe_ta.Text)) tabaghe_ta = txttabaghe_ta.Text.ToInt32OrDefault();
            if (txtSize.Text.ToInt32OrDefault() > 0)
                pageSize = txtSize.Text.ToInt32OrDefault();
            var query = _ownersBL.GetListFileSearch(_idCustomer, pageNumber, pageSize, txtCode.Text, txtMName.Text, txtMTel1.Text,
                chkForosh.Checked, chkPishForosh.Checked, chkEjare.Checked, _filetype, txtAddress.Text, RegionItem, txtprice1_s.Text.Replace(",", ""),
                txtprice1_e.Text.Replace(",", ""), txtprice2_s.Text.Replace(",", ""), txtprice2_e.Text.Replace(",", ""), txtDet.Text, archive, txtkhab_az.Text.ToInt32OrDefault(), txtkhab_ta.Text.ToInt32OrDefault(),
                txtsenbana_az.Text.ToInt32OrDefault(), txtsenbana_ta.Text.ToInt32OrDefault(), tabaghe_az, tabaghe_ta,
                txtVahed_az.Text.ToInt32OrDefault(), txtvahed_ta.Text.ToInt32OrDefault(), chkAsansor.Checked, chkParkingmotor.Checked, chkAnbari.Checked, txtVahedinTbaghe_az.Text.ToInt32OrDefault(), txtVahedinTbaghe_ta.Text.ToInt32OrDefault(),
                txtTabaghat_az.Text.ToInt32OrDefault(), txtTabaghat_ta.Text.ToInt32OrDefault(), txtParking_az.Text, txtParking_ta.Text, txtAsansorTabaghe.Text.ToInt32OrDefault(), txtMetrazh_az.Text.ToInt32OrDefault(), txtMetrazh_ta.Text.ToInt32OrDefault(),txtmasahat_az.Text.ToDecimalOrDefault(),txtmasahat_ta.Text.ToDecimalOrDefault(),
                _startdate, _enddate, _isPoshe: _isPoshe);
            _lstowner = query;
            try
            {
                _lstfinal = query.Items
                .Select(x => new GridFileListDto
                {
                    ID = x.ID,
                    Address = _isCustomerView && !string.IsNullOrEmpty(x.Address)
    ? (x.Address.Contains("پلاک")
        ? x.Address.Substring(0, x.Address.IndexOf("پلاک"))
        : x.Address)
    : x.Address,
                    Anbari = x.Anbari,
                    Asansor = x.Asansor,
                    ContractType = x.ContractType,
                    CreationDate = x.CreationDate,
                    FileType = x.FileType,
                    MName = x.MName,
                    Parking = x.Parking,
                    Price1 = x.Price1,
                    Price2 = x.Price2,
                    Region = x.Region,
                    Senbana = x.Senbana == 0 ? "نوساز" : x.Senbana.ToString(),
                    Tabaghe = x.Tabaghe == 0 ? "همکف" : x.Tabaghe == -1 ? "زیر همکف" : x.Tabaghe.ToString(),
                    Zirbana = x.Zirbana,
                    LastDate = x.LastDate,
                    isDeleted = x.isDeleted,
                    Masahat = x.Masahat,
                    Tol = x.Tol,
                    Newrecord = x.Newrecord,
                    Customerid = x.Customerid,
                    ReturnTime = x.ReturnTime
                })
                .ToList();



            }
            catch (Exception ex)
            {


            }
            gmain.DataSource = null;
            gmain.DataSource = _lstfinal;
            lblSumRecord.Text = query.TotalCount.ToString();
            lblfrom.Text = query.PageNumber.ToString();
            lblTo.Text = query.TotalPages.ToString();
            if (query.PageNumber == 1)
            {
                btnP.Enabled = false;
            }
            else btnP.Enabled = true;
            if (query.PageNumber == query.TotalPages) btnN.Enabled = false; else btnN.Enabled = true;
            if (query.TotalCount == 0)
            {
                gmain.Visible = false;
            }
            if (chkMostaghelat.Checked)
            {
                gmain.RootTable.Columns["Masahat"].Visible = true;
                gmain.RootTable.Columns["Tol"].Visible = true;
                gmain.RootTable.Columns["Anbari"].Visible = false;
            }
            else
            {
                gmain.RootTable.Columns["Masahat"].Visible = false;
                gmain.RootTable.Columns["Tol"].Visible = false;
                gmain.RootTable.Columns["Anbari"].Visible = true;
            }
            gmain.Select();
            gmain.Focus();
        }

        public void FillData(bool _type = true)
        {
            _ownersBL = new OwnersBL();
            gmain.Visible = true;
            List<long> RegionItem = new List<long>();
            if (checkListRegion.CheckedItems.Count > 0)
                RegionItem = checkListRegion.CheckedItems.Cast<chkItem>().Select(x => x.Id).ToList();

            long _filetype = chkAparteman.Checked ? 1 : chkEdari.Checked ? 2 : chkMaghaze.Checked ? 4 : chkMostaghelat.Checked ? 3 : 0;
            int archive = 0;
            if (chkIsDelete.Checked)
            {
                archive = 1;
            }
            else if (chkIsDelete_NotDelete.Checked)
            { archive = -1; }
            DateTime? _startdate = null;
            DateTime? _enddate = null;
            if (clsDateStart.Text != "____/__/__")
            {
                if (clsDateStart.Tag?.ToString().Length > 0)
                    _startdate = (clsDateStart.Text + " " + clsDateStart.Tag.ToString() + ":00").ToMiladiDate();
                else
                    _startdate = clsDateStart.Text.ToMiladiDate();
            }

            if (clsDateEnd.Text != "____/__/__")
                _enddate = clsDateEnd.Text.ToMiladiDate();
            int? tabaghe_az = null, tabaghe_ta = null;
            if (!string.IsNullOrEmpty(txttabaghe_az.Text)) tabaghe_az = txttabaghe_az.Text.ToInt32OrDefault();
            if (!string.IsNullOrEmpty(txttabaghe_ta.Text)) tabaghe_ta = txttabaghe_ta.Text.ToInt32OrDefault();
            if (txtSize.Text.ToInt32OrDefault() > 0)
                pageSize = txtSize.Text.ToInt32OrDefault();
            var query = _ownersBL.GetListFileSearch(_idCustomer, pageNumber, pageSize, txtCode.Text, txtMName.Text, txtMTel1.Text,
                chkForosh.Checked, chkPishForosh.Checked, chkEjare.Checked, _filetype, txtAddress.Text, RegionItem, txtprice1_s.Text.Replace(",", ""),
                txtprice1_e.Text.Replace(",", ""), txtprice2_s.Text.Replace(",", ""), txtprice2_e.Text.Replace(",", ""), txtDet.Text, archive, txtkhab_az.Text.ToInt32OrDefault(), txtkhab_ta.Text.ToInt32OrDefault(),
                txtsenbana_az.Text.ToInt32OrDefault(), txtsenbana_ta.Text.ToInt32OrDefault(), tabaghe_az, tabaghe_ta, txtVahed_az.Text.ToInt32OrDefault(), txtvahed_ta.Text.ToInt32OrDefault(), chkAsansor.Checked, chkParkingmotor.Checked, chkAnbari.Checked,
                txtVahedinTbaghe_az.Text.ToInt32OrDefault(), txtVahedinTbaghe_ta.Text.ToInt32OrDefault(),
                txtTabaghat_az.Text.ToInt32OrDefault(), txtTabaghat_ta.Text.ToInt32OrDefault(), txtParking_az.Text, txtParking_ta.Text, txtAsansorTabaghe.Text.ToInt32OrDefault(), txtMetrazh_az.Text.ToInt32OrDefault(), txtMetrazh_ta.Text.ToInt32OrDefault(),txtmasahat_az.Text.ToDecimalOrDefault(),txtmasahat_ta.Text.ToDecimalOrDefault(),
                _startdate, _enddate, _type, _isPoshe);
            _lstowner = query;
            _lstowner = query;
            try
            {
                _lstfinal = query.Items
                .Select(x => new GridFileListDto
                {
                    ID = x.ID,
                    Address = _isCustomerView && !string.IsNullOrEmpty(x.Address)
    ? (x.Address.Contains("پلاک")
        ? x.Address.Substring(0, x.Address.IndexOf("پلاک"))
        : x.Address)
    : x.Address,
                    Anbari = x.Anbari,
                    Asansor = x.Asansor,
                    ContractType = x.ContractType,
                    CreationDate = x.CreationDate,
                    FileType = x.FileType,
                    MName = x.MName,
                    Parking = x.Parking,
                    Price1 = x.Price1,
                    Price2 = x.Price2,
                    Region = x.Region,
                    Senbana = x.Senbana == 0 ? "نوساز" : x.Senbana.ToString(),
                    Tabaghe = x.Tabaghe == 0 ? "همکف" : x.Tabaghe == -1 ? "زیر همکف" : x.Tabaghe.ToString(),
                    Zirbana = x.Zirbana,
                    LastDate = x.LastDate,
                    isDeleted = x.isDeleted,
                    Masahat = x.Masahat,
                    Tol = x.Tol,
                    Newrecord = x.Newrecord,
                    Customerid = x.Customerid,
                    ReturnTime = x.ReturnTime

                })
                .ToList();



            }
            catch (Exception ex)
            {


            }
            gmain.DataSource = _lstfinal;
            lblSumRecord.Text = query.TotalCount.ToString();
            lblfrom.Text = query.PageNumber.ToString();
            lblTo.Text = query.TotalPages.ToString();
            if (query.PageNumber == 1)
            {
                btnP.Enabled = false;
            }
            else btnP.Enabled = true;
            if (query.PageNumber == query.TotalPages) btnN.Enabled = false; else btnN.Enabled = true;
            if (chkMostaghelat.Checked)
            {
                gmain.RootTable.Columns["Masahat"].Visible = true;
                gmain.RootTable.Columns["Tol"].Visible = true;
                gmain.RootTable.Columns["Anbari"].Visible = false;
            }
            else
            {
                gmain.RootTable.Columns["Masahat"].Visible = false;
                gmain.RootTable.Columns["Tol"].Visible = false;
                gmain.RootTable.Columns["Anbari"].Visible = true;
            }
            gmain.Select();
            gmain.Focus();
            if (checkListRegion.Items.Count == 0)
            {
                List<Models.GNR.Region> _region = new List<Models.GNR.Region>();
                using (var db = new RealEstateContext())
                {

                    _region = db.Region_List.ToList();
                }

                checkListRegion.Items.Clear();
                foreach (var item in _region)
                {
                    checkListRegion.Items.Add(new chkItem { Id = item.ID, Name = item.Name }, false);
                }
            }


            //StatusSelections.FindObjectWithItem(UpdatedStatus).Selected = true;

            //cmbRegion.DisplayMember = "Name";
            //cmbRegion.ValueMember = "ID";
            //cmbRegion.DataSource = _region;


        }


        private void btnMalek_Click(object sender, EventArgs e)
        {
            ShowTabUnderButtonAnimated(btnMalek, tabControlSearch, tabMalek);
            //tabControlSearch.Visible = true;
            //tabMalek.Visible = true;
            //tabControlSearch.SelectedTabIndex = 0;
        }

        private void btnMoamleType_Click(object sender, EventArgs e)
        {
            ShowTabUnderButtonAnimated(btnMoamleType, tabControlSearch, tabMoamele);
        }


        private void btnFileType_Click(object sender, EventArgs e)
        {
            ShowTabUnderButtonAnimated(btnFileType, tabControlSearch, tabType);
        }

        private void btnRegion_Click(object sender, EventArgs e)
        {
            ShowTabUnderButtonAnimated(btnRegion, tabControlSearch, tabRegion);
        }

        private void btnPrice_Click(object sender, EventArgs e)
        {
            ShowTabUnderButtonAnimated(btnPrice, tabControlSearch, tabPrice);
        }

        private void btnOther_Click(object sender, EventArgs e)
        {
            ShowTabUnderButtonAnimated(btnOther, tabControlSearch, tabOther);
        }

        private void btnArchive_Click(object sender, EventArgs e)
        {
            ShowTabUnderButtonAnimated(btnArchive, tabControlSearch, tabbiygani);
        }

        private void btnDate_Click(object sender, EventArgs e)
        {
            ShowTabUnderButtonAnimated(btnDate, tabControlSearch, tabDate);
        }

        //// ---- متغیرها ----
        //private bool isAnimating = false;
        //private bool isOpen = false;
        //private DevComponents.DotNetBar.TabControlPanel currentPanel = null;
        //private int targetHeight = 0;

        // ---- نمایش TabControl زیر دکمه با انیمیشن ----
        private void ShowTabUnderButtonAnimated(ButtonX btn, DevComponents.DotNetBar.TabControl tabControl, DevComponents.DotNetBar.TabControlPanel tabPage)
        {
            if (isAnimating) return;

            // اگر همان پنل باز است => بسته شود
            if (isOpen && currentPanel == tabPage)
            {
                StartSlide(false);
                return;
            }

            // پنهان کردن پنل قبلی
            if (currentPanel != null && currentPanel != tabPage)
            {
                currentPanel.Visible = false;
            }

            currentPanel = tabPage;
            currentPanel.Visible = true;

            // انتخاب تب مربوطه
            var tab = tabControl.Tabs.Cast<DevComponents.DotNetBar.TabItem>()
                        .FirstOrDefault(t => t.AttachedControl == tabPage);
            if (tab != null)
                tabControl.SelectedTab = tab;

            // محاسبه ارتفاع بر اساس محتوای پنل
            targetHeight = GetPanelContentSize(tabPage).Height;
            if (tabPage.Controls.Count == 0)
                targetHeight = 50; // حداقل ارتفاع

            // موقعیت TabControl زیر دکمه (راست به چپ)
            var btnScreenPoint = btn.PointToScreen(Point.Empty);
            var formPoint = this.PointToClient(btnScreenPoint);
            tabControl.Location = new Point(formPoint.X + btn.Width - tabControl.Width, formPoint.Y + btn.Height + 2);

            // Width ثابت باشد
            tabControl.Width = tabControl.Width;
            tabControl.Height = 0;
            tabControl.Visible = true;
            tabControl.BringToFront();

            StartSlide(true);
        }

        // ---- انیمیشن باز/بسته شدن TabControl ----
        private void SlideTimer_Tick(object sender, EventArgs e)
        {
            if (!isAnimating) return;

            int step = 20;

            if (isOpen)
            {
                if (tabControlSearch.Height < targetHeight)
                {
                    tabControlSearch.Height += step;
                    if (tabControlSearch.Height > targetHeight)
                        tabControlSearch.Height = targetHeight;
                }
                else
                {
                    slideTimer.Stop();
                    isAnimating = false;
                }
            }
            else
            {
                if (tabControlSearch.Height > 0)
                {
                    tabControlSearch.Height -= step;
                    if (tabControlSearch.Height < 0)
                        tabControlSearch.Height = 0;
                }
                else
                {
                    popupList.Visible = false;
                    tabControlSearch.Visible = false;
                    slideTimer.Stop();
                    isAnimating = false;
                }
            }
        }

        // ---- شروع انیمیشن ----
        private void StartSlide(bool open)
        {
            isAnimating = true;
            isOpen = open;
            slideTimer.Start();
        }



        // ---- محاسبه اندازه واقعی محتوا ----
        private Size GetPanelContentSize(Control panel)
        {
            if (panel.Controls.Count == 0)
                return new Size(panel.Width, 50); // حداقل اندازه

            int maxRight = 0;
            int maxBottom = 0;

            foreach (Control c in panel.Controls)
            {
                int right = c.Right + c.Margin.Right;
                int bottom = c.Bottom + c.Margin.Bottom;

                if (right > maxRight)
                    maxRight = right;
                if (bottom > maxBottom)
                    maxBottom = bottom;
            }

            return new Size(maxRight + 10, maxBottom + 10);
        }

        private void CheckBoxMoamele_CheckedChanged(object sender, EventArgs e)
        {
            // کنترل فرستنده رو بگیر
            CheckBox selectedCheckBox = sender as CheckBox;

            // فقط اگر چک‌باکس انتخاب شد (نه لغو انتخاب)
            if (selectedCheckBox.Checked)
            {
                // در تمام چک‌باکس‌های داخل پنل بگرد
                foreach (Control ctrl in tabMoamele.Controls)
                {
                    if (ctrl is CheckBox cb && cb != selectedCheckBox)
                    {
                        cb.Checked = false; // بقیه رو غیرفعال کن
                    }
                }
            }
        }

        private void txtprice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }

        private void CheckBoxFileTypo_CheckedChanged(object sender, EventArgs e)
        {
            // کنترل فرستنده رو بگیر
            CheckBox selectedCheckBox = sender as CheckBox;

            // فقط اگر چک‌باکس انتخاب شد (نه لغو انتخاب)
            if (selectedCheckBox.Checked)
            {
                // در تمام چک‌باکس‌های داخل پنل بگرد
                foreach (Control ctrl in tabType.Controls)
                {
                    if (ctrl is CheckBox cb && cb != selectedCheckBox)
                    {
                        cb.Checked = false; // بقیه رو غیرفعال کن
                    }
                }
            }
        }

        private void txtprice1_s_TextChanged(object sender, EventArgs e)
        {
            FormatNumberTextBox(txtprice1_s);
        }

        private void txtprice1_e_TextChanged(object sender, EventArgs e)
        {
            FormatNumberTextBox(txtprice1_e);
        }

        private void txtprice2_s_TextChanged(object sender, EventArgs e)
        {
            FormatNumberTextBox(txtprice2_s);
        }

        private void txtprice2_e_TextChanged(object sender, EventArgs e)
        {
            FormatNumberTextBox(txtprice2_e);
        }

        private void btnDeleteFilter_Click(object sender, EventArgs e)
        {
            _idCustomer = 0;
            popupList.Visible = false;
            tabControlSearch.Visible = false;
            StartSlide(false);
            ResetFormControls(this); // کل فرم را پاک می‌کند
            FillData();
            SetButtonSearch();
        }
        private void ResetFormControls(Control parent)
        {
            foreach (Control ctrl in parent.Controls)
            {
                if (ctrl is TextBox txt)
                    txt.Clear();
                else if (ctrl is ComboBox cmb)
                    cmb.SelectedIndex = -1;
                else if (ctrl is CheckBox chk)
                    chk.Checked = false;
                else if (ctrl is RadioButton rdo)
                    rdo.Checked = false;
                else if (ctrl is NumericUpDown num)
                    num.Value = num.Minimum;
                else if (ctrl is DateTimePicker dtp)
                    dtp.Value = DateTime.Now;
                else if (ctrl is CheckedListBox chl)
                {
                    for (int i = 0; i < chl.Items.Count; i++)
                        chl.SetItemChecked(i, false);
                    txtRegion.Text = "";
                }
                // اگر کنترل شامل کنترل‌های دیگر هم هست (مثل Panel، GroupBox، TabPage)
                if (ctrl.HasChildren)
                    ResetFormControls(ctrl);

                btnMalek.TextColor = Color.Black;
                btnMalek.TextColor = Color.Black;
                btnArchive.TextColor = Color.Black;
                btnDate.TextColor = Color.Black;
                btnMoamleType.TextColor = Color.Black;
                btnRegion.TextColor = Color.Black;
                btnPrice.TextColor = Color.Black;
                btnOther.TextColor = Color.Black;
                btnFileType.TextColor = Color.Black;
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            if (gmain.CurrentRow == null)
            {
                FrmMsgBox.Show("یک ردیف از جدول را انتخاب کنید");
                return;
            }
            ViewFile();
        }

        private void cmbRegion_DropDownClosed(object sender, EventArgs e)
        {


        }

        private void txtRegion_MouseClick(object sender, MouseEventArgs e)
        {
            if (checkListRegion.Visible)
            {
                checkListRegion.Visible = false;
            }
            else
            {
                checkListRegion.Visible = true;
            }
        }

        private void checkListRegion_SelectedValueChanged(object sender, EventArgs e)
        {

        }

        private void checkListRegion_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void checkListRegion_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            this.BeginInvoke((MethodInvoker)delegate
            {
                var selected = checkListRegion.CheckedItems.Cast<object>().Select(x => x.ToString());
                txtRegion.Text = string.Join(" , ", selected);
            });
            //string str = "";
            //foreach (var item in checkListRegion.CheckedItems)
            //{
            //    if (str.Length == 0)
            //        str += item.ToString();
            //    else str += "," + item.ToString();
            //}
            //txtRegion.Text = str;
        }

        private void grdmain_Click(object sender, EventArgs e)
        {
            tabControlSearch.Visible = false;
            popupList.Visible = false;
        }

        private void panelSearch_Click(object sender, EventArgs e)
        {
            popupList.Visible = false;
            tabControlSearch.Visible = false;

            StartSlide(false);
        }

        private void panelBody_Click(object sender, EventArgs e)
        {
            popupList.Visible = false;
            tabControlSearch.Visible = false;

            StartSlide(false);
        }



        private void gmain_MouseClick(object sender, MouseEventArgs e)
        {
            popupList.Visible = false;
            tabControlSearch.Visible = false;

            StartSlide(false);

        }

        private void gmain_FormattingRow(object sender, RowLoadEventArgs e)
        {


        }

        private void btnMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;

        }

        private void FrmMain_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
            }

        }

        // استفاده:


        private void FormatNumberTextBox(Guna2TextBox txt)
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
            popupList.Visible = false;
        }

        private void btnSaveSearch_Click(object sender, EventArgs e)
        {
            popupList.Visible = false;
            tabControlSearch.Visible = false;
            if (btnMoamleType.BackColor != Color.LightGreen || btnFileType.BackColor != Color.LightGreen)
            {
                FrmMsgBox.Show("نوع معامله و نوع ملک باید مشخص باشد");
                return;
            }
            StartSlide(false);
            List<long> RegionItem = new List<long>();
            if (checkListRegion.CheckedItems.Count > 0)
                RegionItem = checkListRegion.CheckedItems.Cast<chkItem>().Select(x => x.Id).ToList();
            int archive = 0;
            if (chkIsDelete.Checked)
            { archive = 1; }
            else if (chkIsDelete_NotDelete.Checked) { archive = -1; }

            long _filetype = chkAparteman.Checked ? 1 : chkEdari.Checked ? 2 : chkMaghaze.Checked ? 4 : chkMostaghelat.Checked ? 3 : 0;

            FileSearchFilterDto fileSearchFilterDto = new FileSearchFilterDto()
            {
                Address = txtAddress.Text,
                Code = txtCode.Text,
                Ejare = chkEjare.Checked,
                FileType = _filetype,
                Forosh = chkForosh.Checked,
                IsDelete = archive,
                MName = txtMName.Text,
                MTel = txtMTel1.Text,
                PishForosh = chkPishForosh.Checked,
                Price1_End = txtprice1_e.Text,
                Price1_Start = txtprice1_s.Text,
                Price2_End = txtprice2_e.Text,
                Price2_Start = txtprice2_s.Text,
                Region = RegionItem,
                Tozihat = txtDet.Text,
                Khab_az = string.IsNullOrEmpty(txtkhab_az.Text) ? null : txtkhab_az.Text?.ToInt32OrDefault(),
                Khab_ta = string.IsNullOrEmpty(txtkhab_ta.Text) ? null : txtkhab_ta.Text?.ToInt32OrDefault(),
                Senbana_az = string.IsNullOrEmpty(txtsenbana_az.Text) ? null : txtsenbana_az.Text?.ToInt32OrDefault(),
                Senbana_ta = string.IsNullOrEmpty(txtsenbana_ta.Text) ? null : txtsenbana_ta.Text?.ToInt32OrDefault(),
                Tabaghe_az = string.IsNullOrEmpty(txttabaghe_az.Text) ? null : txttabaghe_az.Text?.ToInt32OrDefault(),
                Tabaghe_ta = string.IsNullOrEmpty(txttabaghe_ta.Text) ? null : txttabaghe_ta.Text?.ToInt32OrDefault(),
                Vahed_az = string.IsNullOrEmpty(txtVahed_az.Text) ? null : txtVahed_az.Text?.ToInt32OrDefault(),
                Vahed_ta = string.IsNullOrEmpty(txtvahed_ta.Text) ? null : txtvahed_ta.Text?.ToInt32OrDefault(),
                VahedinTbaghe_az = string.IsNullOrEmpty(txtVahedinTbaghe_az.Text) ? null : txtVahedinTbaghe_az.Text?.ToInt32OrDefault(),
                VahedinTbaghe_ta = string.IsNullOrEmpty(txtVahedinTbaghe_ta.Text) ? null : txtVahedinTbaghe_ta.Text?.ToInt32OrDefault(),
                Tabaghat_az = string.IsNullOrEmpty(txtTabaghat_az.Text) ? null : txtTabaghat_az.Text?.ToInt32OrDefault(),
                Tabaghat_ta = string.IsNullOrEmpty(txtTabaghat_ta.Text) ? null : txtTabaghat_ta.Text?.ToInt32OrDefault(),
                Parking_az = string.IsNullOrEmpty(txtParking_az.Text) ? null : txtParking_az.Text,
                Parking_ta = string.IsNullOrEmpty(txtParking_ta.Text) ? null : txtParking_ta.Text,
                AsansorTabaghe = string.IsNullOrEmpty(txtAsansorTabaghe.Text) ? null : txtAsansorTabaghe.Text?.ToInt32OrDefault(),
                Metrazh_az = string.IsNullOrEmpty(txtMetrazh_az.Text) ? null : txtMetrazh_az.Text?.ToInt32OrDefault(),
                Metrazh_ta = string.IsNullOrEmpty(txtMetrazh_ta.Text) ? null : txtMetrazh_ta.Text?.ToInt32OrDefault(),
                Asansor = chkAsansor.Checked,
                Parking = chkParkingmotor.Checked,
                Anbari = chkAnbari.Checked,
                Masahat_az= string.IsNullOrEmpty(txtmasahat_az.Text) ? null : txtmasahat_az.Text?.ToDecimalOrDefault(),
                Masahat_ta= string.IsNullOrEmpty(txtmasahat_ta.Text) ? null : txtmasahat_ta.Text?.ToDecimalOrDefault(),



                //startdate = clsDateStart.Text,
                //enddate = clsDateEnd.Text


            };
            FrmCustomerSearch frmCustomerSearch;
            if (_idCustomer == 0)
                frmCustomerSearch = new FrmCustomerSearch(fileSearchFilterDto);
            else
                frmCustomerSearch = new FrmCustomerSearch(_idCustomer, fileSearchFilterDto, true);
            if (frmCustomerSearch.ShowDialog() == DialogResult.OK)
            {
                FrmMsgBox.Show("ذخیره شد");
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            if (WindowState == FormWindowState.Maximized)
            {
                Rectangle screen = Screen.PrimaryScreen.WorkingArea;
                this.Location = new Point(0, 0);
                this.Size = new Size(screen.Width, screen.Height);
            }
        }

        private void tabControlSearch_VisibleChanged(object sender, EventArgs e)
        {
            SetButtonSearch();
        }

        private void SetButtonSearch()
        {
            popupList.Visible = false;
            var result = GetTabsWithData(tabControlSearch);
            if (result.Count() == 0)
            {
                btnMalek.BackColor = Color.Transparent;
                btnMalek.BackColor = Color.Transparent;
                btnArchive.BackColor = Color.Transparent;
                btnDate.BackColor = Color.Transparent;
                btnMoamleType.BackColor = Color.Transparent;
                btnRegion.BackColor = Color.Transparent;
                btnPrice.BackColor = Color.Transparent;
                btnOther.BackColor = Color.Transparent;
                btnFileType.BackColor = Color.Transparent;
            }
            foreach (var tab in result)
            {

                if (tab == tabMalek) btnMalek.BackColor = Color.LightGreen;
                if (tab == tabbiygani) btnArchive.BackColor = Color.LightGreen;
                if (tab == tabDate) btnDate.BackColor = Color.LightGreen;
                if (tab == tabMoamele) btnMoamleType.BackColor = Color.LightGreen;
                if (tab == tabRegion) btnRegion.BackColor = Color.LightGreen;
                if (tab == tabPrice) btnPrice.BackColor = Color.LightGreen;
                if (tab == tabOther) btnOther.BackColor = Color.LightGreen;
                if (tab == tabType) btnFileType.BackColor = Color.LightGreen;


            }
        }

        private void btnCustomerSearchDetView_Click(object sender, EventArgs e)
        {
            _isPoshe = false;

            try
            {

                if (!panelBody.Controls.Contains(frmCustomerSearchDet))
                {
                    frmCustomerSearchDet = new FrmCustomerSearchDet();
                    panelSearch.Dock = DockStyle.None;
                    panelRecordGrid.Dock = DockStyle.None;
                    frmCustomerSearchDet.Dock = DockStyle.Fill;
                    frmCustomerSearchDet.TopLevel = false;
                    panelBody.Controls.Add(frmCustomerSearchDet);
                    frmCustomerSearchDet.TopLevel = false;
                    frmCustomerSearchDet.DataSaved += () =>
                    {


                        if (frmCustomerSearchDet.fileSearchFilterDto != null)
                        {
                            var filter = frmCustomerSearchDet.fileSearchFilterDto;
                            _idCustomer = frmCustomerSearchDet._idCustomer;
                            txtMName.Text = filter.MName;
                            txtCode.Text = filter.Code;
                            txtMTel1.Text = filter.MTel;
                            if (filter.Forosh)
                                chkForosh.Checked = true;
                            else if (filter.PishForosh)
                                chkPishForosh.Checked = true;
                            else if (filter.Ejare)
                                chkEjare.Checked = true;
                            if (filter.FileType == 1)
                                chkAparteman.Checked = true;
                            else if (filter.FileType == 2)
                                chkEdari.Checked = true;
                            else if (filter.FileType == 3)
                                chkMostaghelat.Checked = true;
                            else if (filter.FileType == 4)
                                chkMaghaze.Checked = true;
                            txtAddress.Text = filter.Address;
                            try
                            {
                                //var items = checkListRegion.Items.Cast<chkItem>().ToList();

                                //for (int i = 0; i < checkListRegion.Items.Count; i++)
                                //{
                                //    var item = (chkItem)checkListRegion.Items[i];
                                //    if (filter.Region.Contains(item.Id))
                                //    {
                                //        checkListRegion.SetItemChecked(i, true);
                                //    }
                                //    else
                                //    {
                                //        checkListRegion.SetItemChecked(i, false);
                                //    }
                                //}


                                //foreach (var itemregion in filter.Region)
                                //{
                                //    foreach (var item in items)
                                //    {
                                //        if (item.Id == itemregion)
                                //        {
                                //            int index = checkListRegion.Items.IndexOf(item);
                                //            checkListRegion.SetItemChecked(index, true);
                                //        }
                                //    }
                                //}

                                // فرض می‌کنیم filter.Region یک List<long> است
                                foreach (chkItem item in checkListRegion.Items.Cast<chkItem>().ToList())
                                {
                                    if (filter.Region.Contains(item.Id))
                                    {
                                        int index = checkListRegion.Items.IndexOf(item);
                                        checkListRegion.SetItemChecked(index, true);
                                    }
                                    else
                                    {
                                        int index = checkListRegion.Items.IndexOf(item);
                                        checkListRegion.SetItemChecked(index, false);
                                    }
                                }




                            }
                            catch (Exception ex)
                            {

                            }
                            txtprice1_s.Text = filter.Price1_Start;
                            txtprice1_e.Text = filter.Price1_End;
                            txtprice2_s.Text = filter.Price2_Start;
                            txtprice2_e.Text = filter.Price2_End;
                            txtDet.Text = filter.Tozihat;
                            txtkhab_az.Text = filter.Khab_az.ToStringOrDefult();
                            txtkhab_ta.Text = filter.Khab_ta.ToStringOrDefult();
                            txtsenbana_az.Text = filter.Senbana_az.ToStringOrDefult();
                            txtsenbana_ta.Text = filter.Senbana_ta.ToStringOrDefult();
                            txttabaghe_az.Text = filter.Tabaghe_az.ToStringOrDefult();
                            txttabaghe_ta.Text = filter.Tabaghe_ta.ToStringOrDefult();
                            txtVahed_az.Text = filter.Vahed_az.ToStringOrDefult();
                            txtvahed_ta.Text = filter.Vahed_ta.ToStringOrDefult();
                            chkAnbari.Checked = filter.Anbari;
                            chkParkingmotor.Checked = filter.Parking;
                            chkAsansor.Checked = filter.Asansor;
                            txtMetrazh_az.Text = filter.Metrazh_az.ToStringOrDefult();
                            txtMetrazh_ta.Text = filter.Metrazh_ta.ToStringOrDefult();
                            txtTabaghat_az.Text = filter.Tabaghat_az.ToStringOrDefult();
                            txtTabaghat_ta.Text = filter.Tabaghat_ta.ToStringOrDefult();
                            txtVahedinTbaghe_az.Text = filter.VahedinTbaghe_az.ToStringOrDefult();
                            txtVahedinTbaghe_ta.Text = filter.VahedinTbaghe_ta.ToStringOrDefult();
                            txtParking_az.Text = filter.Parking_az.ToStringOrDefult();
                            txtParking_ta.Text = filter.Parking_ta.ToStringOrDefult();
                            txtAsansorTabaghe.Text = filter.AsansorTabaghe.ToStringOrDefult();
                            if (filter.Forosh || filter.PishForosh)
                            {

                                var date = DateTime.Now.AddMonths(-12);
                                clsDateStart.Text = date.ToShamsiDate();
                                clsDateEnd.Text = DateTime.Now.ToShamsiDate();


                            }
                            else
                            {
                                var date = DateTime.Now.AddMonths(-4);
                                clsDateStart.Text = date.ToShamsiDate();
                                clsDateEnd.Text = DateTime.Now.ToShamsiDate();
                            }
                            //if (frmCustomerSearchDet._idCustomer == 0)
                            //{
                            //    _idCustomer = 0;
                            //    if (filter.Forosh || filter.PishForosh)
                            //    {

                            //        var date = DateTime.Now.AddMonths(-12);
                            //        clsDateStart.Text = date.ToShamsiDate();
                            //        clsDateEnd.Text = DateTime.Now.ToShamsiDate();


                            //    }
                            //    else
                            //    {
                            //        var date = DateTime.Now.AddMonths(-4);
                            //        clsDateStart.Text = date.ToShamsiDate();
                            //        clsDateEnd.Text = DateTime.Now.ToShamsiDate();
                            //    }

                            //}
                            //else
                            //{
                            //    _idCustomer = frmCustomerSearchDet._idCustomer;
                            //    clsDateStart.Text = frmCustomerSearchDet._Customer.ModificationDate?.ToShamsiDate() ?? frmCustomerSearchDet._Customer.CreationDate.ToShamsiDate();
                            //    clsDateStart.Tag = frmCustomerSearchDet._Customer.ModificationDate?.GetTime() ?? frmCustomerSearchDet._Customer.CreationDate.GetTime();
                            //    clsDateEnd.Text = DateTime.Now.ToShamsiDate();
                            //}

                            SetButtonSearch();
                        }
                        if (_idCustomer == 0)
                            FillData(true);
                        else
                            FillData(false);
                        //if (_idCustomer == 0)
                        //    gmain.RootTable.RowFormatStyle = new GridEXFormatStyle()
                        //    {
                        //        BackColor = Color.LightGray
                        //    };
                        //else
                        //{
                        //    gmain.RootTable.RowFormatStyle = new GridEXFormatStyle()
                        //    {
                        //        BackColor = Color.LightGreen
                        //    };
                        //}
                        frmCustomerSearchDet.Close();
                        panelBody.Controls.Remove(frmCustomerSearchDet);

                        panelSearch.Dock = DockStyle.Top;
                        panelRecordGrid.Dock = DockStyle.Bottom;
                    };

                    frmCustomerSearchDet.Show();
                    frmCustomerSearchDet.BringToFront();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void btnUserManage_Click(object sender, EventArgs e)
        {

            try
            {
                string _connectionstring_master = "";
                using (var db = new RealEstateContext())
                {
                    var _setting = db.Settings.FirstOrDefault();
                    _connectionstring_master = _setting.masterConnectionString;
                }
                if (_connectionstring_master == null || _connectionstring_master == "")
                {
                    FrmMsgBox.Show("ارتباط با سرور آنلان برقرار نیست", "خطا");
                    return;
                }

                FrmUserManage frmUserManage = new FrmUserManage(_connectionstring_master);
                if (frmUserManage.ShowDialog() == DialogResult.OK)
                {

                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public List<DevComponents.DotNetBar.TabControlPanel> GetTabsWithData(DevComponents.DotNetBar.TabControl tabControl)
        {
            List<DevComponents.DotNetBar.TabControlPanel> tabsWithData = new List<DevComponents.DotNetBar.TabControlPanel>();

            foreach (DevComponents.DotNetBar.TabItem tabItem in tabControl.Tabs)
            {
                var tab = tabItem.AttachedControl as DevComponents.DotNetBar.TabControlPanel;
                if (tab == null) continue;

                bool hasData = false;

                foreach (Control control in tab.Controls)
                {
                    if (control is Guna.UI2.WinForms.Guna2TextBox gtxt && !string.IsNullOrWhiteSpace(gtxt.Text))
                        hasData = true;

                    else if (control is TextBox txt && !string.IsNullOrWhiteSpace(txt.Text))
                        hasData = true;

                    else if (control is Guna.UI2.WinForms.Guna2CheckBox gchk && gchk.Checked)
                        hasData = true;

                    else if (control is CheckBox chk && chk.Checked)
                        hasData = true;

                    if (hasData) break;
                }

                if (hasData)
                    tabsWithData.Add(tab);
            }

            return tabsWithData;
        }
        public void SetDate(object sender, EventArgs e)
        {
            if (chkForosh.Checked || chkPishForosh.Checked)
            {
                var date = DateTime.Now.AddMonths(-12);
                clsDateStart.Text = date.ToShamsiDate();
                clsDateEnd.Text = DateTime.Now.ToShamsiDate();
            }
            else if (chkEjare.Checked)
            {
                var date = DateTime.Now.AddMonths(-4);
                clsDateStart.Text = date.ToShamsiDate();
                clsDateEnd.Text = DateTime.Now.ToShamsiDate();
            }
            else
            {
                clsDateStart.ClearValue();
                clsDateEnd.ClearValue();
            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (gmain.CurrentRow == null)
            {
                FrmMsgBox.Show("یک ردیف را انتخاب کنید");
                return;
            }
            if (ClsBaseSetting.UserID != 1 && ClsBaseSetting.UserIn.UserAccsessList.Where(x => x.AccessID == 9).Any() == false)
            {
                FrmMsgBox.Show("مجوز ندارید", "دسترسی", MsgBoxType.Error);
                return;
            }
            var _record = (GridFileListDto)gmain.CurrentRow.DataRow;
            if (FrmMsgBox.Show("آیا از حذف فایل  " + _record.MName + " مطمن هستید ؟ ", "حذف", MsgBoxType.Question, MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;
            if (_record.isDeleted) return;
            var result = _ownersBL.IsDelete(_record.ID);
            if (result.Status == OperationResult.OpreationResultStatus.Success)
            {
                FrmMsgBox.Show("با موفقیت انجام شد", "حذف", MsgBoxType.Info);
                FillData();
            }
        }

        private void gmain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnView.PerformClick();
            }
        }

        private void btnCustomer_Click(object sender, EventArgs e)
        {
            if (_isCustomerView)
            {
                _isCustomerView = false;
                btnCustomer.Symbol = "✅";

            }
            else
            {
                _isCustomerView = true;
                btnCustomer.Symbol = "❌";
            }
            FillData();
        }

        private void btnSetting_Click(object sender, EventArgs e)
        {
            string _connectionstring_master = "";
            using (var db = new RealEstateContext())
            {
                var _setting = db.Settings.FirstOrDefault();
                _connectionstring_master = _setting.masterConnectionString;
            }
            if (_connectionstring_master == null || _connectionstring_master == "")
            {
                FrmMsgBox.Show("ارتباط با سرور آنلان برقرار نیست", "خطا");
                return;
            }
            FrmSetting frmSetting = new FrmSetting(_connectionstring_master);
            if (frmSetting.ShowDialog() == DialogResult.OK)
            {

                List<Models.GNR.Region> _region = new List<Models.GNR.Region>();
                using (var db = new RealEstateContext())
                {

                    _region = db.Region_List.ToList();
                }

                checkListRegion.Items.Clear();
                foreach (var item in _region)
                {
                    checkListRegion.Items.Add(new chkItem { Id = item.ID, Name = item.Name }, false);
                }
            }
        }

        private void txtAsansorTabaghe_TextChanged(object sender, EventArgs e)
        {
            if (txtAsansorTabaghe.Text.Length > 0)
            {
                txttabaghe_az.Clear();
                txttabaghe_ta.Clear();
                chkAsansor.Checked = false;
                txttabaghe_az.Enabled = false;
                txttabaghe_ta.Enabled = false;
                chkAsansor.Enabled = false;
            }
            else
            {

                txttabaghe_az.Enabled = true;
                txttabaghe_ta.Enabled = true;
                chkAsansor.Enabled = true;
            }
        }

        private void FrmMain_Shown(object sender, EventArgs e)
        {
            FrmSearchNews frmSearchNews = new FrmSearchNews();
            if (frmSearchNews.ShowDialog() == DialogResult.OK)
            {

            }
            if (ClsBaseSetting.UserID == 1)
            {
                try
                {
                    using (var db = new RealEstateContext())
                    {
                        var today = DateTime.Now.Date;
                        var limitDate = today.AddMonths(-11);

                        var lst = db.Owners_List
                            .Where(x =>
                                x.IsDelete &&
                                x.IsDeleteDateTime.HasValue &&
                                x.Ejare &&
                                x.IsDeleteDateTime.Value <= limitDate
                            )
                            .ToList();

                        foreach (var x in lst)
                        {
                            x.ReturnTime = DateTime.Now;
                            x.IsDelete = false;
                            x.IsDeleteDateTime = null;
                        }

                        db.SaveChanges();
                    }

                }
                catch (Exception ex)
                {


                }
            }

            txtversion.Text = "V : " + Application.ProductVersion.ToString();
        }

        private void lblUser_Click(object sender, EventArgs e)
        {
            FrmPassword frmPassword = new FrmPassword();
            if (frmPassword.ShowDialog() == DialogResult.OK)
            {

            }
        }

        private void btnMaxim_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
                this.WindowState = FormWindowState.Normal;
            else
                this.WindowState = FormWindowState.Maximized;
        }

        private void btnPoshe_Click(object sender, EventArgs e)
        {
            _isPoshe = true;


            try
            {
                frmCustomerSearchDet.Close();
                panelBody.Controls.Remove(frmCustomerSearchDet);

                panelSearch.Dock = DockStyle.Top;
                panelRecordGrid.Dock = DockStyle.Bottom;
                btnDeleteFilter.PerformClick();
            }
            catch (Exception)
            {


            }
            FillData();
        }

        private void tabControlSearch_SelectedTabChanged(object sender, TabStripTabChangedEventArgs e)
        {
            SetButtonSearch();
        }

        private void FrmMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btnSearch.PerformClick();
            }
            else if (e.KeyCode == Keys.F3)
            {
                btnDeleteFilter.PerformClick();
            }
        }



        private void tabPrice_Click(object sender, EventArgs e)
        {
            popupList.Visible = false;
        }

        private void btnremovefile_Click(object sender, EventArgs e)
        {
            var _selectrow = gmain.CurrentRow.Position;
            if (gmain.CurrentRow == null)
            {
                FrmMsgBox.Show("یک ردیف را انتخاب کنید");
                return;
            }
            if (ClsBaseSetting.UserID != 1)
            {
                FrmMsgBox.Show("مجوز ندارید", "دسترسی", MsgBoxType.Error);
                return;
            }
            var _record = (GridFileListDto)gmain.CurrentRow.DataRow;
            if (FrmMsgBox.Show("آیا از حذف فایل  " + _record.MName + " مطمن هستید ؟ ", "حذف کامل فایل", MsgBoxType.Question, MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;
            using (var db = new RealEstateContext())
            {
                try
                {

                    var file = db.Owners_List.Where(x => x.ID == _record.ID).FirstOrDefault();
                    db.OwnersDeleted_List.Add(new OwnersDeleted()
                    {
                        ID = Guid.NewGuid(),
                        OwnerGuiD = file.MasterID,

                    });
                    db.Owners_List.Remove(file);
                    
                    db.SaveChanges();
                    FrmMsgBox.Show("با موفقیت انجام شد", "حذف کامل فایل");
                    FillData();
                    gmain.MoveToRowIndex(_selectrow);
                }
                catch (Exception ex)
                {
                    FrmMsgBox.Show("  با خطا مواجه شد  " + ex.Message, "حذف کامل فایل");
                    gmain.MoveToRowIndex(_selectrow);
                    return;
                }

            }
        }

        private void btnreturnfile_Click(object sender, EventArgs e)
        {
            if (gmain.CurrentRow == null)
            {
                FrmMsgBox.Show("یک ردیف را انتخاب کنید");
                return;
            }
            if (ClsBaseSetting.UserID != 1 && ClsBaseSetting.UserIn.UserAccsessList.Where(x => x.AccessID == 9).Any() == false)
            {
                FrmMsgBox.Show("مجوز ندارید", "دسترسی", MsgBoxType.Error);
                return;
            }
            var _record = (GridFileListDto)gmain.CurrentRow.DataRow;
            if (FrmMsgBox.Show("آیا از تبدیل فایل  " + _record.MName + " مطمن هستید ؟ ", "تبدیل به فایل عادی", MsgBoxType.Question, MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;
            using (var db = new RealEstateContext())
            {
                try
                {
                    var file = db.Owners_List.Where(x => x.ID == _record.ID).FirstOrDefault();
                    file.ReturnTime = null;
                    db.SaveChanges();
                    FrmMsgBox.Show("با موفقیت انجام شد", "تبدیل به فایل عادی");
                    FillData();
                }
                catch (Exception ex)
                {
                    FrmMsgBox.Show("  با خطا مواجه شد  " + ex.Message, "تبدیل به فایل عادی");
                    return;
                }

            }
        }

        private void timerAlarm_Tick(object sender, EventArgs e)
        {
            FrmSearchNews frmSearchNews = new FrmSearchNews();
            if (frmSearchNews.ShowDialog() == DialogResult.OK)
            {

            }
        }

        private void btnUserActivity_Click(object sender, EventArgs e)
        {
            FrmUserActivityReport frmUserActivityReport = new FrmUserActivityReport();
            frmUserActivityReport.ShowDialog();
        }

        private void btnBackup_Click(object sender, EventArgs e)
        {
            using (var dialog = new SaveFileDialog())
            {
                dialog.Filter = "SQL Backup (*.bak)|*.bak";
                dialog.FileName = $"RealEstate_{DateTime.Now:yyyyMMdd_HHmmss}.bak";

                if (dialog.ShowDialog() != DialogResult.OK)
                    return;

                try
                {
                    BackupDatabase(dialog.FileName);
                    MessageBox.Show("بک‌آپ با موفقیت گرفته شد", "موفق", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "خطا در بک‌آپ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void SetCheckedItemsByIds(CheckedListBox listBox, List<long> ids)
        {
            // محافظت برای حالت null
            if (listBox == null || ids == null) return;

            // اطمینان از اجرا روی UI thread
            if (listBox.InvokeRequired)
            {
                listBox.Invoke(new Action(() => SetCheckedItemsByIds(listBox, ids)));
                return;
            }

            // جلوگیری از flicker
            listBox.BeginUpdate();

            try
            {
                // اگر آیتم‌ها ممکنه از نوع دیگری باشند، بهتره از Index و دستی چک کنیم
                for (int i = 0; i < listBox.Items.Count; i++)
                {
                    var raw = listBox.Items[i];

                    long itemId = -1;
                    bool gotId = false;

                    // 1) اگر کلاس chkItem هست
                    if (raw is chkItem ci)
                    {
                        itemId = ci.Id;
                        gotId = true;
                    }
                    else
                    {
                        // 2) اگر آیتم یک عدد یا رشته است، سعی کن پارس کنی
                        if (raw is long l) { itemId = l; gotId = true; }
                        else if (raw is int ii) { itemId = ii; gotId = true; }
                        else if (raw is string s)
                        {
                            if (long.TryParse(s, out var parsed)) { itemId = parsed; gotId = true; }
                        }
                    }

                    // اگر نتونستیم آی‌دی استخراج کنیم، آیتم رو uncheck کن
                    if (!gotId)
                    {
                        listBox.SetItemChecked(i, false);
                        continue;
                    }

                    // مقایسه با لیست ids
                    bool shouldCheck = ids.Contains(itemId);

                    // اگر میخوای هر بار فقط تیک بخوره ولی برداشته نشه، از این خط استفاده نکن
                    listBox.SetItemChecked(i, shouldCheck);
                }
            }
            finally
            {
                listBox.EndUpdate();
            }
        }

        private void btnRiminder_Click(object sender, EventArgs e)
        {
            FrmReminderList frmReminder = new FrmReminderList();
            frmReminder.ShowDialog();
        }

        private void txtversion_DoubleClick(object sender, EventArgs e)
        {
            if (ClsBaseSetting.UserID == 1)
            {
                using (var db = new RealEstateContext())
                {
                    StringBuilder st = new StringBuilder();
                    var query = db.SyncState_List.FirstOrDefault();

                    if (query != null)
                    {
                        st.AppendLine("آخرین بروزرسانی ارسال دیتا : " + query.LastPush?.ToString("yyyy/MM/dd HH:mm:ss"));
                        st.AppendLine("***********");
                        st.AppendLine("آخرین  بروزرسانی دریافت دیتا : " + query.LastPull?.ToString("yyyy/MM/dd HH:mm:ss"));
                        st.AppendLine("***********");
                        st.AppendLine("آخرین  بروزرسانی حذف دیتا : " + query.LastDeleteOwner?.ToString("yyyy/MM/dd HH:mm:ss"));
                        st.AppendLine("***********");
                        st.AppendLine("آخرین بروزرسانی ارسال عکس : " + query.LastPushImage?.ToString("yyyy/MM/dd HH:mm:ss"));
                        st.AppendLine("***********");
                        st.AppendLine("آخرین بروزرسانی دریافت عکس : " + query.LastPullImage?.ToString("yyyy/MM/dd HH:mm:ss"));

                        MessageBox.Show(st.ToString());
                    }
                }
            }
        }

        private void tabControlSearch_Click(object sender, EventArgs e)
        {

        }

        private void txtSize_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyData== Keys.Enter)
            {
                btnSearchMain.PerformClick();
            }
        }

        private void BackupDatabase(string filePath)
        {
            var connectionString = ClsConnection.Connect();

            using (var con = new SqlConnection(connectionString))
            {
                con.Open();

                var sql = $@"
BACKUP DATABASE [{con.Database}]
TO DISK = @path
WITH INIT, FORMAT, STATS = 10;
";

                using (var cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@path", filePath);
                    cmd.CommandTimeout = 0; // بک‌آپ‌های سنگین هم کم نیارن
                    cmd.ExecuteNonQuery();
                }
            }
        }


    }
}
