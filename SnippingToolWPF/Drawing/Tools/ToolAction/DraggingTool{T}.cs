using System.Windows;

namespace SnippingToolWPF.Tools.ToolAction;

/// <summary>
///     Abstract class that implements IDrawingTool for classes that need draaging
///     if we want classes to have abstraction (reset) And have the implementation of IDrawingTool
/// </summary>
/// <typeparam name="T">An UIElement</typeparam>
public abstract class DraggingTool<T> : IDrawingTool where T : DrawingShape, new()
{
    /// <summary>
    /// Create a generic drawingshape
    /// </summary>
    protected T DrawingShape { get; init; } = new T();

    public abstract bool LockedAspectRatio { get; set; }

    public abstract bool IsDrawing { get; set; }

    DrawingShape IDrawingTool.DrawingShape => DrawingShape;

    public abstract DrawingToolAction OnDragStarted(Point startPosition, DrawingShape? item);
    public abstract void RightButtonDown();
    public abstract DrawingToolAction OnDragContinued(Point position, DrawingShape? item);
    public abstract DrawingToolAction OnDragFinished();

    /// <summary>
    ///  Reset the visual of the DrawingTool, this is the reason we needed this abstract class.
    /// </summary>
    public abstract void ResetVisual();
}