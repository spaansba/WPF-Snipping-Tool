using System.Runtime.InteropServices;

namespace SnippingTool.Interop.Win32Api;

[StructLayout(LayoutKind.Sequential)]
public struct Win32Rect
{
    public int Left;
    public int Top;
    public int Right;
    public int Bottom;


    public int Width => Right - Left;

    public int Height => Bottom - Top;
}