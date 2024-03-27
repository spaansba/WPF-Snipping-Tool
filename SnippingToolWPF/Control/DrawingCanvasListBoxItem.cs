using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SnippingToolWPF.Common;

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

    /// <summary>
    /// We override this method so that when a user holds the mouse down on a selected item and moves over another item
    /// the selection does not change.
    /// </summary>
    protected override void OnMouseEnter(MouseEventArgs e) { }
    protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
    {
        if (KeyboardHelper.IsShiftPressed()) 
        { 
            e.Handled = true;
        }
        base.OnMouseLeftButtonDown(e);
        DrawingCanvas?.OnItemOnMouseLeftButtonDown(e, this.IsSelected);
    }
    
    protected override void OnMouseMove(MouseEventArgs e)
    {
        e.Handled = true;
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