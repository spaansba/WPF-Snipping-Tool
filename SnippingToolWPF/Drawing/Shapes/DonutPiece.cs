using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SnippingToolWPF;

public class DonutPiece : Shape
{
    protected override Geometry DefiningGeometry 
    { 
        get 
        { 
            var geometry = new StreamGeometry();
            using var context = geometry.Open();
            Draw(context);
            return geometry; 
        } 
    }
    
    /// <summary>
    /// M 100,20 // BeginFigure
    // L 100,0 // LineTo
    // A 100,100 90 0 1 200,100 // ArcTo
    // L 180,100 // LineTo
    // M 100,20 // BeginFigure
    // A 80,80 90 0 1 180,100 // ArcTo
    /// </summary>
    /// <param name="context"></param>
    private void Draw(StreamGeometryContext context)
    {
        var innerRadius = 0.5;
        var donutThickness = 20d;
        var outerRadius = innerRadius + donutThickness;
        var innerDiameter = innerRadius * 2;
        var outerDiameter = outerRadius * 2;
        var startAngle = -90d; // starting at -90, which is the negative Y axis (UP) since 0 is the postive axis
        var sweepAngle = 90d;
        
        // Points using 0,0 as the center
        var outerRadiusVector = new Vector(outerRadius, outerRadius);
        var innerRadiusVector = new Vector(innerRadius, innerRadius);
        var point1 = GetPointAlongCircleFromAngle(startAngle, outerRadius) + outerRadiusVector;
        var point2 = GetPointAlongCircleFromAngle(startAngle + sweepAngle, outerRadius) + outerRadiusVector;
        var point3 = GetPointAlongCircleFromAngle(startAngle + sweepAngle, innerRadius) + innerRadiusVector;
        var point4 = GetPointAlongCircleFromAngle(startAngle, innerRadius) + innerRadiusVector;
        var outerArcSize = GetSizeFromPoints(point1, point2);
        var innerArcSize = GetSizeFromPoints(point3, point4);
        var arcRotationAngle = 0d;

        context.BeginFigure(point4, isFilled: true, isClosed: false);
        context.LineTo(point1, isStroked: true, isSmoothJoin: false);
        context.ArcTo(point2, outerArcSize, arcRotationAngle, false, SweepDirection.Clockwise, isStroked: true, isSmoothJoin: false);
        context.LineTo(point3, isStroked: true, isSmoothJoin: false);
        context.BeginFigure(point4, isFilled: true, isClosed: false); // TODO: We shouldn't need this
        context.ArcTo(point3, innerArcSize, arcRotationAngle, false, SweepDirection.Clockwise, isStroked: true, isSmoothJoin: false);
    }
    
    private static Point GetPointAlongCircleFromAngle(double angle, double radius)
        => new(radius * Math.Cos(angle), radius * Math.Sin(angle));
    
    private static Size GetSizeFromPoints(Point a, Point b)
        => new(Math.Abs(a.X - b.X), Math.Abs(a.Y - b.Y));
    
}