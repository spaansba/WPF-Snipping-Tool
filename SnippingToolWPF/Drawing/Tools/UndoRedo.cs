using SnippingToolWPF.Tools.ToolAction;

namespace SnippingToolWPF.Tools;

public sealed class UndoRedo
{
    private readonly Stack<DrawingToolAction> redoActions = new Stack<DrawingToolAction>();
    private readonly Stack<DrawingToolAction> visibleActions = new Stack<DrawingToolAction>();

    public bool TryUndo(out DrawingToolAction item)
    {
        // If there is an item on top of the stack, pop it and return true
        if (!visibleActions.TryPop(out item))
            return false;

        redoActions.Push(item);
        return true;
    }

    public bool TryRedo(out DrawingToolAction item)
    {
        // If there is an item on top of the stack, pop it and return true
        if (!redoActions.TryPop(out item))
            return false;

        visibleActions.Push(item);
        return true;
    }

    public void AddAction(DrawingToolAction action)
    {
        visibleActions.Push(action);
        redoActions.Clear(); //Everytime we add an action we clear the redo stack
    }
}