
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace RealEstateFilingWin.Utility
{
    public static class CommonExtension
    {
        public static void ApplyToAllControls(Control parent)
        {
            foreach (Control c in parent.Controls)
            {
                // تبدیل اعداد هر متنی
                c.Text = ToPersianNumbers(c.Text);

                // اگر کنترل فرزند دارد، بازگشتی برو داخلش
                if (c.HasChildren)
                    ApplyToAllControls(c);
            }
        }

        public static string ToPersianNumbers(string input)
        {
            if (string.IsNullOrEmpty(input)) return input;

            return input
                .Replace("0", "۰").Replace("1", "۱")
                .Replace("2", "۲").Replace("3", "۳")
                .Replace("4", "۴").Replace("5", "۵")
                .Replace("6", "۶").Replace("7", "۷")
                .Replace("8", "۸").Replace("9", "۹");
        }
        public static long ToInt64OrDefault2(this string value, long defaultValue = 0)
        {
            if (string.IsNullOrWhiteSpace(value))
                return defaultValue;

            // جایگزینی علائم اشتباه و فاصله‌ها
            value = value.Trim().Replace("/", ".").Replace(",", ".");

            // اگر مقدار اعشاری بود، اول به decimal بخون بعد به long تبدیل کن
            if (decimal.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var dec))
                return (long)dec;

            return defaultValue;
        }
        public static int ToInt32OrDefault2(this string value, int defaultValue = 0)
        {
            if (string.IsNullOrWhiteSpace(value))
                return defaultValue;

            // جایگزینی علائم اشتباه و فاصله‌ها
            value = value.Trim().Replace("/", ".").Replace(",", ".");

            // اگر مقدار اعشاری بود، اول به decimal بخون بعد به long تبدیل کن
            if (decimal.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var dec))
                return (int)dec;

            return defaultValue;
        }
        public static Int32 ToInt32OrDefault(this string value, Int32 defaultValue = 0)
        {
            Int32 result;
            return Int32.TryParse(value, out result) ? result : defaultValue;
        }

        public static Int64 ToInt64OrDefault(this string value, Int64 defaultValue = 0)
        {
            Int64 result;
            return Int64.TryParse(value, out result) ? result : defaultValue;
        }
        public static string ToStringOrDefult(this object value)
        {
            String result;
            if (value == null)
            {
                result = "";

            }
            else
                result = Convert.ToString(value);
            return result;
        }
        public static Decimal ToDecimalOrDefault(this object value, Decimal defaultValue = 0)
        {
            try
            {
                
                if (value == null) value = 0;
                var _value = value.ToString();
                decimal result;
                var a = (decimal.TryParse(_value, out result) ? result : defaultValue);
                decimal aa = 0;
                if (!value.ToString().Contains(".") && !value.ToString().Contains("/"))
                    //aa = Convert.ToDecimal(string.Format("{0:N2}", Convert.ToDecimal(a.ToString().Replace(".", "/"))));
                    aa = Math.Round(a, Convert.ToInt32(defaultValue));
                else
                    aa = a;
                return Convert.ToDecimal(aa);
            }
            catch (Exception)
            {
                return 0;
            }

        }
        public static bool ToBooleanOrDefault(this object value, bool defaultValue = false)
        {
            try
            {
                if (value == null) return defaultValue;
                string valueStr = Convert.ToString(value);
                return bool.Parse(valueStr);
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static Int64 ToInt64OrDefault(this object value, Int64 defaultValue = 0)
        {
            if (value == null) return defaultValue;
            string valueStr = Convert.ToString(value);
            return valueStr.ToInt64OrDefault();
        }

        public static Int32 ToInt32OrDefault(this object value, Int32 defaultValue = 0)
        {
            if (value == null) return defaultValue;
            string valueStr = Convert.ToString(value);
            return valueStr.ToInt32OrDefault();
        }

        public static List<T> ConvertDataTable<T>(this DataTable dt)
        {
            List<T> data = new List<T>();
            var rows = dt.Rows;
            foreach (DataRow row in rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }

        public static T GetItem<T>(this DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            var index = 0;
            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                    {
                        if (!(dr[column.ColumnName] is DBNull))
                        {
                            var objct = dr.ItemArray[index];
                            var param = Expression.Parameter(objct.GetType());
                            //if (pro.PropertyType.Name == "Int64")
                            //{
                            //    var value = Expression.Lambda(Expression.Convert(param, pro.PropertyType), param).Compile().DynamicInvoke(objct);

                            //    pro.SetValue(obj, value);

                            //}
                            //else
                            //{


                            var value = Expression.Lambda(Expression.Convert(param, pro.PropertyType), param).Compile().DynamicInvoke(objct);

                            pro.SetValue(obj, value);
                            //}
                        }
                    }
                    else
                        continue;
                }
                index++;
            }
            return obj;
        }

        public static long GetCountNumberInString(this string text)
        {
            long n;
            string number = "";
            if (text == null || text == "") n = 0;
            else
            {
                for (int i = 0; i < text.Length; i++)
                {
                    if (char.IsNumber(text[i]))
                    {
                        if (number == "")
                            number = text[i].ToString();
                        else
                            number = number + text[i];
                    }
                }
                n = number.ToInt64OrDefault();
            }
            return n;
        }

        

        #region Convert byte array to an Object
        public static byte[] ObjectToByteArray(Object obj)
        {
            if (obj == null)
                return null;

            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            bf.Serialize(ms, obj);

            return ms.ToArray();
        }
        public static Object ByteArrayToObject(byte[] arrBytes)
        {
            
            MemoryStream memStream = new MemoryStream();
            BinaryFormatter binForm = new BinaryFormatter();
            memStream.Write(arrBytes, 0, arrBytes.Length);
            memStream.Seek(0, SeekOrigin.Begin);
            //Object obj = (Object)binForm.Deserialize(memStream);
            Object obj = (Object)binForm.Deserialize(memStream);

            return obj;
        }
        #endregion

        private static readonly ToolTip _toolTip = new ToolTip()
        {
            AutoPopDelay = 8000,      // زمان ماندن Tooltip
            InitialDelay = 400,       // تأخیر اولیه قبل از نمایش
            ReshowDelay = 100,        // زمان تأخیر بین نمایش‌های متوالی
            ShowAlways = true,        // همیشه نمایش داده شود
            IsBalloon = true,         // به صورت حبابی نمایش بده (زیباتر)
            BackColor = Color.LightYellow, // رنگ پس‌زمینه
            ForeColor = Color.Black,  // رنگ متن
            ToolTipIcon = ToolTipIcon.Info, // آیکون اطلاعات
            ToolTipTitle = "راهنما"  // عنوان بالای Tooltip
            

        };

        /// <summary>
        /// اضافه کردن Tooltip به هر کنترل
        /// </summary>
        /// <param name="control">کنترلی که می‌خوای Tooltip داشته باشه</param>
        /// <param name="text">متن Tooltip</param>
        public static void AddToolTip(Control control, string text)
        {
            if (control == null || string.IsNullOrWhiteSpace(text))
                return;

            _toolTip.SetToolTip(control, text);
        }
    }
}
