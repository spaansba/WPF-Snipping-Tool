using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnippingToolWPF.Drawing.Tools.ShapeTools;
/// <summary>
/// To add a shape:
/// 1. Put its name in ShapeOptions
/// 2. Add its points to CreateInitialShape
/// 3. Add it to the switch expression in CreateInitialShape.Create
/// </summary>
public enum ShapeOptions
{
    Rectangle,
    Triangle,
    Ellipse,
    Diamond,
    Pentagon,
    Hexagon,
    Heptagon,
    Hectagon,
    Star,
}
