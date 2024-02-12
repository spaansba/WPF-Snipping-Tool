using System.Windows;
using System.Windows.Controls;

namespace SnippingToolWPF.ExtensionMethods
{
    public static class PanelExtensions
    {
        public static TPanel AddChildren<TPanel>(this TPanel panel, IEnumerable<UIElement> children)
            where TPanel : Panel
        {
            foreach (var child in children) 
                panel.Children.Add(child);
            return panel;
        }

        public static TPanel AddChildren<TPanel>(this TPanel panel, params UIElement[] children)
            where TPanel : Panel
        {
            foreach (var child in children)
                panel.Children.Add(child);
            return panel;
        }


    }
}
