using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SnippingToolWPF.Control.UserControls;

/// <summary>
///     Interaction logic for ColorSelector.xaml
/// </summary>
public partial class ColorSelector
{
    public static readonly DependencyProperty SelectedColorProperty = DependencyProperty.Register(
        nameof(SelectedColor),
        typeof(Color),
        typeof(ColorSelector),
        new FrameworkPropertyMetadata(Colors.Black) { BindsTwoWayByDefault = true });


    public static readonly DependencyProperty CustomColorSwatchesProperty = DependencyProperty.Register(
        nameof(CustomColorSwatches),
        typeof(IEnumerable<Color>),
        typeof(ColorSelector),
        new FrameworkPropertyMetadata(null) { BindsTwoWayByDefault = true }); //TODO: had to set to null or error

    public ColorSelector()
    {
        CustomColorSwatches = new List<Color>();
        InitializeComponent();
    }

    public Color SelectedColor
    {
        get => (Color)GetValue(SelectedColorProperty);
        set => SetValue(SelectedColorProperty, value);
    }

    public IEnumerable<Color>? CustomColorSwatches
    {
        get => (IEnumerable<Color>?)GetValue(CustomColorSwatchesProperty) ?? new List<Color>();
        set => SetValue(CustomColorSwatchesProperty, value);
    }

    private void ColorListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        // Handle the SelectionChanged event here
        PopupToggle.IsChecked = false; // Assuming you want to close the Popup
    }
}