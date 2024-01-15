using Snipping_Tool_V4.Modules;
using Snipping_Tool_V4.Screenshots.Modules.Drawing;
using Snipping_Tool_V4.Screenshots.Modules.Drawing.Tools;

namespace Snipping_Tool_V4.Screenshots.Modules.Screen_Screenshot
{
    public abstract class PictureBoxButton<Type> : PictureBox
    {
        protected Type Value { get; }

        //TODO: MAke all buttons disabled if in screenshot taking mode or when there is no picture in the picture box
        protected static Brush SelectionRectangleFill { get; } = new SolidBrush(Color.FromArgb(30, Color.Blue));
        protected static Pen SelectionRectangleStroke { get; set; } = PenCache.GetPen(Color.Black, 1);
        protected ScreenshotDrawingViewModel Viewmodel { get; }
        protected abstract void HandleClick();
        protected abstract bool IsSelected { get; }
        protected abstract void Draw(Graphics graphics, Rectangle rect);

        public PictureBoxButton(Type value, Size size, ScreenshotDrawingViewModel viewModel)
        {
            Value = value;
            Viewmodel = viewModel;
            viewModel.PropertyChanged += (_, _) => this.Invalidate(); // Invalidate when the current tool changes, so if we select a tool programatically the button will update
            this.BackColor = Color.Transparent;
            this.Tag = value;
            this.DoubleBuffered = true;
            this.Margin = new Padding(1);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            Rectangle rect = new Rectangle(Point.Empty, this.Size).Shrink(new Padding(1)); // shrink by 1 to make room for the border

            if (this.IsSelected && typeof(Type) != typeof(Color))
            {
                e.Graphics.FillRectangle(SelectionRectangleFill, rect);
                e.Graphics.DrawRectangle(SelectionRectangleStroke, rect);
            }

            rect = rect.Shrink(new Padding(2));
            Draw(e.Graphics, rect);
        }

        protected override void OnClick(EventArgs e)
        {
            HandleClick();
            base.OnClick(e);
        }
    }
    public class ToolButton : PictureBoxButton<Tool>
    {
        public ToolButton(Tool value, Size size, ScreenshotDrawingViewModel viewModel) : base(value, size, viewModel)
        {
        }

        protected override void HandleClick()
        {
            this.Viewmodel.CurrentTool = this.Value;
        }

        protected override bool IsSelected => this.Viewmodel.CurrentTool == this.Value;

        protected override void Draw(Graphics g, Rectangle bounds)
        {
            Pen userStroke;
            if (Viewmodel.Stroke.Color == Color.White || Viewmodel.Stroke.Color == Color.Transparent)
            {
                userStroke = PenCache.GetPen(Color.LightGray, 1);
            }
            else
            {
                userStroke = PenCache.GetPen(this.Viewmodel.Stroke.Color, 1);
            }
           

            this.Value.DrawToolIcon(g, userStroke, Viewmodel.Fill, bounds);
        }
    }

    public class LineThicknessButton : PictureBoxButton<int>
    {
        public LineThicknessButton(int value, Size size, ScreenshotDrawingViewModel viewModel) : base(value, size, viewModel)
        {
        }

        protected override void HandleClick()
        {
            this.Viewmodel.PenThickness = this.Value;
        }

        protected override bool IsSelected => this.Viewmodel.PenThickness == this.Value;

        protected override void Draw(Graphics g, Rectangle bounds)
        {
            Pen userStroke = PenCache.GetPen(this.Viewmodel.Stroke.Color, 1);
            g.DrawLine(userStroke, new Point(bounds.Left,bounds.Top), new Point(bounds.Right, bounds.Bottom));
        }
    }

    public class ColorSelectionStrokeButton : PictureBoxButton<Color>
    {
        public ColorSelectionStrokeButton(Color value, Size size, ScreenshotDrawingViewModel viewModel) : base(value, size, viewModel)
        {
        }

        protected override void HandleClick()
        {
            ColorDialog colorDialog = new ColorDialog() { FullOpen = true };

            if (colorDialog.ShowDialog() == DialogResult.OK && colorDialog.Color != Color.Transparent)
                this.Viewmodel.PenColor = colorDialog.Color;
        }

        protected override bool IsSelected => this.Viewmodel.Stroke.Color == this.Value;

        protected override void Draw(Graphics g, Rectangle bounds)
        {
            Pen userStroke = PenCache.GetPen(this.Viewmodel.Stroke.Color, 1);
            g.FillRectangle(userStroke.Brush, bounds);
        }
    }
    public class ColorSelectionFillButton : PictureBoxButton<Color>
    {
        public ColorSelectionFillButton(Color value, Size size, ScreenshotDrawingViewModel viewModel) : base(value, size, viewModel)
        {
        }

        protected override void HandleClick()
        {
            ColorDialog colorDialog = new ColorDialog() { FullOpen = true };

            if (colorDialog.ShowDialog() == DialogResult.OK)
                this.Viewmodel.FillColor = colorDialog.Color;
        }

        protected override bool IsSelected => this.Viewmodel.fillColor == this.Value;

        protected override void Draw(Graphics g, Rectangle bounds)
        {
            Brush fill;
            if (Viewmodel.fillColor == Color.Transparent)
            {
                fill = new SolidBrush(Color.White);
            }
            else
            {
                fill = this.Viewmodel.Fill;
            }
            g.FillRectangle(fill, bounds);
        }
    }
}
