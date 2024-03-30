using System.Diagnostics;
using System.Windows;
using SnippingToolWPF.WPFExtensions;

namespace SnippingToolWPF.Common;
[DebuggerStepThrough]
public abstract class BindingProxy<TSelf, T> : Freezable
    where TSelf : BindingProxy<TSelf, T>, new()
{
    public static readonly DependencyProperty DataProperty = DependencyProperty.Register(
        nameof(Data),
        typeof(object),
        typeof(BindingProxy<TSelf, T>));

    public T? Data
    {
        get => this.GetValue<T?>(DataProperty);
        set => this.SetValue<T?>(DataProperty, value);
    }

    protected sealed override TSelf CreateInstanceCore()
    {
        return new TSelf();
    }
}