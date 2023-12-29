using Snipping_Tool_V4.Modules;
using System.Drawing.Drawing2D;

namespace Snipping_Tool_V4.Forms
{
    public partial class BackGroundForm : Form
    {
        private Point startPoint;
        private Point currentPoint;
        private bool mousePressed = false;
        private Bitmap bufferedImage; // Buffer for drawing
        private Bitmap originalImage;
        private ScreenshotForm screenshot;
        private Rectangle choosenScreenshot;

        // Standard ellipse size consts/attributes
        private const int sizeCircle = 130;
        private const int avrgSizeOfTaskBar = 40;
        private int pixelsFromMouseWidth = 14;
        private int pixelsFromMouseHeight = 14;

        public BackGroundForm(ScreenshotForm screenshotForm)
        {
            InitializeComponent();
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            screenshot = screenshotForm;
        }

        private void BackGroundForm_MouseDown(object sender, MouseEventArgs e)
        {
            mousePressed = true;
            startPoint = e.Location;
            currentPoint = e.Location;
        }
        private void BackGroundForm_MouseUp(object sender, MouseEventArgs e)
        {
            // Only run if BOTH mouse buttons are not pressed
            if (!AreAnyMouseButtonsPressed())
            {
                Refresh();
                mousePressed = false;

                // Create new points showing the true outline of the picture
                Point topLeftPicture = new Point(0, 0);
                Point bottomRightPicture = new Point(0, 0);
                topLeftPicture.X = Math.Min(startPoint.X, currentPoint.X);
                topLeftPicture.Y = Math.Min(startPoint.Y, currentPoint.Y);
                bottomRightPicture.X = Math.Max(startPoint.X, currentPoint.X);
                bottomRightPicture.Y = Math.Max(startPoint.Y, currentPoint.Y);    

                Image croppedImage = CropImage(printScreen.entireScreen, topLeftPicture, bottomRightPicture);

                // Sending the cropped image back to the MainForm and close this form
                screenshot.Invoke((MethodInvoker)delegate
                {
                    screenshot.UpdateScreenshotImage(croppedImage);
                });

                this.Close();
            }
        }

        private Image CropImage(Image originalImage, Point topLeftPicture, Point bottomRightPicture)
        {
            // Calculate the rectangle dimensions
            int width = bottomRightPicture.X - topLeftPicture.X;
            int height = bottomRightPicture.Y - topLeftPicture.Y;

            // Create a new bitmap with the specified size
            Bitmap croppedBitmap = new Bitmap(width, height);

            // Create a graphics object from the cropped bitmap
            using (Graphics g = Graphics.FromImage(croppedBitmap))
            {
                // Define the portion to be drawn from the original image
                Rectangle sourceRect = new Rectangle(topLeftPicture.X, topLeftPicture.Y, width, height);
                Rectangle destRect = new Rectangle(0, 0, width, height);

                // Draw the portion of the original image onto the cropped bitmap
                g.DrawImage(originalImage, destRect, sourceRect, GraphicsUnit.Pixel);
            }

            return croppedBitmap; // Return the cropped image
        }

