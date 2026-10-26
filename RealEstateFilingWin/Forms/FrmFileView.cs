//using RealEstateFiling.Services;
//using RealEstateFilingWin.GNR.Models;
//using RealEstateFilingWin.Models.FIL;
//using RealEstateFilingWin.Models.GNR;
//using RealEstateFilingWin.Services.FIL;
//using RealEstateFilingWin.Utility;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
//using System.Drawing;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Forms;
//using System.Drawing.Printing;
//using System.Reflection;



//namespace RealEstateFilingWin.Forms
//{
//    public partial class FrmFileView : FrmBase
//    {
//        public event Action DataSaved;
//        public Models.FIL.Owners _PEntity = new Owners();
//        List<OwnerDet> _lstOwnerDet = new List<OwnerDet>();
//        private long _id = 0;
//        OwnersBL _ownersBL = new OwnersBL();
//        List<FileType> _fileType = new List<FileType>();
//        List<Models.GNR.Region> _regions = new List<Models.GNR.Region>();
//        List<SanadStatus> _sanadStatuses = new List<SanadStatus>();
//        List<FileStatus> _fileStatuses = new List<FileStatus>();
//        List<Views> _views = new List<Views>();
//        private Bitmap memoryImage;
//        List<long> _listid;
//        int _index;
//        RealEstateContext db = new RealEstateContext();
//        public FrmFileView(long ID, List<long> arraysid)
//        {
//            InitializeComponent();
//            _id = ID;
//            _listid = arraysid.Distinct().ToList();
//            FillCmb();

//        }

//        private void txtViewID_Click(object sender, EventArgs e)
//        {

//        }

//        private void FrmFileView_Load(object sender, EventArgs e)
//        {
//            if (_id != 0)
//            {

//                // هر بار context جدید ساخته شود تا از کش و tracking جدا باشد

//                _PEntity = EfFormBinder.LoadWithDetails<Owners>(db, _id, this, nameof(Owners.OwnerDetsList));

//                if (_PEntity?.OwnerDetsList != null)
//                {
//                    _lstOwnerDet = _PEntity.OwnerDetsList.ToList();
//                    grdDet.DataSource = _lstOwnerDet;
//                    if (_lstOwnerDet.Count() > 0)
//                    {
//                        grdDet.MoveFirst();
//                        txtPrice1.Text = _lstOwnerDet[0].Price1.ToString("N0");
//                        txtPrice2.Text = _lstOwnerDet[0].Price2.ToString("N0");
//                        txtAddress.Text = _PEntity.Address + " _ پلاک " + _PEntity.Plak + " _  واحد " + _PEntity.Vahed;
//                        txtSenbana.Text = _PEntity.Senbana == 0 ? "نوساز" : _PEntity.Senbana.ToString();
//                    }

//                }
//                if (_PEntity.IsDelete)
//                    this.BackColor = Color.FromArgb(200, 200, 200);
//                else if(_PEntity.FiletypeID == 1)
//                    this.BackColor = Color.FromArgb(255, 210, 210);
//                else if (_PEntity.FiletypeID == 2)
//                    this.BackColor = Color.FromArgb(255, 245, 200);
//                else if (_PEntity.FiletypeID == 3)
//                    this.BackColor = Color.FromArgb(180, 220, 180);
//                else if (_PEntity.FiletypeID == 4)
//                    this.BackColor = Color.FromArgb(160, 200, 255);



//                FillOther();
//                if (_PEntity.IsDelete)
//                {
//                    btnreturn.Enabled = true;
//                    btnDelete.Enabled = false;
//                }
//                else
//                {
//                    btnreturn.Enabled = false;
//                    btnDelete.Enabled = true;
//                }

//            }

//        }

//        private void btnClose_Click(object sender, EventArgs e)
//        {
//            DialogResult = DialogResult.OK;
//            DataSaved?.Invoke();
//            this.Close();
//        }

