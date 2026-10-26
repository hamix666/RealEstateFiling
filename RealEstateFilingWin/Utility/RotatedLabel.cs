using System;
using System.Drawing;
using System.Windows.Forms;

public class RotatedLabel : Control
{
    public float Angle { get; set; } = -45; // زاویه چرخش متن
    public RotatedLabel()
    {
        this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
        this.BackColor = Color.Transparent;
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
        using (Brush brush = new SolidBrush(this.ForeColor))
        {
            var center = new PointF(this.Width / 2f, this.Height / 2f);
            e.Graphics.TranslateTransform(center.X, center.Y);
            e.Graphics.RotateTransform(Angle);
            e.Graphics.DrawString(this.Text, this.Font, brush, -center.X, -center.Y);
            e.Graphics.ResetTransform();
        }
    }
}
