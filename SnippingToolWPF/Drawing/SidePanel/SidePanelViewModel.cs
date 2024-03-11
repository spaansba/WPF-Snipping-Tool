using CommunityToolkit.Mvvm.ComponentModel;
using SnippingToolWPF.Tools.ToolAction;

namespace SnippingToolWPF.SidePanel;

public abstract class SidePanelViewModel : ObservableValidator
{
    public abstract string Header { get; }
    protected DrawingViewModel DrawingViewModel { get; }
    protected SidePanelViewModel(DrawingViewModel drawingViewModel)
    {
        this.DrawingViewModel = drawingViewModel;
    }
    public virtual IDrawingTool? Tool => null;
}