//        private void btnDelete_Click(object sender, EventArgs e)
//        {
//            if (FrmMsgBox.Show("آیا از انجام این عملیات مطمن هستید؟", "حذف", MsgBoxType.Question, MessageBoxButtons.YesNo) == DialogResult.Yes)
//            {
//                var _result = _ownersBL.IsDelete(_PEntity.ID);
//                if (_result.Status == OperationResult.OpreationResultStatus.Success)
//                {
//                    FrmMsgBox.Show("با موفقیت انجام شد", "حذف", MsgBoxType.Info);
//                    btnClose.PerformClick();
//                }
//            }
//        }

//        private void btnEdit_Click(object sender, EventArgs e)
//        {
//            FrmFile frmFile = new FrmFile(_PEntity.ID.ToInt64OrDefault());

//            if (frmFile.ShowDialog() == DialogResult.OK)
//            {
//                if (_id != 0)
//                {
//                    // هر بار context جدید ساخته شود تا از کش و tracking جدا باشد

//                    _PEntity = EfFormBinder.LoadWithDetails<Owners>(db, _id, this, nameof(Owners.OwnerDetsList));

//                    if (_PEntity?.OwnerDetsList != null)
//                    {
//                        _lstOwnerDet = _PEntity.OwnerDetsList.ToList();
//                        grdDet.DataSource = _lstOwnerDet;
//                    }
//                    if (_PEntity.FiletypeID == 1)
//                        this.BackColor = Color.FromArgb(255, 255, 192);


//                }
//                FillOther();
//            }
//        }

//        private void FillOther()
//        {
//            var op = _ownersBL.GetOperatorName(_PEntity.UserIDCreator, _PEntity.UserIDModifier);
//            txtCreatorName.Text = op.CreatorName;
//            txtModifierName.Text = op.ModifierName;
//            txtCreateDate.Text = _PEntity.CreationDate.ToShamsiDate();
//            txtModifierDate.Text = _PEntity.ModificationDate.ToShamsiDate();
//            txtFiletypeID.Text = _fileType.Where(x => x.ID == _PEntity.FiletypeID).FirstOrDefault().Name;
//            txtFileStatusID.Text = _fileStatuses.Where(x => x.ID == _PEntity.FileStatusID).FirstOrDefault().Name;
//            txtRegionID.Text = _regions.Where(x => x.ID == _PEntity.RegionID).FirstOrDefault().Name;
//            txtViewID.Text = _views.Where(x => x.ID == _PEntity.ViewID).FirstOrDefault().Name;
//        }

//        private void grdDet_MouseDown(object sender, MouseEventArgs e)
//        {
//            if (e.Button == MouseButtons.Left)
//            {
//                Models.FIL.OwnerDet ownerDet = (Models.FIL.OwnerDet)grdDet.CurrentRow.DataRow;
//                if (ownerDet != null)
//                {
//                    txtPrice1.Text = ownerDet.Price1.ToString("N0");
//                    txtPrice2.Text = ownerDet.Price2.ToString("N0");
//                }
//            }
//        }

//        private void FillCmb()
//        {

//            _fileType = db.FileType_List.AsNoTracking().ToList();

//            _sanadStatuses = db.SanadStatus_List.AsNoTracking().ToList();

//            _views = db.Views_List.AsNoTracking().ToList();

//            _regions = db.Region_List.AsNoTracking().ToList();

//            _fileStatuses = db.FileStatus_List.AsNoTracking().ToList();

//        }

//        private void CaptureControlHighQuality(Control ctrl)
//        {
//            // ابعاد واقعی کنترل
//            int realWidth = ctrl.DisplayRectangle.Width;
//            int realHeight = ctrl.DisplayRectangle.Height;

//            // مقیاس افزایش کیفیت (۳ برابر یعنی رزولوشن حدود ۳۰۰dpi)
//            int scale = 1;

//            memoryImage = new Bitmap(realWidth * scale, realHeight * scale);

