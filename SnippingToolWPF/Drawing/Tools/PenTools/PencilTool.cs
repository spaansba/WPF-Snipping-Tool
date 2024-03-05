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
using System.Windows.Media.Effects;
using System.Drawing.Imaging;
using System.Windows.Media.Imaging;
using System.IO;
using System.Reflection;
using SnippingToolWPF.ExtensionMethods;
using System.Windows.Controls;

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

    // TODO: If user holds shift draw a perfect straight line
    public bool LockedAspectRatio { get; set; } = false;
    public bool IsDrawing { get; set; } = false;

    private int counter = 0;
    //The amount of points between every line within the polyline drawn
    private const int FreehandSensitivity = 4;

    public DrawingToolAction LeftButtonDown(Point position, UIElement? element)
    {
        IsDrawing = true;
        Visual.StrokeThickness = this.options.Thickness;
        Visual.Stroke = this.options.SelectedBrush;
        Visual.Opacity = this.options.RealOpacity;
        Visual.UseLayoutRounding = true;
        Visual.StrokeDashCap = PenLineCap.Round;
        Visual.StrokeStartLineCap = PenLineCap.Round;
        Visual.StrokeEndLineCap = PenLineCap.Round;
        Visual.StrokeLineJoin = PenLineJoin.Round;
        Visual.Points.Clear();
        //     Visual.Effect = customEffect;
        Visual.Points.Add(position);
        return DrawingToolAction.StartMouseCapture();

    }

    public DrawingToolAction MouseMove(Point position, UIElement? element)
    {
        if (!IsDrawing)
            return DrawingToolAction.DoNothing;

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
        IsDrawing = false;
        if (options.PenTipArrow)
            AddArrowHead(Visual);

        // Get the smallest X and Y and create a new point list based on the Visual.Points
        // In this list we substract the minY and minX from each point so we can set the canvas of the Shape correctly to match the DrawingCanvas
        var minX = this.Visual.Points.Min(static p => p.X);
        var minY = this.Visual.Points.Min(static p => p.Y);
        var newPoints = new PointCollection(this.Visual.Points.Count);
        foreach (var point in this.Visual.Points)
        {
            newPoints.Add(new Point(point.X - minX, point.Y - minY));
        }

        // Clone the Polyline so we remove the parent and put it on the canvas, als now we can clear the current Visual
        Polyline finalLine = (Polyline)this.Visual.Clone();
        finalLine.Points = newPoints;

        Canvas.SetLeft(finalLine, minX-1); //-1 to fit in the DrawingListBox
        Canvas.SetTop(finalLine, minY-1);

        Visual.Points.Clear();
        return new DrawingToolAction(StartAction: DrawingToolActionItem.Shape(finalLine), StopAction: DrawingToolActionItem.MouseCapture()).WithUndo();
    }



    #region Calculate / add arrow head

    private void AddArrowHead(Polyline visual)
    {
        if (visual.Points is [.., _, var point1, var point2])
        { 
            var(arrowPoint1, arrowPoint2) = GetArrowHeadPoints(point1, point2);

            visual.Points.Add(arrowPoint1);
            visual.Points.Add(point2); // make sure it doesn't become a triangle
            visual.Points.Add(arrowPoint2);
        }
    }

    private (Point A, Point B) GetArrowHeadPoints(Point position1, Point position2)
    {
        Vector lineDirection = position2 - position1;
        double arrowheadLength = Math.Min(Visual.StrokeThickness / 1.2, 30); // change the first value to make the line smaller/bigger

        Vector rotatedDirection1 = lineDirection.RotateDegrees(140);
        Vector rotatedDirection2 = lineDirection.RotateDegrees(-140);

        Point arrowheadEnd1 = position2 + arrowheadLength * rotatedDirection1;
        Point arrowheadEnd2 = position2 + arrowheadLength * rotatedDirection2;

        return (arrowheadEnd1, arrowheadEnd2);
    }


    public static Vector Rotate(Vector vector, double angle)
    {
        double newX = vector.X * Math.Cos(angle) - vector.Y * Math.Sin(angle);
        double newY = vector.X * Math.Sin(angle) + vector.Y * Math.Cos(angle);
        return new Vector(newX, newY);
    }

    #endregion


    //TODO: make the XAML colorPicker have an option for Rainbow Gradient
    public static LinearGradientBrush GetRainbowGradientBrush()
    {
        LinearGradientBrush rainbowBrush = new LinearGradientBrush();
        rainbowBrush.StartPoint = new Point(0, 0);
        rainbowBrush.EndPoint = new Point(1, 0);

        GradientStopCollection gradientStops = new GradientStopCollection();
        gradientStops.Add(new GradientStop(Colors.Red, 0));
        gradientStops.Add(new GradientStop(Colors.Orange, 0.17));
        gradientStops.Add(new GradientStop(Colors.Yellow, 0.33));
        gradientStops.Add(new GradientStop(Colors.Green, 0.5));
        gradientStops.Add(new GradientStop(Colors.Blue, 0.67));
        gradientStops.Add(new GradientStop(Colors.Indigo, 0.83));
        gradientStops.Add(new GradientStop(Colors.Violet, 1.0));

        rainbowBrush.GradientStops = gradientStops;

        return rainbowBrush;
    }
}