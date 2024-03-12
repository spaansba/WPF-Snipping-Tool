using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SnippingToolWPF.Control.Behaviors;

public partial class NumericTextBoxBehavior : AttachableForStyleBehavior<NumericTextBoxBehavior, TextBox>
{
    public bool FloatingPoint
    {
        get => (bool)AssociatedObject.GetValue(NumericTextBox.FloatingPointProperty);
        set => AssociatedObject.SetValue(NumericTextBox.FloatingPointProperty, value);
    }

    public bool AllowNegative
    {
        get => (bool)AssociatedObject.GetValue(NumericTextBox.AllowNegativeProperty);
        set => AssociatedObject.SetValue(NumericTextBox.AllowNegativeProperty, value);
    }

    private Regex Regex => (FloatingPoint, AllowNegative) switch
    {
        (FloatingPoint: true, AllowNegative: true) => SignedFloatingPointRegex(),
        (FloatingPoint: true, AllowNegative: false) => UnsignedFloatingPointRegex(),
        (FloatingPoint: false, AllowNegative: true) => SignedIntegerRegex(),
        (FloatingPoint: false, AllowNegative: false) => UnsignedIntegerRegex()
    };

    /// <summary>
    ///     Check if input is valid no matter source the input is generated (keystrokes / pasting)
    /// </summary>
    private int SelectionEnd => AssociatedObject.SelectionStart + AssociatedObject.SelectionLength;

    [GeneratedRegex(@"^-?[0-9]*\.?[0-9]*$")]
    private static partial Regex SignedFloatingPointRegex();

    [GeneratedRegex(@"^[0-9]*\.?[0-9]*$")]
    private static partial Regex UnsignedFloatingPointRegex();

    [GeneratedRegex(@"^-?[0-9]*$")]
    private static partial Regex SignedIntegerRegex();

    [GeneratedRegex(@"^[0-9]*$")]
    private static partial Regex UnsignedIntegerRegex();

    private bool IsValidInput(string newInput)
    {
        var beforeSelection = AssociatedObject.Text[..AssociatedObject.SelectionStart];
        var afterSelection = AssociatedObject.Text[SelectionEnd..];
        var newText = new StringBuilder().Append(beforeSelection).Append(newInput).Append(afterSelection).ToString();
        return Regex.IsMatch(newText);
    }

    private void OnPasting(object sender, DataObjectPastingEventArgs e)
    {
        var success = e.DataObject.GetDataPresent(typeof(string))
                      && IsValidInput(e.DataObject.GetData(typeof(string))?.ToString() ?? string.Empty);
        if (!success)
            e.CancelCommand();
    }

    private void OnPreviewTextInput(object sender, TextCompositionEventArgs e)
    {
        e.Handled = !IsValidInput(e.Text);
    }

    protected override void OnAttached()
    {
        base.OnAttached();
        AssociatedObject.PreviewTextInput += OnPreviewTextInput;
        AssociatedObject.AddHandler(DataObject.PastingEvent, new DataObjectPastingEventHandler(OnPasting));
    }

    protected override void OnDetaching()
    {
        AssociatedObject.PreviewTextInput -= OnPreviewTextInput;
        AssociatedObject.RemoveHandler(DataObject.PastingEvent, new DataObjectPastingEventHandler(OnPasting));
        base.OnDetaching();
    }
}