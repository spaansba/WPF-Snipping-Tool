using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace SnippingToolWPF;

public class DrawingShapeAdorner : Adorner
{
    private readonly VisualCollection visualChildren;
    private DrawingShape childElement;
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
        this.childElement = adornedElement;
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
            TopLeft,TopRight,BottomRight,BottomLeft,Top,Bottom,Left,Right,ShapeRotation.RotationThumb
        }; 
    }
    
    /// <summary>
    /// Used during the layout process, when ResizeAdorner is being rendered on the screen this method gets called.
    /// Gets the visual associated with the adornerVisuals
    /// </summary>
    protected override Size ArrangeOverride(Size finalSize)
    {
        base.ArrangeOverride(finalSize);
        foreach (var visual in visualChildren)
        {
            if (visual is AnchoredThumb thumb)
            {
                if (finalSize.Width < 30 || finalSize.Height < 30)
                {
                    //Mike: Ask why element gets shrinked when offset parameter is reached (for now its 0 to not cause buggs)
                    thumb.ArrangeIntoParentWithOffset(finalSize, 0);
                }
                else
                {
                    thumb.ArrangeIntoParent(finalSize);
                }
            }
        }
        return finalSize;
    }
    
    /// <param name="visible">True makes the Adorner Visible, False hides the adorner</param>
    public void AdornerVisibility(bool visible)
    {
        if (visible)
            visualChildren.OfType<AnchoredThumb>().ToList().ForEach(thumb => thumb.UnHide());
        else
            visualChildren.OfType<AnchoredThumb>().ToList().ForEach(thumb => thumb.Hide());
    }
    
    protected override int VisualChildrenCount => visualChildren.Count;
    protected override Visual GetVisualChild(int index) => visualChildren[index];
    
}