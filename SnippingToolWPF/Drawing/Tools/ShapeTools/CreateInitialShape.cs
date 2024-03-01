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
    private static readonly Point[] trianglePoints
    = [new(0.5, 0), new(0, 1), new(1, 1)];

    private static readonly Point[] pentagonPoints
        = [new Point(0.5, 0), new Point(0, 1), new Point(1, 1), new Point(0.809016994, 0.587785252), new Point(0.309016994, 0.951056516)];
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
