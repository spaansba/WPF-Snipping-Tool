using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Shapes;

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
    private ShapeRotation ShapeRotation { get; }
    
    public DrawingShapeAdorner(DrawingShape adornedElement) : base(adornedElement)
    {
        this.TopLeft = new ResizeThumb(adornedElement, ThumbLocation.TopLeft);
        this.TopRight = new ResizeThumb(adornedElement, ThumbLocation.TopRight);
        this.BottomRight = new ResizeThumb(adornedElement, ThumbLocation.BottomRight);
        this.BottomLeft = new ResizeThumb(adornedElement, ThumbLocation.BottomLeft);
        this.Top = new ResizeThumb(adornedElement, ThumbLocation.Top);
        this.Bottom = new ResizeThumb(adornedElement, ThumbLocation.Bottom);
        this.Left = new ResizeThumb(adornedElement, ThumbLocation.Left);
        this.Right = new ResizeThumb(adornedElement, ThumbLocation.Right);
        this.ShapeRotation = new ShapeRotation(adornedElement, new Size(adornedElement.Width, adornedElement.Height));
        
        visualChildren = new VisualCollection(this)
        {
            TopLeft,TopRight,BottomRight,BottomLeft,Top,Bottom,Left,Right,ShapeRotation.RotationThumb,ShapeRotation.AngleCircle 
        };
        
    }
    
    /// <summary>
    /// Used during the layout process, when ResizeAdorner is being rendered on the screen this method gets called.
    /// Gets the visual associated with the adornerVisuals
    /// </summary>
    protected override Size ArrangeOverride(Size finalSize)
    {
        base.ArrangeOverride(finalSize);
        this.Top.ArrangeIntoParent(finalSize);
        this.Bottom.ArrangeIntoParent(finalSize);
        this.Left.ArrangeIntoParent(finalSize);
        this.Right.ArrangeIntoParent(finalSize);
        this.TopLeft.ArrangeIntoParent(finalSize);
        this.BottomLeft.ArrangeIntoParent(finalSize);
        this.TopRight.ArrangeIntoParent(finalSize);
        this.BottomRight.ArrangeIntoParent(finalSize);
        this.ShapeRotation.RotationThumb.ArrangeIntoParent(finalSize);
    //    this.ShapeRotation.AngleCircle.ArrangeIntoParent(finalSize);
        return finalSize;
    }
    protected override int VisualChildrenCount => visualChildren.Count;
    protected override Visual GetVisualChild(int index) => visualChildren[index];
}