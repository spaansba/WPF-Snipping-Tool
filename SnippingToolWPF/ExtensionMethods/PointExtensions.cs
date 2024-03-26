using System.Windows;
using SnippingToolWPF.Common;

namespace SnippingToolWPF.ExtensionMethods;

public static class PointExtensions
{
    /// <summary>
    /// Creates a rectangle starting from 2 points
    /// </summary>
    public static Rect ToRectangleFromCorner(this Point point1, Point point2) 
        => new()
        {
            // Set current size of the rect
            Width = Math.Abs(point1.X - point2.X),
            Height = Math.Abs(point1.Y - point2.Y),
            
            // Set the new position of the rect
            X = Math.Min(point1.X, point2.X),
            Y = Math.Min(point1.Y, point2.Y)
        };


    public static Rect ToRectangleFromCenter(this Point center, Size size) => new (center, size);
    
    /// <summary>
    ///  
    /// Returns the point on a size based on the ThumbLocation
    /// </summary>
    /// <param name="offset">offset is the size of the thumb, otherwise the right/bottom thumbs are outside of the shape
    /// while the left / top thumbs are iunside the shape </param>
    public static Point GetCornerOrSide(this Size size, ThumbLocation cornerOrSide, double offset = 0)
    {

        var halfOffset = offset != 0 ? offset / 2 : 0;
        
        switch (cornerOrSide)
        {
            case ThumbLocation.TopLeft:
                return new Point(0 - offset, 0 - offset);
            case ThumbLocation.Top:
                return new Point((size.Width / 2) - halfOffset, 0 - offset);
            case ThumbLocation.TopRight:
                return new Point(size.Width, 0 - offset);
            case ThumbLocation.Right:
                return new Point(size.Width, (size.Height / 2) - halfOffset);
            case ThumbLocation.BottomRight:
                return new Point(size.Width, size.Height);
            case ThumbLocation.Bottom:
                return new Point((size.Width / 2) - halfOffset, size.Height);
            case ThumbLocation.BottomLeft:
                return new Point(0 - offset, size.Height);
            case ThumbLocation.Left:
                return new Point(0 - offset, (size.Height / 2) - halfOffset);
            default:
                throw new ArgumentOutOfRangeException(nameof(cornerOrSide), cornerOrSide, null);
        }
    }

    /// <summary>
    /// Links to the math extension class so we can use this as a Point Extension method
    /// </summary>
    public static Point MakeRelativeTo(this Point pointToTranslate, Point origin)
        => MathExtra.MakePointRelativeTo(pointToTranslate, origin);

}