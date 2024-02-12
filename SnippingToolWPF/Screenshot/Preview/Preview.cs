using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SnippingToolWPF.Screenshot.Preview
{
    public abstract class Preview
    {
        public BitmapSource? FillBitmap;

        protected Shape FillShapeWithBitmap(Shape shapeToFill, Point begin, Point end)
        {
            // Calculate a rectangle in the image to crop 
            Int32Rect cropRect = new Int32Rect((int)Math.Min(begin.X, end.X), (int)Math.Min(begin.Y, end.Y),
                                                (int)Math.Abs(end.X - begin.X), (int)Math.Abs(end.Y - begin.Y));

            CroppedBitmap croppedBitmap = new CroppedBitmap(FillBitmap, cropRect);

            // Set the filling of the shape to the cropped image
            ImageBrush imageBrush = new ImageBrush(croppedBitmap);
            shapeToFill.Fill = imageBrush;

            return shapeToFill;
        }
    }
}
