using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Text.RegularExpressions;
using System.Windows.Input;

namespace SnippingToolWPF.Control;

/// <summary>
/// Creates a custom textbox that only allows numeric characters, with the option of signed (negatives) or unsigned integers / floats
/// </summary>
public static class NumericTextBox
{
    #region Dependency Property for Allowing Negatives
    public static readonly DependencyProperty AllowNegativeProperty = DependencyProperty.RegisterAttached(
        "AllowNegative",
        typeof(bool),
        typeof(NumericTextBox),
        new FrameworkPropertyMetadata(true));

    public static bool GetAllowNegative(DependencyObject target)
        => (bool)target.GetValue(AllowNegativeProperty);

    public static void SetAllowNegative(DependencyObject target, bool value)
        => target.SetValue(AllowNegativeProperty, value);
    #endregion

    #region Dependency Property for Allowing floating points
    public static readonly DependencyProperty FloatingPointProperty = DependencyProperty.RegisterAttached(
        "FloatingPoint",
        typeof(bool),
        typeof(NumericTextBox),
        new FrameworkPropertyMetadata(true));

    public static bool GetFloatingPoint(DependencyObject target)
        => (bool)target.GetValue(FloatingPointProperty);

    public static void SetFloatingPoint(DependencyObject target, bool value)
        => target.SetValue(FloatingPointProperty, value);
    #endregion
}
