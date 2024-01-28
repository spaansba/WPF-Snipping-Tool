using CommunityToolkit.Mvvm.ComponentModel;

namespace SnippingToolWPF
{
    public sealed partial class PencilsSidePanelViewModel : SidePanelViewModel
    {
        public override string Header => "Pencils";

        [ObservableProperty]
        private PencilOptions pencilOption = PencilOptions.Pen;

        public PencilsSidePanelViewModel(DrawingViewModel drawingViewModel) : base(drawingViewModel)
        {
        }
    }
}
