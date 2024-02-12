using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SnippingToolWPF
{
    /// <summary>
    /// Create generic GetValue and SetValue for DependencyObjects
    /// </summary>
    public static class DependencyObjectExtensions
    {
        [return: NotNullIfNotNull(nameof(specifiedDefault))]
        public static T? GetValue<T>(this DependencyObject obj, DependencyProperty property, T? specifiedDefault = default)
        {
            if (obj.GetValue(property) is T typed)
                return typed;
            return specifiedDefault;
        }

        [return: NotNullIfNotNull(nameof(value))]
        public static T SetValue<T>(this DependencyObject obj, DependencyProperty property, T value)
        {
            obj.SetValue(property, Boxes.Box(value));
            return value;
        }
    }
}
