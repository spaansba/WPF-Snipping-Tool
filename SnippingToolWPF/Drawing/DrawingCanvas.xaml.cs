using System.Collections;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using SnippingToolWPF.Control;
using SnippingToolWPF.Drawing.Tools;

namespace SnippingToolWPF;

[TemplatePart(Name = PartCurrentCanvas, Type = typeof(Canvas))]
[TemplatePart(Name = PartItemsControl, Type = typeof(DrawingCanvasListBox))]
[ContentProperty(nameof(Shapes))]
public class DrawingCanvas : System.Windows.Controls.Control
{

    private const string PartCurrentCanvas = "PART_CurrentCanvas";
    private const string PartItemsControl = "PART_ItemsControl";
    private CompositeCollection allItems; // Collection of the screenshot + shapes

    public UndoRedo UndoRedoStacks = new UndoRedo(); // To make Undo Redo work 

    static DrawingCanvas()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(DrawingCanvas),
            new FrameworkPropertyMetadata(typeof(DrawingCanvas)));
    }

    public DrawingCanvas()
    {
        this.Loaded += OnLoaded;
        this.Shapes = new ObservableCollection<UIElement>(); // set it because the property getter is not used in all circumstances
        this.allItems = CreateAllItemCollection(); 
    }

    #region set-up / tool change etc

    /// <summary>
    /// List of all items in the Composite, including screenshot and all shapes drawn
    /// </summary>
    private CompositeCollection CreateAllItemCollection()
    {
        return new CompositeCollection()
        {
            new SingleItemCollectionContainer
                {
                    Item = new Image()
                        .WithBinding(
                            Image.SourceProperty,
                            new(ScreenshotProperty),
                            this
                        ),
                },
            new CollectionContainer().WithBinding(CollectionContainer.CollectionProperty,new(ShapesProperty),this)
        };
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        this.Focus(); // To allow keydown
        OnToolChanged(this.Tool);
    }

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();
        this.CurrentCanvas = this.GetTemplateChild(PartCurrentCanvas) as Canvas;
        this.ItemsControl = this.GetTemplateChild(PartItemsControl) as DrawingCanvasListBox;
    }

    private DrawingCanvasListBox? itemsControl;
    private DrawingCanvasListBox? ItemsControl
    {
        get => this.itemsControl;
        set
        {
            if (this.itemsControl == value)
                return;
            if (this.itemsControl is not null)
            {
                this.itemsControl.DrawingCanvas = null;
                this.itemsControl.ItemsSource = null;
            }
                
            this.itemsControl = value;

            if (this.itemsControl is not null)
            {
                this.itemsControl.DrawingCanvas = this;
                this.itemsControl.ItemsSource = this.allItems;
            }
                
        }
    }

    // Basically on load
    private Canvas? currentCanvas;
    private Canvas? CurrentCanvas
    {
        get => this.currentCanvas;
        set
        {
            if (this.currentCanvas == value) 
                return;

            if (this.currentCanvas is not null) //Reset if previously set before somehow, pure for safety
                this.currentCanvas.Children.Clear();

            this.currentCanvas = value;

            if (this.currentCanvas is not null)
                OnToolChanged(this.Tool);
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

        if (this.CurrentCanvas is null)
            return;

        this.CurrentCanvas.Children.Clear();
        UIElement? visual = newValue?.Visual;

        if (visual is not null)
            this.CurrentCanvas.Children.Add(visual); // Not drawing canvas but the top canvas 
    }

    #endregion

    #region Drawing on the canvas
    private bool isDrawing;
    protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
    {
        base.OnMouseLeftButtonDown(e);
        Perform(this.Tool?.LeftButtonDown(e.GetPosition(this),null));
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
        base.OnMouseMove(e);

        if (this.isDrawing)
            Perform(this.Tool?.MouseMove(e.GetPosition(this),null));
    }

    protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
    {
        base.OnMouseLeftButtonUp(e);
       if (this.isDrawing)
        {
            Perform(this.Tool?.MouseMove(e.GetPosition(this), null));
            Perform(this.Tool?.LeftButtonUp());
        }
    }

    private void Perform(DrawingToolAction? action)
    {
        if (action.HasValue)
            Perform(action.Value);

        // Only add the Undoable flags from DrawingTool to the undoredostack
        if (action?.OnlyPerformUndoable() is { IncludeInUndoStack: true } undoableAction)
        {
            this.UndoRedoStacks.AddAction(undoableAction);
        }
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
            this.ReleaseMouseCapture();
            isDrawing = false;
        }
        if (action.IsKeyboardFocus)
        {
            Keyboard.Focus(this);
        }
        if (action.IsShape) // For eraser tool
        {
            this.Shapes.Remove(action.Item);
        }
    }

    private void PerformStartAction(DrawingToolActionItem action)
    {
        if (action.IsMouseCapture)
        {
            this.CaptureMouse();
            isDrawing = true;
        }
        if (action.IsKeyboardFocus)
        {
            Keyboard.Focus(this.Tool?.Visual);
        }
        if (action.IsShape)
        {
            this.Shapes.Add(action.Item);
        }
    }

    #endregion

    #region Keyboard Handlers

    //Retaining aspect ratio (holding shift etc) is done in the Tool

    protected override void OnPreviewKeyDown(KeyEventArgs e)
    {
        base.OnPreviewKeyDown(e);

        if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.Z) // Undo last action
        {
            // If undo is possible do the action
            if (this.UndoRedoStacks.TryUndo(out var action))
            { 
                Perform(action.Reverse());
            }
            // https://en.wikipedia.org/wiki/Memento_pattern
        }

        if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.Y) // Redo the Undo
        {
            // If redo is possible to the action
            if (this.UndoRedoStacks.TryRedo(out var action))
            {
                Perform(action);
            }
        }
    }

    #endregion

    #region Shapes

    public static readonly DependencyProperty ShapesProperty = DependencyProperty.Register(
        nameof(Shapes),
        typeof(ObservableCollection<UIElement>),
        typeof(DrawingCanvas),
        new FrameworkPropertyMetadata());

    public ObservableCollection<UIElement> Shapes
    {
        get => this.GetValue<ObservableCollection<UIElement>>(ShapesProperty)
            ?? this.SetValue<ObservableCollection<UIElement>>(ShapesProperty, new()); // < dont return a null value
        set
        {
            this.SetValue<ObservableCollection<UIElement>>(ShapesProperty, value);
        }
    }

    public void UpdateShapesFromUndoRedo()
    {
   //     Shapes = new ObservableCollection<UIElement>(UndoRedoStacks.VisibleStack);
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
        set => this.SetValue(ScreenshotProperty, value);    
    }


    #endregion

    #region On Item Mouse Events 

    internal void OnItemMouseEvent(DrawingCanvasListBoxItem item, MouseEventArgs e)
    {
        if (item.Content is not UIElement element)
            return;
        if(e.RoutedEvent == MouseLeftButtonDownEvent)
            Perform(this.Tool?.LeftButtonDown(e.GetPosition(element), element));
        if (e.RoutedEvent == MouseMoveEvent && e.LeftButton == MouseButtonState.Pressed)
            Perform(this.Tool?.MouseMove(e.GetPosition(element), element));
    }

    #endregion

}
