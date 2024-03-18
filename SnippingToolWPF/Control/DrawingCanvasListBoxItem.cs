using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

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

    internal DrawingCanvas? DrawingCanvas { get; set; }

    protected override void OnRender(DrawingContext context)
    {
        // Find first Adorner Layer in Visual tree and add the custom ResizeAdorner
      //  AdornerLayer.GetAdornerLayer(this)?.Add(new ResizeAdorner(this));
        base.OnRender(context);
    }

/// <summary>
    ///     when a shape on the convas gets clicked, notify the drawing canvas so that it can do the Mouse events over there
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
}