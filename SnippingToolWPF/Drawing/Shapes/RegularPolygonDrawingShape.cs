using SnippingToolWPF.Drawing.Tools.PolygonTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows;

namespace SnippingToolWPF.Drawing.Shapes;

public sealed class RegularPolygonDrawingShape : ShapeDrawingShape<Polygon>
{
    public RegularPolygonDrawingShape()
    {
        this.VisualInternal = CreateVisual();
    }
    public RegularPolygonDrawingShape(int polygonSides, double degreesRotated = 0d, double innerCircle = 0d)
    {
        this.NumberOfSides = polygonSides;
      //  this.degreesRotated = degreesRotated;
        this.PointGenerationRotationAngle = degreesRotated;
        this.VisualInternal = CreateVisual();
    }

    protected override Polygon CreateVisual()
    {
        return new Polygon()
        {
            Points = GetPolygonPoints(),
            Stroke = Brushes.Black,
        };
    }

    public const int DefaultNumberOfSides = 3;

    public static readonly DependencyProperty NumberOfSidesProperty = DependencyProperty.Register(
        nameof(NumberOfSides),
        typeof(int),
        typeof(RegularPolygonDrawingShape),
        new FrameworkPropertyMetadata(
            DefaultNumberOfSides,
            FrameworkPropertyMetadataOptions.AffectsRender
        ),
        validateValueCallback: static proposedValue => proposedValue is int value && value >= 3
    );

    public int NumberOfSides
    {
        get => this.GetValue<int>(NumberOfSidesProperty);
        set => this.SetValue<int>(NumberOfSidesProperty, value);
    }

    protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
    {
        base.OnPropertyChanged(e);
        if (e.Property == NumberOfSidesProperty)
            this.RegeneratePoints();
        if (e.Property == PointGenerationRotationAngleProperty)
            this.RegeneratePoints();
    }

    private void RegeneratePoints()
    {
        this.VisualInternal.Points = GetPolygonPoints();
    } 

    private PointCollection GetPolygonPoints() => new(CreateInitialPolygon.GeneratePolygonPoints(this.NumberOfSides, PointGenerationRotationAngle));

    private static readonly DependencyPropertyKey PointGenerationRotationAnglePropertyKey = DependencyProperty.RegisterReadOnly(
        name: nameof(PointGenerationRotationAngle),
        propertyType: typeof(double),
        ownerType: typeof(RegularPolygonDrawingShape),
        typeMetadata: new FrameworkPropertyMetadata(
            0d,
            FrameworkPropertyMetadataOptions.AffectsMeasure
        )
    );

    public static readonly DependencyProperty PointGenerationRotationAngleProperty = PointGenerationRotationAnglePropertyKey.DependencyProperty;

    public double PointGenerationRotationAngle
    {
        get => this.GetValue<double>(PointGenerationRotationAngleProperty);
        private set => this.SetValue(PointGenerationRotationAnglePropertyKey, value);
    }


}
