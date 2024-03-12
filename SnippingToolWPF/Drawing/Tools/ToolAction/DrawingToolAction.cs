namespace SnippingToolWPF.Tools.ToolAction;

public readonly record struct DrawingToolAction(
    DrawingToolActionItem StartAction,
    DrawingToolActionItem StopAction,
    bool IncludeInUndoStack = false)
{
    #region Undo/Redo capabilities

    // Reverse it for undo / redo function
    public DrawingToolAction Reverse()
    {
        return this with
        {
            StartAction = StopAction,
            StopAction = StartAction
        };
    }

    // Extension method that changes the IncludeInUndoStack to True, this allows the action's Undoable flags to end up in the undo/redo stack
    public DrawingToolAction WithUndo()
    {
        return this with { IncludeInUndoStack = true };
    }

    // Strip down all flags from start/stop except the flags(actions) that can be undone/redone
    public DrawingToolAction OnlyPerformUndoable()
    {
        var start = StartAction.OnlyUndoableActions();
        var stop = StopAction.OnlyUndoableActions();
        return new DrawingToolAction(
            start,
            stop,
            IncludeInUndoStack && (start != default || stop != default));
    }

    #endregion

    #region Actions

    private static DrawingToolAction Start(DrawingToolActionItem item)
    {
        return new DrawingToolAction(
            item,
            default);
    }

    private static DrawingToolAction Stop(DrawingToolActionItem item)
    {
        return new DrawingToolAction(
            default,
            item);
    }

    public static DrawingToolAction StartMouseCapture()
    {
        return Start(DrawingToolActionItem.MouseCapture());
    }

    public static DrawingToolAction StopMouseCapture()
    {
        return Stop(DrawingToolActionItem.MouseCapture());
    }

    public static DrawingToolAction StartKeyboardFocus()
    {
        return Start(DrawingToolActionItem.KeyboardFocus());
    }

    public static DrawingToolAction StopKeyboardFocus()
    {
        return Stop(DrawingToolActionItem.KeyboardFocus());
    }

    public static DrawingToolAction AddShape(DrawingShape item)
    {
        return Start(DrawingToolActionItem.Shape(item));
    }

    public static DrawingToolAction RemoveShape(DrawingShape item)
    {
        return Stop(DrawingToolActionItem.Shape(item));
    }

    public static DrawingToolAction DoNothing => default;

    #endregion
}