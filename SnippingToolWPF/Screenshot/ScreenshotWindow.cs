using SnippingToolWPF.ExtensionMethods;
using SnippingToolWPF.Interop;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Color = System.Windows.Media.Color;

namespace SnippingToolWPF.Screenshot;

/// <summary>
/// Creates a new window that allows users to create screenshots
/// </summary>
public partial class ScreenshotWindow : Window
{
    private Point begin;
    private bool isCreatingScreenshot;
    private Rect userFullScreenRect = MonitorInfo.GetTotalMonitorRect();
    private readonly BitmapSource userBackground;
    private const double PreviewEllipseSize = 126;
    private RectangleGeometry selectionGeometry = new RectangleGeometry();
    private Path semiTransparency;

    private PreviewEllipse PreviewEllipse = new PreviewEllipse()
    {
        Width = PreviewEllipseSize,
        Height = PreviewEllipseSize,
    };

    public ScreenshotWindow()
    {
        (this.Left, this.Top, this.Width, this.Height) = userFullScreenRect; // use deconstruct extension to set values all at ones
        userBackground = TakeScreenshots.FromWpfRect(userFullScreenRect);

        this.Background = new ImageBrush(this.userBackground);
        this.semiTransparency = CreateSemiTransparency(selectionGeometry, userFullScreenRect);
        this.Content = new Canvas().AddChildren(this.semiTransparency, this.PreviewEllipse);

        this.ResizeMode = ResizeMode.NoResize;
        // this.WindowStyle = WindowStyle.None;
    }

    private static Path CreateSemiTransparency(RectangleGeometry selectionGeometry, Rect userFullScreenRect)
    {
        return new Path()
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
        //backgroundCanvas.Children.Clear();
        Point mousePosition = e.GetPosition(this);

        //Preview Rectangle

        if (isCreatingScreenshot)
        {
            //backgroundCanvas.Children.Add(this.PreviewRectangle.CreatePreviewRectangle(begin, mousePosition));
            this.selectionGeometry.Rect = new Rect(begin, mousePosition);
        }

        // Preview ellipse

        // TODO: on hover over dark pixel make stroke white, on hover over light pixel make stroke black
        //PreviewEllipse.BaseEllipse.Stroke = Brushes.Black;
 
        //Canvas FullPreviewEllipse = PreviewEllipse.FullPreviewEllipse;
        //Canvas.SetLeft(FullPreviewEllipse, mousePosition.X + 30);
        //Canvas.SetTop(FullPreviewEllipse, mousePosition.Y + 30);

        
        
        //backgroundCanvas.Children.Add(FullPreviewEllipse);
        base.OnMouseMove(e);
    }

    protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
    {
        if (e.ChangedButton == MouseButton.Left)
            isCreatingScreenshot = false;
        base.OnMouseLeftButtonUp(e);
    }
    #endregion
}
// https://learn.microsoft.com/en-us/dotnet/api/system.windows.media.imaging.bitmapsource.copypixels?view=windowsdesktop-8.0#system-windows-media-imaging-bitmapsource-copypixels(system-windows-int32rect-system-intptr-system-int32-system-int32)