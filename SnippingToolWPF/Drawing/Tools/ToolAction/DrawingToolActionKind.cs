  using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SnippingToolWPF.Drawing.Tools;

[Flags]
public enum DrawingToolActionKind
{
    None = 0b_0000_0000,
    MouseCapture = 0b_0000_0001,
    KeyboardFocus = 0b_0000_0010,
    Shape = 0b_0000_0100,
}
