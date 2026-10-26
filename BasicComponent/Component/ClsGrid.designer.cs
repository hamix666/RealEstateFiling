namespace BasicComponent
{
    partial class ClsGrid
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClsGrid));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip();
            this.ItemChoose = new System.Windows.Forms.ToolStripMenuItem();
            this.ItemdelColumn = new System.Windows.Forms.ToolStripMenuItem();
            this.ItemWordwrap = new System.Windows.Forms.ToolStripMenuItem();
            this.ItemSaveLayout = new System.Windows.Forms.ToolStripMenuItem();
            this.ItemDelLayout = new System.Windows.Forms.ToolStripMenuItem();
            this.ItemShowGroupSum = new System.Windows.Forms.ToolStripMenuItem();
            this.ItemShowTotalSum = new System.Windows.Forms.ToolStripMenuItem();
            this.ItemFunc = new System.Windows.Forms.ToolStripMenuItem();
            this.ItemSum = new System.Windows.Forms.ToolStripMenuItem();
            this.ItemAVG = new System.Windows.Forms.ToolStripMenuItem();
            this.ItemMin = new System.Windows.Forms.ToolStripMenuItem();
            this.ItemMax = new System.Windows.Forms.ToolStripMenuItem();
            this.ItemCount = new System.Windows.Forms.ToolStripMenuItem();
            this.ItemCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.ItemPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.RecordInsert = new System.Windows.Forms.ToolStripMenuItem();
            this.ItemRTL = new System.Windows.Forms.ToolStripMenuItem();
            this.ItemGroupBy = new System.Windows.Forms.ToolStripMenuItem();
            this.ItemFilter = new System.Windows.Forms.ToolStripMenuItem();
            this.ItemRowNo = new System.Windows.Forms.ToolStripMenuItem();
            this.ItemRowNoIncreaseWidth = new System.Windows.Forms.ToolStripMenuItem();
            this.ItemRowNoDecreaseWidth = new System.Windows.Forms.ToolStripMenuItem();
            this.ItemExportToExcel = new System.Windows.Forms.ToolStripMenuItem();
            this.ItemAutoSizeColumn = new System.Windows.Forms.ToolStripMenuItem();
            this.Remove_Sort = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ItemChoose,
            this.ItemdelColumn,
            this.ItemWordwrap,
            this.ItemSaveLayout,
            this.ItemDelLayout,
            this.ItemShowGroupSum,
            this.ItemShowTotalSum,
            this.ItemFunc,
            this.ItemCopy,
            this.ItemPaste,
            this.RecordInsert,
            this.ItemRTL,
            this.ItemGroupBy,
            this.ItemFilter,
            this.ItemRowNo,
            this.ItemRowNoIncreaseWidth,
            this.ItemRowNoDecreaseWidth,
            this.ItemExportToExcel,
            this.ItemAutoSizeColumn,
            this.Remove_Sort});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.contextMenuStrip1.Size = new System.Drawing.Size(204, 444);
            // 
            // ItemChoose
            // 
            this.ItemChoose.Name = "ItemChoose";
            this.ItemChoose.Size = new System.Drawing.Size(203, 22);
            this.ItemChoose.Text = "انتخاب فیلد";
            this.ItemChoose.Click += new System.EventHandler(this.ItemChoose_Click);
            // 
            // ItemdelColumn
            // 
            this.ItemdelColumn.Name = "ItemdelColumn";
            this.ItemdelColumn.Size = new System.Drawing.Size(203, 22);
            this.ItemdelColumn.Text = "حذف این ستون";
            this.ItemdelColumn.Click += new System.EventHandler(this.ItemdelColumn_Click);
            // 
            // ItemWordwrap
            // 
            this.ItemWordwrap.Name = "ItemWordwrap";
            this.ItemWordwrap.Size = new System.Drawing.Size(203, 22);
            this.ItemWordwrap.Text = "چند خطی کردن این ستون";
            this.ItemWordwrap.Click += new System.EventHandler(this.ItemWordwrap_Click);
            // 
            // ItemSaveLayout
            // 
            this.ItemSaveLayout.Name = "ItemSaveLayout";
            this.ItemSaveLayout.Size = new System.Drawing.Size(203, 22);
            this.ItemSaveLayout.Text = "ذخیره به عنوان پیش فرض";
            this.ItemSaveLayout.Click += new System.EventHandler(this.ItemSaveLayout_Click);
            // 
            // ItemDelLayout
            // 
            this.ItemDelLayout.Name = "ItemDelLayout";
            this.ItemDelLayout.Size = new System.Drawing.Size(203, 22);
            this.ItemDelLayout.Text = "حذف پیش فرض ها";
            this.ItemDelLayout.Click += new System.EventHandler(this.ItemDelLayout_Click);
            // 
            // ItemShowGroupSum
            // 
            this.ItemShowGroupSum.Name = "ItemShowGroupSum";
            this.ItemShowGroupSum.Size = new System.Drawing.Size(203, 22);
            this.ItemShowGroupSum.Text = "نمایش مجموع گروه";
            this.ItemShowGroupSum.Click += new System.EventHandler(this.ItemShowGroupSum_Click);
            // 
            // ItemShowTotalSum
            // 
            this.ItemShowTotalSum.Name = "ItemShowTotalSum";
            this.ItemShowTotalSum.Size = new System.Drawing.Size(203, 22);
            this.ItemShowTotalSum.Text = "نمایش مجموع کلی";
            this.ItemShowTotalSum.Click += new System.EventHandler(this.ItemShowTotalSum_Click);
            // 
            // ItemFunc
            // 
            this.ItemFunc.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ItemSum,
            this.ItemAVG,
            this.ItemMin,
            this.ItemMax,
            this.ItemCount});
            this.ItemFunc.Name = "ItemFunc";
            this.ItemFunc.Size = new System.Drawing.Size(203, 22);
            this.ItemFunc.Text = "توابع";
            // 
            // ItemSum
            // 
            this.ItemSum.Name = "ItemSum";
            this.ItemSum.Size = new System.Drawing.Size(114, 22);
            this.ItemSum.Text = "جمع";
            this.ItemSum.Click += new System.EventHandler(this.ItemSum_Click);
            // 
            // ItemAVG
            // 
            this.ItemAVG.Name = "ItemAVG";
            this.ItemAVG.Size = new System.Drawing.Size(114, 22);
            this.ItemAVG.Text = "میانگین";
            this.ItemAVG.Click += new System.EventHandler(this.ItemAVG_Click);
            // 
            // ItemMin
            // 
            this.ItemMin.Name = "ItemMin";
            this.ItemMin.Size = new System.Drawing.Size(114, 22);
            this.ItemMin.Text = "کمترین";
            this.ItemMin.Click += new System.EventHandler(this.ItemMin_Click);
            // 
            // ItemMax
            // 
            this.ItemMax.Name = "ItemMax";
            this.ItemMax.Size = new System.Drawing.Size(114, 22);
            this.ItemMax.Text = "بیشترین";
            this.ItemMax.Click += new System.EventHandler(this.ItemMax_Click);
            // 
            // ItemCount
            // 
            this.ItemCount.Name = "ItemCount";
            this.ItemCount.Size = new System.Drawing.Size(114, 22);
            this.ItemCount.Text = "تعداد";
            this.ItemCount.Click += new System.EventHandler(this.ItemCount_Click);
            // 
            // ItemCopy
            // 
            this.ItemCopy.Enabled = false;
            this.ItemCopy.Name = "ItemCopy";
            this.ItemCopy.Size = new System.Drawing.Size(203, 22);
            this.ItemCopy.Text = "کپی";
            this.ItemCopy.Click += new System.EventHandler(this.ItemCopy_Click);
            // 
            // ItemPaste
            // 
            this.ItemPaste.Enabled = false;
            this.ItemPaste.Name = "ItemPaste";
            this.ItemPaste.Size = new System.Drawing.Size(203, 22);
            this.ItemPaste.Text = "چسباندن";
            this.ItemPaste.Click += new System.EventHandler(this.ItemPaste_Click);
            // 
            // RecordInsert
            // 
            this.RecordInsert.Name = "RecordInsert";
            this.RecordInsert.Size = new System.Drawing.Size(203, 22);
            this.RecordInsert.Text = "ردیف خالی";
            // 
            // ItemRTL
            // 
            this.ItemRTL.Name = "ItemRTL";
            this.ItemRTL.Size = new System.Drawing.Size(203, 22);
            this.ItemRTL.Text = "چپ به راست/راست به چپ";
            this.ItemRTL.Click += new System.EventHandler(this.ItemRTL_Click);
            // 
            // ItemGroupBy
            // 
            this.ItemGroupBy.Name = "ItemGroupBy";
            this.ItemGroupBy.Size = new System.Drawing.Size(203, 22);
            this.ItemGroupBy.Text = "گروه بندی";
            this.ItemGroupBy.Click += new System.EventHandler(this.ItemGroupBy_Click);
            // 
            // ItemFilter
            // 
            this.ItemFilter.Name = "ItemFilter";
            this.ItemFilter.Size = new System.Drawing.Size(203, 22);
            this.ItemFilter.Text = "فیلتر";
            this.ItemFilter.Click += new System.EventHandler(this.ItemFilter_Click);
            // 
            // ItemRowNo
            // 
            this.ItemRowNo.Name = "ItemRowNo";
            this.ItemRowNo.Size = new System.Drawing.Size(203, 22);
            this.ItemRowNo.Text = "ردیف";
            this.ItemRowNo.Click += new System.EventHandler(this.ItemRowNo_Click);
            // 
            // ItemRowNoIncreaseWidth
            // 
            this.ItemRowNoIncreaseWidth.Name = "ItemRowNoIncreaseWidth";
            this.ItemRowNoIncreaseWidth.Size = new System.Drawing.Size(203, 22);
            this.ItemRowNoIncreaseWidth.Text = "افزایش سایز ردیف + ";
            this.ItemRowNoIncreaseWidth.Click += new System.EventHandler(this.ItemRowNoIncreaseWidth_Click);
            // 
            // ItemRowNoDecreaseWidth
            // 
            this.ItemRowNoDecreaseWidth.Name = "ItemRowNoDecreaseWidth";
            this.ItemRowNoDecreaseWidth.Size = new System.Drawing.Size(203, 22);
            this.ItemRowNoDecreaseWidth.Text = "کاهش سایز ردیف - ";
            this.ItemRowNoDecreaseWidth.Click += new System.EventHandler(this.ItemRowNoDecreaseWidth_Click);
            // 
            // ItemExportToExcel
            // 
            this.ItemExportToExcel.Name = "ItemExportToExcel";
            this.ItemExportToExcel.Size = new System.Drawing.Size(203, 22);
            this.ItemExportToExcel.Text = "خروجی به اکسل";
            this.ItemExportToExcel.Click += new System.EventHandler(this.ItemExportToExcel_Click);
            // 
            // ItemAutoSizeColumn
            // 
            this.ItemAutoSizeColumn.Name = "ItemAutoSizeColumn";
            this.ItemAutoSizeColumn.Size = new System.Drawing.Size(203, 22);
            this.ItemAutoSizeColumn.Text = "اندازه شدن خودکار";
            this.ItemAutoSizeColumn.Click += new System.EventHandler(this.ItemAutoSizeColumn_Click);
            // 
            // Remove_Sort
            // 
            this.Remove_Sort.Name = "Remove_Sort";
            this.Remove_Sort.Size = new System.Drawing.Size(203, 22);
            this.Remove_Sort.Text = "حذف مرتب سازی";
            this.Remove_Sort.Click += new System.EventHandler(this.Remove_Sort_Click);
            // 
            // ClsGrid
            // 
            this.AlternatingColors = true;
            this.BuiltInTextsData = resources.GetString("$this.BuiltInTextsData");
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.DefaultFilterRowComparison = Janus.Windows.GridEX.FilterConditionOperator.Contains;
            this.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown;
            this.FocusCellFormatStyle.BackColor = System.Drawing.Color.LemonChiffon;
            this.Font = new System.Drawing.Font("Vazirmatn", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.SelectedFormatStyle.BackColor = System.Drawing.Color.LemonChiffon;
            this.SelectedFormatStyle.ForeColor = System.Drawing.Color.Black;
            this.UpdateMode = Janus.Windows.GridEX.UpdateMode.CellUpdate;
            this.CellValueChanged += new Janus.Windows.GridEX.ColumnActionEventHandler(this.ClsGrid_CellValueChanged);
            this.FormattingRow += new Janus.Windows.GridEX.RowLoadEventHandler(this.ClsGrid_FormattingRow);
            this.CellEdited += new Janus.Windows.GridEX.ColumnActionEventHandler(this.ClsGrid_CellEdited);
            this.CellUpdated += new Janus.Windows.GridEX.ColumnActionEventHandler(this.ClsGrid_CellUpdated);
            this.UpdatingCell += new Janus.Windows.GridEX.UpdatingCellEventHandler(this.ClsGrid_UpdatingCell);
            this.GroupsChanged += new Janus.Windows.GridEX.GroupsChangedEventHandler(this.ClsGrid_GroupsChanged);
            this.SizingColumnSet += new Janus.Windows.GridEX.SizingColumnSetEventHandler(this.ClsGrid_SizingColumnSet);
            this.ColumnHeaderClick += new Janus.Windows.GridEX.ColumnActionEventHandler(this.ClsGrid_ColumnHeaderClick);
            this.CurrentCellChanged += new System.EventHandler(this.ClsGrid_CurrentCellChanged);
            this.Click += new System.EventHandler(this.ClsGrid_Click);
            this.DoubleClick += new System.EventHandler(this.ClsGrid_DoubleClick);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ClsGrid_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ClsGrid_KeyPress);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.ClsGrid_KeyUp);
            this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ClsGrid_MouseDoubleClick);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ClsGrid_MouseUp);
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ToolStripMenuItem ItemChoose;
        private System.Windows.Forms.ToolStripMenuItem ItemdelColumn;
        private System.Windows.Forms.ToolStripMenuItem ItemWordwrap;
        private System.Windows.Forms.ToolStripMenuItem ItemSaveLayout;
        private System.Windows.Forms.ToolStripMenuItem ItemDelLayout;
        private System.Windows.Forms.ToolStripMenuItem ItemShowGroupSum;
        private System.Windows.Forms.ToolStripMenuItem ItemShowTotalSum;
        private System.Windows.Forms.ToolStripMenuItem ItemFunc;
       

        private System.Windows.Forms.ToolStripMenuItem ItemSum;
        private System.Windows.Forms.ToolStripMenuItem ItemAVG;
        private System.Windows.Forms.ToolStripMenuItem ItemMin;
        private System.Windows.Forms.ToolStripMenuItem ItemMax;

       
        private System.Windows.Forms.ToolStripMenuItem ItemCopy;

        private System.Windows.Forms.ToolStripMenuItem ItemCount;
        private System.Windows.Forms.ToolStripMenuItem ItemRTL;
        private System.Windows.Forms.ToolStripMenuItem ItemGroupBy;
        private System.Windows.Forms.ToolStripMenuItem ItemFilter;
        private System.Windows.Forms.ToolStripMenuItem ItemRowNo;
        private System.Windows.Forms.ToolStripMenuItem ItemRowNoDecreaseWidth;
        private System.Windows.Forms.ToolStripMenuItem ItemRowNoIncreaseWidth;
        private System.Windows.Forms.ToolStripMenuItem ItemExportToExcel;
        private System.Windows.Forms.ToolStripMenuItem ItemAutoSizeColumn;
        public System.Windows.Forms.ToolStripMenuItem ItemPaste;
        public System.Windows.Forms.ToolStripMenuItem RecordInsert;
        public System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem Remove_Sort;
    }
}
