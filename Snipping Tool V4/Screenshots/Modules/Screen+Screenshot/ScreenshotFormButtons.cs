using Snipping_Tool_V4.Modules;
using Snipping_Tool_V4.Screenshots.Modules.Drawing;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace Snipping_Tool_V4.Screenshots.Modules.Screen_Screenshot
{
    public class ToolButton : PictureBox
    {
        public Tool Tool { get; }
        public bool IsChecked { get; set; }
        private readonly int SizeOffset = 3;
        private static Pen SelectedOutlineStroke { get; } = PenCache.GetPen(Color.BlueViolet, 1);
        private static Brush SelectedFill { get; } = new SolidBrush(Color.FromArgb(30, Color.Blue));
        private static Pen Stroke { get; } = PenCache.GetPen(Color.DeepSkyBlue,2);
        private static Brush Fill { get; set; } = new SolidBrush(Color.FromArgb(50, Color.DeepSkyBlue));
        public ToolButton(Tool tool, Size size)
        {
            Tool = tool;
            this.BackColor = Color.Transparent;
            this.Text = "";
            this.Tag = tool;
            this.AutoSize = false;
            this.ClientSize = size;
            this.IsChecked = false;
            this.DoubleBuffered = true;

            this.MouseClick += (s, e) =>
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    this.IsChecked = true;

                    foreach (var control in this.Parent.Controls)
                    {
                        if (control is ToolButton button && button != this)
                        {
                            button.IsChecked = false;
                            button.Invalidate();
                        }
                    }
                    this.Invalidate();
                }
            };
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            if (this.IsChecked)
            {
                //Draw the selected outline
                var bounds = new Rectangle(0,0, this.Size.Width - SizeOffset, this.Size.Width - SizeOffset);
                e.Graphics.FillRectangle(SelectedFill, bounds);
                e.Graphics.DrawRectangle(SelectedOutlineStroke, bounds);
            }
            else
            {
                this.BackColor = Color.Transparent;
            }

            var rect = new Rectangle(0, 0, this.Size.Width - SizeOffset, this.Size.Width - SizeOffset);

            // TODO: For now put fill to null, later we will create a logic to either fill or draw the icon
            Fill = null;
            this.Tool.DrawToolIcon(e.Graphics, Stroke, Fill, rect);
        }
    }
}
