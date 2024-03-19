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
    
    internal DrawingCanvas? DrawingCanvas { get; set; }

    /// <summary>
    ///     Returns true if the item is (or should be) its own item container, basically recreating the base ItemsControl
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    protected override bool IsItemItsOwnContainerOverride(object item)
    {
        return item is DrawingCanvasListBoxItem;
    }

    /// <summary>
    ///     Create or identify the element used to display the given item (returns new content presenter), basically recreating
    ///     the base ItemsControl
    /// </summary>
    protected override DependencyObject GetContainerForItemOverride()
    {
        return new DrawingCanvasListBoxItem();
    }

    protected override void PrepareContainerForItemOverride(DependencyObject? element, object? item)
    {
        base.PrepareContainerForItemOverride(element, item);

        if (element is not DrawingCanvasListBoxItem listBoxItem || item is not UIElement uiElement) return;


        // Reason we only check for Left and Top
        // https://source.dot.net/#PresentationFramework/System/Windows/Controls/Canvas.cs,286
        if (uiElement.ReadLocalValue(Canvas.LeftProperty) != DependencyProperty.UnsetValue)
            Canvas.SetLeft(listBoxItem, Canvas.GetLeft(uiElement) - 1); // - 1 to adjust for the ListBox 

        if (uiElement.ReadLocalValue(Canvas.TopProperty) != DependencyProperty.UnsetValue)
            Canvas.SetTop(listBoxItem, Canvas.GetTop(uiElement) - 1); // - 1 to adjust for the ListBox 

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
