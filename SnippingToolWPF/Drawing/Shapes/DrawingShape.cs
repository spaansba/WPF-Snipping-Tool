using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Shapes;
using SnippingToolWPF.ExtensionMethods;
using SnippingToolWPF.WPFExtensions;

namespace SnippingToolWPF;

public abstract class DrawingShape : FrameworkElement, IShape, ICloneable<DrawingShape>
{
    static DrawingShape()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(DrawingShape),
            new FrameworkPropertyMetadata(typeof(DrawingShape)));
    }

    public RotateTransform RotateTransform { get; set; } = new RotateTransform();
    
    protected DrawingShape()
    {
        textBlock = SetupTextBlock(this);
        BindingOperations.SetBinding(RotateTransform, RotateTransform.AngleProperty,
            new Binding { Source = this, Path = new PropertyPath(AngleProperty) });
        this.LayoutTransform = RotateTransform;
        this.RenderTransformOrigin = new Point(0.5,0.5);

    }

    #region Text and Visual Setup

    public static readonly DependencyProperty VisualProperty = DependencyProperty.Register(
        nameof(Visual),
        typeof(UIElement),
        typeof(DrawingShape),
        new FrameworkPropertyMetadata(
            default(Visual),
            FrameworkPropertyMetadataOptions.AffectsMeasure));

    // ReSharper disable once NotAccessedField.Local
    private readonly TextBlock textBlock;
    
    public UIElement? Visual
    {
        get => this.GetValue<UIElement?>(VisualProperty);
        init => this.SetValue<UIElement?>(VisualProperty, value);
    }
    
    private static TextBlock SetupTextBlock(DrawingShape parent)
    {
        var textBlock = new TextBlock();
        textBlock.SetBinding(TextBlock.TextProperty,
            new Binding { Source = parent, Path = new PropertyPath(TextProperty) });
        parent.AddVisualChild(textBlock);
        parent.AddLogicalChild(textBlock);
        return textBlock;
    }
    
    public abstract DrawingShape Clone();

    #endregion

    #region On Property Change methods (Visual Change / Selection Change)

    protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
    {
        base.OnPropertyChanged(e);
        if (e.Property == VisualProperty) //If Visual gets replaced call OnVisualChanged
        {
            OnVisualChanged(e.OldValue as UIElement, e.NewValue as UIElement);
        }
        else if (e.Property == IsSelectedProperty)
        {
            OnIsSelectedChange(e.NewValue is true);
        }
    }
    private DrawingShapeAdorner? DrawingShapeAdorner { get; set; }
    private void OnIsSelectedChange(bool isSelected)
    {
        if (isSelected)
        {
            this.DrawingShapeAdorner = new DrawingShapeAdorner(this);
            AdornerLayer.GetAdornerLayer(this)?.Add(this.DrawingShapeAdorner);
        }
        else
        {
            if (this.DrawingShapeAdorner != null) AdornerLayer.GetAdornerLayer(this)?.Remove(this.DrawingShapeAdorner);
        }
    }

    /// <summary>
    /// Add the shape to the canvas on visual changed
    /// </summary>
    private void OnVisualChanged(UIElement? oldValue, UIElement? newValue)
    {
        if (oldValue is not null)
        {
            this.RemoveVisualChild(oldValue);
            this.RemoveLogicalChild(oldValue);
        }

        if (newValue is not null)
        {
            this.AddVisualChild(newValue);
            this.AddLogicalChild(newValue);
        }
        OnVisualChangedOverride(oldValue, newValue);
    }

    protected virtual void OnVisualChangedOverride(UIElement? oldValue, UIElement? newValue)
    {
        // We don't do anything in here this is purely for override so we protect OnVisualChanged making it unoverrideable
    }
    #endregion

    #region Visual Children override FrameworkElement
    protected override IEnumerator LogicalChildren
    {
        get
        {
            if (this.Visual is not null)
                yield return this.Visual;
            yield return this.textBlock;
        }
    }

    protected override int VisualChildrenCount => this.Visual is not null ? 2 : 1;

    protected override Visual GetVisualChild(int index) => index switch
    {
        0 when this.Visual is not null => this.Visual,
        0 => this.textBlock,
        1 when this.Visual is not null => this.textBlock,
        _ => throw new ArgumentOutOfRangeException(nameof(index)),
    };
    
    #endregion

    #region Measure / Arrange

    /// <summary>
    /// Measure lets me tell my parent how much space I want, given a constraint
    ///  Arrange lets my parent tell me how much space I get.
    /// </summary>

    protected override Size MeasureOverride(Size constraint)
    {
        var (visualWidth, visualHeight) = this.Visual.MeasureAndReturn(constraint); //Nullable allowed in extension
        var (textWidth, textHeight) = this.textBlock.MeasureAndReturn(constraint);
        return new Size(Math.Max(visualWidth,textWidth), Math.Max(visualHeight,textHeight));
    }

    /// <summary>
    /// Gets called after MeasureOverride
    /// </summary>
    protected override Size ArrangeOverride(Size arrangeSize)
    {
        //TODO: allow movement of textblock within drawingshape
        this.Visual?.Arrange((new Rect(arrangeSize)));
        this.textBlock.Arrange(new Rect(arrangeSize));
        return arrangeSize;
    }

    #endregion
    
    #region Remaining Dependency properties

    public static readonly DependencyProperty TextProperty = TextBlock.TextProperty.AddOwner(typeof(DrawingShape));

    public string? Text
    {
        get => this.GetValue<string?>(TextProperty);
        set => this.SetValue<string?>(TextProperty, value);
    }

    public static readonly DependencyProperty FontSizeProperty =
        TextBlock.FontSizeProperty.AddOwner(typeof(DrawingShape));

    public double FontSize
    {
        get => this.GetValue<double>(FontSizeProperty);
        set => this.SetValue<double>(FontSizeProperty, value);
    }

    public static readonly DependencyProperty StrokeProperty = Shape.StrokeProperty.AddOwner(typeof(DrawingShape));

    public Brush? Stroke
    {
        get => this.GetValue<Brush?>(StrokeProperty);
        set => this.SetValue<Brush?>(StrokeProperty, value);
    }

    public static readonly DependencyProperty FillProperty = Shape.FillProperty.AddOwner(typeof(DrawingShape));

    public Brush? Fill
    {
        get => this.GetValue<Brush?>(FillProperty);
        set => this.SetValue<Brush?>(FillProperty, value);
    }

    public static readonly DependencyProperty StrokeThicknessProperty =
        Shape.StrokeThicknessProperty.AddOwner(typeof(DrawingShape));

    public double StrokeThickness
    {
        get => this.GetValue<double>(StrokeThicknessProperty);
        set => this.SetValue<double>(StrokeThicknessProperty, value);
    }

    public static readonly DependencyProperty StretchProperty = Shape.StretchProperty.AddOwner(typeof(DrawingShape));

    public Stretch? Stretch
    {
        get => this.GetValue<Stretch?>(StretchProperty);
        set => this.SetValue<Stretch?>(StretchProperty, value);
    }

    public static readonly DependencyProperty StrokeDashArrayProperty =
        Shape.StrokeDashArrayProperty.AddOwner(typeof(DrawingShape));

    public DoubleCollection? StrokeDashArray
    {
        get => this.GetValue<DoubleCollection?>(StrokeDashArrayProperty);
        set => this.SetValue<DoubleCollection?>(StrokeDashArrayProperty, value);
    }

    public static readonly DependencyProperty StrokeDashCapProperty =
        Shape.StrokeDashCapProperty.AddOwner(typeof(DrawingShape));

    public PenLineCap? StrokeDashCap
    {
        get => this.GetValue<PenLineCap?>(StrokeDashCapProperty);
        set => this.SetValue<PenLineCap?>(StrokeDashCapProperty, value);
    }

    public static readonly DependencyProperty StrokeDashOffsetProperty =
        Shape.StrokeDashOffsetProperty.AddOwner(typeof(DrawingShape));

    public double StrokeDashOffset
    {
        get => this.GetValue<double>(StrokeDashOffsetProperty);
        set => this.SetValue<double>(StrokeDashOffsetProperty, value);
    }


    public static readonly DependencyProperty StrokeEndLineCapProperty =
        Shape.StrokeEndLineCapProperty.AddOwner(typeof(DrawingShape));

    public PenLineCap? StrokeEndLineCap
    {
        get => this.GetValue<PenLineCap?>(StrokeEndLineCapProperty);
        set => this.SetValue<PenLineCap?>(StrokeEndLineCapProperty, value);
    }

    public static readonly DependencyProperty StrokeLineJoinProperty =
        Shape.StrokeLineJoinProperty.AddOwner(typeof(DrawingShape));

    public PenLineJoin? StrokeLineJoin
    {
        get => this.GetValue<PenLineJoin?>(StrokeLineJoinProperty);
        set => this.SetValue<PenLineJoin?>(StrokeLineJoinProperty, value);
    }

    public static readonly DependencyProperty StrokeMiterLimitProperty =
        Shape.StrokeMiterLimitProperty.AddOwner(typeof(DrawingShape));

    public double StrokeMiterLimit
    {
        get => this.GetValue<double>(StrokeMiterLimitProperty);
        set => this.SetValue<double>(StrokeMiterLimitProperty, value);
    }

    public static readonly DependencyProperty StrokeStartLineCapProperty =
        Shape.StrokeStartLineCapProperty.AddOwner(typeof(DrawingShape));

    public PenLineCap? StrokeStartLineCap
    {
        get => this.GetValue<PenLineCap?>(StrokeStartLineCapProperty);
        set => this.SetValue<PenLineCap?>(StrokeStartLineCapProperty, value);
    }

    public static readonly DependencyProperty
        FillRuleProperty = Polygon.FillRuleProperty.AddOwner(typeof(DrawingShape));

    public FillRule? FillRule
    {
        get => this.GetValue<FillRule?>(FillRuleProperty);
        set => this.SetValue<FillRule?>(FillRuleProperty, value);
    }

    public static readonly DependencyProperty LeftProperty = Canvas.LeftProperty.AddOwner(typeof(DrawingShape));

    public double Left
    {
        get => this.GetValue<double>(LeftProperty);
        set => this.SetValue<double?>(LeftProperty, value);
    }

    public static readonly DependencyProperty TopProperty = Canvas.TopProperty.AddOwner(typeof(DrawingShape));

    public double  Top
    {
        get => this.GetValue<double>(TopProperty);
        set => this.SetValue<double>(TopProperty, value);
    }
    
    public static readonly DependencyProperty BottomProperty = Canvas.BottomProperty.AddOwner(typeof(DrawingShape));

    public double  Bottom
    {
        get => this.GetValue<double>(BottomProperty);
        set => this.SetValue<double>(BottomProperty, value);
    }

    public static readonly DependencyProperty AngleProperty = RotateTransform.AngleProperty.AddOwner(
        typeof(DrawingShape)
        , new PropertyMetadata(0.0));

    public double Angle
    {
        get => this.GetValue<double>(AngleProperty);
        set => this.SetValue<double>(AngleProperty, value);
    }
    
    public static readonly DependencyProperty IsSelectedProperty =
        Selector.IsSelectedProperty.AddOwner(typeof(DrawingShape));

    public bool IsSelected
    {
        get => this.GetValue<bool>(IsSelectedProperty);
        set => this.SetValue<bool>(IsSelectedProperty, value);
    }

    #endregion Dependency properties
}