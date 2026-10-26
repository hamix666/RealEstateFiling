using System;
using System.Globalization;
using System.Linq;

namespace BasicComponent
{
    public static class DateExtensions
    {
        public static string ToShamsiDate(this string inputDate)
        {
            return inputDate != null ? ToShamsiDate(Convert.ToDateTime(inputDate)) : string.Empty;
        }

        public static string ToShamsiDate(this object inputDate)
        {
            return inputDate != null ? ToShamsiDate(Convert.ToDateTime(inputDate)) : string.Empty;
        }
        public static DateTime ConvertDateStyle(DateTime _date)
        {
            var dt = _date.ToString("yyyy/MM/dd");
            var rdt = Convert.ToDateTime(dt);
            return rdt;
        }
        public static string ToShamsiDateTime(this string inputDate)
        {
            DateTime dateTime;
            if (inputDate != null)
            {
                dateTime = Convert.ToDateTime(inputDate);
                if (dateTime.Year != 1)
                {
                    var cal = new PersianDateTime(dateTime); //FarsiLibrary.Utils.PersianDate(inputDate);
                                                             //#5425
                    return $"{cal.Year:0000}/{cal.Month:00}/{cal.Day:00} {cal.Hour:00}:{cal.Minute:00}:{cal.Second:00}";
                }
            }
            return string.Empty;
        }
        public static string ToShamsiDate(this DateTime inputDate)
        {
            if (inputDate != null && inputDate.Year != 1)
            {
                var cal = new PersianDateTime(inputDate); //FarsiLibrary.Utils.PersianDate(inputDate);
                return cal.Date.ToString("yyyy/MM/dd");
                //#5425
                //return $"{cal.Year.ToString("0000")}/{cal.Month.ToString("00")}/{cal.Day.ToString("00")}";
            }
            else
            {
                return string.Empty;
            }
        }
        public static string ToShamsiDate_Time(this DateTime inputDate)
        {
            if (inputDate != null)
            {
                var cal = new PersianDateTime(inputDate); //FarsiLibrary.Utils.PersianDate(inputDate);

                return $"{cal.Hour}:{cal.Minute}:{cal.Second} {cal.Year.ToString("0000")}/{cal.Month.ToString("00")}/{cal.Day.ToString("00")}";
            }
            else
            {
                return string.Empty;
            }
        }
        public static string GetTime(this DateTime inputDate)
        {
            if (inputDate != null)
            {
                return $"{inputDate.Hour.ToString("00")}:{inputDate.Minute.ToString("00")}";
            }
            else
            {
                return "00:00";
            }
        }

        public static string ToHijriDate(this string inputDate, int hijriAdjustment = 0)
        {
            return inputDate != null ? ToHijriDate(Convert.ToDateTime(inputDate), hijriAdjustment) : string.Empty;
        }

        public static string ToHijriDate(this object inputDate, int hijriAdjustment = 0)
        {
            return inputDate != null ? ToHijriDate(Convert.ToDateTime(inputDate), hijriAdjustment) : string.Empty;
        }

        public static string ToHijriDate(this DateTime inputDate, int hijriAdjustment = 0)
        {
            if (inputDate != null)
            {
                var cal = new System.Globalization.HijriCalendar();

                if (hijriAdjustment != 0)
                    cal.HijriAdjustment = hijriAdjustment;

                return $"{cal.GetYear(inputDate).ToString("0000")}/{cal.GetMonth(inputDate).ToString("00")}/{cal.GetDayOfMonth(inputDate).ToString("00")}";
            }
            else
            {
                return string.Empty;
            }
        }

