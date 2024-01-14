using Snipping_Tool_V4.Modules;
using Snipping_Tool_V4.Screenshots.Modules.Drawing;

namespace Snipping_Tool_V4.Screenshots.Modules.Screen_Screenshot
{
    public class ToolButton : PictureBox
    {
        public Tool Tool { get; }
        private readonly int SizeOffset = 2;
        private static Brush SelectedFill { get; } = new SolidBrush(Color.FromArgb(30, Color.Blue));
        private static Brush Fill { get; set; } = new SolidBrush(Color.FromArgb(50, Color.LightBlue));
        private static Pen SelectedStroke { get; set; } = PenCache.GetPen(Color.Black, 1);
        private ScreenshotDrawingViewModel Viewmodel { get; set; }

        public ToolButton(Tool tool, Size size, ScreenshotDrawingViewModel viewModel)
        {
            Tool = tool;
            Viewmodel = viewModel;
            this.BackColor = Color.Transparent;
            this.Tag = tool;
            this.AutoSize = false;
            this.ClientSize = size;
            this.DoubleBuffered = true;
            viewModel.PropertyChanged += (_, _) => this.Invalidate();
        }

        protected override void OnClick(EventArgs e)
        {
            this.Viewmodel.CurrentTool = this.Tool;
            base.OnClick(e);
            foreach (ToolButton button in this.Parent.Controls)
            {
                button.Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            if (this.Viewmodel.CurrentTool == this.Tool)
            {
                var bounds = new Rectangle(1, 1, this.Size.Width - SizeOffset, this.Size.Height - SizeOffset); // - 2 to account for the border
                e.Graphics.FillRectangle(SelectedFill, bounds);
                e.Graphics.DrawRectangle(SelectedStroke, bounds);
            }
            else
            {
                this.BackColor = Color.Transparent;
            }

            var rect = new Rectangle(SizeOffset, SizeOffset, this.Size.Width - (SizeOffset * 2), this.Size.Height - (SizeOffset * 2));

            Pen userStroke = PenCache.GetPen(Viewmodel.Stroke.Color, 1);
            this.Tool.DrawToolIcon(e.Graphics, userStroke, Fill, rect);
        }
    }
}
