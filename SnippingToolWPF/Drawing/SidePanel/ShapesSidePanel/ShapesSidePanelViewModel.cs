using SnippingToolWPF.Drawing.Tools;
using SnippingToolWPF.Drawing.Tools.ToolAction;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Media;
using SnippingToolWPF.Drawing.Shapes;
using SnippingToolWPF.Drawing.Tools.PolygonTools;
using System.Windows.Shapes;

namespace SnippingToolWPF;

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

        PolygonSelected = new PremadePolygonInfo(4,0,false);
    }

    /// <summary>
    /// Create the shapes presented on the buttons in the sidepanel
    /// </summary>
    public static IReadOnlyList<PremadePolygonInfo> CreateButtonPolygons() =>
    [
        new PremadePolygonInfo(4, 45), // Tetragon (rectangle)
        new PremadePolygonInfo(1000, 0), // Ellipse
        new PremadePolygonInfo(3, 30), // Triangle
        new PremadePolygonInfo(4, 0), // Diamond
        new PremadePolygonInfo(5, 126), // Pentagon
        new PremadePolygonInfo( 6, 30), // Hexagon
        new PremadePolygonInfo( 7, 13), // Septagon
        new PremadePolygonInfo(8, 0), // Octagon
        new PremadePolygonInfo(10, 0, true), // 5 pointed Star
    ];

    #endregion

    #region Thickness - Connecting the Slider / Textbox with eachother and data clamping

    public const double MinimumThickness = 1;
    public const double MaximumThickness = 100;
    private const double DefaultThickness = 6;

    private string thicknessString = DefaultThickness.ToString();
    public double LastValidThickness { get; set; }

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
                OnPropertyChangedThickness(nameof(this.Thickness), value);
            }
        }
    }
    private void OnPropertyChangedThickness(string propertyName, string value)
    {
        if (string.IsNullOrEmpty(value))
            return;

        if (double.TryParse(value, CultureInfo.CurrentCulture, out double doubleValue))
        {
            double clampedValue = Math.Clamp(doubleValue, MinimumThickness, MaximumThickness);
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
            return double.TryParse(this.ThicknessString, CultureInfo.CurrentCulture, out double value)
            ? value : DefaultThickness;
        }
        set
        {
            OnPropertyChanged(nameof(Thickness));
            this.ThicknessString = value.ToString();
        }
    }

    #endregion

    #region Shape Opacity
    public const double MinimumOpacity = 1;
    public const double MaximumOpacity = 100;
    public const double DefaultOpacity = 100;

    [Range(MinimumOpacity, MaximumOpacity)]
    private string opacityString = DefaultOpacity.ToString();
    public double LastValidOpacity { get; set; }

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
                OnPropertyChangedOpacity(nameof(this.Opacity), value);
            }
        }
    }
    private void OnPropertyChangedOpacity(string propertyName, string value)
    {
        if (string.IsNullOrEmpty(value))
            return;

        if (double.TryParse(value, CultureInfo.CurrentCulture, out double doubleValue))
        {
            double clampedValue = Math.Clamp(doubleValue, MinimumOpacity, MaximumOpacity);
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
        get
        {
            return double.TryParse(this.OpacityString, CultureInfo.CurrentCulture, out double value)
            ? value : DefaultOpacity;
        }
        set
        {
            OnPropertyChanged(nameof(Opacity));
            this.OpacityString = value.ToString();
        }
    }

    /// <summary>
    /// Use This value for any opacity properties, as opacity is a value between 0 and 1.0
    /// </summary>
    public double RealOpacity => Opacity / 100;
    #endregion

    #region Shape Fill
    public Brush shapeStroke = Brushes.Black;
    public Brush shapeFill = Brushes.Transparent;
    //TODO: Shape Fill
    #endregion
}