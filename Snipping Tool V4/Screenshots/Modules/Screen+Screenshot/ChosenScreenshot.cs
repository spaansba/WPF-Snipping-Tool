using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snipping_Tool_V4.Screenshots.Modules
{
    public class ChosenScreenshot
    {
        public Image screenshot { get; set; }
        public Point topLeftLocation { get; set; }
        public Point bottomRightLocation { get; set; }
        public Rectangle screenshotRectangle { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }
        public UserScreenInformation thisPictureScreenInfo { get; private set; } = new UserScreenInformation();

        public ChosenScreenshot(Point topLeft, Point bottomRight)
        {
            setInfo(topLeft, bottomRight); // Set info instantly with consturctor otherwise use the setInfo method
        }
        public ChosenScreenshot()
        {

        }
        public void setInfo(Point topLeft, Point bottomRight)
        {
            topLeftLocation = topLeft;
            bottomRightLocation = bottomRight;
            Width = bottomRightLocation.X - topLeftLocation.X;
            Height = bottomRightLocation.Y - topLeftLocation.Y;
            screenshotRectangle = new Rectangle(topLeftLocation, new Size(Width, Height));
        }
    }
}
