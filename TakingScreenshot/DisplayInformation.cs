using SnippingTool.Interop;
using System.Drawing;
using System.Runtime.InteropServices;
using static SnippingToolWPF.Interop.MonitorInfo;

// Interop between WPF and WIN32 API
namespace SnippingToolWPF.Interop;

public static class MonitorInfo
{
    /// <summary>
    /// Gathers minotor info for all the users monitors and returns the aggrageted Rect
    /// </summary>
    public static WpfRect GetTotalMonitorRect()
    {
        return GetMonitorRects().Aggregate(WpfRect.Union); // Switch WinformsRect to WPFRect
    }

    internal delegate bool MonitorEnumProc(IntPtr hMonitor, IntPtr hdcMonitor, ref RECT lprcMonitor, IntPtr dwData);

    private static List<WpfRect> GetMonitorRects()
    {
        List<WpfRect> monitorRects = new List<WpfRect>();
        NativeMethods.EnumDisplayMonitors(IntPtr.Zero, IntPtr.Zero, EnumDisplayMonitorsCallback, IntPtr.Zero);

        // Local function (method in method)
        bool EnumDisplayMonitorsCallback(IntPtr hMonitor, IntPtr hdcMonitor, ref RECT lprcMonitor, IntPtr dwData)
        {

            MONITORINFO mi = new MONITORINFO();
            mi.cbSize = Marshal.SizeOf(mi);
            if (NativeMethods.GetMonitorInfo(hMonitor, ref mi))
            {
                monitorRects.Add(mi.rcMonitor.ToWpfRect());
            }
            return true;
        }

        return monitorRects;
    }
}
file static class NativeMethods
{
    [DllImport("user32.dll")]
    public static extern bool EnumDisplayMonitors(IntPtr hdc, IntPtr lprcClip, MonitorEnumProc lpfnEnum, IntPtr dwData);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern bool GetMonitorInfo(IntPtr hMonitor, ref MONITORINFO lpmi);
}