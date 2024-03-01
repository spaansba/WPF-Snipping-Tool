using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace SnippingToolWPF.ExtensionMethods;

public static class VectorExtensions
{
    public static Vector RotateDegrees(this Vector vector, double angle)
    {
        Matrix m = Matrix.Identity;
        m.Rotate(angle);
        return m.Transform(vector);
    }

    /// <summary>
    /// First converts Radians to Degrees, then Uses the RotateDegrees method
    /// </summary>
    public static Vector RotateRadians(this Vector vector, double angle) => vector.RotateDegrees(double.RadiansToDegrees(angle));

}
