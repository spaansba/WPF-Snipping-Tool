using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snipping_Tool_V4.Screenshots
{
    public enum ScreenshotMode
    {
        Fullscreen,
        Window,
        Rectangle,
    }

    public enum ScreenshotTimer
    {
        None,
        OneSecond,
        TwoSeconds,
        ThreeSeconds,
        FourSeconds,
        FiveSeconds,
    }

    public enum DrawingShape
    {
        Freehand,
        Line,
        Arrow,
        Rectangle,
        Ellipse,
        Circle, //Perfect circle unlike ellipse which can be stretched
        Text,
    }
}
