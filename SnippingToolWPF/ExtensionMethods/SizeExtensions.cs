using System.Windows;

namespace SnippingToolWPF.ExtensionMethods;

public static class SizeExtensions
{
    public static void Deconstruct(this Size size, out double width, out double height)
    {
        width = size.Width;
        height = size.Height;
    }
}