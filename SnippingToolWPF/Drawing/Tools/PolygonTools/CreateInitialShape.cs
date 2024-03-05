using System.Windows;
using System.Windows.Ink;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SnippingToolWPF.Drawing.Tools.PolygonTools;

/// <summary>
/// Creates initial shapes on a 1x1 plane based on the polygonOptions
/// </summary>
public static class CreateInitialPolygon
{
    #region Create Polygon Shape
    public static Shape Create(PolygonOptions shapeToCreate) => Create(shapeToCreate, null, null);

    public static Shape Create(PolygonOptions shapeToCreate, double? thickness, Brush? stroke) => shapeToCreate switch
    {
        PolygonOptions.Ellipse => new Ellipse() { StrokeThickness = thickness ?? default, Stroke = stroke ?? default }, // Only non-polygon
        PolygonOptions.Triangle => CreatePolygon(GeneratePolygonPoints(3, 30), thickness, stroke),
        PolygonOptions.Rectangle => CreatePolygon(GeneratePolygonPoints(4, 45), thickness, stroke),
        PolygonOptions.Diamond => CreatePolygon(GeneratePolygonPoints(4), thickness, stroke),
        PolygonOptions.Pentagon => CreatePolygon(GeneratePolygonPoints(5, 270), thickness, stroke),
        PolygonOptions.Hexagon => CreatePolygon(GeneratePolygonPoints(6), thickness, stroke),
        PolygonOptions.Heptagon => CreatePolygon(GeneratePolygonPoints(7), thickness, stroke),
        PolygonOptions.Hectagon => CreatePolygon(GeneratePolygonPoints(8), thickness, stroke),
        PolygonOptions.Star => CreatePolygon(GenerateStarPoints(5, 0.6), thickness, stroke),
        _ => throw new ArgumentOutOfRangeException(nameof(shapeToCreate), shapeToCreate, default)
    };

    static Polygon CreatePolygon(IEnumerable<Point> points, double? thickness, Brush? stroke) =>
        new() { Stretch = Stretch.Fill, Points = new PointCollection(points), StrokeThickness = thickness ?? 1.0, Stroke = stroke ?? default };
    #endregion

    #region Polygon Points Creating
    /// <summary>
    /// Generates polygon points in a 1x1 pane
    /// </summary>
    /// <param name="vertices">amount of vertices the polygon should have</param>
    /// <param name="rotationDegrees">degrees the polygon should rotate in</param>
    /// <param name="circleSize">the size of the circumscribed circle of the inner circle, must be a value from 0.1 to 1</param>
    /// <returns></returns>
    public static Point[] GeneratePolygonPoints(int vertices, double rotationDegrees = 0, double circleSize = 1.0)
    {
        Point[] points = new Point[vertices];
        var rotation = double.DegreesToRadians(rotationDegrees);
        var angleIncrement = -double.DegreesToRadians(360d / vertices);

        for (int i = 0; i < vertices; i++)
        {
            double angle = (angleIncrement * i) + rotation; // Start from the bottom and move clockwise
            double x = (circleSize / 2) * Math.Cos(angle) + (circleSize / 2);
            double y = (circleSize / 2) * Math.Sin(angle) + (circleSize / 2);
            points[i] = new Point(x, y);
        }

        return points;
    }

    /// <summary>
    /// Creating a star is basically creating 2 set of polygon points, 1 outer and 1 inner
    /// The inner points are
    /// </summary>
    /// <param name="outerVertices">Total vertices of the star (amount of starpoints) </param>
    /// <param name="innerSize">the size of the circumscribed circle of the inner circle, must be a value from 0.1 to 1</param>
    /// <param name="rotation">degrees the star will be rotated in</param>
    /// <returns></returns>
    public static Point[] GenerateStarPoints(int outerVertices, double innerSize, double rotationDegrees = 0)
    {
        var offset = 360d / outerVertices / 2;

        var outer = GeneratePolygonPoints(outerVertices, rotationDegrees, 1.0);
        var inner = GeneratePolygonPoints(outerVertices, rotationDegrees + offset, innerSize);

        var result = new Point[outerVertices * 2];
        for (var i = 0; i < outerVertices; ++i)
        {
            result[i * 2] = outer[i];
            result[i * 2 + 1] = inner[i];
        }
        return result;
    }

    #endregion

}