//            using (Graphics g = Graphics.FromImage(memoryImage))
//            {
//                g.ScaleTransform(scale, scale);
//                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
//                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
//                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
//                ctrl.DrawToBitmap(memoryImage, new Rectangle(0, 0, realWidth, realHeight));
//            }
//        }

//        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
//        {
//            // تنظیم مقیاس برای چاپ کامل بدون برش
//            float scale = Math.Min(
//                (float)e.MarginBounds.Width / memoryImage.Width,
//                (float)e.MarginBounds.Height / memoryImage.Height);

//            int drawWidth = (int)(memoryImage.Width * scale);
//            int drawHeight = (int)(memoryImage.Height * scale);

//            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
//            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
//            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

//            e.Graphics.DrawImage(memoryImage, e.MarginBounds.Left, e.MarginBounds.Top, drawWidth, drawHeight);
//        }

//        private void btnPrint_Click(object sender, EventArgs e)
//        {
//            CaptureControlHighQuality(panelBody);

//            PrintDocument printDocument1 = new PrintDocument();
//            printDocument1.DefaultPageSettings.Landscape = true;
//            printDocument1.PrintPage += printDocument1_PrintPage;

//            using (PrintPreviewDialog preview = new PrintPreviewDialog())
//            {
//                preview.Document = printDocument1;
//                preview.ShowDialog();
//            }
//        }

//        private void btnN_Click(object sender, EventArgs e)
//        {
//            GetIndex(+1);
//        }

//        public void GetIndex(int typeindex)
//        {
//            if (_id != 0)
//            {


//                if (_index == 0 && typeindex == -1)
//                {
//                    FrmMsgBox.Show("ابتدای لیست میباشد");
//                    return;
//                }
//                else if (_index == _listid.Count() - 1 && typeindex == +1)
//                {
//                    FrmMsgBox.Show("انتهای لیست میباشد");
//                    return;
//                }
//                _id = _listid[_index + typeindex];
//                _index = _listid.GroupBy(x => x).ToList().FindIndex(g => g.Key == _id);
//                // هر بار context جدید ساخته شود تا از کش و tracking جدا باشد

//                _PEntity = EfFormBinder.LoadWithDetails<Owners>(db, _id, this, nameof(Owners.OwnerDetsList));

//                if (_PEntity?.OwnerDetsList != null)
//                {
//                    _lstOwnerDet = _PEntity.OwnerDetsList.ToList();
//                    grdDet.DataSource = _lstOwnerDet;
//                    if (_lstOwnerDet.Count() > 0)
//                    {
//                        grdDet.MoveFirst();
//                        txtPrice1.Text = _lstOwnerDet[0].Price1.ToString("N0");
//                        txtPrice2.Text = _lstOwnerDet[0].Price2.ToString("N0");
//                    }

//                }
//                if (_PEntity.FiletypeID == 1)
//                    this.BackColor = Color.FromArgb(255, 255, 192);

//                FillOther();
//                if (_PEntity.IsDelete)
//                {
//                    btnreturn.Enabled = true;
//                    btnDelete.Enabled = false;
//                }
//                else
//                {
//                    btnreturn.Enabled = false;
//                    btnDelete.Enabled = true;
//                }

//                if (_PEntity.IsDelete)
//                    this.BackColor = Color.FromArgb(200, 200, 200);
//                else if (_PEntity.FiletypeID == 1)
//                    this.BackColor = Color.FromArgb(255, 210, 210);
//                else if (_PEntity.FiletypeID == 2)
//                    this.BackColor = Color.FromArgb(255, 245, 200);
//                else if (_PEntity.FiletypeID == 3)
//                    this.BackColor = Color.FromArgb(180, 220, 180);
//                else if (_PEntity.FiletypeID == 4)
//                    this.BackColor = Color.FromArgb(160, 200, 255);

//            }
//        }

//        private void btnP_Click(object sender, EventArgs e)
//        {
//            GetIndex(-1);
//        }

