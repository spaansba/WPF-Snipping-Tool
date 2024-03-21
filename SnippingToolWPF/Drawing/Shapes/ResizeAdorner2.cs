using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Media;

namespace SnippingToolWPF;

public class ResizeAdorner2 : Adorner
{
    private VisualCollection AdornerVisuals;
    private Thumb thumb1, thumb2;
    public ResizeAdorner2(UIElement adornedElement) : base(adornedElement)
    {
        AdornerVisuals = new VisualCollection(this);
        thumb1 = new Thumb();
        thumb2 = new Thumb();

        thumb1.DragDelta += Thumb1_DragDelta;
        thumb2.DragDelta += Thumb2_DragDelta;

        AdornerVisuals.Add(thumb1);
        AdornerVisuals.Add(thumb2);
    }

    private void Thumb2_DragDelta(object sender, DragDeltaEventArgs e)
    {
        var ele = (FrameworkElement)AdornedElement;
        var newHeight = ele.Height + e.VerticalChange < 0 ? 0 : ele.Height + e.VerticalChange;
        var newWidth = ele.Width + e.HorizontalChange < 0 ? 0 : ele.Width + e.HorizontalChange;
        
        ele.Height = newHeight;
        ele.Width = newWidth;
    }

    private void Thumb1_DragDelta(object sender, DragDeltaEventArgs e)
    {
        
        var ele = (FrameworkElement)AdornedElement;
        var newHeight = ele.Height - e.VerticalChange < 0 ? 0 : ele.Height - e.VerticalChange;
        var newWidth = ele.Width - e.HorizontalChange < 0 ? 0 : ele.Width - e.HorizontalChange;
        ele.Height = newHeight;
        ele.Width = newWidth;
        
    }


    protected override Visual GetVisualChild(int index)
    {
        return AdornerVisuals[index];
    }

    protected override int VisualChildrenCount => AdornerVisuals.Count;

    protected override Size ArrangeOverride(Size finalSize)
    {
        thumb1.Arrange(new Rect(0,0,10,10));
        thumb2.Arrange(new Rect(AdornedElement.DesiredSize.Width,AdornedElement.DesiredSize.Height,10,10));
        
        
        return base.ArrangeOverride(finalSize);
    }
}
