using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Windows.Media;
using SnippingToolWPF.Tools.PolygonTools;
using SnippingToolWPF.Tools.ToolAction;

namespace SnippingToolWPF.SidePanel.ShapesSidePanel;

public sealed class ShapesSidePanelViewModel : SidePanelViewModel
{
    public override string Header => "Shapes";

    #region Shape/Tool Selection 
   
    private IDrawingTool? tool;
    public override IDrawingTool? Tool => tool;

    /// <summary>
    /// Get the Shape selected by the user in the sidepanel, the enum is stored in the shapes Tag
    /// </summary>
    private PremadePolygonInfo? polygonSelected;
    public PremadePolygonInfo? PolygonSelected
    {
        get => polygonSelected;
        set
        {
            if (value is not null)
            {
                polygonSelected = value;
                UpdateTool(); // Update the Tool to give it the new shape
            }
        }
    }
    /// <summary>
    /// Used to update the Tool Property and notify the change
    /// </summary>
    private void UpdateTool()
    {
        tool = new PolygonTool(this);
        OnPropertyChanged(nameof(Tool)); // Notify the change 
    }

    #endregion

    #region ctor
    public IReadOnlyList<PremadePolygonInfo> Polygons { get; set; }

    public ShapesSidePanelViewModel(DrawingViewModel drawingViewModel) : base(drawingViewModel)
    {
        UpdateTool();
        Polygons = CreateButtonPolygons();

        PolygonSelected = new(4);
    }

    /// <summary>
    /// Create the shapes presented on the buttons in the sidepanel
    /// </summary>
    private static IReadOnlyList<PremadePolygonInfo> CreateButtonPolygons() =>
    [
        new(4, 45), // Tetragon (rectangle)
        new(1000), // Ellipse
        new(3, 30), // Triangle
        new(4), // Diamond
        new(5, 126), // Pentagon
        new( 6, 30), // Hexagon
        new( 7, 13), // Septagon
        new(8), // Octagon
        new(10, 0, true), // 5 pointed Star
    ];

    #endregion

    #region Thickness - Connecting the Slider / Textbox with eachother and data clamping

    public const double MinimumThickness = 1;
    public const double MaximumThickness = 100;
    private const double DefaultThickness = 6;

    private string thicknessString = DefaultThickness.ToString(CultureInfo.InvariantCulture);
    private double LastValidThickness { get; set; }

    /// <summary>
    /// ThicknessString is bound to the textbox, on property change, clamp the value if needed and update the slider (Thickness)
    /// </summary>
    public string ThicknessString
    {
        get => thicknessString;
        set
        {
            if (this.SetProperty(ref thicknessString, value, validate: true))
            {
                OnPropertyChangedThickness(value);
            }
        }
    }
    private void OnPropertyChangedThickness(string value)
    {
        if (string.IsNullOrEmpty(value))
            return;

        if (double.TryParse(value, CultureInfo.CurrentCulture, out var doubleValue))
        {
            var clampedValue = Math.Clamp(doubleValue, MinimumThickness, MaximumThickness);
            this.Thickness = clampedValue;
            this.LastValidThickness = clampedValue;
        }
        else //invalid entry use last valid thickness
        {
            this.Thickness = this.LastValidThickness;
        }
    }

    public double Thickness
    {
        get
        {
            return double.TryParse(this.ThicknessString, CultureInfo.CurrentCulture, out var value)
            ? value : DefaultThickness;
        }
        set
        {
            OnPropertyChanged();
            this.ThicknessString = value.ToString(CultureInfo.InvariantCulture);
        }
    }

    #endregion

    #region Shape Opacity
    public const double MinimumOpacity = 1;
    public const double MaximumOpacity = 100;
    private const double DefaultOpacity = 100;

    [Range(MinimumOpacity, MaximumOpacity)]
    private string opacityString = DefaultOpacity.ToString(CultureInfo.InvariantCulture);

    private double LastValidOpacity { get; set; }

    /// <summary>
    /// OpacityString is bound to the textbox, on property change, clamp the value if needed and update the slider (Opacity)
    /// </summary>
    public string OpacityString
    {
        get => opacityString;
        set
        {
            if (this.SetProperty(ref opacityString, value, validate: true))
            {
                OnPropertyChangedOpacity(value);
            }
        }
    }
    private void OnPropertyChangedOpacity(string value)
    {
        if (string.IsNullOrEmpty(value))
            return;

        if (double.TryParse(value, CultureInfo.CurrentCulture, out var doubleValue))
        {
            var clampedValue = Math.Clamp(doubleValue, MinimumOpacity, MaximumOpacity);
            this.Opacity = clampedValue;
            this.LastValidOpacity = clampedValue;
        }
        else //invalid entry use last valid Opacity
        {
            this.Opacity = this.LastValidOpacity;
        }
    }

    /// <summary>
    /// Opacity is bound to the slider, OpacityString to the textbox.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public double Opacity
    {
        get => double.TryParse(this.OpacityString, CultureInfo.CurrentCulture, out var value)
                ? value : DefaultOpacity;
        set
        {
            OnPropertyChanged();
            this.OpacityString = value.ToString(CultureInfo.InvariantCulture);
        }
    }

    /// <summary>
    /// Use This value for any opacity properties, as opacity is a value between 0 and 1.0
    /// </summary>
    public double RealOpacity => Opacity / 100;
    #endregion

    #region Shape Fill
    public readonly Brush ShapeStroke = Brushes.Black;
    public readonly Brush ShapeFill = Brushes.Transparent;
    //TODO: Shape Fill
    #endregion
}