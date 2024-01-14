using Snipping_Tool_V4.Forms;
using Snipping_Tool_V4.Main;

namespace Snipping_Tool_V4.Screenshots.Modules
{
    internal class DisplayingScreenshot
    {
        private ChosenScreenshot screenshotInfo;
        private ScreenshotForm displayForm;
        private MainForm mainForm;
        private const int whiteBorderAroundScreenshotSize = 80; // = 40 on each side

        public DisplayingScreenshot(ChosenScreenshot screenshotInformation, ScreenshotForm form)
        {
            screenshotInfo = screenshotInformation;
            displayForm = form;
            mainForm = displayForm.mainForm;
        }

        public void displayScreenshot()
        {
            int forcedFormPadding = 40; // -X to not fill the entire screen with the form

            // Save a reference to the old image
            Image oldImage = screenshotInfo.screenshot;

            // Add a white border around the screenshot
            Bitmap borderedImage = new Bitmap(screenshotInfo.screenshot.Width + whiteBorderAroundScreenshotSize, screenshotInfo.screenshot.Height + whiteBorderAroundScreenshotSize);
            using (Graphics g = Graphics.FromImage(borderedImage))
            {
                g.Clear(Color.White);
                g.DrawImage(screenshotInfo.screenshot, new Point(whiteBorderAroundScreenshotSize / 2, whiteBorderAroundScreenshotSize / 2));
            }

            // Resize the form to the image or to the max size
            // (basically only create scrolling bars on the panel if the size is too big, otherwise resize the form to fit the picture)
            int formSidePanelClosedWidth = (int)MainFormMeasurements.formSidePanelClosedWidth;
            int titleBarHeight = (int)MainFormMeasurements.titleBarHeight;
            int maxFormWidth = screenshotInfo.thisPictureScreenInfo.getCurrentScreenWidth(Cursor.Position) - forcedFormPadding;
            int maxFormHeight = screenshotInfo.thisPictureScreenInfo.getCurrentScreenHeight(Cursor.Position) - forcedFormPadding;

            if (borderedImage.Width > (mainForm.Width - formSidePanelClosedWidth))
            {
                mainForm.Width = borderedImage.Width > maxFormWidth ? maxFormWidth : (borderedImage.Width + formSidePanelClosedWidth + forcedFormPadding);
            }

            if (borderedImage.Height > (mainForm.Height - titleBarHeight - 20))
            {
                mainForm.Height = borderedImage.Height > maxFormHeight ? maxFormHeight : (borderedImage.Height + titleBarHeight + 20 + forcedFormPadding);
            }

            displayForm.screenshotResultPicture.Height = borderedImage.Height;
            displayForm.screenshotResultPicture.Width = borderedImage.Width;
            displayForm.screenshotResultPicture.Location = new Point(0, 0);
            displayForm.screenshotResultPicture.Image = borderedImage;

            displayForm.takingScreenshot = false;

            Point newLocation = calculateNewMainFormLocation();
            mainForm.Location = newLocation;

        }
        /// <summary>
        /// Calculates the new location of the mainForm, so that the printscreen location perfectly alligns
        /// with the imagebox location in the form (is very smooth)
        /// </summary>
        /// <returns>The location on which the MainForm should reside</returns>
        private Point calculateNewMainFormLocation()
        {
            Point pictureTopLeftScreen = displayForm.screenshotResultPicture.PointToScreen(Point.Empty);
            int offsetX = pictureTopLeftScreen.X - (screenshotInfo.topLeftLocation.X - whiteBorderAroundScreenshotSize / 2);
            int offsetY = pictureTopLeftScreen.Y - (screenshotInfo.topLeftLocation.Y - whiteBorderAroundScreenshotSize / 2);
            int newX = mainForm.Left - offsetX;
            int newY = mainForm.Top - offsetY;

            // If the newY/X are outside of the screen region push it back in
            Rectangle screenBounds = screenshotInfo.thisPictureScreenInfo.totalScreenRectangle;
            int windowsTaskBarOffset = 32;

            if (newX < screenBounds.Left)
            {
                newX = screenBounds.Left;
            }
            else if (newX + mainForm.Width > screenBounds.Right)
            {
                newX = screenBounds.Right - mainForm.Width;
            }

            if (newY < screenBounds.Top)
            {
                newY = screenBounds.Top;
            }
            else if (newY + mainForm.Height > screenBounds.Bottom)
            {
                newY = screenBounds.Bottom - mainForm.Height - windowsTaskBarOffset;
            }

            return new Point(newX, newY);

        }
    }
}
