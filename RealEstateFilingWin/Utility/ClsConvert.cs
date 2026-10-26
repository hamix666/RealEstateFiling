using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;


namespace RealEstateFilingWin.Utility
{
    public class ClsConvert
    {
        #region Method

        public static string FormatChenge(string obj)
        {
            string[] str = null;
            string aa = null;
            string aa2 = null;
            if (obj != "")
            {
                str = obj.Split('.');
                if (str != null)
                {
                    aa = string.Format("{0:N0}", Convert.ToDecimal(str[0]));
                    if (str.Length > 1)
                        if (Convert.ToDecimal(str[1]) > 0)//(int)NovinCommon.ClsBaseSetting.CountScale>0)
                            aa2 = '.' + str[1].Substring(0, 2);//(int)NovinCommon.ClsBaseSetting.CountScale);
                    obj = aa + aa2;
                }
            }

            return obj;
        }

        

        public static void Chenge_FormatLabel(DevComponents.DotNetBar.LabelX sender)
        {
            string total = string.Format("{0:N0}", Convert.ToDecimal(sender.Text));
            sender.Text = total;
        }

        public static void Chenge_FormatLabel(System.Windows.Forms.Label sender)
        {
            string total = string.Format("{0:N0}", Convert.ToDecimal(sender.Text));
            sender.Text = total;
        }
        public static byte[] ImageToByteArray(System.Drawing.Image imageIn)
        {
            if (imageIn == null) return null;
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            return ms.ToArray();
        }

        public static Image ByteArrayToImage(byte[] byteArrayIn)
        {
            if (byteArrayIn == null) return null;
            MemoryStream ms = new MemoryStream(byteArrayIn);
           
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }



        public static List<T> ConvertDataTable<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }
        public static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                    {
                        if (!(dr[column.ColumnName] is DBNull))
                            pro.SetValue(obj, dr[column.ColumnName], null);
                    }
                    else
                        continue;
                }
            }
            return obj;
        }



        

        
        #endregion


    }
}
