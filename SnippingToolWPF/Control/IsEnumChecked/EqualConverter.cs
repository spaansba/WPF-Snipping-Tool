﻿using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace SnippingToolWPF.Control.IsEnumChecked;

public class EqualConverter : MarkupExtension, IValueConverter
{
    public static EqualConverter Instance { get; } = new EqualConverter();

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value?.Equals(parameter);
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value?.Equals(true) == true ? parameter : Binding.DoNothing;
    }

    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return Instance;
    }
}