using Microsoft.Xaml.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SnippingToolWPF.Control;

public partial class NumericTextBoxBehavior : AttachableForStyleBehavior<NumericTextBoxBehavior, TextBox>
{

    [GeneratedRegex(@"^-?[0-9]*\.?[0-9]*$")]
    private static partial Regex SignedFloatingPointRegex();

    [GeneratedRegex(@"^[0-9]*\.?[0-9]*$")]
    private static partial Regex UnsignedFloatingPointRegex();

    [GeneratedRegex(@"^-?[0-9]*$")]
    private static partial Regex SignedIntegerRegex();

    [GeneratedRegex(@"^[0-9]*$")]
    private static partial Regex UnsignedIntegerRegex();

    public bool FloatingPoint
    {
        get => (bool)this.AssociatedObject.GetValue(NumericTextBox.FloatingPointProperty);
        set => this.AssociatedObject.SetValue(NumericTextBox.FloatingPointProperty, value);
    }

    public bool AllowNegative
    {
        get => (bool)this.AssociatedObject.GetValue(NumericTextBox.AllowNegativeProperty);
        set => this.AssociatedObject.SetValue(NumericTextBox.AllowNegativeProperty, value);
    }

    private Regex Regex => (this.FloatingPoint, this.AllowNegative) switch
    {
        (FloatingPoint: true, AllowNegative: true) => SignedFloatingPointRegex(),
        (FloatingPoint: true, AllowNegative: false) => UnsignedFloatingPointRegex(),
        (FloatingPoint: false, AllowNegative: true) => SignedIntegerRegex(),
        (FloatingPoint: false, AllowNegative: false) => UnsignedIntegerRegex(),
    };

    /// <summary>
    /// Check if input is valid no matter source the input is generated (keystrokes / pasting)
    /// </summary>
    private int SelectionEnd => this.AssociatedObject.SelectionStart + this.AssociatedObject.SelectionLength;
    private bool IsValidInput(string newInput)
    {
        string? beforeSelection = this.AssociatedObject.Text.Substring(0, this.AssociatedObject.SelectionStart);
        string? afterSelection = this.AssociatedObject.Text.Substring(this.SelectionEnd);
        string? newText = $"{beforeSelection}{newInput}{afterSelection}";
        return this.Regex.IsMatch(newText);
    }

    private void OnPasting(object sender, DataObjectPastingEventArgs e)
    {
        var success = e.DataObject.GetDataPresent(typeof(string))
          && this.IsValidInput(e.DataObject.GetData(typeof(string))?.ToString() ?? string.Empty);
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
        this.AssociatedObject.PreviewTextInput += OnPreviewTextInput;
        this.AssociatedObject.AddHandler(DataObject.PastingEvent, new DataObjectPastingEventHandler(this.OnPasting));
    }

    protected override void OnDetaching()
    {
        this.AssociatedObject.PreviewTextInput -= OnPreviewTextInput;
        this.AssociatedObject.RemoveHandler(DataObject.PastingEvent, new DataObjectPastingEventHandler(this.OnPasting));
        base.OnDetaching();
    }
}
