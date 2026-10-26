using Janus.Windows.GridEX;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;



namespace BasicComponent
{
    public partial class ClsGrid : Janus.Windows.GridEX.GridEX, ClsInterface.IBasicComponent
    {
        #region Property & Variable

        public int PrecisionNumber { get; set; }

        private IList<CustomGridColumn> _customGridColumns;
        private IList<CustomCurrencyModel> _currencyList;
        private int _currentDecimalPrecisionNumber;

        private Janus.Windows.GridEX.GridEXColumn M_Col;
        private Janus.Windows.GridEX.GridEXColumnSet M_ColSet;
        public GridEXRow M_Row, S_Row;

        public string ExcelName = "";

        private int _contextMenu;

        public int ContextMenuS
        {
            get { return _contextMenu; }
            set { _contextMenu = value; }
        }
        public bool M_Edit;
        private bool isChanged = true;

       
        #endregion

        #region Constructor
        public ClsGrid(IContainer container)
        {

            if (this.Site == null || !this.Site.DesignMode)
            {
                container.Add(this);
                InitializeComponent();

                if (this.FilterMode == Janus.Windows.GridEX.FilterMode.Automatic)
                {
                    this.Col = 1;
                    this.Row = -2;
                }
            }
            M_Edit = false;
            this.ColumnAutoResize = false;
        }

        #endregion

        #region Events
        private void ItemAutoSizeColumn_Click(object sender, EventArgs e)
        {
            ColumnAutoResize = !ColumnAutoResize;
        }

        private void ItemChoose_Click(object sender, EventArgs e)
        {
            ShowFieldChooser(this.FindForm(), "انتخاب فیلدهای بیشتر");

        }
        private void Remove_Sort_Click(object sender, EventArgs e)
        {

            this.RootTable.SortKeys.Clear();



        }

        private void ItemdelColumn_Click(object sender, EventArgs e)
        {
            if(M_ColSet != null)
            {
                M_ColSet.Visible = false;
            }
            else if (M_Col != null)
            {
                M_Col.Visible = false;
            }
            else 
            {
               
            }
        }

        private void ItemDelLayout_Click(object sender, EventArgs e)
        {
           
            try
            {
                System.IO.File.Delete(Application.StartupPath + "\\ProFile\\" + GetProfileName(this));
            }
            catch (Exception ex)
            {
                
            }


        }
        System.Windows.Forms.SaveFileDialog tmpDigalog = new SaveFileDialog();
        private void ItemExportToExcel_Click(object sender, EventArgs e)
        {
            try
            {

                tmpDigalog.FileName = ExcelName + ".xls";
                tmpDigalog.AddExtension = true;
                tmpDigalog.DefaultExt = "xls";
                tmpDigalog.Filter = "Excel files(*.xls)|*.xls";
                if (tmpDigalog.ShowDialog() == DialogResult.OK)
                {
                    using (Janus.Windows.GridEX.Export.GridEXExporter GridEXExporter1 =
                        new Janus.Windows.GridEX.Export.GridEXExporter())
                    {
                        GridEXExporter1.GridEX = this;
                        GridEXExporter1.IncludeHeaders = true;
                        GridEXExporter1.IncludeChildTables = true;
                        GridEXExporter1.IncludeFormatStyle = true;
                        GridEXExporter1.IncludeExcelProcessingInstruction = true;
                        using (System.IO.FileStream tmpIo =
                            new System.IO.FileStream(tmpDigalog.FileName, System.IO.FileMode.Create))
                        {
                            GridEXExporter1.Export(tmpIo);

                        }
                    }
                }
            }
            catch (Exception ex)
            {

                SaveFileDialog SaveFileDialog = new SaveFileDialog();
                SaveFileDialog.Filter = "Excel Documents (*.xls)|*.xls";
                SaveFileDialog.FileName = "export.xls";
                //if (SaveFileDialog.ShowDialog() == DialogResult.OK)
                //{
                //ClsObserver.ToCsV(this, tmpDigalog.FileName);
                //}
                //throw ex;
            }
        }

        private void ItemFilter_Click(object sender, EventArgs e)
        {
            this.FilterMode = FilterMode == Janus.Windows.GridEX.FilterMode.Automatic ?
                Janus.Windows.GridEX.FilterMode.None : Janus.Windows.GridEX.FilterMode.Automatic;
        }

        private void ItemGroupBy_Click(object sender, EventArgs e)
        {
            GroupByBoxVisible = !GroupByBoxVisible;
        }

