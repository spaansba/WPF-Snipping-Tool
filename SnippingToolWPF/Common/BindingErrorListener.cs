using System.Diagnostics;

namespace SnippingToolWPF.Common;

internal sealed class BindingErrorListener : TraceListener
{
    private readonly Action<string?> errorHandler;

    public BindingErrorListener(Action<string?> errorHandler)
    {
        this.errorHandler = errorHandler;
    }

    public override void WriteLine(string? message)
    {
        errorHandler.Invoke(message);
    }

    public override void Write(string? message)
    {
    }

    public static void Create(Action<string?> errorHandler)
    {
        var bindingTrace = PresentationTraceSources.DataBindingSource;
        bindingTrace.Listeners.Add(new BindingErrorListener(errorHandler));
        bindingTrace.Switch.Level = SourceLevels.Error;
    }

    public static void Break()
    {
        Create(static _ => Debugger.Break());
    }

    public static void WriteToConsole()
    {
        Create(static message => Console.WriteLine(message));
    }
}