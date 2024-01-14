using Snipping_Tool_V4.Modules;
using Snipping_Tool_V4.Screenshots;
using Snipping_Tool_V4.Screenshots.Modules;
using System.Drawing.Drawing2D;

namespace Snipping_Tool_V4.Forms
{
    public partial class TakingScreenShot : Form
    {
        private Point startPoint;
        private Point currentPoint;
        private bool mousePressed = false;
        private Bitmap bufferedBitmap; // Buffer for drawing
        private Bitmap originalBitmap;
        private ScreenshotForm screenshotForm;
        private Rectangle choosenScreenshot;
        private ScreenshotMode screenshotMode;
        PreviewEllipse previewEllipse;
        Rectangle hoveredWindowRect;

        private WindowInformation windowInformation = new WindowInformation();
        private UserScreenInformation userScreenInformation = new UserScreenInformation();
        private ChosenScreenshot screenshotInfoWindow;
        private ChosenScreenshot screenshotInfoRect;

        public TakingScreenShot(ScreenshotForm screenShotForm, ScreenshotMode mode)
        {
            InitializeComponent();
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            screenshotForm = screenShotForm;
            screenshotMode = mode;
            DoubleBuffered = true;
            screenshotInfoRect = new ChosenScreenshot(); // We initiate it now otherwise the picture ends up with the pen still on it.
        }
        #region mouse move event
        private void BackGroundForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (mousePressed)
            {
                screenshotMode = ScreenshotMode.Rectangle;
                currentPoint = e.Location;
                Refresh(); // refresh calls the paint event
            }
            else // if mouse is not pressed show the current top window where the mouse is on
            {
                screenshotMode = ScreenshotMode.Window;
                int tempPreviousWindow = windowInformation.currentWindowTupleOnTop;
                windowInformation.TopWindowUnderMouse(Cursor.Position, this);

                // Only update the rectangle on the window if there is a new top window found (and if there is already paint)
                if (tempPreviousWindow != windowInformation.currentWindowTupleOnTop)
                {
                    highlightTopMostWindow();
                }
            }
        }
        #endregion
        #region actually taking the screenshot either window or rectangle
        private void BackGroundForm_MouseUp(object sender, MouseEventArgs e)
        {
            // Only run if BOTH mouse buttons are not pressed
            if (!AreAnyMouseButtonsPressed())
            {

                Refresh();
                mousePressed = false;

                // Create new points showing the true outline of the picture
                Point topLeftPicture = new Point(Math.Min(startPoint.X, currentPoint.X), Math.Min(startPoint.Y, currentPoint.Y));
                Point bottomRightPicture = new Point(Math.Max(startPoint.X, currentPoint.X), Math.Max(startPoint.Y, currentPoint.Y));

                // Get the image from the user
                if (topLeftPicture == bottomRightPicture) // means user selected a window instead of making their own snippet
                {

                    int indexTopWindow = windowInformation.currentWindowTupleOnTop;
                    var selectedTopWindow = windowInformation.VisibleParentWindows[indexTopWindow];

                    // Bring the choosen window to the front THEN take the screenshot
                    windowInformation.BringWindowToFront(selectedTopWindow.Handle, selectedTopWindow.Title);
                    screenshotInfoWindow = new ChosenScreenshot(selectedTopWindow.RectangleLocation.TopLeft, selectedTopWindow.RectangleLocation.BottomRight);
                    screenshotInfoWindow.screenshot = getImageFromFullScreen(screenshotInfoWindow.thisPictureScreenInfo.entireScreen, screenshotInfoWindow);
                    windowInformation.BringWindowToFront(screenshotForm.mainForm.Handle);
                    screenshotForm.UpdateScreenshotImage(screenshotInfoWindow);
                }
                // if X or Y are the same it means that their is no selection but a straight line, skip it
                else if (topLeftPicture.X != bottomRightPicture.X && topLeftPicture.Y != bottomRightPicture.Y)
                {
                    screenshotInfoRect.setInfo(topLeftPicture, bottomRightPicture);
                    screenshotInfoRect.screenshot = getImageFromFullScreen(screenshotInfoRect.thisPictureScreenInfo.entireScreen, screenshotInfoRect);
                    screenshotForm.UpdateScreenshotImage(screenshotInfoRect);
                }
            }
        }

