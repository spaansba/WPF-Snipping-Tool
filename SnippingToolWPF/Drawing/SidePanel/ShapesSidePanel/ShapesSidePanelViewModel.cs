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
    private static readonly Brush shapeBorderStroke = Brushes.Black;
    public override string Header => "Shapes";

    #region Shape/Tool Selection 
    public List<Shape> Shapes { get; set; }
    public readonly Shape ChosenShape = new Rectangle();
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
    public ShapesSidePanelViewModel(DrawingViewModel drawingViewModel) : base(drawingViewModel)
    {
        this.tool = new ShapeTool(this);

        Shapes = new List<Shape>();
        Shapes.Add(new Rectangle { Width = shapeSize, Height = shapeSize, Stroke = shapeBorderStroke });
        Shapes.Add(new Ellipse { Width = shapeSize, Height = shapeSize, Stroke = shapeBorderStroke });
    }
}
