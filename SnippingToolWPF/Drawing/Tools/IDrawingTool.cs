using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SnippingToolWPF.Drawing.Tools
{
    public interface IDrawingTool
    {
        public UIElement? Visual { get; }
        public DrawingToolAction LeftButtonDown(Point position);
        public DrawingToolAction MouseMove(Point position);
        public DrawingToolAction LeftButtonUp();
    }

    /// <summary>
    /// Use Begin and Continue from IDrawingTool
    /// Finish returns T UIElement so we override it
    /// </summary>
    public interface IDrawingTool<out T> : IDrawingTool
        where T : UIElement
    {
        public new T? Visual { get; }
        UIElement? IDrawingTool.Visual => this.Visual;
    }
}
