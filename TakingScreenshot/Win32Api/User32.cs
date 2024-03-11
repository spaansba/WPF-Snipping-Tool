using System.Runtime.InteropServices;

namespace SnippingTool.Interop.Win32Api;

class User32
{
    [DllImport("user32.dll")]
    public static extern IntPtr GetDesktopWindow();

    // http://msdn.microsoft.com/en-us/library/dd144871(VS.85).aspx
    [DllImport("user32.dll")]
    public static extern IntPtr GetDC(IntPtr hwnd);

    // http://msdn.microsoft.com/en-us/library/dd162920(VS.85).aspx
    [DllImport("user32.dll")]
    public static extern int ReleaseDC(IntPtr hwnd, IntPtr dc);

    // Important note for Vista / Win7 on this function. In those version, rectangle returned is not 100% correct
    [DllImport("user32.dll")]
    public static extern bool GetWindowRect(IntPtr hWnd, out Win32Rect rect);

    [DllImport("user32.dll")]
    public static extern bool GetClientRect(IntPtr hWnd, out Win32Rect rect);

    [DllImport("user32.dll")]
    public static extern bool ClientToScreen(IntPtr hWnd, ref Win32Point lpPoint);

    [DllImport("user32.dll")]
    public static extern IntPtr SetForegroundWindow(IntPtr hWnd);

    [DllImport("user32.dll")]
    public static extern IntPtr WindowFromPoint(Win32Point pt);


}
