using Snipping_Tool_V4.Properties;
using System.Security.Policy;

namespace Snipping_Tool_V4.Screenshots.Modules.Drawing.Tools
{
    public delegate void DrawOrFillShape(Graphics graphics, Rectangle bounds, Pen? stroke, Brush? fill);

    public abstract class Tool
    {
        public bool IsActive { get; private set; }
        public bool LockedAspectRatio { get; set; } = false;
        public virtual void Begin(Point location, Pen? stroke, Brush? fill)
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

    public static class Tools
    {

        public static FreehandTool Freehand { get; } = new();
        public static EraserTool Eraser { get; } = new();
        public static RectangleSelectorTool RectangleSelector { get; } = new();
        public static ColorSelectorTool ColorSelector { get; } = new();
        public static BucketTool BucketTool { get; } = new();
        public static RectangularShapeTool Triangle { get; } = new RectangularShapeTool(DrawOrFillTriangle);
        public static RectangularShapeTool Pentagon { get; } = new RectangularShapeTool(DrawOrFillPentagon);
        public static RectangularShapeTool Hexagon { get; } = new RectangularShapeTool(DrawOrFillHexagon);
        public static RectangularShapeTool Rectangle { get; } = new RectangularShapeTool(DrawOrFillRectangle);
        public static RectangularShapeTool Ellipse { get; } = new RectangularShapeTool(DrawOrFillEllipse);
        public static RectangularShapeTool Star { get; } = new RectangularShapeTool(DrawOrFillStar);
        public static RectangularShapeTool Heart { get; } = new RectangularShapeTool(DrawOrFillHeart);

        #region List of Tools for populating the ToolBox on the Form
        /// <summary>
        /// List of all Tools, used to populate the ToolBox on the Form
        /// </summary>
        public static List<ShapeTool> ShapeTools = new List<ShapeTool>()
        {
            Rectangle,
            Ellipse,
            Pentagon,
            Triangle,
            Hexagon,
            Star,
            Heart
        };

        /// <summary>
        /// List of all Special Tools, used to populate the ToolBox on the Form
        /// </summary>
        public static List<SpecialTools> SpecialTools = new List<SpecialTools>()
        {
            Freehand,
            Eraser,
            RectangleSelector,
            ColorSelector,
            BucketTool
        };
        #endregion

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
    }
}
