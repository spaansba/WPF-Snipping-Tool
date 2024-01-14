using Snipping_Tool_V4.Screenshots.Modules;
using System.Drawing.Drawing2D;

namespace Snipping_Tool_V4.Modules
{
    public class PreviewEllipse
    {
        private Point mouseLocation;
        private Bitmap originalBitmap;

        // Standard ellipse size consts/attribute
        private const int sizeCircle = 130;
        private int pixelsFromMouseWidth = 14;
        private int pixelsFromMouseHeight = 14;

        private UserScreenInformation screenInfo;

        public PreviewEllipse(Point currentMouseLocation, Bitmap original, UserScreenInformation UserScreenInformation)
        {
            mouseLocation = currentMouseLocation;
            originalBitmap = original;
            screenInfo = UserScreenInformation;
        }

        /// <summary>
        /// When selecting a screenshot area by holding the mouse this ellipse shows you a zoomed in version of where the 
        /// mouse is located for more precise pictures
        /// </summary>
        public void drawPreviewEllipse(PaintEventArgs e)
        {
            // Draw an preview Elipse
            using (Pen elipsePen = new Pen(Color.White, 2))
            {
                Color pixelColor = originalBitmap.GetPixel(mouseLocation.X, mouseLocation.Y);

                // Check if the pixel is dark or light based on the threshold
                // This color decides the outside of the circle, light  = black, dark = white
                if ((int)(0.299 * pixelColor.R + 0.587 * pixelColor.G + 0.114 * pixelColor.B) < 128)
                {
                    elipsePen.Color = Color.White;
                }
                else
                {
                    elipsePen.Color = Color.Black;
                }

                //Followin code draws an circle 
                const int ellipsePoints = 360;
                PointF[] circlePoints = new PointF[ellipsePoints];

                Rectangle ellipseBounds = setEllipseLocation();

                for (int i = 0; i < ellipsePoints; i++)
                {
                    float angle = 360f / ellipsePoints * i;
                    float x = 130 / 2 + (ellipseBounds.X + ellipseBounds.Width / 2 * (float)Math.Cos(angle * Math.PI / 180));
                    float y = 130 / 2 + (ellipseBounds.Y + ellipseBounds.Height / 2 * (float)Math.Sin(angle * Math.PI / 180));
                    circlePoints[i] = new PointF(x, y);
                }

                e.Graphics.DrawLines(elipsePen, circlePoints);

                // set clip to the ellipse, means that new drawings cant clip over the outside of the ellipse
                GraphicsPath ellipsePath = new GraphicsPath();
                ellipsePath.AddEllipse(ellipseBounds);
                e.Graphics.SetClip(ellipsePath);

                // Calculate the zoomed rectangle based on the mouse position
                int ZoomFactor = 4;
                Rectangle zoomedRect = new Rectangle(
                    mouseLocation.X - (sizeCircle / (2 * ZoomFactor)),
                    mouseLocation.Y - (sizeCircle / (2 * ZoomFactor)),
                    sizeCircle / ZoomFactor,
                    sizeCircle / ZoomFactor);

                // Draw zoomed in pic inside the ellipse
                e.Graphics.DrawImage(originalBitmap, ellipseBounds, zoomedRect, GraphicsUnit.Pixel);

                //Draw a roster inside the ellipse
                drawPreviewRoster(e, ellipseBounds);

                //Draw the outline circle (again) at the end so there are no rough outlines
                e.Graphics.DrawLines(elipsePen, circlePoints);
                e.Graphics.ResetClip();
            }
        }

        /// <summary>
        /// Calculate elipse location based on the mouse location
        /// If elipse exceeds screen boundaries, adjust the location
        /// </summary>
        /// <returns>Elipse display location</returns>
        private Rectangle setEllipseLocation()
        {
            Rectangle ellipseBounds = new Rectangle(mouseLocation.X + pixelsFromMouseWidth, mouseLocation.Y + pixelsFromMouseHeight, sizeCircle, sizeCircle);

            if (ellipseBounds.Left < 0)
            {
                ellipseBounds.X = 0;
            }
            else if (ellipseBounds.Right > screenInfo.screenWidth)
            {
                ellipseBounds.X = screenInfo.screenWidth - sizeCircle;
            }
            if (ellipseBounds.Top < 0)
            {
                ellipseBounds.Y = 0;
            }
            else if (ellipseBounds.Bottom > screenInfo.screenHeight)
            {
                ellipseBounds.Y = screenInfo.screenHeight - sizeCircle;
            }
            return ellipseBounds;
        }

        /// <summary>
        /// Creates a rectangle with a black roster for inside the ellipse
        /// the middle of the rectangle will be ligtblue
        /// </summary>
        /// <param name="e">paintevents from the userform paint event</param>
        private void drawPreviewRoster(PaintEventArgs e, Rectangle ellipseBounds)
        {
            Pen squarePen = PenCache.GetPen(Color.FromArgb(60, Color.Black), 1);

            int pixelSize = 9; // The size of the black squares
            int numSquaresPerAxes = pixelSize * 2;

            // Calculate the start location of the squares
            int startX = ellipseBounds.X - (pixelSize / 2);
            int startY = ellipseBounds.Y - (pixelSize / 2);

            List<Point> middleSquares = new();
            Point middleMiddleSquares = new Point(0, 0);

            // Draw the black squares around the pixels of the ellipse
            for (int i = 0; i < numSquaresPerAxes; i++)
            {
                for (int j = 0; j < numSquaresPerAxes; j++)
                {
                    int x = startX + (i * pixelSize);
                    int y = startY + (j * pixelSize);

                    if (j == 7 && i == 7)
                    {
                        middleMiddleSquares = new Point(x, y);
                    }
                    else if (j == 7 || i == 7)
                    {
                        middleSquares.Add(new Point(x, y));
                    }
                    else
                    {
                        e.Graphics.DrawRectangle(squarePen, x, y, pixelSize, pixelSize);
                    }

                }
            }

            //Create lightblue brush and fill the middle y/x squares
            Color lightBlue = Color.FromArgb(40, 0, 100, 255);
            SolidBrush lightBlueBrush = new SolidBrush(lightBlue);

            foreach (Point square in middleSquares)
            {

                e.Graphics.FillRectangle(lightBlueBrush, square.X, square.Y, pixelSize, pixelSize);
            }

            // Draw the absolute middle square
            Pen middleSquarePen = PenCache.GetPen(Color.FromArgb(255, Color.Black));
            e.Graphics.DrawRectangle(middleSquarePen, middleMiddleSquares.X, middleMiddleSquares.Y, pixelSize, pixelSize);
        }
    }
}
