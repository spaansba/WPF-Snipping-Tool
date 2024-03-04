using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SnippingToolWPF.Drawing.Tools.ToolAction
{
    //TODO: Implement this in PencilTool
    public abstract class DraggingTool<TVisual> : IDrawingTool<TVisual> where TVisual : UIElement
    {
        public TVisual? Visual { get; }

        public bool LockedAspectRatio => throw new NotImplementedException();

        protected abstract void Start(Point position);
        protected abstract void Continue(Point position);
        protected abstract DrawingToolAction Finish();
        protected abstract void Reset();

        public virtual DrawingToolAction LeftButtonDown(Point position, UIElement? item)
        {
            this.Start(position);
            return DrawingToolAction.StartMouseCapture();
        }

        public virtual DrawingToolAction LeftButtonUp()
        {
            var result = this.Finish();
            result = result with { StopAction = result.StopAction.Combine(DrawingToolActionItem.MouseCapture()) };
            this.Reset();
            return result;
        }

        public virtual DrawingToolAction MouseMove(Point position, UIElement? item)
        {
            this.Continue(position);
            return DrawingToolAction.DoNothing;
        }

    }
}
