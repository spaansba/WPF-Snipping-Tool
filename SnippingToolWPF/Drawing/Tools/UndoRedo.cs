using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnippingToolWPF.Drawing.Tools;

public sealed class UndoRedo<T>
{
    public Stack<T> VisibleStack = new Stack<T>();
    private Stack<T> RedoStack = new Stack<T>();
    private T? currentItem;

    public void Undo()
    {
        hif (VisibleStack.Count > 0)
        {
            currentItem = VisibleStack.Pop();
            RedoStack.Push(currentItem);
        }
    }

    public void Redo()
    {
        if (RedoStack.Count > 0)
        {
            currentItem = RedoStack.Pop();
            VisibleStack.Push(currentItem);
        }
    }

    public void AddItem(T item)
    {
        RedoStack.Clear();
        VisibleStack.Push(item);
    }

    // TODO: Add this to the clear canvas
    public void Reset()
    {
        VisibleStack.Clear();
        RedoStack.Clear();
    }

}
