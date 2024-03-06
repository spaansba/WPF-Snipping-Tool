using CommunityToolkit.Mvvm.ComponentModel;
using SnippingToolWPF.Drawing.Tools.PolygonTools;
using System.ComponentModel;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SnippingToolWPF.Drawing.Editing;

public sealed partial class PolygonShape : ShapeViewModel
{
    [ObservableProperty]
    private int vertices = 3;

    /// <summary>
    /// Every other Vertex will be inwarded by this number, creating a star with N points
    /// </summary>
    [ObservableProperty]
    private double innerCircle = 1.0;


    public PolygonShape(int vertices, double degreesRotated = 0d, double innerCircle = 1.0)
    {
        this.Vertices = vertices;
        this.InnerCircle = innerCircle;
        this.DegreesRotated = degreesRotated;
        Shape = CreateInitialPolygon.Create(this.Vertices, this.DegreesRotated, this.InnerCircle);
    }

    private void OnPropertyChanged(object sender, PropertyChangedEventArgs e) => UpdateShape();

    private void UpdateShape()
    {
        Shape = CreateInitialPolygon.Create(this.Vertices, this.DegreesRotated, this.InnerCircle);
    }
}
