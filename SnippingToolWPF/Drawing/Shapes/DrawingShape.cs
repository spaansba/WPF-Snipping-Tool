using System.Collections;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;
using SnippingToolWPF.Common;
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
    
    protected DrawingShape()
    {
        textBlock = SetupTextBlock(this);

        var translateTransform = new TranslateTransform();
        var rotateTransform = new RotateTransform();
        
        BindingOperations.SetBinding(rotateTransform, RotateTransform.AngleProperty,
            new Binding { Source = this, Path = new PropertyPath(AngleProperty) });
        
        // Allowing Rotation of the Shape (not the adorner)
        this.RenderTransform = new TransformGroup()
        {
            Children = { translateTransform, rotateTransform }
        };

        this.Angle = 0;
        this.RenderTransformOrigin = new Point(0.5, 0.5);
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
        textBlock.SetBinding(TextBlock.TextProperty, new Binding { Source = parent, Path = new PropertyPath(AngleProperty), StringFormat = "{0:G3}°"});
        textBlock.HorizontalAlignment = HorizontalAlignment.Center;
        textBlock.VerticalAlignment = VerticalAlignment.Center;
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
        if (e.Property == IsListBoxSelectedProperty)
        {
            Debug.WriteLine($"propety {e.Property} new Value {e.NewValue} old value {e.OldValue}");
            OnShapeSelectedChange(e.NewValue is true);
        }
    }
    
    #region Adorners

    private DrawingShapeAdorner? DrawingShapeAdorner { get; set; }

    private void OnShapeSelectedChange(bool isSelected)
    {
        if (isSelected)
        {
          //  CreateAdorners();
            
            /// Dispatcher Allows us to select a shape on drawing
            Dispatcher.CurrentDispatcher.Invoke(AddAdorners, System.Windows.Threading.DispatcherPriority.Background);
        }
        else
        {
            RemoveAdorners();
        }
    }
    private void CreateAdorners() => DrawingShapeAdorner ??= new DrawingShapeAdorner(this);
    
    private void AddAdorners()
    {
        AdornerLayer.GetAdornerLayer(this)?.Add(this.DrawingShapeAdorner ?? throw new InvalidOperationException());
        this.DrawingShapeAdorner?.AdornerVisibility(true);
    }

    private void RemoveAdorners()
    {
        if (this.DrawingShapeAdorner != null) AdornerLayer.GetAdornerLayer(this)?.Remove(this.DrawingShapeAdorner);
    }
    
    /// <summary>
    /// For when the Shape is being changed, for example being rotated / resized / moved
    /// </summary>
    internal void StartChanging()
    {
        this.IsChanging = true;
        this.DrawingShapeAdorner?.AdornerVisibility(false);
        RegenerateDrawingShapeRectanglePoints(this);
    }
    
    /// <summary>
    /// For when the Shape stops being changed, for example being rotated / resized / moved
    /// </summary>
    internal void FinishChanging()
    {
        this.IsChanging = false;
        this.DrawingShapeAdorner?.AdornerVisibility(true);
        RegenerateDrawingShapeRectanglePoints(this);
    }

    #endregion
    
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

    #region Points of Rectangle Around DrawingShape

    public Point BottomRightPoint { get; private set; }
    public Point TopLeftPoint { get; private set; }
    public Point TopRightPoint { get; private set; }
    public Point BottomLeftPoint { get; private set; }
    public Point TopPoint { get; private set; }
    public Point BottomPoint { get; private set; }
    public Point RightPoint { get; private set; }
    public Point LeftPoint { get; private set; }

    /// <summary>
    ///     Calculates the points around the rectangle of the DrawingShape
    ///     These Points are relative to the DrawingCanvas (top left of DrawingCanvas = TopLeftPoint 0,0)
    /// </summary>
    public static void RegenerateDrawingShapeRectanglePoints(DrawingShape shape)
    {
        var halfHeight = shape.Height / 2;
        var halfWidth = shape.Width / 2;
                
        shape.TopPoint = new Point(shape.Left + halfWidth, shape.Top);
        shape.BottomPoint = new Point(shape.Left + halfWidth, shape.Top + shape.Height);
        shape.RightPoint = new Point(shape.Left + shape.Width, shape.Top + halfHeight);
        shape.LeftPoint = new Point(shape.Left, shape.Top + halfHeight);

        shape.TopLeftPoint = new Point(shape.Left, shape.Top);
        shape.TopRightPoint = new Point(shape.Left + shape.Width, shape.Top);
        shape.BottomLeftPoint = new Point(shape.Left, shape.Top + shape.Height);
        shape.BottomRightPoint = new Point(shape.Left + shape.Width, shape.Top + shape.Height);

    }

    #endregion
    
    #region Visual Children override FrameworkElement & Measure / Arrange
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
    
    /// <summary>
    /// Only a single DrawingShape will ever have this as True as that is the last ListBox Selected
    /// For multi Select check IsShapeSelectedProperty
    /// </summary>
    public static readonly DependencyProperty IsListBoxSelectedProperty =
        Selector.IsSelectedProperty.AddOwner(typeof(DrawingShape));

    public bool IsListBoxSelected
    {
        get => this.GetValue<bool>(IsListBoxSelectedProperty);
        set => this.SetValue<bool>(IsListBoxSelectedProperty, value);
    }
    

    private static readonly DependencyProperty IsChangingProperty = DependencyProperty.Register(
        name: nameof(IsChanging),
        propertyType: typeof(bool),
        ownerType: typeof(DrawingShape),
        typeMetadata: new FrameworkPropertyMetadata(Boxes.False)
    );

    public bool IsChanging
    {
        get => this.GetValue<bool>(IsChangingProperty);
        private set => this.SetValue<bool>(IsChangingProperty, value);
    }
    
    #endregion Dependency properties
}