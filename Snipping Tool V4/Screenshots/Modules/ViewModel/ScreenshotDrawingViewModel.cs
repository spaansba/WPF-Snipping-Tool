using Snipping_Tool_V4.Modules;
using Snipping_Tool_V4.Screenshots.Modules.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snipping_Tool_V4.Screenshots.Modules.ViewModels
{
    public class ScreenshotDrawingViewModel
    {
        public Tool? CurrentTool { get; set; }
        public int PenThickness { get; set; }
        public Color PenColor { get; set; } = Color.Black;
        public Pen Pen => new Pen(PenColor, PenThickness);
        public List<Shape> Shapes { get; set; } = new();

        public event EventHandler? DrawingChanged;
        private void RaiseDrawingChanged() => this.DrawingChanged?.Invoke(this, EventArgs.Empty);


        public void Undo()
        {
            if (this.Shapes.Count > 0)
            {
                this.Shapes.RemoveAt(this.Shapes.Count - 1);
                RaiseDrawingChanged();
            }
        }

        public void Clear()
        {
            this.Shapes.Clear();
            RaiseDrawingChanged();
        }

        public void Reset()
        {
            CurrentTool.Reset();
            RaiseDrawingChanged();
        }

        public void Begin(Point location)
        {
            CurrentTool?.Begin(location, PenCache.GetPen(this.Pen.Color, this.PenThickness));
            RaiseDrawingChanged();
        }

        public void Continue(Point location)
        {
            if (CurrentTool != null && CurrentTool.IsActive)
            {
                this.CurrentTool.Continue(location);
                RaiseDrawingChanged();
            }
        }

        public void Finish(Point location)
        {
            this.CurrentTool?.Finish(location, this.Shapes);
            RaiseDrawingChanged();
        }



        public void Draw(Graphics graphics)
        {
            foreach (Shape shape in this.Shapes)
            {
                shape.Draw(graphics);
            }

            if (this.CurrentTool != null && this.CurrentTool.IsActive)
            {
                this.CurrentTool.Draw(graphics);
            }
        }
    }
}
