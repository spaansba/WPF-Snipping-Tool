namespace SnippingToolWPF.Tools.ToolAction;

public readonly record struct DrawingToolAction(DrawingToolActionItem StartAction, DrawingToolActionItem StopAction, bool IncludeInUndoStack = false)
{
    #region Undo/Redo capabilities 

    // Reverse it for undo / redo function
    public DrawingToolAction Reverse() => this with
    {
        StartAction = this.StopAction,
        StopAction = this.StartAction,
    };

    // Extension method that changes the IncludeInUndoStack to True, this allows the action's Undoable flags to end up in the undo/redo stack
    public DrawingToolAction WithUndo() => this with { IncludeInUndoStack = true };

    // Strip down all flags from start/stop except the flags(actions) that can be undone/redone
    public DrawingToolAction OnlyPerformUndoable()
    {
        var start = this.StartAction.OnlyUndoableActions();
        var stop = this.StopAction.OnlyUndoableActions();
        return new DrawingToolAction(
            StartAction: start,
            StopAction: stop,
            IncludeInUndoStack: this.IncludeInUndoStack && (start != default || stop != default));
    }
    #endregion

    #region Actions

    private static DrawingToolAction Start(DrawingToolActionItem item) => new(
        StartAction: item,
        StopAction: default);

    private static DrawingToolAction Stop(DrawingToolActionItem item) => new(
        StartAction: default,
        StopAction: item);

    public static DrawingToolAction StartMouseCapture() => Start(DrawingToolActionItem.MouseCapture());
    public static DrawingToolAction StopMouseCapture() => Stop(DrawingToolActionItem.MouseCapture());
    public static DrawingToolAction StartKeyboardFocus() => Start(DrawingToolActionItem.KeyboardFocus());
    public static DrawingToolAction StopKeyboardFocus() => Stop(DrawingToolActionItem.KeyboardFocus());
    public static DrawingToolAction AddShape(DrawingShape item) => Start(DrawingToolActionItem.Shape(item));
    public static DrawingToolAction RemoveShape(DrawingShape item) => Stop(DrawingToolActionItem.Shape(item));
    public static DrawingToolAction DoNothing => default;
    #endregion
}
