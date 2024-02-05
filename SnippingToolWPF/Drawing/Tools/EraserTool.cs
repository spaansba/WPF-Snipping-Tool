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
                    // Iterate through polyline segments or points
                    for (int i = 0; i < polyline.Points.Count - 1; i++)
                    {
                        Point startPoint = polyline.Points[i];
                        Point endPoint = polyline.Points[i + 1];

                        // Check if the mouse position is close to the segment
                        if (IsPointNearSegment(position, startPoint, endPoint))
                        {
                            // Adjust the polyline by removing or modifying the segment
                            polyline.Points.RemoveAt(i + 1);
                            // Optionally, you may need to update other properties like DefiningGeometry
                        }
                    }
                }
            }
            return DrawingToolAction.DoNothing;
        }

        private bool IsPointNearSegment(Point p, Point start, Point end)
        {
            const double Tolerance = 5; // Adjust this tolerance based on your requirements

            double distance = CalculateDistanceToSegment(p, start, end);

            return distance < Tolerance;
        }

        private double CalculateDistanceToSegment(Point p, Point start, Point end)
        {
            double lengthSquared = (start - end).LengthSquared;
            if (lengthSquared == 0)
                return (p - start).Length;

            double t = Math.Max(0, Math.Min(1, ((p - start) * (end - start)) / lengthSquared));
            Point projection = start + t * (end - start);

            return (p - projection).Length;
        }
        public DrawingToolAction LeftButtonUp()
        {
            return DrawingToolAction.StopMouseCapture();
        }
    }
}
