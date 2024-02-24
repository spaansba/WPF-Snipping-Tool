using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SnippingToolWPF.Drawing.Tools.ShapeTools;

public abstract class ShapeCreator : Shape
{
    public virtual void SetStartingPoint(Point startingPoint)
    {
        StartingPoint = startingPoint;
        EndingPoint = startingPoint;
        UpdateGeometry();
    }
    public virtual void ChangeEndingPoint(Point endingPoint)
    {
        EndingPoint = endingPoint;
        UpdateGeometry();
    }
    public virtual void UpdateGeometry()
    {
        OuterRectangle = new RectangleGeometry(new Rect(
            Math.Min(StartingPoint.X, EndingPoint.X),
            Math.Min(StartingPoint.Y, EndingPoint.Y),
            Math.Abs(StartingPoint.X - EndingPoint.X),
            Math.Abs(StartingPoint.Y - EndingPoint.Y)
        ));
    }
    public abstract void Reset();
    public Point StartingPoint { get; set; } // Start of rectangle
    public Point EndingPoint { get; set; } // End of rectangle
    public RectangleGeometry? OuterRectangle { get; set; } // The rectangle we use as outline for our shapes
    public double Rleft => OuterRectangle?.Rect.Left ?? 0;
    public double Rtop => OuterRectangle?.Rect.Top ?? 0;
    public double Rright => OuterRectangle?.Rect.Right ?? 0;
    public double Rbottom => OuterRectangle?.Rect.Bottom ?? 0;
    public double Rwidth => OuterRectangle?.Rect.Width ?? 0;
    public double Rheigth => OuterRectangle?.Rect.Height ?? 0;
    protected override Geometry? DefiningGeometry { get; } // This is the Geometry that holds the instructions on how to draw our shape

}

