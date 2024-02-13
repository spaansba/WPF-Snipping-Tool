using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SnippingToolWPF.Interop;

public class ScreenCaptureException : Exception
{
    public ScreenCaptureException(string message, Exception innerException)
        : base(message, innerException)
    { }

    public ScreenCaptureException(string message)
        : base(message)
    { }

}
