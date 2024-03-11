using SnippingToolWPF.Tools.ToolAction;

namespace SnippingToolWPF.Tools;

public sealed class UndoRedo
{
    private readonly Stack<DrawingToolAction> visibleActions = new();
    private readonly Stack<DrawingToolAction> redoActions = new();

    public bool TryUndo(out DrawingToolAction item)
    {
        // If there is an item on top of the stack, pop it and return true
        if (!this.visibleActions.TryPop(out item))
            return false;

        this.redoActions.Push(item);
        return true;
    }
    public bool TryRedo(out DrawingToolAction item)
    {
        // If there is an item on top of the stack, pop it and return true
        if (!this.redoActions.TryPop(out item))
            return false;

        this.visibleActions.Push(item);
        return true;
    }

    public void AddAction(DrawingToolAction action)
    {
        visibleActions.Push(action);
        redoActions.Clear(); //Everytime we add an action we clear the redo stack
    }
}
