using System.Windows;
using SnippingToolWPF.Tools.ToolAction;

namespace SnippingToolWPF.Tools.PenTools;

public sealed class EraserTool : IDrawingTool
{
    public DrawingShape? DrawingShape => null;

    public bool LockedAspectRatio => throw new NotImplementedException();

    public bool IsDrawing { get; private set; }

    #region Mouse Events

    public DrawingToolAction LeftButtonDown(Point position, DrawingShape? item)
    {
        IsDrawing = true;
        return item is not null ? DrawingToolAction.RemoveShape(item).WithUndo() : DrawingToolAction.DoNothing;
    }

    public DrawingToolAction MouseMove(Point position, DrawingShape? item)
    {
        if (!IsDrawing)
            return DrawingToolAction.DoNothing;
        if (item is not null)
            return DrawingToolAction.RemoveShape(item).WithUndo();
        return DrawingToolAction.DoNothing;
    }

    public DrawingToolAction LeftButtonUp()
    {
        IsDrawing = false;
        return DrawingToolAction.StopMouseCapture();
    }

    public void RightButtonDown()
    {
    }

    #endregion
}