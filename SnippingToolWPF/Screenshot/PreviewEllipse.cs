using SnippingToolWPF.ExtensionMethods;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SnippingToolWPF.Screenshot
{
    public sealed class PreviewEllipse : Decorator
    {
        private const int NonHighLightedCells = 4;
        private const int HighlightedIndex = NonHighLightedCells; // Alias for readability
        private const int ColumnCount = NonHighLightedCells * 2 + 1; // make sure this is devidable by PreviewEllipseWidth
        private SolidColorBrush transparentBrush = new SolidColorBrush(Colors.Transparent);
        private SolidColorBrush lightBlueBrush = new SolidColorBrush(Color.FromArgb(40, 0, 100, 255));
        private SolidColorBrush lightGrayBrush = new SolidColorBrush(Color.FromArgb(50, 169, 169, 169));

        private readonly Ellipse mainEllipse;
        public PreviewEllipse()
        {
            mainEllipse = new Ellipse()
            {
                Stroke = Brushes.Black,
                StrokeThickness = 2
            };
            this.Child = new Grid().AddChildren(this.mainEllipse, this.CreateGrid());
            
        }

        private UniformGrid CreateGrid() => new UniformGrid() 
        {
            Columns = ColumnCount,
            Clip = mainEllipse.RenderedGeometry,
        }.AddChildren(this.CreateRectangles());
 

        private IEnumerable<UIElement> CreateRectangles()
        {
            for (var row = 0; row < ColumnCount; ++row)
            {
                for (var col = 0; col < ColumnCount; ++col)
                {
                    // Highlight middle cells except for middle_middle
                    var isHighlighted = (row, col) switch
                    {
                        (HighlightedIndex, HighlightedIndex) => false,
                        (HighlightedIndex, _) => true,
                        (_, HighlightedIndex) => true,
                        _ => false,
                    };
                    yield return new Rectangle()
                    {
                        Stroke = lightGrayBrush,
                        StrokeThickness = 1,
                        Fill = isHighlighted ? lightBlueBrush : lightGrayBrush,
                    };
                }
            }
        }
    }
}
