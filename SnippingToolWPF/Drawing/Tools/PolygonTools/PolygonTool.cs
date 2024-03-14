using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using SnippingToolWPF.ExtensionMethods;
using SnippingToolWPF.SidePanel.ShapesSidePanel;
using SnippingToolWPF.Tools.ToolAction;

namespace SnippingToolWPF.Tools.PolygonTools;

public sealed class PolygonTool : DraggingTool<RegularPolygonDrawingShape>
{
    private readonly ShapesSidePanelViewModel options;
    private Point startPoint;
    
    public PolygonTool(ShapesSidePanelViewModel options)
    {
        this.options = options;
        
        // If there is a standard Rotation Angle, create a new DrawingShape. Otherwise use DraggingTool's DawingShape
        if (options.PolygonSelected is not null && options.PolygonSelected.RotationAngle > 0 )
            this.DrawingShape = new RegularPolygonDrawingShape(options.PolygonSelected.RotationAngle);
        
        //TODO: Fix WithBindingPathParts bugs so we can use it instead of WithBinding
        DrawingShape.WithBinding(
                RegularPolygonDrawingShape.NumberOfSidesProperty,
                new PropertyPath(
                    $"{nameof(ShapesSidePanelViewModel.PolygonSelected)}.{nameof(PremadePolygonInfo.NumberOfSides)}"),
                options)
            .WithBinding(
                SnippingToolWPF.DrawingShape.StrokeThicknessProperty,
                new PropertyPath($"{nameof(ShapesSidePanelViewModel.Thickness)}"),
                options)
            .WithBinding(
                RegularPolygonDrawingShape.StarInnerCircleSizeProperty,
                new PropertyPath(
                    $"{nameof(ShapesSidePanelViewModel.PolygonSelected)}.{nameof(PremadePolygonInfo.StarInnerCircleSize)}"),
                options)
            .WithBinding(
                SnippingToolWPF.DrawingShape.StrokeProperty,
                new PropertyPath($"{nameof(ShapesSidePanelViewModel.Stroke)}"),
                options);
        //TODO: Create Angle property and bind to drawingshape.Angle
        
        ResetVisual(); //Reset otherwise after switching polygon shapes there will be a small shape in the top left of the screen
    }

    public override bool IsDrawing { get; set; }

    public override void ResetVisual()
    {
        DrawingShape.Width = 0;
        DrawingShape.Height = 0;
    }

    #region Mouse Events

    public override DrawingToolAction LeftButtonDown(Point position, DrawingShape? item)
    {
        IsDrawing = true;
        startPoint = position;
        DrawingShape.Left = position.X;
        DrawingShape.Top = position.Y;
        DrawingShape.Stroke = options.Stroke;
        DrawingShape.StrokeDashCap = PenLineCap.Round;
        DrawingShape.StrokeStartLineCap = PenLineCap.Round;
        DrawingShape.StrokeEndLineCap = PenLineCap.Round;
        DrawingShape.StrokeLineJoin = PenLineJoin.Round;
        DrawingShape.StrokeThickness = options.Thickness;
        DrawingShape.Opacity = options.RealOpacity;
        DrawingShape.Fill = options.ShapeFill;
        DrawingShape.Tag = "For Testing";
        DrawingShape.Stretch = Stretch.Fill;
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
        DrawingShape.Width = Math.Abs(position.X - startPoint.X);
        DrawingShape.Height = Math.Abs(position.Y - startPoint.Y);

        // Set the new position of the polygon (we do this because otherwise a polygon can only be drawn to bottom right and not in any direction)
        DrawingShape.Left = Math.Min(startPoint.X, position.X);
        DrawingShape.Top = Math.Min(startPoint.Y, position.Y);
        return DrawingToolAction.DoNothing;
    }

    public override DrawingToolAction LeftButtonUp()
    {
        IsDrawing = false;
        var finalPolygon = DrawingShape.Clone(new Size(DrawingShape.Width, DrawingShape.Height));
        ResetVisual();
        finalPolygon.Stretch = Stretch.Fill;
        return new DrawingToolAction(DrawingToolActionItem.Shape(finalPolygon), DrawingToolActionItem.MouseCapture())
            .WithUndo();
    }

    /// <summary>
    ///     Resets the visual if drawing and pressing right mouse button
    /// </summary>
    public override void RightButtonDown()
    {
        if (!IsDrawing) return;

        ResetVisual();
        IsDrawing = false;
    }

    #endregion

    #region Locked Aspect Ratio

    /// <summary>
    ///     If user holds shift, return true, else false
    ///     When locked Aspect Ratio is activated, the polygon drawn on the canvas will have perfect perportions. e.g. perfect
    ///     Rectangle / triangle
    /// </summary>
    private void CheckIfLockedAspectRatio()
    {
        LockedAspectRatio = Keyboard.Modifiers == ModifierKeys.Shift;
    }

    public override bool LockedAspectRatio { get; set; }

    private Point GetLockedAspectRatioEndPoint(Point location)
    {
        var dx = location.X - startPoint.X;
        var dy = location.Y - startPoint.Y;
        var max = Math.Max(Math.Abs(dx), Math.Abs(dy));
        var x = startPoint.X + Math.Sign(dx) * max;
        var y = startPoint.Y + Math.Sign(dy) * max;
        return new Point(x, y);
    }

    #endregion
}