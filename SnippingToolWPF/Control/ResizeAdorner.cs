using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace SnippingToolWPF.Control;

public class ResizeAdorner : Adorner
{
    private const double angle = 0.0;
    private Point transformOrigin = new Point(0, 0);
    private readonly VisualCollection visualChilderns;
    private readonly Thumb LeftTop = new Thumb() { Background = Brushes.Aqua, Width = 25, Height = 25};
    private readonly Thumb RightTop = new Thumb() { Background = Brushes.Aqua, Width = 25, Height = 25};
    private readonly Thumb RightBottom = new Thumb() { Background = Brushes.Aqua, Width = 25, Height = 25};
    private readonly Thumb LeftBottom = new Thumb() { Background = Brushes.Aqua, Width = 25, Height = 25};
    private readonly FrameworkElement childElement;
    private bool dragStarted;
    private bool isHorizontalDrag;
    public ResizeAdorner(UIElement adornedElement) : base(adornedElement)
    {
        childElement = (FrameworkElement)AdornedElement;
        visualChilderns = new VisualCollection(this)
        {
            CreateThumbPart(ref LeftTop),
            CreateThumbPart(ref RightTop),
            CreateThumbPart(ref RightBottom),
            CreateThumbPart(ref LeftBottom)
        };
        LeftTop.DragDelta += (sender, e) =>
        {
            var hor = e.HorizontalChange;
            var vert = e.VerticalChange;
            if (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift))
            {
                if (dragStarted) isHorizontalDrag = Math.Abs(hor) > Math.Abs(vert);
                if (isHorizontalDrag) vert = hor; else hor = vert;
            }
            ResizeX(hor);
            ResizeY(vert);
            dragStarted = false;
            e.Handled = true;
        };
        RightTop.DragDelta += (sender, e) =>
        {
            var hor = e.HorizontalChange;
            var vert = e.VerticalChange;
            System.Diagnostics.Debug.WriteLine(hor + "," + vert + "," + (Math.Abs(hor) > Math.Abs(vert)) + "," + childElement.Height + "," + childElement.Width + "," + dragStarted + "," + isHorizontalDrag);
            if (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift))
            {
                if (dragStarted) isHorizontalDrag = Math.Abs(hor) > Math.Abs(vert);
                if (isHorizontalDrag) vert = -hor; else hor = -vert;
            }
            ResizeWidth(hor);
            ResizeY(vert);
            dragStarted = false;
            e.Handled = true;
        };
        
        LeftBottom.DragDelta += (sender, e) =>
        {
            var hor = e.HorizontalChange;
            var vert = e.VerticalChange;
            System.Diagnostics.Debug.WriteLine(hor + "," + vert + "," + (Math.Abs(hor) > Math.Abs(vert)) + "," + childElement.Height + "," + childElement.Width + "," + dragStarted + "," + isHorizontalDrag);
            if (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift))
            {
                if (dragStarted) isHorizontalDrag = Math.Abs(hor) > Math.Abs(vert);
                if (isHorizontalDrag) vert = -hor; else hor = -vert;
            }
            ResizeX(hor);
            ResizeHeight(vert);
            dragStarted = false;
            e.Handled = true;
        };
        
        RightBottom.DragDelta += (sender, e) =>
        {
            var hor = e.HorizontalChange;
            var vert = e.VerticalChange;
            if (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift))
            {
                if (dragStarted) isHorizontalDrag = Math.Abs(hor) > Math.Abs(vert);
                if (isHorizontalDrag) vert = hor; else hor = vert;
            }
            ResizeWidth(hor);
            ResizeHeight(vert);
            dragStarted = false;
            e.Handled = true;
        };
    }
    
    private void ResizeWidth(double e)
    {
        var deltaHorizontal = Math.Min(-e, childElement.ActualWidth - childElement.MinWidth);
        Canvas.SetTop(childElement, Canvas.GetTop(childElement) - transformOrigin.X * deltaHorizontal * Math.Sin(angle));
        Canvas.SetLeft(childElement, Canvas.GetLeft(childElement) + (deltaHorizontal * transformOrigin.X * (1 - Math.Cos(angle))));
        childElement.Width -= deltaHorizontal;
    }
    private void ResizeX(double e)
    {
        var deltaHorizontal = Math.Min(e, childElement.ActualWidth - childElement.MinWidth);
        Canvas.SetTop(childElement, Canvas.GetTop(childElement) + deltaHorizontal * Math.Sin(angle) - transformOrigin.X * deltaHorizontal * Math.Sin(angle));
        Canvas.SetLeft(childElement, Canvas.GetLeft(childElement) + deltaHorizontal * Math.Cos(angle) + (transformOrigin.X * deltaHorizontal * (1 - Math.Cos(angle))));
        childElement.Width -= deltaHorizontal;
    }
    private void ResizeHeight(double e)
    {
        var deltaVertical = Math.Min(-e, childElement.ActualHeight - childElement.MinHeight);
        Canvas.SetTop(childElement, Canvas.GetTop(childElement) + (transformOrigin.Y * deltaVertical * (1 - Math.Cos(-angle))));
        Canvas.SetLeft(childElement, Canvas.GetLeft(childElement) - deltaVertical * transformOrigin.Y * Math.Sin(-angle));
        childElement.Height -= deltaVertical;
    }
    private void ResizeY(double e)
    {
        var deltaVertical = Math.Min(e, childElement.ActualHeight - childElement.MinHeight);
        Canvas.SetTop(childElement, Canvas.GetTop(childElement) + deltaVertical * Math.Cos(-angle) + (transformOrigin.Y * deltaVertical * (1 - Math.Cos(-angle))));
        Canvas.SetLeft(childElement, Canvas.GetLeft(childElement) + deltaVertical * Math.Sin(-angle) - (transformOrigin.Y * deltaVertical * Math.Sin(-angle)));
        childElement.Height -= deltaVertical;
    }
    
    private Thumb CreateThumbPart(ref Thumb cornerThumb)
    {
        cornerThumb.DragStarted += (object sender, DragStartedEventArgs e) => dragStarted = true;
        return cornerThumb;
    }

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
        LeftTop.Arrange(new Rect(-adornerWidth / 2 - 15, -adornerHeight / 2 - 15, adornerWidth, adornerHeight));
        RightTop.Arrange(new Rect(desireWidth - adornerWidth / 2 + 15, -adornerHeight / 2 - 15, adornerWidth, adornerHeight));
        LeftBottom.Arrange(new Rect(-adornerWidth / 2 - 15, desireHeight - adornerHeight / 2 + 15, adornerWidth, adornerHeight));
        RightBottom.Arrange(new Rect(desireWidth - adornerWidth / 2 + 15, desireHeight - adornerHeight / 2 + 15, adornerWidth, adornerHeight));
        return finalSize;
    }
    protected override int VisualChildrenCount => visualChilderns.Count;
    protected override Visual GetVisualChild(int index) => visualChilderns[index];
}