using SnippingToolWPF.Drawing.Tools.ShapeTools;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SnippingToolWPF.Drawing.Tools.ToolAction;

//TODO: Make this class generic for all Shapes
public sealed class ShapeTool : IDrawingTool<TriangleShape>
{
    private readonly ShapesSidePanelViewModel options;

    public TriangleShape Visual { get; set; }
    public IShapeCreator? ShapeCreator { get; set; }

    public ShapeTool(ShapesSidePanelViewModel options)
    {
        this.options = options;
        this.Visual = new TriangleShape();
    }

    public DrawingToolAction LeftButtonDown(Point position, UIElement? item)
    {
        this.Visual.StrokeThickness = 2;
        this.Visual.Stroke = new SolidColorBrush(Colors.Black);
        this.Visual.SetStartingPoint(position);
        return DrawingToolAction.StartMouseCapture();
    }

    public DrawingToolAction MouseMove(Point position, UIElement? item)
    {
        this.Visual.ChangeEndingPoint(position);
        return DrawingToolAction.DoNothing;
    }

    public DrawingToolAction LeftButtonUp()
    {
        TriangleShape triangleShape = Visual;
        this.Visual.Reset();
        return new DrawingToolAction(StartAction: DrawingToolActionItem.Shape(triangleShape), StopAction: DrawingToolActionItem.MouseCapture()).WithUndo();
    }
}
