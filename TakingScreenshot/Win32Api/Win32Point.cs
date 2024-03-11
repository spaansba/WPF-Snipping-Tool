using System.Runtime.InteropServices;

namespace SnippingTool.Interop.Win32Api;

[StructLayout(LayoutKind.Sequential)]
public struct Win32Point
{
    public int X;
    public int Y;

    public Win32Point(int x, int y)
    {
        this.X = x;
        this.Y = y;
    }

}

