
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SnippingToolWPF.Screenshot.Preview;

public class PreviewRectangle : Preview
{
    private Rectangle previewRect;

    public PreviewRectangle(BitmapSource rectFilling)
    {
        this.previewRect = new Rectangle { Stroke = Brushes.Black, StrokeThickness = 1 };
        FillBitmap = rectFilling;
    }

    public Rectangle CreatePreviewRectangle(Point begin, Point end)
    {
        // Set the size and position of the rectangle
        Canvas.SetLeft(previewRect, Math.Min(begin.X, end.X));
        Canvas.SetTop(previewRect, Math.Min(begin.Y, end.Y));

        previewRect.Width = Math.Abs(end.X - begin.X);
        previewRect.Height = Math.Abs(end.Y - begin.Y);

        if (previewRect.Height != 0 && previewRect.Width != 0)
        {
            FillShapeWithBitmap(previewRect, begin, end);
        }

        return previewRect;
    }

}
