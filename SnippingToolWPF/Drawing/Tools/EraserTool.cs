using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;

namespace SnippingToolWPF.Drawing.Tools
{
    public sealed class EraserTool : IDrawingTool<Polyline>
    {
        private readonly PencilsSidePanelViewModel options;
        private DrawingViewModel DrawingViewModel;
        public EraserTool(PencilsSidePanelViewModel options, DrawingViewModel drawingView)
        {
            this.DrawingViewModel = drawingView;
            this.options = options;
        }
        public Polyline Visual { get; } = new Polyline();

        public DrawingToolAction LeftButtonDown(Point position)
        {
            Visual.StrokeThickness = this.options.Thickness;
            Visual.Stroke = null;
            return DrawingToolAction.StartMouseCapture();
        }

        public DrawingToolAction MouseMove(Point position)
        {
            foreach (UIElement element in DrawingViewModel.DrawingObjects)
            {
                if (element is Polyline polyline)
                {
                    if (polyline.Points.Contains(position))
                    {
                        polyline.Points.Remove(position);
                    }
                }
            }
            return DrawingToolAction.DoNothing;
        }
        public DrawingToolAction LeftButtonUp()
        {
            return DrawingToolAction.StopMouseCapture();
        }
    }
}
