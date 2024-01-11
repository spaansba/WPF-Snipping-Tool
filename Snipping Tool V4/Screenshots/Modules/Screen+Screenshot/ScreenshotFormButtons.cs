using Snipping_Tool_V4.Modules;
using Snipping_Tool_V4.Screenshots.Modules.Drawing;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snipping_Tool_V4.Screenshots.Modules.Screen_Screenshot
{
    public class ToolButton : RadioButton
    {
        public Tool Tool { get; }
        private static Pen SelectedStroke { get; } = PenCache.GetPen(Color.Blue, 1);
        private static Brush SelectedFill { get; } = new SolidBrush(Color.FromArgb(50, Color.Blue));
        private static Pen Stroke { get; } = PenCache.GetPen(Color.DeepSkyBlue, 1);
        private static Brush Fill { get; set; } = new SolidBrush(Color.FromArgb(50, Color.DeepSkyBlue));
        public ToolButton(Tool tool, Size size)
        {
            Tool = tool;
            this.BackColor = Color.Transparent;
            this.Appearance = Appearance.Button;
            this.Text = "";
            this.AutoSize = false;
            this.ClientSize = size;
        }

        protected override void OnPaint(PaintEventArgs e)
        {

            if (this.Checked)
            {
                var bounds = new Rectangle(Point.Empty, this.Size);
                e.Graphics.FillRectangle(SelectedFill, bounds);
                e.Graphics.DrawRectangle(SelectedStroke, bounds);
            }

            var rect = new Rectangle(Point.Empty, this.Size);

            // TODO: For now put fill to null, later we will create a logic to either fill or draw the icon
            Fill = null;
            this.Tool.DrawToolIcon(e.Graphics, Stroke, Fill, rect);
        }
    }
}
