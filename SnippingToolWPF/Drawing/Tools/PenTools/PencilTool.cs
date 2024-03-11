using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using SnippingToolWPF.ExtensionMethods;
using SnippingToolWPF.SidePanel.PencilSidePanel;
using SnippingToolWPF.Tools.ToolAction;

namespace SnippingToolWPF.Tools.PenTools;

//TODO : hold ctrl to snap the end to the beginning of the line 
public sealed class PencilTool : DraggingTool<RegularPolylineDrawingShape>
{
    private readonly PencilsSidePanelViewModel options;
    public PencilTool(PencilsSidePanelViewModel options)
    {
        this.options = options;
    }

    public override RegularPolylineDrawingShape DrawingShape { get; } = new();

    // TODO: If user holds shift draw a perfect straight line
    public override bool LockedAspectRatio { get; set; } = false;
    public override bool IsDrawing { get; set; }

    //public override void ResetVisual() => this.DrawingShape.Points.Clear();
    public override void ResetVisual() => this.DrawingShape.StrokeThickness = 10;
    #region Mouse Events
    public override DrawingToolAction LeftButtonDown(Point position, DrawingShape? element)
    {
        IsDrawing = true;
        this.DrawingShape.StrokeThickness = this.options.Thickness;
        this.DrawingShape.Stroke = this.options.SelectedBrush;
        this.DrawingShape.Opacity = this.options.RealOpacity;
        this.DrawingShape.UseLayoutRounding = true;
        this.DrawingShape.StrokeDashCap = PenLineCap.Round;
        this.DrawingShape.StrokeStartLineCap = PenLineCap.Round;
        this.DrawingShape.StrokeEndLineCap = PenLineCap.Round;
        this.DrawingShape.StrokeLineJoin = PenLineJoin.Round;
        //Visual.Points.Clear();
        ////     Visual.Effect = customEffect;
        //Visual.Points.Add(position);
        return DrawingToolAction.StartMouseCapture();

    }

    //The amount of points between every line within the polyline drawn
    // ReSharper disable once UnusedMember.Local
    private const int FreehandSensitivity = 4;
    public override DrawingToolAction MouseMove(Point position, DrawingShape? element)
    {
        //if (!IsDrawing)
        // return DrawingToolAction.DoNothing;

        //if (Visual.Points.Count > 0)
        //{
        //    Point lastPoint = Visual.Points[Visual.Points.Count - 1];
        //    double distance = Point.Subtract(lastPoint, position).Length;

        //    if (distance < FreehandSensitivity)
        //        return DrawingToolAction.DoNothing;
        //}

        //Visual.Points.Add(position);

        //// TODO: Make it working so the arrow is getting added while drawing

        return DrawingToolAction.DoNothing;
    }

    public override DrawingToolAction LeftButtonUp()
    {
        //IsDrawing = false;
        //if (options.PenTipArrow)
        //    AddArrowHead(Visual);

        //// Get the smallest X and Y and create a new point list based on the Visual.Points
        //// In this list we substract the minY and minX from each point so we can set the canvas of the Shape correctly to match the DrawingCanvas
        //var minX = this.Visual.Points.Min(static p => p.X);
        //var minY = this.Visual.Points.Min(static p => p.Y);
        //var newPoints = new PointCollection(this.Visual.Points.Count);
        //foreach (var point in this.Visual.Points)
        //{
        //    newPoints.Add(new Point(point.X - minX, point.Y - minY));
        //}

        //// Clone the Polyline so we remove the parent and put it on the canvas, als now we can clear the current Visual
        //Polyline finalLine = (Polyline)this.Visual.Clone();
        //finalLine.Points = newPoints;

        //Canvas.SetLeft(finalLine, minX);
        //Canvas.SetTop(finalLine, minY);

        //ResetVisual();
        return DrawingToolAction.DoNothing;
   //     return new DrawingToolAction(StartAction: DrawingToolActionItem.Shape(finalLine), StopAction: DrawingToolActionItem.MouseCapture()).WithUndo();
    }

    /// <summary>
    /// Resets the visual if drawing and right clicking
    /// </summary>
    public override void RightButtonDown()
    {
        if (!IsDrawing) return;
        
        ResetVisual();
        IsDrawing = false;
    }

    #endregion

    #region Calculate / add arrow head

    // ReSharper disable once UnusedMember.Local
    private void AddArrowHead(Polyline visual)
    {
        if (visual.Points is not [.., _, var point1, var point2]) return;
        var(arrowPoint1, arrowPoint2) = GetArrowHeadPoints(point1, point2);

        visual.Points.Add(arrowPoint1);
        visual.Points.Add(point2); // make sure it doesn't become a triangle
        visual.Points.Add(arrowPoint2);
    }

    private (Point A, Point B) GetArrowHeadPoints(Point position1, Point position2)
    {
        var lineDirection = position2 - position1;
        var arrowheadLength = Math.Min(this.DrawingShape.StrokeThickness / 1.2, 30); // change the first value to make the line smaller/bigger

        var rotatedDirection1 = lineDirection.RotateDegrees(140);
        var rotatedDirection2 = lineDirection.RotateDegrees(-140);

        var arrowheadEnd1 = position2 + arrowheadLength * rotatedDirection1;
        var arrowheadEnd2 = position2 + arrowheadLength * rotatedDirection2;

        return (arrowheadEnd1, arrowheadEnd2);
    }


    public static Vector Rotate(Vector vector, double angle)
    {
        var newX = vector.X * Math.Cos(angle) - vector.Y * Math.Sin(angle);
        var newY = vector.X * Math.Sin(angle) + vector.Y * Math.Cos(angle);
        return new(newX, newY);
    }

    #endregion


    //TODO: make the XAML colorPicker have an option for Rainbow Gradient
    public static LinearGradientBrush GetRainbowGradientBrush()
    {
        var rainbowBrush = new LinearGradientBrush();
        rainbowBrush.StartPoint = new(0, 0);
        rainbowBrush.EndPoint = new(1, 0);

        var gradientStops = new GradientStopCollection();
        gradientStops.Add(new(Colors.Red, 0));
        gradientStops.Add(new(Colors.Orange, 0.17));
        gradientStops.Add(new(Colors.Yellow, 0.33));
        gradientStops.Add(new(Colors.Green, 0.5));
        gradientStops.Add(new(Colors.Blue, 0.67));
        gradientStops.Add(new(Colors.Indigo, 0.83));
        gradientStops.Add(new(Colors.Violet, 1.0));

        rainbowBrush.GradientStops = gradientStops;

        return rainbowBrush;
    }
}