using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SnippingToolWPF.WPFExtensions;

namespace SnippingToolWPF.Control;

public class DrawingCanvasListBoxItem : ListBoxItem
{
    /// <summary>
    /// Override the default style for this type (ListBoxItem), now use (DrawingCanvasListBoxItem)
    /// </summary>
    static DrawingCanvasListBoxItem()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(DrawingCanvasListBoxItem),
            new FrameworkPropertyMetadata(typeof(DrawingCanvasListBoxItem)));
    }

    public DrawingCanvasListBoxItem()
    {
        this.ResizeAdorner = new ResizeAdorner(this);
    }

    #region DrawingCanvas events
    internal DrawingCanvas? DrawingCanvas { get; set; }
    /// <summary>
    ///   when a shape on the convas gets clicked, notify the drawing canvas so that it can do the Mouse events over there
    /// </summary>
    protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
    {
        base.OnMouseLeftButtonDown(e);

        // Get the Position of the mouse on the Drawing Canvas not on the DrawingCanvasListBoxItem
        var drawingCanvasPoint = e.GetPosition(DrawingCanvas);

        DrawingCanvas?.OnItemMouseEvent(this, e, drawingCanvasPoint);
    }

    /// <summary>
    ///     when item/shape on the canvas gets hovered over while mouse is down, notify drawing canvas so that it can do the
    ///     Mouse events over there
    /// </summary>
    /// <param name="e"></param>
    protected override void OnMouseMove(MouseEventArgs e)
    {
        base.OnMouseMove(e);

        // Get the Position of the mouse on the Drawing Canvas not on the DrawingCanvasListBoxItem
        var drawingCanvasPoint = e.GetPosition(DrawingCanvas);

        DrawingCanvas?.OnItemMouseEvent(this, e, drawingCanvasPoint);
    }

    protected override void OnSelected(RoutedEventArgs e)
    {
        base.OnSelected(e);
        Debug.WriteLine("Select");
    }

    #endregion
    
    #region Adorner Layers

    public static readonly DependencyProperty IsAdornerLayerVisibleProperty =
        DependencyProperty.RegisterAttached(
            nameof(IsAdornerLayerVisible),
            typeof(bool),
            typeof(DrawingCanvasListBoxItem),
            new FrameworkPropertyMetadata(false));
    
    public bool IsAdornerLayerVisible
    {
        get => this.GetValue<bool>(IsAdornerLayerVisibleProperty);
        set => SetValue(IsAdornerLayerVisibleProperty, value);
    }

    private ResizeAdorner ResizeAdorner { get; }
    
    protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
    {
        base.OnPropertyChanged(e);
        if (e.Property == IsAdornerLayerVisibleProperty) 
        {
            if (IsAdornerLayerVisible)
            { 
             //   AdornerLayer.GetAdornerLayer(this)?.Add(this.ResizeAdorner);
            }
            else
            {
            //   AdornerLayer.GetAdornerLayer(this)?.Remove(this.ResizeAdorner);
            }
        }
    }

    #endregion
    
}