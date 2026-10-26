using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace BasicComponent
{
    public partial class CheckedComboBox : ComboBox
    {
        public readonly CheckedListBox _checkedListBox;
        public readonly ToolStripDropDown _dropDown;
        private string _displayMember;
        private string _valueMember;
        private IEnumerable _dataSource;

        public CheckedComboBox()
        {
            this.DropDownStyle = ComboBoxStyle.DropDownList;
            this.DrawMode = DrawMode.OwnerDrawFixed;

            _checkedListBox = new CheckedListBox
            {
                BorderStyle = BorderStyle.None,
                CheckOnClick = true,
                BackColor = Color.White
            };

            // وقتی وضعیت چک تغییر کرد، بعد از اعمال، متن را بروزرسانی کن
            _checkedListBox.ItemCheck += (s, e) =>
            {
                this.BeginInvoke((Action)(() => UpdateText()));
            };

            // ساخت DropDown
            _dropDown = new ToolStripDropDown { Padding = Padding.Empty };
            var host = new ToolStripControlHost(_checkedListBox)
            {
                Margin = Padding.Empty,
                Padding = Padding.Empty,
                AutoSize = false
            };
            _dropDown.Items.Add(host);
        }

        // پشتیبانی از DataSource
        public new IEnumerable DataSource
        {
            get => _dataSource;
            set
            {
                _dataSource = value;
                _checkedListBox.Items.Clear();
                if (_dataSource == null) return;

                foreach (var item in _dataSource)
                {
                    string text = _displayMember != null ? GetPropertyValue(item, _displayMember)?.ToString() : item.ToString();
                    object val = _valueMember != null ? GetPropertyValue(item, _valueMember) : item;
                    _checkedListBox.Items.Add(new ComboItem { Text = text, Value = val });
                }
            }
        }

        public new string DisplayMember
        {
            get => _displayMember;
            set => _displayMember = value;
        }

        public new string ValueMember
        {
            get => _valueMember;
            set => _valueMember = value;
        }

        protected override void OnDropDown(EventArgs e)
        {
            // اندازه DropDown برابر عرض کنترل
            _checkedListBox.Width = this.Width;
            _checkedListBox.Height = 150;

            // موقعیت درست زیر ComboBox
            var location = this.PointToScreen(new Point(0, this.Height));
            _dropDown.Show(location);
        }

        private void UpdateText()
        {
            var selected = _checkedListBox.CheckedItems.Cast<ComboItem>().Select(i => i.Text);
            this.Text = string.Join(", ", selected);
            this.Refresh();
        }

        private object GetPropertyValue(object obj, string propName)
        {
            var prop = obj.GetType().GetProperty(propName);
            return prop?.GetValue(obj);
        }

        public List<object> SelectedValues =>
            _checkedListBox.CheckedItems.Cast<ComboItem>().Select(i => i.Value).ToList();

        public List<string> SelectedTexts =>
            _checkedListBox.CheckedItems.Cast<ComboItem>().Select(i => i.Text).ToList();

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            e.DrawBackground();
            if (e.Index >= 0 && e.Index < Items.Count)
            {
                string text = Items[e.Index].ToString();
                using (Brush brush = new SolidBrush(e.ForeColor))
                {
                    e.Graphics.DrawString(text, e.Font, brush, e.Bounds);
                }
            }
            e.DrawFocusRectangle();
        }

        private class ComboItem
        {
            public string Text { get; set; }
            public object Value { get; set; }
            public override string ToString() => Text;
        }

        private void CheckedComboBox_DropDownClosed(object sender, EventArgs e)
        {
            this.BeginInvoke((Action)(() => UpdateText()));
        }
    }
}
