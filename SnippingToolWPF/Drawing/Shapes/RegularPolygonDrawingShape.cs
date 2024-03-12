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
        Visual = CreateVisual();
    }
    private Polygon CreateVisual()
    {
        var poly = new Polygon
        {
            Points = GetPolygonPoints()
        };
        ClearBindings(poly);
        SetUpBindings(poly);
        return poly;
    }
    
    private void RegeneratePoints()
    {
        this.Visual = CreateVisual();
    }

    private PointCollection GetPolygonPoints()
    {
        return new PointCollection(
            CreateInitialPolygon.GeneratePolygonPoints(NumberOfSides, PointGenerationRotationAngle));
    }

    protected override void PopulateClone(RegularPolygonDrawingShape clone)
    {
        base.PopulateClone(clone);
        Console.WriteLine();
    }

    protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
    {
        base.OnPropertyChanged(e);
        if (e.Property == NumberOfSidesProperty) //Recalc points if we change the number of sides
            RegeneratePoints();
        if (e.Property == PointGenerationRotationAngleProperty) // Recalc points if we change the Angle
            RegeneratePoints();
    }
    
    #region  Dependency Properties
    private const int DefaultNumberOfSides = 4;

    public static readonly DependencyProperty NumberOfSidesProperty = DependencyProperty.Register(
        nameof(NumberOfSides),
        typeof(int),
        typeof(RegularPolygonDrawingShape),
        new FrameworkPropertyMetadata(
            DefaultNumberOfSides,
            FrameworkPropertyMetadataOptions.AffectsRender
        ),
        static proposedValue => proposedValue is >= 3
    );
    public int NumberOfSides
    {
        get => this.GetValue<int>(NumberOfSidesProperty);
        set => this.SetValue<int>(NumberOfSidesProperty, value);
    }

    private static readonly DependencyPropertyKey PointGenerationRotationAnglePropertyKey =
        DependencyProperty.RegisterReadOnly(
            nameof(PointGenerationRotationAngle),
            typeof(double),
            typeof(RegularPolygonDrawingShape),
            new FrameworkPropertyMetadata(
                0d,
                FrameworkPropertyMetadataOptions.AffectsMeasure
            )
        );

    public static readonly DependencyProperty PointGenerationRotationAngleProperty =
        PointGenerationRotationAnglePropertyKey.DependencyProperty;
    
    public double PointGenerationRotationAngle
    {
        get => this.GetValue<double>(PointGenerationRotationAngleProperty);
        private set => SetValue(PointGenerationRotationAnglePropertyKey, value);
    }
    #endregion
}