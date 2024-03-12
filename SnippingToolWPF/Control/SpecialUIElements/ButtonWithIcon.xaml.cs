using System.Windows;
using System.Windows.Controls;

namespace SnippingToolWPF.Control.SpecialUIElements;

public static class ButtonWithIcon
{
    public static readonly DependencyProperty IconLocationProperty = DependencyProperty.RegisterAttached(
        "IconLocation",
        typeof(Dock),
        typeof(ButtonWithIcon),
        new FrameworkPropertyMetadata(default(Dock)));

    public static readonly DependencyProperty IconProperty = DependencyProperty.RegisterAttached(
        "Icon",
        typeof(object),
        typeof(ButtonWithIcon),
        new FrameworkPropertyMetadata());

    public static readonly DependencyProperty IconTemplateProperty = DependencyProperty.RegisterAttached(
        "IconTemplate",
        typeof(DataTemplate),
        typeof(ButtonWithIcon),
        new FrameworkPropertyMetadata());

    public static readonly DependencyProperty IconTemplateSelectorProperty = DependencyProperty.RegisterAttached(
        "IconTemplateSelector",
        typeof(DataTemplateSelector),
        typeof(ButtonWithIcon),
        new FrameworkPropertyMetadata());

    public static Dock GetIconLocation(DependencyObject target)
    {
        return (Dock)target.GetValue(IconLocationProperty);
    }

    public static void SetIconLocation(DependencyObject target, Dock value)
    {
        target.SetValue(IconLocationProperty, value);
    }

    public static object? GetIcon(DependencyObject target)
    {
        return target.GetValue(IconProperty);
    }

    public static void SetIcon(DependencyObject target, object? value)
    {
        target.SetValue(IconProperty, value);
    }

    public static DataTemplate? GetIconTemplate(DependencyObject target)
    {
        return (DataTemplate?)target.GetValue(IconTemplateProperty);
    }

    public static void SetIconTemplate(DependencyObject target, DataTemplate? value)
    {
        target.SetValue(IconTemplateProperty, value);
    }

    public static DataTemplateSelector? GetIconTemplateSelector(DependencyObject target)
    {
        return (DataTemplateSelector?)target.GetValue(IconTemplateSelectorProperty);
    }

    public static void SetIconTemplateSelector(DependencyObject target, DataTemplateSelector? value)
    {
        target.SetValue(IconTemplateSelectorProperty, value);
    }
}