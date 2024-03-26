using System.Windows;

namespace SnippingToolWPF.ExtensionMethods;

public static class UiElementExtensions
{
    /// <summary>
    /// Returns the Measure Size
    /// Measure lets me tell my parent how much space I want, given a constraint
    /// </summary>
    public static Size MeasureAndReturn(this UIElement? obj, Size constraint)
    {
        if (obj is null)
            return new Size();
        obj.Measure(constraint);
        return obj.DesiredSize;
    }
}