        private void ItemRowNo_Click(object sender, EventArgs e)
        {
            RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            RootTable.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            RowHeaderContent = RowHeaderContent == Janus.Windows.GridEX.RowHeaderContent.RowIndex ?
                Janus.Windows.GridEX.RowHeaderContent.Default : Janus.Windows.GridEX.RowHeaderContent.RowIndex;
            RootTable.RowHeaderContent = RootTable.RowHeaderContent == Janus.Windows.GridEX.RowHeaderContent.RowIndex ?
                            Janus.Windows.GridEX.RowHeaderContent.Default : Janus.Windows.GridEX.RowHeaderContent.RowIndex;


        }
        private void ItemRowNoIncreaseWidth_Click(object sendeer, EventArgs e)
        {
            this.RootTable.RowHeaderWidth += 50;
        }
        private void ItemRowNoDecreaseWidth_Click(object sendeer, EventArgs e)
        {
            if (this.RootTable.RowHeaderWidth <= 50)
                return;
            this.RootTable.RowHeaderWidth -= 50;

            //DataGridViewRowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.EnableResizing;
        }
        private void ItemCopy_Click(object sender, EventArgs e)
        {

            if (M_Row.DataRow != null)
            {
                S_Row = M_Row;
                ItemPaste.Enabled = true;
            }
        }

        private void ItemPaste_Click(object sender, EventArgs e)
        {
            //if (S_Row != null)
            //{
            //    foreach (var itemCell in S_Row.Table.Columns)
            //    {
            //        var index = S_Row.Cells[((Janus.Windows.GridEX.GridEXColumn) itemCell).Key].Value;
            //        foreach (var column in this.RootTable.Columns)
            //        {
            //            if (((Janus.Windows.GridEX.GridEXColumn)itemCell).Key == ((Janus.Windows.GridEX.GridEXColumn)column).Key)
            //            {
            //                this.SetValue(((Janus.Windows.GridEX.GridEXColumn)column).Key,index);
            //            }
            //        }
            //    }
            //    //CurrentRow.GridEX.DataSource = S_Row.DataRow;
            //    //S_Row.DataRow
            //    ItemPaste.Enabled = false;
            //}
        }

        //private void RecordInsert_Click(object sender,EventArgs e)
        //{

        //    if (this.CurrentRow !=null && this.CurrentRow.DataRow != null)
        //    {

        //        var b = this.CurrentRow.DataRow;
        //        var a = this.DataSource;




        //    }

        //}

        private void ItemRTL_Click(object sender, EventArgs e)
        {
            if (M_Col != null)
                M_Col.RightToLeft = M_Col.RightToLeft == RightToLeft.Yes ?
                    RightToLeft.No : RightToLeft.Yes;
        }

        private void ItemSaveLayout_Click(object sender, EventArgs e)
        {
            string filePath = Path.Combine(Application.StartupPath, "ProFile", GetProfileName(this));

            // اطمینان از وجود پوشه
            Directory.CreateDirectory(Path.GetDirectoryName(filePath));

            // فقط با using فایل را باز کن و بنویس
            using (FileStream tmpIO = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                SaveLayoutFile(tmpIO);
            }

        }

        private void ItemShowGroupSum_Click(object sender, EventArgs e)
        {
            RootTable.GroupTotals = RootTable.GroupTotals == Janus.Windows.GridEX.GroupTotals.Always ?
                Janus.Windows.GridEX.GroupTotals.Never : Janus.Windows.GridEX.GroupTotals.Always;

        }

        private void ItemShowTotalSum_Click(object sender, EventArgs e)
        {
            RootTable.TotalRow = RootTable.TotalRow == Janus.Windows.GridEX.InheritableBoolean.True ?
                            Janus.Windows.GridEX.InheritableBoolean.False : Janus.Windows.GridEX.InheritableBoolean.True;

        }

        private void ItemWordwrap_Click(object sender, EventArgs e)
        {
            if (M_Col != null)
            {
                M_Col.WordWrap = !M_Col.WordWrap;
                M_Col.MaxLines = 10;
            }
        }

        private void ItemSum_Click(object sender, EventArgs e)
        {
            if (M_Col != null)
            {
                M_Col.AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum;
                M_Col.TotalFormatString = "{0} جمع";
            }
        }

        private void ItemAVG_Click(object sender, EventArgs e)
        {
            if (M_Col != null)
            {
                M_Col.AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Average;
                M_Col.TotalFormatString = "{0} میانگین";
            }
        }

