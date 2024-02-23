using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SnippingToolWPF.Drawing.Tools;

public sealed class UndoRedo
{
    public Stack<DrawingToolAction> VisibleActions = new Stack<DrawingToolAction>();
    private Stack<DrawingToolAction> RedoActions = new Stack<DrawingToolAction>();

    public bool TryUndo(out DrawingToolAction item)
    {
        // If there is an item on top of the stack, pop it and return true
        if (!this.VisibleActions.TryPop(out item))
            return false;

        this.RedoActions.Push(item);
        return true;
    }
    public bool TryRedo(out DrawingToolAction item)
    {
        // If there is an item on top of the stack, pop it and return true
        if (!this.RedoActions.TryPop(out item))
            return false;

        this.VisibleActions.Push(item);
        return true;
    }

    public void AddAction(DrawingToolAction action)
    {
        VisibleActions.Push(action);
        RedoActions.Clear(); //Everytime we add an action we clear the redo stack
    }
}
