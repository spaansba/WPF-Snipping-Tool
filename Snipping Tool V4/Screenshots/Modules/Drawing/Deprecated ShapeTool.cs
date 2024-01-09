//using System;
//using System.Collections.Generic;
//using System.Diagnostics.SymbolStore;
//using System.Drawing;
//using System.Drawing.Drawing2D;
//using System.Windows.Forms;

//namespace Snipping_Tool_V4.Screenshots.Modules.Drawing
//{
//  //  public abstract class ShapeTool : DrawingTool
//    {
//        public abstract Point startPoint { get; set; }
//    public abstract Point endPoint { get; set; }
//    public abstract Rectangle bounds { get; set; }
//    public abstract DrawingPen stroke { get; set; }
//    public override void Begin(Point location, DrawingPen? stroke)
//    {
//        this.stroke = stroke;
//        base.Begin(location, stroke);
//    }
//}

//public class FreeHand : ShapeTool
//{
//    public override Rectangle bounds { get; set; }
//    public override DrawingPen stroke { get; set; }
//    public override Point startPoint { get; set; }
//    public override Point endPoint { get; set; }

//    public List<Point> currentDrawingPoints { get; set; }

//    // Constructor for temp
//    public FreeHand()
//    {
//    }

//    public FreeHand(Point startpoint, DrawingPen pen)
//    {
//        currentDrawingPoints = new List<Point>();
//        startPoint = startpoint;
//        currentDrawingPoints.Add(startpoint);
//        stroke = pen;
//    }

//    //public override void Draw(Graphics graphics, ShapeTool shape)
//    //{
//    //    if (currentDrawingPoints != null)
//    //    {
//    //        if (currentDrawingPoints.Count > 1)
//    //        {
//    //            using (Pen shapePen = new Pen(shape.stroke.pen.Color, shape.stroke.pen.Width))
//    //            {
//    //                List<Point> curvePoints = currentDrawingPoints.ToList();
//    //                graphics.DrawCurve(shapePen, curvePoints.ToArray());
//    //            }
//    //        }
//    //    }
//    //}

//    public override void Begin(Point location, DrawingPen? stroke)
//    {
//        throw new NotImplementedException();
//    }

//    public override void Continue(Point newLocation)
//    {
//        throw new NotImplementedException();
//    }

//    public override void Draw(Graphics graphics)
//    {
//        throw new NotImplementedException();
//    }
//}

//public class RectangleShapeTool : ShapeTool
//{


//    public Point location { get; private set; }
//    public Size size { get; private set; }
//    public override Point startPoint { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
//    public override Point endPoint { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
//    public override Rectangle bounds { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
//    public override DrawingPen stroke { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

//    public RectangleShapeTool(Point startpoint, DrawingPen pen)
//    {
//        startPoint = startpoint;
//        stroke = pen;
//    }

//    public RectangleShapeTool()
//    {

//    }

//    public override void Continue(Point newLocation)
//    {
//        throw new NotImplementedException();
//    }

//    public override void Draw(Graphics graphics)
//    {
//        throw new NotImplementedException();
//    }

//    public override void Finish(Point newLocation, IList<ShapeTool> shapeList)
//    {
//        this.size = new Size(newLocation.X - location.X, newLocation.Y - location.Y);
//        var newShape = new RectangleShape();
//        base.Finish(newLocation, shapeList);
//    }

//}
//public class CircleShapeTool : ShapeTool
//{
//    public override Rectangle bounds { get; set; }
//    public override DrawingPen stroke { get; set; }
//    public override Point startPoint { get; set; }
//    public override Point endPoint { get; set; }

//    public CircleShapeTool(Point startpoint, DrawingPen pen)
//    {
//        startPoint = startpoint;
//        stroke = pen;
//    }

//    //public override void CreateShape(Point endpoint)
//    //{
//    //    endPoint = endpoint;
//    //    int radius = (int)Math.Sqrt(Math.Pow(endPoint.X - startPoint.X, 2) + Math.Pow(endPoint.Y - startPoint.Y, 2));
//    //    bounds = new Rectangle(startPoint.X - radius, startPoint.Y - radius, 2 * radius, 2 * radius);
//    //}

//    public override void Draw(Graphics graphics)
//    {
//        graphics.DrawEllipse(stroke.pen, bounds);
//    }

//    public override void Begin(Point location, DrawingPen? stroke)
//    {
//        throw new NotImplementedException();
//    }

//    public override void Continue(Point newLocation)
//    {
//        throw new NotImplementedException();
//    }

//}

//public class EllipseShapeTool : ShapeTool
//{
//    public override Rectangle bounds { get; set; }
//    public override DrawingPen stroke { get; set; }
//    public override Point startPoint { get; set; }
//    public override Point endPoint { get; set; }

//    public EllipseShapeTool(Point startpoint, DrawingPen pen)
//    {
//        startPoint = startpoint;
//        stroke = pen;
//    }

//    //public override void CreateShape(Point endpoint)
//    //{
//    //    endPoint = endpoint;
//    //    int width = Math.Abs(endPoint.X - startPoint.X) * 2; // Adjust width for ellipse
//    //    int height = Math.Abs(endPoint.Y - startPoint.Y) * 2; // Adjust height for ellipse

//    //    // Calculate bounds considering the rectangle to define an ellipse
//    //    int x = Math.Min(startPoint.X, endPoint.X) + (Math.Abs(endPoint.X - startPoint.X) / 2) - (width / 2);
//    //    int y = Math.Min(startPoint.Y, endPoint.Y) + (Math.Abs(endPoint.Y - startPoint.Y) / 2) - (height / 2);

//    //    bounds = new Rectangle(x, y, width, height);
//    //}

//    //public override void Draw(Graphics graphics, ShapeTool shape)
//    //{
//    //    graphics.DrawEllipse(stroke.pen, bounds);
//    //}

//    public override void Begin(Point location, DrawingPen? stroke)
//    {
//        throw new NotImplementedException();
//    }

//    public override void Continue(Point newLocation)
//    {
//        throw new NotImplementedException();
//    }

//    public override void Draw(Graphics graphics)
//    {
//        throw new NotImplementedException();
//    }
//}
//}
