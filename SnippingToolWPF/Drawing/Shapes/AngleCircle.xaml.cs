using System.Windows;
using SnippingToolWPF.Control.UserControls;
using SnippingToolWPF.ExtensionMethods;

namespace SnippingToolWPF;

public partial class AngleCircle
{
    public AngleCircle(Size parentSize)
    {
        InitializeComponent();
        this.Visibility = Visibility.Hidden;
        this.Arrange(new Rect(parentSize));
    }

    public void MakeVisible() => this.Visibility = Visibility.Visible;
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
        get => (double)GetValue(EllipseRadiusProperty);
        set => SetValue(EllipseRadiusProperty, value);
    }
    
    public static readonly DependencyProperty AngleTextBoxProperty = DependencyProperty.Register(
        nameof(AngleTextBox),
        typeof(string),
        typeof(AngleCircle),
        new FrameworkPropertyMetadata("0°"));
    
    public string AngleTextBox
    {
        get => (string)GetValue(AngleTextBoxProperty);
        set => SetValue(AngleTextBoxProperty, value);
    }
}