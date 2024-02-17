using CommunityToolkit.Mvvm.ComponentModel;
using SnippingToolWPF.Drawing.Tools;

namespace SnippingToolWPF;

public abstract class SidePanelViewModel : ObservableValidator
{
    public abstract string Header { get; }
    protected DrawingViewModel drawingViewModel { get; }
    protected SidePanelViewModel(DrawingViewModel drawingViewModel)
    {
        this.drawingViewModel = drawingViewModel;
    }
    public virtual IDrawingTool? Tool {get; }
}
