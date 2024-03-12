using System.Windows;

namespace SnippingToolWPF.Control.SpecialUIElements;

internal class SliderTitleAndTextBox
{
    public static readonly DependencyProperty SliderHeaderProperty = DependencyProperty.RegisterAttached(
        "SliderHeader",
        typeof(string),
        typeof(SliderTitleAndTextBox), new PropertyMetadata("Header not set"));

    public static readonly DependencyProperty SliderValueInTextProperty = DependencyProperty.RegisterAttached(
        "SliderValueInText",
        typeof(string),
        typeof(SliderTitleAndTextBox), new PropertyMetadata("Value not set"));

    public static readonly DependencyProperty StaticTextBoxTextProperty = DependencyProperty.RegisterAttached(
        "StaticTextBoxText",
        typeof(string),
        typeof(SliderTitleAndTextBox), new PropertyMetadata("px"));

    public static string GetSliderHeader(DependencyObject target)
    {
        return (string)target.GetValue(SliderHeaderProperty);
    }

    public static void SetSliderHeader(DependencyObject target, string value)
    {
        target.SetValue(SliderHeaderProperty, value);
    }

    public static string GetSliderValueInText(DependencyObject target)
    {
        return (string)target.GetValue(SliderValueInTextProperty);
    }

    public static void SetSliderValueInText(DependencyObject target, string value)
    {
        target.SetValue(SliderValueInTextProperty, value);
    }

    public static string GetStaticTextBoxText(DependencyObject target)
    {
        return (string)target.GetValue(StaticTextBoxTextProperty);
    }

    public static void SetStaticTextBoxText(DependencyObject target, string value)
    {
        target.SetValue(StaticTextBoxTextProperty, value);
    }
}