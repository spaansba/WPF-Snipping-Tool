﻿using System;
using System.Collections.Generic;
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

public class DrawingCanvasListBox : ItemsControl
{
    internal DrawingCanvas? DrawingCanvas { get; set; }

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

        // If element is a DrawingCanvasListBoxItem and item is a UIElement
        if (element is DrawingCanvasListBoxItem listBoxItem && item is UIElement uiElement)
        {
            // Check if Canvas.Left attached property is set on the item
            if (uiElement.ReadLocalValue(Canvas.LeftProperty) != DependencyProperty.UnsetValue)
            {
                // Set the Canvas.Left property on the listBoxItem
                Canvas.SetLeft(listBoxItem, Canvas.GetLeft(uiElement));
            }

            // Check if Canvas.Top attached property is set on the item
            if (uiElement.ReadLocalValue(Canvas.TopProperty) != DependencyProperty.UnsetValue)
            {
                // Set the Canvas.Top property on the listBoxItem
                Canvas.SetTop(listBoxItem, Canvas.GetTop(uiElement));
            }

            // Set the DrawingCanvas property on the listBoxItem
            listBoxItem.DrawingCanvas = this.DrawingCanvas;
        }
    }

    protected override void ClearContainerForItemOverride(DependencyObject element, object item)
    {
        base.ClearContainerForItemOverride(element, item);

        if(element is DrawingCanvasListBoxItem listBoxItem)
            listBoxItem.DrawingCanvas = null;
    }
}

public class DrawingCanvasListBoxItem : ContentPresenter
{
    internal DrawingCanvas? DrawingCanvas { get; set;}

    /// <summary>
    /// when a shape on the convas gets clicked, notify the drawing canvas
    /// </summary>
    protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
    {
        base.OnMouseLeftButtonDown(e);
        DrawingCanvas?.OnItemMouseEvent(this, e);
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
}
