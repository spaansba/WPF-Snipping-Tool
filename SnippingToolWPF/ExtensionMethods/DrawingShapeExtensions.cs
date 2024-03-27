using System.Windows;

namespace SnippingToolWPF.ExtensionMethods;

public static class DrawingShapeExtensions
{
    public static void CreateRectFromOppositeCorners(this DrawingShape shape, Point point1, Point point2)
    {
        (shape.Left, shape.Top, shape.Width, shape.Height) = point1.ToRectangleFromCorner(point2);
    }
    
    public static void CreateRectFromOppositeCenters(this DrawingShape shape, Point currentCenter, Point oppositeCenter, double halfSize, bool height)
    {
        Point topLeft;
        Point bottomRight;
        if (height)
        {
            topLeft = currentCenter with { Y = currentCenter.Y - halfSize };
            bottomRight = oppositeCenter with { Y = oppositeCenter.Y + halfSize };
        }
        else
        {
            topLeft = currentCenter with { X = currentCenter.X - halfSize };
            bottomRight = oppositeCenter with { X = oppositeCenter.X + halfSize };
        }

        (shape.Left, shape.Top, shape.Width, shape.Height) = topLeft.ToRectangleFromCorner(bottomRight);
    }
    
    public static Point GetCenterPoint(this DrawingShape element) => new (element.Width / 2, element.Height / 2);
}