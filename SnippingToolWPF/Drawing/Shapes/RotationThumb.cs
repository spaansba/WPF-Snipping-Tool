using System.Windows;

namespace SnippingToolWPF;
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

