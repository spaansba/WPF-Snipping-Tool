using System.Windows;

namespace SnippingToolWPF.ExtensionMethods;

public static class DrawingShapeExtensions
{
    public static void SetOppositeCorners(this DrawingShape shape, Point point1, Point point2)
    {
        (shape.Left, shape.Top, shape.Width, shape.Height) = point1.ToRectangleFromCorner(point2);
    }
    public static Point GetCenterPoint(this DrawingShape element) => new (element.Height / 2, element.Width / 2);
}