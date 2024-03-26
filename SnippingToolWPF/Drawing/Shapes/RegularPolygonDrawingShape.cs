using System.Windows;
using System.Windows.Data;
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
        
        //Because Visual has bound to PointsProperty, setting this.Points will update this.Visual 
        this.Points = new PointCollection(CreateInitialPolygon.GeneratePolygonPoints(NumberOfSides, PointGenerationRotationAngle));
    }
    
    public RegularPolygonDrawingShape(double pointGenerationRotationAngle)
    {
        this.Visual = CreateVisual();
        this.PointGenerationRotationAngle = pointGenerationRotationAngle;
        
        //Because Visual has bound to PointsProperty, setting this.Points will update this.Visual 
        this.Points = new PointCollection(CreateInitialPolygon.GeneratePolygonPoints(NumberOfSides, PointGenerationRotationAngle));
    }
    
    protected override void PopulateClone(RegularPolygonDrawingShape clone)
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
        clone.Left = this.Left;
        clone.Top = this.Top;
        clone.Width = this.Width;
        clone.Height = this.Height;
        clone.Points = this.Points;
        clone.FillRule = this.FillRule;
    }

    protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
    {
        base.OnPropertyChanged(e);
        if (e.Property == NumberOfSidesProperty) //Recalc points if we change the number of sides
            this.Points = new PointCollection(CreateInitialPolygon.GeneralPolygonPoints(NumberOfSides, PointGenerationRotationAngle,
                StarInnerCircleSize));
        if (e.Property == PointGenerationRotationAngleProperty) // Recalc points if we change the Angle
            this.Points = new PointCollection(CreateInitialPolygon.GeneralPolygonPoints(NumberOfSides, PointGenerationRotationAngle,
                StarInnerCircleSize));
        if (e.Property == StarInnerCircleSizeProperty) // Recalc points if we change the Angle
            this.Points = new PointCollection(CreateInitialPolygon.GeneralPolygonPoints(NumberOfSides, PointGenerationRotationAngle,
                StarInnerCircleSize));
    }
    
    #region  Bindings
    protected override void SetUpBindings(Polygon visual)
    {
        base.SetUpBindings(visual); // Call the next base AKA next most derived - ShapeDrawingShape in this case
        
        /// Make sure to clear all bindings we set 
        
        visual.SetBinding(
            Polygon.PointsProperty,
            new Binding
            {
                Source = this,
                Path = new PropertyPath(RegularPolygonDrawingShape.PointsProperty),
            });
    }
    
    protected override void ClearBindings(Polygon visual)
    {
        base.ClearBindings(visual); // Call the next base AKA next most derived - ShapeDrawingShape in this case
        BindingOperations.ClearBinding(visual, Polygon.PointsProperty);
    }
    
    #endregion
    
    #region  Dependency Properties
    private const int DefaultNumberOfSides = 4;

    public static readonly DependencyProperty NumberOfSidesProperty = DependencyProperty.Register(
        nameof(NumberOfSides),
        typeof(int),
        typeof(RegularPolygonDrawingShape),
        new FrameworkPropertyMetadata(
            defaultValue: DefaultNumberOfSides,
            flags: FrameworkPropertyMetadataOptions.AffectsRender
        ),
        static proposedValue => proposedValue is >= 3
    );
    public int NumberOfSides
    {
        get => this.GetValue<int>(NumberOfSidesProperty);
        set => this.SetValue<int>(NumberOfSidesProperty, value);
    }
    
    private const double DefaultStarInnerCircleSize= 1.0;
    
    public static readonly DependencyProperty StarInnerCircleSizeProperty = DependencyProperty.Register(
        nameof(StarInnerCircleSize),
        typeof(double),
        typeof(RegularPolygonDrawingShape),
        new FrameworkPropertyMetadata(
            defaultValue: DefaultStarInnerCircleSize,
            flags: FrameworkPropertyMetadataOptions.AffectsRender
    ));
    public double StarInnerCircleSize
    {
        get => this.GetValue<double>(StarInnerCircleSizeProperty);
        set => this.SetValue<double>(StarInnerCircleSizeProperty, value);
    }
    
    /// <summary>
    /// PointGenerationRotationAngleProperty is different from Angle. This is a 1 time set angle that is set ones by
    /// the premade shapes in the sidepanel. This differs from the DrawingShape.Angle property as that propety can
    /// be adjusted by the user while editing. This is readonly
    /// </summary>
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
        private init => SetValue(PointGenerationRotationAnglePropertyKey, value);
    }
    
    public static readonly DependencyProperty PointsProperty = Polygon.PointsProperty.AddOwner(typeof(RegularPolygonDrawingShape));

    private PointCollection? Points
    {
        get => this.GetValue<PointCollection?>(PointsProperty);
        set => this.SetValue<PointCollection?>(PointsProperty, value);
    }
    #endregion
}