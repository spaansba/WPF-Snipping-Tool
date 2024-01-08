using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snipping_Tool_V4.Screenshots.Modules.Drawing
{
    public abstract class Tool
    {
        public bool IsActive { get; private set; }
        public virtual void Begin(Point location, Pen? stroke)
        {
            IsActive = true;
        }
        public abstract void Continue(Point newLocation);
        public virtual void Finish(Point newLocation, IList<Shape> shapeList)
        {
            IsActive = false;
        }

        public abstract void Draw(Graphics graphics);

        public virtual void Reset()
        {
            IsActive = false;
        }
    }

    public static class Tools
    {
        public static RectangleShapeTool rectangle { get; } = new();

        public static FreehandTool freehand { get; } = new();
    }

    public abstract class ShapeTool : Tool
    {
        public Pen? Stroke { get; private set; }
        public override void Begin(Point location, Pen? stroke)
        {
            this.Stroke = stroke;
            base.Begin(location, stroke);
        }
    }

    public sealed class FreehandTool : ShapeTool
    {
        public Point Start { get; private set; }
        public Point End { get; private set; }

        private List<Point> drawingPoints { get; set; }

        public override void Reset()
        {
            List<Point> drawingPoints = new();
            base.Reset();
        }

        public override void Begin(Point location, Pen? stroke)
        {
            Start = location;
            drawingPoints = new List<Point>();
            drawingPoints.Add(location);
            base.Begin(location, stroke);
        }
        public override void Continue(Point newLocation)
        {
            drawingPoints.Add(newLocation);
        }

        public override void Finish(Point newLocation, IList<Shape> shapeList)
        {
            this.End = newLocation;
            drawingPoints.Add(newLocation);
            var newShape = new FreehandShape(this.Stroke, this.drawingPoints);
            shapeList.Add(newShape);
            base.Finish(newLocation, shapeList);
        }

        public override void Draw(Graphics graphics)
        {
            FreehandShape.DrawFreeHand(graphics, this.drawingPoints, this.Stroke);
        }
    }

    public sealed class RectangleShapeTool : ShapeTool
    {
        public Point Start { get; private set; }
        public Point End { get; private set; }
        public override void Begin(Point location, Pen? stroke)
        {
            this.Start = location;
            this.End = location; //set to end so that the rectangle is not drawn until the mouse is moved
            base.Begin(location, stroke);
        }

        public override void Reset()
        {
            this.Start = new Point(0, 0);
            this.End = new Point(0, 0);
            base.Reset();
        }

        public override void Continue(Point newLocation)
        {
            this.End = newLocation;
        }

        public override void Finish(Point newLocation, IList<Shape> shapeList)
        {
            this.End = newLocation;
            var newShape = new RectangleShape(this.Stroke, this.GetRectangle());
            shapeList.Add(newShape);
            base.Finish(newLocation, shapeList);
        }
        public override void Draw(Graphics graphics)
        {
            RectangleShape.DrawRectangle(graphics, this.GetRectangle(), this.Stroke);
        }

        private Rectangle GetRectangle()
        {
            return new Rectangle(
                Math.Min(this.Start.X, this.End.X), 
                Math.Min(this.Start.Y, this.End.Y), 
                Math.Abs(this.Start.X - this.End.X), 
                Math.Abs(this.Start.Y - this.End.Y)); 
        }
    }
}
