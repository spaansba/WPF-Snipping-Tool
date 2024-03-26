using System.Windows;
using System.Windows.Controls.Primitives;
using SnippingToolWPF.ExtensionMethods;

namespace SnippingToolWPF;

public abstract class AnchoredThumb : Thumb
{
    private readonly Vector offset;

    private ThumbLocation AnchorPoint { get; }

    public Point CurrentThumbLocation { get; set; }
    
    protected AnchoredThumb(ThumbLocation anchorPoint, Point offset) {
        this.AnchorPoint = anchorPoint;
        this.offset = new Vector(offset.X, offset.Y);
    }

    internal void ArrangeIntoParent(Size parentSize)
    {
        var centerPoint = parentSize.GetCornerOrSide(this.AnchorPoint,this.DesiredSize.Width);
        var offsetPoint = centerPoint + this.offset;
        CurrentThumbLocation = offsetPoint;
        this.Arrange(new Rect(offsetPoint,this.DesiredSize));
    //    this.Arrange(offsetPoint.ToRectangleFromCenter(this.DesiredSize));
    }
}