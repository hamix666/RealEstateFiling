using Janus.Windows.GridEX;
using System;
using System.Globalization;

namespace BasicComponent
{
    public static class GridExtensions
    {


        /// <summary>
        /// Convert cell value to shamsi date and show on it's text
        /// </summary>
        /// <param name="eXCell"></param>
        public static void FormatAsShamsiDate(this Janus.Windows.GridEX.GridEXCell eXCell)
        {
            if (eXCell != null)
                if (eXCell.Value is DateTime)
                    eXCell.Text = Convert.ToDateTime(eXCell.Value).ToShamsiDate();
        }

        /// <summary>
        /// Convert cell value to hijri date and show on it's text
        /// </summary>
        /// <param name="eXCell"></param>

        public static void FormatAsHijriDate(this Janus.Windows.GridEX.GridEXCell eXCell, int hijriAdjustment = 0)
        {
            if (eXCell != null)
                if (eXCell.Value is DateTime)
                    eXCell.Text = Convert.ToDateTime(eXCell.Value).ToHijriDate(hijriAdjustment);
        }

        public static bool IsNumeric(this string input, System.Globalization.NumberStyles numberStyle = System.Globalization.NumberStyles.Currency)
        {
            var result = double.TryParse(input, numberStyle, System.Globalization.CultureInfo.CurrentCulture, out double temp);
            return result;
        }

        public static bool IsNullOrEmpty(this object columnValue)
        {
            return string.IsNullOrEmpty(columnValue as string);
        }

        public static bool IsPercent(this GridEXColumn column)
        {
            return !string.IsNullOrEmpty(column.Key) ? column.Key.Trim().EndsWith("Percent") : false;
        }

        public static bool IsQty(this GridEXColumn column)
        {
            return !string.IsNullOrEmpty(column.Key) ? column.Key.Trim().StartsWith("QTY") : false;
        }

        public static bool IsCurrencyBase(this GridEXColumn column)
        {
            return !string.IsNullOrEmpty(column.Key) ? column.Key.Trim().EndsWith("BaseCurrency") || column.Key.Trim().EndsWith("BaseCurrency_O") : false;
        }

        public static bool IsCode(this GridEXColumn column)
        {
            return !string.IsNullOrEmpty(column.Key) ? column.Key.Trim() == "CodeGood" || column.Key.Trim() == "CodeInventory" : false;
        }

        public static void SetValueExists(this GridEX gridEX, string column, object value)
        {
            if (gridEX == null) return;
            if (column == null) return;

            var columnCollection = gridEX.RootTable.Columns;

            if (columnCollection.Contains(column))
            {
                gridEX.SetValue(column, Convert.ToString(value));
                //gridEX.CurrentRow.Cells[column].Value = null;
                //gridEX.CurrentRow.Cells[column].Value = Convert.ToString(value);
                //gridEX.CurrentRow.Cells[column].Text = Convert.ToString(value);

            }
        }

        public static void SetValueExistsByRow(this GridEXRow gridEXRow, string column, object value)
        {
            if (gridEXRow == null) return;
            if (column == null) return;
            gridEXRow.BeginEdit();
            gridEXRow.Cells[column].Value = null;
            gridEXRow.Cells[column].Value = Convert.ToString(value);
            gridEXRow.Cells[column].Text = Convert.ToString(value);
            //gridEXRow.EndEdit();

        }


