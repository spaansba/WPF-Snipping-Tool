using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows;
using SnippingToolWPF.Interop;
using System.Windows.Input;
using SnippingToolWPF.Screenshot;
using System.Windows.Media;

namespace SnippingToolWPF;

public partial class DrawingViewModel : ObservableObject
{
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(SidePanelContent))]
    private SidePanelContentKind sidePanelContentKind = SidePanelContentKind.Shapes; //Change for startup sidepanel

    //List of the side panel enums
    public IReadOnlyList<SidePanelContentKind> AllSidePanelContentKinds { get; } = Enum.GetValues<SidePanelContentKind>();

    private readonly PencilsSidePanelViewModel pencilsPanel;
    private readonly ShapesSidePanelViewModel shapesPanel;
    private readonly StickersSidePanelViewModel stickersPanel;
    private readonly TextSidePanelViewModel textPanel;
    public ICommand ClearCanvas { get; private set; }
    public ICommand TakeScreenshot { get; private set; }

    public DrawingViewModel()
    {
        this.pencilsPanel = new PencilsSidePanelViewModel(this);
        this.shapesPanel = new ShapesSidePanelViewModel(this);
        this.stickersPanel = new StickersSidePanelViewModel(this);
        this.textPanel = new TextSidePanelViewModel(this);
        this.ClearCanvas = new RelayCommand(ExecuteClearCanvasButton);
        this.TakeScreenshot = new RelayCommand(ExecuteTakeScreenshot);
    }

    public SidePanelViewModel? SidePanelContent => SidePanelContentKind switch
    {
        SidePanelContentKind.Pencils => pencilsPanel,
        SidePanelContentKind.Shapes => shapesPanel,
        SidePanelContentKind.Stickers => stickersPanel,
        SidePanelContentKind.Text => textPanel,
        _ => null,
    };

    #region Drawing Objects creating / clearing

    public ObservableCollection<UIElement> DrawingObjects { get; private set; } = new ObservableCollection<UIElement>();

    /// <summary>
    /// Button in Xaml is linked via the RelayCommand class so the button can be in the viewmodel instead of the code-behind
    /// </summary>
    private void ExecuteClearCanvasButton(object? parameter)
    {
        //TODO: Create custom messagebox so we center it in the middle of the app isntead of screen 

        MessageBoxResult result = MessageBox.Show(Application.Current.MainWindow,
            "Are you sure you want to clear the canvas?", "Clear Canvas", MessageBoxButton.YesNo, MessageBoxImage.Question);

        if (result == MessageBoxResult.Yes)
            this.ClearDrawingObjects();
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
        get => this.screenshot;
        private set => this.SetProperty(ref this.screenshot, value);
    }

    private void ExecuteTakeScreenshot(object? parameter)
    {
        var result = ScreenshotWindow.GetScreenshot();
        if ( result is not null)
        {
            this.Screenshot = result;
        }
    }

    

    #endregion
}
