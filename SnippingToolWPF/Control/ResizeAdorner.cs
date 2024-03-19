using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Media;

namespace SnippingToolWPF.Control;

public class ResizeAdorner : Adorner
{
    private readonly VisualCollection visualChilderns;
    private readonly Thumb topLeftThumb = new() {Background = Brushes.Aqua, Height = 10, Width = 10};
    private readonly Thumb bottomRightThumb = new() {Background = Brushes.Aqua, Height = 10, Width = 10};

    public ResizeAdorner(UIElement adornedElement) : base(adornedElement)
    {
        topLeftThumb.DragDelta += TopLeftThumb_DragDelta;
        bottomRightThumb.DragDelta += BottomRightThumb_DragDelta;
        
        visualChilderns = new VisualCollection(this)
        {
            topLeftThumb,
            bottomRightThumb,
        };
    }

    private void TopLeftThumb_DragDelta(object sender, DragDeltaEventArgs e)
    {
        var element = (FrameworkElement)AdornedElement;
        element.Height = element.ActualHeight - e.VerticalChange < 0 ? 0 : element.ActualHeight - e.VerticalChange ;
        element.Width = element.ActualWidth - e.HorizontalChange < 0 ? 0 : element.ActualWidth - e.HorizontalChange;
    }
    private void BottomRightThumb_DragDelta(object sender, DragDeltaEventArgs e)
    {
        var element = (FrameworkElement)AdornedElement;
        element.Height = element.Height + e.VerticalChange < 0 ? 0 : element.Height + e.VerticalChange ;
        element.Width = element.Width + e.HorizontalChange < 0 ? 0 : element.Width + e.HorizontalChange;
    }

    /// <summary>
    /// Used during the layout process, when ResizeAdorner is being rendered on the screen this method gets called.
    /// Gets the visual associated with the adornerVisuals
    /// </summary>
    protected override Visual GetVisualChild(int index)
    {
        return adornerVisuals[index];
    }

    protected override int VisualChildrenCount => adornerVisuals.Count;

    protected override Size ArrangeOverride(Size finalSize)
    {
        const int offset = 5;
        //Arrange thumb relative to the UIElement in the constructor (adornedElement)
        topLeftThumb.Arrange(new Rect(- offset,- offset,10,10));
        bottomRightThumb.Arrange(new Rect(AdornedElement.DesiredSize.Width -offset, AdornedElement.DesiredSize.Height - offset, 10,10)); 
        return base.ArrangeOverride(finalSize);
    }
    
}