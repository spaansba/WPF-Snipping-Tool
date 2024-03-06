using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SnippingToolWPF.Drawing.Shapes;

public abstract class DrawingShape : FrameworkElement
{
    private readonly TextBlock textBlock;
    private readonly Shape shape;
    public DrawingShape()
    {
        this.textBlock = SetUpTextBlock(this);
        this.shape = SetUpShape(this);
    }
    #region Shape / Textblock setup
    private static TextBlock SetUpTextBlock(DrawingShape parent)
    {
        throw new NotImplementedException();
    }

    private static Shape SetUpShape(DrawingShape parnet)
    {
        throw new NotImplementedException();
    }
    #endregion

    #region Add Dependency Properties

    public static readonly DependencyProperty TextProperty = TextBlock.TextProperty.AddOwner(typeof(DrawingShape));
    public string? Text
    {
        get => this.GetValue<string?>(TextProperty);
        set => this.SetValue(TextProperty, value);
    }

    public static readonly DependencyProperty StrokeProperty = Shape.StrokeProperty.AddOwner(typeof(DrawingShape));
    public Brush? Stroke
    {
        get => this.GetValue<Brush?>(StrokeProperty);
        set => this.SetValue(StrokeProperty, value);
    }

    #endregion

    #region Measureing

    protected override Size MeasureOverride(Size availableSize)
    {
        this.shape.Measure(availableSize);
        this.textBlock.Measure(availableSize);
        return new Size(
            Math.Max(this.shape.DesiredSize.Width, this.textBlock.DesiredSize.Width),
            Math.Max(this.shape.DesiredSize.Height, this.textBlock.DesiredSize.Height));
    }

    protected override int VisualChildrenCount => 2;

    protected override Size ArrangeOverride(Size finalSize)
    {
        this.shape.Arrange(new Rect(finalSize));
        this.textBlock.Arrange(new Rect(finalSize));
        return new Size(
            Math.Max(this.shape.DesiredSize.Width, this.textBlock.DesiredSize.Width),
            Math.Max(this.shape.DesiredSize.Height, this.textBlock.DesiredSize.Height));
    }

    #endregion

    #region Children

    protected override Visual GetVisualChild(int index) => index switch
    {
        0 => this.shape,
        1 => this.textBlock,
        _ => throw new ArgumentOutOfRangeException(nameof(index), index, default),
    };

    protected override IEnumerator? LogicalChildren
    {
        get
        {
            yield return this.shape;
            yield return this.textBlock;
        }
    }
    #endregion
}
