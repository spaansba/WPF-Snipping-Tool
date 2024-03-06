using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SnippingToolWPF.Drawing.Tools.ToolAction;

/// <summary>
/// Abstract class that implements IDrawingTool for classes that need draaging
/// if we want classes to have abstraction (reset) And have the implementation of IDrawingTool
/// </summary>
/// <typeparam name="T">An UIElement</typeparam>
public abstract class DraggingTool<T> : IDrawingTool where T : UIElement
{
    public abstract T? Visual { get; }

    public abstract bool LockedAspectRatio {  get; set; }

    public abstract bool IsDrawing {  get; set; }

    UIElement? IDrawingTool.Visual => Visual;

    public abstract DrawingToolAction LeftButtonDown(Point position, UIElement? item);
    public abstract void RightButtonDown();
    public abstract DrawingToolAction LeftButtonUp();

    public abstract DrawingToolAction MouseMove(Point position, UIElement? item);

    /// <summary>
    /// Reset the visual of the DrawingTool, this is the reason we needed this abstract class.
    /// </summary>
    public abstract void ResetVisual();
}
