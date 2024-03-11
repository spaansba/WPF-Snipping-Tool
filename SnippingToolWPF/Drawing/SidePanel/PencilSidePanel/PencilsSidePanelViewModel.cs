using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Windows.Media;
using SnippingToolWPF.Tools.PenTools;
using SnippingToolWPF.Tools.ToolAction;

namespace SnippingToolWPF.SidePanel.PencilSidePanel;

public sealed class PencilsSidePanelViewModel : SidePanelViewModel
{
    public override string Header => "Pencils";

    public PencilsSidePanelViewModel(DrawingViewModel drawingViewModel) : base(drawingViewModel)
    {
        LastValidThickness = DefaultThickness;
        LastValidOpacity = DefaultOpacity;
        SelectedBrush = new SolidColorBrush(defaultPenColor);
        this.tool = new PencilTool(this);
    }

    #region Tool Selection

    private PencilTool? pencilTool;
    private PencilTool PencilTool => this.pencilTool ??= new(this);

    private EraserTool? eraserTool;
    private EraserTool EraserTool => this.eraserTool ??= new();

    private IDrawingTool? tool;
    public override IDrawingTool? Tool => tool;

    private PencilOptions pencilOption = PencilOptions.Pen;
    public PencilOptions PencilOption
    {
        get => pencilOption;
        set
        {
            if (!SetProperty(ref this.pencilOption, value)) return;
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
                _ => PencilTool,
            };
            this.SetProperty(ref this.tool, newTool,nameof(this.Tool));
        }
    }
    #endregion

    #region Pen Tip Shape

    public bool PenTipArrow { get; set; }


    #endregion

    #region Thickness - Connecting the Slider / Textbox with eachother and data clamping

    public const double MinimumThickness = 1;
    public const double MaximumThickness = 100;
    private const double DefaultThickness = 6;


    [Range(MinimumThickness, MaximumThickness)]
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
            if(this.SetProperty(ref thicknessString, value, validate: true))
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

    /// <summary>
    /// Thickness is bound to the slider, on change change the textbox.
    /// </summary>
    public double Thickness
    {
        get => double.TryParse(this.ThicknessString, CultureInfo.CurrentCulture, out var value)
                ? value : DefaultThickness;
        set
        {
            OnPropertyChanged();
            this.ThicknessString = value.ToString(CultureInfo.InvariantCulture);
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

    #region Selected Color

    private readonly Color defaultPenColor = Colors.Black;
    private Color? selectedColor;
    public Color? SelectedColor
    {
        get => selectedColor;
        set
        {
            if (!SetProperty(ref selectedColor, value)) return;
            if (value.HasValue)
            {
                SelectedBrush = new SolidColorBrush(value.Value);
            }
            else
            {
                SelectedBrush = new SolidColorBrush(defaultPenColor); 
            }
        }
    }
    // Brush that is in connection with the SelectedColor
    public Brush? SelectedBrush;

    #endregion

}
