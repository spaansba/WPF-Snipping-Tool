using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using SnippingToolWPF.Screenshot;
using SnippingToolWPF.SidePanel;
using SnippingToolWPF.SidePanel.EditSidePanel;
using SnippingToolWPF.SidePanel.PencilSidePanel;
using SnippingToolWPF.SidePanel.ShapesSidePanel;
using SnippingToolWPF.SidePanel.StickersSidePanel;
using SnippingToolWPF.SidePanel.TextSidePanel;
using SnippingToolWPF.WPFExtensions;

namespace SnippingToolWPF;

public partial class DrawingViewModel : ObservableObject
{
    private readonly EditSidePanelViewModel editPanel;

    private readonly PencilsSidePanelViewModel pencilsPanel;
    private readonly ShapesSidePanelViewModel shapesPanel;
    private readonly StickersSidePanelViewModel stickersPanel;
    private readonly TextSidePanelViewModel textPanel;

    [ObservableProperty] [NotifyPropertyChangedFor(nameof(SidePanelContent))]
    private SidePanelContentKind sidePanelContentKind = SidePanelContentKind.Shapes; //Change for startup sidepanel

    public DrawingViewModel()
    {
        pencilsPanel = new PencilsSidePanelViewModel(this);
        shapesPanel = new ShapesSidePanelViewModel(this);
        stickersPanel = new StickersSidePanelViewModel(this);
        textPanel = new TextSidePanelViewModel(this);
        editPanel = new EditSidePanelViewModel(this);
        ClearCanvas = new RelayCommand(ExecuteClearCanvasButton);
        TakeScreenshot = new RelayCommand(ExecuteTakeScreenshot);
        
        // Top bar Relay Commands
        Application.Current.MainWindow!.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight; // Make full screen not overlap task bar
        MoveWindowCommand = new RelayCommand(_ => { Application.Current.MainWindow.DragMove(); });
        ShutDownWindowCommand = new RelayCommand(_ => { Application.Current.Shutdown(); });
        // ReSharper disable once BitwiseOperatorOnEnumWithoutFlags
        MaximizeWindowCommand = new RelayCommand(_ =>
        {
            Application.Current.MainWindow.WindowState ^= WindowState.Maximized;
        });
        MinimizeWindowCommand = new RelayCommand(_ =>
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        });
    }

    //List of the side panel enums
    public IReadOnlyList<SidePanelContentKind> AllSidePanelContentKinds { get; } =
        Enum.GetValues<SidePanelContentKind>();

    public ICommand ClearCanvas { get; private set; }
    public ICommand TakeScreenshot { get; private set; }
    public ICommand MoveWindowCommand { get; private set; }
    public ICommand ShutDownWindowCommand { get; private set; }
    public ICommand MaximizeWindowCommand { get; private set; }
    public ICommand MinimizeWindowCommand { get; private set; }

    public SidePanelViewModel? SidePanelContent => SidePanelContentKind switch
    {
        SidePanelContentKind.Pencils => pencilsPanel,
        SidePanelContentKind.Shapes => shapesPanel,
        SidePanelContentKind.Stickers => stickersPanel,
        SidePanelContentKind.Text => textPanel,
        SidePanelContentKind.Edit => editPanel,
        _ => null
    };

    #region Drawing Objects creating / clearing

    public ObservableCollection<DrawingShape> DrawingObjects { get; } = new();
    
    /// <summary>
    ///     Button in Xaml is linked via the RelayCommand class so the button can be in the viewmodel instead of the
    ///     code-behind
    /// </summary>
    private void ExecuteClearCanvasButton(object? parameter)
    {
        //TODO: Create custom messagebox so we center it in the middle of the app isntead of screen 

        if (Application.Current.MainWindow == null) return;

        var result = MessageBox.Show(Application.Current.MainWindow,
            "Are you sure you want to clear the canvas?", "Clear Canvas", MessageBoxButton.YesNo,
            MessageBoxImage.Question);

        if (result == MessageBoxResult.Yes)
            ClearDrawingObjects();
    }

    private void ClearDrawingObjects()
    {
        DrawingObjects.Clear();
    }

    #endregion

    #region Take Screenshot

    private ImageSource? screenshot;

    public ImageSource? Screenshot
    {
        get => screenshot;
        private set => SetProperty(ref screenshot, value);
    }

    private void ExecuteTakeScreenshot(object? parameter)
    {
        var result = ScreenshotWindow.GetScreenshot();
        if (result is not null) Screenshot = result;
    }

    #endregion

    #region Shape Selection

    private object? selectedShape;
    
    public object? SelectedShape
    {
        get => selectedShape;

        // Define which code should activate ones a Shape is Selected
        set
        {
            SetProperty(ref selectedShape, value);
            Debug.WriteLine("object selected - DrawingViewModel");
        }
    }
    #endregion
}