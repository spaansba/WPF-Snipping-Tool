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

    private int radius = 100;
    private int donutThickness = 20;
    
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
        context.BeginFigure(new Point(radius, donutThickness), isFilled: true, isClosed: false);
        context.LineTo(new Point(radius, 0), isStroked: true, isSmoothJoin: false);
        context.ArcTo(new Point(200, radius), new Size(100, 100), 0, false, SweepDirection.Clockwise, true, false);
        context.LineTo(new Point(180, radius), isStroked: true, isSmoothJoin: false);
        context.BeginFigure(new Point(radius, donutThickness), isFilled: true, isClosed: false);
        context.ArcTo(new Point(180, radius), new Size(80, 80), 0, false, SweepDirection.Clockwise, true, false);
    }
}