//        private void btnreturn_Click(object sender, EventArgs e)
//        {
//            if (FrmMsgBox.Show("آیا از انجام این عملیات مطمن هستید؟", "احیای فایل", MsgBoxType.Question, MessageBoxButtons.YesNo) == DialogResult.Yes)
//            {
//                var _result = _ownersBL.IsDelete(_PEntity.ID);
//                if (_result.Status == OperationResult.OpreationResultStatus.Success)
//                {
//                    FrmMsgBox.Show("با موفقیت انجام شد", "احیای", MsgBoxType.Info);
//                    btnClose.PerformClick();
//                }
//            }
//        }
//    }
//}


using RealEstateFiling.Services;
using RealEstateFilingWin.Models.FIL;
using RealEstateFilingWin.Models.GNR;
using RealEstateFilingWin.Services.FIL;
using RealEstateFilingWin.Utility;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.Data.Entity;
using Common; // EF6 Include & AsNoTracking

namespace RealEstateFilingWin.Forms
{
    public partial class FrmFileView : FrmBase
    {
        public event Action DataSaved;

        private Owners _PEntity = new Owners();
        private List<OwnerDet> _lstOwnerDet = new List<OwnerDet>();
        private long _id = 0;
        private Guid _guid = Guid.Empty;
        private int _index = 0;
        private List<long> _listid = new List<long>();
        private OwnersBL _ownersBL = new OwnersBL();

        // Lookup dictionaries
        private Dictionary<long, string> _dictFileTypes = new Dictionary<long, string>();
        private Dictionary<long, string> _dictRegions = new Dictionary<long, string>();
        private Dictionary<long, string> _dictFileStatuses = new Dictionary<long, string>();
        private Dictionary<long, string> _dictViews = new Dictionary<long, string>();

        private Bitmap memoryImage;
        public bool _SearchView = false;
        public long _customerid;

        public FrmFileView(long ID, List<long> arraysid)
        {
            InitializeComponent();
            _id = ID;
            _listid = arraysid?.Distinct().ToList() ?? new List<long>();
            FillCmb();
        }

        #region Fill Lookup Dictionaries
        private void FillCmb()
        {
            using (var db = new RealEstateContext())
            {
                _dictFileTypes = db.FileType_List.AsNoTracking().ToDictionary(x => x.ID, x => x.Name);
                _dictRegions = db.Region_List.AsNoTracking().ToDictionary(x => x.ID, x => x.Name);
                _dictFileStatuses = db.FileStatus_List.AsNoTracking().ToDictionary(x => x.ID, x => x.Name);
                _dictViews = db.Views_List.AsNoTracking().ToDictionary(x => x.ID, x => x.Name);
            }
        }
        #endregion

        #region Load Owner with Eager Loading
        private Owners LoadOwner(long id)
        {
            Owners owner;

            using (var db = new RealEstateContext())
            {
                db.Configuration.LazyLoadingEnabled = false;

                owner = db.Owners_List
                          .AsNoTracking()
                          .Include(o => o.OwnerDetsList.Select(d => d.OwnerDet_Kabinet)) // Include navigation
                          .Include(o => o.OwnerDetsList.Select(d => d.OwnerDet_Kafposh)) // Include navigation
                          .Include(o => o.OwnerDetsList.Select(d => d.OwnerDet_ServiceWc)) // Include navigation

                          .FirstOrDefault(o => o.ID == id);
            }
            _guid = owner.MasterID;
            return owner;
        }
        #endregion

