using Snipping_Tool_V4.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snipping_Tool_V4.Screenshots.Modules.Drawing.Tools
{
    public abstract class SpecialTools : Tool
    {
        public Pen? stroke { get; private set; }
        public Brush? fill { get; private set; }
        public abstract Image pngImage { get; }
        public override void Begin(Point location, Pen? stroke, Brush? fill)
        {
            this.fill = fill;
            this.stroke = stroke;
            base.Begin(location, stroke, fill);
        }
        public override void DrawToolIcon(Graphics graphics, Pen? stroke, Brush? fill, Rectangle rect)
        {
            // Calculate the position to center the image within the rectangle
            int x = rect.X + (rect.Width - pngImage.Width) / 2;
            int y = rect.Y + (rect.Height - pngImage.Height) / 2;

            graphics.DrawImage(pngImage, x, y, pngImage.Width, pngImage.Height);
            pngImage.Dispose();
        }
    }

    public sealed class FreehandTool : SpecialTools
    {
        public Point Start { get; private set; }
        public Point End { get; private set; }
        public Point LastShiftPressLocation { get; private set; }

        private List<Point> drawingPoints { get; set; }
        public override Image pngImage { get => Resources.PencilTool; }

        public override void Reset()
        {
            List<Point> drawingPoints = new();
            base.Reset();
        }

        public override void Begin(Point location, Pen? stroke, Brush? fill)
        {
            Start = location;
            drawingPoints = new List<Point>();
            drawingPoints.Add(location);
            base.Begin(location, stroke, fill);
        }
        public override void Continue(Point newLocation)
        {
            drawingPoints.Add(CalculateNewEndLocation(newLocation));
        }

        public override void Finish(Point newLocation, IList<Shape> shapeList)
        {
            End = CalculateNewEndLocation(newLocation);
            drawingPoints.Add(newLocation);
            var newShape = new FreehandShape(stroke, fill, drawingPoints);
            shapeList.Add(newShape);
            base.Finish(newLocation, shapeList);
        }

        public override void Draw(Graphics graphics)
        {
            FreehandShape.DrawFreeHand(graphics, drawingPoints, stroke);
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

    public sealed class EraserTool : SpecialTools
    {
        public override Image pngImage { get => Resources.SelectionTool; }

        public override Point CalculateNewEndLocation(Point location)
        {
            return new Point((Size)Point.Empty);
        }

        public override void Continue(Point newLocation)
        {

        }

        public override void Draw(Graphics graphics)
        {

        }
    }

    //TODO make RectangleSelectorTool
    public sealed class RectangleSelectorTool : SpecialTools
    {
        public override Image pngImage { get => Resources.EraserTool; }

        public override Point CalculateNewEndLocation(Point location)
        {
            return new Point((Size)Point.Empty);
        }

        public override void Continue(Point newLocation)
        {

        }

        public override void Draw(Graphics graphics)
        {

        }
    }

    //TODO make ColorSelectorTool
    public sealed class ColorSelectorTool : SpecialTools
    {
        public override Image pngImage { get => Resources.DropperTool; }

        public override Point CalculateNewEndLocation(Point location)
        {
            return new Point((Size)Point.Empty);
        }

        public override void Continue(Point newLocation)
        {

        }

        public override void Draw(Graphics graphics)
        {

        }
    }

    //TODO make BucketTool
    public sealed class BucketTool : SpecialTools
    {
        public override Image pngImage { get => Resources.BucketTool; }

        public override Point CalculateNewEndLocation(Point location)
        {
            return new Point((Size)Point.Empty);
        }

        public override void Continue(Point newLocation)
        {

        }

        public override void Draw(Graphics graphics)
        {

        }
    }
}
