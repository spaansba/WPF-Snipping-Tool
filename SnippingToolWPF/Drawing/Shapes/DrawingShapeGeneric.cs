using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;

namespace SnippingToolWPF.Drawing.Shapes;

public abstract class DrawingShape<TVisual> : DrawingShape
    where TVisual : UIElement, new()
{
    public DrawingShape()
    {
        this.Visual = CreateVisual();
    }
    protected virtual TVisual CreateVisual() => new TVisual(); // Can override this if you want to customize /how/ the visual is created.
    [AllowNull]
    public new TVisual Visual
    {
        get => this.GetValue<TVisual?>(VisualProperty)
               ?? this.SetValue<TVisual>(VisualProperty, CreateVisual()); // Visual will never be null
        set => this.SetValue<TVisual?>(VisualProperty, value);
    }
    protected virtual void OnVisualChanged(TVisual? oldValue, TVisual? newValue)
    {
        if (oldValue is not null)
            ClearBindings(oldValue);
        if (newValue is not null)
            SetUpBindings(newValue);
    }
    protected sealed override void OnVisualChanged(UIElement? oldValue, UIElement? newValue)
        => OnVisualChanged(oldValue as TVisual, newValue as TVisual);
    protected virtual void ClearBindings(TVisual visual)
    {
    }
    protected virtual void SetUpBindings(TVisual visual)
    {
    }
}