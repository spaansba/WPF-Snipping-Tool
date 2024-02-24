using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SnippingToolWPF.Drawing.Tools.ShapeTools;

public class TriangleShape : ShapeCreator
{
    public RectangleGeometry? OuterRectangle { get; private set; }
    public Point StartingPoint { get; set; }
    public Point EndingPoint { get; set; }
    protected override Geometry DefiningGeometry => triangleGeometry;

    private PathGeometry triangleGeometry = new PathGeometry();

    public void SetStartingPoint(Point startingPoint)
    {
        StartingPoint = startingPoint;
        EndingPoint = startingPoint;
        UpdateTriangleGeometry();
    }

    public void ChangeEndingPoint(Point endingPoint)
    {
        EndingPoint = endingPoint;
        UpdateTriangleGeometry();
    }

    private void UpdateTriangleGeometry()
    {
        // Calculate rectangle dimensions
        double left = Math.Min(StartingPoint.X, EndingPoint.X);
        double top = Math.Min(StartingPoint.Y, EndingPoint.Y);
        double width = Math.Abs(StartingPoint.X - EndingPoint.X);
        double height = Math.Abs(StartingPoint.Y - EndingPoint.Y);

        // Create an invisible rectangle geometry
        OuterRectangle = new RectangleGeometry(new Rect(left, top, width, height));

        // Create a triangle within the rectangle
        PathFigure pathFigure = new PathFigure();
        pathFigure.StartPoint = new Point(left + width / 2, top); // Start the triangle in the middel of the topleft / topright of the rectangle
        pathFigure.Segments.Add(new LineSegment(new Point(left, top + height), true)); // Line to the Bottom Left
        pathFigure.Segments.Add(new LineSegment(new Point(left + width, top + height), true)); // Line to the bottom right
        pathFigure.IsClosed = true; // Makes the last line back to the first line
        triangleGeometry.Figures.Clear();
        triangleGeometry.Figures.Add(pathFigure);
    }
    public void Reset()
    {
        triangleGeometry.Figures.Clear();
    }
}
