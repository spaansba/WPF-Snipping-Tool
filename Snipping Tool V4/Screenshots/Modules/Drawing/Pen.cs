using System.Configuration;
using System.Drawing.Drawing2D;

namespace Snipping_Tool_V4.Screenshots.Modules.Drawing
{
    public class DrawingPen
    {
        public Pen pen { get; private set; }
        public DrawingPen(Color userColor, int userSize)
        {
            pen = new Pen(userColor, userSize);
            pen.StartCap = LineCap.Round;
            pen.EndCap = LineCap.Round;
            pen.LineJoin = LineJoin.Round;
            pen.DashStyle = DashStyle.Solid;
            pen.DashOffset = 5;
        }
    }
}
