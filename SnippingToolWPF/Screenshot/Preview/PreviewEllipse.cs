using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SnippingToolWPF.Screenshot.Preview;

public class PreviewEllipse : Preview
{
    public Canvas FullPreviewEllipse { get; set; }
    public Ellipse BaseEllipse { get; set; }
    private Grid GridWithRectangles { get; set; }

    private const int PreviewEllipseWidth = 126;
    private const int PreviewEllipseHeight = PreviewEllipseWidth;

    private const int GridSize = 9;
    private const int CellSize = PreviewEllipseWidth / GridSize;
    private SolidColorBrush transparentBrush = new SolidColorBrush(Colors.Transparent);
    private SolidColorBrush lightBlueBrush = new SolidColorBrush(Color.FromArgb(40, 0, 100, 255));
    private SolidColorBrush lightGrayBrush = new SolidColorBrush(Color.FromArgb(50, 169, 169, 169));

    public PreviewEllipse(BitmapSource ellipseFilling)
    {
        FillBitmap = ellipseFilling;

        // Create ellipse pieces
        BaseEllipse = CreateBaseEllipse();
        GridWithRectangles = CreateGridWithRectangles();

        // Make it so the Grid is only visible inside the ellipse
        GridWithRectangles.Clip = new EllipseGeometry(new Point(PreviewEllipseWidth / 2, PreviewEllipseHeight / 2),
                                                      PreviewEllipseWidth / 2, PreviewEllipseHeight / 2);

        // Return full build preview ellipse
        FullPreviewEllipse = CombinePreviewEllipsePieces();
    }

    #region Create static base of ellipse

    private Ellipse CreateBaseEllipse()
    {
        Ellipse previewEllipse = new Ellipse
        {
            Width = PreviewEllipseWidth,
            Height = PreviewEllipseHeight,
            Stroke = Brushes.Black,
            StrokeThickness = 2
        };

        return previewEllipse;
    }

    private Grid CreateGridWithRectangles()
    {
        Grid grid = new Grid();

        for (int i = 0; i < GridSize; i++)
        {
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(CellSize) });
            grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(CellSize) });

            for (int j = 0; j < GridSize; j++)
            {
                Rectangle cell = new Rectangle()
                {
                    Width = CellSize,
                    Height = CellSize,
                    Stroke = lightGrayBrush,
                    StrokeThickness = 1,

                    // Color in middle cells
                    Fill = j == (GridSize - 1) / 2 || i == (GridSize - 1) / 2 ?
                    j == i ? transparentBrush :
                    lightBlueBrush :
                    null
                };

                Grid.SetColumn(cell, j);
                Grid.SetRow(cell, i);
                grid.Children.Add(cell);
            }
        }

        return grid;
    }

    private Canvas CombinePreviewEllipsePieces()
    {
        // The order of putting children into the canvas matters, dont switch around
        Canvas container = new Canvas();
        container.Children.Add(GridWithRectangles);
        container.Children.Add(BaseEllipse);
        return container;
    }
    #endregion



}
