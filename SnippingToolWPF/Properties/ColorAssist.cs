using System.Windows;
using System.Windows.Media;

namespace SnippingToolWPF.Properties;

//https://learn.microsoft.com/en-us/dotnet/api/system.windows.frameworkpropertymetadataoptions?view=windowsdesktop-8.0&viewFallbackFrom=net-8.0
//https://learn.microsoft.com/en-us/dotnet/api/system.windows.frameworkpropertymetadataoptions?view=windowsdesktop-8.0&viewFallbackFrom=net-8.0
public static class ColorAssist
{
    #region Hover Background fill / border

    public static readonly DependencyProperty HoverBackgroundFillProperty =
        DependencyProperty.RegisterAttached(
            "HoverBackgroundFill",
            typeof(Brush),
            typeof(ColorAssist),
            new FrameworkPropertyMetadata(new SolidColorBrush(Colors.Yellow)));

    public static Brush? GetHoverBackgroundFill(DependencyObject obj)
    {
        return (Brush?)obj.GetValue(HoverBackgroundFillProperty);
    }

    public static void SetHoverBackgroundFill(DependencyObject obj, Brush? value)
    {
        obj.SetValue(HoverBackgroundFillProperty, value);
    }

    public static readonly DependencyProperty HoverBackgroundBorderProperty =
        DependencyProperty.RegisterAttached(
            "HoverBackgroundBorder",
            typeof(Brush),
            typeof(ColorAssist),
            new FrameworkPropertyMetadata(new SolidColorBrush(Colors.Black)));

    public static Brush? GetHoverBackgroundBorder(DependencyObject obj)
    {
        return (Brush?)obj.GetValue(HoverBackgroundBorderProperty);
    }

    public static void SetHoverBackgroundBorder(DependencyObject obj, Brush? value)
    {
        obj.SetValue(HoverBackgroundBorderProperty, value);
    }

    #endregion

    #region IsPressed fill / border

    public static readonly DependencyProperty IsPressedBackgroundFillProperty =
        DependencyProperty.RegisterAttached(
            "IsPressedBackgroundFill",
            typeof(Brush),
            typeof(ColorAssist),
            new FrameworkPropertyMetadata(
                new SolidColorBrush(Color.FromArgb(0xFF, 0xD4, 0xD9, 0xE1)))); //TODO: make this bind to a color in xaml

    public static Brush? GetIsPressedBackgroundFill(DependencyObject obj)
    {
        return (Brush?)obj.GetValue(IsPressedBackgroundFillProperty);
    }

    public static void SetIsPressedBackgroundFill(DependencyObject obj, Brush? value)
    {
        obj.SetValue(IsPressedBackgroundFillProperty, value);
    }

    public static readonly DependencyProperty IsPressedBackgroundBorderProperty =
        DependencyProperty.RegisterAttached(
            "IsPressedBackgroundBorder",
            typeof(Brush),
            typeof(ColorAssist),
            new FrameworkPropertyMetadata(new SolidColorBrush(Colors.Black)));

    public static Brush? GetIsPressedBackgroundBorder(DependencyObject obj)
    {
        return (Brush?)obj.GetValue(IsPressedBackgroundBorderProperty);
    }

    public static void SetIsPressedBackgroundBorder(DependencyObject obj, Brush? value)
    {
        obj.SetValue(IsPressedBackgroundBorderProperty, value);
    }

    #endregion

    #region IsChecked fill / border

    public static readonly DependencyProperty IsCheckedBackgroundFillProperty =
        DependencyProperty.RegisterAttached(
            "IsCheckedBackgroundFill",
            typeof(Brush),
            typeof(ColorAssist),
            new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromArgb(0xFF, 0x94, 0x94, 0x93))));

    public static Brush? GetIsCheckedBackgroundFill(DependencyObject obj)
    {
        return (Brush?)obj.GetValue(IsCheckedBackgroundFillProperty);
    }

    public static void SetIsCheckedBackgroundFill(DependencyObject obj, Brush? value)
    {
        obj.SetValue(IsCheckedBackgroundFillProperty, value);
    }

    public static readonly DependencyProperty IsCheckedBackgroundBorderProperty =
        DependencyProperty.RegisterAttached(
            "IsCheckedBackgroundBorder",
            typeof(Brush),
            typeof(ColorAssist),
            new FrameworkPropertyMetadata(new SolidColorBrush(Colors.Black)));

    public static Brush? GetIsCheckedBackgroundBorder(DependencyObject obj)
    {
        return (Brush?)obj.GetValue(IsCheckedBackgroundBorderProperty);
    }

    public static void SetIsCheckedBackgroundBorder(DependencyObject obj, Brush? value)
    {
        obj.SetValue(IsCheckedBackgroundBorderProperty, value);
    }

    #endregion

    #region IsEnabled fill / border

    public static readonly DependencyProperty IsEnabledBackgroundFillProperty =
        DependencyProperty.RegisterAttached(
            "IsEnabledBackgroundFill",
            typeof(Brush),
            typeof(ColorAssist),
            new FrameworkPropertyMetadata(new SolidColorBrush(Colors.Green)));

    public static Brush? GetIsEnabledBackgroundFill(DependencyObject obj)
    {
        return (Brush?)obj.GetValue(IsEnabledBackgroundFillProperty);
    }

    public static void SetIsEnabledBackgroundFill(DependencyObject obj, Brush? value)
    {
        obj.SetValue(IsEnabledBackgroundFillProperty, value);
    }

    public static readonly DependencyProperty IsEnabledBackgroundBorderProperty =
        DependencyProperty.RegisterAttached(
            "IsEnabledBackgroundBorder",
            typeof(Brush),
            typeof(ColorAssist),
            new FrameworkPropertyMetadata(new SolidColorBrush(Colors.Black)));

    public static Brush? GetIsEnabledBackgroundBorder(DependencyObject obj)
    {
        return (Brush?)obj.GetValue(IsEnabledBackgroundBorderProperty);
    }

    public static void SetIsEnabledBackgroundBorder(DependencyObject obj, Brush? value)
    {
        obj.SetValue(IsEnabledBackgroundBorderProperty, value);
    }

    #endregion
}