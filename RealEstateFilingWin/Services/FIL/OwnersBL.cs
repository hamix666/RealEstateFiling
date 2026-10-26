using Common;
using RealEstateFiling.Services;
using RealEstateFilingWin.DTO;
using RealEstateFilingWin.Models.FIL;
using RealEstateFilingWin.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Windows.Forms;

namespace RealEstateFilingWin.Services.FIL
{
    public class OwnersBL : RepoFullBase<Owners>
    {
        OperationResult _operationResult = new OperationResult();

        public OwnersBL(RealEstateContext repositoryContext) : base(repositoryContext)
        {

        }
        public OwnersBL() : base()
        {
            _Contexct = new RealEstateContext();
        }

        public PagedResult<FileListDto> GetListFile(int pageNumber, int pageSize)
        {
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize <= 0) pageSize = 10;

            int offset = (pageNumber - 1) * pageSize;

            // کوئری داده‌ها
            var query = @"
        SELECT 
    o.ID,
    CASE 
        WHEN o.Forosh = 1 THEN N'فروش'
        WHEN o.PishForosh = 1 THEN N'پیش‌فروش'
        WHEN o.Ejare = 1 THEN N'اجاره'
        ELSE N'نامشخص'
    END AS ContractType,
    ft.Name AS FileType,
    o.CreationDate,
    rg.Name AS Region,
	o.MName,ot.Tabaghe,ot.Zirbana,
	(case when ot.Asansor=1 then N'دارد' else N'ندارد' end)Asansor,
	ot.Parking,(case when ot.Anbari=1 then N'دارد' else N'ندارد' end)Anbari,
	o.Senbana,
    ot.Price1,
    ot.Price2,
    o.Address
FROM FIL.Owners o
JOIN GNR.FileType ft ON ft.ID = o.FiletypeID
JOIN GNR.Region rg ON rg.ID = o.RegionID
LEFT JOIN FIL.OwnerDet ot ON ot.OwnerID = o.ID
ORDER BY o.ID DESC
OFFSET @Offset ROWS
FETCH NEXT @PageSize ROWS ONLY;
    ";

            // کوئری تعداد کل
            var countQuery = "SELECT COUNT(*) FROM FIL.Owners";

            int totalCount = _Contexct.Database.SqlQuery<int>(countQuery).FirstOrDefault();

