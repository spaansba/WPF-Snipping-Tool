using System.Windows;
using System.Windows.Documents;

namespace WPF_Shapes;

/// <summary>
///     Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow
{
    public MainWindow()
    {
        InitializeComponent();

        Loaded += MainWindow_Loaded;
    }

    private void MainWindow_Loaded(object sender, RoutedEventArgs e)
    {
        AdornerLayer.GetAdornerLayer(MyGrid).Add(new ResizeAdorner2(MyButton));
    }
    
}