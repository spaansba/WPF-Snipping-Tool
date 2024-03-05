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
    #region Shape Points
    private static readonly Point[] diamondPoints =
    [
        new(0.5, 0), // Top center
        new(1, 0.5), // Center Right
        new(0.5, 1), // Bottom Center
        new(0, 0.5), // Center Left
    ];

    private static readonly Point[] starPoints =
    [
        new(0.5, 0), // Top Center
        new(0.61, 0.35), // Top Right
        new(0.95, 0.35), // Right, Top
        new(0.68, 0.57), // Right, Center
        new(0.79, 0.91), // Right, Bottom
        new(0.5, 0.7), // Bottom Center
        new(0.21, 0.91), // Left, Bottom
        new(0.32, 0.57), // Left, Center
        new(0.05, 0.35), // Left, Top
        new(0.39, 0.35), // Top Left
    ];

    public static Point[] GeneratePolygonPoints(int sides)
    {
        if (sides < 3)
            throw new ArgumentException("A polygon must have at least 3 sides.", nameof(sides));

        Point[] points = new Point[sides];

        for (int i = 0; i < sides; i++)
        {
            double angle = -(Math.PI / 2) - (2 * Math.PI * i / sides); // Start from the bottom and move clockwise
            double x = 0.5 * Math.Cos(angle) + 0.5; // Adjust x to fit within a 1x1 square
            double y = 0.5 * Math.Sin(angle) + 0.5; // Adjust y to fit within a 1x1 square
            points[i] = new Point(x, y);
        }

        return points;
    }

    #endregion
    public static Shape Create(ShapeOptions shapeToCreate) => Create(shapeToCreate, null, null);

    public static Shape Create(ShapeOptions shapeToCreate, double? thickness, Brush? stroke) => shapeToCreate switch
    {
        ShapeOptions.Ellipse => new Ellipse() { StrokeThickness = thickness ?? default, Stroke = stroke ?? default },
        ShapeOptions.Rectangle => new Rectangle() { StrokeThickness = thickness ?? default, Stroke = stroke ?? default },
        ShapeOptions.Triangle => CreatePolygon(GeneratePolygonPoints(3), thickness, stroke),
        ShapeOptions.Diamond => CreatePolygon(diamondPoints, thickness, stroke),
        ShapeOptions.Pentagon => CreatePolygon(GeneratePolygonPoints(5), thickness, stroke),
        ShapeOptions.Hexagon => CreatePolygon(GeneratePolygonPoints(6), thickness, stroke),
        ShapeOptions.Heptagon => CreatePolygon(GeneratePolygonPoints(7), thickness, stroke),
        ShapeOptions.Hectagon => CreatePolygon(GeneratePolygonPoints(8), thickness, stroke),
        ShapeOptions.Star => CreatePolygon(starPoints, thickness, stroke),
        _ => throw new ArgumentOutOfRangeException(nameof(shapeToCreate), shapeToCreate, default)
    };

    static Polygon CreatePolygon(IEnumerable<Point> points, double? thickness, Brush? stroke) =>
        new() { Stretch = Stretch.Fill, Points = new PointCollection(points), StrokeThickness = thickness ?? 1.0, Stroke = stroke ?? default };
}
