using SnippingToolWPF.Drawing.Shapes;
using System.Windows;

namespace SnippingToolWPF.Drawing.Tools.ToolAction;

/// <summary>
/// Abstract class that implements IDrawingTool for classes that need draaging
/// if we want classes to have abstraction (reset) And have the implementation of IDrawingTool
/// </summary>
/// <typeparam name="T">An UIElement</typeparam>
public abstract class DraggingTool<T> : IDrawingTool where T : DrawingShape
{
    public abstract T? Visual { get; }

    public abstract bool LockedAspectRatio {  get; set; }

    public abstract bool IsDrawing {  get; set; }

    DrawingShape? IDrawingTool.Visual => Visual;

    public abstract DrawingToolAction LeftButtonDown(Point position, DrawingShape? item);
    public abstract void RightButtonDown();
    public abstract DrawingToolAction LeftButtonUp();

    public abstract DrawingToolAction MouseMove(Point position, DrawingShape? item);

    /// <summary>
    /// Reset the visual of the DrawingTool, this is the reason we needed this abstract class.
    /// </summary>
    public abstract void ResetVisual();
}
