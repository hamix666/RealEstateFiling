using Common;
using Newtonsoft.Json;
using RealEstateFiling.Services;
using RealEstateFilingWin.DTO;
using RealEstateFilingWin.Services.FIL;
using RealEstateFilingWin.Utility;
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
    public partial class FrmSearchNews : RealEstateFilingWin.Forms.FrmBase
    {
        public FrmSearchNews()
        {
            InitializeComponent();
        }

        private void FrmSearchNews_Load(object sender, EventArgs e)
        {
            var _lstcustomerDtos = new List<CustomerDto>();
            using (var db = new RealEstateContext())
            {
                var _lst = db.Customer_List.Where(x => x.UserIDCreator == ClsBaseSetting.UserID && x.IsDeleted==false).OrderByDescending(w => w.CreationDate).ToList();


                var _ownersBL = new OwnersBL();
                if (_lst.Count > 0)
                {
                    foreach (var item in _lst)
                    {

                        var filters = JsonConvert.DeserializeObject<FileSearchFilterDto>(item.FilterJson);

                        DateTime? _startdate = null;
                        DateTime? _enddate = null;

                        _startdate = item.ModificationDate ?? item.CreationDate;


                        var query = _ownersBL.GetListFileSearch(0, 0, 1000, filters.Code, filters.MName, filters.MTel,
                   filters.Forosh, filters.PishForosh, filters.Ejare, filters.FileType, filters.Address, filters.Region, filters.Price1_Start.Replace(",", ""),
                   filters.Price1_End.Replace(",", ""), filters.Price2_Start.Replace(",", ""), filters.Price2_End.Replace(",", ""), filters.Tozihat, filters.IsDelete,
                   filters.Khab_az.ToInt32OrDefault(), filters.Khab_ta.ToInt32OrDefault(), filters.Senbana_az.ToInt32OrDefault(), filters.Senbana_ta.ToInt32OrDefault(),
                   filters.Tabaghe_az, filters.Tabaghe_ta, filters.Vahed_az.ToInt32OrDefault(), filters.Vahed_ta.ToInt32OrDefault(),
                   filters.Asansor, filters.Parking, filters.Anbari,
                   filters.VahedinTbaghe_az.ToInt32OrDefault(), filters.VahedinTbaghe_ta.ToInt32OrDefault(), filters.Tabaghat_az.ToInt32OrDefault(), filters.Tabaghat_ta.ToInt32OrDefault(),
                   filters.Parking_az, filters.Parking_ta, filters.AsansorTabaghe.ToInt32OrDefault(),filters.Metrazh_az.ToInt32OrDefault(),filters.Metrazh_ta.ToInt32OrDefault(), filters.Masahat_az.ToDecimalOrDefault(), filters.Masahat_ta.ToDecimalOrDefault()
                   , _startdate, _enddate);
                        var _readidstr = db.CustomerReadOwner_List.Where(x => x.CustomerID == item.ID).Select(w => w.OwnerID).ToList();
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
                            CreateDate = item.CreationDate.ToShamsiDate(),
                            FileType = _filetype,
                            Phone = item.Tel1 + " - " + item.Tel2 + " - " + item.Tel3,
                            IsDeleted = item.IsDeleted,
                            CountMember = item.CountMember,


                        };

                        if (_customer.CountFind > 0)
                        {
                            StringBuilder st = new StringBuilder();
                            st.Append("برای مشتری : " + item.Name + " تعداد " + _customer.CountFind + " مورد یافت شد ");

                            Label lbl = new Label()
                            {
                                Margin=new Padding(10),
                                Text = st.ToString(),
                                AutoSize = false,
                                Dock = DockStyle.Top,
                                Font = new System.Drawing.Font("Vazirmatn", 12F, System.Drawing.FontStyle.Bold),
                                BackColor = Color.Transparent,
                                RightToLeft = RightToLeft.Yes

                            };
                            panelMain.Controls.Add(lbl);
                        }
                        
                    }
                }
                



            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
