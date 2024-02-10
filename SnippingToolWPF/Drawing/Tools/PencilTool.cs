using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using System.Windows;
using System.Windows.Media;
using System.Diagnostics;
using System.IO.Pipes;

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

        public DrawingToolAction LeftButtonDown(Point position, UIElement? element)
        {
            Visual.StrokeThickness = this.options.Thickness;
            Visual.Stroke = this.options.SelectedBrush; // TODO: Allow setting this in PencilsSidePanelViewModel
            Visual.Opacity = this.options.RealOpacity;
            Visual.UseLayoutRounding = true;
            Visual.StrokeDashCap = PenLineCap.Round;
            Visual.StrokeStartLineCap = PenLineCap.Round;
            Visual.StrokeEndLineCap = PenLineCap.Round;
            Visual.StrokeLineJoin = PenLineJoin.Round;
            Visual.StrokeMiterLimit = 1d; //no clue what this does tbh
            Visual.Points.Clear();
            Visual.Points.Add(position);
            return DrawingToolAction.StartMouseCapture();
        }

        //private Point currentArrowPosition1;
        //private Point currentArrowPosition2;

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

            // TODO: Make it working so the arrow is getting added while drawing

            //if (options.penTipArrow && Visual.Points.Count > 2)
            //{
            //    Visual.Points.Remove(currentArrowPosition1);
            //    Visual.Points.Remove(currentArrowPosition2);

            //    Point point1 = Visual.Points[Visual.Points.Count - 2];
            //    Point point2 = Visual.Points[Visual.Points.Count - 1];
            //    Point[] arrowheadPoints = GetArrowHeadPoints(point1, point2);
            //    currentArrowPosition1 = arrowheadPoints[0];
            //    currentArrowPosition2 = arrowheadPoints[1];

            //    Visual.Points.Add(arrowheadPoints[0]);
            //    Visual.Points.Add(position);
            //    Visual.Points.Add(arrowheadPoints[1]);
            //}
            return DrawingToolAction.DoNothing;
        }

        public DrawingToolAction LeftButtonUp()
        {
            if (Visual.Points.Count < 2)
            {
                return DrawingToolAction.StopMouseCapture();
            }

            // Get the last two points from Visual.Points
            Point point1 = Visual.Points[Visual.Points.Count - 2];
            Point point2 = Visual.Points[Visual.Points.Count - 1];

            Point[] arrowheadPoints = GetArrowHeadPoints(point1, point2);

            Visual.Points.Add(arrowheadPoints[0]);
            Visual.Points.Add(point2); // make sure it doesnt become a triangle
            Visual.Points.Add(arrowheadPoints[1]);

            // Create the Polyline representing the arrowhead
            Polyline result = new Polyline()
            {
                Stroke = Visual.Stroke,
                StrokeThickness = Visual.StrokeThickness,
                Opacity = Visual.Opacity,
                UseLayoutRounding = true,
                StrokeDashCap = Visual.StrokeDashCap,
                StrokeStartLineCap = Visual.StrokeStartLineCap,
                StrokeEndLineCap = Visual.StrokeEndLineCap,
                StrokeLineJoin = Visual.StrokeLineJoin,
                StrokeMiterLimit = Visual.StrokeMiterLimit,
                Points = new PointCollection(Visual.Points),
            };

            // Return the DrawingToolAction with the arrowhead and the stop action for mouse capture
            return new DrawingToolAction(StartAction: DrawingToolActionItem.Shape(result), StopAction: DrawingToolActionItem.MouseCapture());
        }

        private Point[] GetArrowHeadPoints(Point position1, Point position2)
        {
            // Calculate the vector representing the direction of the line segment
            Vector lineDirection = position2 - position1;
           
            double arrowheadLength = Visual.StrokeThickness switch
            {
                <= 5 => 3,
                <= 15 => 5,
                <= 25 => 8,
                <= 35 => 10,
                <= 45 => 12,
                <= 55 => 16,
                <= 65 => 20,
                <= 75 => 24,
                <= 85 => 26,
                <= 100 => 30,
                _ => 5 // Default case
            };

            // Calculate the position of the arrowhead
            Point arrowheadPoint = position2;

            // Calculate the points of the arrowhead
            Vector rotatedDirection1 = Rotate(lineDirection, Math.PI - Math.PI / 5); // Rotate clockwise 
            Vector rotatedDirection2 = Rotate(lineDirection, Math.PI + Math.PI / 5); // Rotate counterclockwise 

            Point arrowheadEnd1 = arrowheadPoint + arrowheadLength * rotatedDirection1;
            Point arrowheadEnd2 = arrowheadPoint + arrowheadLength * rotatedDirection2;

            return [arrowheadEnd1, arrowheadEnd2];
        }

        public static Vector Rotate(Vector vector, double angle)
        {
            double newX = vector.X * Math.Cos(angle) - vector.Y * Math.Sin(angle);
            double newY = vector.X * Math.Sin(angle) + vector.Y * Math.Cos(angle);
            return new Vector(newX, newY);
        }
    }
}
