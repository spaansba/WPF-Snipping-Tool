using System.Windows;

namespace SnippingToolWPF;

public class DrawingShape<TSelf, TVisual> : DrawingShape<TSelf>
    where TVisual : UIElement, new()
    where TSelf : DrawingShape<TSelf>, new()
{
    public DrawingShape()
    {
        //Alse: No need to call SetUpBindings, change in visualProperty > notifies OnPropertyChanged > calls SetUpBindings
        // ReSharper disable once VirtualMemberCallInConstructor
        this.Visual = CreateVisual();
    }
    
    /// <summary>
    /// If desired, change how a new visual is created
    /// </summary>
    /// <returns>The visual(<typeparam name="TSelf"></typeparam>) used in DrawingShape</returns>
    /// <remarks>
    ///<b>Note to implementers:</b> this method should not rely on any behaviour in <typeparam name="TSelf"></typeparam>
    /// nor any virtual properties/methods
    /// </remarks>
    protected virtual TVisual CreateVisual()
    {
        return new TVisual();
        // Can override this if you want to customize /how/ the visual is created.
    }
        
    /// <summary>
    /// Calls OnVisualChanged in this class as we cant override base OnVisualChanged
    /// </summary>
    protected override void OnVisualChangedOverride(UIElement? oldValue, UIElement? newValue) =>
        OnVisualChanged(oldValue as TVisual, newValue as TVisual);
    
    /// <summary>
    /// If Visual changes clear and set the bindings after base OnVisualChanged
    /// </summary>
    private void OnVisualChanged(TVisual? oldValue, TVisual? newValue)
    {
        if (oldValue is not null)
            ClearBindings(oldValue);
        if (newValue is not null)
            SetUpBindings(newValue);
    }

    protected virtual void ClearBindings(TVisual visual)
    {
        //method to be overriden
    }
    
    protected virtual void SetUpBindings(TVisual visual)
    {
        //method to be overriden
    }
}