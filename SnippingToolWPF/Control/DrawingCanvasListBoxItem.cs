using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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

    #region Send Mouse Events back to the Drawing Canvas so they can get handled there

    protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
    {
        base.OnMouseLeftButtonDown(e);
        DrawingCanvas?.OnItemOnMouseLeftButtonDown(e);
    }
    protected override void OnMouseMove(MouseEventArgs e)
    {
        base.OnMouseMove(e);
        DrawingCanvas?.OnItemOnMouseMove(e);
    }
    protected override void OnMouseRightButtonDown(MouseButtonEventArgs e)
    {
        base.OnMouseRightButtonDown(e);
        DrawingCanvas?.OnItemOnMouseRightButtonDown(e);
    }
    #endregion
    
    protected override void OnSelected(RoutedEventArgs e)
    {
        base.OnSelected(e);
        Debug.WriteLine("Select");
    }
    
}