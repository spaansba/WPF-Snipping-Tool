using CommunityToolkit.Mvvm.ComponentModel;
using SnippingToolWPF.Tools.ToolAction;

namespace SnippingToolWPF.SidePanel;

public abstract class SidePanelViewModel : ObservableValidator
{
    protected SidePanelViewModel(DrawingViewModel drawingViewModel)
    {
        DrawingViewModel = drawingViewModel;
    }

    public abstract string Header { get; }
    protected DrawingViewModel DrawingViewModel { get; }
    public virtual IDrawingTool? Tool => null;
}