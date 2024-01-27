using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using FontAwesome6;
using FontAwesome6.Svg;

namespace SnippingToolWPF.Control;

[MarkupExtensionReturnType(typeof(SvgAwesome))]
public sealed class FontAwesomeExtension : MarkupExtension
{
    public FontAwesomeExtension()
    {
    }

    public FontAwesomeExtension(EFontAwesomeIcon icon)
    {
        Icon = icon;
    }

    public FontAwesomeExtension(EFontAwesomeIcon icon, double height)
    {
        Icon = icon;
        Height = height;
    }

    public FontAwesomeExtension(EFontAwesomeIcon icon, double height, double width)
    {
        Icon = icon;
        Height = height;
        Width = width;
    }

    public FontAwesomeExtension(EFontAwesomeIcon icon, double height, double width, Stretch stretch)
    {
        Icon = icon;
        Height = height;
        Width = width;
        Stretch = stretch;
    }

    [ConstructorArgument("icon")]
    public EFontAwesomeIcon Icon { get; set; } = EFontAwesomeIcon.None;

    [ConstructorArgument("stretch")]
    public Stretch Stretch { get; set; } = Stretch.Uniform;

    [ConstructorArgument("height")]
    public double Height { get; set; } = double.NaN;

    [ConstructorArgument("width")]
    public double Width { get; set; } = double.NaN;

    public Brush? PrimaryColor { get; set; }

    public object? PrimaryColorKey { get; set; }

    public Brush? SecondaryColor { get; set; }

    public object? SecondaryColorKey { get; set; }

    public bool Spin { get; set; }
    public double SpinDuration { get; set; } = 1.0;
    public bool Pulse { get; set; }
    public double PulseDuration { get; set; } = 1.0;
    public double Rotation { get; set; }
    public EFlipOrientation FlipOrientation { get; set; } = EFlipOrientation.Normal;
    public double? PrimaryOpacity { get; set; }
    public double? SecondaryOpacity { get; set; }
    public bool? SwapOpacity { get; set; }
    public HorizontalAlignment HorizontalAlignment { get; set; } = HorizontalAlignment.Stretch;
    public VerticalAlignment VerticalAlignment { get; set; } = VerticalAlignment.Stretch;
    public Thickness Margin { get; set; }




    public StretchDirection StretchDirection { get; set; } = StretchDirection.Both;


    private static Brush? GetBrush(FrameworkElement? resourceLocator, object? resourceKey)
        => resourceKey is null ? null : resourceLocator?.TryFindResource(resourceKey) as Brush;


    public override object ProvideValue(IServiceProvider? serviceProvider)
    {
        var resourceLocator = serviceProvider?.GetService<IProvideValueTarget>()?.TargetObject as FrameworkElement;
        PrimaryColor ??= GetBrush(resourceLocator, PrimaryColorKey) ?? FontAwesomeDefaults.PrimaryColor;
        SecondaryColor ??= GetBrush(resourceLocator, SecondaryColorKey) ?? FontAwesomeDefaults.SecondaryColor;
        return new SvgAwesome
        {
            PrimaryColor = PrimaryColor.CloneIfNotFrozen(),
            SecondaryColor = SecondaryColor.CloneIfNotFrozen(),
            Icon = Icon,
            Spin = Spin,
            SpinDuration = SpinDuration,
            Pulse = Pulse,
            PulseDuration = PulseDuration,
            Rotation = Rotation,
            FlipOrientation = FlipOrientation,
            PrimaryOpacity = PrimaryOpacity,
            SecondaryOpacity = SecondaryOpacity,
            SwapOpacity = SwapOpacity,
            Stretch = Stretch,
            StretchDirection = StretchDirection,
            Height = Height,
            Width = Width,
            VerticalAlignment = VerticalAlignment,
            HorizontalAlignment = HorizontalAlignment,
            Margin = Margin,
        };
    }

}