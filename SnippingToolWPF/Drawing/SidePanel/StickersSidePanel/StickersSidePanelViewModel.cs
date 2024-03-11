namespace SnippingToolWPF.SidePanel.StickersSidePanel;

public sealed class StickersSidePanelViewModel : SidePanelViewModel
{
    public override string Header => "Stickers";
    public StickersSidePanelViewModel(DrawingViewModel drawingViewModel) : base(drawingViewModel)
    {
    }
}