        private void BackGroundForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (mousePressed)
            {
                currentPoint = e.Location;
                // refresh calls the paint event
                Refresh();
            }
        }
        /// <summary>
        /// Draws a rectangle on the userform according to the users mouse
        /// Undo the previously done background transparancy in the selected rectangle
        /// </summary>
        private void BackGroundForm_Paint(object sender, PaintEventArgs e)
        {
            // for better quality picture in zoom in
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

            choosenScreenshot = new Rectangle(
            Math.Min(startPoint.X, currentPoint.X),
            Math.Min(startPoint.Y, currentPoint.Y),
            Math.Abs(startPoint.X - currentPoint.X),
            Math.Abs(startPoint.Y - currentPoint.Y));

            if (mousePressed && startPoint != currentPoint)
            {
                // Draw the untransparent part of the original image within the selected rectangle
                e.Graphics.DrawImage(originalImage, choosenScreenshot, choosenScreenshot, GraphicsUnit.Pixel);

                using (Pen rectPen = new Pen(Color.Green, 2))
                {
                    e.Graphics.DrawRectangle(rectPen, choosenScreenshot);
                }

                // Draw an preview Elipse
                using (Pen elipsePen = new Pen(Color.White, 2))
                {
                    Color pixelColor = originalImage.GetPixel(currentPoint.X, currentPoint.Y);

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

                    // ellipseBounds is the rectangle around the previewCircle
                    Rectangle ellipseBounds = new Rectangle(currentPoint.X + pixelsFromMouseWidth, currentPoint.Y + pixelsFromMouseHeight, sizeCircle, sizeCircle);

                    for (int i = 0; i < ellipsePoints; i++)
                    {
                        float angle = 360f / ellipsePoints * i;
                        float x = 130 / 2 + (ellipseBounds.X + ellipseBounds.Width / 2 * (float)Math.Cos(angle * Math.PI / 180));
                        float y = 130 / 2 + (ellipseBounds.Y + ellipseBounds.Height / 2 * (float)Math.Sin(angle * Math.PI / 180));
                        circlePoints[i] = new PointF(x, y);
                    }

                    e.Graphics.DrawLines(elipsePen, circlePoints);

                    //// set clip to the ellipse, means that new drawings cant clip over the outside of the ellipse
                    GraphicsPath ellipsePath = new GraphicsPath();
                    ellipsePath.AddEllipse(ellipseBounds);
                    e.Graphics.SetClip(ellipsePath);

                    // Calculate the zoomed rectangle based on the mouse position
                    int ZoomFactor = 4;
                    Rectangle zoomedRect = new Rectangle(
                        currentPoint.X - (sizeCircle / (2 * ZoomFactor)),
                        currentPoint.Y - (sizeCircle / (2 * ZoomFactor)),
                        sizeCircle / ZoomFactor,
                        sizeCircle / ZoomFactor);

                    // Draw zoomed in pic inside the ellipse
                    e.Graphics.DrawImage(originalImage, ellipseBounds, zoomedRect, GraphicsUnit.Pixel);

                    //Draw a roster inside the ellipse
                    DrawPreviewRoster(e);

                    //Draw the outline circle (again) at the end so there are no rough outlines
                    e.Graphics.DrawLines(elipsePen, circlePoints);
                    // Reset the clip
                    e.Graphics.ResetClip();
                }
            }
            else
            {
                using (Pen pen = new Pen(Color.Green, 2))
                {
                    e.Graphics.DrawRectangle(pen, choosenScreenshot);
                }
            }
        }
        /// <summary>
        /// Creates a rectangle with a black roster for inside the ellipse
        /// the middle of the rectangle will be ligtblue
        /// </summary>
        /// <param name="e">paintevents from the userform paint event</param>
        private void DrawPreviewRoster(PaintEventArgs e)
        {
            using (Pen squarePen = new Pen(Color.FromArgb(60, Color.Black), 1))
            {
                int pixelSize = 9; // Define the size of the black squares

                // Calculate the number of squares to draw around the ellipse
                int numSquaresPerAxes = pixelSize * 2;

                // Calculate the coordinates of the top-left corner of each square
                int startX = currentPoint.X + pixelsFromMouseWidth - (pixelSize / 2);
                int startY = currentPoint.Y + pixelsFromMouseHeight - (pixelSize / 2);

                List<Point> middleSquares = new List<Point>();
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

                // Define the very light blue color with transparency
                Color lightBlue = Color.FromArgb(40, 0, 100, 255); // Adjust the RGB values as needed

                // Create a SolidBrush with the defined color
                SolidBrush lightBlueBrush = new SolidBrush(lightBlue);

                foreach (Point square in middleSquares)
                {

                    e.Graphics.FillRectangle(lightBlueBrush, square.X, square.Y, pixelSize, pixelSize);
                }

                squarePen.Color = Color.FromArgb(255, Color.Black); // 
                e.Graphics.DrawRectangle(squarePen, middleMiddleSquares.X, middleMiddleSquares.Y, pixelSize, pixelSize);
            }
        }

        /// <summary>
        /// Colors the background image of the form so it is a bit transparant
        /// </summary>
        private void BackGroundForm_Load(object sender, EventArgs e)
        {
            if (this.BackgroundImage != null)
            {
                originalImage = new Bitmap(this.BackgroundImage);
                bufferedImage = new Bitmap(this.BackgroundImage);

                using (Graphics g = Graphics.FromImage(bufferedImage))
                {
                    // Create a semi-transparent brush and paint over the background image
                    using (SolidBrush brush = new SolidBrush(Color.FromArgb(50, Color.White)))
                    {
                        g.FillRectangle(brush, 0, 0, bufferedImage.Width, bufferedImage.Height);
                    }
                }
                this.BackgroundImage = bufferedImage;
            }
        }
        static bool AreAnyMouseButtonsPressed()
        {
            // Get the current state of mouse buttons
            MouseButtons buttons = Control.MouseButtons;

            // Check if any of the buttons are pressed
            return buttons != MouseButtons.None;
        }
    }
}