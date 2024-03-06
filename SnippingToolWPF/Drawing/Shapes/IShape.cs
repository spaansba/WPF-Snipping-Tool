using System.ComponentModel;
using System.Windows.Media;

namespace SnippingToolWPF.Drawing.Editing;

/// <summary>
/// Interface for our ShapeViewModel, if a class does not want to abstract from ShapeViewModel it can still implement this interface 
/// </summary>
public interface IShape : INotifyPropertyChanged
{
    public Brush? Fill { get; }
    public Brush? Stroke { get; }
    public double StrokeThickness { get; }
    public string? Text { get; }
    public double FontSize { get; }
    public double X { get; }
    public double Y { get; }
    public double Width { get; }
    public double Height { get; }
    public double DegreesRotated { get; }
}
