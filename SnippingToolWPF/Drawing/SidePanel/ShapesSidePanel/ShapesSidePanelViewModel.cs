using SnippingToolWPF.Drawing.Tools;
using SnippingToolWPF.Drawing.Tools.ShapeTools;
using SnippingToolWPF.Drawing.Tools.ToolAction;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SnippingToolWPF;

public sealed class ShapesSidePanelViewModel : SidePanelViewModel
{
    private const int shapeSize = 20;
    private static readonly Brush shapeMenuBorderStroke = Brushes.Black;
    public override string Header => "Shapes";

    #region Shape/Tool Selection 
    public List<Shape> Shapes { get; set; }
    public ShapeCreator ChosenShape = new RectangleShape(); // Default Shape
    private ShapeTool? shapeTool;
    private ShapeTool ShapeTool => this.shapeTool ??= new ShapeTool(this);

    private IDrawingTool? tool;
    public override IDrawingTool? Tool => tool;

    private ShapeOptions shapeOption = ShapeOptions.Rectangle;
    public ShapeOptions ShapeOption
    {
        get => shapeOption;
        set
        {
            if (SetProperty(ref this.shapeOption, value))
            {
                IDrawingTool newTool = value switch
                {
                    ShapeOptions.Rectangle => ShapeTool,
                    _ => ShapeTool,
                };
                this.SetProperty(ref this.tool, newTool, nameof(this.Tool));
            }
        }
    }
    #endregion

    #region Shape Stroke / Thickness
    //TODO: Shape Thickness / Stroke
    public int shapeStrokeThickness = 1;
    public Brush shapeStroke = Brushes.Black;
    #endregion

    #region Shape Opacity
    public double shapeOpacity = 1;
    //TODO: Shape Opacity
    #endregion

    #region Shape Fill
    public Brush shapeFill = Brushes.Transparent;
    //TODO: Shape Fill
    #endregion
    public ShapesSidePanelViewModel(DrawingViewModel drawingViewModel) : base(drawingViewModel)
    {
        this.tool = new ShapeTool(this);

        Shapes = new List<Shape>();
        Shapes.Add(new Rectangle { Width = shapeSize, Height = shapeSize, Stroke = shapeMenuBorderStroke });
        Shapes.Add(new Ellipse { Width = shapeSize, Height = shapeSize, Stroke = shapeMenuBorderStroke });
    }
}
