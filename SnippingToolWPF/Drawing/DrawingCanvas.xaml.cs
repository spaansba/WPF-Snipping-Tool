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

namespace SnippingToolWPF
{
    /// <summary>
    /// Interaction logic for DrawingCanvas.xaml
    /// </summary>
    public class DrawingCanvas : ItemsControl
    {
        static DrawingCanvas()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(DrawingCanvas),
                new FrameworkPropertyMetadata( typeof(DrawingCanvas)));
        }
    }
}
