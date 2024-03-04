using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;

namespace SnippingToolWPF.Drawing.Tools;

public sealed class EraserTool : IDrawingTool
{
    private readonly PencilsSidePanelViewModel options;
    private DrawingViewModel DrawingViewModel;
    public EraserTool(PencilsSidePanelViewModel options, DrawingViewModel drawingView)
    {
        this.DrawingViewModel = drawingView;
        this.options = options;
    }

    public UIElement? Visual => null;

    public bool LockedAspectRatio => throw new NotImplementedException();

    public DrawingToolAction LeftButtonDown(Point position, UIElement? item)
    {
        if (item is not null)
            return DrawingToolAction.RemoveShape(item).WithUndo();
        
        return DrawingToolAction.DoNothing;
    }

    public DrawingToolAction MouseMove(Point position, UIElement? item)
    {
        if (item is not null)
            return DrawingToolAction.RemoveShape(item).WithUndo();
        return DrawingToolAction.DoNothing;
    }

    public DrawingToolAction LeftButtonUp()
    {
        return DrawingToolAction.StopMouseCapture();
    }
}
