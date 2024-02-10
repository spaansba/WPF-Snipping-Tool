using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SnippingToolWPF.Screenshot
{
    public static class ScreenshotFilters
    {
        /// <summary>
        /// Adds filter to a screenshot
        /// </summary>
        /// <param name="bitmapSource"> has to be a BitmapSource not regular Bitmap</param>
        /// <param name="filterToAdd">Color is System.Windows.Media</param>
        /// <returns></returns>
        public static BitmapSource AddFilter(BitmapSource bitmapSource, Color filterToAdd)
        {
            DrawingVisual visual = new DrawingVisual();
            using (DrawingContext context = visual.RenderOpen())

            {
                context.DrawImage(bitmapSource, new Rect(0, 0, bitmapSource.Width, bitmapSource.Height));

                // Draw rectangle over visual with semi transparant
                context.DrawRectangle(new SolidColorBrush(
                    filterToAdd), 
                    null,
                    new Rect(0, 0, bitmapSource.Width, bitmapSource.Height));
            }

            // Render the DrawingVisual to a RenderTargetBitmap
            RenderTargetBitmap filteredScreenshot = new RenderTargetBitmap(
                (int)bitmapSource.Width, (int)bitmapSource.Height, 96, 96, PixelFormats.Default);
            filteredScreenshot.Render(visual);

            return filteredScreenshot;
        }
    }
}
