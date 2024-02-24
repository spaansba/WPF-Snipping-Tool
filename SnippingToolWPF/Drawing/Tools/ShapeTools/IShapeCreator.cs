using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

public abstract class ShapeCreator : Shape
{
    protected void SetStartingPoint(Point startingPoint)
    {

    }
    protected void ChangeEndingPoint(Point endingPoint)
    {

    }
    protected void Reset()
    {

    }
    public RectangleGeometry? OuterRectangle { get; }
    public Point StartingPoint {  get; }
    public Point EndingPoint { get; }   
}

