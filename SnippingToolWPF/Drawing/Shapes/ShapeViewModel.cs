using CommunityToolkit.Mvvm.ComponentModel;
using SnippingToolWPF.Drawing.Tools.PolygonTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SnippingToolWPF.Drawing.Editing;

public abstract partial class ShapeViewModel : ObservableObject, IShape
{

    [ObservableProperty]
    private Shape shape = CreateInitialPolygon.Create(4, 0, 1.0);

    [ObservableProperty]
    private Brush? fill = null;

    [ObservableProperty]
    private Brush? stroke = Brushes.Black;

    [ObservableProperty]
    private double strokeThickness = 5;

    [ObservableProperty]
    private string? text;

    [ObservableProperty]
    private double fontSize;

    [ObservableProperty]
    private double x;

    [ObservableProperty]
    private double y;

    [ObservableProperty]
    private double width;

    [ObservableProperty]
    private double height;

    [ObservableProperty]
    private double degreesRotated;
}
