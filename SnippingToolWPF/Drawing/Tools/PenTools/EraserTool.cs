using SnippingToolWPF.Drawing.Editing;
using SnippingToolWPF.Drawing.Shapes;
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

    public DrawingShape? DrawingShape => null;

    public bool LockedAspectRatio => throw new NotImplementedException();

    public bool IsDrawing {get; set; } = false;

    #region Mouse Events
    public DrawingToolAction LeftButtonDown(Point position, DrawingShape? item)
    {
        IsDrawing = true;
        if (item is not null)
            return DrawingToolAction.RemoveShape(item).WithUndo();
        
        return DrawingToolAction.DoNothing;
    }

    public DrawingToolAction MouseMove(Point position, DrawingShape? item)
    {
        if (!IsDrawing)
            return DrawingToolAction.DoNothing;
        if (item is not null)
            return DrawingToolAction.RemoveShape(item).WithUndo();
        return DrawingToolAction.DoNothing;
    }

    public DrawingToolAction LeftButtonUp()
    {
        IsDrawing = false;
        return DrawingToolAction.StopMouseCapture();
    }

    public void RightButtonDown()
    {
    }
    #endregion
}
