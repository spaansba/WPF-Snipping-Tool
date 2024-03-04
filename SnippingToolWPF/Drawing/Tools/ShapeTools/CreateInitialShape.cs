using System.Windows;
using System.Windows.Ink;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SnippingToolWPF.Drawing.Tools.ShapeTools;

/// <summary>
/// Creates initial shapes on a 1x1 plane based on the ShapeOptions
/// </summary>
public static class CreateInitialShape
{
    private static readonly Point[] trianglePoints = 
    [   
      new(0.5, 0), // Top Center
      new(1, 1), //Bottom Right
      new(0, 1), // Bottom Left
    ];

    private static readonly Point[] pentagonPoints =
    [
        new(0.5, 0), // Top Center
        new(0, 0.4), // Mid Right
        new(0.2, 1), // Bottom Right
        new(0.8, 1), // Bottom Left
        new(1, 0.4),  // Mid Left
    ];
    public static Shape Create(ShapeOptions shapeToCreate) => Create(shapeToCreate, null, null);

    public static Shape Create(ShapeOptions shapeToCreate, double? thickness, Brush? stroke) => shapeToCreate switch
    {
        ShapeOptions.Ellipse => new Ellipse() { StrokeThickness = thickness ?? default, Stroke = stroke ?? default},
        ShapeOptions.Rectangle => new Rectangle() { StrokeThickness = thickness ?? default, Stroke = stroke ?? default },
        ShapeOptions.Triangle => CreatePolygon(trianglePoints, thickness, stroke),
        ShapeOptions.Pentagon => CreatePolygon(pentagonPoints, thickness, stroke),
        _ => throw new ArgumentOutOfRangeException(nameof(shapeToCreate), shapeToCreate, default)
    };

    static Polygon CreatePolygon(IEnumerable<Point> points, double? thickness, Brush? stroke) =>
        new() { Stretch = Stretch.Fill, Points = new PointCollection(points), StrokeThickness = thickness ?? 1.0, Stroke = stroke ?? default };
}
