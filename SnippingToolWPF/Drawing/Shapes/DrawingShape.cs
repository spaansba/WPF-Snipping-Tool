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
        if (e.Property == VisualProperty)
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
            FrameworkPropertyMetadataOptions.AffectsMeasure
        )
    );

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
    #endregion Dependency properties
}