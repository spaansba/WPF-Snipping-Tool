using SnippingToolWPF.Drawing.Tools;
using SnippingToolWPF.Drawing.Tools.PolygonTools;
using SnippingToolWPF.Drawing.Tools.ToolAction;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SnippingToolWPF;

public sealed class ShapesSidePanelViewModel : SidePanelViewModel
{
    public override string Header => "Shapes";

    #region Shape/Tool Selection 
   
    private IDrawingTool? tool;
    public override IDrawingTool? Tool => tool;

    public PolygonOptions polygonOption = PolygonOptions.Rectangle;

    /// <summary>
    /// Get the Shape selected by the user in the sidepanel, the enum is stored in the shapes Tag
    /// </summary>
    private Shape? polygonSelected;
    public Shape? PolygonSelected
    {
        get => polygonSelected;
        set
        {
            if (value is not null)
            {
                polygonSelected = value;
                polygonOption = value.Tag as PolygonOptions? ?? PolygonOptions.Rectangle;
                UpdateTool(); // Update the Tool to give it the new shape
            }
        }
    }
    /// <summary>
    /// Used to update the Tool Property and notify the change
    /// </summary>
    private void UpdateTool()
    {
        tool = new PolygonTool(this);
        OnPropertyChanged(nameof(Tool)); // Notify the change 
    }

    #endregion

    #region ctor
    public List<Shape> Polygons { get; set; }

    public ShapesSidePanelViewModel(DrawingViewModel drawingViewModel) : base(drawingViewModel)
    {
        UpdateTool();
        Polygons = CreateButtonShapes();
    }

    /// <summary>
    /// Create the shapes presented on the buttons in the sidepanel using Linq
    /// </summary>
    public static List<Shape> CreateButtonShapes() =>
    Enum.GetValues(typeof(PolygonOptions))
        .Cast<PolygonOptions>()
        .Select(option =>
        {
            var polygon = CreateInitialPolygon.Create(option, 1.0, Brushes.Black);
            polygon.Tag = option;
            return polygon;
        })
        .ToList();

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
}
