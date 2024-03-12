namespace SnippingToolWPF.SidePanel.EditSidePanel;

public sealed class EditSidePanelViewModel : SidePanelViewModel
{
    public EditSidePanelViewModel(DrawingViewModel drawingViewModel) : base(drawingViewModel)
    {
        Console.WriteLine("Filler");
    }

    public override string Header => "Edit";
}