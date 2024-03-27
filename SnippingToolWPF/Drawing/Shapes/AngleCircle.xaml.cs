using System.Globalization;
using System.Windows;
using SnippingToolWPF.ExtensionMethods;
using SnippingToolWPF.WPFExtensions;

namespace SnippingToolWPF;

public partial class AngleCircle
{
    public AngleCircle(Size parentSize)
    {
        InitializeComponent();
        if (parentSize.Width > 0 && parentSize.Height > 0)
        {
            this.Visibility = Visibility.Hidden;
            this.Arrange(new Rect(parentSize));
        }

    }

    public void MakeVisible()
    {
        this.Visibility = Visibility.Visible;
        this.Arrange(new Rect(new Point(0,0), this.DesiredSize));
    }
    public void MakeInvisible() => this.Visibility = Visibility.Hidden;

    internal void ArrangeIntoParent(Size parentSize)
    {
        var centerPoint = parentSize.GetCornerOrSide(ThumbLocation.TopLeft);
        this.Arrange(new Rect(centerPoint, this.DesiredSize));
    }
    
    public static readonly DependencyProperty EllipseRadiusProperty = DependencyProperty.Register(
        nameof(EllipseRadius),
        typeof(double),
        typeof(AngleCircle),
        new FrameworkPropertyMetadata(100d));
    
    public double EllipseRadius
    {
        get => this.GetValue<double>(EllipseRadiusProperty);
        set => this.SetValue<double>(EllipseRadiusProperty, value);
    }
    
    internal void ChangeAngleTextBox(double angle)
    {
        this.AngleTextBox = $"{Math.Round(angle).ToString(CultureInfo.InvariantCulture)}°";
    }
    
    public static readonly DependencyProperty AngleTextBoxProperty = DependencyProperty.Register(
        nameof(AngleTextBox),
        typeof(string),
        typeof(AngleCircle),new FrameworkPropertyMetadata( "0°"));
    
    public string? AngleTextBox
    {
        get => this.GetValue<string?>(AngleTextBoxProperty);
        set => this.SetValue<string?>(AngleTextBoxProperty, value);
    }
    
}