using System.Windows;
using System.Windows.Media;

namespace SnippingToolWPF.Tools.PolygonTools;

public sealed record PremadePolygonInfo(
    int NumberOfSides = 3,
    double RotationAngle = 0,
    double StarInnerCircleSize = 1.0)
{
    public PointCollection GeneratedPoints => new (CreateInitialPolygon.GeneralPolygonPoints(NumberOfSides, RotationAngle, StarInnerCircleSize));
}