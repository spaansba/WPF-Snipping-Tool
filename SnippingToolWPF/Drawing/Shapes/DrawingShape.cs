using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SnippingToolWPF.Drawing.Shapes;

public abstract class DrawingShape : Decorator
{
    private readonly TextBlock textBlock;
    private readonly Canvas canvas;
    public DrawingShape()
    {
        this.textBlock = SetupTextBlock(this);
        this.canvas = new Canvas() { Children = { textBlock } };
        this.Child = this.canvas;
    }

    protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
    {
        base.OnPropertyChanged(e);
        if (e.Property == VisualProperty) //If Visual gets replaced call OnVisualChanged
            this.OnVisualChanged(e.OldValue as UIElement, e.NewValue as UIElement);
    }

    protected virtual void OnVisualChanged(UIElement? oldValue, UIElement? newValue)
    {
        this.canvas.Children.Clear();
        if (newValue is not null)
            this.canvas.Children.Add(newValue);
        this.canvas.Children.Add(this.textBlock);
    }

    public static readonly DependencyProperty VisualProperty = DependencyProperty.Register(
        nameof(Visual),
        typeof(UIElement),
        typeof(DrawingShape),
        new FrameworkPropertyMetadata(
            default(Visual),
            FrameworkPropertyMetadataOptions.AffectsMeasure));

    public UIElement? Visual
    {
        get => this.GetValue<UIElement?>(VisualProperty);
        set => this.SetValue<UIElement?>(VisualProperty, value);
    }

    private static TextBlock SetupTextBlock(DrawingShape parent)
    {
        var textBlock = new TextBlock();
        textBlock.SetBinding(TextBlock.TextProperty, new Binding() { Source = parent, Path = new PropertyPath(DrawingShape.TextProperty) });
        return textBlock;
    }

    #region Dependency properties
    public static readonly DependencyProperty TextProperty = TextBlock.TextProperty.AddOwner(typeof(DrawingShape));
    public string? Text
    {
        get => this.GetValue<string?>(TextProperty);
        set => this.SetValue<string?>(TextProperty, value);
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

    public static readonly DependencyProperty StrokeThicknessProperty = Shape.StrokeThicknessProperty.AddOwner(typeof(DrawingShape));
    public double? StrokeThickness
    {
        get => this.GetValue<double?>(StrokeThicknessProperty);
        set => this.SetValue<double?>(StrokeThicknessProperty, value);
    }

    public static readonly DependencyProperty StretchProperty = Shape.StretchProperty.AddOwner(typeof(DrawingShape));
    public DependencyProperty? Stretch
    {
        get => this.GetValue<DependencyProperty?>(StretchProperty);
        set => this.SetValue<DependencyProperty?>(StretchProperty, value);
    }

    public static readonly DependencyProperty StrokeDashArrayProperty = Shape.StrokeDashArrayProperty.AddOwner(typeof(DrawingShape));
    public DependencyProperty? StrokeDashArray
    {
        get => this.GetValue<DependencyProperty?>(StrokeDashArrayProperty);
        set => this.SetValue<DependencyProperty?>(StrokeDashArrayProperty, value);
    }

    public static readonly DependencyProperty StrokeDashCapProperty = Shape.StrokeDashCapProperty.AddOwner(typeof(DrawingShape));
    public DependencyProperty? StrokeDashCap
    {
        get => this.GetValue<DependencyProperty?>(StrokeDashCapProperty);
        set => this.SetValue<DependencyProperty?>(StrokeDashCapProperty, value);
    }

    public static readonly DependencyProperty StrokeDashOffsetProperty = Shape.StrokeDashOffsetProperty.AddOwner(typeof(DrawingShape));
    public DependencyProperty? StrokeDashOffset
    {
        get => this.GetValue<DependencyProperty?>(StrokeDashOffsetProperty);
        set => this.SetValue<DependencyProperty?>(StrokeDashOffsetProperty, value);
    }


    public static readonly DependencyProperty StrokeEndLineCapProperty = Shape.StrokeEndLineCapProperty.AddOwner(typeof(DrawingShape));
    public DependencyProperty? StrokeEndLineCap
    {
        get => this.GetValue<DependencyProperty?>(StrokeEndLineCapProperty);
        set => this.SetValue<DependencyProperty?>(StrokeEndLineCapProperty, value);
    }

    public static readonly DependencyProperty StrokeLineJoinProperty = Shape.StrokeLineJoinProperty.AddOwner(typeof(DrawingShape));
    public DependencyProperty? StrokeLineJoin
    {
        get => this.GetValue<DependencyProperty?>(StrokeLineJoinProperty);
        set => this.SetValue<DependencyProperty?>(StrokeLineJoinProperty, value);
    }

    public static readonly DependencyProperty StrokeMiterLimitProperty = Shape.StrokeMiterLimitProperty.AddOwner(typeof(DrawingShape));
    public double? StrokeMiterLimit
    {
        get => this.GetValue<double?>(StrokeMiterLimitProperty);
        set => this.SetValue<double?>(StrokeMiterLimitProperty, value);
    }

    public static readonly DependencyProperty StrokeStartLineCapProperty = Shape.StrokeStartLineCapProperty.AddOwner(typeof(DrawingShape));
    public DependencyProperty? StrokeStartLineCap
    {
        get => this.GetValue<DependencyProperty?>(StrokeStartLineCapProperty);
        set => this.SetValue<DependencyProperty?>(StrokeStartLineCapProperty, value);
    }
    #endregion Dependency properties
}