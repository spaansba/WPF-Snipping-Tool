using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;

namespace SnippingToolWPF.Drawing.Tools
{
    public readonly record struct DrawingToolAction(DrawingToolActionItem StartAction, DrawingToolActionItem StopAction)
    {
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
    }
}
