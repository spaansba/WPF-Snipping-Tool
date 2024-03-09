using SnippingToolWPF.Drawing.Shapes;
using SnippingToolWPF.ExtensionMethods;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SnippingToolWPF.Drawing.Tools.ToolAction;

public sealed class PolygonTool : DraggingTool<DrawingShape>
{
    private readonly ShapesSidePanelViewModel options;
    public override bool IsDrawing { get; set; } = false;
    public override DrawingShape DrawingShape { get; }

    private Point startPoint;

    public PolygonTool(ShapesSidePanelViewModel options)
    {
        this.options = options;
        this.DrawingShape = options.polygonOption;
        ResetVisual();
    }
    public override void ResetVisual()
    {
        this.DrawingShape.Width = 0;
        this.DrawingShape.Height = 0;
    }

    #region Mouse Events
    public override DrawingToolAction LeftButtonDown(Point position, DrawingShape? item)
    {
        IsDrawing = true;
        startPoint = position;
        Canvas.SetLeft(this.DrawingShape, position.X);
        Canvas.SetTop(this.DrawingShape, position.Y);
        this.DrawingShape.Stroke = options.shapeStroke;
        this.DrawingShape.StrokeDashCap = PenLineCap.Round;
        this.DrawingShape.StrokeStartLineCap = PenLineCap.Round;
        this.DrawingShape.StrokeEndLineCap = PenLineCap.Round;
        this.DrawingShape.StrokeLineJoin = PenLineJoin.Round;
        this.DrawingShape.StrokeThickness = options.Thickness;
        this.DrawingShape.Opacity = options.RealOpacity;
        this.DrawingShape.Fill = options.shapeFill;
        return DrawingToolAction.StartMouseCapture();
    }

    public override DrawingToolAction MouseMove(Point position, DrawingShape? item)
    {
        if (!IsDrawing)
            return DrawingToolAction.DoNothing;

        CheckIfLockedAspectRatio();
        if (LockedAspectRatio)
            position = GetLockedAspectRatioEndPoint(position);

        // Set current size of the polygon (like a rectangle)
        this.DrawingShape.Width = Math.Abs(position.X - startPoint.X);
        this.DrawingShape.Height = Math.Abs(position.Y - startPoint.Y);

        // Set the new position of the polygon (we do this because otherwise a polygon can only be drawn to bottom right and not in any direction)
        Canvas.SetLeft(this.DrawingShape, Math.Min(startPoint.X, position.X)); 
        Canvas.SetTop(this.DrawingShape, Math.Min(startPoint.Y, position.Y));
        return DrawingToolAction.DoNothing;  
    }
    public override DrawingToolAction LeftButtonUp()
    {
        IsDrawing = false;
        var finalPolygon = this.DrawingShape.Clone(new Size(this.DrawingShape.Width, this.DrawingShape.Height));
        ResetVisual();
        return new DrawingToolAction(StartAction: DrawingToolActionItem.Shape(finalPolygon), StopAction: DrawingToolActionItem.MouseCapture()).WithUndo();
    }

    /// <summary>
    /// Resets the visual if drawing and pressing right mouse button
    /// </summary>
    public override void RightButtonDown()
    {
        if (IsDrawing)
        {
            ResetVisual();
            IsDrawing = false;
        }
    }
    #endregion

    #region Locked Aspect Ratio
    /// <summary>
    /// If user holds shift, return true, else false
    /// When locked Aspect Ratio is activated, the polygon drawn on the canvas will have perfect perportions. e.g. perfect Rectangle / triangle
    /// </summary>
    private void CheckIfLockedAspectRatio() => LockedAspectRatio = Keyboard.Modifiers == ModifierKeys.Shift;
    public override bool LockedAspectRatio { get; set; } = false;

    public Point GetLockedAspectRatioEndPoint(Point location)
    {
        double dx = location.X - startPoint.X;
        double dy = location.Y - startPoint.Y;
        double max = Math.Max(Math.Abs(dx), Math.Abs(dy));
        double x = startPoint.X + Math.Sign(dx) * max;
        double y = startPoint.Y + Math.Sign(dy) * max;
        return new Point(x, y);
    }
    #endregion
}