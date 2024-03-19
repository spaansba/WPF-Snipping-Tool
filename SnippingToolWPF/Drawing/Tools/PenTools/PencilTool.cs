using System.Windows;
using System.Windows.Media;
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
    
    // TODO: If user holds shift draw a perfect straight line
    public override bool LockedAspectRatio { get; set; } = false;
    public override bool IsDrawing { get; set; }

    //public override void ResetVisual() => this.DrawingShape.Points.Clear();
    public override void ResetVisual()
    {
        DrawingShape.Points.Clear();
        DrawingShape.StrokeThickness = 10;
    }

    #region Mouse Events

    public override DrawingToolAction LeftButtonDown(Point position, DrawingShape? element)
    {
        IsDrawing = true;
        DrawingShape.StrokeThickness = options.Thickness;
        DrawingShape.Stroke = options.SelectedBrush;
        DrawingShape.Opacity = options.RealOpacity;
        DrawingShape.UseLayoutRounding = true;
        DrawingShape.StrokeDashCap = PenLineCap.Round;
        DrawingShape.StrokeStartLineCap = PenLineCap.Round;
        DrawingShape.StrokeEndLineCap = PenLineCap.Round;
        DrawingShape.StrokeLineJoin = PenLineJoin.Round;
        DrawingShape.Points.Add(position);

        //Visual.Points.Clear();
        ////     Visual.Effect = customEffect;
        return DrawingToolAction.StartMouseCapture();
    }

    //The amount of points between every line within the polyline drawn
    // ReSharper disable once UnusedMember.Local
    private const int FreehandSensitivity = 4;

    public override DrawingToolAction MouseMove(Point position, DrawingShape? element)
    {
        if (!IsDrawing)
         return DrawingToolAction.DoNothing;

        if (DrawingShape.Points.Count > 0)
        {
            var lastPoint = DrawingShape.Points[^1];
            var distance = Point.Subtract(lastPoint, position).Length;

            if (distance < FreehandSensitivity)
                return DrawingToolAction.DoNothing;
        }

        DrawingShape.Points.Add(position);

        //// TODO: Make it working so the arrow is getting added while drawing

        return DrawingToolAction.DoNothing;
    }

    public override DrawingToolAction LeftButtonUp()
    {
        IsDrawing = false;
        if (options.PenTipArrow)
            AddArrowHead(DrawingShape);
        
        // Clone the Polyline so we remove the parent and put it on the canvas, als now we can clear the current Visual
        var finalLine = this.DrawingShape.Clone();
        
        ResetVisual();
        return new DrawingToolAction(StartAction: DrawingToolActionItem.Shape(finalLine), StopAction: DrawingToolActionItem.MouseCapture()).WithUndo();
    }

    /// <summary>
    ///     Resets the visual if drawing and right clicking
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
    private void AddArrowHead(RegularPolylineDrawingShape visual)
    {
        if (visual.Points is not [.., _, var point1, var point2]) return;
        var (arrowPoint1, arrowPoint2) = GetArrowHeadPoints(point1, point2);

        visual.Points.Add(arrowPoint1);
        visual.Points.Add(point2); // make sure it doesn't become a triangle
        visual.Points.Add(arrowPoint2);
    }

    private (Point A, Point B) GetArrowHeadPoints(Point position1, Point position2)
    {
        var lineDirection = position2 - position1;
        var arrowheadLength =
            Math.Min(DrawingShape.StrokeThickness / 1.2, 30); // change the first value to make the line smaller/bigger

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
        return new Vector(newX, newY);
    }

    #endregion
    
    //TODO: make the XAML colorPicker have an option for Rainbow Gradient
    public static LinearGradientBrush GetRainbowGradientBrush()
    {
        var rainbowBrush = new LinearGradientBrush();
        rainbowBrush.StartPoint = new Point(0, 0);
        rainbowBrush.EndPoint = new Point(1, 0);

        var gradientStops = new GradientStopCollection();
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