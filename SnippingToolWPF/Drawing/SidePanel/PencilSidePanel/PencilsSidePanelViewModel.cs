using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Windows.Media;
using SnippingToolWPF.Tools.PenTools;
using SnippingToolWPF.Tools.ToolAction;

namespace SnippingToolWPF.SidePanel.PencilSidePanel;

public sealed class PencilsSidePanelViewModel : SidePanelViewModel
{
    public PencilsSidePanelViewModel(DrawingViewModel drawingViewModel) : base(drawingViewModel)
    {
        LastValidThickness = DefaultThickness;
        LastValidOpacity = DefaultOpacity;
        SelectedBrush = new SolidColorBrush(defaultPenColor);
        tool = new PencilTool(this);
    }

    public override string Header => "Pencils";

    #region Pen Tip Shape

    public bool PenTipArrow { get; set; }

    #endregion

    #region Tool Selection

    private PencilTool? pencilTool;
    private PencilTool PencilTool => pencilTool ??= new PencilTool(this);

    private EraserTool? eraserTool;
    private EraserTool EraserTool => eraserTool ??= new EraserTool();

    private IDrawingTool? tool;
    public override IDrawingTool? Tool => tool;

    private PencilOptions pencilOption = PencilOptions.Pen;

    public PencilOptions PencilOption
    {
        get => pencilOption;
        set
        {
            if (!SetProperty(ref pencilOption, value)) return;
            IDrawingTool newTool = value switch
            {
                PencilOptions.Eraser => EraserTool,
                //PencilOptions.Pen => EllipsePenTool,
                //PencilOptions.Calligraphy => CalligraphyTool,
                //PencilOptions.RegularPencil => RegularPencilTool,
                //PencilOptions.Chalk => ChalkTool,
                //PencilOptions.Graffiti => GraffitiTool,
                //PencilOptions.Bucket => BucketTool,
                //PencilOptions.Oil => OilTool,
                _ => PencilTool
            };
            SetProperty(ref tool, newTool, nameof(Tool));
        }
    }

    #endregion

    #region Thickness - Connecting the Slider / Textbox with eachother and data clamping

    public const double MinimumThickness = 1;
    public const double MaximumThickness = 100;
    private const double DefaultThickness = 6;
    
    [Range(MinimumThickness, MaximumThickness)]
    private string thicknessString = DefaultThickness.ToString(CultureInfo.InvariantCulture);

    private double LastValidThickness { get; set; }

    /// <summary>
    ///     ThicknessString is bound to the textbox, on property change, clamp the value if needed and update the slider
    ///     (Thickness)
    /// </summary>
    public string ThicknessString
    {
        get => thicknessString;
        set
        {
            if (SetProperty(ref thicknessString, value, true)) OnPropertyChangedThickness(value);
        }
    }

    private void OnPropertyChangedThickness(string value)
    {
        if (string.IsNullOrEmpty(value))
            return;

        if (double.TryParse(value, CultureInfo.CurrentCulture, out var doubleValue))
        {
            var clampedValue = Math.Clamp(doubleValue, MinimumThickness, MaximumThickness);
            Thickness = clampedValue;
            LastValidThickness = clampedValue;
        }
        else //invalid entry use last valid thickness
        {
            Thickness = LastValidThickness;
        }
    }

    /// <summary>
    ///     Thickness is bound to the slider, on change change the textbox.
    /// </summary>
    public double Thickness
    {
        get => double.TryParse(ThicknessString, CultureInfo.CurrentCulture, out var value)
            ? value
            : DefaultThickness;
        set
        {
            OnPropertyChanged();
            ThicknessString = value.ToString(CultureInfo.InvariantCulture);
        }
    }

    #endregion

    #region Opacity - Connecting the Slider / Textbox with eachother and data clamping

    public const double MinimumOpacity = 1;
    public const double MaximumOpacity = 100;
    private const double DefaultOpacity = 100;

    [Range(MinimumOpacity, MaximumOpacity)]
    private string opacityString = DefaultOpacity.ToString(CultureInfo.InvariantCulture);

    public double LastValidOpacity { get; set; }

    /// <summary>
    ///     OpacityString is bound to the textbox, on property change, clamp the value if needed and update the slider
    ///     (Opacity)
    /// </summary>
    public string OpacityString
    {
        get => opacityString;
        set
        {
            if (SetProperty(ref opacityString, value, true)) OnPropertyChangedOpacity(value);
        }
    }

    private void OnPropertyChangedOpacity(string value)
    {
        if (string.IsNullOrEmpty(value))
            return;

        if (double.TryParse(value, CultureInfo.CurrentCulture, out var doubleValue))
        {
            var clampedValue = Math.Clamp(doubleValue, MinimumOpacity, MaximumOpacity);
            Opacity = clampedValue;
            LastValidOpacity = clampedValue;
        }
        else //invalid entry use last valid Opacity
        {
            Opacity = LastValidOpacity;
        }
    }

    /// <summary>
    ///     Opacity is bound to the slider, OpacityString to the textbox.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public double Opacity
    {
        get => double.TryParse(OpacityString, CultureInfo.CurrentCulture, out var value)
            ? value
            : DefaultOpacity;
        set
        {
            OnPropertyChanged();
            OpacityString = value.ToString(CultureInfo.InvariantCulture);
        }
    }

    /// <summary>
    ///     Use This value for any opacity properties, as opacity is a value between 0 and 1.0
    /// </summary>
    public double RealOpacity => Opacity / 100;

    #endregion

    #region Selected Color

    private static readonly Color defaultPenColor = Colors.Black;
    
    private Color? selectedStroke;
    // Brush that is in connection with the SelectedColor
    public Brush? SelectedBrush { get; private set; }

    public Color? SelectedStroke
    {
        get => selectedStroke;
        set
        {
            if (!SetProperty(ref selectedStroke, value)) return;
            SelectedBrush = value.HasValue ? new SolidColorBrush(value.Value) : new SolidColorBrush(defaultPenColor);
        }
    }

    #endregion
}