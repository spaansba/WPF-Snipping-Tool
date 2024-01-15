using Snipping_Tool_V4.Screenshots.Modules.Screen_Screenshot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snipping_Tool_V4
{
    public static class RectangleExtenstions
    {
        public static Rectangle Shrink(this Rectangle rect, Padding padding)
        {
            return new Rectangle(
                rect.X + padding.Left, rect.Y + padding.Top,
                rect.Width - padding.Horizontal, rect.Height - padding.Vertical);
        }
    }
}
