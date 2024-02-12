using SnippingToolWPF.Drawing.Tools;
using SnippingToolWPF.Interop;
using SnippingToolWPF.Screenshot.Preview;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Color = System.Windows.Media.Color;

namespace SnippingToolWPF.Screenshot;

/// <summary>
/// Interaction logic for ScreenshotWindow.xaml
/// </summary>
public partial class ScreenshotWindow : Window
{
    private Rect UserFullScreenRect = MonitorInfo.GetTotalMonitorRect();
    private BitmapSource UserBackground => TakeScreenshots.FromWpfRect(UserFullScreenRect.X,
                                                              UserFullScreenRect.Y,
                                                              UserFullScreenRect.Width,
                                                              UserFullScreenRect.Height);

    // Change alpha (first) for amount of gray (transparancy) in filter color
    private BitmapSource UserBackgroundFiltered => ScreenshotFilters.AddFilter(UserBackground,
                                                                          Color.FromArgb(50, 255, 255, 255));
    private Canvas backgroundCanvas = new Canvas();
    private PreviewEllipse PreviewEllipse;
    private PreviewRectangle PreviewRectangle;

    public ScreenshotWindow()
    {
        InitializeComponent();
        SetWindowProperties();
        PreviewEllipse = new PreviewEllipse(UserBackground);
        PreviewRectangle = new PreviewRectangle(UserBackground);
    }

    public void SetWindowProperties()
    {
        this.Width = UserFullScreenRect.Width;
        this.Height = UserFullScreenRect.Height;
        this.Left = UserFullScreenRect.Left;
        this.Top = UserFullScreenRect.Top;
        this.Background = new ImageBrush(UserBackgroundFiltered);
        this.Content = backgroundCanvas;
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
        backgroundCanvas.Children.Clear();
        Point mousePosition = e.GetPosition(this);

        //Preview Rectangle

        if (IsCreatingScreenshot)
        {
            backgroundCanvas.Children.Add(this.PreviewRectangle.CreatePreviewRectangle(begin, mousePosition));
        }

        // TODO: on hover over dark pixel make stroke white, on hover over light pixel make stroke black
        PreviewEllipse.BaseEllipse.Stroke = Brushes.Black;
 
        Canvas FullPreviewEllipse = PreviewEllipse.FullPreviewEllipse;
        Canvas.SetLeft(FullPreviewEllipse, mousePosition.X + 30);
        Canvas.SetTop(FullPreviewEllipse, mousePosition.Y + 30);
     
        backgroundCanvas.Children.Add(FullPreviewEllipse);
    }

    private void OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
    {
        if (e.ChangedButton == MouseButton.Left)
            IsCreatingScreenshot = false;
    }

}
// https://learn.microsoft.com/en-us/dotnet/api/system.windows.media.imaging.bitmapsource.copypixels?view=windowsdesktop-8.0#system-windows-media-imaging-bitmapsource-copypixels(system-windows-int32rect-system-intptr-system-int32-system-int32)