using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using SnippingToolWPF.ExtensionMethods;

namespace SnippingToolWPF;

public class ResizeThumb : AnchoredThumb
{
    //TODO make Resize work within the Redo / Undo Stack
    //TODO create a Locked aspect ratio class
    private readonly bool dragStarted = false; // Dont ask why this is needed
    private bool isHorizontalDrag;
    static ResizeThumb()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(ResizeThumb), 
            new FrameworkPropertyMetadata(typeof(ResizeThumb)));
    }
    
    private readonly DrawingShape childElement;
    
    public ResizeThumb(DrawingShape adornedElement, ThumbLocation thumbCornerOrSide) : base(thumbCornerOrSide, new Point(0,0))
    {
        this.childElement = adornedElement;

        switch (thumbCornerOrSide)
        {
            case ThumbLocation.TopLeft:
                this.DragDelta += OnTopLeftDragDelta;
                break;
            case ThumbLocation.TopRight:
                this.DragDelta += OnTopRightDragDelta;
                break;
            case ThumbLocation.BottomRight:
                this.DragDelta += OnBottomRightDragDelta;
                break;
            case ThumbLocation.BottomLeft:
                this.DragDelta += OnBottomLeftDragDelta;
                break;
            case ThumbLocation.Top:
                this.DragDelta += OnTopDragDelta;
                break;
            case ThumbLocation.Bottom:
                this.DragDelta += OnBottomDragDelta;
                break;
            case ThumbLocation.Left:
                this.DragDelta += OnLeftDragDelta;
                break;
            case ThumbLocation.Right:
                this.DragDelta += OnRightDragDelta;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(thumbCornerOrSide), thumbCornerOrSide, null);
        }
        this.DragStarted += OnDragStarted;
    }

    #region Drag Delta Events per Thumb

    private Point bottomRight;
    private Point topLeft;
    private Point topRight;
    private Point bottomLeft;
    private void OnDragStarted(object _, DragStartedEventArgs e)
    {
        bottomRight = new Point(childElement.Left + childElement.Width, childElement.Top + childElement.Height);
        topLeft = new Point(childElement.Left, childElement.Top);
        topRight = new Point(childElement.Left + childElement.Width, childElement.Top);
        bottomLeft = new Point(childElement.Left, childElement.Top + childElement.Height);
    } 

    private void OnTopDragDelta(object _, DragDeltaEventArgs e)
    {
        var currentPoint = new Point(childElement.Left, childElement.Top + e.VerticalChange);
        childElement.SetOppositeCorners(currentPoint, bottomRight);
    }

    private void OnBottomDragDelta(object _, DragDeltaEventArgs e)
    {
        var currentPoint = new Point(childElement.Left, childElement.Top + childElement.Height + e.VerticalChange);
        childElement.SetOppositeCorners(currentPoint, topRight);
    }

    private void OnLeftDragDelta(object _, DragDeltaEventArgs e)
    {
        var currentPoint = new Point(childElement.Left + e.HorizontalChange, childElement.Top);
        childElement.SetOppositeCorners(currentPoint, bottomRight);
    }

    private void OnRightDragDelta(object _, DragDeltaEventArgs e)
    {
        var currentPoint = new Point(childElement.Left + childElement.Width + e.HorizontalChange, childElement.Top);
        childElement.SetOppositeCorners(currentPoint, bottomLeft);
    }
    
    private void OnTopLeftDragDelta(object _, DragDeltaEventArgs e)
    {
        var (hor, vert) = GetHorizontalVerticalChange(e, false);
        var currentPoint = new Point(childElement.Left + hor, childElement.Top + vert);
        childElement.SetOppositeCorners(currentPoint, bottomRight);
    }

    private void OnBottomRightDragDelta(object _, DragDeltaEventArgs e)
    {
        var (hor, vert) = GetHorizontalVerticalChange(e, false);
        var currentPoint = new Point(childElement.Left + childElement.Width + hor, childElement.Top + childElement.Height + vert);
        childElement.SetOppositeCorners(currentPoint, topLeft);
    }
    
    private void OnBottomLeftDragDelta(object _, DragDeltaEventArgs e)
    {
        var (hor, vert) = GetHorizontalVerticalChange(e, true);
        var currentPoint = new Point(childElement.Left + hor, childElement.Top + childElement.Height + vert);
        childElement.SetOppositeCorners(currentPoint, topRight);
    }
    
    private void OnTopRightDragDelta(object _, DragDeltaEventArgs e)
    {
        var (hor, vert) = GetHorizontalVerticalChange(e, true);
        var currentPoint = new Point(childElement.Left + childElement.Width + hor, childElement.Top + vert);
        childElement.SetOppositeCorners(currentPoint, bottomLeft);
    }
    
    #endregion

    #region Get Correct Horitonzal / Vertical Change while draggine any thumb

    /// <summary>
    /// Returns the horizontal/vertical change of the thumb location,
    /// If the user holds Shift will return the change while retaining the perfect aspectratio
    /// </summary>
    /// <param name="e">DragDeltaEventArgs</param>
    /// <param name="invert">BottomRight / TopLeft needs to be inverted</param>
    /// <returns></returns>
    private (double hor, double vert) GetHorizontalVerticalChange(DragDeltaEventArgs e, bool invert)
    {
        var hor = e.HorizontalChange;
        var vert = e.VerticalChange;
        if (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift))
            (hor, vert) = RetainAspectRatioCalculation(hor, vert, invert);
        return (hor, vert);
    }
    
    /// <summary>
    /// Calculates the new horizontal and vertical changes to retain the aspect ratio of the adorned element during resizing (while holding shift).
    /// </summary>
    private (double newHorizontalChange, double newVerticalChange) RetainAspectRatioCalculation(double horizontalChange,
        double verticalChange, bool invert)
    {
        //Mike no clue why but I cant remove dragStarted, even though its always false
        if (dragStarted)(isHorizontalDrag) = Math.Abs(horizontalChange) > Math.Abs(verticalChange);
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

    #endregion
}