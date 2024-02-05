using System.Collections;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using SnippingToolWPF.Drawing.Tools;

namespace SnippingToolWPF
{
    [TemplatePart(Name = PartCurrentCanvas, Type = typeof(Canvas))]
    [ContentProperty(nameof(Shapes))]
    public class DrawingCanvas : System.Windows.Controls.Control
    {

        private const string PartCurrentCanvas = "PART_CurrentCanvas";

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
        }

        #region set-up / tool change etc

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            OnToolChanged(this.Tool);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.CurrentCanvas = this.GetTemplateChild(PartCurrentCanvas) as Canvas;
            OnToolChanged(this.Tool);

            this.Focus(); // To allow keydown
            this.KeyDown += DrawingCanvasKeyDown;
        }
        private void DrawingCanvasKeyDown(object sender, KeyEventArgs e)
        {
            isTyping = true;
        }

        private Canvas? CurrentCanvas { get; set; }

        public static readonly DependencyProperty ToolProperty = DependencyProperty.Register(
        nameof(Tool),
        typeof(IDrawingTool),
        typeof(DrawingCanvas),
        new FrameworkPropertyMetadata(
            default,
            OnToolChanged));

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
            if (this.CurrentCanvas is null)
                return;
            this.CurrentCanvas.Children.Clear();
            UIElement? visual = newValue?.Visual;
            if (visual is not null)
                this.CurrentCanvas.Children.Add(visual);
        }

        #endregion

        #region Drawing on the canvas
        private bool isDrawing;
        private bool isTyping;
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            Perform(this.Tool?.LeftButtonDown(e.GetPosition(this)));
        }


        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (this.isDrawing)
                Perform(this.Tool?.MouseMove(e.GetPosition(this)));
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonUp(e);
           if (this.isDrawing)
            {
                Perform(this.Tool?.MouseMove(e.GetPosition(this)));
                Perform(this.Tool?.LeftButtonUp());
            }
        }

        private void Perform(DrawingToolAction? action)
        {
            if (action.HasValue)
                Perform(action.Value);
        }

        private void Perform(DrawingToolAction action)
        {
            PerformStartAction(action.StartAction);
            PerformStopAction(action.StopAction);
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
                isTyping = true;
                Keyboard.Focus(this.Tool?.Visual);
            }
            if (action.IsShape)
            {
                this.Shapes.Add(action.Item);
            }
        }

        #endregion

        #region Undo / Redo
        public void UndoLastAction(object sender, KeyEventArgs e)
        {
            if (Shapes.Count  > 0)
            {
            //    this.Shapes.Remove[Shapes.Count - 1];
            }
        }

        #endregion


        private void PerformStopAction(DrawingToolActionItem action)
        {
            if (action.IsMouseCapture)
            {
                this.ReleaseMouseCapture();
                isDrawing = false;
            }
            if (action.IsKeyboardFocus)
            {
                isTyping = false;
                Keyboard.Focus(this);
            }
            if (action.IsShape)
            {
                this.Shapes.Remove(action.Item);
            }
        }
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
            set => this.SetValue<ObservableCollection<UIElement>>(ShapesProperty, value);
        }

        #endregion
    }
}
