using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;

namespace SnippingToolWPF.Drawing.Tools;

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
        return new(
            StartAction: start,
            StopAction: stop,
            IncludeInUndoStack: this.IncludeInUndoStack && (start != default || stop != default));
    }
    #endregion

    #region Actions
    public static DrawingToolAction Start(DrawingToolActionItem item) => new DrawingToolAction(
        StartAction: item,
        StopAction: default);

    public static DrawingToolAction Stop(DrawingToolActionItem item) => new DrawingToolAction(
        StartAction: default,
        StopAction: item);

    public static DrawingToolAction StartMouseCapture() => Start(DrawingToolActionItem.MouseCapture());
    public static DrawingToolAction StopMouseCapture() => Stop(DrawingToolActionItem.MouseCapture());
    public static DrawingToolAction StartKeyboardFocus() => Start(DrawingToolActionItem.KeyboardFocus());
    public static DrawingToolAction StopKeyboardFocus() => Stop(DrawingToolActionItem.KeyboardFocus());
    public static DrawingToolAction AddShape(UIElement item) => Start(DrawingToolActionItem.Shape(item));
    public static DrawingToolAction RemoveShape(UIElement item) => Stop(DrawingToolActionItem.Shape(item));
    public static DrawingToolAction DoNothing => default;
    #endregion
}
