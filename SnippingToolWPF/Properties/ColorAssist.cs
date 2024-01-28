using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Drawing;

namespace SnippingToolWPF
{
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
                new FrameworkPropertyMetadata(defaultValue: new SolidColorBrush(Colors.Yellow)));

        public static Brush? GetHoverBackgroundFill(DependencyObject obj)
            => (Brush?)obj.GetValue(HoverBackgroundFillProperty);
        public static void SetHoverBackgroundFill(DependencyObject obj, Brush? value)
            => obj.SetValue(HoverBackgroundFillProperty, value);

        public static readonly DependencyProperty HoverBackgroundBorderProperty =
            DependencyProperty.RegisterAttached(
           "HoverBackgroundBorder",
           typeof(Brush),
           typeof(ColorAssist),
           new FrameworkPropertyMetadata(defaultValue: new SolidColorBrush(Colors.Black)));

        public static Brush? GetHoverBackgroundBorder(DependencyObject obj)
            => (Brush?)obj.GetValue(HoverBackgroundBorderProperty);
        public static void SetHoverBackgroundBorder(DependencyObject obj, Brush? value)
            => obj.SetValue(HoverBackgroundBorderProperty, value);

        #endregion

        #region IsPressed fill / border
        public static readonly DependencyProperty IsPressedBackgroundFillProperty =
            DependencyProperty.RegisterAttached(
        "IsPressedBackgroundFill",
        typeof(Brush),
        typeof(ColorAssist),
        new FrameworkPropertyMetadata(defaultValue: new SolidColorBrush(Colors.Green)));

        public static Brush? GetIsPressedBackgroundFill(DependencyObject obj)
            => (Brush?)obj.GetValue(IsPressedBackgroundFillProperty);
        public static void SetIsPressedBackgroundFill(DependencyObject obj, Brush? value)
            => obj.SetValue(IsPressedBackgroundFillProperty, value);

        public static readonly DependencyProperty IsPressedBackgroundBorderProperty =
        DependencyProperty.RegisterAttached(
           "IsPressedBackgroundBorder",
           typeof(Brush),
           typeof(ColorAssist),
           new FrameworkPropertyMetadata(defaultValue: new SolidColorBrush(Colors.Black)));

        public static Brush? GetIsPressedBackgroundBorder(DependencyObject obj)
            => (Brush?)obj.GetValue(IsPressedBackgroundBorderProperty);
        public static void SetIsPressedBackgroundBorder(DependencyObject obj, Brush? value)
            => obj.SetValue(IsPressedBackgroundBorderProperty, value);

        #endregion

        #region IsChecked fill / border
        public static readonly DependencyProperty IsCheckedBackgroundFillProperty =
        DependencyProperty.RegisterAttached(
        "IsCheckedBackgroundFill",
        typeof(Brush),
        typeof(ColorAssist),
        new FrameworkPropertyMetadata(defaultValue: new SolidColorBrush(Colors.Green)));

        public static Brush? GetIsCheckedBackgroundFill(DependencyObject obj)
            => (Brush?)obj.GetValue(IsCheckedBackgroundFillProperty);
        public static void SetIsCheckedBackgroundFill(DependencyObject obj, Brush? value)
            => obj.SetValue(IsCheckedBackgroundFillProperty, value);

        public static readonly DependencyProperty IsCheckedBackgroundBorderProperty =
        DependencyProperty.RegisterAttached(
           "IsCheckedBackgroundBorder",
           typeof(Brush),
           typeof(ColorAssist),
           new FrameworkPropertyMetadata(defaultValue: new SolidColorBrush(Colors.Black)));

        public static Brush? GetIsCheckedBackgroundBorder(DependencyObject obj)
            => (Brush?)obj.GetValue(IsCheckedBackgroundBorderProperty);
        public static void SetIsCheckedBackgroundBorder(DependencyObject obj, Brush? value)
            => obj.SetValue(IsCheckedBackgroundBorderProperty, value);

        #endregion

        #region IsEnabled fill / border
        public static readonly DependencyProperty IsEnabledBackgroundFillProperty =
        DependencyProperty.RegisterAttached(
        "IsEnabledBackgroundFill",
        typeof(Brush),
        typeof(ColorAssist),
        new FrameworkPropertyMetadata(defaultValue: new SolidColorBrush(Colors.Green)));

        public static Brush? GetIsEnabledBackgroundFill(DependencyObject obj)
            => (Brush?)obj.GetValue(IsEnabledBackgroundFillProperty);
        public static void SetIsEnabledBackgroundFill(DependencyObject obj, Brush? value)
            => obj.SetValue(IsEnabledBackgroundFillProperty, value);

        public static readonly DependencyProperty IsEnabledBackgroundBorderProperty =
        DependencyProperty.RegisterAttached(
           "IsEnabledBackgroundBorder",
           typeof(Brush),
           typeof(ColorAssist),
           new FrameworkPropertyMetadata(defaultValue: new SolidColorBrush(Colors.Black)));

        public static Brush? GetIsEnabledBackgroundBorder(DependencyObject obj)
            => (Brush?)obj.GetValue(IsEnabledBackgroundBorderProperty);
        public static void SetIsEnabledBackgroundBorder(DependencyObject obj, Brush? value)
            => obj.SetValue(IsEnabledBackgroundBorderProperty, value);

        #endregion
    }
}