        public static DateTime ToMiladiDate(this string inputDate)
        {
            DateTime str = new DateTime();
            try
            {
                var date = "";
                var time = "00:00:00";
                var lst = inputDate?.Split(' ').ToList();
                if (lst.Count() == 2)
                {
                    date = lst.Where(l => l.Contains("/")).FirstOrDefault();
                    time = lst.Where(l => l.Contains(":")).FirstOrDefault();
                }
                else
                    date = inputDate;

                if (date == "____/__/__" || date.Trim() == "") return str;
                if (date == null) return str;

                if (date != null)
                {
                    PersianCalendar pc = new PersianCalendar();
                    var spdate = date.Split('/');
                    var sptime = time.Split(':');
                    var _datetime = pc.ToDateTime(Convert.ToInt32(spdate[0]), Convert.ToInt32(spdate[1]), Convert.ToInt32(spdate[2]), Convert.ToInt32(sptime[0]), Convert.ToInt32(sptime[1]), Convert.ToInt32(sptime[2]), 0);
                    var aaa = new DateTime(Convert.ToInt32(spdate[0]), Convert.ToInt32(spdate[1]), Convert.ToInt32(spdate[2]), Convert.ToInt32(sptime[0]), Convert.ToInt32(sptime[1]), Convert.ToInt32(sptime[2]), 0, pc);
                    PersianDateTime persianDate = PersianDateTime.Parse(date, time);
                    return persianDate.ToDateTime();
                }
            }
            catch (Exception ex)
            {
                //BasicComponent.OtherExtension.Log(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name);
            }
            return str;
        }
        public static bool ComparisonDate(string start, string end)
        {


            DateTime _date1, _date2;
            
                _date1 = Convert.ToDateTime(start);
                _date2 = Convert.ToDateTime(end);
            
            

            if (_date2 <= _date1)
                return false;
            return true;
        }
        public static int Difference(string start, string end,out DateTime d1,out DateTime d2,out string d1p,out string d2p)
        {
            DateTime _date1, _date2;
            
                _date1 = Convert.ToDateTime(start);
                _date2 = Convert.ToDateTime(end);
           

            var difference = (_date2 - _date1).Days;
            d1 = _date2.AddDays(1);
            d2 = d1.AddDays(difference+1);
            d1p = d1.ToShamsiDate();
            d2p = d2.ToShamsiDate();
            return difference;
        }
        public static DateTime TimeStartWith0000(this DateTime inputDate)
        {
            try
            {
                TimeSpan Time = new TimeSpan(0, 0, 0);
                inputDate = inputDate.Date + Time;
            }
            catch (Exception ex)
            {
                //BasicComponent.OtherExtension.Log(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name);
            }
            return inputDate;
        }
        /// <summary>
        /// تبدیل ساعت به 23:59:59
        /// </summary>
        /// <param name="inputDate"></param>
        /// <returns></returns>
        public static DateTime TimeEndWith2359(this DateTime inputDate)
        {
            try
            {
                TimeSpan Time = new TimeSpan(23, 59, 59);
                inputDate = inputDate.Date + Time;
            }
            catch (Exception ex)
            {
                //BasicComponent.OtherExtension.Log(ex, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name);
            }
            return inputDate;
        }

        public static string GetDayAndMonth(this DateTime dateTime)
        {
            string str = "";
            PersianCalendar p = new PersianCalendar();
            switch (p.GetDayOfWeek(dateTime))
            {
                case DayOfWeek.Sunday: str = "یکشنبه";
                    break;
                case DayOfWeek.Monday: str = "دوشنبه";
                    break;
                case DayOfWeek.Tuesday: str = "سه شنبه";
                    break;
                case DayOfWeek.Wednesday: str = "چهارشنبه";
                    break;
                case DayOfWeek.Thursday: str = "پنج شنبه";
                    break;
                case DayOfWeek.Friday: str = "جمعه";
                    break;
                case DayOfWeek.Saturday: str = "شنبه";
                    break;
                default:
                    break;
            }
            var a = dateTime.ToShamsiDate().Split('/')[2];
            str +=  " " + a;
            switch (p.GetMonth(dateTime))
            {
                case 01: str += " " + "فروردین"; break;
                case 02: str += " " + "اردیبهشت"; break;
                case 03: str += " " + "خرداد"; break;
                case 04: str += " " + "تیر"; break;
                case 05: str += " " + "مرداد"; break;
                case 06: str += " " + "شهریور"; break;
                case 07: str += " " + "مهر"; break;
                case 08: str += " " + "آبان"; break;
                case 09: str += " " + "آذر"; break;
                case 10: str += " " + "دی"; break;
                case 11: str += " " + "بهمن"; break;
                case 12: str += " " + "اسفند"; break;
                default:
                    break;
            }
            var b =dateTime.ToShamsiDate().Split('/')[0];
            str += " " + b;
            return str;
        }

    }
}
