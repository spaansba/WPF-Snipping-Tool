using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SnippingToolWPF.ExtensionMethods
{
    public static class RectangleExtensions
    {
        public static void Deconstruct(this Rect rectangle,
            out double x,
            out double y,
            out double width,
            out double height)
        {
            x = rectangle.Location.X;
            y = rectangle.Location.Y;
            width = rectangle.Width;
            height = rectangle.Height;
        }
    }
}
