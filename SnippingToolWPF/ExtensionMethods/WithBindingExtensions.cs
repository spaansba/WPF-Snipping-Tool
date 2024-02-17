using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace SnippingToolWPF
{
    public static class WithBindingExtensions
    {
        public static T WithBinding<T>(this T target,
            DependencyProperty targetProperty,
            PropertyPath path,
            object source,
            IValueConverter? converter = default,
            object? converterParameter = default) where T : DependencyObject
        {
            BindingOperations.SetBinding(target, targetProperty, new Binding()
            {
                Source = source,
                Converter = converter,
                Path = path,
                ConverterParameter = converterParameter
            });
            return target;
        }
    }
}