        #region Bind Data to Form
        private void BindOwnerData()
        {
            //EfFormBinder.LoadWithDetails<Owners>(db, _id, this, nameof(Owners.OwnerDetsList));
            EfFormBinder.ModelToControls<Owners>(_PEntity, this);
            if (_PEntity == null)
            {
                grdDet.DataSource = null;
                return;
            }

            _lstOwnerDet = _PEntity.OwnerDetsList != null
                ? _PEntity.OwnerDetsList.ToList()
                : new List<OwnerDet>();

            grdDet.DataSource = _lstOwnerDet;

            if (_lstOwnerDet.Count > 0)
            {
                grdDet.MoveFirst();
                txtPrice1.Text = _lstOwnerDet[0].Price1.ToString("N0");
                txtPrice2.Text = _lstOwnerDet[0].Price2.ToString("N0");
            }
            else
            {
                //txtPrice1.Clear();
                //txtPrice2.Clear();
            }

            txtAddress.Text = string.Format("{0} _ پلاک {1} _ واحد {2}", _PEntity.Address, _PEntity.Plak, _PEntity.Vahed);
            txtSenbana.Text = _PEntity.Senbana == 0 ? "نوساز" : _PEntity.Senbana.ToString();

            SetFormColor();
            FillOther();

            btnreturn.Enabled = _PEntity.IsDelete;
            btnDelete.Enabled = !_PEntity.IsDelete;
            label1.Text = txtMName.Text;
            txtMasahat.Text = _PEntity.Masahat == null ? "0" : Convert.ToInt32(_PEntity.Masahat).ToString();
            txtTol.Text = _PEntity.Tol == null ? "0" : Convert.ToInt32(_PEntity.Tol).ToString();
            if (_PEntity.PishForosh)
            {
                lblDatePishForoshTahvil.Visible = true;
                txtDatePishForoshTahvil.Visible = true;
                txtDatePishForoshTahvil.Text = _PEntity.DatePishForoshTahvil.ToShamsiDate();
            }
            else
            {
                lblDatePishForoshTahvil.Visible = false;
                txtDatePishForoshTahvil.Visible = false;
            }
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
            using (var db=new RealEstateContext())
            {
                var query = db.Branches.Where(x => x.ID == _PEntity.BranchID).FirstOrDefault();
                if(query!=null)
                txtCode.Text = _PEntity.Code + "_" + query.Code  ;
            }
            
        }

        private void SetFormColor()
        {
            if (_PEntity.IsDelete)
            {
                this.BackColor = Color.FromArgb(200, 200, 200);
                return;
            }

            if (_PEntity.FiletypeID == 1)
                this.BackColor = Color.FromArgb(255, 230, 230); // صورتی روشن‌تر
            else if (_PEntity.FiletypeID == 2)
                this.BackColor = Color.FromArgb(255, 250, 220); // زرد روشن‌تر
            else if (_PEntity.FiletypeID == 3)
                this.BackColor = Color.FromArgb(200, 240, 200); // سبز روشن‌تر
            else if (_PEntity.FiletypeID == 4)
                this.BackColor = Color.FromArgb(200, 220, 255); // آبی روشن‌تر
            else
                this.BackColor = SystemColors.Control;
        }

        private void FillOther()
        {
            var op = _ownersBL.GetOperatorName(_PEntity.UserIDCreator, _PEntity.UserIDModifier);
            txtCreatorName.Text = op.CreatorName;
            txtModifierName.Text = op.ModifierName;
            txtCreateDate.Text = _PEntity.CreationDate.ToShamsiDate();
            txtModifierDate.Text = _PEntity.ModificationDate.ToShamsiDate();

            txtFiletypeID.Text = _dictFileTypes.ContainsKey(_PEntity.FiletypeID) ? _dictFileTypes[_PEntity.FiletypeID] : "-";
            txtFileStatusID.Text = _dictFileStatuses.ContainsKey(_PEntity.FileStatusID) ? _dictFileStatuses[_PEntity.FileStatusID] : "-";
            txtRegionID.Text = _dictRegions.ContainsKey(_PEntity.RegionID) ? _dictRegions[_PEntity.RegionID] : "-";
            txtViewID.Text = _dictViews.ContainsKey(_PEntity.ViewID) ? _dictViews[_PEntity.ViewID] : "-";
        }
        #endregion

        #region Form Load
        private void FrmFileView_Load(object sender, EventArgs e)
        {
            if (_id == 0) return;

            _index = _listid.FindIndex(x => x == _id);
            _PEntity = LoadOwner(_id);  // Load کامل entity با navigation
            BindOwnerData();
            CommonExtension.ApplyToAllControls(this);
        }
        #endregion

