using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace SnippingToolWPF.Drawing.Tools.ShapeTools;

public class RectangleShape : ShapeCreator
{
    protected override Geometry DefiningGeometry => RectangleGeometry;

    private readonly PathGeometry RectangleGeometry = new PathGeometry();

    public override void UpdateGeometry()
    {
        base.UpdateGeometry();
        if (OuterRectangle is not null)
        {
            RectangleGeometry.Figures.Clear();

            var rectanglePath = new PathFigure
            {
                StartPoint = new Point(Rleft, Rtop),
                IsClosed = true,
                Segments =
                {
                    new LineSegment(new Point(Rright, Rtop), true),
                    new LineSegment(new Point(Rright, Rbottom), true),
                    new LineSegment(new Point(Rleft, Rbottom), true)
                }
            };
            RectangleGeometry.Figures.Add(rectanglePath);
        }
    }
    public override void Reset()
    {
        RectangleGeometry.Figures.Clear();
    }
}
