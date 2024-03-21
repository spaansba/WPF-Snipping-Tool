using System.Windows;
using System.Windows.Controls.Primitives;

namespace SnippingToolWPF;

public class AdornerThumb : Thumb
{
    static AdornerThumb()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(AdornerThumb), 
            new FrameworkPropertyMetadata(typeof(AdornerThumb)));
    }
}