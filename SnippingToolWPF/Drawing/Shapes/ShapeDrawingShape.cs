
using System.Windows;
using System.Windows.Data;
using System.Windows.Shapes;

namespace SnippingToolWPF.Drawing.Shapes;

public abstract class ShapeDrawingShape<TVisual> : DrawingShape<TVisual>
    where TVisual : Shape, new()
{
    protected override void ClearBindings(TVisual visual)
    {
        // TODO: Fill in other properties that pertain to Shape
        BindingOperations.ClearBinding(visual, Shape.StrokeProperty);
    }

    protected override void SetUpBindings(TVisual visual)
    {
        // TODO: Fill in other properties that pertain to Shape
        // is equivalent to: Stroke="{Binding RelativeSource={RelativeSource Self}, Path=Stroke}"
        visual.SetBinding(Shape.StrokeProperty, new Binding() { Source = this, Path = new PropertyPath(DrawingShape.StrokeProperty) });
    }
}