using System.Windows;
using System.Windows.Controls.Primitives;

namespace SnippingToolWPF;

public class ResizeThumb : Thumb
{
    public ResizeThumb()
    {
        
    }
    static ResizeThumb()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(ResizeThumb), 
            new FrameworkPropertyMetadata(typeof(ResizeThumb)));
    }
    
    private readonly DrawingShape? childElement;
    private readonly CornerOrSide? thumbCornerOrSide;
    
    public ResizeThumb(DrawingShape adornedElement, CornerOrSide thumbCornerOrSide)
    {
        this.childElement = adornedElement;
        this.thumbCornerOrSide = thumbCornerOrSide;

        this.DragDelta += thumb_DragDelta;
    }

    private void thumb_DragDelta(object sender, DragDeltaEventArgs e)
    {
        throw new NotImplementedException();
    }
}