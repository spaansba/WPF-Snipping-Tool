namespace SnippingToolWPF.Tools.ToolAction;

public readonly record struct DrawingCanvasAction(
    DrawingToolActionItem Action,
    bool IncludeInUndoStack = false)
{
   
}

