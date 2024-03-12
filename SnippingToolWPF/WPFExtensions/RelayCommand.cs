using System.Windows.Input;

namespace SnippingToolWPF.WPFExtensions;

public class RelayCommand : ICommand
{
    private readonly Func<object?, bool>? canExecute;
    private readonly Action<object?> execute;

    public RelayCommand(Action<object?> execute, Func<object?, bool>? canExecute = null)
    {
        this.execute = execute ?? throw new ArgumentNullException(nameof(execute));
        this.canExecute = canExecute;
    }

    public bool CanExecute(object? parameter)
    {
        return canExecute?.Invoke(parameter) ?? true;
    }

    public void Execute(object? parameter)
    {
        execute(parameter);
    }

    public event EventHandler? CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value!;
        remove => CommandManager.RequerySuggested -= value!;
    }
}