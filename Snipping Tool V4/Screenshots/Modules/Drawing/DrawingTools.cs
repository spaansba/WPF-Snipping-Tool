namespace Snipping_Tool_V4.Screenshots.Modules.Drawing
{

    public abstract class Tool
    {
        public bool IsActive { get; private set; }
        public bool LockedAspectRatio { get; set; } = false;
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

        public abstract void DrawToolIcon(Graphics graphics, Pen? stroke, Brush? fill, Rectangle rect);

        public abstract Point CalculateNewEndLocation(Point location);

        public virtual void Reset()
        {
            IsActive = false;
        }
    }

    public delegate void DrawOrFillShape(Graphics graphics, Rectangle bounds, Pen? stroke, Brush? fill);

    public static class Tools
    {

        public static FreehandTool Freehand { get; } = new();
        public static RectangularShapeTool Triangle { get; } = new RectangularShapeTool(DrawOrFillTriangle);
        public static RectangularShapeTool Pentagon { get; } = new RectangularShapeTool(DrawOrFillPentagon);
        public static RectangularShapeTool Hexagon { get; } = new RectangularShapeTool(DrawOrFillHexagon);
        public static RectangularShapeTool Rectangle { get; } = new RectangularShapeTool(DrawOrFillRectangle);
        public static RectangularShapeTool Ellipse { get; } = new RectangularShapeTool(DrawOrFillEllipse);
        public static RectangularShapeTool Star { get; } = new RectangularShapeTool(DrawOrFillStar);
        public static RectangularShapeTool Heart { get; } = new RectangularShapeTool(DrawOrFillHeart);

        public static void DrawPolygonX(Graphics graphics, Point[] points, Pen? stroke, Brush? fill)
        {
            if (fill != null)
                graphics.FillPolygon(fill, points);
            if (stroke != null)
                graphics.DrawPolygon(stroke, points);
        }
        public static void DrawOrFillTriangle(Graphics graphics, Rectangle bounds, Pen? stroke, Brush? fill)
        {
            Point[] points = new Point[] {
                new Point(bounds.Left + bounds.Width / 2, bounds.Top),
                new Point(bounds.Right, bounds.Bottom),
                new Point(bounds.Left, bounds.Bottom)
                };
            DrawPolygonX(graphics, points, stroke, fill);
        }
        public static void DrawOrFillPentagon(Graphics graphics, Rectangle bounds, Pen? stroke, Brush? fill)
        {
            Point[] points = new Point[]
            {
                new Point(bounds.Left + bounds.Width / 2, bounds.Top),
                new Point(bounds.Right, bounds.Top + bounds.Height / 3),
                new Point(bounds.Right - bounds.Width / 4, bounds.Bottom),
                new Point(bounds.Left + bounds.Width / 4, bounds.Bottom),
                new Point(bounds.Left, bounds.Top + bounds.Height / 3)
                };
            DrawPolygonX(graphics, points, stroke, fill);
        }
        public static void DrawOrFillHexagon(Graphics graphics, Rectangle bounds, Pen? stroke, Brush? fill)
        {
            Point[] points = new Point[] {
                new Point(bounds.Left + bounds.Width / 4, bounds.Top),
                new Point(bounds.Right - bounds.Width / 4, bounds.Top),
                new Point(bounds.Right, bounds.Top + bounds.Height / 2),
                new Point(bounds.Right - bounds.Width / 4, bounds.Bottom),
                new Point(bounds.Left + bounds.Width / 4, bounds.Bottom),
                new Point(bounds.Left, bounds.Top + bounds.Height / 2)
                };

            DrawPolygonX(graphics, points, stroke, fill);
        }

        public static void DrawOrFillStar(Graphics graphics, Rectangle bounds, Pen? stroke, Brush? fill)
        {
            int width = bounds.Width;
            int height = bounds.Height;
            int left = bounds.Left;
            int top = bounds.Top;
            Point[] points = new Point[]
            {
                new Point(left + width/2, top), // Top center
                new Point(left + (int)Math.Round(width * 0.38), top +(int) Math.Round(height * 0.38)), // Inner top left
                new Point(left, top + (int)Math.Round(height * 0.40)), // left
                new Point(left + (int)Math.Round(width * 0.32), top + (int)Math.Round(height * 0.62)), // Inner bottom left
                new Point(left + (int)Math.Round(width * 0.20), top + height), // bottom left
                new Point(left + width/2, top + (int)Math.Round(height * 0.74)), // Inner Bottom Center
                new Point(left + (int)Math.Round(width * 0.80), top + height), // Bottom Right
                new Point(left + (int)Math.Round(width * 0.68), top + (int)Math.Round(height * 0.62)), // Inner bottom right
                new Point(left + width, top + (int)Math.Round(height * 0.40)), // Right
                new Point(left + (int)Math.Round(width * 0.62), top + (int)Math.Round(height * 0.38)), // Inner Top right
            };

            DrawPolygonX(graphics, points, stroke, fill);
        }

        public static void DrawOrFillHeart(Graphics graphics, Rectangle bounds, Pen? stroke, Brush? fill)
        {
            // Draw a heart shape from braziers
            Point[] points = new Point[]
            {
                new Point(bounds.Left + bounds.Width / 2, bounds.Top + bounds.Height / 4),
                new Point(bounds.Left + bounds.Width / 4, bounds.Top),
                new Point(bounds.Left, bounds.Top + bounds.Height / 4),
                new Point(bounds.Left, bounds.Top + bounds.Height / 2),
                new Point(bounds.Left + bounds.Width / 4, bounds.Top + bounds.Height * 3 / 4),
                new Point(bounds.Left + bounds.Width / 2, bounds.Bottom),
                new Point(bounds.Right - bounds.Width / 4, bounds.Top + bounds.Height * 3 / 4),
                new Point(bounds.Right, bounds.Top + bounds.Height / 2),
                new Point(bounds.Right, bounds.Top + bounds.Height / 4),
                new Point(bounds.Right - bounds.Width / 4, bounds.Top)
            };
            DrawPolygonX(graphics, points, stroke, fill);
        }


        public static void DrawOrFillRectangle(Graphics graphics, Rectangle bounds, Pen? stroke, Brush? fill)
        {
            if (stroke != null) graphics.DrawRectangle(stroke, bounds);
            if (fill != null) graphics.FillRectangle(fill, bounds);
        }
        public static void DrawOrFillEllipse(Graphics graphics, Rectangle bounds, Pen? stroke, Brush? fill)
        {
            if (stroke != null) graphics.DrawEllipse(stroke, bounds);
            if (fill != null) graphics.FillEllipse(fill, bounds);
        }

        /// <summary>
        /// List of all Tools, used to populate the ToolBox on the Form
        /// </summary>
        public static List<RectangularShapeTool> ShapeTools = new List<RectangularShapeTool>()
        {
            Rectangle,
            Ellipse,
            Pentagon,
            Triangle,
            Hexagon,
            Star,
            Heart
        };
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
            var newShape = new RectangularShape(this.drawOrFillShape, stroke, fill, this.GetRectangle());
            shapeList.Add(newShape);
            base.Finish(newLocation, shapeList);
        }

        public override void Draw(Graphics graphics)
        {
            Shape.DrawCurrentShapeInRectangle(drawOrFillShape, graphics, this.GetRectangle(), stroke, fill);
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

            if (LockedAspectRatio)
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
            if (!LockedAspectRatio)
            {
                LastShiftPressLocation = new Point(0, 0);
            }

            if (LockedAspectRatio)
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
