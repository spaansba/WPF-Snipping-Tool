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
        var poly =  new Polygon
        {
            Points = GetPolygonPoints(),
        };
        ClearBindings(poly);
        SetUpBindings(poly);
        return poly;
    }

    private const int DefaultNumberOfSides = 3;

    public static readonly DependencyProperty NumberOfSidesProperty = DependencyProperty.Register(
        nameof(NumberOfSides),
        typeof(int),
        typeof(RegularPolygonDrawingShape),
        new FrameworkPropertyMetadata(
            defaultValue: DefaultNumberOfSides,
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
        if (e.Property == NumberOfSidesProperty) //Recalc points if we change the number of sides
            this.RegeneratePoints();
        if (e.Property == PointGenerationRotationAngleProperty) // Recalc points if we change the Angle
            this.RegeneratePoints();
    }

    private void RegeneratePoints()
    {
        this.Points = GetPolygonPoints();
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
