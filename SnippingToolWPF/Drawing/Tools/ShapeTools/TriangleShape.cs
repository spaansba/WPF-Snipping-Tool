using System.Windows;
using System.Windows.Media;
namespace SnippingToolWPF.Drawing.Tools.ShapeTools;

public class TriangleShape : ShapeCreator
{
    protected override Geometry DefiningGeometry => triangleGeometry;

    private readonly PathGeometry triangleGeometry = new PathGeometry();

    public override void UpdateGeometry()
    {
        base.UpdateGeometry();

        if (OuterRectangle is not null)
        {
            // Create a triangle within the rectangle
            PathFigure trianglePath = new PathFigure()
            {
                StartPoint = new Point(Rleft + Rwidth / 2, Rtop), // Start the triangle in the middel of the topleft / topright of the rectangle
                IsClosed = true,  // Makes the last line back to the first line
                Segments =
                {
                    new LineSegment(new Point(Rleft, Rtop + Rheigth), true), // Line to the Bottom Left
                    new LineSegment(new Point(Rleft + Rwidth, Rtop + Rheigth), true) // Line to bottom right

                }
            };
            triangleGeometry.Figures.Clear();
            triangleGeometry.Figures.Add(trianglePath);
        }
    }
    public override void Reset()
    {
        triangleGeometry.Figures.Clear();
    }
}
