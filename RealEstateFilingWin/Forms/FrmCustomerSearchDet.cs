
using Common;
using Janus.Windows.GridEX;
using Newtonsoft.Json;
using RealEstateFiling.Services;
using RealEstateFilingWin.DTO;
using RealEstateFilingWin.Models.CUS;
using RealEstateFilingWin.Services.FIL;
using RealEstateFilingWin.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static RealEstateFilingWin.Forms.FrmMain;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace RealEstateFilingWin.Forms
{
    public partial class FrmCustomerSearchDet : FrmBase
    {
        public event Action DataSaved;
        public FileSearchFilterDto fileSearchFilterDto;
        List<CustomerDto> _customerList = new List<CustomerDto>();
        OwnersBL _ownersBL;
        List<CustomerDto> _lstcustomerDtos = new List<CustomerDto>();
        List<FileListDto> _lstowner = new List<FileListDto>();
        public long _idCustomer = 0;
        public Customer _Customer;
        public FrmCustomerSearchDet()
        {
            InitializeComponent();
        }

        private void FrmCustomerSearchDet_Load(object sender, EventArgs e)
        {
            _lstcustomerDtos = new List<CustomerDto>();
            List<Customer> _lst = new List<Customer>();
            using (var db = new RealEstateContext())
            {
                 _lst = db.Customer_List
    .Where(x => x.UserIDCreator == ClsBaseSetting.UserID )
    .OrderByDescending(x => x.ModificationDate)
    .ThenByDescending(x => x.CreationDate)
    .ThenBy(x => x.IsDeleted)
    .ToList();



                _ownersBL = new OwnersBL();
                if (_lst.Count > 0)
                {
                    foreach (var item in _lst.Where(x=>x.IsDeleted==false).ToList())
                    {

                        var filters = JsonConvert.DeserializeObject<FileSearchFilterDto>(item.FilterJson);

                        DateTime? _startdate = null;
                        DateTime? _enddate = null;
                        
                            _startdate = item.ModificationDate ?? item.CreationDate;
                        

                        var query = _ownersBL.GetListFileSearch(0,0, 1000, filters.Code, filters.MName, filters.MTel,
                   filters.Forosh, filters.PishForosh, filters.Ejare, filters.FileType, filters.Address, filters.Region, filters.Price1_Start.Replace(",", ""),
                   filters.Price1_End.Replace(",", ""), filters.Price2_Start.Replace(",", ""), filters.Price2_End.Replace(",", ""), filters.Tozihat, filters.IsDelete,
                   filters.Khab_az.ToInt32OrDefault(),filters.Khab_ta.ToInt32OrDefault(),filters.Senbana_az.ToInt32OrDefault(),filters.Senbana_ta.ToInt32OrDefault(),
                   filters.Tabaghe_az,filters.Tabaghe_ta,filters.Vahed_az.ToInt32OrDefault(),filters.Vahed_ta.ToInt32OrDefault(),
                   filters.Asansor,filters.Parking,filters.Anbari,
                   filters.VahedinTbaghe_az.ToInt32OrDefault(),filters.VahedinTbaghe_ta.ToInt32OrDefault(),filters.Tabaghat_az.ToInt32OrDefault(),filters.Tabaghat_ta.ToInt32OrDefault(),
                   filters.Parking_az,filters.Parking_ta,filters.AsansorTabaghe.ToInt32OrDefault(),filters.Metrazh_az,filters.Metrazh_ta, filters.Masahat_az.ToDecimalOrDefault(), filters.Masahat_ta.ToDecimalOrDefault()
                   , _startdate, _enddate,_type:false);
                        var _readidstr = db.CustomerReadOwner_List.Where(x => x.CustomerID == item.ID).Select(w=>w.OwnerID).ToList();
                        List<long> _readid = new List<long>();
                        
                        
                            foreach (var itemstr in _readidstr)
                            {
                                _readid.Add(itemstr.ToInt64OrDefault());
                            }
                        var _date = item.ModificationDate ?? item.CreationDate;
                        var _queryfinal = query.Items.Where(x => !_readid.Contains(x.ID) && (x.CreationDate.ToMiladiDate() > _date || x.ModificationDate.ToMiladiDate() > _date)).ToList();
                        var _lstid = _queryfinal.Count() == 0 ? "" : string.Join(",", _queryfinal.Select(x => x.ID.ToString()).Distinct());
                        var _filetype = filters.Forosh ? "فروش" : filters.PishForosh ? "پیش فروش" : "رهن و اجاره";
                        CustomerDto _customer = new CustomerDto()
                        {
                            Address = item.Addres,
                            ID = item.ID,
                            CountFind = _queryfinal.Count(),
                            CreationDate = item.CreationDate,
                            FilterJson = item.FilterJson,
                            Name = item.Name,
                            Tel1 = item.Tel1,
                            Tel2 = item.Tel2,
                            Tel3 = item.Tel3,
                            Title = item.Title,
                            UserIDCreator = item.UserIDCreator,
                            ListOwnerID = _lstid,
                            CreateDate= _date.ToShamsiDate(),
                            FileType=_filetype,
                            Phone=item.Tel1 +" - "+item.Tel2 +" - "+item.Tel3,
                            IsDeleted=item.IsDeleted,
                            CountMember=item.CountMember,
                            

                        };

                        _lstcustomerDtos.Add(_customer);
                    }
                }



            }
            grdCustomerSearch.DataSource = null;
            grdCustomerSearch.DataSource = _lstcustomerDtos.Where(x=>x.IsDeleted==false).ToList();
            var _customerdelete = _lst.Where(x=>x.IsDeleted==true).Select(item => new CustomerDto()
            {
                Address = item.Addres,
                ID = item.ID,
                //CountFind = _queryfinal.Count(),
                CreationDate = item.CreationDate,
                FilterJson = item.FilterJson,
                Name = item.Name,
                Tel1 = item.Tel1,
                Tel2 = item.Tel2,
                Tel3 = item.Tel3,
                Title = item.Title,
                UserIDCreator = item.UserIDCreator,
                ListOwnerID = "" ,
                CreateDate = item.CreationDate.ToShamsiDate(),
                //FileType = _filetype,
                Phone = item.Tel1 + " - " + item.Tel2 + " - " + item.Tel3,
                IsDeleted = item.IsDeleted,
                //CountMember = item.CountMember,
            });
            grdCustomerDelete.DataSource = null;
            grdCustomerDelete.DataSource = _customerdelete.ToList();
        }

        private void grdCustomerSearch_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnEdit.PerformClick();



            



        }

        public void FillData(long ID,bool type)
        {
            using (var db = new RealEstateContext())
            {
                 _Customer = db.Customer_List.Where(x => x.ID==ID).FirstOrDefault();


                _ownersBL = new OwnersBL();
               
                if (_Customer!=null)
                {


                    var filters = JsonConvert.DeserializeObject<FileSearchFilterDto>(_Customer.FilterJson);

                    fileSearchFilterDto = filters;
                    
                    
               //     var query = _ownersBL.GetListFileSearch(0, 1000, filters.Code, filters.MName, filters.MTel,
               //filters.Forosh, filters.PishForosh, filters.Ejare, filters.FileType, filters.Address, filters.Region, filters.Price1_Start.Replace(",", ""),
               //filters.Price1_End.Replace(",", ""), filters.Price2_Start.Replace(",", ""), filters.Price2_End.Replace(",", ""), filters.Tozihat, filters.IsDelete);

               //     _lstowner = query.Items.Where(x => x.CreationDate.ToMiladiDate() > _Customer.CreationDate || x.ModificationDate.ToMiladiDate() > _Customer.CreationDate).ToList();
               //     var _lstid = _lstowner.Count() == 0 ? "" : string.Join(",", _lstowner.Select(x => x.ID.ToString()).Distinct());


               //     grdSearcView.DataSource = _lstowner;
               //     if (type == false)
               //         grdSearcView.RootTable.RowFormatStyle = new GridEXFormatStyle()
               //         {
               //             BackColor = Color.LightGray
               //         };
               //     else
               //     {
               //         grdSearcView.RootTable.RowFormatStyle = new GridEXFormatStyle()
               //         {
               //             BackColor = Color.LightGreen
               //         };
               //     }   
               //     tabControl1.SelectedTabIndex = 1;
                }
                

            }






        }

        private void grdSearcView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //if (grdSearcView.CurrentRow.RowType == RowType.Record)
            //    ViewFile();
        }

        //private void ViewFile()
        //{
        //    if (_lstowner.Count() == 0)
        //        return;
        //    List<long> _lstid = _lstowner.Select(w => w.ID).ToList();
        //    var _selectrow = grdSearcView.CurrentRow.Position;
        //    var ID = grdSearcView.CurrentRow.Cells["ID"].Value;
        //    FrmFileView frmFile = new FrmFileView(ID.ToInt64OrDefault(), _lstid);
        //    frmFile._customerid = _idCustomer;
        //    if(grdSearcView.RootTable.RowFormatStyle.BackColor!=Color.LightGray)
        //    frmFile._SearchView = true;
            

        //    frmFile.Show();
        //}

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (grdCustomerSearch.CurrentRow != null && grdCustomerSearch.CurrentRow.Cells["Count"].Value.ToInt64OrDefault() > 0)
            {
                FillData(grdCustomerSearch.CurrentRow.Cells["ID"].Value.ToInt64OrDefault(), true);
                _idCustomer = grdCustomerSearch.CurrentRow.Cells["ID"].Value.ToInt64OrDefault();

            }
            else if (FrmMsgBox.Show("اطلاعات جدید یافت نشد میخواهید اطلاعات قدیمی از تاریخ ایجاد تا به امروز را مشاهده کنید؟", "", MsgBoxType.Question, MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                FillData(grdCustomerSearch.CurrentRow.Cells["ID"].Value.ToInt64OrDefault(), false);
                _idCustomer = 0;
            }
            DataSaved?.Invoke();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (grdCustomerSearch.CurrentRow != null)
            {
                if (FrmMsgBox.Show("آیا از حذف مشتری (" + grdCustomerSearch.CurrentRow.Cells["Name"].Value +")اطمینان دارید؟", "", MsgBoxType.Question, MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    using (var db = new RealEstateContext())
                    {
                        long id = grdCustomerSearch.CurrentRow.Cells["ID"].Value.ToInt64OrDefault();
                        var _model = db.Customer_List.Where(x => x.ID == id).FirstOrDefault();
                        if (_model != null)
                        {
                            _model.IsDeleted = true;
                            _model.ModificationDate = DateTime.Now;
                            db.SaveChanges();
                            this.FrmCustomerSearchDet_Load(sender, e);
                        }
                    }
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if(grdCustomerSearch.CurrentRow == null)
            {
                FrmMsgBox.Show("یک رکورد را انتخاب کنید");
                return;
            }
            if (grdCustomerSearch.CurrentRow != null && grdCustomerSearch.CurrentRow.Cells["Count"].Value.ToInt64OrDefault() > 0)
            {
                FillData(grdCustomerSearch.CurrentRow.Cells["ID"].Value.ToInt64OrDefault(), true);
                _idCustomer = grdCustomerSearch.CurrentRow.Cells["ID"].Value.ToInt64OrDefault();
                DataSaved?.Invoke();

            }
            else /*if (FrmMsgBox.Show("اطلاعات جدید یافت نشد میخواهید اطلاعات قدیمی از تاریخ ایجاد تا به امروز را مشاهده کنید؟", "", MsgBoxType.Question, MessageBoxButtons.YesNo) == DialogResult.Yes)*/
            {
                FillData(grdCustomerSearch.CurrentRow.Cells["ID"].Value.ToInt64OrDefault(), false);
                _idCustomer = grdCustomerSearch.CurrentRow.Cells["ID"].Value.ToInt64OrDefault();
                DataSaved?.Invoke();
            }
            //var cuid = grdCustomerSearch.CurrentRow.Cells["ID"].Value.ToInt64OrDefault();
            //Customer cudata;
            //using (var db = new RealEstateContext())
            //{
            //    cudata=db.Customer_List.Where(x=>x.ID==cuid).FirstOrDefault();
            //}
            //var filters = JsonConvert.DeserializeObject<FileSearchFilterDto>(cudata.FilterJson);

            //FrmCustomerSearch frmCustomerSearch =new FrmCustomerSearch(cuid,filters,true);
            //if (frmCustomerSearch.ShowDialog() == DialogResult.OK)
            //{
            //    FrmCustomerSearchDet_Load(sender,e);
            //}
        }

        private void btnreturn_Click(object sender, EventArgs e)
        {
            if (grdCustomerDelete.CurrentRow != null)
            {
                if (FrmMsgBox.Show("آیا از احیای مشتری (" + grdCustomerDelete.CurrentRow.Cells["Name"].Value + ")اطمینان دارید؟", "", MsgBoxType.Question, MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    using (var db = new RealEstateContext())
                    {
                        long id = grdCustomerDelete.CurrentRow.Cells["ID"].Value.ToInt64OrDefault();
                        var _model = db.Customer_List.Where(x => x.ID == id).FirstOrDefault();
                        if (_model != null)
                        {
                            _model.IsDeleted = false;
                            _model.ModificationDate = DateTime.Now;
                            db.SaveChanges();
                            this.FrmCustomerSearchDet_Load(sender, e);
                        }
                    }
                }
            }
        }
    }
}