            var items = _Contexct.Database.SqlQuery<RawFileListDto>(
                query,
                new SqlParameter("@Offset", offset),
                new SqlParameter("@PageSize", pageSize)
            ).ToList();
            List<FileListDto> lst = new List<FileListDto>();
            foreach (var item in items)
            {
                lst.Add(new FileListDto()
                {
                    Address = item.Address,
                    ContractType = item.ContractType,
                    CreationDate = item.CreationDate.ToShamsiDate(),
                    FileType = item.FileType,
                    ID = item.ID,
                    Price1 = item.Price1,
                    Price2 = item.Price2,
                    Region = item.Region,
                    Anbari = item.Anbari,
                    Asansor = item.Asansor,
                    MName = item.MName,
                    Parking = item.Parking,
                    Senbana = item.Senbana,
                    Tabaghe = item.Tabaghe,
                    Zirbana = item.Zirbana,

                });
            }
            return new PagedResult<FileListDto>
            {
                Items = lst,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }
        public PagedResult<FileListDto> GetListFileSearch(long idcustomer,
    int pageNumber,
    int pageSize,
    string code,
    string mname,
    string mtel,
    bool forosh,
    bool pishforosh,
    bool ejare,
    long filetype,
    string address,
    List<long> Region,
    string price1_s,
    string price1_e,
    string price2_s,
    string price2_e,
    string tozihat, int isdelete, int? khab_az, int? khab_ta, int? senbana_az, int? senbana_ta, int? tabaghe_az, int? tabaghe_ta, int? vahed_az, int? vahed_ta,
    bool asansor, bool parking, bool anbari, int? VahedinTbaghe_az, int? VahedinTbaghe_ta, int? Tabaghat_az, int? Tabaghat_ta,
    string Parking_az, string Parking_ta, int? AsansorTabaghe, int? Metrazh_az, int? Metrazh_ta,decimal? Masahat_az,decimal? Masahat_ta,
    DateTime? startdate, DateTime? endDate, bool _type = true, bool _isPoshe = false)
        {
            try
            {
                var useraccsess = ClsBaseSetting.UserIn.UserAccsessList.ToList();
                if (pageNumber < 1) pageNumber = 1;
                if (pageSize <= 0) pageSize = 10;

                int offset = (pageNumber - 1) * pageSize;

                var query = new StringBuilder(@"
        SELECT 
            o.ID,
            CASE 
                WHEN o.Forosh = 1 THEN N'فروش'
                WHEN o.PishForosh = 1 THEN N'پیش‌فروش'
                WHEN o.Ejare = 1 THEN N'اجاره'
                ELSE N'نامشخص'
            END AS ContractType,
            ft.Name AS FileType,
            o.CreationDate,
            rg.Name AS Region,
            o.MName,
            ot.Tabaghe,
            ot.Zirbana,
            (CASE WHEN ot.Asansor = 1 THEN N'دارد' ELSE N'ندارد' END) AS Asansor,
            ot.Parking,
            (CASE WHEN ot.Anbari = 1 THEN N'دارد' ELSE N'ندارد' END) AS Anbari,
            o.Senbana,
            ot.Price1,
            ot.Price2,o.IsDelete isDeleted,isNULL(o.Masahat,0)Masahat,isNULL(o.Tol,0)Tol,
            (o.Address +N' _ پلاک '+ ISNULL(o.Plak,'') +N' _ واحد '+ ISNULL(o.Vahed,'')) Address,o.ModificationDate,

             -- این ستون تاریخ بیشترین مقدار را برمی‌گرداند
            (SELECT MAX(v) 
             FROM (VALUES (o.ViewPhoneDate), (o.ModificationDate), (o.CreationDate)) AS value(v)
            ) AS LastActivityDate,   
            (SELECT MAX(v) 
             FROM (VALUES  (o.ModificationDate), (o.CreationDate)) AS value(v)
            ) AS LastDate,o.ReturnTime

        FROM FIL.Owners o
        JOIN GNR.FileType ft ON ft.ID = o.FiletypeID
        JOIN GNR.Region rg ON rg.ID = o.RegionID
        LEFT JOIN FIL.OwnerDet ot ON ot.OwnerID = o.ID
        left join MNG.UserOwner uo on o.ID=uo.OwnerID
        WHERE 1=1
    ");

                var parameters = new List<SqlParameter>();

                // --- فیلترهای داینامیک ---
                if (_isPoshe)
                {
                    query.Append(" AND uo.UserID= @UserID");
                    parameters.Add(new SqlParameter("@UserID", ClsBaseSetting.UserID));
                }
                if (!string.IsNullOrWhiteSpace(code))
                {
                    query.Append(" AND o.Code LIKE @Code");
                    parameters.Add(new SqlParameter("@Code", "%" + code + "%"));
                }

                if (!string.IsNullOrWhiteSpace(mname))
                {
                    query.Append(" AND o.MName LIKE @MName");
                    parameters.Add(new SqlParameter("@MName", "%" + mname + "%"));
                }

                if (!string.IsNullOrWhiteSpace(mtel))
                {
                    query.Append(" AND (o.MTel1 LIKE @MTel or o.MTel2 LIKE @MTel or o.STel1 LIKE @MTel or o.STel2 LIKE @MTel)");
                    parameters.Add(new SqlParameter("@MTel", "%" + mtel + "%"));
                }

                if (!forosh && !pishforosh && !ejare)
                {

                    if (ClsBaseSetting.UserIn != null && ClsBaseSetting.UserIn.ID != 1) // ادمین کامل دسترسی دارد
                    {
                        // useraccsess از بالا گرفته شده
                        bool canForosh = useraccsess.Any(x => x.AccessID == 1);
                        bool canPishForosh = useraccsess.Any(x => x.AccessID == 2);
                        bool canEjare = useraccsess.Any(x => x.AccessID == 3);

                        var accessConditions = new List<string>();

                        if (canForosh)
                            accessConditions.Add("o.Forosh = 1");
                        if (canPishForosh)
                            accessConditions.Add("o.PishForosh = 1");
                        if (canEjare)
                            accessConditions.Add("o.Ejare = 1");

                        if (accessConditions.Count > 0)
                            query.Append(" AND (" + string.Join(" OR ", accessConditions) + ")");
                        else
                            query.Append(" AND 1=0"); // اگر هیچ دسترسی نداره، چیزی برنگردون
                    }
                    else
                    {
                        if (forosh)
                            query.Append(" AND o.Forosh = 1");

                        if (pishforosh)
                            query.Append(" AND o.PishForosh = 1");

                        if (ejare)
                            query.Append(" AND o.Ejare = 1");
                    }
                }
                else
                {
                    if (forosh)
                        query.Append(" AND o.Forosh = 1");

                    if (pishforosh)
                        query.Append(" AND o.PishForosh = 1");

                    if (ejare)
                        query.Append(" AND o.Ejare = 1");
                }


                //if (forosh)
                //    query.Append(" AND o.Forosh = 1");

                //if (pishforosh)
                //    query.Append(" AND o.PishForosh = 1");

                //if (ejare)
                //    query.Append(" AND o.Ejare = 1");




                //if (filetype > 0)
                //{
                //    query.Append(" AND o.FileTypeID = @FileType");
                //    parameters.Add(new SqlParameter("@FileType", filetype));
                //}
                if (filetype == 0)
                {
                    if (ClsBaseSetting.UserIn != null && ClsBaseSetting.UserIn.ID != 1) // ادمین کامل دسترسی دارد
                    {
                        var fileTypeConditions = new List<string>();
                        var userFileTypes = useraccsess.Where(x => x.AccessID >= 4).Select(x => x.AccessID - 3).ToList();

                        for (int i = 0; i < userFileTypes.Count; i++)
                        {
                            string paramName = "@FileType" + i;
                            fileTypeConditions.Add(paramName);
                            parameters.Add(new SqlParameter(paramName, userFileTypes[i]));
                        }

                        if (fileTypeConditions.Count > 0)
                            query.Append(" AND o.FileTypeID IN (" + string.Join(",", fileTypeConditions) + ")");
                        else
                            query.Append(" AND 1=0"); // اگر هیچ دسترسی نداره، چیزی برنگردون
                    }
                    else
                    {
                        if (filetype > 0)
                        {
                            query.Append(" AND o.FileTypeID = @FileType");
                            parameters.Add(new SqlParameter("@FileType", filetype));
                        }
                    }
                }
                else
                {
                    query.Append(" AND o.FileTypeID = @FileType");
                    parameters.Add(new SqlParameter("@FileType", filetype));
                }

                if (!string.IsNullOrWhiteSpace(address))
                {
                    query.Append(" AND (o.Address +N' _ پلاک '+ ISNULL(o.Plak,'') +N' _ واحد '+ ISNULL(o.Vahed,'')) LIKE @Address");
                    parameters.Add(new SqlParameter("@Address", "%" + address + "%"));
                }

                if (Region != null && Region.Count > 0)
                {
                    var regionConditions = new List<string>();

                    for (int i = 0; i < Region.Count; i++)
                    {
                        string paramName = "@Region" + i;
                        regionConditions.Add(paramName);
                        parameters.Add(new SqlParameter(paramName, Region[i]));
                    }

                    query.Append(" AND o.RegionID IN (" + string.Join(",", regionConditions) + ")");
                }


                if (!string.IsNullOrWhiteSpace(tozihat))
                {
                    query.Append(" AND o.Det LIKE @Tozihat");
                    parameters.Add(new SqlParameter("@Tozihat", "%" + tozihat + "%"));
                }
                if (!ejare)
                {
                    if (!string.IsNullOrWhiteSpace(price1_s))
                    {

                        query.Append(" AND ot.Price1 >= @Price1S");
                        parameters.Add(new SqlParameter("@Price1S", Convert.ToDecimal(price1_s)));
                    }

                    if (!string.IsNullOrWhiteSpace(price1_e))
                    {

                        query.Append(" AND ot.Price1 <= @Price1E");
                        parameters.Add(new SqlParameter("@Price1E", Convert.ToDecimal(price1_e)));
                    }

                    if (!string.IsNullOrWhiteSpace(price2_s))
                    {

                        query.Append(" AND ot.Price2 >= @Price2S");
                        parameters.Add(new SqlParameter("@Price2S", Convert.ToDecimal(price2_s)));
                    }

                    if (!string.IsNullOrWhiteSpace(price2_e))
                    {

                        query.Append(" AND ot.Price2 <= @Price2E");
                        parameters.Add(new SqlParameter("@Price2E", Convert.ToDecimal(price2_e)));
                    }
                }
                else
                {

                    // مقدار پیش‌فرض برای محاسبه از (اگر خالی باشد 0 در نظر گرفته شود)
                    decimal price1From = string.IsNullOrWhiteSpace(price1_s) ? 0 : Convert.ToDecimal(price1_s);
                    decimal price2From = string.IsNullOrWhiteSpace(price2_s) ? 0 : Convert.ToDecimal(price2_s);

                    // بازه از: جمع اجاره و رهن تبدیل‌شده به اجاره
                    decimal userFrom = price1From + (price2From * 0.03m);

                    // بازه تا: اگر هر دو فیلد تا خالی باشند، شرط بالا اعمال نشود
                    bool hasPrice1To = !string.IsNullOrWhiteSpace(price1_e);
                    bool hasPrice2To = !string.IsNullOrWhiteSpace(price2_e);

                    decimal price1To = hasPrice1To ? Convert.ToDecimal(price1_e) : 0;
                    decimal price2To = hasPrice2To ? Convert.ToDecimal(price2_e) : 0;

                    decimal userTo = price1To + (price2To * 0.03m);

                    if (userFrom > 0)
                    {
                        query.Append(" AND (ot.Price1 + (ot.Price2 * 0.03)) >= @UserFrom");
                        parameters.Add(new SqlParameter("@UserFrom", userFrom));
                    }

                    // فقط اگر بازه بالا مشخص شده باشد، شرط <= اضافه شود
                    if (hasPrice1To || hasPrice2To)
                    {
                        query.Append(" AND (ot.Price1 + (ot.Price2 * 0.03)) <= @UserTo");
                        parameters.Add(new SqlParameter("@UserTo", userTo));
                    }






                    //if (!string.IsNullOrWhiteSpace(price1_s) && !string.IsNullOrWhiteSpace(price2_s))
                    //{
                    //    query.Append(" AND  ((ot.Price1 + (ot.Price2 * 0.03)) >= @Price1S or (ot.Price2 + (ot.Price1 / (0.03))) >= @Price2S)");

                    //    parameters.Add(new SqlParameter("@Price1S", Convert.ToDecimal(price1_s)));
                    //    parameters.Add(new SqlParameter("@Price2S", Convert.ToDecimal(price2_s)));
                    //}
                    //else
                    //{
                    //    if (!string.IsNullOrWhiteSpace(price1_s))
                    //    {

                    //        query.Append(" AND  (ot.Price1 + (ot.Price2 * 0.03)) >= @Price1S");

                    //        parameters.Add(new SqlParameter("@Price1S", Convert.ToDecimal(price1_s)));
                    //    }
                    //    if (!string.IsNullOrWhiteSpace(price2_s))
                    //    {

                    //        query.Append(" AND (ot.Price2 + (ot.Price1 / (0.03))) >= @Price2S");
                    //        parameters.Add(new SqlParameter("@Price2S", Convert.ToDecimal(price2_s)));
                    //    }
                    //}
                    //if (!string.IsNullOrWhiteSpace(price1_e) && !string.IsNullOrWhiteSpace(price2_e))
                    //{
                    //    query.Append(" AND ((ot.Price1 + (ot.Price2 * 0.03)) <= @Price1E or (ot.Price2 + (ot.Price1 / (0.03))) <= @Price2E)");
                    //    parameters.Add(new SqlParameter("@Price1E", Convert.ToDecimal(price1_e)));
                    //    parameters.Add(new SqlParameter("@Price2E", Convert.ToDecimal(price2_e)));
                    //}
                    //else
                    //{
                    //    if (!string.IsNullOrWhiteSpace(price1_e))
                    //    {

                    //        query.Append(" AND (ot.Price1 + (ot.Price2 * 0.03)) <= @Price1E");
                    //        parameters.Add(new SqlParameter("@Price1E", Convert.ToDecimal(price1_e)));
                    //    }
                    //    if (!string.IsNullOrWhiteSpace(price2_e))
                    //    {

                    //        query.Append(" AND (ot.Price2 + (ot.Price1 / (0.03))) <= @Price2E");
                    //        parameters.Add(new SqlParameter("@Price2E", Convert.ToDecimal(price2_e)));
                    //    }
                    //}


                }
                //if (!string.IsNullOrWhiteSpace(price1_s))
                //{
                //    if (!ejare)
                //        query.Append(" AND ot.Price1 >= @Price1S");
                //    else
                //        query.Append(" AND  (ot.Price1 + (ot.Price2 * 0.03)) >= @Price1S");

                //    parameters.Add(new SqlParameter("@Price1S", Convert.ToDecimal(price1_s)));
                //}

                //if (!string.IsNullOrWhiteSpace(price1_e))
                //{
                //    if (!ejare)
                //        query.Append(" AND ot.Price1 <= @Price1E");
                //    else
                //        query.Append(" AND (ot.Price1 + (ot.Price2 * 0.03)) <= @Price1E");
                //    parameters.Add(new SqlParameter("@Price1E", Convert.ToDecimal(price1_e)));
                //}

                //if (!string.IsNullOrWhiteSpace(price2_s))
                //{
                //    if (!ejare)
                //        query.Append(" AND ot.Price2 >= @Price2S");
                //    else
                //        query.Append(" AND (ot.Price2 + (ot.Price1 / (0.03))) >= @Price2S");
                //    parameters.Add(new SqlParameter("@Price2S", Convert.ToDecimal(price2_s)));
                //}

                //if (!string.IsNullOrWhiteSpace(price2_e))
                //{
                //    if (!ejare)
                //        query.Append(" AND ot.Price2 <= @Price2E");
                //    else
                //        query.Append(" AND (ot.Price2 + (ot.Price1 / (0.03))) <= @Price2E");
                //    parameters.Add(new SqlParameter("@Price2E", Convert.ToDecimal(price2_e)));
                //}
                if (isdelete == 1)
                {
                    query.Append(" AND o.IsDelete =@IsDelete");
                    parameters.Add(new SqlParameter("@IsDelete", isdelete));
                }
                else if (isdelete == 0)
                {
                    query.Append(" AND o.IsDelete =@IsDelete");
                    parameters.Add(new SqlParameter("@IsDelete", isdelete));
                }


                if (khab_az != 0)
                {
                    query.Append(" AND ot.Khab >= @khab_az");
                    parameters.Add(new SqlParameter("@khab_az", khab_az));
                }
                if (khab_ta != 0)
                {
                    query.Append(" AND ot.Khab <= @khab_ta");
                    parameters.Add(new SqlParameter("@khab_ta", khab_ta));
                }
                if (senbana_az != 0)
                {
                    query.Append(" AND o.Senbana >= @senbana_az");
                    parameters.Add(new SqlParameter("@senbana_az", senbana_az));
                }
                if (senbana_ta != 0)
                {
                    query.Append(" AND o.Senbana <= @senbana_ta");
                    parameters.Add(new SqlParameter("@senbana_ta", senbana_ta));
                }
                if (tabaghe_az != null)
                {
                    query.Append(" AND ot.Tabaghe >= @tabaghe_az");
                    parameters.Add(new SqlParameter("@tabaghe_az", tabaghe_az));
                }
                if (tabaghe_ta != null)
                {
                    query.Append(" AND ot.Tabaghe <= @tabaghe_ta");
                    parameters.Add(new SqlParameter("@tabaghe_ta", tabaghe_ta));
                }

                if (asansor)
                {
                    query.Append(" AND ot.Asansor =1");

                }
                if (parking)
                {
                    query.Append(" AND o.PMotor =1");

                }
                if (anbari)
                {
                    query.Append(" AND ot.Anbari =1");

                }

                if (VahedinTbaghe_az.HasValue && VahedinTbaghe_az > 0)
                {
                    query.Append(" AND o.AllVahed >= @VahedinTbaghe_az");
                    parameters.Add(new SqlParameter("@VahedinTbaghe_az", VahedinTbaghe_az));
                }
                if (VahedinTbaghe_ta.HasValue && VahedinTbaghe_ta > 0)
                {
                    query.Append(" AND o.AllVahed <= @VahedinTbaghe_ta");
                    parameters.Add(new SqlParameter("@VahedinTbaghe_ta", VahedinTbaghe_ta));
                }
                if (Tabaghat_az.HasValue && Tabaghat_az > 0)
                {
                    query.Append(" AND o.AllTabaghe >= @Tabaghat_az");
                    parameters.Add(new SqlParameter("@Tabaghat_az", Tabaghat_az));
                }
                if (Tabaghat_ta.HasValue && Tabaghat_ta > 0)
                {
                    query.Append(" AND o.AllTabaghe <= @Tabaghat_ta");
                    parameters.Add(new SqlParameter("@Tabaghat_ta", Tabaghat_ta));
                }

                if (Metrazh_az.HasValue && Metrazh_az > 0)
                {
                    query.Append(" AND ot.Zirbana >= @Metrazh_az");
                    parameters.Add(new SqlParameter("@Metrazh_az", Metrazh_az));
                }
                if (Metrazh_ta.HasValue && Metrazh_ta > 0)
                {
                    query.Append(" AND ot.Zirbana <= @Metrazh_ta");
                    parameters.Add(new SqlParameter("@Metrazh_ta", Metrazh_ta));
                }


                if (Masahat_az.HasValue && Masahat_az > 0)
                {
                    query.Append(" AND o.Masahat >= @Masahat_az");
                    parameters.Add(new SqlParameter("@Masahat_az", Masahat_az));
                }
                if (Masahat_ta.HasValue && Masahat_ta > 0)
                {
                    query.Append(" AND o.Masahat <= @Masahat_ta");
                    parameters.Add(new SqlParameter("@Masahat_ta", Masahat_ta));
                }


                if (!string.IsNullOrEmpty(Parking_az))
                {
                    query.Append(" AND ot.Parking >= @Parking_az");
                    parameters.Add(new SqlParameter("@Parking_az", Parking_az));
                }
                if (!string.IsNullOrEmpty(Parking_ta))
                {
                    query.Append(" AND ot.Parking <= @Parking_ta");
                    parameters.Add(new SqlParameter("@Parking_ta", Parking_ta));
                }

                if (AsansorTabaghe.HasValue && AsansorTabaghe > 0)
                {
                    query.Append(" AND (ot.Tabaghe <= @AsansorTabaghe And (ot.Asansor=1 or ot.Asansor=0) or (ot.Tabaghe >= @AsansorTabaghe And ot.Asansor=1) )");
                    parameters.Add(new SqlParameter("@AsansorTabaghe", AsansorTabaghe));
                }

                if (vahed_az.HasValue && vahed_az > 0)
                {
                    query.Append(" AND (SELECT COUNT(*) FROM FIL.OwnerDet od WHERE od.OwnerID = o.ID) >= @VahedAz");
                    parameters.Add(new SqlParameter("@VahedAz", vahed_az.Value));
                }

                if (vahed_ta.HasValue && vahed_ta > 0)
                {
                    query.Append(" AND (SELECT COUNT(*) FROM FIL.OwnerDet od WHERE od.OwnerID = o.ID) <= @VahedTa");
                    parameters.Add(new SqlParameter("@VahedTa", vahed_ta.Value));
                }

                if (_type)
                {
                    if (startdate != null)
                    {
                        query.Append(" AND (SELECT MAX(v) \r\n             FROM (VALUES (o.ViewPhoneDate), (o.ModificationDate), (o.CreationDate)) AS value(v)\r\n            ) >= @startdate");
                        parameters.Add(new SqlParameter("@startdate", Convert.ToDateTime(startdate).TimeStartWith0000()));
                    }
                    if (endDate != null)
                    {
                        query.Append(" AND (SELECT MAX(v) \r\n             FROM (VALUES (o.ViewPhoneDate), (o.ModificationDate), (o.CreationDate)) AS value(v)\r\n            ) <= @enddate");
                        parameters.Add(new SqlParameter("@enddate", Convert.ToDateTime(endDate).TimeEndWith2359()));
                    }
                }
                else
                {
                    if (startdate != null)
                    {
                        query.Append(" AND (SELECT MAX(v) \r\n             FROM (VALUES (o.ModificationDate), (o.CreationDate)) AS value(v)\r\n            ) >= @startdate");
                        parameters.Add(new SqlParameter("@startdate", startdate));
                    }
                    if (endDate != null)
                    {
                        query.Append(" AND (SELECT MAX(v) \r\n             FROM (VALUES  (o.ModificationDate), (o.CreationDate)) AS value(v)\r\n            ) <= @enddate");
                        parameters.Add(new SqlParameter("@enddate", Convert.ToDateTime(endDate).TimeEndWith2359()));
                    }
                }
                // --- صفحه‌بندی ---
                if (_type)
                    query.Append(@" 
        ORDER BY LastActivityDate DESC
        OFFSET @Offset ROWS
        FETCH NEXT @PageSize ROWS ONLY;
    ");
                else
                    query.Append(@" 
        ORDER BY LastDate DESC
        OFFSET @Offset ROWS
        FETCH NEXT @PageSize ROWS ONLY;
    ");

                parameters.Add(new SqlParameter("@Offset", offset));
                parameters.Add(new SqlParameter("@PageSize", pageSize));

                // ✅ اجرای کوئری داده‌ها
                var items = _Contexct.Database.SqlQuery<RawFileListDto>(
                    query.ToString(),
                    parameters.ToArray()
                ).ToList();

                // ✅ محاسبه‌ی تعداد کل
                var countQuery = new StringBuilder(@"
        SELECT COUNT(*) 
        FROM FIL.Owners o
        LEFT JOIN FIL.OwnerDet ot ON ot.OwnerID = o.ID
        left join MNG.UserOwner uo on o.ID=uo.OwnerID
        WHERE 1=1
    ");

                var countParams = new List<SqlParameter>();
                foreach (var p in parameters)
                {
                    if (p.ParameterName != "@Offset" && p.ParameterName != "@PageSize")
                        countParams.Add(new SqlParameter(p.ParameterName, p.Value));
                }

                // اضافه کردن همان شرط‌ها برای شمارش
                countQuery.Append(query.ToString().Split(new[] { "WHERE 1=1" }, StringSplitOptions.None)[1].Split(new[] { "ORDER BY" }, StringSplitOptions.None)[0]);
                int totalCount = 0;
                try
                {
                    totalCount = _Contexct.Database.SqlQuery<int>(
                        countQuery.ToString(),
                        countParams.ToArray()
                    ).FirstOrDefault();
                }
                catch (Exception ex)
                {


                }

                // --- تبدیل به DTO ---
                List<FileListDto> lst = new List<FileListDto>();
                var _customerred = _Contexct.CustomerReadOwner_List.Where(x => x.CustomerID == idcustomer).Select(w => w.OwnerID).ToList();
                var _customer = _Contexct.Customer_List.Where(x => x.ID == idcustomer).FirstOrDefault();
                foreach (var item in items)
                {
                    int _newrecord = 0;
                    if (_customer != null && item.LastDate >= (_customer.ModificationDate ?? _customer.CreationDate) && !_customerred.Any(x => item.ID == x))
                    {
                        _newrecord = 1;
                    }
                    //var _newrecord = _customerred.Any(x => item.ID == x);
                    lst.Add(new FileListDto()
                    {
                        Address = item.Address,
                        ContractType = item.ContractType,
                        CreationDate = item.CreationDate.ToString().ToShamsiDateTime(),
                        ModificationDate = item.ModificationDate != null ? item.ModificationDate.ToString().ToShamsiDateTime() : null,
                        FileType = item.FileType,
                        ID = item.ID,
                        Price1 = item.Price1,
                        Price2 = item.Price2,
                        Region = item.Region,
                        Anbari = item.Anbari,
                        Asansor = item.Asansor,
                        MName = item.MName,
                        Parking = item.Parking,
                        Senbana = item.Senbana,
                        Tabaghe = item.Tabaghe,
                        Zirbana = item.Zirbana,
                        LastDate = item.LastDate.ToString().ToShamsiDateTime(),
                        isDeleted = item.isDeleted,
                        Masahat = Convert.ToInt32(item.Masahat),
                        Tol = Convert.ToInt32(item.Tol),
                        Newrecord = (idcustomer > 0 && _newrecord == 1) ? 1 : 0,
                        Customerid = idcustomer,
                        ReturnTime=item.ReturnTime?.ToString().ToShamsiDateTime(),

                    });
                }

                return new PagedResult<FileListDto>
                {
                    Items = lst,
                    TotalCount = totalCount,
                    PageNumber = pageNumber,
                    PageSize = pageSize
                };
            }
            catch (Exception ex)
            {
                return new PagedResult<FileListDto>();
                
            }
        }


        public OperationResult IsDelete(long ID)
        {
            var query = _Contexct.Owners_List.Where(x => x.ID == ID).FirstOrDefault();
            try
            {
                if (!query.IsDelete)
                {
                    query.IsDelete = true;
                    query.IsDeleteDateTime = DateTime.Now;
                    query.ReturnTime = null;
                }
                else
                {
                    query.IsDelete = false;
                    query.IsDeleteDateTime = null;
                    //query.ReturnTime = DateTime.Now;
                }

                _operationResult = Update(query);
                _operationResult.Status = OperationResult.OpreationResultStatus.Success;
            }
            catch (Exception ex)
            {
                _operationResult.Message = ex.Message;

            }
            return _operationResult;
        }
        public string GetLastFreeCode()
        {
            string _code = "1000";
            var query = _Contexct.Owners_List.OrderByDescending(x => x.Code).FirstOrDefault();
            if (query != null)
            {
                _code = (query.Code.ToInt64OrDefault() + 1).ToString();
            }
            return _code;
        }


    }
}
