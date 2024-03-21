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
    private AdornerThumb LeftTop { get; }
    private AdornerThumb RightTop { get; }
    private AdornerThumb RightBottom { get; }
    private AdornerThumb LeftBottom { get; }
    private readonly DrawingShape childElement;
    private bool dragStarted;
    private bool isHorizontalDrag;
    public ResizeAdorner(DrawingShape adornedElement) : base(adornedElement)
    {
        childElement = adornedElement;
        
        LeftTop = CreateThumbPart(Cursors.SizeNWSE);
        RightTop = CreateThumbPart(Cursors.SizeNESW);
        RightBottom = CreateThumbPart(Cursors.SizeNWSE);
        LeftBottom = CreateThumbPart(Cursors.SizeNESW);
        
        visualChilderns = new VisualCollection(this)
        {
            LeftTop,RightTop,RightBottom,LeftBottom
        };
        
        LeftTop.DragDelta += (_, e) =>
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
        RightTop.DragDelta += (_, e) =>
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
        
        LeftBottom.DragDelta += (_, e) =>
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
        
        RightBottom.DragDelta += (_, e) =>
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
    
    private AdornerThumb CreateThumbPart(Cursor cursor)
    {
        var cornerThumb = new AdornerThumb
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