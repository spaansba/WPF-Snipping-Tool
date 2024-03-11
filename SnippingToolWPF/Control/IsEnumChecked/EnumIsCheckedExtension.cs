using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Markup;

namespace SnippingToolWPF.Control.IsEnumChecked;

public class EnumIsCheckedExtension : MarkupExtension
{
    private const string Message =
        $"The property '{{0}}' on target '{{1}}' is not valid for a {nameof(EnumIsCheckedExtension)}. The {nameof(EnumIsCheckedExtension)} " +
        $"target must be a {nameof(ToggleButton)}, and the target property must be the {nameof(ToggleButton.IsChecked)} {nameof(DependencyProperty)}.";

    public EnumIsCheckedExtension()
    {
    }

    public EnumIsCheckedExtension(PropertyPath path)
    {
        Path = path;
    }

    public bool AlsoSetContent { get; set; }
    public Enum? Value { get; set; }
    public string? ElementName { get; set; }
    public RelativeSource? RelativeSource { get; set; }
    public object? Source { get; set; }
    public bool ValidatesOnDataErrors { get; set; }
    public bool ValidatesOnExceptions { get; set; }

    [ConstructorArgument("path")]
    public PropertyPath? Path { get; set; }

    [TypeConverter(typeof(CultureInfoIetfLanguageTagConverter))]
    public CultureInfo? ConverterCulture { get; set; }

    public override object? ProvideValue(IServiceProvider serviceProvider)
    {
        if (serviceProvider.GetService(typeof(IProvideValueTarget)) is not IProvideValueTarget valueProvider)
            return null;

        var bindingTarget = valueProvider.TargetObject as ToggleButton;
        var bindingProperty = valueProvider.TargetProperty as DependencyProperty;
        if (bindingProperty?.Name is not nameof(ToggleButton.IsChecked) || bindingTarget is null)
        {
            ThrowException(valueProvider.TargetProperty, valueProvider.TargetObject);
        }
        var binding = new Binding
        {
            Path = this.Path,
            Converter = EqualConverter.Instance,
            ConverterCulture = this.ConverterCulture,
            ConverterParameter = this.Value,
            Mode = BindingMode.Default,
            ValidatesOnDataErrors = this.ValidatesOnDataErrors,
            ValidatesOnExceptions = this.ValidatesOnExceptions,
        };
        if (this.ElementName is not null) binding.ElementName = this.ElementName;
        if (this.RelativeSource is not null) binding.RelativeSource = this.RelativeSource;
        if (this.Source is not null) binding.Source = this.Source;
        if (this.AlsoSetContent) bindingTarget.Content = this.Value;
        return SetBinding(bindingTarget, bindingProperty, binding);
    }

    [DoesNotReturn]
    private static void ThrowException(object? targetProperty, object? targetObject)
    {
        throw new NotSupportedException(string.Format(
            Message,
            targetProperty,
            targetObject
        ));
    }

    private static object? SetBinding(DependencyObject bindingTarget, DependencyProperty bindingTargetProperty, Binding binding)
    {
        BindingOperations.SetBinding(bindingTarget, bindingTargetProperty, binding);
        return bindingTarget.GetValue(bindingTargetProperty);
    }
}