using System.ComponentModel;
using System.Windows.Media;

namespace SnippingToolWPF.Drawing.Editing;

/// <summary>
/// Interface for our DrawingShape, if a class does not want to abstract from DrawingShape it can still implement this interface 
/// </summary>
public interface IShape
{
    public Brush? Fill { get; }
    public Brush? Stroke { get; }
    public string? Text { get; }
    public double StrokeThickness { get; }
    public double FontSize { get; }
    /// <summary>
    /// AKA > X value
    /// </summary>
    public double Left { get; }
    /// <summary>
    /// AKA > Y value
    /// </summary>
    public double Top { get; }
    public double Width { get; }
    public double Height { get; }
    public double Angle { get; }
}
