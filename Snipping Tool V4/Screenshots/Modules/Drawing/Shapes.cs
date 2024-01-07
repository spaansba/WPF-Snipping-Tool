using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Snipping_Tool_V4.Screenshots.Modules.Drawing
{
    public abstract class Shape
    {
        public abstract void Draw(Graphics graphics, Shape shape);
        public abstract void CreateShape(Point endpoint);
        public abstract Point startPoint { get; set; }
        public abstract Point endPoint { get; set; }
        public abstract Rectangle bounds { get; set; }
        public abstract DrawingPen usingPen { get; set; } 
        public abstract List<Point> currentDrawingPointss { get; set; }
    }

    public class FreeHand : Shape
    {
        public override Rectangle bounds { get; set; }
        public override DrawingPen usingPen { get; set; }
        public override Point startPoint { get; set; }
        public override Point endPoint { get; set; }

        public override List<Point> currentDrawingPointss { get; set; }

        // Constructor for temp
        public FreeHand()
        {
        }

        public FreeHand(Point startpoint, DrawingPen pen)
        {
            currentDrawingPointss = new List<Point>();
            startPoint = startpoint;
            currentDrawingPointss.Add(startpoint);
            usingPen = pen;
        }

        public override void Draw(Graphics graphics, Shape shape)
        {
            if (currentDrawingPointss != null)
            {
                if (currentDrawingPointss.Count > 1)
                {
                    using (Pen shapePen = new Pen(shape.usingPen.pen.Color, shape.usingPen.pen.Width))
                    {
                        List<Point> curvePoints = shape.currentDrawingPointss.ToList();
                        graphics.DrawCurve(shapePen, curvePoints.ToArray());
                    }
                }
            }
        }

        public override void CreateShape(Point endpoint)
        {
        }
    }

    public class RectangleShape : Shape
    {
        public override Rectangle bounds { get; set; }
        public override DrawingPen usingPen { get; set; }
        public override Point startPoint { get; set; }
        public override Point endPoint { get; set; }
        public override List<Point> currentDrawingPointss { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public RectangleShape(Point startpoint, DrawingPen pen)
        {
            startPoint = startpoint;
            usingPen = pen;
        }

        public override void CreateShape(Point endpoint)
        {
            endPoint = endpoint;
            int x = Math.Min(startPoint.X, endPoint.X);
            int y = Math.Min(startPoint.Y, endPoint.Y);
            int width = Math.Abs(startPoint.X - endPoint.X);
            int height = Math.Abs(startPoint.Y - endPoint.Y);
            bounds = new Rectangle(x, y, width, height);
        }

        public override void Draw(Graphics graphics, Shape shape)
        {
            graphics.DrawRectangle(usingPen.pen, bounds);
        }

    }
    public class EllipseShape : Shape
    {
        public override Rectangle bounds { get; set; }
        public override DrawingPen usingPen { get; set; }
        public override Point startPoint { get; set; }
        public override Point endPoint { get; set; }
        public override List<Point> currentDrawingPointss { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public EllipseShape(Point startpoint, DrawingPen pen)
        {
            startPoint = startpoint;
            usingPen = pen;
        }

        public override void CreateShape(Point endpoint)
        {
            endPoint = endpoint;
            int radius = (int)Math.Sqrt(Math.Pow(endPoint.X - startPoint.X, 2) + Math.Pow(endPoint.Y - startPoint.Y, 2));
            bounds = new Rectangle(startPoint.X - radius, startPoint.Y - radius, 2 * radius, 2 * radius);
        }

        public override void Draw(Graphics graphics, Shape shape)
        {
            graphics.DrawEllipse(usingPen.pen, bounds);
        }
    }
}
