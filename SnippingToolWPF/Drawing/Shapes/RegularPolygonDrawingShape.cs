using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using SnippingToolWPF.Tools.PolygonTools;
using SnippingToolWPF.WPFExtensions;

namespace SnippingToolWPF;

public sealed class RegularPolygonDrawingShape : ShapeDrawingShape<RegularPolygonDrawingShape, Polygon>
{
    public RegularPolygonDrawingShape()
    {
        this.Visual = CreateVisual();
    }

    private Polygon CreateVisual()
    {
        return new()
        {
            Points = GetPolygonPoints(),
            Stroke = Brushes.Black,
        };
    }

    private const int DefaultNumberOfSides = 3;

    public static readonly DependencyProperty NumberOfSidesProperty = DependencyProperty.Register(
        nameof(NumberOfSides),
        typeof(int),
        typeof(RegularPolygonDrawingShape),
        new FrameworkPropertyMetadata(
            DefaultNumberOfSides,
            FrameworkPropertyMetadataOptions.AffectsRender
        ),
        validateValueCallback: static proposedValue => proposedValue is >= 3
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
    //    this.VisualInternal.Points = GetPolygonPoints();
    } 

    private PointCollection GetPolygonPoints() => new(CreateInitialPolygon.GeneratePolygonPoints(this.NumberOfSides, PointGenerationRotationAngle));

    private static readonly DependencyPropertyKey PointGenerationRotationAnglePropertyKey = DependencyProperty.RegisterReadOnly(
        name: nameof(PointGenerationRotationAngle),
        propertyType: typeof(double),
        ownerType: typeof(RegularPolygonDrawingShape),
        typeMetadata: new FrameworkPropertyMetadata(
            defaultValue: 0d,
            FrameworkPropertyMetadataOptions.AffectsMeasure
        )
    );

    public static readonly DependencyProperty PointGenerationRotationAngleProperty = PointGenerationRotationAnglePropertyKey.DependencyProperty;

    public double PointGenerationRotationAngle
    {
        get => this.GetValue<double>(PointGenerationRotationAngleProperty);
        private set => this.SetValue(PointGenerationRotationAnglePropertyKey, value);
    }
    protected override void PopulateClone(RegularPolygonDrawingShape clone)
    {
        base.PopulateClone(clone);
        Console.WriteLine();
    }
}
