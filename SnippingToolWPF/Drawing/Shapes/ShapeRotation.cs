using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using SnippingToolWPF.Common;
using SnippingToolWPF.ExtensionMethods;

namespace SnippingToolWPF;

public class ShapeRotation
{
    public RotationThumb RotationThumb { get; } = new();
    public AngleCircle AngleCircle { get; }

    private readonly DrawingShape childElement;
    public ShapeRotation(DrawingShape adornedElement, Size parentSize)
    {
        this.childElement = adornedElement;
        this.AngleCircle = new AngleCircle(parentSize);
        this.RotationThumb.DragStarted += OnDragStarted;
        this.RotationThumb.DragCompleted += OnDragCompleted;
        this.RotationThumb.DragDelta += OnRotationDragDelta;
    }
    
    /// <summary>
    /// Starting Point relative to the Screen
    /// </summary>
    private Point startingPoint;
    /// <summary>
    /// Shape Center Point Relative to the screen
    /// </summary>
    private Point shapeCenter;
    private double currentAngle;
    private double totalAngleChange;
    
    private void OnDragStarted(object sender, DragStartedEventArgs e)
    {
        this.childElement.StartChanging();
        this.AngleCircle.MakeVisible();
        this.shapeCenter = childElement.PointToScreen(childElement.GetCenterPoint());
        this.startingPoint = RotationThumb.PointToScreen(Mouse.GetPosition(RotationThumb));
        this.currentAngle = childElement.Angle;
    }

    private void OnRotationDragDelta(object sender, DragDeltaEventArgs e)
    {
        
    //    Debug.WriteLine($"horChange {e.HorizontalChange}, verChange {e.VerticalChange}");
    
        var currentPoint = RotationThumb.PointToScreen(Mouse.GetPosition(RotationThumb));
     //   var currentPoint = this.startingPoint + new Vector(
            // e.HorizontalChange,
            // e.VerticalChange);
            
        // Debug.WriteLine($"startingPoint {startingPoint}, currentPoint {currentPoint}");

        var angleChange = MathExtra.AngleBetweenInDegrees(
            currentPoint.MakeRelativeTo(shapeCenter),startingPoint.MakeRelativeTo(shapeCenter));

        totalAngleChange += angleChange;
        
        // Debug.WriteLine($"startingPoint {startingPoint.MakeRelativeTo(shapeCenter)}, currentPoint {currentPoint.MakeRelativeTo(shapeCenter)}, shapeCenter {shapeCenter}");
        // Debug.WriteLine($"CurrentAngle {childElement.Angle}, angleChange {angleChange}. total angle change {totalAngleChange}");
        // Debug.WriteLine(" ");
        //
        this.childElement.Angle = currentAngle + totalAngleChange;
        this.AngleCircle.ChangeAngleTextBox(childElement.Angle);
    }

    private void OnDragCompleted(object sender, DragCompletedEventArgs e)
    {
        this.childElement.FinishChanging();
        this.AngleCircle.MakeVisible();
        this.totalAngleChange = 0;
    }
}