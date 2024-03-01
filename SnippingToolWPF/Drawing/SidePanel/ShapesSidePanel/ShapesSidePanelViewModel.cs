﻿using SnippingToolWPF.Drawing.Tools;
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
    public override string Header => "Shapes";

    #region Shape/Tool Selection 
   
    private IDrawingTool? tool;
    public override IDrawingTool? Tool => tool;

    public ShapeOptions shapeOption = ShapeOptions.Rectangle;

    private Shape? shapeSelected;
    public Shape? ShapeSelected
    {
        get => shapeSelected;
        set
        {
            if (value is not null)
            {
                shapeSelected = value;
                shapeOption = value.Tag switch
                {
                    ShapeOptions.Rectangle => ShapeOptions.Rectangle,
                    ShapeOptions.Triangle => ShapeOptions.Triangle,
                    ShapeOptions.Ellipse => ShapeOptions.Ellipse,
                    ShapeOptions.Pentagon => ShapeOptions.Pentagon,
                    _ => ShapeOptions.Rectangle,
                };
                this.tool = new ShapeTool(this);
            }
        }
    }

    #endregion

    #region ctor
    public List<Shape> Shapes { get; set; }

    public ShapesSidePanelViewModel(DrawingViewModel drawingViewModel) : base(drawingViewModel)
    {
        this.tool = new ShapeTool(this);
        Shapes = CreateButtonShapes();
    }

    /// <summary>
    /// Create the shapes presented on the buttons in the sidepanel using Linq
    /// </summary>
    public static List<Shape> CreateButtonShapes() =>
    Enum.GetValues(typeof(ShapeOptions))
        .Cast<ShapeOptions>()
        .Select(option =>
        {
            var shape = CreateInitialShape.Create(option, 1.0, Brushes.Black);
            shape.Tag = option;
            return shape;
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
