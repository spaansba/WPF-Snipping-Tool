namespace SnippingTool.Interop;

public class ScreenCaptureException : Exception
{
    public ScreenCaptureException(string message, Exception innerException)
        : base(message, innerException)
    { }

    public ScreenCaptureException(string message)
        : base(message)
    { }

}
