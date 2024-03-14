using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using SnippingToolWPF.Common;

namespace SnippingToolWPF.Tools.PolygonTools;

/// <summary>
///     Creates initial shapes on a 1x1 plane based on the polygonOptions
/// </summary>
public static class CreateInitialPolygon
{
    #region Polygon Points Creating

    /// <summary>
    ///     Returns a Polygon with N vertices in a 1x1 plane
    /// </summary>
    /// <param name="vertices">amount of vertices of the polygon</param>
    /// <param name="rotationDegrees">degrees the polygon should rotate in</param>
    /// <param name="innerCircleSize">
    ///     the size of the circumscribed circle of the inner circle, must be a value from 0.1 to 1.
    ///     When this is set the Polygon will be shaped like a star figure
    public static Point[] GeneralPolygonPoints(int vertices, double rotationDegrees = 0, double innerCircleSize = 1.0)
    {
        return innerCircleSize >= 1.0
            ? GeneratePolygonPoints(vertices, rotationDegrees)
            : GenerateStarPoints(vertices / 2, rotationDegrees, innerCircleSize);
    }
    
    public static Point[] GeneratePolygonPoints(int vertices, double rotationDegrees = 0, double innerCircleSize = 1.0)
    {
        var points = new Point[vertices];
        var rotation = double.DegreesToRadians(rotationDegrees);
        var angleIncrement = -double.DegreesToRadians(360d / vertices);

        for (var i = 0; i < vertices; i++)
        {
            var angle = angleIncrement * i + rotation; // Start from the bottom and move clockwise
            var x = innerCircleSize / 2 * Math.Cos(angle) + innerCircleSize / 2;
            var y = innerCircleSize / 2 * Math.Sin(angle) + innerCircleSize / 2;
            points[i] = new Point(x, y);
        }

        return points;
    }

    /// <summary>
    ///     Creating a star is basically creating 2 set of polygon points, 1 outer and 1 inner
    ///     The inner points are
    /// </summary>
    /// <param name="outerVertices">Total vertices of the star (amount of starpoints) </param>
    /// <param name="innerCircleSize">the size of the circumscribed circle of the inner circle, must be a value from 0.1 to 1</param>
    /// <param name="rotationDegrees">degrees the star will be rotated in</param>
    /// <returns></returns>
    public static Point[] GenerateStarPoints(int outerVertices, double rotationDegrees = 0,
        double innerCircleSize = 0.5)
    {
        var offset = 360d / outerVertices / 2;

        var outer = GeneratePolygonPoints(outerVertices, rotationDegrees);
        var inner = GeneratePolygonPoints(outerVertices, rotationDegrees + offset, innerCircleSize);

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