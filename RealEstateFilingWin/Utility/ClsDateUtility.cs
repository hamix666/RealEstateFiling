using System;
using System.Globalization;

namespace RealEstateFilingWin.Utility
{
    public static class ClsDateUtility
    {
        #region Method
        public static string DateTimeNow()
        {
            
                return ClsDateUtility.ConvertToPersianDate(DateTime.Now);
            
        }
        public static string ConvertToPersianDate(DateTime input)
        {
            try
            {
                if (input != null)
                {
                    PersianCalendar cal = new PersianCalendar();
                    int Month = cal.GetMonth(input);
                    int Day = cal.GetDayOfMonth(input);

                    string str =
                     string.Format("{0}/{1}/{2}", cal.GetYear(input)
                        , Month < 10 ? "0" + Month.ToString() : Month.ToString()
                        , Day < 10 ? "0" + Day.ToString() : Day.ToString());
                    return  str.Substring (0,str.Length );
                }
                else
                {
                    return "";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DateTime ConvertToMiladiDate(string strdate)
        {
            DateTime str = new DateTime();
            if (strdate == "____/__/__") return str;
            if (strdate.Contains(" ")) return str;        

            string[] strList = strdate.Substring(0, strdate.Length).Split('/');
            
            if (strList.Length == 3)
            {
                try
                {
                    if (strList[2].Length == 2)
                    {
                        strList[2] = "13" + strList[2];
                    }
                    int PYear = Convert.ToInt32(strList[2]);
                    int PMonth = Convert.ToInt32(strList[1]);
                    int PDay = Convert.ToInt32(strList[0]);

                    System.Globalization.PersianCalendar pc = new System.Globalization.PersianCalendar();
                    str = pc.ToDateTime(PYear, PMonth, PDay, 1, 1, 1, 1, GregorianCalendar.ADEra);
                }
                catch
                {
                    if (strList[0].Length == 2)
                    {
                        strList[0] = "13" + strList[0];
                    }
                    int PYear = Convert.ToInt32(strList[0]);
                    int PMonth = Convert.ToInt32(strList[1]);
                    if (strList[2].Length > 2)
                    {
                        strList[2] = strList[2].Substring(2, 2);
                    }
                    int PDay = Convert.ToInt32(strList[2]);
                    System.Globalization.PersianCalendar pc = new System.Globalization.PersianCalendar();
                    try
                    {
                        str = pc.ToDateTime(PYear, PMonth, PDay, 1, 1, 1, 1, GregorianCalendar.ADEra);
                    }
                    catch
                    {
                        try
                        {
                            string strDay = strList[2].Substring(2, 2);
                            PDay = Convert.ToInt32(strDay);
                            str = pc.ToDateTime(PYear, PMonth, PDay, 1, 1, 1, 1, GregorianCalendar.ADEra);
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                }
            }
            return str;
        }
        public static bool ValidateDate(string MyDate)
        {


            if (MyDate == "") return true;
            DateTime temp;
            if (DateTime.TryParse(MyDate, out temp))
            {

                return true;
            }
            else
            {
                if (
                    (Convert.ToInt32(MyDate.Substring(5, 2)) < 6 
                    && (MyDate.Substring(8) == "29" || MyDate.Substring(8) == "30" 
                    || MyDate.Substring(8) == "31") || (Convert.ToInt32(MyDate.Substring(5, 2)) > 6 
                    && (MyDate.Substring(8) == "31"))))
                    return true;
                else
                    return false;
            }
        }
        public static bool ComparisonDate(string start,string end)
        {
            DateTime _date1, _date2;
            _date1= ClsDateUtility.ConvertToMiladiDate(start) ;
            _date2=  ClsDateUtility.ConvertToMiladiDate(end)  ;

            if (_date2 < _date1)
                return false;
            return true;
        }
        #endregion
    }
}
