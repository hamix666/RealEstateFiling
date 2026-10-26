using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;


namespace BasicComponent
{
    public partial class ClsMaskedTextBox : DevComponents.DotNetBar.Controls.MaskedTextBoxAdv, ClsInterface.IBasicComponent
    {
        #region Property & Variable     
        public bool IsShamsi
        {
            get;
            set;
        }
        public bool IsTime { get; set; }

        Point ppp;
        #endregion 

        #region Constructor
        public ClsMaskedTextBox()
        {
            InitializeComponent();

            ButtonClear.Visible = false;
            ButtonCustom.Visible = true;

        }

        public ClsMaskedTextBox(IContainer container)
        {
            InitializeComponent();
            this.IsShamsi = false;
            container.Add(this);
            ButtonClear.Visible = false;
            ButtonCustom.Visible = true;
            ButtonCustom2.Visible = false;
            ButtonDropDown.Visible = false;
        }

        #endregion

        #region IBasicComponent Members
        public ClsInterface.EnumBasicComponentType BasicComponentType
        {
            get
            {
                return ClsInterface.EnumBasicComponentType.MaskTextBox;
            }
        }

        public string BLObjectName
        {
            get;
            set;
        }

        public string FieldName
        {
            get;
            set;
        }

        public string GetMethodName
        {
            get;
            set;
        }

        public ClsInterface.EnumLockStatus LockStatus
        {
            get;
            set;
        } = ClsInterface.EnumLockStatus.EditAble;

        ClsInterface.EnumViewStatus _ViewStatus;
        public ClsInterface.EnumViewStatus ViewStatus
        {
            get
            {
                return _ViewStatus;
            }
            set
            {
                _ViewStatus = value;
                if (value == ClsInterface.EnumViewStatus.ViewMode)
                    Enabled = false;
                else
                    Enabled = (LockStatus == ClsInterface.EnumLockStatus.EditAble) ? true : false;
            }
        }
        public void ClearValue()
        {
            Text = "";
            this.Focus();
        }

        public bool IsRequierd { get; set; }
        public ClsInterface.ValidateType ValidateType { get; set; } = ClsInterface.ValidateType.IsNullOrEmpty;


        #endregion

        #region Method

