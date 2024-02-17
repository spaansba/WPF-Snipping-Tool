using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace SnippingToolWPF;

public class SingleItemCollectionContainer : CollectionContainer
{
    public static readonly DependencyProperty ItemProperty = DependencyProperty.Register(
        nameof(Item),
        typeof(object),
        typeof(SingleItemCollectionContainer),
        new FrameworkPropertyMetadata()
        {
            PropertyChangedCallback = ItemChangedCallback,
        }
    );

    public object? Item
    {
        get => this.GetValue<object?>(ItemProperty);
        set => this.SetValue<object?>(ItemProperty, value);
    }


    private bool createdCollection;

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
