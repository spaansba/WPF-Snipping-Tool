using System.Windows.Data;
using System.Windows.Shapes;

namespace SnippingToolWPF;

public abstract class ShapeDrawingShape<TSelf, TVisual> : DrawingShape<TSelf, TVisual>
    where TVisual : Shape, new()
    where TSelf : DrawingShape<TSelf>, new()
{
    protected void ClearBindings(TVisual visual)
    {
        BindingOperations.ClearBinding(visual, Shape.StrokeProperty);
        BindingOperations.ClearBinding(visual, Shape.FillProperty);
        BindingOperations.ClearBinding(visual, Shape.StrokeThicknessProperty);
        BindingOperations.ClearBinding(visual, Shape.StretchProperty);
        BindingOperations.ClearBinding(visual, Shape.StrokeDashArrayProperty);
        BindingOperations.ClearBinding(visual, Shape.StrokeDashOffsetProperty);
        BindingOperations.ClearBinding(visual, Shape.StrokeDashCapProperty);
        BindingOperations.ClearBinding(visual, Shape.StrokeEndLineCapProperty);
        BindingOperations.ClearBinding(visual, Shape.StrokeStartLineCapProperty);
        BindingOperations.ClearBinding(visual, Shape.StrokeMiterLimitProperty);
        BindingOperations.ClearBinding(visual, Shape.StrokeLineJoinProperty);
    }

    protected void SetUpBindings(TVisual visual)
    {
        // is equivalent to: Stroke="{Binding RelativeSource={RelativeSource Self}, Path=Stroke}"
        visual.SetBinding(Shape.StrokeProperty, new Binding() { Source = this, Path = new(StrokeProperty)});
        visual.SetBinding(Shape.FillProperty, new Binding() { Source = this, Path = new(FillProperty) });
        visual.SetBinding(Shape.StrokeThicknessProperty, new Binding() { Source = this, Path = new(StrokeThicknessProperty)});
        visual.SetBinding(Shape.StretchProperty, new Binding() { Source = this, Path = new(StretchProperty) });
        visual.SetBinding(Shape.StrokeDashArrayProperty, new Binding() { Source = this, Path = new(StrokeDashArrayProperty) });
        visual.SetBinding(Shape.StrokeDashOffsetProperty, new Binding() { Source = this, Path = new(StrokeDashOffsetProperty) });
        visual.SetBinding(Shape.StrokeDashCapProperty, new Binding() { Source = this, Path = new(StrokeDashCapProperty) });
        visual.SetBinding(Shape.StrokeEndLineCapProperty, new Binding() { Source = this, Path = new(StrokeEndLineCapProperty) });
        visual.SetBinding(Shape.StrokeStartLineCapProperty, new Binding() { Source = this, Path = new(StrokeStartLineCapProperty) });
        visual.SetBinding(Shape.StrokeMiterLimitProperty, new Binding() { Source = this, Path = new(StrokeMiterLimitProperty) });
        visual.SetBinding(Shape.StrokeLineJoinProperty, new Binding() { Source = this, Path = new(StrokeLineJoinProperty) });
    }
}