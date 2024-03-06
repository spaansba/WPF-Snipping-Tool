using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SnippingToolWPF.Drawing.Tools;

public interface IDrawingTool
{
    public UIElement? Visual { get; }
    public bool LockedAspectRatio { get; } // Implement that if user holds shift while moving the mouse that aspect ratio stays perfect
    public bool IsDrawing {  get; }
    public DrawingToolAction LeftButtonDown(Point position, UIElement? item);
    public void RightButtonDown();
    public DrawingToolAction MouseMove(Point position, UIElement? item);
    public DrawingToolAction LeftButtonUp();
}

public interface IDrawingTool<out T> : IDrawingTool
    where T : UIElement
{
    public new T? Visual { get; }
    UIElement? IDrawingTool.Visual => this.Visual;
}
