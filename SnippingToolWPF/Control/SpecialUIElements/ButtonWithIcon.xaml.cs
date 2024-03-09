using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace SnippingToolWPF;

public static class ButtonWithIcon
{
    public static readonly DependencyProperty IconLocationProperty = DependencyProperty.RegisterAttached( 
    name: "IconLocation",
    propertyType: typeof(Dock), 
    ownerType: typeof(ButtonWithIcon),
    defaultMetadata: new FrameworkPropertyMetadata(default(Dock)));

    public static Dock GetIconLocation(DependencyObject target) => (Dock)target.GetValue(IconLocationProperty);
    public static void SetIconLocation(DependencyObject target, Dock value) => target.SetValue(IconLocationProperty, value);

    public static readonly DependencyProperty IconProperty = DependencyProperty.RegisterAttached(
    name: "Icon",
    propertyType: typeof(object),
    ownerType: typeof(ButtonWithIcon),
    defaultMetadata: new FrameworkPropertyMetadata());

    public static object? GetIcon(DependencyObject target) => target.GetValue(IconProperty);
    public static void SetIcon(DependencyObject target, object? value) => target.SetValue(IconProperty, value);

    public static readonly DependencyProperty IconTemplateProperty = DependencyProperty.RegisterAttached(
    name: "IconTemplate",
    propertyType: typeof(DataTemplate),
    ownerType: typeof(ButtonWithIcon),
    defaultMetadata: new FrameworkPropertyMetadata());

    public static DataTemplate? GetIconTemplate(DependencyObject target) => (DataTemplate?)target.GetValue(IconTemplateProperty);
    public static void SetIconTemplate(DependencyObject target, DataTemplate? value) => target.SetValue(IconTemplateProperty, value);

    public static readonly DependencyProperty IconTemplateSelectorProperty = DependencyProperty.RegisterAttached(
    name: "IconTemplateSelector",
    propertyType: typeof(DataTemplateSelector),
    ownerType: typeof(ButtonWithIcon),
    defaultMetadata: new FrameworkPropertyMetadata());

    public static DataTemplateSelector? GetIconTemplateSelector(DependencyObject target) => (DataTemplateSelector?)target.GetValue(IconTemplateSelectorProperty);

    public static void SetIconTemplateSelector(DependencyObject target, DataTemplateSelector? value) => target.SetValue(IconTemplateSelectorProperty, value);
}
