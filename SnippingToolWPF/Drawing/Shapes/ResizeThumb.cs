using System.Diagnostics;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using SnippingToolWPF.Common;
using SnippingToolWPF.ExtensionMethods;

namespace SnippingToolWPF;

public class ResizeThumb : AnchoredThumb
{
    //TODO make Resize work within the Redo / Undo Stack
    //TODO create a Locked aspect ratio class
    //TODO make it so if the element is to small the thumbs stick out a bit instead of being cramped up
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
                this.Cursor = Cursors.SizeNWSE;
                break;
            case ThumbLocation.TopRight:
                this.DragDelta += OnTopRightDragDelta;
                this.Cursor = Cursors.SizeNESW;
                break;
            case ThumbLocation.BottomRight:
                this.DragDelta += OnBottomRightDragDelta;
                this.Cursor = Cursors.SizeNWSE;
                break;
            case ThumbLocation.BottomLeft:
                this.DragDelta += OnBottomLeftDragDelta;
                this.Cursor = Cursors.SizeNESW;
                break;
            case ThumbLocation.Top:
                this.DragDelta += OnTopDragDelta;
                this.Cursor = Cursors.SizeNS;
                break;
            case ThumbLocation.Bottom:
                this.DragDelta += OnBottomDragDelta;
                this.Cursor = Cursors.SizeNS;
                break;
            case ThumbLocation.Left:
                this.DragDelta += OnLeftDragDelta;
                this.Cursor = Cursors.SizeWE;
                break;
            case ThumbLocation.Right:
                this.DragDelta += OnRightDragDelta;
                this.Cursor = Cursors.SizeWE;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(thumbCornerOrSide), thumbCornerOrSide, null);
        }
        this.DragStarted += OnDragStarted;
        this.DragCompleted += OnDragCompleted;
    }

    #region Drag Delta Events per Thumb
    
    private void OnDragStarted(object sender, DragStartedEventArgs e)
    {
        this.childElement.StartChanging();
        // topLeft.TransformPoint(childElement, "Top Left Transformed");
        // Debug.WriteLine($"Top left Normal {topLeft}");
        // topRight.TransformPoint(childElement, "Top Right Transformed");
        // Debug.WriteLine($"Top right Normal {topRight}");
        // bottomLeft.TransformPoint(childElement, "Bottom Left Transformed");
        // Debug.WriteLine($"Bottom Left Normal {bottomLeft}");
        // bottomRight.TransformPoint(childElement, "Bottom Right Transformed");
        // Debug.WriteLine($"Bottom Right Normal {bottomRight}");
        // Output the calculated points for debugging
        
        Debug.WriteLine("");
        
       
    }

    private void OnDragCompleted(object sender, DragCompletedEventArgs e)
    {
        this.childElement.FinishChanging();
    }
    
    private void OnTopDragDelta(object _, DragDeltaEventArgs e)
    {
        var halfWidth = childElement.Width / 2;
        var currentPoint = new Point(childElement.Left + halfWidth, childElement.Top + e.VerticalChange);
        childElement.CreateRectFromOppositeCenters(currentPoint, childElement.BottomPoint, halfWidth, false);
    }

    private void OnBottomDragDelta(object _, DragDeltaEventArgs e)
    {
        var halfWidth = childElement.Width / 2;
        var currentPoint = new Point(childElement.Left + halfWidth, childElement.Top + childElement.Height + e.VerticalChange);
        childElement.CreateRectFromOppositeCenters(currentPoint, childElement.TopPoint, halfWidth, false);
    }
    
    private void OnLeftDragDelta(object _, DragDeltaEventArgs e)
    {
        var halfHeight = childElement.Height / 2;
        var currentPoint = new Point(childElement.Left + e.HorizontalChange, childElement.Top + halfHeight);
        childElement.CreateRectFromOppositeCenters(currentPoint, childElement.RightPoint, halfHeight, true);
    }

    private void OnRightDragDelta(object _, DragDeltaEventArgs e)
    {
        var halfHeight = childElement.Height / 2;
        var currentPoint = new Point(childElement.Left + childElement.Width + e.HorizontalChange, childElement.Top + halfHeight);
        childElement.CreateRectFromOppositeCenters(currentPoint, childElement.LeftPoint, halfHeight, true);
    }
    
    private void OnTopLeftDragDelta(object _, DragDeltaEventArgs e)
    {
        var (hor, vert) = GetHorizontalVerticalChange(e, false);
        var currentPoint = new Point(childElement.Left + hor, childElement.Top + vert);
        childElement.CreateRectFromOppositeCorners(currentPoint, childElement.BottomRightPoint);
    }

    private void OnBottomRightDragDelta(object _, DragDeltaEventArgs e)
    {
        var (hor, vert) = GetHorizontalVerticalChange(e, false);
        var currentPoint = new Point(childElement.Left + childElement.Width + hor, childElement.Top + childElement.Height + vert);
        childElement.CreateRectFromOppositeCorners(currentPoint, childElement.TopLeftPoint);
    }
    
    private void OnBottomLeftDragDelta(object _, DragDeltaEventArgs e)
    {
        var (hor, vert) = GetHorizontalVerticalChange(e, true);
        var currentPoint = new Point(childElement.Left + hor, childElement.Top + childElement.Height + vert);
        childElement.CreateRectFromOppositeCorners(currentPoint, childElement.TopRightPoint);
    }
    
    private void OnTopRightDragDelta(object _, DragDeltaEventArgs e)
    {
        var (hor, vert) = GetHorizontalVerticalChange(e, true);
        var currentPoint = new Point(childElement.Left + childElement.Width + hor, childElement.Top + vert);
        childElement.CreateRectFromOppositeCorners(currentPoint, childElement.BottomLeftPoint);
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
        if (KeyboardHelper.IsShiftPressed())
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