using System.Diagnostics.CodeAnalysis;

namespace SnippingToolWPF.Tools.ToolAction;

/// <summary>
/// Each action we take, most of the time we have a start and stop DrawingToolActionItem
/// </summary>
public readonly record struct DrawingToolActionItem
{
    // Const with kinds that are undoable in Undo / Redo, all other kinds will be ignored
    private const DrawingToolActionKind UndoableKinds = DrawingToolActionKind.Shape;

    /// <summary>
    /// If withUndo remove all other flags from kind except UndoableKinds
    /// </summary>
    /// <returns>DrawingToolActionItem with only undoable kinds as flags</returns>
    public DrawingToolActionItem OnlyUndoableActions()
    {
        return this with { Kind = this.Kind & UndoableKinds };
    }

    public DrawingToolActionKind Kind { get; private init; }

    [MemberNotNullWhen(true, nameof(Item))]
    public bool IsShape => Kind.HasFlag(DrawingToolActionKind.Shape);
    public bool IsMouseCapture => Kind.HasFlag(DrawingToolActionKind.MouseCapture);
    public bool IsKeyboardFocus => Kind.HasFlag(DrawingToolActionKind.KeyboardFocus);

    public static DrawingToolActionItem MouseCapture() => new() { Kind = DrawingToolActionKind.MouseCapture };
    public static DrawingToolActionItem KeyboardFocus() => new() { Kind = DrawingToolActionKind.KeyboardFocus };

    public DrawingShape? Item { get; private init; }
    public static DrawingToolActionItem Shape(DrawingShape item)
    {
        ArgumentNullException.ThrowIfNull(item); // Make sure Shape is initiated with an item
        return new() { Kind = DrawingToolActionKind.Shape, Item = item };
    }

    /// Combine methods are the same but for difference contexts
    public static DrawingToolActionItem Combine(DrawingToolActionItem a, DrawingToolActionItem b)
    {
        return new()
        {
            Kind = a.Kind | b.Kind,
            Item = a.Item ?? b.Item,
        };
    }
    public DrawingToolActionItem Combine(DrawingToolActionItem other) => Combine(this, other);

}
