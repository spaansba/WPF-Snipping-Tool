using System.Windows;
using System.Windows.Controls;

namespace SnippingToolWPF.WPFExtensions;

public static class UiElementExtensions
{
    public static T SetCanvasPosition<T>(this T element, double x, double y)
        where T : UIElement
    {
        Canvas.SetLeft(element, x);
        Canvas.SetTop(element, y);
        return element;
    }
}