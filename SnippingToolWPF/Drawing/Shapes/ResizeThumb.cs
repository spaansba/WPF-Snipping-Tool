using System.Diagnostics;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace SnippingToolWPF;

public class ResizeThumb : Thumb
{
    private const double angle = 0.0;
    private Point transformOrigin = new Point(0, 0);
    private bool dragStarted;
    private bool isHorizontalDrag;
    static ResizeThumb()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(ResizeThumb), 
            new FrameworkPropertyMetadata(typeof(ResizeThumb)));
    }
    
    private readonly DrawingShape childElement;
    
    public ResizeThumb(DrawingShape adornedElement, CornerOrSide thumbCornerOrSide)
    {
        this.childElement = adornedElement;

        switch (thumbCornerOrSide)
        {
            case CornerOrSide.TopLeft:
                this.DragDelta += OnTopLeftDragDelta;
                break;
            case CornerOrSide.TopRight:
                this.DragDelta += OnTopRightDragDelta;
                break;
            case CornerOrSide.BottomRight:
                this.DragDelta += OnBottomRightDragDelta;
                break;
            case CornerOrSide.BottomLeft:
                this.DragDelta += OnBottomLeftDragDelta;
                break;
            case CornerOrSide.Top:
                this.DragDelta += OnTopDragDelta;
                break;
            case CornerOrSide.Bottom:
                this.DragDelta += OnBottomDragDelta;
                break;
            case CornerOrSide.Left:
                this.DragDelta += OnLeftDragDelta;
                break;
            case CornerOrSide.Right:
                this.DragDelta += OnRightDragDelta;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(thumbCornerOrSide), thumbCornerOrSide, null);
        }
        
        this.DragStarted += (object sender, DragStartedEventArgs e) => dragStarted = true;
    }

    #region Drag Delta Events per Thumb
    
    private void OnTopDragDelta(object _, DragDeltaEventArgs e)
    {
        ResizeY(e.VerticalChange);
        FinishDragDelta(e);
    }
    private void OnBottomDragDelta(object _, DragDeltaEventArgs e)
    {
        ResizeHeight(e.VerticalChange);
        FinishDragDelta(e);
    }
    private void OnLeftDragDelta(object _, DragDeltaEventArgs e)
    {
        ResizeX(e.HorizontalChange);
        FinishDragDelta(e);
    }
    private void OnRightDragDelta(object _, DragDeltaEventArgs e)
    {
        ResizeWidth(e.HorizontalChange);
        FinishDragDelta(e);
    }
    
    private void OnTopLeftDragDelta(object _, DragDeltaEventArgs e)
    {
        var (hor, vert) = GetHorizontalVerticalChange(e, false);
        ResizeX(hor);
        ResizeY(vert);
        FinishDragDelta(e);
    }

    private void OnBottomRightDragDelta(object _, DragDeltaEventArgs e)
    {
        var (hor, vert) = GetHorizontalVerticalChange(e, false);
        ResizeWidth(hor);
        ResizeHeight(vert);
        FinishDragDelta(e);
    }
    
    private void OnBottomLeftDragDelta(object _, DragDeltaEventArgs e)
    {
        var (hor, vert) = GetHorizontalVerticalChange(e, true);
        ResizeX(hor);
        ResizeHeight(vert);
        FinishDragDelta(e);
    }

    private void OnTopRightDragDelta(object _, DragDeltaEventArgs e)
    {
        var (hor, vert) = GetHorizontalVerticalChange(e, true);
        ResizeWidth(hor);
        ResizeY(vert);
        FinishDragDelta(e);
    }
    
    /// <summary>
    /// puts dragStarted to false and makes sure the DragDeltaEventArgs is handled
    /// </summary>
    private void FinishDragDelta(DragDeltaEventArgs e)
    {
        dragStarted = false;
        e.Handled = true;
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

    #endregion
    
    #region Resize Width / Height / X / Y methods

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
        
        Debug.WriteLine($"height:{childElement.Height}, Width:{childElement.Width} Top:{childElement.Top}, Left{childElement.Left}");
    }
    private void ResizeY(double e)
    {
        var deltaVertical = Math.Min(e, childElement.ActualHeight - childElement.MinHeight);
        childElement.Top = childElement.Top + deltaVertical * Math.Cos(-angle) + (transformOrigin.Y * deltaVertical * (1 - Math.Cos(-angle)));
        childElement.Left = childElement.Left + deltaVertical * Math.Sin(-angle) - (transformOrigin.Y * deltaVertical * Math.Sin(-angle));
        childElement.Height -= deltaVertical;
    }

    #endregion
    
}