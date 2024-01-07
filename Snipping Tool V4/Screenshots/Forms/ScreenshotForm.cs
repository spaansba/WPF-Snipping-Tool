using Snipping_Tool_V4.Modules;
using static Snipping_Tool_V4.Modules.UserformMotions;
using Snipping_Tool_V4.Screenshots.Modules;
using Snipping_Tool_V4.Screenshots;
using Snipping_Tool_V4.Main;
using Snipping_Tool_V4.Screenshots.Modules.Drawing;
using System.Drawing;

namespace Snipping_Tool_V4.Forms
{
    public partial class ScreenshotForm : baseChildFormsTemplate
    {
        private Form takingScreenshotForm;
        public MainForm mainForm;

        // Create all the moving objects and their opened and closed values (widht or height are interchangable, we are only expanding in 1 direction)
        private UserformMotions timerMotion;
        private UserformMotions currentMovingObject;

        // To check if we are currently taking a screenshot 
        public bool takingScreenshot = false;
        private ScreenshotTimer screenshotDelay = 0;
        private const int timerMotionMenuClosed = 47;
        private const int timerMotionMenuOpen = 176;
        private const int pictureModeWidth = 350;

        // Mode options
        private ScreenshotMode screenShotMode;


        private UserScreenInformation screenInfo = new UserScreenInformation();

        //For image drawing (we call erasing / shapes also "drawing" for simplicity)
        private ImageDrawingManager drawingManager;


        public ScreenshotForm(MainForm mainform)
        {
            InitializeComponent();

            List<Button> timerChildButtons = new List<Button> { timerNoTimer, timer1, timer2, timer3, timer4, timer5 };
            timerMotion = new UserformMotions(timerMotionMenuOpen, timerMotionMenuClosed, false, "Timer", 15, this.timerFlowPanel, false, "Screenshot", this.timerButton, timerChildButtons);
            mainForm = mainform;

            // Allows user to draw on the screenshot
            drawingManager = new ImageDrawingManager(screenshotResultPicture);

            var table = tableLayoutPanel1(10);
            var tools = Enum.GetValues<DrawingShape>();
            
        }

