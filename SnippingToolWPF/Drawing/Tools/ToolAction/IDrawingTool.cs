using System.Windows;

namespace SnippingToolWPF.Tools.ToolAction;

/// <summary>
///     Handles input events for drawing DrawingShapes on the canvas
/// </summary>
public interface IDrawingTool
{
    public DrawingShape? DrawingShape { get; }

    public bool
        LockedAspectRatio
    {
        get;
    } // Implement that if user holds shift while moving the mouse that aspect ratio stays perfect

    public bool IsDrawing { get; }
    public DrawingToolAction LeftButtonDown(Point position, DrawingShape? item);
    public void RightButtonDown();
    public DrawingToolAction MouseMove(Point position, DrawingShape? item);
    public DrawingToolAction LeftButtonUp();
}

public interface IDrawingTool<out T> : IDrawingTool
    where T : DrawingShape
{
    public new T? DrawingShape { get; }
    DrawingShape? IDrawingTool.DrawingShape => DrawingShape;
}