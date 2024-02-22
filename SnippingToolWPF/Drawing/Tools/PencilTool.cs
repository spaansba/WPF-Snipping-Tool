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

namespace SnippingToolWPF.Drawing.Tools;


//TODO : hold ctrl to snap the end to the beginning of the line 
public sealed class PencilTool : IDrawingTool<Polyline>
{
    private readonly PencilsSidePanelViewModel options;
    public PencilTool(PencilsSidePanelViewModel options)
    {
        this.options = options;
    }

    public Polyline Visual { get; } = new Polyline();

    //The amount of points between every line within the polyline drawn
    private const int FreehandSensitivity = 4;

    public DrawingToolAction LeftButtonDown(Point position, UIElement? element)
    {
        Visual.StrokeThickness = this.options.Thickness;
        Visual.Stroke = this.options.SelectedBrush;
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

    public DrawingToolAction MouseMove(Point position, UIElement? element)
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
 
        return DrawingToolAction.DoNothing;
    }

    public DrawingToolAction LeftButtonUp()
    {
        if (options.PenTipArrow)
            AddArrowHead(Visual);

        Polyline finalLine = new Polyline()
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

        Visual.Points.Clear();
        return new DrawingToolAction(StartAction: DrawingToolActionItem.Shape(finalLine), StopAction: DrawingToolActionItem.MouseCapture());
    }

    #region Calculate / add arrow head

    private void AddArrowHead(Polyline visual)
    {
        if (visual.Points.Count > 3)
        {
            Point point1 = visual.Points[visual.Points.Count - 2];
            Point point2 = visual.Points[visual.Points.Count - 1];

            Point[] arrowheadPoints = GetArrowHeadPoints(point1, point2);
            visual.Points.Add(arrowheadPoints[0]);
            visual.Points.Add(point2); // make sure it doesn't become a triangle
            visual.Points.Add(arrowheadPoints[1]);
        }
    }

    private Point[] GetArrowHeadPoints(Point position1, Point position2)
    {
        Vector lineDirection = position2 - position1;
        double arrowheadLength = Math.Min(Visual.StrokeThickness / 1.2, 30); // change the first value to make the line smaller/bigger
        double angleInRadians = 140 * (Math.PI / 180);

        Vector rotatedDirection1 = Rotate(lineDirection, -angleInRadians); // Rotate clockwise 
        Vector rotatedDirection2 = Rotate(lineDirection, angleInRadians); // Rotate counterclockwise 

        Point arrowheadEnd1 = position2 + arrowheadLength * rotatedDirection1;
        Point arrowheadEnd2 = position2 + arrowheadLength * rotatedDirection2;

        return [arrowheadEnd1, arrowheadEnd2];
    }

    public static Vector Rotate(Vector vector, double angle)
    {
        double newX = vector.X * Math.Cos(angle) - vector.Y * Math.Sin(angle);
        double newY = vector.X * Math.Sin(angle) + vector.Y * Math.Cos(angle);
        return new Vector(newX, newY);
    }

    #endregion
}
