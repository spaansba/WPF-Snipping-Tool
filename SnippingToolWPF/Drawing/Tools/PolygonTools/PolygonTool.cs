using SnippingToolWPF.Drawing.Tools.PolygonTools;
using SnippingToolWPF.ExtensionMethods;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SnippingToolWPF.Drawing.Tools.ToolAction;

//TODO: Make this class generic for all Shapes
public sealed class PolygonTool : IDrawingTool<Shape>
{
    private readonly ShapesSidePanelViewModel options;
    public bool IsDrawing { get; set; } = false;
    public Shape Visual { get; set; }

    private Point startPoint;

    public PolygonTool(ShapesSidePanelViewModel options)
    {
        this.options = options;
        this.Visual = CreateInitialPolygon.Create(this.options.polygonOption);
    }
    public DrawingToolAction LeftButtonDown(Point position, UIElement? item)
    {
        IsDrawing = true;
        startPoint = position;
        Canvas.SetLeft(this.Visual, position.X);
        Canvas.SetTop(this.Visual, position.Y);
        this.Visual.Stroke = options.shapeStroke;
        this.Visual.StrokeThickness = options.shapeStrokeThickness;
        this.Visual.Opacity = options.shapeOpacity;
        this.Visual.Fill = options.shapeFill;
        return DrawingToolAction.StartMouseCapture();
    }

    public DrawingToolAction MouseMove(Point position, UIElement? item)
    {
        if (!IsDrawing)
            return DrawingToolAction.DoNothing;

        CheckIfLockedAspectRatio();
        if (LockedAspectRatio)
            position = GetLockedAspectRatioEndPoint(position);

        // Set current size of the shape (like a rectangle)
        this.Visual.Width = Math.Abs(position.X - startPoint.X);
        this.Visual.Height = Math.Abs(position.Y - startPoint.Y);

        // Set the new position of the shape (we do this because otherwise a shape can only be drawn to bottom right and not in any direction)
        Canvas.SetLeft(this.Visual, Math.Min(startPoint.X, position.X)); 
        Canvas.SetTop(this.Visual, Math.Min(startPoint.Y, position.Y));
        return DrawingToolAction.DoNothing;  
    }
    public DrawingToolAction LeftButtonUp()
    {
        IsDrawing = false;
        var finalShape = this.Visual.Clone(); 
        Canvas.SetLeft(finalShape, Canvas.GetLeft(finalShape)); 
        Canvas.SetTop(finalShape, Canvas.GetTop(finalShape)); 
        this.Visual.Width = 0;
        this.Visual.Height = 0;
        return new DrawingToolAction(StartAction: DrawingToolActionItem.Shape(finalShape), StopAction: DrawingToolActionItem.MouseCapture()).WithUndo();
    }

    #region Locked Aspect Ratio
    /// <summary>
    /// If user holds shift, return true, else false
    /// When locked Aspect Ratio is activated, the shape drawn on the canvas will have perfect perportions. e.g. perfect Rectangle / Ellipse
    /// </summary>
    private void CheckIfLockedAspectRatio() => LockedAspectRatio = Keyboard.Modifiers == ModifierKeys.Shift;
    public bool LockedAspectRatio { get; set; } = false;

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