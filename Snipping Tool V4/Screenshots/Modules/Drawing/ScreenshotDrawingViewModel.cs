using CommunityToolkit.Mvvm.ComponentModel;
using Snipping_Tool_V4.Modules;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Snipping_Tool_V4.Screenshots.Modules.Drawing
{
    public partial class ScreenshotDrawingViewModel : ObservableObject
    {
        public int PenThickness { get; set; }
        public Color PenColor { get; set; } = Color.Black;
        public Pen Stroke => new Pen(PenColor, PenThickness);
        public List<Shape> Shapes { get; set; } = new();

        public event EventHandler? DrawingChanged;
        private void RaiseDrawingChanged() => DrawingChanged?.Invoke(this, EventArgs.Empty);

        [ObservableProperty]
        private Tool? currentTool;


        public void Begin(Point location)
        {
            CurrentTool?.Begin(location, PenCache.GetPen(Stroke.Color, PenThickness));
            RaiseDrawingChanged();
        }

        public void SetButtonState()
        {

        }

        public void Continue(Point location)
        {
            if (CurrentTool != null && CurrentTool.IsActive)
            {
                CurrentTool.Continue(location);
                RaiseDrawingChanged();
            }
        }

        public void Finish(Point location)
        {
            CurrentTool?.Finish(location, Shapes);
            RaiseDrawingChanged();
        }

        public void Undo()
        {
            if (Shapes.Count > 0)
            {
                Shapes.RemoveAt(Shapes.Count - 1);
                RaiseDrawingChanged();
            }
        }

        public void Clear()
        {
            Shapes.Clear();
            RaiseDrawingChanged();
        }

        public void Reset()
        {
            CurrentTool.Reset();
            RaiseDrawingChanged();
        }

        public void SetShiftPressed(bool shiftPressed)
        {
            CurrentTool.LockedAspectRatio = shiftPressed;
        }

        public void Draw(Graphics graphics)
        {
            foreach (Shape shape in Shapes)
            {
                shape.Draw(graphics);
            }

            if (CurrentTool != null && CurrentTool.IsActive)
            {
                CurrentTool.Draw(graphics);
            }
        }
    }
}
