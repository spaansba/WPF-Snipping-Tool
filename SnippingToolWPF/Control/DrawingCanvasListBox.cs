﻿using System.Collections.Specialized;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace SnippingToolWPF.Control;

/// <summary>
///     We carry the DrawingCanvas instance all the way thru to the individual item containers.
///     So, now every item container knows what DrawingCanvas it belongs to.
///     In theory this could be done by looking up the tree but this is a lot more reliable
/// </summary>
public class DrawingCanvasListBox : ListBox
{
    /// <summary>
    /// Override the default style for this type (ListBoxItem), now use (DrawingCanvasListBoxItem)
    /// </summary>
    static DrawingCanvasListBox()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(DrawingCanvasListBox),
            new FrameworkPropertyMetadata(typeof(DrawingCanvasListBox)));
    }

    public DrawingCanvasListBox()
    {
        ((INotifyCollectionChanged)this.Items).CollectionChanged += OnCollectionChanged;
    }

    private void OnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        Debug.WriteLine($"new item added {e.NewItems?.Count}");
    }
    
    internal DrawingCanvas? DrawingCanvas { get; set; }

    /// <summary>
    ///  Returns true if the item is (or should be) its own item container, basically recreating the base ItemsControl
    /// </summary>
    protected override bool IsItemItsOwnContainerOverride(object item)
    {
        return item is DrawingCanvasListBoxItem;
    }

    /// <summary>
    ///  Create or identify the element used to display the given item (returns new content presenter), basically recreating
    ///  the base ItemsControl
    /// </summary>
    protected override DependencyObject GetContainerForItemOverride()
    {
        return new DrawingCanvasListBoxItem();
    }

    protected override void PrepareContainerForItemOverride(DependencyObject? element, object? item)
    {
        base.PrepareContainerForItemOverride(element, item);

        if (element is not DrawingCanvasListBoxItem listBoxItem || item is not UIElement) return;
        
        // Reason we only check for Left and Top
        // https://source.dot.net/#PresentationFramework/System/Windows/Controls/Canvas.cs,286
        
        HorizontalContentAlignment = HorizontalAlignment.Stretch;
        VerticalContentAlignment = VerticalAlignment.Stretch;

        listBoxItem.DrawingCanvas = DrawingCanvas;
    }
    
    protected override void ClearContainerForItemOverride(DependencyObject element, object item)
    {
        base.ClearContainerForItemOverride(element, item);

        if (element is DrawingCanvasListBoxItem listBoxItem)
            listBoxItem.DrawingCanvas = null;
    }
}
