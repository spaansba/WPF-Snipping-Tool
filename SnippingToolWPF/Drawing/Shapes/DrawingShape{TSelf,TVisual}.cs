using System.Diagnostics.CodeAnalysis;
using System.Windows;
using SnippingToolWPF.WPFExtensions;

namespace SnippingToolWPF;

public class DrawingShape<TSelf, TVisual> : DrawingShape<TSelf>
    where TVisual : UIElement, new()
    where TSelf : DrawingShape<TSelf>, new()
{
    public DrawingShape()
    {
        VisualInternal = CreateVisual();
    }

    /// <summary>
    ///     The Shape Portion of the DrawingShape
    /// </summary>
    [AllowNull]
    private TVisual VisualInternal
    {
        // Visual will never be null
        set => this.SetValue<TVisual?>(VisualProperty, value);
    }

    private TVisual CreateVisual()
    {
        return new TVisual();
        // Can override this if you want to customize /how/ the visual is created.
    }

    /// <summary>
    ///     If Visual changes clear and set the bindings
    /// </summary>
    private void OnVisualChanged(TVisual? oldValue, TVisual? newValue)
    {
        if (oldValue is not null)
            ClearBindings(oldValue);
        if (newValue is not null)
            SetUpBindings(newValue);
    }

    protected override void OnVisualChangedOverride(UIElement? oldValue, UIElement? newValue)
    {
        OnVisualChanged(oldValue as TVisual, newValue as TVisual);
    }

    // ReSharper disable once UnusedParameter.Local
    private void ClearBindings(TVisual visual)
    {
    }

    // ReSharper disable once UnusedParameter.Local
    private void SetUpBindings(TVisual visual)
    {
    }
}