using Snipping_Tool_V4.Modules;
using Snipping_Tool_V4.Screenshots.Modules.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snipping_Tool_V4.Screenshots.Modules.Screen_Screenshot
{
    public class ToolButton : RadioButton
    {
        public Tool Tool { get; }
        private static Panel DisplayPanel { get; set; }
        private static Pen SelectedStroke { get; } = PenCache.GetPen(Color.Blue, 1);
        private static Brush SelectedFill { get; } = new SolidBrush(Color.FromArgb(50, Color.Blue));
        private static Pen Stroke { get; } = PenCache.GetPen(Color.DeepSkyBlue, 1);
        private static Brush Fill { get; set; } = new SolidBrush(Color.FromArgb(50, Color.DeepSkyBlue));
        public ToolButton(Tool tool, Panel panel)
        {
            Tool = tool;
            DisplayPanel = panel;
            this.BackColor = Color.Transparent;
            this.Appearance = Appearance.Button;
            this.Text = "";
            this.AutoSize = false;  
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            int columns = 4;
            int rows = 3;

            // Calculate panel dimensions
            int panelHeight = DisplayPanel.Height / rows;
            int panelWidth = DisplayPanel.Width / columns;

            // Determine the smaller dimension for a perfect square
            int size = Math.Min(panelWidth, panelHeight);

            // Set the ClientSize to enforce a square shape
            this.ClientSize = new Size(size, size);

            var rect = new Rectangle(panelHeight, panelWidth, size, size);

            if (this.Checked)
            {
                var bounds = new Rectangle(Point.Empty, this.Size);
                e.Graphics.FillRectangle(SelectedFill, bounds);
                e.Graphics.DrawRectangle(SelectedStroke, bounds);
            }

            // TODO: For now put fill to null, later we will create a logic to either fill or draw the icon
            Fill = null;
            this.Tool.DrawToolIcon(e.Graphics, Stroke, Fill, rect);
        }
    }
}
