using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SnippingToolWPF
{
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
}
