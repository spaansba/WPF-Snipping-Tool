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
        public void Begin(Point position);
        public void Continue(Point position);
        public UIElement? Finish();

    }

    /// <summary>
    /// Use Begin and Continue from IDrawingTool
    /// Finish returns T UIElement so we override it
    /// </summary>
    public interface IDrawingTool<out T> : IDrawingTool
        where T : UIElement
    {
        public new T? Visual { get; }
        public new T? Finish();
        UIElement? IDrawingTool.Finish() => this.Finish();
        UIElement? IDrawingTool.Visual => this.Visual;
    }
}
