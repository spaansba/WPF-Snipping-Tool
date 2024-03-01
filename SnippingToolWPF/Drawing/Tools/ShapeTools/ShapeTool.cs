using SnippingToolWPF.Drawing.Tools.ShapeTools;
using SnippingToolWPF.ExtensionMethods;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SnippingToolWPF.Drawing.Tools.ToolAction;

//TODO: Make this class generic for all Shapes
public sealed class ShapeTool : IDrawingTool<Shape>
{
    private readonly ShapesSidePanelViewModel options;

    public Shape Visual { get; set; }

    private Point startPoint;

    public ShapeTool(ShapesSidePanelViewModel options)
    {
        this.options = options;
        this.Visual = CreateInitialShape.Create(this.options.shapeOption);
    }

    public DrawingToolAction LeftButtonDown(Point position, UIElement? item)
    {
        startPoint = position;
        this.Visual.Stroke = options.shapeStroke;
        this.Visual.StrokeThickness = options.shapeStrokeThickness;
        this.Visual.Opacity = options.shapeOpacity;
        this.Visual.Fill = options.shapeFill;
        Canvas.SetLeft(this.Visual, position.X);
        Canvas.SetTop(this.Visual, position.Y);
        return DrawingToolAction.StartMouseCapture();
    }

    public DrawingToolAction MouseMove(Point position, UIElement? item)
    {
        // Set current size of the shape (like a rectangle)
        this.Visual.Width = Math.Abs(position.X - startPoint.X);
        this.Visual.Height = Math.Abs(position.Y - startPoint.Y);

        // Set the new position of the shape (we do this because otherwise a shape can only be drawn to bottom right and not in any direction)
        Canvas.SetLeft(this.Visual, Math.Min(startPoint.X, position.X));
        Canvas.SetTop(this.Visual, Math.Min(startPoint.Y, position.Y));
        return DrawingToolAction.DoNothing;
        
    }
    public DrawingToolAction LeftButtonUp()
    {
        var finalShape = this.Visual.Clone();
        this.Visual.Width = 0;
        this.Visual.Height = 0;
        return new DrawingToolAction(StartAction: DrawingToolActionItem.Shape(finalShape), StopAction: DrawingToolActionItem.MouseCapture()).WithUndo();
    }
}
