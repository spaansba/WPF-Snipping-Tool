using SnippingToolWPF.Control;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;


namespace SnippingToolWPF.Drawing
{
    /// <summary>
    /// Interaction logic for TextSidePanel.xaml
    /// </summary>
    public partial class TextSidePanel : UserControl
    {
        public TextSidePanel()
        {
            InitializeComponent();
        }
        private void BorderColorSelectorButton_Click(object sender, RoutedEventArgs e)
        {
            // Create an instance of ColorSelector.xaml user control
            ColorSelector colorSelector = new ColorSelector();

            // Create a Popup control
            Popup popup = new Popup
            {
                Child = colorSelector,
                Placement = PlacementMode.Bottom,
                IsOpen = true
            };
        }
    }
}
