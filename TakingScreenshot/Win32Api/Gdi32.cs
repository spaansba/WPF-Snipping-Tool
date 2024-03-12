using System.Runtime.InteropServices;

namespace SnippingTool.Interop.Win32Api;

internal class Gdi32
{
    public const int SRCCOPY = 0xCC0020;

    // http://msdn.microsoft.com/en-us/library/dd183370(VS.85).aspx
    [DllImport("gdi32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool BitBlt(IntPtr hDestDC, int x, int y, int nWidth, int nHeight, IntPtr hSrcDC, int xSrc,
        int ySrc, int dwRop);

    // http://msdn.microsoft.com/en-us/library/dd183488(VS.85).aspx
    [DllImport("gdi32.dll")]
    public static extern IntPtr CreateCompatibleBitmap(IntPtr hdc, int nWidth, int nHeight);

    // http://msdn.microsoft.com/en-us/library/dd183489(VS.85).aspx
    [DllImport("gdi32.dll", SetLastError = true)]
    public static extern IntPtr CreateCompatibleDC(IntPtr hdc);

    // http://msdn.microsoft.com/en-us/library/dd162957(VS.85).aspx
    [DllImport("gdi32.dll", ExactSpelling = true, PreserveSig = true, SetLastError = true)]
    public static extern IntPtr SelectObject(IntPtr hdc, IntPtr hgdiobj);

    // http://msdn.microsoft.com/en-us/library/dd183539(VS.85).aspx
    [DllImport("gdi32.dll")]
    public static extern bool DeleteObject(IntPtr hObject);
}