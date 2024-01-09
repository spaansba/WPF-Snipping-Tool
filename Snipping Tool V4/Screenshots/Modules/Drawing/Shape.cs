using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snipping_Tool_V4.Screenshots.Modules.Drawing
{
    public abstract class Shape
    {
        protected Shape(Pen? stroke, Brush? fill)
        {
            this.stroke = stroke;
            this.fill = fill;
        }
        public abstract void Draw(Graphics graphics);
        public abstract Rectangle bounds { get; }
        public Pen? stroke { get; }
        public Brush? fill { get; }

        public static void DrawCurrentShapeInRectangle(DrawOrFillShape drawOrFillShape, Graphics graphics, Rectangle bounds, Pen? stroke, Brush? fill)
        {
            drawOrFillShape(graphics, bounds, stroke, fill);
        }
    }

    public class RectangularShape : Shape
    {
        public override Rectangle bounds { get; }
        private readonly DrawOrFillShape drawOrFillShape;

        public RectangularShape(DrawOrFillShape drawOrFillShape, Pen? stroke, Brush? fill, Rectangle bounds) : base(stroke, fill)
        {
            this.bounds = bounds;
            this.drawOrFillShape = drawOrFillShape;
        }
        public override void Draw(Graphics graphics)
        {
            drawOrFillShape(graphics, bounds, stroke, fill);
        }
    }

    public class FreehandShape : Shape
    {
        public override Rectangle bounds { get; }
        public List<Point> drawingPoints { get; private set; }

        public FreehandShape(Pen? stroke, Brush? fill, List<Point> currentDrawingPoints) : base(stroke, fill)
        {
            this.drawingPoints = currentDrawingPoints;
        }
        public override void Draw(Graphics graphics)
        {
            DrawFreeHand(graphics, drawingPoints, stroke);
        }

        public static void DrawFreeHand(Graphics graphics, List<Point> drawingPoints, Pen? stroke)
        {
            if (drawingPoints.Count > 1)
            {
                List<Point> curvePoints = drawingPoints.ToList();
                graphics.DrawCurve(stroke, curvePoints.ToArray());
            }
        }
    }
}