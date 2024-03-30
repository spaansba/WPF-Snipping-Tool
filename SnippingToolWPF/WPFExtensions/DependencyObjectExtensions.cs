using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using SnippingToolWPF.Common;

namespace SnippingToolWPF.WPFExtensions;

/// <summary>
///     Create generic GetValue and SetValue for DependencyObjects
/// </summary>
[DebuggerStepThrough]
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
    [DebuggerStepThrough]
    public static T SetValue<T>(this DependencyObject obj, DependencyProperty property, T value)
    {
        obj.SetValue(property, Boxes.Box(value));
        return value;
    }
}