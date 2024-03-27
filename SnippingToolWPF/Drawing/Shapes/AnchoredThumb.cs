using System.Windows;
using System.Windows.Controls.Primitives;
using SnippingToolWPF.ExtensionMethods;

namespace SnippingToolWPF;

public abstract class AnchoredThumb : Thumb
{
    private readonly Vector regularOffset;
    private ThumbLocation AnchorPoint { get; }

    protected AnchoredThumb(ThumbLocation anchorPoint, Point offset) {
        this.AnchorPoint = anchorPoint;
        this.regularOffset = new Vector(offset.X, offset.Y);
    }
    
    internal void ArrangeIntoParent(Size parentSize)
    {
        var thumbLocation = parentSize.GetCornerOrSide(this.AnchorPoint, this.DesiredSize.Width);
        thumbLocation += this.regularOffset;
        this.Arrange(new Rect(thumbLocation, this.DesiredSize));
     
    }

    internal void ArrangeIntoParentWithOffset(Size parentSize, double offset)
    {
        var thumbLocation = parentSize.GetCornerOrSide(this.AnchorPoint, this.DesiredSize.Width);
        var extraOffset = CalculateExtraOffset(offset);
        thumbLocation = thumbLocation + this.regularOffset + extraOffset;
        this.Arrange(new Rect(thumbLocation, this.DesiredSize));
    }
    
    /// <summary>
    /// Adds X offset to the thumb based on location of the thumb.
    /// Exmaple usage: For when a visual gets to small add a standard offset
    /// </summary>
    private Vector CalculateExtraOffset(double offset) => this.AnchorPoint switch
    {
        ThumbLocation.TopLeft => new Vector(-offset,-offset),
        ThumbLocation.Top => new Vector(0,-offset),
        ThumbLocation.TopRight => new Vector(offset,-offset),
        ThumbLocation.Right => new Vector(offset,0),
        ThumbLocation.BottomRight => new Vector(offset,offset),
        ThumbLocation.Bottom => new Vector(0,offset),
        ThumbLocation.BottomLeft => new Vector(- offset,offset),
        ThumbLocation.Left => new Vector(-offset,0),
        _ => throw new ArgumentOutOfRangeException()
    };
    
    
    
    internal void Hide() => this.Opacity = 0;
    
    internal void UnHide() => this.Opacity = 0.7;
    
}