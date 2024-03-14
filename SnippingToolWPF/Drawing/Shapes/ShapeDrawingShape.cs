using System.Windows;
using System.Windows.Data;
using System.Windows.Shapes;

namespace SnippingToolWPF;
/// <summary>
/// ShapeDrawingShape handles all of the properties that pertain to ALL Shape
/// </summary>
public abstract class ShapeDrawingShape<TSelf, TVisual> : DrawingShape<TSelf, TVisual>
    where TVisual : Shape, new()
    where TSelf : DrawingShape<TSelf>, new()
{
    protected override void ClearBindings(TVisual visual)
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
        // BindingOperations.ClearBinding(visual, Canvas.LeftProperty);
        // BindingOperations.ClearBinding(visual, Canvas.TopProperty);
    }

    protected override void SetUpBindings(TVisual visual)
    {
        // is equivalent to: Stroke="{Binding RelativeSource={RelativeSource Self}, Path=Stroke}"
        visual.SetBinding(Shape.StrokeProperty, new Binding { Source = this, Path = new PropertyPath(StrokeProperty) });
        visual.SetBinding(Shape.FillProperty, new Binding { Source = this, Path = new PropertyPath(FillProperty) });
        visual.SetBinding(Shape.StrokeThicknessProperty,
            new Binding { Source = this, Path = new PropertyPath(StrokeThicknessProperty) });
        visual.SetBinding(Shape.StretchProperty,
            new Binding { Source = this, Path = new PropertyPath(StretchProperty) });
        visual.SetBinding(Shape.StrokeDashArrayProperty,
            new Binding { Source = this, Path = new PropertyPath(StrokeDashArrayProperty) });
        visual.SetBinding(Shape.StrokeDashOffsetProperty,
            new Binding { Source = this, Path = new PropertyPath(StrokeDashOffsetProperty) });
        visual.SetBinding(Shape.StrokeDashCapProperty,
            new Binding { Source = this, Path = new PropertyPath(StrokeDashCapProperty) });
        visual.SetBinding(Shape.StrokeEndLineCapProperty,
            new Binding { Source = this, Path = new PropertyPath(StrokeEndLineCapProperty) });
        visual.SetBinding(Shape.StrokeStartLineCapProperty,
            new Binding { Source = this, Path = new PropertyPath(StrokeStartLineCapProperty) });
        visual.SetBinding(Shape.StrokeMiterLimitProperty,
            new Binding { Source = this, Path = new PropertyPath(StrokeMiterLimitProperty) });
        visual.SetBinding(Shape.StrokeLineJoinProperty,
            new Binding { Source = this, Path = new PropertyPath(StrokeLineJoinProperty) });
        // visual.SetBinding(Canvas.LeftProperty, new Binding() { Source = this, Path = new(LeftProperty) });
        // visual.SetBinding(Canvas.TopProperty, new Binding() { Source = this, Path = new(TopProperty) });
    }
}