        private static TableLayoutPanel tableLayoutPanel1(int toolCount)
        {
            var columnCount = 7;
            var (rowcount, remainder) = Math.DivRem(toolCount, columnCount);
            if (remainder > 0)
            {
                rowcount++;
            }
            var table = new TableLayoutPanel();
            for (var i = 0; i < rowcount; i++)
            {
                table.RowStyles.Add(new RowStyle(SizeType.Percent, 100 / rowcount));
            }
            for (var i = 0; i < columnCount; i++)
            {
                table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100 / columnCount));
            }
            table.Location = new Point(50, 50);
            table.BackColor = Color.Red;
            return table;

        }


        #region Create New Screenshot
        private async void newScreenshotButton_Click(object sender, EventArgs e)
        {
            drawingManager.ClearDrawings(); // Clear all user drawings
            // Check if the user wants to create a new screenshot or want to cancel the current screenshot
            if (takingScreenshot)
            {
                resetFormsAfterScreenshot();
                takingScreenshot = false;
            }
            else
            {
                // For every screenshot mode
                closeAllDropdownsExcept();
                formStateTakePicture();
                takingScreenshot = true;

                await Task.Delay(200); //wait for the screen is fully hidden before we take the screenshot

                // Make a screenshot of the current screens
                using (Graphics graphics = Graphics.FromImage(screenInfo.entireScreen))
                {
                    graphics.CopyFromScreen(screenInfo.screenLeft, screenInfo.screenTop, 0, 0, screenInfo.entireScreen.Size);
                }

                // Create the background form with a picture of the current background
                takingScreenshotForm = new TakingScreenShot(this, screenShotMode)
                {
                    Location = new Point(screenInfo.screenLeft, screenInfo.screenTop),
                    Size = new Size(screenInfo.screenWidth, screenInfo.screenHeight),
                    BackgroundImage = (Image)screenInfo.entireScreen.Clone()
                };
                takingScreenshotForm.Show();

                // Change the new screenshot button in a Cancel button
                newScreenshotButton.BackColor = SystemColors.GradientInactiveCaption;
                newScreenshotButton.Text = "         &Cancel";
                mainForm.Visible = true;

            }

        }
        /// <summary>
        /// Screenshot gets added from the BackGroundForm, so has to be public
        /// </summary>
        /// <param name="image">The Screenshot that is being created by the user</param>
        public void UpdateScreenshotImage(ChosenScreenshot screenshotInfo)
        {
            resetFormsAfterScreenshot();
            DisplayingScreenshot displayingScreenshot = new DisplayingScreenshot(screenshotInfo, this);
            displayingScreenshot.displayScreenshot();
            ShowMessage("Screenshot copied to clipboard", this);
        }
        #endregion
        #region Form States
        private void formStateTakePicture()
        {
            // Make the main userform smaller and invisble and att a timer
            mainForm.menuHideExpandButton.Enabled = false;
            screenshotResultPicture.Image = null;
            mainForm.Height = timerButton.Height + (int)MainFormMeasurements.titleBarHeight;
            mainForm.sidebarFlowPanel.Width = 0; // disable side menu bar when taking a picture
            mainForm.Width = pictureModeWidth;
            mainForm.Visible = false;
            mainForm.FormBorderStyle = FormBorderStyle.FixedSingle;
            Task.Delay((int)screenshotDelay * 1000).Wait();
        }
        private void resetFormsAfterScreenshot()
        {
            if (takingScreenshot)
            {
                mainForm.resetFormState();
                newScreenshotButton.BackColor = TopBarColor;
                newScreenshotButton.Text = "       &New";
                takingScreenshot = false;
                takingScreenshotForm.Close();
            }
        }
        #endregion
        #region Moving Objects (own dropdown boxes)
        private void screenShotTimer_Tick(object sender, EventArgs e)
        {
            MotionChange(currentMovingObject);

            if (currentMovingObject.endOfMotion)
            {
                currentMovingObject.endOfMotion = false;
                setTriangleOnButton(currentMovingObject);
                closeAllDropdownsExcept(currentMovingObject);
                screenShotTimer.Stop();
                currentMovingObject = null;
            }
        }
        #endregion
        #region Set Screenshot Delay
        private void timerButton_Click(object sender, EventArgs e)
        {
            if (takingScreenshot)
            {
                mainForm.Height = 1 + timerMotionMenuOpen + 10; // 10 is padding
            }

            currentMovingObject = timerMotion;
            screenShotTimer.Start();
            setScreenShotDelay(screenshotDelay);
            resetFormsAfterScreenshot();
        }
        private void setScreenShotDelay(ScreenshotTimer delay, object sendObject = null)
        {
            screenshotDelay = delay;
            int delayInt = (int)delay;
            changeValueBetweenParentheses(timerMotion.parentButton, delayInt.ToString());
            currentMovingObject = timerMotion;
            screenShotTimer.Start();

            //Color the child button light gray to indicate which one is active at the moment
            if (sendObject != null)
            {
                Button sendButton = sendObject as Button;
                colorActiveMenuItem(timerMotion.childButtons, sendButton);
            }
        }
        private void timerNoTimer_Click(object sender, EventArgs e)
        {
            setScreenShotDelay(ScreenshotTimer.None, sender);
        }
        private void timer1_Click(object sender, EventArgs e)
        {
            setScreenShotDelay(ScreenshotTimer.OneSecond, sender);
        }
        private void timer2_Click(object sender, EventArgs e)
        {
            setScreenShotDelay(ScreenshotTimer.TwoSeconds, sender);
        }
        private void timer3_Click(object sender, EventArgs e)
        {
            setScreenShotDelay(ScreenshotTimer.ThreeSeconds, sender);
        }
        private void timer4_Click(object sender, EventArgs e)
        {
            setScreenShotDelay(ScreenshotTimer.FourSeconds, sender);
        }
        private void timer5_Click(object sender, EventArgs e)
        {
            setScreenShotDelay(ScreenshotTimer.FiveSeconds, sender);
        }
        #endregion
        #region Deactivate Form
        /// <summary>
        /// Reset the userform if another userform gets activated
        /// </summary>
        private void ScreenshotForm_Deactivate(object sender, EventArgs e)
        {
            deactivateForm(this, mainForm, false);
        }
        #endregion

        #region Key and Click events (context menu)
        /// <summary>
        /// if CTRL Z is pressed remove the last drawing on the userform
        /// </summary>
        private void ScreenshotForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.Z)
            {
                drawingManager.controlZOnDrawing();
            }
        }
        #endregion

        private void redLineButton_Click(object sender, EventArgs e)
        {
            drawingManager.chosenColor = Color.Red;
        }

        private void blackLineButton_Click(object sender, EventArgs e)
        {
            drawingManager.chosenColor = Color.Black;
        }

        private void freeHandButton_Click(object sender, EventArgs e)
        {
            drawingManager.chosenShape = DrawingShape.Freehand;
        }

        private void RectangleShapeButton_Click(object sender, EventArgs e)
        {
            drawingManager.CurrentTool = Tools.rectangle;
         //   drawingManager.chosenShape = DrawingShape.Rectangle;
        }

        private void circleButton_Click(object sender, EventArgs e)
        {
            drawingManager.chosenShape = DrawingShape.Circle;
        }
        private void EllipseButton_Click(object sender, EventArgs e)
        {
            drawingManager.chosenShape = DrawingShape.Ellipse;
        }

        private void size3Button_Click(object sender, EventArgs e)
        {
            drawingManager.chosenSize = 3;
        }

        private void size10Button_Click(object sender, EventArgs e)
        {
            drawingManager.chosenSize = 10;
        }
        

    }
}
