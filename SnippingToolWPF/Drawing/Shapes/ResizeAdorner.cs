using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace SnippingToolWPF;

public class ResizeAdorner : Adorner
{
    private const double angle = 0.0;
    private Point transformOrigin = new Point(0, 0);
    private readonly VisualCollection visualChilderns;
    private ResizeThumb LeftTop { get; }
    private ResizeThumb RightTop { get; }
    private ResizeThumb RightBottom { get; }
    private ResizeThumb LeftBottom { get; }
    private readonly DrawingShape childElement;
    private bool dragStarted;
    public ResizeAdorner(DrawingShape adornedElement) : base(adornedElement)
    {
        childElement = adornedElement;
        
        // LeftTop = new ResizeThumb(adornedElement, CornerOrSide.TopLeft);
        // RightTop = new ResizeThumb(adornedElement, CornerOrSide.TopRight);
        // RightBottom = new ResizeThumb(adornedElement, CornerOrSide.BottomRight);
        // LeftBottom = new ResizeThumb(adornedElement, CornerOrSide.BottomLeft);
        
        LeftTop = CreateThumbPart(Cursors.SizeNWSE);
        RightTop = CreateThumbPart(Cursors.SizeNESW);
        RightBottom = CreateThumbPart(Cursors.SizeNWSE);
        LeftBottom = CreateThumbPart(Cursors.SizeNESW);
        
        visualChilderns = new VisualCollection(this)
        {
            LeftTop,RightTop,RightBottom,LeftBottom
        };
        
        LeftTop.DragDelta += OnLeftTopOnDragDelta;
        RightTop.DragDelta += OnRightTopOnDragDelta;
        LeftBottom.DragDelta += OnLeftBottomOnDragDelta;
        RightBottom.DragDelta += OnRightBottomOnDragDelta;
    }
    
    private (double hor, double vert) GetChange(DragDeltaEventArgs e, bool invert)
    {
        var hor = e.HorizontalChange;
        var vert = e.VerticalChange;
        if (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift))
            (hor, vert) = RetainAspectRatioCalculation(hor, vert, invert);
        return (hor, vert);
    }
    
    private void OnLeftTopOnDragDelta(object _, DragDeltaEventArgs e)
    {
        var (hor, vert) = GetChange(e, false);
        ResizeX(hor);
        ResizeY(vert);
        dragStarted = false;
        e.Handled = true;
    }

    private void OnRightBottomOnDragDelta(object _, DragDeltaEventArgs e)
    {
        var (hor, vert) = GetChange(e, false);
        ResizeWidth(hor);
        ResizeHeight(vert);
        dragStarted = false;
        e.Handled = true;
    }
    
    private void OnLeftBottomOnDragDelta(object _, DragDeltaEventArgs e)
    {
        var (hor, vert) = GetChange(e, true);
        ResizeX(hor);
        ResizeHeight(vert);
        dragStarted = false;
        e.Handled = true;
    }

    private void OnRightTopOnDragDelta(object _, DragDeltaEventArgs e)
    {
        var (hor, vert) = GetChange(e, true);
        ResizeWidth(hor);
        ResizeY(vert);
        dragStarted = false;
        e.Handled = true;
    }
    
    /// <summary>
    /// Calculates the new horizontal and vertical changes to retain the aspect ratio of the adorned element during resizing (while holding shift).
    /// </summary>
    private (double newHorizontalChange, double newVerticalChange) RetainAspectRatioCalculation(double horizontalChange,
        double verticalChange, bool invert)
    {
        if (dragStarted) isHorizontalDrag = Math.Abs(horizontalChange) > Math.Abs(verticalChange);
        if (isHorizontalDrag)
        {
            if (invert)
                return (horizontalChange, -horizontalChange);
            else
                return (horizontalChange, horizontalChange);
        }
        
        if (invert)
            return (-verticalChange, verticalChange);
        else
            return (verticalChange, verticalChange);
    }

    private ResizeThumb CreateThumbPart(Cursor cursor)
    {
        var cornerThumb = new ResizeThumb()
        {
            Cursor = cursor
        };
        cornerThumb.DragStarted += (object sender, DragStartedEventArgs e) => dragStarted = true;
        return cornerThumb;
    }
    
    private void ResizeWidth(double e)
    {
        var deltaHorizontal = Math.Min(-e, childElement.ActualWidth - childElement.MinWidth);
        childElement.Left = childElement.Left + deltaHorizontal * transformOrigin.X * (1 - Math.Cos(angle));
        childElement.Width -= deltaHorizontal;
    }

    private void ResizeX(double e)
    {
        var deltaHorizontal = Math.Min(e, childElement.ActualWidth - childElement.MinWidth);
        childElement.Top = childElement.Top + deltaHorizontal * Math.Sin(angle) - transformOrigin.X * deltaHorizontal * Math.Sin(angle);
        childElement.Left = childElement.Left + deltaHorizontal * Math.Cos(angle) + (transformOrigin.X * deltaHorizontal * (1 - Math.Cos(angle)));
        childElement.Width -= deltaHorizontal;
    }
    private void ResizeHeight(double e)
    {
        var deltaVertical = Math.Min(-e, childElement.ActualHeight - childElement.MinHeight);
        childElement.Top = childElement.Top + deltaVertical * transformOrigin.Y * (1 - Math.Cos(-angle));
        childElement.Height -= deltaVertical;
    }
    private void ResizeY(double e)
    {
        var deltaVertical = Math.Min(e, childElement.ActualHeight - childElement.MinHeight);
        childElement.Top = childElement.Top + deltaVertical * Math.Cos(-angle) + (transformOrigin.Y * deltaVertical * (1 - Math.Cos(-angle)));
        childElement.Left = childElement.Left + deltaVertical * Math.Sin(-angle) - (transformOrigin.Y * deltaVertical * Math.Sin(-angle));
        childElement.Height -= deltaVertical;
    }
    
    // Location of the Adorner relative to the AdornerdElement
    private const int adornerOffset = 0; 
    
    /// <summary>
    /// Used during the layout process, when ResizeAdorner is being rendered on the screen this method gets called.
    /// Gets the visual associated with the adornerVisuals
    /// </summary>
    protected override Size ArrangeOverride(Size finalSize)
    {
        base.ArrangeOverride(finalSize);
        var desireWidth = AdornedElement.DesiredSize.Width;
        var desireHeight = AdornedElement.DesiredSize.Height;
        var adornerWidth = this.DesiredSize.Width;
        var adornerHeight = this.DesiredSize.Height;
        LeftTop.Arrange(new Rect(-adornerWidth / 2 - adornerOffset, -adornerHeight / 2 - adornerOffset, adornerWidth, adornerHeight));
        RightTop.Arrange(new Rect(desireWidth - adornerWidth / 2 + adornerOffset, -adornerHeight / 2 - adornerOffset, adornerWidth, adornerHeight));
        LeftBottom.Arrange(new Rect(-adornerWidth / 2 - adornerOffset, desireHeight - adornerHeight / 2 + adornerOffset, adornerWidth, adornerHeight));
        RightBottom.Arrange(new Rect(desireWidth - adornerWidth / 2 + adornerOffset, desireHeight - adornerHeight / 2 + adornerOffset, adornerWidth, adornerHeight));
        return finalSize;
    }
    protected override int VisualChildrenCount => visualChilderns.Count;
    protected override Visual GetVisualChild(int index) => visualChilderns[index];
}