using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace SnippingToolWPF;

public class DrawingShapeAdorner : Adorner
{
    private readonly VisualCollection visualChildren;
    private ResizeThumb TopLeft { get; }
    private ResizeThumb TopRight { get; }
    private ResizeThumb BottomRight { get; }
    private ResizeThumb BottomLeft { get; }
    private ResizeThumb Top { get; }
    private ResizeThumb Bottom { get; }
    private ResizeThumb Left { get; }
    private ResizeThumb Right { get; }

    private readonly DrawingShape childElement;
    public DrawingShapeAdorner(DrawingShape adornedElement) : base(adornedElement)
    {
        childElement = adornedElement;
        
        TopLeft = new ResizeThumb(adornedElement, CornerOrSide.TopLeft);
        TopRight = new ResizeThumb(adornedElement, CornerOrSide.TopRight);
        BottomRight = new ResizeThumb(adornedElement, CornerOrSide.BottomRight);
        BottomLeft = new ResizeThumb(adornedElement, CornerOrSide.BottomLeft);
        Top = new ResizeThumb(adornedElement, CornerOrSide.Top);
        Bottom = new ResizeThumb(adornedElement, CornerOrSide.Bottom);
        Left = new ResizeThumb(adornedElement, CornerOrSide.Left);
        Right = new ResizeThumb(adornedElement, CornerOrSide.Right);
        
        visualChildren = new VisualCollection(this)
        {
            TopLeft,TopRight,BottomRight,BottomLeft,Top,Bottom,Left,Right
        };
        
    }
    
    /// <summary>
    /// Used during the layout process, when ResizeAdorner is being rendered on the screen this method gets called.
    /// Gets the visual associated with the adornerVisuals
    /// </summary>
    protected override Size ArrangeOverride(Size finalSize)
    {
        const int offset = 0; // Location of the AdornerThumb relative to the AdornerdElement
        base.ArrangeOverride(finalSize);
        var desireWidth = AdornedElement.DesiredSize.Width;
        var desireHeight = AdornedElement.DesiredSize.Height;
        var adornerWidth = this.DesiredSize.Width;
        var adornerHeight = this.DesiredSize.Height;
        var halfAdornerWidth = adornerWidth / 2;
        var halfAdornerHeight = adornerHeight / 2;
        TopLeft.Arrange(new Rect(-halfAdornerWidth - offset, -halfAdornerHeight - offset, adornerWidth, adornerHeight));
        TopRight.Arrange(new Rect(desireWidth - halfAdornerWidth + offset, -halfAdornerHeight - offset, adornerWidth, adornerHeight));
        BottomRight.Arrange(new Rect(desireWidth - halfAdornerWidth + offset, desireHeight - halfAdornerHeight + offset, adornerWidth, adornerHeight));
        BottomLeft.Arrange(new Rect(-halfAdornerWidth - offset, desireHeight - halfAdornerHeight + offset, adornerWidth, adornerHeight));
        Top.Arrange(new Rect(desireWidth / 2 - halfAdornerWidth, -halfAdornerHeight - offset, adornerWidth, adornerHeight));
        Bottom.Arrange(new Rect(desireWidth / 2 - halfAdornerWidth, desireHeight - halfAdornerHeight + offset, adornerWidth, adornerHeight));
        Left.Arrange(new Rect(-halfAdornerWidth - offset, desireHeight / 2 - halfAdornerHeight, adornerWidth, adornerHeight));
        Right.Arrange(new Rect(desireWidth - halfAdornerWidth + offset, desireHeight / 2 - halfAdornerHeight, adornerWidth, adornerHeight));
        return finalSize;
    }
    protected override int VisualChildrenCount => visualChildren.Count;
    protected override Visual GetVisualChild(int index) => visualChildren[index];
}