using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.ComponentModel;
using SnippingToolWPF.Drawing.Tools;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace SnippingToolWPF;

public sealed partial class PencilsSidePanelViewModel : SidePanelViewModel
{
    public override string Header => "Pencils";

    public PencilsSidePanelViewModel(DrawingViewModel drawingViewModel) : base(drawingViewModel)
    {
        LastValidThickness = DefaultThickness;
        LastValidOpacity = DefaultOpacity;
        SelectedBrush = new SolidColorBrush(DefaultPenColor);
        this.tool = new PencilTool(this);
    }

    #region Tool Selection

    private PencilTool? pencilTool;
    private PencilTool PencilTool => this.pencilTool ??= new PencilTool(this);

    private EraserTool? eraserTool;
    private EraserTool EraserTool => this.eraserTool ??= new EraserTool(this, drawingViewModel);

    private IDrawingTool? tool;
    public override IDrawingTool? Tool => tool;

    private PencilOptions pencilOption = PencilOptions.Pen;
    public PencilOptions PencilOption
    {
        get => pencilOption;
        set
        {
           if(SetProperty(ref this.pencilOption, value))
            {
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
            if(this.SetProperty(ref thicknessString, value, validate: true))
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

    /// <summary>
    /// Thickness is bound to the slider, on change change the textbox.
    /// </summary>
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

    #region Opacity - Connecting the Slider / Textbox with eachother and data clamping
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

    #region Selected Color

    private Color DefaultPenColor = Colors.Black;
    private Color? selectedColor;
    public Color? SelectedColor
    {
        get => selectedColor;
        set
        {
            if (SetProperty(ref selectedColor, value))
            {
                if (value.HasValue)
                {
                    SelectedBrush = new SolidColorBrush(value.Value);
                }
                else
                {
                    SelectedBrush = new SolidColorBrush(DefaultPenColor); 
                }
            }
        }
    }
    // Brush that is in connection with the SelectedColor
    public Brush? SelectedBrush;

    #endregion

}
