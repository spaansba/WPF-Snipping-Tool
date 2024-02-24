using SnippingToolWPF.Drawing.Tools.ShapeTools;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SnippingToolWPF.Drawing.Tools.ToolAction;

//TODO: Make this class generic for all Shapes
public sealed class ShapeTool : IDrawingTool<ShapeCreator>
{
    private readonly ShapesSidePanelViewModel options;

    public ShapeCreator Visual { get; set; }

    public ShapeTool(ShapesSidePanelViewModel options)
    {
        this.options = options;
        this.Visual = options.ChosenShape; //TODO: I dont think this will update between tool/shape switches
    }

    public DrawingToolAction LeftButtonDown(Point position, UIElement? item)
    {
        this.Visual.Stroke = options.shapeStroke;
        this.Visual.StrokeThickness = options.shapeStrokeThickness;
        this.Visual.Opacity = options.shapeOpacity;
        this.Visual.Fill = options.shapeFill;
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
        ShapeCreator createdShape = Visual;
 //       this.Visual.Reset();
        return new DrawingToolAction(StartAction: DrawingToolActionItem.Shape(createdShape), StopAction: DrawingToolActionItem.MouseCapture()).WithUndo();
    }
}
