using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnippingToolWPF.Drawing.Tools.PolygonTools;
/// <summary>
/// To add a shape:
/// 1. Put its name in PolygonOptions
/// 2. Add its points to CreateInitialShape
/// 3. Add it to the switch expression in CreateInitialShape.Create
/// </summary>
public enum PolygonOptions
{
    Rectangle,
    Triangle,
    Ellipse, // Not a polygon but bare with me...
    Diamond,
    Pentagon,
    Hexagon,
    Heptagon,
    Hectagon,
    Star,
}
