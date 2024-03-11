using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using SnippingToolWPF.ExtensionMethods;
using SnippingToolWPF.SidePanel.ShapesSidePanel;
using SnippingToolWPF.Tools.ToolAction;

namespace SnippingToolWPF.Tools.PolygonTools;

public sealed class PolygonTool : DraggingTool<DrawingShape>
{
    private readonly ShapesSidePanelViewModel options;
    public override bool IsDrawing { get; set; }
    public override DrawingShape DrawingShape { get; }

    private Point startPoint;

    public PolygonTool(ShapesSidePanelViewModel options)
    {
        this.options = options;

        //TODO: Fix WithBindingPathParts bugs so we can use it instead of WithBinding
        this.DrawingShape = new RegularPolygonDrawingShape()
        .WithBinding(
        RegularPolygonDrawingShape.NumberOfSidesProperty,
        new($"{nameof(ShapesSidePanelViewModel.PolygonSelected)}.{nameof(PremadePolygonInfo.NumberOfSides)}"),
        options)
        .WithBinding(
            DrawingShape.AngleProperty,
        new($"{nameof(ShapesSidePanelViewModel.PolygonSelected)}.{nameof(PremadePolygonInfo.RotationAngle)}"),
        options)
        .WithBinding(
            DrawingShape.StrokeThicknessProperty,
        new($"{nameof(ShapesSidePanelViewModel.Thickness)}"),
        options);
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
        this.DrawingShape.Left = position.X;
        this.DrawingShape.Top = position.Y;
        this.DrawingShape.Stroke = options.ShapeStroke;
        this.DrawingShape.StrokeDashCap = PenLineCap.Round;
        this.DrawingShape.StrokeStartLineCap = PenLineCap.Round;
        this.DrawingShape.StrokeEndLineCap = PenLineCap.Round;
        this.DrawingShape.StrokeLineJoin = PenLineJoin.Round;
        this.DrawingShape.StrokeThickness = options.Thickness;
        this.DrawingShape.Opacity = options.RealOpacity;
        this.DrawingShape.Fill = options.ShapeFill;
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
        this.DrawingShape.Left = Math.Min(startPoint.X, position.X);
        this.DrawingShape.Top = Math.Min(startPoint.Y, position.Y);
        return DrawingToolAction.DoNothing;  
    }
    public override DrawingToolAction LeftButtonUp()
    {
        IsDrawing = false;
        var finalPolygon = this.DrawingShape.Clone(new(this.DrawingShape.Width, this.DrawingShape.Height));
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
    public override bool LockedAspectRatio { get; set; }

    public Point GetLockedAspectRatioEndPoint(Point location)
    {
        var dx = location.X - startPoint.X;
        var dy = location.Y - startPoint.Y;
        var max = Math.Max(Math.Abs(dx), Math.Abs(dy));
        var x = startPoint.X + Math.Sign(dx) * max;
        var y = startPoint.Y + Math.Sign(dy) * max;
        return new(x, y);
    }
    #endregion
}