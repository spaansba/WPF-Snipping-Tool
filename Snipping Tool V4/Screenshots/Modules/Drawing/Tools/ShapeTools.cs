using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snipping_Tool_V4.Screenshots.Modules.Drawing.Tools
{
    public abstract class ShapeTool : Tool
    {
        public Pen? stroke { get; private set; }
        public Brush? fill { get; private set; }
        public override void Begin(Point location, Pen? stroke, Brush? fill)
        {
            this.fill = fill;
            this.stroke = stroke;
            base.Begin(location, stroke, fill);
        }
    }

    public class RectangularShapeTool : ShapeTool
    {
        public Point Start { get; private set; }
        public Point End { get; private set; }

        private readonly DrawOrFillShape drawOrFillShape;

        public RectangularShapeTool(DrawOrFillShape drawOrFillShape)
        {
            this.drawOrFillShape = drawOrFillShape;
        }

        public override void Begin(Point location, Pen? stroke, Brush? fill)
        {
            Start = location;
            End = CalculateNewEndLocation(location);
            base.Begin(location, stroke, fill);
        }

        public override void Reset()
        {
            Start = new Point();
            End = new Point(0, 0);
            base.Reset();
        }

        public override void Continue(Point newLocation)
        {
            End = CalculateNewEndLocation(newLocation);
        }

        public override void Finish(Point newLocation, IList<Shape> shapeList)
        {
            End = CalculateNewEndLocation(newLocation);
            var newShape = new RectangularShape(drawOrFillShape, stroke, fill, GetRectangle());
            shapeList.Add(newShape);
            base.Finish(newLocation, shapeList);
        }

        public override void Draw(Graphics graphics)
        {
            Shape.DrawCurrentShapeInRectangle(drawOrFillShape, graphics, GetRectangle(), stroke, fill);
        }

        public override void DrawToolIcon(Graphics graphics, Pen? stroke, Brush? fill, Rectangle rect)
        {
            Shape.DrawCurrentShapeInRectangle(drawOrFillShape, graphics, rect, stroke, fill);
        }

        #region Helper Methods  
        /// <summary>
        /// IF shift is held down, calculate the end location to be a perfect shape in a perfect square
        /// </summary>
        public override Point CalculateNewEndLocation(Point location)
        {
            if (LockedAspectRatio)
            {
                int x = location.X;
                int y = location.Y;
                int width = Math.Abs(Start.X - x);
                int height = Math.Abs(Start.Y - y);
                int max = Math.Max(width, height);
                if (Start.X < x) x = Start.X + max;
                else x = Start.X - max;
                if (Start.Y < y) y = Start.Y + max;
                else y = Start.Y - max;
                return new Point(x, y);
            }
            else
            {
                return location;
            }
        }

        private Rectangle GetRectangle()
        {
            return new Rectangle(
                Math.Min(Start.X, End.X), Math.Min(Start.Y, End.Y),
                Math.Abs(Start.X - End.X), Math.Abs(Start.Y - End.Y));
        }
        #endregion

    }
}
