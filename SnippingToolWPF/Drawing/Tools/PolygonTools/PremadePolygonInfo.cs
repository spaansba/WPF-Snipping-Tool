using System.Windows;
using System.Windows.Media;

namespace SnippingToolWPF.Tools.PolygonTools;

public sealed record PremadePolygonInfo(
    int NumberOfSides = 3,
    double RotationAngle = 0,
    bool IsStar = false)
{
    private const double StarInnerCircleSize = 0.6;

    public PointCollection GeneratedPoints => new PointCollection(GeneratePoints());

    public Point[] GeneratePoints()
    {
        return !IsStar
            ? CreateInitialPolygon.GeneratePolygonPoints(NumberOfSides, RotationAngle)
            : CreateInitialPolygon.GenerateStarPoints(NumberOfSides / 2, RotationAngle, StarInnerCircleSize);
    }
}