        public static void SetValueExists(this GridEXRow gridEXRow, string column, object value)
        {
            if (gridEXRow == null) return;
            if (column == null) return;
            if (value == null) return;
            try
            {
                if (gridEXRow.GridEX.RootTable.Columns.Contains(column))
                {
                    var val = gridEXRow.Cells[column]?.Value;
                    if (val == null && !gridEXRow.Cells[column].Column.DataMember.ToLower().Contains("basecurrency"))
                        return;
                    else if (gridEXRow.Cells[column].Column.DataMember.ToLower().Contains("basecurrency"))
                    {
                        val = Convert.ToDecimal(0);
                    }
                    var newVal = value.ToString().Replace("/", ".").ToString();
                    switch (val)
                    {
                        case string _:
                            gridEXRow.Cells[column].Value = newVal;
                            break;
                        case int _:
                            gridEXRow.Cells[column].Value = int.Parse(newVal);
                            break;
                        case long _:
                            gridEXRow.Cells[column].Value = long.Parse(newVal);
                            break;
                        case decimal _:
                            gridEXRow.Cells[column].Value = decimal.Parse(newVal, CultureInfo.InvariantCulture);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                new Exception(value.ToString());
                //ex.AddToLog();
            }
        }
        public static object GetValueExists(this GridEXRow gridEXCell, string column)
        {

            if (gridEXCell == null) return null;

            if (gridEXCell.GridEX.RootTable.Columns.Contains(column))
                return gridEXCell.Cells[column].Value;
            else
                return null;
        }

        public static object GetValueExists(this GridEX gridEX, string column)
        {

            if (gridEX == null) return null;

            if (gridEX.RootTable.Columns.Contains(column) && gridEX.CurrentRow != null)
                return gridEX.GetValue(column);
            //return gridEX.CurrentRow.Cells[column].Value;
            else
                return null;
        }
        public static void ColumnsEditTypeExist(this GridEX gridEX, string Column, EditType editType)
        {
            if (gridEX != null && gridEX.RootTable.Columns.Contains(Column))

                gridEX.CurrentRow.Cells[Column].EditType = editType;
        }
        public static void SelectableExist(this GridEX gridEX, string Column, bool Selectable)
        {
            if (gridEX != null && gridEX.RootTable.Columns.Contains(Column))

                gridEX.RootTable.Columns[Column].Selectable = Selectable;
        }

        /// <summary>
        /// فرمت سلول های گرید با تعداد اعشار دریافتی
        /// </summary>
        /// <param name="eXCell"></param>
        /// <param name="precisionCount"></param>
        public static void FormatPrecisionCount(this Janus.Windows.GridEX.GridEXCell eXCell, int precisionCount)
        {
            if (eXCell != null && eXCell.Value != "")
            {

                eXCell.Text = Convert.ToDecimal(eXCell.Value).ToString($"N{precisionCount}");

            }
        }


        /// <summary>
        /// نمایش اعداد دیسمال با تعداداعشار دریافتی
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static string FormatPrecisionCount(this decimal number, int precisionCount)
        {
            return number.ToString($"N{precisionCount}");
        }


        /// <summary>
        /// set Default FormatString and InputMask For GridEXColumn with Input Parameter
        /// </summary>
        /// <param name="column"></param>

        public static void SetPrecisionCount(this Janus.Windows.GridEX.GridEXColumn column, int formatStringPrecisionCount, int inputMaskPrecisionCount)
        {
            if (column == null)
                return;

            if (column.FormatString != $"N{formatStringPrecisionCount}")
                column.FormatString = column.TotalFormatString = $"N{formatStringPrecisionCount}";


            if (column.InputMask != $"Number{inputMaskPrecisionCount}")
                column.InputMask = $"Number{inputMaskPrecisionCount}";

            if (column.AggregateFunction == AggregateFunction.Sum)
                column.TotalFormatString = column.TotalFormatString = $"N{formatStringPrecisionCount}";

        }
        public static void SetPrecisionCountCells(this Janus.Windows.GridEX.GridEXCell cell, int formatStringPrecisionCount, int inputMaskPrecisionCount)
        {

            if (cell == null)
                return;

            if (cell.Column.FormatString != $"N{formatStringPrecisionCount}")
                cell.Column.FormatString = cell.Column.TotalFormatString = $"N{formatStringPrecisionCount}";


            if (cell.Column.InputMask != $"Number{inputMaskPrecisionCount}")
                cell.Column.InputMask = $"Number{inputMaskPrecisionCount}";

            if (cell.Column.AggregateFunction == AggregateFunction.Sum)
                cell.Column.TotalFormatString = cell.Column.TotalFormatString = $"N{formatStringPrecisionCount}";

        }
        /// <summary>
        /// set Default FormatString and InputMask For GridEXColumn with Input Parameter
        /// </summary>
        /// <param name="column"></param>

        public static void SetCustomFormatAndMask(this Janus.Windows.GridEX.GridEXColumn column, string formatString, string inputMask)
        {
            if (column == null)
                return;

            if (column.FormatString != formatString)
                column.FormatString = column.TotalFormatString = inputMask;

            if (column.InputMask != inputMask)
                column.InputMask = inputMask;
        }




        public static void ClearFormat(this Janus.Windows.GridEX.GridEXColumn column, bool selectable = false)
        {
            if (column == null)
                return;

            column.FormatString = null;
            column.TotalFormatString = null;
            column.InputMask = null;
            column.Selectable = selectable;
        }
    }
}
