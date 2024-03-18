using System.Collections.Frozen;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Shapes;
using SnippingToolWPF.WPFExtensions;

namespace SnippingToolWPF;

public sealed class RegularPolylineDrawingShape : ShapeDrawingShape<RegularPolylineDrawingShape, Polyline>
// ReSharper disable once RedundantTypeDeclarationBody
{
    public RegularPolylineDrawingShape()
    {
        this.Visual = CreateVisual();
        this.Points = new PointCollection(); // otherwise IsFrozen exception
    }

    protected override void PopulateClone(RegularPolylineDrawingShape clone)
    {
        base.PopulateClone(clone);
        clone.Stretch = this.Stretch;
        clone.Stroke = this.Stroke;
        clone.StrokeThickness = this.StrokeThickness;
        clone.Opacity = this.Opacity;
        clone.StrokeDashCap = this.StrokeDashCap;
        clone.StrokeStartLineCap = this.StrokeStartLineCap;
        clone.StrokeEndLineCap = this.StrokeEndLineCap;
        clone.UseLayoutRounding = this.UseLayoutRounding;
        clone.StrokeLineJoin = this.StrokeLineJoin;
        clone.Fill = this.Fill;
        clone.Effect = this.Effect;
        
        // Get the smallest X and Y and create a new point list based on the Visual.Points
        // In this list we substract the minY and minX from each point so we can set the canvas of the Shape correctly to match the DrawingCanvas
        var minX = this.Points.Min(static p => p.X);
        var minY = this.Points.Min(static p => p.Y);
        var newPoints = new PointCollection(this.Points.Count);
        foreach (var point in this.Points)
        {
            newPoints.Add(new Point(point.X - minX, point.Y - minY));
        }
        
        clone.Points = newPoints;
        clone.Top = minY;
        clone.Left = minX;

    }
    
    protected override void SetUpBindings(Polyline visual)
    {
        base.SetUpBindings(visual); // Call the next base AKA next most derived - ShapeDrawingShape in this case
        
        /// Make sure to clear all bindings we set 
        
        visual.SetBinding(
            Polyline.PointsProperty,
            new Binding
            {
                Source = this,
                Path = new PropertyPath(RegularPolylineDrawingShape.PointsProperty),
            });
    }
    
    protected override void ClearBindings(Polyline visual)
    {
        base.ClearBindings(visual); // Call the next base AKA next most derived - ShapeDrawingShape in this case
        BindingOperations.ClearBinding(visual, Polyline.PointsProperty);
    }
    
    public static readonly DependencyProperty PointsProperty = Polyline.PointsProperty.AddOwner(typeof(RegularPolylineDrawingShape));

    public PointCollection Points
    {
        get => this.GetValue<PointCollection>(PointsProperty) ?? new(); // Make sure Points is never null
        set => this.SetValue<PointCollection>(PointsProperty, value);
    }
    
}

public class FreezableDefaultValueFactory
{
    public FreezableDefaultValueFactory(object empty)
    {
        throw new NotImplementedException();
    }
}