using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ReaLTaiizor.Util.RoundInt;

namespace Snipping_Tool_V4.Screenshots.Modules.Drawing
{

    public abstract class Tool
    {
        public bool IsActive { get; private set; }
        public bool shiftPressed { get; set; } = false; //TODO: Ask if its bad that this is not private set, gets set in the viewmodel
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

        public abstract void DrawToolIcon(Graphics graphics, Pen stroke, Brush fill, Rectangle rect);

        public abstract Point CalculateNewEndLocation(Point location);

        public virtual void Reset()
        {
            IsActive = false;
        }
    }

    public delegate void DrawOrFillShape(Graphics graphics, Rectangle bounds, Pen? stroke, Brush? fill, int? sides);

    public static class Tools
    {

        public static FreehandTool Freehand { get; } = new();
        public static RectangularShapeTool Triangle { get; } = new RectangularShapeTool(DrawOrFillPolygon, 3);
        public static RectangularShapeTool Pentagon { get; } = new RectangularShapeTool(DrawOrFillPolygon, 5);
        public static RectangularShapeTool Hexagon { get; } = new RectangularShapeTool(DrawOrFillPolygon, 6);
        public static RectangularShapeTool Heptagon { get; } = new RectangularShapeTool(DrawOrFillPolygon, 7);
        public static RectangularShapeTool Rectangle { get; } = new RectangularShapeTool(DrawOrFillRectangle);
        public static RectangularShapeTool Ellipse { get; } = new RectangularShapeTool(DrawOrFillEllipse);

        /// <summary>
        /// Draws a polygon with the given number of sides
        /// TODO: Fix the polygon drawing so that it is centered in the rectangle
        /// </summary>
        public static void DrawOrFillPolygon(Graphics graphics, Rectangle bounds, Pen? stroke, Brush? fill, int? sides)
        {
            PointF[] points = new PointF[(int)sides];
            double angleIncrement = (2 * Math.PI) / (int)sides;

            PointF center = new PointF(bounds.X + bounds.Width / 2, bounds.Y + bounds.Height / 2);
            double radius = Math.Min(bounds.Width, bounds.Height) / 2;

            // Calculate the distance from the center to the furthest point
            double distanceFromCenter = radius * Math.Cos(angleIncrement / 2);

            for (int i = 0; i < (int)sides; i++)
            {
                double angle = i * angleIncrement - Math.PI / 2; // Starting from the top
                float x = center.X + (float)(distanceFromCenter * Math.Cos(angle));
                float y = center.Y + (float)(distanceFromCenter * Math.Sin(angle));
                points[i] = new PointF(x, y);
            }

            if (stroke != null) graphics.DrawPolygon(stroke, points);
            if (fill != null) graphics.FillPolygon(fill, points);
        }

        public static void DrawOrFillRectangle(Graphics graphics, Rectangle bounds, Pen? stroke, Brush? fill, int? sides)
        {
            if (stroke != null) graphics.DrawRectangle(stroke, bounds);
            if (fill != null) graphics.FillRectangle(fill, bounds);
        }
        public static void DrawOrFillEllipse(Graphics graphics, Rectangle bounds, Pen? stroke, Brush? fill, int? sides)
        {
            if (stroke != null) graphics.DrawEllipse(stroke, bounds);
            if (fill != null) graphics.FillEllipse(fill, bounds);
        }

    }

    public abstract class ShapeTool : Tool
    {
        public Pen? stroke { get; private set; }
        public Brush? fill { get; private set; } // TODO: Implement fill
        public override void Begin(Point location, Pen? stroke)
        {
            this.stroke = stroke;
            base.Begin(location, stroke);
        }
    }

    public class RectangularShapeTool : ShapeTool
    {
        public Point Start { get; private set; }
        public Point End { get; private set; }

        private readonly DrawOrFillShape drawOrFillShape;

        private readonly int sides;

        public RectangularShapeTool(DrawOrFillShape drawOrFillShape, int sides)
        {
            this.drawOrFillShape = drawOrFillShape;
            this.sides = sides;
        }
        public RectangularShapeTool(DrawOrFillShape drawOrFillShape)
        {
            this.drawOrFillShape = drawOrFillShape;
        }


        public override void Begin(Point location, Pen? stroke)
        {
            Start = location;
            End = CalculateNewEndLocation(location); 
            base.Begin(location, stroke);
        }

        public override void Reset()
        {
            Start = new Point(0, 0);
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
            var newShape = new RectangularShape(this.drawOrFillShape, stroke, fill, this.GetRectangle(), sides);
            shapeList.Add(newShape);
            base.Finish(newLocation, shapeList);
        }

        public override void Draw(Graphics graphics)
        {
            Shape.DrawCurrentShapeInRectangle(drawOrFillShape, graphics, this.GetRectangle(), stroke, fill, sides);
        }

        public override void DrawToolIcon(Graphics graphics, Pen stroke, Brush fill, Rectangle rect)
        {
        //    drawShapeOutline(graphics, rect, stroke);
        }

        #region Helper Methods  
        /// <summary>
        /// IF shift is held down, calculate the end location to be a perfect shape in a perfect square
        /// </summary>
        public override Point CalculateNewEndLocation(Point location)
        {
            if (shiftPressed)
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

    public sealed class FreehandTool : ShapeTool
    {
        public Point Start { get; private set; }
        public Point End { get; private set; }
        public Point LastShiftPressLocation { get; private set; }

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
            drawingPoints.Add(CalculateNewEndLocation(newLocation));
        }

        public override void Finish(Point newLocation, IList<Shape> shapeList)
        {
            End = CalculateNewEndLocation(newLocation);
            drawingPoints.Add(newLocation);
            var newShape = new FreehandShape(this.stroke, fill, this.drawingPoints);
            shapeList.Add(newShape);
            base.Finish(newLocation, shapeList);
        }

        public override void Draw(Graphics graphics)
        {
            FreehandShape.DrawFreeHand(graphics, this.drawingPoints, this.stroke);
        }

        public override void DrawToolIcon(Graphics graphics, Pen stroke, Brush fill, Rectangle rect)
        {
            throw new NotImplementedException();
        }

        #region Calculate End Location in Case Shift is Pressed
        public override Point CalculateNewEndLocation(Point location)
        {
            SetLastShiftPressLocation(location);

            if (shiftPressed)
            {
                //Check if the location is horizontal or vertical further away from the last shift press location
                int x = location.X;
                int y = location.Y;
                int xDistance = Math.Abs(LastShiftPressLocation.X - x);
                int yDistance = Math.Abs(LastShiftPressLocation.Y - y);
                if (xDistance > yDistance)
                {
                    y = LastShiftPressLocation.Y;
                }
                else
                {
                    x = LastShiftPressLocation.X;
                }
                return new Point(x, y);
            }
            else
            {
                return location;
            }
        }

        private void SetLastShiftPressLocation(Point location)
        {
            if (!shiftPressed)
            {
                LastShiftPressLocation = new Point(0, 0);
            }

            if (shiftPressed)
            {
                if (LastShiftPressLocation == new Point(0, 0))
                {
                    LastShiftPressLocation = location;
                }
            }
        }
        #endregion
    }
}
