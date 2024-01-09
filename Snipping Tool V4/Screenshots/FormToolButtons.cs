using Snipping_Tool_V4.Modules;
using Snipping_Tool_V4.Screenshots.Modules.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snipping_Tool_V4.Screenshots
{
    public class ToolButton : RadioButton
    {
        public Tool tool { get; set; }
        private static Pen selectedStroke { get; } = PenCache.GetPen(Color.Blue, 1);
        private static Brush selectedFill { get; } = new SolidBrush(Color.CornflowerBlue);
        private static Pen stroke { get; } = PenCache.GetPen(Color.DeepSkyBlue, 1);
        private static Brush fill { get; } = new SolidBrush(Color.LightSkyBlue);

        public ToolButton(Tool tool)
        {
            this.tool = tool;
        }
    
        protected override void OnPaint(PaintEventArgs e)
        {
            if (this.Checked)
            {
                var bounds = new Rectangle(Point.Empty, this.Size);
                e.Graphics.FillRectangle(selectedFill, bounds);
                e.Graphics.DrawRectangle(selectedStroke, bounds);
            }
            var rect = new Rectangle(
                this.Padding.Left,
                this.Padding.Top,
                this.Width - this.Padding.Horizontal,
                this.Height - this.Padding.Vertical
            );
            this.tool.DrawToolIcon(e.Graphics, stroke, fill, rect);
        }      
    }
}
