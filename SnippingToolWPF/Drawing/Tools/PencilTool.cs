using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using System.Windows;
using System.Windows.Media;

namespace SnippingToolWPF.Drawing.Tools
{

    //TODO : hold ctrl to snap the end to the beginning of the line 
    public sealed class PencilTool : IDrawingTool<Polyline>
    {
        private readonly PencilsSidePanelViewModel options;
        public PencilTool(PencilsSidePanelViewModel options)
        {
            this.options = options;
        }

        public Polyline Visual { get; } = new Polyline();
        private const int FreehandSensitivity = 4;

        public DrawingToolAction LeftButtonDown(Point position)
        {
            Visual.StrokeThickness = this.options.Thickness;
            Visual.Stroke = Brushes.Black; // TODO: Allow setting this in PencilsSidePanelViewModel
            Visual.Opacity = this.options.RealOpacity;
            Visual.UseLayoutRounding = true;
            Visual.StrokeLineJoin = Visual.StrokeLineJoin;
            Visual.Points.Clear();
            Visual.Points.Add(position);
            return DrawingToolAction.StartMouseCapture();
        }
        public DrawingToolAction MouseMove(Point position)
        {
            if (Visual.Points.Count > 0)
            {
               Point lastPoint = Visual.Points[Visual.Points.Count - 1];
               double distance = Point.Subtract(lastPoint, position).Length;
                if (distance < FreehandSensitivity) 
                    return DrawingToolAction.DoNothing;
            }
            Visual.Points.Add(position);
            return DrawingToolAction.DoNothing;
        }

        public DrawingToolAction LeftButtonUp()
        {
            return new DrawingToolAction
                (StartAction: DrawingToolActionItem.Shape(
                new Polyline()
                {
                    Stroke = Visual.Stroke,
                    StrokeThickness = Visual.StrokeThickness,
                    Opacity = Visual.Opacity,
                    UseLayoutRounding = true,
                    StrokeLineJoin = Visual.StrokeLineJoin,
                    Points = new PointCollection(Visual.Points),
                }
                ),
                StopAction: DrawingToolActionItem.MouseCapture()
            );
        }
    }
}
