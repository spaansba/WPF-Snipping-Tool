using SnippingToolWPF.Drawing.Tools;
using SnippingToolWPF.Interop;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
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
    public ScreenshotWindow()
    {
        InitializeComponent();
        SetWindowProperties();
    }

    public void SetWindowProperties()
    {
        this.Width = UserFullScreenRect.Width;
        this.Height = UserFullScreenRect.Height;
        this.Left = UserFullScreenRect.Left;
        this.Top = UserFullScreenRect.Top;
        this.Background = new ImageBrush(UserBackgroundFiltered);
        this.Tool = 
    }

    public IDrawingTool? Tool; 

    private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        Console.WriteLine();
    }
}
