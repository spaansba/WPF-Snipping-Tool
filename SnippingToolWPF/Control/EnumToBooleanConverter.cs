﻿using System.Globalization;
using System.Windows.Data;

namespace SnippingToolWPF.Control;

public class EnumToBooleanConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value == null || parameter == null)
            return false;

        return value.Equals(parameter);
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value == null || parameter == null || !(value is bool))
            return null;

        return (bool)value ? parameter : Binding.DoNothing;
    }
}
