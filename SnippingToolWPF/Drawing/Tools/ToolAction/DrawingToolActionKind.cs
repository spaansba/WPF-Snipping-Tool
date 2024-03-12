namespace SnippingToolWPF.Tools.ToolAction;

[Flags]
public enum DrawingToolActionKind
{
    None = 0b_0000_0000,
    MouseCapture = 0b_0000_0001,
    KeyboardFocus = 0b_0000_0010,
    Shape = 0b_0000_0100
}