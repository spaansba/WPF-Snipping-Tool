using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SnippingToolWPF;

///public sealed class DrawingCanvasBindingProxy : BindingProxy<DrawingCanvasBindingProxy, DrawingCanvas>;

public abstract class BindingProxy<TSelf, T> : Freezable
    where TSelf : BindingProxy<TSelf, T>, new()
{
    protected sealed override TSelf CreateInstanceCore() => new TSelf();

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
