using System.Runtime.Versioning;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using SnippingTool.Interop.Win32Api;

namespace SnippingTool.Interop;

public class ScreenCapture
{
    public static BitmapSource CaptureFullScreen(bool addToClipboard)
    {
        return OperatingSystem.IsWindowsVersionAtLeast(6)
            ? // API calls require windows 7.0 >
            CaptureFullScreenWindows_Version6(addToClipboard)
            : CaptureFullScreenWindows_Legacy(addToClipboard);
    }

    [SupportedOSPlatform("windows6.0")]
    public static BitmapSource CaptureFullScreenWindows_Version6(bool addToClipboard)
    {
        return CaptureRegion(
            User32.GetDesktopWindow(),
            (int)SystemParameters.VirtualScreenLeft,
            (int)SystemParameters.VirtualScreenTop,
            (int)SystemParameters.VirtualScreenWidth,
            (int)SystemParameters.VirtualScreenHeight,
            addToClipboard
        );
    }

    // ReSharper disable once UnusedParameter.Local
    private static BitmapSource CaptureFullScreenWindows_Legacy(bool addToClipboard)
    {
        // TODO: implement old version
        throw new NotImplementedException();
    }

    // capture a window. This doesn't do the alt-prtscrn version that loses the window shadow.
    // this version captures the shadow and optionally inserts a blank (usually white) area behind
    // it to keep the screen shot clean
    public static BitmapSource CaptureWindow(IntPtr hWnd, bool recolorBackground, Color substituteBackgroundColor,
        bool addToClipboard)
    {
        var rect = GetWindowActualRect(hWnd);

        Window? blankingWindow = null;

        if (recolorBackground)
        {
            blankingWindow = new Window
            {
                WindowStyle = WindowStyle.None,
                Title = string.Empty,
                ShowInTaskbar = false,
                AllowsTransparency = true,
                Background = new SolidColorBrush(substituteBackgroundColor)
            };

            blankingWindow.Show();

            const double fudge = 20;

            blankingWindow.Left = rect.X - fudge / 2;
            blankingWindow.Top = rect.Y - fudge / 2;
            blankingWindow.Width = rect.Width + fudge;
            blankingWindow.Height = rect.Height + fudge;
        }

        // bring the to-be-captured window to capture to the foreground
        // there's a race condition here where the blanking window
        // sometimes comes to the top. Hate those. There is surely
        // a non-WPF native solution to the blanking window which likely
        // involves drawing directly on the desktop or the target window

        User32.SetForegroundWindow(hWnd);

        var captured = CaptureRegion(
            hWnd,
            rect.X,
            rect.Y,
            rect.Width,
            rect.Height,
            true);

        if (blankingWindow != null)
            blankingWindow.Close();

        return captured;
    }

    // capture a region of the full screen
    public static BitmapSource CaptureRegion(int x, int y, int width, int height, bool addToClipboard)
    {
        return CaptureRegion(User32.GetDesktopWindow(), x, y, width, height, addToClipboard);
    }

    // capture a region of a the screen, defined by the hWnd
    public static BitmapSource CaptureRegion(
        IntPtr hWnd, int x, int y, int width, int height, bool addToClipboard)
    {
        var sourceDc = IntPtr.Zero;
        var compatibleBitmapHandle = IntPtr.Zero;
        BitmapSource bitmap;

        try
        {
            // gets the main desktop and all open windows
            sourceDc = User32.GetDC(User32.GetDesktopWindow());
            //sourceDC = User32.GetDC(hWnd);
            var targetDc = Gdi32.CreateCompatibleDC(sourceDc);

            // create a bitmap compatible with our target DC
            compatibleBitmapHandle = Gdi32.CreateCompatibleBitmap(sourceDc, width, height);

            // gets the bitmap into the target device context
            Gdi32.SelectObject(targetDc, compatibleBitmapHandle);

            // copy from source to destination
            Gdi32.BitBlt(targetDc, 0, 0, width, height, sourceDc, x, y, Gdi32.SRCCOPY);

            // Here's the WPF glue to make it all work. It converts from an 
            // hBitmap to a BitmapSource. Love the WPF interop functions
            bitmap = Imaging.CreateBitmapSourceFromHBitmap(
                compatibleBitmapHandle, IntPtr.Zero, Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());

            if (addToClipboard)
            {
                //Clipboard.SetImage(bitmap); // high memory usage for large images
                IDataObject data = new DataObject();
                data.SetData(DataFormats.Dib, bitmap, false);
                Clipboard.SetDataObject(data, false);
            }
        }
        catch (Exception ex)
        {
            throw new ScreenCaptureException(
                string.Format("Error capturing region {0},{1},{2},{3}", x, y, width, height), ex);
        }
        finally
        {
            Gdi32.DeleteObject(compatibleBitmapHandle);

            User32.ReleaseDC(IntPtr.Zero, sourceDc);
            User32.ReleaseDC(IntPtr.Zero, sourceDc);
        }

        return bitmap;
    }

    // this accounts for the border and shadow. Serious fudgery here.
    private static Int32Rect GetWindowActualRect(IntPtr hWnd)
    {
        User32.GetWindowRect(hWnd, out var windowRect);
        User32.GetClientRect(hWnd, out var clientRect);

        var sideBorder = (windowRect.Width - clientRect.Width) / 2 + 1;

        // sooo, yeah.
        const int hackToAccountForShadow = 4;

        var topLeftPoint = new Win32Point(windowRect.Left - sideBorder, windowRect.Top - sideBorder);

        //User32.ClientToScreen(hWnd, ref topLeftPoint);

        var actualRect = new Int32Rect(
            topLeftPoint.X,
            topLeftPoint.Y,
            windowRect.Width + sideBorder * 2 + hackToAccountForShadow,
            windowRect.Height + sideBorder * 2 + hackToAccountForShadow);

        return actualRect;
    }
}