        /// <summary>
        /// This method gets the original points of the user and returns the actual screenshot
        /// </summary>
        private Image getImageFromFullScreen(Image fullScreenPicture, ChosenScreenshot shot)
        {
            Bitmap croppedBitmap = new Bitmap(shot.Width, shot.Height);

            using (Graphics g = Graphics.FromImage(croppedBitmap))
            {
                // Define the portion to be drawn from the full screen picture
                Rectangle destRect = new(0, 0, shot.Width, shot.Height);

                // Draw the portion of the original image onto the cropped bitmap
                g.DrawImage(fullScreenPicture, destRect, shot.screenshotRectangle, GraphicsUnit.Pixel);
            }
            return croppedBitmap;
        }
        #endregion
        #region User Selecting Window
        private void highlightTopMostWindow()
        {
            var topWindowTuple = windowInformation.VisibleParentWindows[windowInformation.currentWindowTupleOnTop];
            var r = topWindowTuple.RectangleLocation;
            int height = r.BottomRight.X - r.TopLeft.X;
            int width = r.BottomRight.Y - r.TopLeft.Y;
            hoveredWindowRect = new Rectangle(r.TopLeft.X, r.TopLeft.Y, height, width);

            Refresh(); // Calls the paint event and paints the hoveredWindowRect

            Point cursorPos = this.PointToClient(Cursor.Position);
            topWindowLabel(topWindowTuple.Title);
        }

        /// <summary>
        /// Creates a label on the top right of the screen highlighting the top window title
        /// </summary>
        private void topWindowLabel(string labelText)
        {
            removeTopWindowLabel();
            Label windowLabel = new Label
            {
                Tag = "Top Window Label",
                AutoSize = true,
                Padding = new Padding(5, 5, 5, 5),
                Font = new Font("Microsoft Sans Serif", 10),
                Text = labelText,
                Location = new Point(20, 20)
            };
            Controls.Add(windowLabel);
            windowLabel.BringToFront();
        }

        private void removeTopWindowLabel()
        {
            foreach (Control c in Controls)
            {
                if (c.Tag != null && c.Tag.ToString() == "Top Window Label")
                {
                    Controls.Remove(c);
                    break;
                }
            }
        }

        #endregion
        #region User Creating Rectangle
        private void BackGroundForm_MouseDown(object sender, MouseEventArgs e)
        {
            screenshotForm.mainForm.Hide(); // to avoid flickering when turning it back on
            removeTopWindowLabel();
            mousePressed = true;
            startPoint = e.Location;
            currentPoint = e.Location;
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

            Pen rectPen = PenCache.GetPen(Color.Green, 2);

            if (mousePressed && startPoint != currentPoint && screenshotMode == ScreenshotMode.Rectangle)
            {
                choosenScreenshot = new Rectangle(
                    Math.Min(startPoint.X, currentPoint.X),
                    Math.Min(startPoint.Y, currentPoint.Y),
                    Math.Abs(startPoint.X - currentPoint.X),
                    Math.Abs(startPoint.Y - currentPoint.Y));

                // Draw the untransparent part of the original image within the selected rectangle
                e.Graphics.DrawImage(originalBitmap, choosenScreenshot, choosenScreenshot, GraphicsUnit.Pixel);

                e.Graphics.DrawRectangle(rectPen, choosenScreenshot);

                previewEllipse = new(currentPoint, originalBitmap, userScreenInformation);
                previewEllipse.drawPreviewEllipse(e);

            }
            else
            {
                e.Graphics.DrawRectangle(rectPen, choosenScreenshot);
            }

            if (screenshotMode == ScreenshotMode.Window)
            {
                e.Graphics.DrawRectangle(rectPen, hoveredWindowRect);

            }
;

        }
        #endregion
        #region Remaining Events/methods
        /// <summary>
        /// Colors the background image of the form so it is a bit transparant
        /// </summary>
        private void BackGroundForm_Load(object sender, EventArgs e)
        {
            if (this.BackgroundImage != null)
            {
                originalBitmap = new Bitmap(this.BackgroundImage);
                bufferedBitmap = new Bitmap(this.BackgroundImage);

                using (Graphics g = Graphics.FromImage(bufferedBitmap))
                {
                    // Create a semi-transparent brush and paint over the background image
                    using (SolidBrush brush = new SolidBrush(Color.FromArgb(50, Color.White)))
                    {
                        g.FillRectangle(brush, 0, 0, bufferedBitmap.Width, bufferedBitmap.Height);
                    }
                }
                this.BackgroundImage = bufferedBitmap;
            }
        }
        static bool AreAnyMouseButtonsPressed()
        {
            // Get the current state of mouse buttons
            MouseButtons buttons = Control.MouseButtons;

            // Check if any of the buttons are pressed
            return buttons != MouseButtons.None;
        }
        #endregion
    }
}