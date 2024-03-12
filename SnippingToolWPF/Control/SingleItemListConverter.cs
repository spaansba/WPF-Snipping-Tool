using System.Collections;
using System.Windows;
using System.Windows.Data;
using SnippingToolWPF.WPFExtensions;

namespace SnippingToolWPF.Control;

public class SingleItemCollectionContainer : CollectionContainer
{
    public static readonly DependencyProperty ItemProperty = DependencyProperty.Register(
        nameof(Item),
        typeof(object),
        typeof(SingleItemCollectionContainer),
        new FrameworkPropertyMetadata
        {
            PropertyChangedCallback = ItemChangedCallback
        }
    );


    private bool createdCollection;

    public object? Item
    {
        get => this.GetValue<object?>(ItemProperty);
        set => this.SetValue<object?>(ItemProperty, value);
    }

    private static void ItemChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not SingleItemCollectionContainer container)
            return;
        if (e.NewValue is not null && container.ReadLocalValue(CollectionProperty) == DependencyProperty.UnsetValue)
        {
            container.createdCollection = true;
            container.SetCurrentValue(CollectionProperty, new ArrayList { e.NewValue });
        }
        else if (container.createdCollection)
        {
            container.createdCollection = false;
            container.ClearValue(CollectionProperty);
        }
    }
}