        #region Grid Events
        private void grdDet_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left || grdDet.CurrentRow == null) return;

            OwnerDet det = grdDet.CurrentRow.DataRow as OwnerDet;
            if (det != null)
            {
                txtPrice1.Text = det.Price1.ToString("N0");
                txtPrice2.Text = det.Price2.ToString("N0");
            }
        }
        #endregion

        #region Navigation Buttons
        private void btnN_Click(object sender, EventArgs e) => GetIndex(+1);
        private void btnP_Click(object sender, EventArgs e) => GetIndex(-1);
        private void btnViewtel1_Click(object sender, EventArgs e)
        {
            btnViewtel1.Visible = false;
            btnViewtel2.Visible = false;
            using (var db = new RealEstateContext())
            {
                var _model = db.Owners_List.Where(x => x.ID == _PEntity.ID).FirstOrDefault();
                _model.ViewPhoneDate = DateTime.Now;
                _model.ViewPhoneUserID = ClsBaseSetting.UserID;
               
                db.SaveChanges();
            }
        }
        public void GetIndex(int step)
        {
            int newIndex = _index + step;
            if (newIndex < 0)
            {
                FrmMsgBox.Show("ابتدای لیست میباشد");
                return;
            }
            if (newIndex >= _listid.Count)
            {
                FrmMsgBox.Show("انتهای لیست میباشد");
                return;
            }

            _index = newIndex;
            _id = _listid[_index];
            _PEntity = LoadOwner(_id);
            BindOwnerData();
        }
        #endregion

        #region CRUD Buttons
        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;

            this.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (ClsBaseSetting.UserID != 1 && ClsBaseSetting.UserIn.UserAccsessList.Where(x => x.AccessID == 9).Any() == false)
            {
                FrmMsgBox.Show("مجوز ندارید", "دسترسی", MsgBoxType.Error);
                return;
            }
            if (FrmMsgBox.Show("آیا از انجام این عملیات مطمن هستید؟", "حذف", MsgBoxType.Question, MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;

            var result = _ownersBL.IsDelete(_PEntity.ID);
            if (result.Status == OperationResult.OpreationResultStatus.Success)
            {
                FrmMsgBox.Show("با موفقیت انجام شد", "حذف", MsgBoxType.Info);
                btnClose.PerformClick();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (ClsBaseSetting.UserID != 1 && !ClsBaseSetting.UserIn.UserAccsessList.Where(x => x.AccessID == 8).Any())
            {
                FrmMsgBox.Show("مجوز ندارید", "دسترسی", MsgBoxType.Error);
                return;
            }
            FrmFile frmFile = new FrmFile(_PEntity.ID.ToInt64OrDefault());
            if (frmFile.ShowDialog() == DialogResult.OK)
            {
                _PEntity = LoadOwner(_id);
                BindOwnerData();
                DataSaved?.Invoke();
                this.Close();
            }
        }

        private void btnreturn_Click(object sender, EventArgs e)
        {
            if (ClsBaseSetting.UserID != 1 && ClsBaseSetting.UserIn.UserAccsessList.Where(x => x.AccessID == 9).Any() == false)
            {
                FrmMsgBox.Show("مجوز ندارید", "دسترسی", MsgBoxType.Error);
                return;
            }
            if (FrmMsgBox.Show("آیا از انجام این عملیات مطمن هستید؟", "احیای فایل", MsgBoxType.Question, MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;

            var result = _ownersBL.IsDelete(_PEntity.ID);
            if (result.Status == OperationResult.OpreationResultStatus.Success)
            {
                FrmMsgBox.Show("با موفقیت انجام شد", "احیای", MsgBoxType.Info);
                btnClose.PerformClick();
            }
        }
        #endregion

        #region Printing
        private void CaptureControlHighQuality(Control ctrl)
        {
            int w = ctrl.DisplayRectangle.Width;
            int h = ctrl.DisplayRectangle.Height;

            memoryImage?.Dispose();
            memoryImage = new Bitmap(w, h);

            using (Graphics g = Graphics.FromImage(memoryImage))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                ctrl.DrawToBitmap(memoryImage, new Rectangle(0, 0, w, h));
            }
        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            if (memoryImage == null) return;

            float scale = Math.Min(
                (float)e.MarginBounds.Width / memoryImage.Width,
                (float)e.MarginBounds.Height / memoryImage.Height);

            int drawWidth = (int)(memoryImage.Width * scale);
            int drawHeight = (int)(memoryImage.Height * scale);

            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            e.Graphics.DrawImage(memoryImage, e.MarginBounds.Left, e.MarginBounds.Top, drawWidth, drawHeight);
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            CaptureControlHighQuality(panelBody);

            PrintDocument pd = new PrintDocument();
            pd.DefaultPageSettings.Landscape = true;
            pd.PrintPage += printDocument1_PrintPage;

            using (PrintPreviewDialog preview = new PrintPreviewDialog())
            {
                preview.Document = pd;
                preview.ShowDialog();
            }
        }

        #endregion

        private void btnMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;

        }

        private void FrmFileView_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_SearchView)
            {
                if (FrmMsgBox.Show("آیا هنوز به عنوان فایل جدید برای این مشتری نمایش داده شود ؟", "", MsgBoxType.Question, MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    using (var db = new RealEstateContext())
                    {
                        var _customer = db.Customer_List.Where(x => x.ID == _customerid).FirstOrDefault();
                        if (_customer != null)
                        {
                            db.CustomerReadOwner_List.Add(new Models.CUS.CustomerReadOwner()
                            {
                                CustomerID = _customer.ID,
                                OwnerID = _id,
                            });
                            //if (_customer.Description_Latin?.Length > 0)
                            //{
                            //    _customer.Description_Latin += "," + _id.ToString();
                            //}
                            //else _customer.Description_Latin = _id.ToString();
                            db.SaveChanges();
                            DataSaved?.Invoke();
                        }

                    }
                }
            }
        }

        private void btnPic_Click(object sender, EventArgs e)
        {
            FrmPic frmPic = new FrmPic(_guid, txtMName.Text);
            frmPic.ShowDialog();
        }

        private void btnfolert_Click(object sender, EventArgs e)
        {
            if (ClsBaseSetting.UserID != 1 && ClsBaseSetting.UserIn.UserAccsessList.Where(x => x.AccessID == 10).Any() == false)
            {
                FrmMsgBox.Show("مجوز ندارید", "دسترسی", MsgBoxType.Error);
                return;
            }
            using (var db = new RealEstateContext())
            {
                var model = db.UserOwner_List.Where(x => x.OwnerID == _PEntity.ID && x.UserID == ClsBaseSetting.UserID).FirstOrDefault();
                if (model != null)
                {
                    if (FrmMsgBox.Show("آیا این فایل از پوشه شخصی شما حذف شود؟", "سوال", MsgBoxType.Question, MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        db.UserOwner_List.Remove(model);
                        
                        db.SaveChanges();
                        FrmMsgBox.Show("این فایل از پوشه شخصی شما حذف شد");
                    }
                    return;
                }
                if (FrmMsgBox.Show("آیا این فایل به پوشه شخصی شما اضافه شود؟", "سوال", MsgBoxType.Question, MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    db.UserOwner_List.Add(new Models.MNG.UserOwner()
                    {
                        OwnerID = _PEntity.ID,
                        UserID = ClsBaseSetting.UserID,

                    });
                    db.SaveChanges();
                    FrmMsgBox.Show("این فایل به پوشه شخصی شما اضافه شد");
                }

            }
        }

        private void FrmFileView_KeyDown(object sender, KeyEventArgs e)
        {

        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Right)
            {
                btnP.PerformClick();
                return true;
            }
            else if (keyData == Keys.Left)
            {
                btnN.PerformClick();
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

    }
}
