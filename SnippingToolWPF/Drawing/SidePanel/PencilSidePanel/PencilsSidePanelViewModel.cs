using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.ComponentModel;
using SnippingToolWPF.Drawing.Tools;

namespace SnippingToolWPF
{
    public sealed partial class PencilsSidePanelViewModel : SidePanelViewModel
    {
        public PencilsSidePanelViewModel(DrawingViewModel drawingViewModel) : base(drawingViewModel)
        {
            LastValidThickness = DefaultThickness;
            LastValidOpacity = DefaultOpacity;
            this.Tool = new PencilTool(this);
        }

        public override string Header => "Pencils";

      //  private PencilOptions pencilOption = PencilOptions.Pen;

        public PencilOptions PencilOption
        {
            get => pencilOption;
            set
            {
                if (pencilOption != value)
                {
                    switch (value) 
                    {
                        case PencilOptions.Pen:
                            Tool = new PencilTool(this);
                            break;
                        case PencilOptions.Eraser:
                            Tool = new EraserTool(this, drawingViewModel);
                            break;
                        default:
                            Tool = new PencilTool(this);
                            break;
                    }
                    pencilOption = value;
                    OnPropertyChanged(nameof(Tool)); // Make sure the Tool updates succesfully
                }
            }
        }
        private PencilOptions pencilOption = PencilOptions.Pen;

        public override IDrawingTool? Tool { get; set; }

        #region Thickness - Connecting the Slider / Textbox with eachother and data clamping

        public const double MinimumThickness = 1;
        public const double MaximumThickness = 100;
        private const double DefaultThickness = 12;

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
    }
}
