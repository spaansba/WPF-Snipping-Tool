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
        public virtual void Begin(Point location, DrawingPen? stroke)
        {
            IsActive = true;
        }
        public abstract void Continue(Point newLocation);
        public virtual void Finish(Point newLocation, IList<Shape> shapeList)
        {
            IsActive = false;
        }
        public abstract void Draw(Graphics graphics);

    }

    public static class Tools
    {
        public static RectangleShapeTool rectangle { get; } = new();
    }

    public abstract class ShapeTool : Tool
    {
        public DrawingPen? Stroke { get; private set; }
        public override void Begin(Point location, DrawingPen? stroke)
        {
            this.Stroke = stroke;
            base.Begin(location, stroke);
        }
    }

    public sealed class RectangleShapeTool : ShapeTool
    {
        public Point Location { get; private set; }
        public Size Size { get; private set; }
        public override void Begin(Point location, DrawingPen? stroke)
        {
            this.Location = location;
            this.Size = Size.Empty;
            base.Begin(location, stroke);
        }
        public override void Continue(Point newLocation)
        {
            this.Size = new Size(newLocation.X - this.Location.X, newLocation.Y - this.Location.Y);
        }

        public override void Finish(Point newLocation, IList<Shape> shapeList)
        {
            this.Size  = new Size(newLocation.X - this.Location.X, newLocation.Y - this.Location.Y);
            var newShape = new RectangleShape(this.Stroke,this.Location, this.Size);
            shapeList.Add(newShape);
            base.Finish(newLocation, shapeList);
        }
        public override void Draw(Graphics graphics)
        {
            RectangleShape.DrawRectangle(graphics, this.Location, this.Size, this.Stroke);
        }
    }
}
