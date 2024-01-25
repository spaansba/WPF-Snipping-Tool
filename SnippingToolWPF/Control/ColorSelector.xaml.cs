using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SnippingToolWPF.Control
{
    /// <summary>
    /// Interaction logic for ColorSelector.xaml
    /// </summary>
    public partial class ColorSelector : UserControl
    {
        public ColorSelector()
        {
         //   this.CustomColorSwatches = new List<Color>();
            InitializeComponent();
        }

        public IEnumerable<Color>? CustomColorSwatches
        {
            get => (IEnumerable<Color>?)this.GetValue(CustomColorSwatchesProperty) ?? new List<Color>();
         //   set => this.SetValue(CustomColorSwatchesProperty, value);
        }

        public static readonly DependencyProperty CustomColorSwatchesProperty = DependencyProperty.Register(
            nameof(CustomColorSwatches),
            typeof(Color),
            typeof(ColorSelector),
            new FrameworkPropertyMetadata(Colors.Black) { BindsTwoWayByDefault = true });

    }
}
