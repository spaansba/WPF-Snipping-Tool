using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using SnippingToolWPF.Common;
using SnippingToolWPF.ExtensionMethods;

namespace SnippingToolWPF;
/// <summary>
/// To make a rotation we take the start position X coordinate and the mouse position Y coordinate and create a
/// right triangle (1 90 degree angle triangle) with the middle point of the shape.
///
/// right triangle side labels:
/// - "Hypotenuse" is always the side that is opposite the 90 degree angle
/// - "Opposite" is the side that is opposite the angle you're looking for
/// - "Adjacent" is the side that's next to the angle that you're looking for 
///
/// Sin - Opposite / Hypotenuse
/// Cos - Adjacent / Hypotenuse
/// Tan - Opposite / Adjacent << what we need to use
/// </summary>
public class RotationThumb : AnchoredThumb
{
    static RotationThumb()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(RotationThumb), 
            new FrameworkPropertyMetadata(typeof(RotationThumb)));
    }

    public RotationThumb() : base(ThumbLocation.Top, new Point(0,-25)) //offset = 25 above the shape
    {
    }
}

