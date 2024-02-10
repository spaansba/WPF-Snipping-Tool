using System.Drawing;
using System.Windows.Media.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Interop;
using System.Drawing.Imaging;


/// We create this project due to needing libraries from winforms
namespace SnippingToolWPF.Interop;

public static class TakeScreenshots
{
    #region Capture Screen "overloads" 
    public static BitmapSource FromTwoPoints(WinFormsPoint topLeft, WinFormsPoint bottomRight)
    {
        int width = bottomRight.X - topLeft.X;
        int height = topLeft.Y - bottomRight.Y;
        return CaptureScreen(topLeft.X, topLeft.Y, width, height);
    }

    public static BitmapSource FromWinformsRect(int x, int y, int width, int height)
    {
        return CaptureScreen(x, y, width, height);
    }

    public static BitmapSource FromWpfRect(Double x, Double y, Double width, Double height)
    {
        return CaptureScreen(Convert.ToInt32(x), Convert.ToInt32(y), Convert.ToInt32(width), Convert.ToInt32(height));
    }

    public static BitmapSource FromPointAndSize(WinFormsPoint topLeftPoint, WinFormsSize screenshotSize)
    {
        return CaptureScreen(topLeftPoint.X, topLeftPoint.Y, screenshotSize.Width, screenshotSize.Height);
    }

    private static BitmapSource CaptureScreen(int x, int y, int width, int height)
    {
        using Bitmap image = new Bitmap(width, height,PixelFormat.Format32bppArgb);
        using Graphics g = Graphics.FromImage(image);

        g.CopyFromScreen(x, y, x, y,
                 new WinFormsSize(width, height),
                 CopyPixelOperation.SourceCopy);

        return BitmapToBitmapSource(image); 
    }

    private static void SafeScreenshot(Bitmap screenshot)
    {
        string filePath = "C:\\Users\\barts\\OneDrive\\Bureaublad\\Resources\\Test.png"; // TODO: Temp safe location
        screenshot.Save(filePath, ImageFormat.Png);
    }

    #endregion

    #region Convert Bitmap to BitmapSource wpf

    private static BitmapSource BitmapToBitmapSource(Bitmap bmp)
    {
        var hBitmap = bmp.GetHbitmap();
        var imageSource = Imaging.CreateBitmapSourceFromHBitmap(hBitmap, IntPtr.Zero,
            System.Windows.Int32Rect.Empty,
            System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions());
        NativeMethods.DeleteObject(hBitmap);
        return imageSource;
    }

    /// <summary>
    /// Alternitive method to convert Bitmap to BitmapSource wpf
    /// </summary>
    /// 
    //public static BitmapSource Convert(System.Drawing.Bitmap bitmap)
    //{
    //    var bitmapData = bitmap.LockBits(
    //        new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height),
    //        System.Drawing.Imaging.ImageLockMode.ReadOnly, bitmap.PixelFormat);

    //    var bitmapSource = BitmapSource.Create(
    //        bitmapData.Width, bitmapData.Height,
    //        bitmap.HorizontalResolution, bitmap.VerticalResolution,
    //        PixelFormats.Bgr24, null,
    //        bitmapData.Scan0, bitmapData.Stride * bitmapData.Height, bitmapData.Stride);

    //    bitmap.UnlockBits(bitmapData);

    //    return bitmapSource;
    //}

}
file static class NativeMethods
{
    [DllImport("gdi32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static extern bool DeleteObject(IntPtr hObject);
}

#endregion
