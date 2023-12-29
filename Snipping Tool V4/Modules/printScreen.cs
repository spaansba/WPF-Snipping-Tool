using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snipping_Tool_V4.Modules
{
    public static class printScreen
    {
        public static int screenLeft = SystemInformation.VirtualScreen.Left;
        public static int screenTop = SystemInformation.VirtualScreen.Top;
        public static int screenWidth = SystemInformation.VirtualScreen.Width;
        public static int screenHeight = SystemInformation.VirtualScreen.Height;
        public static Bitmap entireScreen = new Bitmap(screenWidth, screenHeight);
    }
}
