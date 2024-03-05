using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SnippingToolWPF.Control;

/// <summary>
/// We carry the DrawingCanvas instance all the way thru to the individual item containers.
/// So, now every item container knows what DrawingCanvas it belongs to.
/// In theory this could be done by looking up the tree but this is a lot more reliable
/// </summary>

public class DrawingCanvasListBox : ListBox
{
    internal DrawingCanvas? DrawingCanvas { get; set; }

    public DrawingCanvasListBox()
    {
        this.BorderBrush = null;
        this.Background = null;
        this.BorderThickness = new Thickness(0);
    }

    /// <summary>
    /// Returns true if the item is (or should be) its own item container, basically recreating the base ItemsControl
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    protected override bool IsItemItsOwnContainerOverride(object item)
    {
        return item is DrawingCanvasListBoxItem;
    }

    /// <summary>
    ///  Create or identify the element used to display the given item (returns new content presenter), basically recreating the base ItemsControl
    /// </summary>
    protected override DependencyObject GetContainerForItemOverride()
    {
        return new DrawingCanvasListBoxItem();
    }

    protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
    {
        base.PrepareContainerForItemOverride(element, item);

        if (element is DrawingCanvasListBoxItem listBoxItem && item is UIElement uiElement)
        {
            // Reason we only check for Left and Top
            // https://source.dot.net/#PresentationFramework/System/Windows/Controls/Canvas.cs,286
            if (uiElement.ReadLocalValue(Canvas.LeftProperty) != DependencyProperty.UnsetValue)
                Canvas.SetLeft(listBoxItem, Canvas.GetLeft(uiElement));

            if (uiElement.ReadLocalValue(Canvas.TopProperty) != DependencyProperty.UnsetValue)
                Canvas.SetTop(listBoxItem, Canvas.GetTop(uiElement));

            listBoxItem.DrawingCanvas = DrawingCanvas;
        }
    }

    protected override void ClearContainerForItemOverride(DependencyObject element, object item)
    {
        base.ClearContainerForItemOverride(element, item);

        if (element is DrawingCanvasListBoxItem listBoxItem)
            listBoxItem.DrawingCanvas = null;
    }
}

public class DrawingCanvasListBoxItem : ListBoxItem
{
    internal DrawingCanvas? DrawingCanvas { get; set; }
    internal DrawingViewModel? DrawingViewModel { get; set; }
    public DrawingCanvasListBoxItem()
    {
        // Set up visual appearance
        this.BorderBrush = null;
        this.Background = null;
        this.BorderThickness = new Thickness(0);
        this.Margin = new Thickness(0);
        this.Padding = new Thickness(0);
    }

    /// <summary>
    /// when a shape on the convas gets clicked, notify the drawing canvas
    /// </summary>
    protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
    {
        base.OnMouseLeftButtonDown(e);
        DrawingCanvas?.OnItemMouseEvent(this, e);
        DrawingViewModel?.OnItemMouseEvent(this, e);
    }
    /// <summary>
    /// when item/shape on the canvas gets hovered over while mouse is down, notify drawing canvas
    /// </summary>
    /// <param name="e"></param>
    protected override void OnMouseMove(MouseEventArgs e)
    {
        base.OnMouseMove(e);
        DrawingCanvas?.OnItemMouseEvent(this, e);
    }

    protected override void OnSelected(RoutedEventArgs e)
    {
        base.OnSelected(e);
    }

}
