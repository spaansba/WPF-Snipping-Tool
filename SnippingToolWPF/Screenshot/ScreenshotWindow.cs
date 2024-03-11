using SnippingToolWPF.ExtensionMethods;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using SnippingTool.Interop;
using Color = System.Windows.Media.Color;

namespace SnippingToolWPF.Screenshot;

/// <summary>
/// Creates a new window that allows users to create screenshots
/// </summary>
public class ScreenshotWindow : Window
{
    
    private Point begin;
    private Int32Rect selectedRect;
    private bool isCreatingScreenshot;
    private readonly Rect userFullScreenRect = new(
                  SystemParameters.VirtualScreenLeft,
                  SystemParameters.VirtualScreenTop,
                  SystemParameters.VirtualScreenWidth,
                  SystemParameters.VirtualScreenHeight);
    private readonly BitmapSource? userBackground;
    private const double PreviewEllipseSize = 126;
    private readonly RectangleGeometry selectionGeometry = new();

    private readonly PreviewEllipse previewEllipse = new()
    {
        Width = PreviewEllipseSize,
        Height = PreviewEllipseSize,
    };

    private ScreenshotWindow()
    {

        (this.Left, this.Top, this.Width, this.Height) = userFullScreenRect; // use deconstruct extension to set values all at ones

        userBackground = ScreenCapture.CaptureFullScreen(addToClipboard: false);

        this.Background = new ImageBrush(this.userBackground);
        var semiTransparency = CreateSemiTransparency(selectionGeometry, userFullScreenRect);
        this.Content = new Canvas().AddChildren(semiTransparency, this.previewEllipse);

        this.ResizeMode = ResizeMode.NoResize;
        // this.WindowStyle = WindowStyle.None;
    }

    private static Path CreateSemiTransparency(RectangleGeometry selectionGeometry, Rect userFullScreenRect)
    {
        return new()
        {
            Fill = new SolidColorBrush()
            {
                Color = Color.FromArgb(80, 128, 128, 128),
            },
            Data = new CombinedGeometry()
            {
                GeometryCombineMode = GeometryCombineMode.Xor,
                Geometry1 = selectionGeometry,
                Geometry2 = new RectangleGeometry
                {
                    Rect = userFullScreenRect
                }
            }
        };
    }

    #region Mouse events
    protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
    {
        if (e.ChangedButton == MouseButton.Left)
        {
            isCreatingScreenshot = true;
            begin = e.GetPosition(this);
        }
        base.OnMouseLeftButtonDown(e);
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
        var mousePosition = e.GetPosition(this);

        if (isCreatingScreenshot)
        {
            this.selectionGeometry.Rect = new(begin, mousePosition);
        }

        Canvas.SetLeft(previewEllipse, mousePosition.X + 30);
        Canvas.SetTop(previewEllipse, mousePosition.Y + 30);

        base.OnMouseMove(e);
    }

    protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
    {
        if (e.ChangedButton == MouseButton.Left)
        {
            var end = e.GetPosition(this);
            this.selectedRect = new((int)begin.X, (int)begin.Y, (int)(end.X - begin.X), (int)(end.Y - begin.Y));
            this.DialogResult = true;
            this.Close();
            isCreatingScreenshot = false; // not nececairy but usefull for testing
            
        }
  
        base.OnMouseLeftButtonUp(e);
    }
    #endregion

    public static ImageSource? GetScreenshot()
    {
        var window = new ScreenshotWindow()
        {
            Topmost = true,
        };
        if (window.ShowDialog() is not true || window.userBackground is null)
            return null;
        
        return new CroppedBitmap(window.userBackground, window.selectedRect);
    }

}
// https://learn.microsoft.com/en-us/dotnet/api/system.windows.media.imaging.bitmapsource.copypixels?view=windowsdesktop-8.0#system-windows-media-imaging-bitmapsource-copypixels(system-windows-int32rect-system-intptr-system-int32-system-int32)