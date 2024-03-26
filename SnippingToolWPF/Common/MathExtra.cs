using System.Diagnostics;
using System.Windows;

namespace SnippingToolWPF.Common;

/// <summary>
/// Not a class for extension methods just a regular static class 
/// </summary>
public static class MathExtra
{
    /// <summary>
    /// Atan2 gives us the Radians between the point and the X axis 
    /// If we calculate this Radians for both points we can subtract them from eachother to calculate the angle in radians between the two points
    /// </summary>
    public static double AngleBetweenInRadians(Point a, Point b)
    {
        var aRadians  = Math.Atan2(a.Y, a.X);
        var bRadians  = Math.Atan2(b.Y, b.X);
        var diff = aRadians - bRadians;
        Debug.WriteLine($"a radians {aRadians}, b radians {bRadians}, diff {diff}");
        return diff;
    }
    
    public static double AngleBetweenInDegrees(Point a, Point b)
    => double.RadiansToDegrees(AngleBetweenInRadians(a, b));
    
    public static Point MakePointRelativeTo(Point pointToTranslate, Point origin)
    => new (pointToTranslate.X + origin.X, pointToTranslate.Y + origin.Y);
    
}