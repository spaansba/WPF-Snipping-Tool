using System.Windows;
using SnippingToolWPF.WPFExtensions;

namespace SnippingToolWPF.Common;
public abstract class BindingProxy<TSelf, T> : Freezable
    where TSelf : BindingProxy<TSelf, T>, new()
{
    protected sealed override TSelf CreateInstanceCore() => new();

    public T? Data
    {
        get => this.GetValue<T?>(DataProperty);
        set => this.SetValue<T?>(DataProperty, value);
    }

    public static readonly DependencyProperty DataProperty = DependencyProperty.Register(
        nameof(Data),
        typeof(object),
        typeof(BindingProxy<TSelf, T>));
}
