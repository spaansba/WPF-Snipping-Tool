using System.Diagnostics;
using System.Windows.Input;

namespace SnippingToolWPF.Common;
[DebuggerStepThrough]
public static class KeyboardHelper
{
    public static bool IsShiftOrCtrlPressed() => IsShiftPressed() || IsCtrlPressed();
    public static bool IsShiftPressed() => (Keyboard.Modifiers.HasFlag(ModifierKeys.Shift));
    public static bool IsCtrlPressed() => (Keyboard.Modifiers.HasFlag(ModifierKeys.Control));
    public static bool IsAltPressed() => (Keyboard.Modifiers.HasFlag(ModifierKeys.Alt));
    public static bool IsCtrlAndZPressed(KeyEventArgs e) => IsCtrlPressed() && e.Key == Key.Z;
    public static bool IsCtrlAndYPressed(KeyEventArgs e) => IsCtrlPressed() && e.Key == Key.Y;
    public static bool IsCtrlAndCPressed(KeyEventArgs e) => IsCtrlPressed() && e.Key == Key.C;
    public static bool IsCtrlAndVPressed(KeyEventArgs e) => IsCtrlPressed() && e.Key == Key.V;
    public static bool IsEscapePressed() => Keyboard.IsKeyDown(Key.Escape);
    public static bool IsDeletePressed() => Keyboard.IsKeyDown(Key.Delete);
}