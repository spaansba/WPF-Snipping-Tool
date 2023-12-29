using Snipping_Tool_V4.Modules;
using System.Windows.Forms;
using static Snipping_Tool_V4.Modules.UserformMotions;

namespace Snipping_Tool_V4.Forms
{

    public partial class ScreenshotForm : Form
    {
        // set by timer
        private int screenshotDelay = 0;

        // Size of the main userform Consts
        private const int sideBarWidth = 90;
        private const int topBarHeigth = 60;

        private Form backGroundForm;

        // Create all the moving objects and their opened and closed values (widht or height are interchangable, we are only expanding in 1 direction)
        private UserformMotions timerMotion;
        private UserformMotions currentMovingObject;
        private UserformMotions currentMovingObject2 = null;

        // For on button dropdown boxes
        private const string triangleDownUnicode = "▼";
        private const string triangleUpUnicode = "▲";

        public ScreenshotForm()
        {
            InitializeComponent();
            timerMotion = new UserformMotions(176, 43, false, "Timer", 15, this.timerFlowPanel, false);

            timerButton.Text = string.Format("         &Timer ({0})  {1}", screenshotDelay, triangleDownUnicode);
        }

        #region Create New Screenshot
        private void newScreenshotButton_Click(object sender, EventArgs e)
        {
            MdiParent.Visible = false;
            Task.Delay(screenshotDelay * 1000).Wait();

            // Get the bounds of all the screens
            using (Graphics graphics = Graphics.FromImage(printScreen.entireScreen))
            {
                // Capture the screen
                graphics.CopyFromScreen(printScreen.screenLeft, printScreen.screenTop, 0, 0, printScreen.entireScreen.Size);
            }

            // Create the background form with a picture of the current background
            backGroundForm = new BackGroundForm(this);
            backGroundForm.Location = new Point(printScreen.screenLeft, printScreen.screenTop);
            backGroundForm.Size = new Size(printScreen.screenWidth, printScreen.screenHeight);
            backGroundForm.BackgroundImage = (Image)printScreen.entireScreen.Clone();
            backGroundForm.Show();
            MdiParent.Visible = true;
        }
        public void UpdateScreenshotImage(Image image)
        {
            if (image.Width > this.Width) { this.Width = image.Width; }
            if (image.Height > this.Height) { this.Height = image.Height; }

            screenshotResultPicture.Image = image;

            screenshotPanel.Location = new Point(sideBarWidth, topBarHeigth);
            screenshotPanel.Width = (this.Width - sideBarWidth) - 20;
            screenshotPanel.Height = (this.Height - topBarHeigth) - 40;
        }
        #endregion
        #region Moving Objects (own dropdown boxes)
        private void timerButton_Click(object sender, EventArgs e)
        {
            currentMovingObject = timerMotion;
            screenShotTimer.Start();
            setScreenShotDelay(screenshotDelay, true);
        }
        private void screenShotTimer_Tick(object sender, EventArgs e)
        {
            if (!currentMovingObject.endOfMotion)
            {
                MotionChange(currentMovingObject);
            }
            bool object2done = false;

            if (currentMovingObject2 != null)
            {
                if (currentMovingObject2.endOfMotion)
                {
                    currentMovingObject2.endOfMotion = false;
                    currentMovingObject2 = null;
                    object2done = true;
                }
                else
                {
                    MotionChange(currentMovingObject2);
                }
            }
            else
            {
                object2done = true;
            }

            if (currentMovingObject.endOfMotion && object2done == true)
            {
                currentMovingObject.endOfMotion = false;
                screenShotTimer.Stop();
                currentMovingObject = null;
            }
        }
        #endregion
        #region Set Screenshot Delay
        private void arrowInTimerTextDirection()
        {

        }
        private void setScreenShotDelay(int delay, bool enable)
        {
            string triangleArrow = timerMotion.expanded ? triangleDownUnicode : triangleUpUnicode; 
            timerButton.Text = string.Format("         &Timer ({0})  {1}", delay, triangleArrow);

            screenshotDelay = delay;
            currentMovingObject = timerMotion;
            screenShotTimer.Start();

            if (enable)
            {
                this.Click += OutsideClickHandler;
            }
            else
            {
                this.Click -= OutsideClickHandler;
            }
        }
        private void OutsideClickHandler(object sender, EventArgs e)
        {
            // Check if the click occurred outside the custom dropdown panel
            Point point = this.PointToClient(Cursor.Position);
            if (!timerFlowPanel.Bounds.Contains(point))
            {
                // If the click is outside the panel, close the dropdown
                currentMovingObject = timerMotion;
                screenShotTimer.Start();
                timerButton.Text = string.Format("         &Timer ({0})  {1}", screenshotDelay, triangleDownUnicode);
                this.Click -= OutsideClickHandler; // Remove the click event handler
            }
        }
        private void timerNoTimer_Click(object sender, EventArgs e)
        {
            setScreenShotDelay(0, false);
        }
        private void timer1_Click(object sender, EventArgs e)
        {
            setScreenShotDelay(1, false);
        }
        private void timer2_Click(object sender, EventArgs e)
        {
            setScreenShotDelay(2, false);
        }
        private void timer3_Click(object sender, EventArgs e)
        {
            setScreenShotDelay(3, false);
        }
        private void timer4_Click(object sender, EventArgs e)
        {
            setScreenShotDelay(4, false);
        }
        private void timer5_Click(object sender, EventArgs e)
        {
            setScreenShotDelay(5, false);
        }
        #endregion
    }
}