        private void ItemCount_Click(object sender, EventArgs e)
        {
            if (M_Col != null)
            {
                M_Col.AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Count;
                M_Col.TotalFormatString = "{0} تعداد";
            }
        }

        private void ItemMax_Click(object sender, EventArgs e)
        {
            if (M_Col != null)
            {
                M_Col.AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Max;
                M_Col.TotalFormatString = "{0} بیشترین";
            }
        }

        private void ItemMin_Click(object sender, EventArgs e)
        {
            if (M_Col != null)
            {
                M_Col.AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Min;
                M_Col.TotalFormatString = "{0} کمترین";
            }
        }

        private void ClsGrid_FormattingRow(object sender, RowLoadEventArgs e)
        {
            if (_customGridColumns != null)
                FormatRow(e.Row);

        }

        private void ClsGrid_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right &&
                this.HitTest(e.X, e.Y) == Janus.Windows.GridEX.GridArea.ColumnHeader)
            {
                M_Col = ColumnFromPoint(e.X, e.Y);
                

            }
            if (e.Button == System.Windows.Forms.MouseButtons.Right &&
                this.HitTest(e.X, e.Y) == Janus.Windows.GridEX.GridArea.ColumnSetHeader)
            {
                M_ColSet = ColumnSetFromPoint(e.X, e.Y);


            }
            else if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                if (this.CopyAndPaste)
                {
                    ItemCopy.Enabled = true;
                    M_Row = this.CurrentRow;
                }
                else
                {
                    ItemCopy.Enabled = false;
                    ItemPaste.Enabled = false;
                }
            }
        }


        private void ClsGrid_KeyDown(object sender, KeyEventArgs e)
        {
            int CurrentIndex = 0;
            if (this.CurrentColumn != null)
            {
                CurrentIndex = this.CurrentColumn.Index;
            }


            if (e.KeyCode == Keys.Delete)
            {
                e.Handled = true;
                return;
            }
            if (e.KeyCode == Keys.F3 && this.RecordCount > 0 && this.CurrentRow.RowType != RowType.NewRecord)
            {
                this.CurrentRow.Delete();
                return;
            }
            try
            {

                /// جداکردن به صورت سه رقمی     
                if ((e.KeyCode == Keys.Enter && this.CurrentColumn?.DataTypeCode == TypeCode.Decimal && this.CurrentRow.Cells[this.CurrentColumn.DataMember].Column.Selectable == true && this.CurrentRow.Cells[this.CurrentColumn.DataMember].Column.EditType != EditType.NoEdit) && this.ViewStatus == ClsInterface.EnumViewStatus.EditMode && this.LockStatus == ClsInterface.EnumLockStatus.EditAble)
                {

                    Decimal _mValue = 0;
                    if (this.GetValue(this.CurrentColumn.DataMember) != null && this.GetValue(this.CurrentColumn.DataMember) != "")
                        _mValue = Convert.ToDecimal(this.GetValueExists(this.CurrentColumn.DataMember));

                    e.Handled = true;
                    //_frmInsertValue = new FrmInsertValue();
                    //_frmInsertValue.M_Value = _mValue.ToString();
                    //_frmInsertValue.M_ClsGrid = this;
                    //_frmInsertValue.ShowDialog();
                    return;
                }
                else
                {

                    if (e.KeyCode == Keys.F11)
                    {
                        this.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
                        this.Col = 0;
                        this.Row = -2;
                    }
                    else if (e.KeyCode == Keys.Enter &&
                        (((this.GetValueExists(this.CurrentColumn?.DataMember) == null ? "" : this.GetValueExists(this.CurrentColumn?.DataMember).ToString()) == "") || isChanged) &&
                        this.CurrentColumn?.EditType == Janus.Windows.GridEX.EditType.DropDownList && this.ViewStatus == ClsInterface.EnumViewStatus.EditMode)
                    {
                        var _col = this.CurrentRow.Cells[this.CurrentColumn.DataMember].Column.Index;
                        
                        SendKeys.Send("{F4}");
                        //this.Col = _col;
                        e.Handled = true;
                        //if (!this.Name.Contains("Inventory"))
                        //{
                        //    this.Col -= 1;
                        //    SendKeys.Send("{F4}");
                        //}
                        //else
                        //{
                        //    this.Col = this.RootTable.Columns["CurrencyID"].Index - 2;
                        //    SendKeys.Send("{F4}");
                        //}


                    }

                    else if (e.Modifiers == Keys.Control && e.KeyCode == Keys.Space)
                    {
                        if (this.CurrentColumn == null) return;

                        if (this.CurrentColumn.EditType == Janus.Windows.GridEX.EditType.DropDownList && this.ViewStatus == ClsInterface.EnumViewStatus.EditMode)
                            SendKeys.Send("{F4}");
                    }
                    else if (e.KeyCode == Keys.Space)
                    {
                        return;
                    }
                    else if ((e.Modifiers == Keys.Control && e.KeyCode == Keys.Delete) && this.RowCount != 0 && this.ViewStatus == ClsInterface.EnumViewStatus.EditMode)
                    {
                        if (this.CurrentColumn == null)
                        {
                            e.Handled = true;
                            return;
                        }
                        if (this.CurrentRow.GridEX.RootTable.AllowEdit == InheritableBoolean.True || this.CurrentRow.GridEX.RootTable.AllowEdit == InheritableBoolean.Default)
                        {
                            if (this.CurrentColumn.EditType != EditType.NoEdit)
                            {
                                this.SetValue(this.CurrentColumn, null);

                            }
                            e.Handled = true;
                        }
                        else
                        {
                            e.Handled = true;
                        }
                    }
                    else if (e.Modifiers == Keys.Control && e.KeyCode == Keys.T)
                    {
                        if (this.CurrentColumn != null && this.CurrentColumn.DataMember.Contains("Date"))
                        {

                           

                                this.SetValue(this.CurrentColumn.DataMember, DateTime.Now.ToString("yyyy/MM/dd").ToShamsiDate());

                           
                            this.Select();
                        }
                    }
                    else if (e.Modifiers == Keys.Control && e.KeyCode == Keys.S)
                    {
                        ItemSaveLayout.PerformClick();
                    }

                }
            }
            catch (Exception ex)
            {
            }
        }
        #endregion

        #region IBasicComponent Members

        [Category("Observer")]
        public string BLObjectName
        {
            get;
            set;

        }
        [Category("Observer")]
        public string FieldName
        {
            get;
            set;

        }
        [Category("Observer")]
        public string GetMethodName
        {
            get;
            set;
        }
        [Category("Observer")]
        public void ClearValue()
        {
            //this.BoundMode = Janus.Windows.GridEX.BoundMode.Unbound;
            //this.ClearItems();
            //this.BoundMode = Janus.Windows.GridEX.BoundMode.Bound;

            this.DataSource = null;
        }

        ClsInterface.EnumViewStatus _ViewStatus;
        [Category("Observer")]
        public ClsInterface.EnumViewStatus ViewStatus
        {
            get
            {
                return _ViewStatus;
            }
            set
            {
                _ViewStatus = value;

                switch (value)
                {
                    case ClsInterface.EnumViewStatus.ViewMode:
                        AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.False;
                        AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False;
                        AllowDelete = Janus.Windows.GridEX.InheritableBoolean.False;
                        break;
                    case ClsInterface.EnumViewStatus.EditMode:
                        if (LockStatus == ClsInterface.EnumLockStatus.EditAble)
                        {
                            AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.True;
                            AllowEdit = Janus.Windows.GridEX.InheritableBoolean.True;
                            AllowDelete = Janus.Windows.GridEX.InheritableBoolean.True;
                            NewRowPosition = Janus.Windows.GridEX.NewRowPosition.BottomRow;
                        }
                        break;
                    default:
                        break;
                }
            }
        }
        [Category("Observer")]
        public ClsInterface.EnumLockStatus LockStatus
        {
            get;
            set;
        }
        [Category("Observer")]
        public ClsInterface.EnumBasicComponentType BasicComponentType
        {
            get
            {
                return ClsInterface.EnumBasicComponentType.Grid;
            }

        }
        [Category("Observer")]
        public bool CopyAndPaste
        {
            get;
            set;
        }
        public bool IsRequierd { get; set; }
        public ClsInterface.ValidateType ValidateType { get; set; } = ClsInterface.ValidateType.IsNullOrEmpty;


        #endregion

        #region Data Source
        public new object DataSource
        {
            get
            {
                return base.DataSource;
            }
            set
            {
                base.DataSource = null;
                base.DataSource = value;
                if (base.Row > 0)
                {
                    this.Row = 0;
                    this.Col = 0;
                }

                if (System.IO.File.Exists(Application.StartupPath + "\\ProFile\\" + GetProfileName(this)))
                {
                    using (System.IO.FileStream tmpIO = new
                        System.IO.FileStream(Application.StartupPath + "\\ProFile\\" +
                        GetProfileName(this), System.IO.FileMode.Open))
                    {
                        LoadLayoutFile(tmpIO);
                        ViewStatus = ViewStatus;
                    }
                }
            }
        }


        #endregion


        #region  Method

        public DataTable GetColText()
        {
            DataTable table1 = new DataTable("Header");
            object[] headerList = new string[this.RootTable.Columns.Count];
            for (int i = 0; i < this.RootTable.Columns.Count; i++)
            {
                headerList[i] = this.RootTable.Columns[i].Caption;
                table1.Columns.Add(this.RootTable.Columns[i].DataMember);
                table1.Columns[i].Caption = this.RootTable.Columns[i].Caption;
            }

            table1.Rows.Add(headerList);
            return table1;
        }

        public string GetColNameVisible()
        {
            string coln = "";
            for (int i = 0; i < this.RootTable.Columns.Count; i++)
            {
                if (this.RootTable.Columns[i].Visible)
                {
                    if (i != 0)
                        coln = coln + " ,";

                    coln = coln + this.RootTable.Columns[i].DataMember.ToString();
                }
            }
            return coln;
        }

        public Dictionary<string, string> GetColDictionaryDisString()
        {

            var _dic = new Dictionary<string, string>();
            for (int i = 0; i < this.RootTable.Columns.Count; i++)
            {
                if (this.RootTable.Columns[i].Visible && (this.RootTable.Columns[i].DataTypeCode == TypeCode.String || this.RootTable.Columns[i].DataTypeCode == TypeCode.DateTime))

                    if (i != -1)
                        _dic.Add(RootTable.Columns[i].Key, RootTable.Columns[i].Caption.ToString());
            }
            return _dic;
        }

        public Dictionary<string, string> GetColDictionary(bool Comparison=false)
        {

            var _dic = new Dictionary<string, string>();
            for (int i = 0; i < this.RootTable.Columns.Count; i++)
            {
                var a = RootTable.Columns[i].Tag;
                if (this.RootTable.Columns[i].Visible && (this.RootTable.Columns[i].DataTypeCode != TypeCode.String && this.RootTable.Columns[i].DataTypeCode != TypeCode.DateTime))
                {
                    if (i != -1 && Comparison == true )
                        try
                        {
                            _dic.Add(RootTable.Columns[i].Key, RootTable.ColumnSets[RootTable.Columns[i].Key.Split('_')[1]]?.Caption + "_" + RootTable.Columns[i].Caption.ToString());

                        }
                        catch (Exception)
                        {

                            
                        }
                    else if (i != -1 && Comparison == false)
                        _dic.Add(RootTable.Columns[i].Key, RootTable.Columns[i].Caption.ToString());

                    
                }
            }
            return _dic;
        }

        public object GetCurrentRow(string fieldName)
        {
            try
            {
                if (this.RowCount == 0) return null;
                if (this.CurrentRow == null) return null;
                if (this.CurrentRow.DataRow == null) return null;

                object s = this.CurrentRow.DataRow;
                var Property = s.GetType().GetProperty(fieldName);
                if (Property == null) return null;
                return Property.GetValue(s, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        string GetProfileName(ClsGrid obj)
        {
            try
            {
                if (obj.ParentForm == null) return "";
                return obj.ParentForm.Name + "_" + obj.Name + ".txt";
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void setshowInFieldChooser(bool s)
        {
            if (s == false)
            {
                foreach (GridEXColumn itemColumn in this.RootTable.Columns)
                {
                    itemColumn.ShowInFieldChooser = false;
                }
            }
            else
            {
                foreach (GridEXColumn itemColumn in this.RootTable.Columns)
                {
                    itemColumn.ShowInFieldChooser = true;
                }
            }
        }
        public void SetVisible_Columns(bool s)
        {
            if (s == false)
            {
                foreach (GridEXColumn itemColumn in this.RootTable.Columns)
                {
                    itemColumn.Visible = false;
                }
            }
            else
            {
                foreach (GridEXColumn itemColumn in this.RootTable.Columns)
                {
                    itemColumn.Visible = true;
                }
            }
        }

        public void Selectable_Columns(bool s)
        {
            if (s == false)
            {
                foreach (GridEXColumn itemColumn in this.RootTable.Columns)
                {
                    itemColumn.Selectable = false;
                }
            }
            else
            {
                foreach (GridEXColumn itemColumn in this.RootTable.Columns)
                {
                    itemColumn.Selectable = true;
                }
            }
        }

        private void SetDefaultFormatInCustomColumns()
        {

            foreach (var item in _customGridColumns)
            {
                if (!this.RootTable.Columns.Contains(item.Key))
                    continue;

                if (string.IsNullOrEmpty(item.Key))
                    continue;

                var col = this.RootTable.Columns[item.Key];

                if (col == null)
                    continue;

                switch (item.Type)
                {
                    case CustomColumnType.DefaultDecimal:
                        col.SetPrecisionCount(_currentDecimalPrecisionNumber, _currentDecimalPrecisionNumber);
                        break;

                    case CustomColumnType.BaseCurrencyDecimal:
                        col.SetPrecisionCount(_currentDecimalPrecisionNumber, _currentDecimalPrecisionNumber);
                        break;

                    case CustomColumnType.Qty:
                        col.SetPrecisionCount(2, 2);
                        break;

                    case CustomColumnType.Percent:
                        col.SetPrecisionCount(2, 4);
                        break;


                    case CustomColumnType.N0:
                        col.SetPrecisionCount(0, 0);
                        break;


                    case CustomColumnType.DateMiladi:
                    case CustomColumnType.DateShamsi:
                    case CustomColumnType.DateHijri:
                        col.SetCustomFormatAndMask(null, "0000/00/00");
                        break;

                    case CustomColumnType.MustBeWithoutFormat:
                        col.ClearFormat(item.Selectable);
                        break;

                        //case CustomColumnType.ImageAndTxt:
                        //    col.ColumnType = ColumnType.ImageAndText;
                        //    Bitmap b = new Bitmap(@"D:\ASGARI\NovinPlus\NovinWinUI\Resources\Acc.png");
                        //    col.Image = b;
                        //    col.RightToLeft = RightToLeft.No;
                        //    col.TextAlignment = TextAlignment.Far;
                        //    col.ImageKey = "ali";
                        //    break;
                }


            }
        }

        private void FormatRow(GridEXRow row)
        {
            if (row == null || row.DataRow == null)
                return;


            var precisionCount = _currentDecimalPrecisionNumber;

            if (RootTable.Columns.Contains("CurrencyID"))
                if (row.Cells["CurrencyID"].Value != null)
                    if (_currencyList.Count > 0)
                    {
                        var tmp = _currencyList.FirstOrDefault(x => x.Id == Convert.ToInt32(row.Cells["CurrencyID"].Value));

                        if (tmp != null)
                            precisionCount = tmp.PrecisionCount;
                    }

            this.SelectedFormatStyle.ForeColor = System.Drawing.Color.Black;

            foreach (var item in _customGridColumns)
            {
                if (!this.RootTable.Columns.Contains(item.Key))
                    continue;

                var col = this.RootTable.Columns[item.Key];

                if (string.IsNullOrEmpty(item.Key))
                    continue;

                if (col == null)
                    continue;

                switch (item.Type)
                {
                    case CustomColumnType.DefaultDecimal:
                        row.Cells[item.Key].FormatPrecisionCount(_currentDecimalPrecisionNumber);
                        break;

                    case CustomColumnType.BaseCurrencyDecimal:
                        row.Cells[item.Key].FormatPrecisionCount(precisionCount);
                        break;
                    case CustomColumnType.Qty:
                        row.Cells[item.Key].FormatPrecisionCount(4);
                        break;
                    //case CustomColumnType.DateMiladi:
                    //    break;

                    case CustomColumnType.DateShamsi:
                        row.Cells[item.Key].FormatAsShamsiDate();
                        break;

                    case CustomColumnType.DateHijri:
                        row.Cells[item.Key].FormatAsHijriDate(-2);
                        break;
                }
            }

        }

        private void ClsGrid_CellEdited(object sender, ColumnActionEventArgs e)
        {
            //this.Update();
            //this.UpdateData();
            if (_customGridColumns != null)
                FormatRow(this.CurrentRow);


            this.Focus();
            this.Select();
        }

        private void ClsGrid_KeyPress(object sender, KeyPressEventArgs e)
        {

            try
            {
                if (this.CurrentColumn?.DataTypeCode == TypeCode.Decimal && (this.FilterRow == null || !this.FilterRow.Selected))
                {
                    e.Handled = true;
                    //var _mValue = "";
                    //e.Handled = true;
                    //if (char.IsDigit(e.KeyChar))
                    //{
                    //    _mValue = e.KeyChar.ToString();
                    //    form1 = new FrmInsertValue();
                    //    form1.M_Value = _mValue;
                    //    form1.M_ClsGrid = this;
                    //    form1.ShowDialog();
                    //    //this.Col += 1;//this.RootTable.Columns[this.CurrentColumn.DataMember].Index;
                    //    //this.MoveToRowIndex(this.RootTable.Columns[this.CurrentColumn.DataMember].Index);
                    //    return;
                    //}



                }
                //else
                //{
                //if (e.KeyChar == '/')
                //{
                //    var value = Convert.ToDecimal(this.GetValue(this.CurrentColumn.DataMember));
                //    this.SetValueExists(this.CurrentColumn.DataMember, '/');
                //}
                if (e.KeyChar == '-')
                {
                    if (this.CurrentColumn == null) return;
                    var value = this.GetValue(this.CurrentColumn?.DataMember);
                    if (value == null) return;

                    Type type = value.GetType();
                    if (type.Equals(typeof(long)) || type.Equals(typeof(decimal)))
                        e.Handled = true;
                }

                int per = 0;
                if (this.CurrentColumn?.DataTypeCode == TypeCode.Decimal && e.KeyChar != '\b')
                {
                    var value = this.GetValue(this.CurrentColumn.DataMember);

                    if (value != null && value.ToString().Replace(",", "").Length >= 22)
                    //if (value != null)
                    {
                        e.Handled = true;
                        return;
                    }
                }

                if (e.KeyChar == '.' && this.CurrentColumn?.DataTypeCode != TypeCode.String && this.CurrentColumn?.Key != "CurrencyID")
                {

                    if (RootTable.Columns.Contains("CurrencyID"))
                        if (this.CurrentRow.Cells["CurrencyID"].Value != null)
                            if (_currencyList?.Count > 0)
                            {
                                var tmp = _currencyList.FirstOrDefault(x => x.Id == Convert.ToInt32(this.CurrentRow.Cells["CurrencyID"].Value));

                                if (tmp != null)
                                    per = tmp.PrecisionCount;
                            }

                    if (this.CurrentColumn == null) return;
                    var value = this.GetValue(this.CurrentColumn.DataMember);
                    if (value == null) return;
                    if (value.ToString().Replace(",", "").Length > 19)

                    {
                        e.Handled = true;
                        return;
                    }
                    Decimal result;
                    var newValue = Decimal.TryParse(Convert.ToString(value), out result) ? result : 0;
                    if (newValue != 0)
                    {
                        this.SetValue(this.CurrentColumn.DataMember, newValue * 1000);

                        //this.SetValue(this.CurrentColumn.DataMember.ToString(), (GetValue(this.CurrentColumn.DataMember.ToString()).ToString() + "000"));
                        SendKeys.Send("{END}");

                        //      if (per > 0)
                        //      {
                        //          for (int i = 0; i < per + 1; i++)
                        //          {
                        //              SendKeys.Send("{BACKSPACE}");
                        //}
                        //      }
                        //      else if (per == 0 && this.CurrentColumn.DataMember.Contains("BaseCurrency"))
                        //      {
                        //          for (int i = 0; i < 5; i++)
                        //          {
                        //              SendKeys.Send("{BACKSPACE}");
                        //         }
                        //      }
                    }

                }

                //}
            }
            catch (Exception ex)
            {
                e.Handled = true;
            }

        }

        private void ClsGrid_GroupsChanged(object sender, GroupsChangedEventArgs e)
        {
            this.RootTable.GroupTotals = Janus.Windows.GridEX.GroupTotals.Always;
        }

        private void ClsGrid_UpdatingCell(object sender, UpdatingCellEventArgs e)
        {
            M_Edit = true;
        }

        public void SetAndApply(IList<CustomGridColumn> columnsList, IList<CustomCurrencyModel> currencyList, int currentDefaultPrecisionNumber)
        {
            if (columnsList == null)
                return;

            if (currencyList == null)
                return;

            _customGridColumns = columnsList;

            _currencyList = currencyList;

            _currentDecimalPrecisionNumber = currentDefaultPrecisionNumber;

            SetDefaultFormatInCustomColumns();

        }

        private void ClsGrid_CurrentCellChanged(object sender, EventArgs e)
        {
            //if (( this.CurrentColumn?.DataTypeCode == TypeCode.Decimal && this.CurrentRow.Cells[this.CurrentColumn.DataMember].Column.Selectable == true && this.CurrentRow.Cells[this.CurrentColumn.DataMember].Column.EditType != EditType.NoEdit))
            //{

            //    long _mValue = 0;
            //    if (this.GetValue(this.CurrentColumn.DataMember) != null && this.GetValue(this.CurrentColumn.DataMember) != "")
            //        _mValue = Convert.ToInt64(this.GetValue(this.CurrentColumn.DataMember));

            //    //e.Handled = true;
            //    form1 = new FrmInsertValue();
            //    form1.M_Value = _mValue.ToString();
            //    form1.M_ClsGrid = this;
            //    form1.ShowDialog();
            //    this.Col += 1;

            //    return;
            //}

            isChanged = true;
            if (this.CurrentColumn != null)
            {
                if (this.CurrentColumn.DataMember == "Comments")
                {
                    if (Convert.ToString(this.GetValueExists("Comments")) == "")
                    {
                        SendKeys.Send(" ");
                        SendKeys.Send("{BACKSPACE}");
                    }
                    else
                    {
                        this.SelectCurrentCellText();
                    }
                }
                else if (this.CurrentColumn.DataMember == "Description")
                {
                    if (Convert.ToString(this.GetValueExists("Description")) == "")
                    {
                        SendKeys.Send(" ");
                        SendKeys.Send("{BACKSPACE}");
                    }
                    else
                    {
                        this.SelectCurrentCellText();
                    }
                }
            }

        }

        private void ClsGrid_DoubleClick(object sender, EventArgs e)
        {
            if(this.CurrentRow != null &&  this.Name.Contains("Header"))
            SendKeys.Send("{F2}");
        }

        private void ClsGrid_Click(object sender, EventArgs e)
        {
            if (Col == 8)
            {

            }
        }

        private void ClsGrid_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //SendKeys..Send("F2");
        }

        private void ClsGrid_CellUpdated(object sender, ColumnActionEventArgs e)
        {
            this.Update();
            this.UpdateData();
        }

        private void ClsGrid_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void ClsGrid_CellValueChanged(object sender, ColumnActionEventArgs e)
        {
            if (this.CurrentColumn.EditType == EditType.DropDownList)
            {
                isChanged = false;
            }
        }

        private void ClsGrid_ColumnHeaderClick(object sender, ColumnActionEventArgs e)
        {

        }

        private void ClsGrid_SizingColumnSet(object sender, SizingColumnSetEventArgs e)
        {

        }

        public void SetInputMask(int PrecisionCount)
        {
            if (this.CurrentRow != null)
            {

                if (this.RootTable.Columns.Contains("FeeBaseCurrency"))
                    this.CurrentRow?.Cells["FeeBaseCurrency"].SetPrecisionCountCells(PrecisionCount, PrecisionCount);
                if (this.RootTable.Columns.Contains("Rate"))
                    this.CurrentRow?.Cells["Rate"].SetPrecisionCountCells(PrecisionCount, PrecisionCount);
                if (this.RootTable.Columns.Contains("Fee"))
                    this.CurrentRow?.Cells["Fee"].SetPrecisionCountCells(PrecisionCount, PrecisionCount);
                if (this.RootTable.Columns.Contains("AmountBaseCurrency"))
                    this.CurrentRow?.Cells["AmountBaseCurrency"].SetPrecisionCountCells(PrecisionCount, PrecisionCount);
                if (this.RootTable.Columns.Contains("Amount"))
                    this.CurrentRow?.Cells["Amount"].SetPrecisionCountCells(PrecisionCount, PrecisionCount);

                if (this.RootTable.Columns.Contains("DebitBaseCurrency"))
                    this.CurrentRow?.Cells["DebitBaseCurrency"].SetPrecisionCountCells(PrecisionCount, PrecisionCount);

                if (this.RootTable.Columns.Contains("CreateBaseCurrency"))
                    this.CurrentRow?.Cells["CreateBaseCurrency"].SetPrecisionCountCells(PrecisionCount, PrecisionCount);

                if (this.RootTable.Columns.Contains("Debit"))
                    this.CurrentRow?.Cells["Debit"].SetPrecisionCountCells(PrecisionCount, PrecisionCount);

                if (this.RootTable.Columns.Contains("Create"))
                    this.CurrentRow?.Cells["Create"].SetPrecisionCountCells(PrecisionCount, PrecisionCount);
            }
        }

        #endregion
    }
}
