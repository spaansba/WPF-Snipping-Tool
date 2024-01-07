using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snipping_Tool_V4.Screenshots.Modules.Drawing
{
    public abstract class Shape
    {
        protected Shape(DrawingPen stroke)
        {
            this.stroke = stroke;
        }
        public abstract void Draw(Graphics graphics, Shape shape);
        public abstract Rectangle bounds { get; }
        public DrawingPen stroke { get; }
    }

    public class RectangleShape : Shape
    {
        public RectangleShape(DrawingPen stroke, Point location, Size size) : base(stroke)
        {
            this.bounds = new (location, size);
        }
        public override Rectangle bounds { get; }

        public override void Draw(Graphics graphics, Shape shape)
        {
            DrawRectangle(graphics, this.bounds.Location, this.bounds.Size, this.stroke);
        }

        public static void DrawRectangle(Graphics graphics, Point location, Size size, DrawingPen stroke)
        {

            graphics.DrawRectangle(stroke.pen, location.X, location.Y, size.Width, size.Height);
  
        }
    }
}
