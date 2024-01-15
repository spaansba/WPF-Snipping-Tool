using CommunityToolkit.Mvvm.ComponentModel;
using Snipping_Tool_V4.Modules;
using Snipping_Tool_V4.Screenshots.Modules.Drawing.Tools;
using System.ComponentModel;
using System.Runtime.CompilerServices;
//https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/access-modifiers#summary-table

namespace Snipping_Tool_V4.Screenshots.Modules.Drawing
{
    public partial class ScreenshotDrawingViewModel : ObservableObject
    {
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Stroke))] // If thickness changes, Notify stroke pen
        private int penThickness = 4;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Stroke))] // If color changes, Notify stroke pen
        public Color penColor = Color.Black;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Fill))] // If color changes, Notify fill brush
        public Color fillColor = Color.Transparent;
        //Color.FromArgb(Color.LightBlue);
        
        public Brush Fill => new SolidBrush(FillColor);

        public Pen Stroke => PenCache.GetPen(penColor, penThickness);
        public List<Shape> Shapes { get; set; } = new();

        public event EventHandler? DrawingChanged;
        private void RaiseDrawingChanged() => DrawingChanged?.Invoke(this, EventArgs.Empty);

        [ObservableProperty]
        private Tool? currentTool;

        public void Begin(Point location)
        {
            CurrentTool?.Begin(location, PenCache.GetPen(Stroke.Color, PenThickness), Fill);
            RaiseDrawingChanged();
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