        private void ClsMaskedTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{Tab}");
            }
            else if (e.Modifiers == Keys.Control && e.KeyCode == Keys.T)
            {
                
                    this.Text = DateTime.Now.ToString("yyyy/MM/dd").ToShamsiDate();

                

                this.Select();
            }
        }

        private void ClsMaskedTextBox_TypeValidationCompleted(object sender, TypeValidationEventArgs e)
        {
            if (this.MaskCompleted == false || this.Text == null)
            {
                if (!e.IsValidInput)
                    this.Focus();
                return;
            }

            if (!this.IsTime)
            {
                if (this.Text.Replace("/", "").Replace("_", "") == "") return;
                if (Convert.ToInt32(this.Text.Substring(5, 2)) <= 6 && (this.Text.Substring(8) == "29" || this.Text.Substring(8) == "30" || this.Text.Substring(8) == "31")) return;
                if (Convert.ToInt32(this.Text.Substring(5, 2)) >= 6 && (this.Text.Substring(8) == "31"))
                {
                    this.Focus();
                    return;
                }
            }
            else
            {
                if (this.Text.Replace(":", "").Replace("_", "") == "") return;

                if ((Convert.ToInt32(this.Text.Substring(0, 2)) > 23 && Convert.ToInt32(this.Text.Substring(0, 2)) < 0) ||
                    (Convert.ToInt32(this.Text.Substring(3, 2)) > 59 && Convert.ToInt32(this.Text.Substring(3, 2)) < 0))
                {
                    this.Focus();
                    return;
                }
            }
            if (!e.IsValidInput)
                this.Focus();
            //e.Cancel = true;      
        }

        private void ClsMaskedTextBox_ButtonClearClick(object sender, CancelEventArgs e)
        {
            Text = IsShamsi ? DateTime.Now.ToShamsiDate() : $@"{DateTime.Now:yyyy/MM/dd}";
        }
        public string ConvertToPersianDate()
        {
            DateTime input = DateTime.Now;// Convert.ToDateTime(this.Text);
            {
                PersianCalendar cal = new PersianCalendar();
                int month = cal.GetMonth(input);
                int Day = cal.GetDayOfMonth(input);

                var str =
                    string.Format("{0}/{1}/{2}", cal.GetYear(input)
                        , month < 10 ? "0" + month.ToString() : month.ToString()
                        , Day < 10 ? "0" + Day.ToString() : Day.ToString());
                return str;
            }
        }

        static DateTime ConvertToMiladiDate(string strdate)
        {
            //if (this.Text.Contains(" ")) return false;
            //if (this.Text == "____/__/__") return false;
            strdate = strdate.Replace(" ", "");
            string[] strList = strdate.Split('/');
            //if (strList[0] == "") return DateTime.Now;
            DateTime str = new DateTime();
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
                    int pDay = Convert.ToInt32(strList[0]);

                    System.Globalization.PersianCalendar pc = new System.Globalization.PersianCalendar();
                    str = pc.ToDateTime(PYear, PMonth, pDay, 1, 1, 1, 1, GregorianCalendar.ADEra);
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
                        catch
                        {
                        }
                    }
                }
            }
            return str;
        }
        public bool ValidationDate(DateTime sdate, DateTime edate)
        {

            DateTime datenew = new DateTime();

            if (this.Text == "____/__/__") return false;
            if (this.Text.Contains(" ")) return false;

            var a = IsShamsi;
            
                datenew = ConvertToMiladiDate(this.Text);
            


            if (DateTime.Compare(datenew.Date, sdate.Date) < 0)
            {
                return false;
            }

            if (DateTime.Compare(datenew.Date, edate.Date) > 0)
            {
                return false;
            }

            return true;
        }
        public bool ExistDate(DateTime sdate, DateTime edate)
        {
            DateTime datenew = new DateTime();
            
                datenew = ConvertToMiladiDate(this.Text);
           
            if ((datenew >= sdate) && (datenew <= edate.AddDays(1)))
            {
                return true;
            }
            return false;
        }
        private void ClsMaskedTextBox_ButtonCustomClick(object sender, EventArgs e)
        {
            
                Text = DateTime.Now.ToShamsiDate();
            
        }

        private void ClsMaskedTextBox_ButtonCustom2Click(object sender, EventArgs e)
        {
            var textBox = (BasicComponent.ClsMaskedTextBox)sender;

            
                var dateTime = textBox.Text;
                if (dateTime == "____/__/__")
                    dateTime = PersianDateTime.Now.ToString("yyyy/MM/dd");
                // var pp = new Point(this.Location.X-160, this.Location.Y+140);

                var p1 = this.PointToScreen(this.Location);
                var p2 = this.PointToScreen(new Point(0, 0));
                var p3 = new Point(p1.X - p2.X, p1.Y - p2.Y);

                if (dateTime.ToMiladiDate() < ("03/22/0622".ToString()).ToMiladiDate())
                    return;

                var form = new BPersianCalender.PCalander(dateTime.ToMiladiDate(), ppp, this.Size);
                form.StartPosition = FormStartPosition.CenterScreen;
                if (form.ShowDialog() == DialogResult.OK)
                {
                    textBox.Invoke((MethodInvoker)delegate
                    {
                        textBox.Text = form.objshamsiCalander.ShamsiDate;
                    });
                }
                //var form = BasicComponent.CustomForms.PersianDateTimePicker.Show(dateTime, ppp,this.Size);
                //if (form.Item1 == DialogResult.OK)
                //{
                //    textBox.Invoke((MethodInvoker)delegate
                //    {
                //        textBox.Text = form.Item2.Substring(0, 19);
                //    });
                //}
            
        }
        #endregion

        private void ClsMaskedTextBox_MouseLeave(object sender, EventArgs e)
        {



        }

        private void ClsMaskedTextBox_MouseDown(object sender, MouseEventArgs e)
        {

            ppp = new Point(e.X, e.Y); ppp = PointToScreen(ppp);
        }

        private void ClsMaskedTextBox_Click(object sender, EventArgs e)
        {

        }

        private void ClsMaskedTextBox_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void ClsMaskedTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void ClsMaskedTextBox_Enter(object sender, EventArgs e)
        {

        }
    }
}
