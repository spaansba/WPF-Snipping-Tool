using SnippingToolWPF.Interop;
using SnippingToolWPF.Screenshot.Preview;
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
    private Rect UserFullScreenRect = MonitorInfo.GetTotalMonitorRect();
    private readonly BitmapSource userBackground;

    private PreviewEllipse PreviewEllipse;
    private PreviewRectangle PreviewRectangle;

    public ScreenshotWindow()
    {
        userBackground = TakeScreenshots.FromWpfRect(UserFullScreenRect.X,
                                                              UserFullScreenRect.Y,
                                                              UserFullScreenRect.Width,
                                                              UserFullScreenRect.Height);

        SetWindowProperties();
        PreviewEllipse = new PreviewEllipse(userBackground);
        PreviewRectangle = new PreviewRectangle(userBackground);
    }

    private RectangleGeometry selectionGeometry = new RectangleGeometry();
    private Path? semiTransparency;

    public void SetWindowProperties()
    {
        this.Width = UserFullScreenRect.Width;
        this.Height = UserFullScreenRect.Height;
        this.Left = UserFullScreenRect.Left;
        this.Top = UserFullScreenRect.Top;
        this.MouseDown += OnMouseLeftButtonDown;
        this.MouseMove += OnMouseMove;
        this.MouseUp += OnMouseLeftButtonUp;

        this.Background = new ImageBrush(this.userBackground);
        this.semiTransparency = new Path()
        {
            Fill = new SolidColorBrush()
            {
                Color = Color.FromArgb(80, 128, 128, 128),
            },
            Data = new CombinedGeometry()
            {
                GeometryCombineMode = GeometryCombineMode.Xor,
                Geometry1 = this.selectionGeometry,
                Geometry2 = new RectangleGeometry
                {
                    Rect = this.UserFullScreenRect
                }
            }
        };
        this.Content = this.semiTransparency;
        this.ResizeMode = ResizeMode.NoResize;
    }

    private Point begin;
    private bool IsCreatingScreenshot;

    private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if (e.ChangedButton == MouseButton.Left)
        {
            IsCreatingScreenshot = true;
            begin = e.GetPosition(this);
        }
    }

    private void OnMouseMove(object sender, MouseEventArgs e)
    {
        //backgroundCanvas.Children.Clear();
        Point mousePosition = e.GetPosition(this);

        //Preview Rectangle

        if (IsCreatingScreenshot)
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
    }

    private void OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
    {
        if (e.ChangedButton == MouseButton.Left)
            IsCreatingScreenshot = false;
    }

}
// https://learn.microsoft.com/en-us/dotnet/api/system.windows.media.imaging.bitmapsource.copypixels?view=windowsdesktop-8.0#system-windows-media-imaging-bitmapsource-copypixels(system-windows-int32rect-system-intptr-system-int32-system-int32)