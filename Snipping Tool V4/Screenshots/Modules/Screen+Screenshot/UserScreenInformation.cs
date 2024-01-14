namespace Snipping_Tool_V4.Screenshots.Modules
{
    public class UserScreenInformation
    {
        // These ints are off the entire screen
        public readonly int screenLeft = SystemInformation.VirtualScreen.Left;
        public readonly int screenTop = SystemInformation.VirtualScreen.Top;
        public readonly int screenWidth = SystemInformation.VirtualScreen.Width;
        public readonly int screenHeight = SystemInformation.VirtualScreen.Height;
        public readonly Rectangle totalScreenRectangle;
        public readonly Bitmap entireScreen;
        public readonly Screen[] screens = Screen.AllScreens;

        public UserScreenInformation()
        {
            totalScreenRectangle = new Rectangle(screenLeft, screenTop, screenWidth, screenHeight);
            entireScreen = new Bitmap(totalScreenRectangle.Width, totalScreenRectangle.Height);

            using (Graphics graphics = Graphics.FromImage(entireScreen))
            {
                graphics.CopyFromScreen(totalScreenRectangle.Left, totalScreenRectangle.Top, 0, 0, totalScreenRectangle.Size);
            }
        }

        // These ints are off the current screen (Point = pixel on any screen)
        public int getCurrentScreenWidth(Point pixel)
        {
            Screen currentScreen = Screen.FromPoint(pixel);
            return currentScreen.Bounds.Width;
        }

        public int getCurrentScreenHeight(Point pixel)
        {
            Screen currentScreen = Screen.FromPoint(pixel);
            return currentScreen.Bounds.Height;
        }

        public bool isPrimaryScreen(Point pixel)
        {
            Screen currentScreen = Screen.FromPoint(pixel);
            return currentScreen.Primary;
        }

    }
}
