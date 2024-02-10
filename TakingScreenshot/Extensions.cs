using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnippingTool.Interop;

internal static class Extensions
{
    public static WinFormsRect ToWinformsRect(this RECT rect)
    {
        return WinFormsRect.FromLTRB(rect.Left, rect.Top, rect.Right, rect.Bottom);
    }

    public static WpfRect ToWpfRect(this WinFormsRect rect)
    {
        return new WpfRect(rect.X, rect.Y, rect.Width, rect.Height);
    }

    public static WpfRect ToWpfRect(this RECT rect)
    {
        return new WpfRect(rect.Left, rect.Top, rect.Right - rect.Left, rect.Bottom - rect.Top);
    }
}
