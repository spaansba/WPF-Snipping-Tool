using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snipping_Tool_V4.Screenshots.Modules.Drawing
{
    public abstract class Shape
    {
        protected Shape(Pen stroke)
        {
            this.stroke = stroke;
        }
        public abstract void Draw(Graphics graphics);
        public abstract Rectangle bounds { get; }
        public Pen stroke { get; }
    }

    public class FreehandShape : Shape
    {
        public override Rectangle bounds { get; }
        public List<Point> drawingPoints { get; private set; }

        public FreehandShape(Pen stroke, List<Point> currentDrawingPoints) : base(stroke)
        {
            this.drawingPoints = currentDrawingPoints;
        }
        public override void Draw(Graphics graphics)
        {
            DrawFreeHand(graphics, drawingPoints, stroke);
        }

        public static void DrawFreeHand(Graphics graphics, List<Point> drawingPoints, Pen stroke)
        {
            if (drawingPoints.Count > 1)
            {
                List<Point> curvePoints = drawingPoints.ToList();
                graphics.DrawCurve(stroke, curvePoints.ToArray());
            }
        }
    }

    public class RectangleShape : Shape
    {
        public RectangleShape(Pen stroke, Rectangle bounds) : base(stroke)
        {
            this.bounds = bounds;
        }
        public override Rectangle bounds { get; }

        public override void Draw(Graphics graphics)
        {
            DrawRectangle(graphics, bounds, this.stroke);
        }

        public static void DrawRectangle(Graphics graphics, Rectangle bounds, Pen stroke)
        {
            graphics.DrawRectangle(stroke, bounds);
        }
    }
}
