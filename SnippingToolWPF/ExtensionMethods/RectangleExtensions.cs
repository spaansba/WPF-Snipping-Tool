using System.Windows;

namespace SnippingToolWPF.ExtensionMethods;

public static class RectangleExtensions
{
    public static void Deconstruct(this Rect rectangle,
        out double left, //x
        out double top, //y
        out double width,
        out double height)
    {
        left = rectangle.Location.X;
        top = rectangle.Location.Y;
        width = rectangle.Width;
        height = rectangle.Height;
    }
}