using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using SnippingToolWPF.Control;
using SnippingToolWPF.ExtensionMethods;
using SnippingToolWPF.Tools;
using SnippingToolWPF.Tools.ToolAction;
using SnippingToolWPF.WPFExtensions;

namespace SnippingToolWPF;

[TemplatePart(Name = PartCurrentCanvas, Type = typeof(Canvas))]
[TemplatePart(Name = PartItemsControl, Type = typeof(DrawingCanvasListBox))]
[ContentProperty(nameof(Shapes))]
public class DrawingCanvas : System.Windows.Controls.Control
{
    private const string PartCurrentCanvas = "PART_CurrentCanvas";
    private const string PartItemsControl = "PART_ItemsControl";
    private readonly CompositeCollection allItems; // Collection of the screenshot + shapes

    private readonly UndoRedo undoRedoStacks = new UndoRedo(); // To make Undo Redo work 

    static DrawingCanvas()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(DrawingCanvas),
            new FrameworkPropertyMetadata(typeof(DrawingCanvas)));
    }

    public DrawingCanvas()
    {
        Loaded += OnLoaded;
        Shapes =
            new ObservableCollection<DrawingShape>(); // set it because the property getter is not used in all circumstances
        allItems = CreateAllItemCollection();
    }

    #region Keyboard Handlers

    //Retaining aspect ratio (holding shift etc) is done in the Tool

    protected override void OnPreviewKeyDown(KeyEventArgs e)
    {
        base.OnPreviewKeyDown(e);

        if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.Z) // Undo last action
            // If undo is possible do the action
            if (undoRedoStacks.TryUndo(out var action))
                Perform(action.Reverse());
        // https://en.wikipedia.org/wiki/Memento_pattern
        if (Keyboard.Modifiers != ModifierKeys.Control || e.Key != Key.Y) return; // Redo the Undo
        {
            // If redo is possible to the action
            if (undoRedoStacks.TryRedo(out var action)) Perform(action);
        }
    }

    #endregion

    #region On Item Mouse Events

    /// <summary>
    ///     Gets Mouse Events from the DrawingCanvasListBoxItem so that the DrawingCanvas can handle them
    /// </summary>
    /// <param name="drawingCanvasPoint">Point Relative to the Drawing Canvas not Relative to the DrawingCanvasListBoxItem</param>
    /// <param name="item"></param>
    internal void OnItemMouseEvent(DrawingCanvasListBoxItem item, MouseEventArgs e, Point drawingCanvasPoint)
    {
        if (item.Content is not DrawingShape element)
            return;

        if (e.RoutedEvent == MouseLeftButtonDownEvent)
            Perform(Tool?.LeftButtonDown(drawingCanvasPoint, element));

        if (e.RoutedEvent == MouseMoveEvent && e.LeftButton == MouseButtonState.Pressed)
            Perform(Tool?.MouseMove(drawingCanvasPoint, element));
    }

    #endregion

    #region set-up / tool change etc

    /// <summary>
    ///     List of all items in the Composite, including screenshot and all shapes drawn
    /// </summary>
    private CompositeCollection CreateAllItemCollection()
    {
        return new CompositeCollection
        {
            new SingleItemCollectionContainer
            {
                Item = new Image()
                    .WithBinding(
                        Image.SourceProperty,
                        new PropertyPath(ScreenshotProperty),
                        this
                    )
            },
            new CollectionContainer().WithBinding(CollectionContainer.CollectionProperty,
                new PropertyPath(ShapesProperty), this)
        };
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        Focus(); // To allow keydown
        OnToolChanged(Tool);
    }

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();
        CurrentCanvas = GetTemplateChild(PartCurrentCanvas) as Canvas;
        ItemsControl = GetTemplateChild(PartItemsControl) as DrawingCanvasListBox;
    }

    private DrawingCanvasListBox? itemsControl;

    private DrawingCanvasListBox? ItemsControl
    {
        set
        {
            if (itemsControl == value)
                return;
            if (itemsControl is not null)
            {
                itemsControl.DrawingCanvas = null;
                itemsControl.ItemsSource = null;
            }

            itemsControl = value;

            if (itemsControl is not null)
            {
                itemsControl.DrawingCanvas = this;
                itemsControl.ItemsSource = allItems;
            }
        }
    }

    // Basically on load
    private Canvas? currentCanvas;

    private Canvas? CurrentCanvas
    {
        get => currentCanvas;
        set
        {
            if (currentCanvas == value)
                return;

            if (currentCanvas is not null) //Reset if previously set before somehow, pure for safety
                currentCanvas.Children.Clear();

            currentCanvas = value;

            if (currentCanvas is not null)
                OnToolChanged(Tool);
        }
    }

    public static readonly DependencyProperty ToolProperty = DependencyProperty.Register(
        nameof(Tool),
        typeof(IDrawingTool),
        typeof(DrawingCanvas),
        new FrameworkPropertyMetadata(
            default,
            OnToolChanged)); //Notify when a tool is changed

    public IDrawingTool? Tool
    {
        get => this.GetValue<IDrawingTool?>(ToolProperty);
        set => this.SetValue<IDrawingTool?>(ToolProperty, value);
    }

    private static void OnToolChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        (d as DrawingCanvas)?.OnToolChanged(e.NewValue as IDrawingTool);
    }

    private void OnToolChanged(IDrawingTool? newValue)
    {
        Keyboard.Focus(this); // We lose keyboardfocus on tool change

        if (CurrentCanvas is null)
            return;

        CurrentCanvas.Children.Clear();
        var visual = newValue?.DrawingShape;

        if (visual is not null)
            CurrentCanvas.Children.Add(visual); // Not drawing canvas but the top canvas 
    }

    #endregion

    #region Drawing on the canvas

    private bool isDrawing;

    protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
    {
        base.OnMouseLeftButtonDown(e);
        Perform(Tool?.LeftButtonDown(e.GetPosition(this), null));
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
        base.OnMouseMove(e);

        if (isDrawing)
            Perform(Tool?.MouseMove(e.GetPosition(this), null));
    }

    protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
    {
        base.OnMouseLeftButtonUp(e);
        if (isDrawing)
        {
            Perform(Tool?.MouseMove(e.GetPosition(this), null));
            Perform(Tool?.LeftButtonUp());
        }
    }

    protected override void OnMouseRightButtonDown(MouseButtonEventArgs e)
    {
        base.OnMouseLeftButtonUp(e);

        if (!isDrawing) return;

        Tool?.RightButtonDown();
        isDrawing = false;
    }


    private void Perform(DrawingToolAction? action)
    {
        if (action.HasValue)
            Perform(action.Value);

        // Only add the Undoable flags from DrawingTool to the undoredostack
        if (action?.OnlyPerformUndoable() is { IncludeInUndoStack: true } undoableAction)
            undoRedoStacks.AddAction(undoableAction);
    }

    private void Perform(DrawingToolAction action)
    {
        PerformStopAction(action.StopAction);
        PerformStartAction(action.StartAction);
    }

    private void PerformStopAction(DrawingToolActionItem action)
    {
        if (action.IsMouseCapture)
        {
            ReleaseMouseCapture();
            isDrawing = false;
        }

        if (action.IsKeyboardFocus) Keyboard.Focus(this);
        if (action.IsShape) // For eraser tool
            Shapes.Remove(action.Item);
    }

    private void PerformStartAction(DrawingToolActionItem action)
    {
        if (action.IsMouseCapture)
        {
            CaptureMouse();
            isDrawing = true;
        }

        if (action.IsKeyboardFocus)
        {
            //TODO: Fix keyboard focus
            //     Keyboard.Focus(this.Tool?.Visual);
        }

        if (action.IsShape)
        {
            Shapes.Add(action.Item);
        }

    }
    
    #endregion

    #region Shapes

    public static readonly DependencyProperty ShapesProperty = DependencyProperty.Register(
        nameof(Shapes),
        typeof(ObservableCollection<DrawingShape>),
        typeof(DrawingCanvas),
        new FrameworkPropertyMetadata());

    public ObservableCollection<DrawingShape> Shapes
    {
        get => this.GetValue<ObservableCollection<DrawingShape>>(ShapesProperty)
               ?? this.SetValue<ObservableCollection<DrawingShape>>(ShapesProperty,
                   new ObservableCollection<DrawingShape>()); // < dont return a null value
        set => this.SetValue<ObservableCollection<DrawingShape>>(ShapesProperty, value);
    }

    #endregion

    #region Screenshot Property

    public static readonly DependencyProperty ScreenshotProperty = DependencyProperty.Register(
        nameof(Screenshot),
        typeof(ImageSource),
        typeof(DrawingCanvas));

    public ImageSource? Screenshot
    {
        get => this.GetValue<ImageSource>(ScreenshotProperty);
        set => SetValue(ScreenshotProperty, value);
    }

    #endregion

    #region Shape Selection

    public static readonly DependencyProperty SelectedItemProperty =
        Selector.SelectedItemProperty.AddOwner(typeof(DrawingCanvas));

    public object? SelectedItem
    {
        get => GetValue(SelectedItemProperty);
        set => SetValue(SelectedItemProperty, value);
    }

    private ResizeAdorner? ResizeAdorner { get; set; }

    protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
    {
        base.OnPropertyChanged(e);
        if (e.Property == SelectedItemProperty)
        {
            if (e.OldValue is UIElement oldValue)
            {
                if (ResizeAdorner != null) AdornerLayer.GetAdornerLayer(oldValue)?.Remove(ResizeAdorner);
            }
            
            if (e.NewValue is UIElement newValue)
            {
                ResizeAdorner = new ResizeAdorner(newValue);
                AdornerLayer.GetAdornerLayer(newValue)?.Add(ResizeAdorner);
            }
        }
    }

    #endregion
}