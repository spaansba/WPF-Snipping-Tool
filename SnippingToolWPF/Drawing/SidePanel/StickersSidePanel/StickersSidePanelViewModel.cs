namespace SnippingToolWPF.SidePanel.StickersSidePanel;

public sealed class StickersSidePanelViewModel : SidePanelViewModel
{
    public StickersSidePanelViewModel(DrawingViewModel drawingViewModel) : base(drawingViewModel)
    {
    }

    public override string Header => "Stickers";
}