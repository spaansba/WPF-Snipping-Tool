﻿using Snipping_Tool_V4.Main;
using Snipping_Tool_V4.Modules;
using Snipping_Tool_V4.Screenshots;
using Snipping_Tool_V4.Screenshots.Modules;
using Snipping_Tool_V4.Screenshots.Modules.Drawing;
using Snipping_Tool_V4.Screenshots.Modules.Drawing.Tools;
using Snipping_Tool_V4.Screenshots.Modules.Screen_Screenshot;
using System.Drawing.Drawing2D;
using static Snipping_Tool_V4.Modules.UserformMotions;

namespace Snipping_Tool_V4.Forms
{
    public partial class ScreenshotForm : baseChildFormsTemplate
    {
        // TODO: Make it so when one of the selfmade dropdowns is open that on next click anywhere the dropdown closes
        // TODO: Create hover effect for the tools/dropdowns

        private Form takingScreenshotForm = new Form();
        public MainForm mainForm;

        // Create all the moving objects and their opened and closed values (widht or height are interchangable, we are only expanding in 1 direction)
        private UserformMotions timerMotion;
        private UserformMotions currentMovingObject = null;

        // To check if we are currently taking a screenshot 
        public bool takingScreenshot = false;
        private ScreenshotTimer screenshotDelay = 0;
        private const int timerMotionMenuClosed = 50;
        private const int timerMotionMenuOpen = 186;
        private const int pictureModeWidth = 350;

        //
        private readonly Size symbolPanelOpenSize = new Size(117, 103);
        private readonly Size symbolPanelClosedSize = new Size(117, 26);
        private bool symbolPanelOpen = false;

        // Mode options
        private ScreenshotMode screenShotMode;

        private UserScreenInformation screenInfo = new UserScreenInformation();

        //For image drawing (we call erasing / shapes also "drawing" for simplicity)
        private ScreenshotDrawingViewModel viewModel = new ScreenshotDrawingViewModel();

        public ScreenshotForm(MainForm mainform)
        {
            InitializeComponent();

            List<Button> timerChildButtons = new List<Button> { timerNoTimer, timer1, timer2, timer3, timer4, timer5 };
            timerMotion = new UserformMotions(timerMotionMenuOpen, timerMotionMenuClosed, false, "Timer", 15, this.timerFlowPanel, false, "Screenshot", this.timerButton, timerChildButtons);
            mainForm = mainform;

            this.viewModel.DrawingChanged += (_, _) => this.screenshotResultPicture.Invalidate();

            screenshotResultPicture.Paint += (_, e) =>
            {
                if (screenshotResultPicture.Image != null)
                {
                    e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    this.viewModel.Draw(e.Graphics);
                }
            };

            screenshotResultPicture.MouseDown += (_, e) =>
            {
                if (screenshotResultPicture.Image == null)
                {
                    return;
                }

                switch (e.Button, viewModel.CurrentTool)
                {
                    case (MouseButtons.Left, not null):
                        viewModel.Begin(e.Location);
                        break;
                    case (MouseButtons.Right, not null):
                        viewModel.Reset();
                        screenshotResultPicture.Invalidate();
                        break;
                    case (MouseButtons.Right, null):
                        imageContextMenu imageContextMenu2 = new imageContextMenu(screenshotResultPicture);
                        imageContextMenu2.ShowContextMenu(e.Location);
                        break;
                }
            };

            screenshotResultPicture.MouseMove += (_, e) =>
            {
                if (e.Button == MouseButtons.Left && screenshotResultPicture.Image != null)
                {
                    viewModel.Continue(e.Location);
                }
            };

            screenshotResultPicture.MouseUp += (_, e) =>
            {
                if (screenshotResultPicture.Image != null && viewModel.CurrentTool.IsActive)
                {
                    viewModel.Finish(e.Location);
                }
            };

            // Set the default tool to freehand/ 4/ black
            // TODO: Activate the radiobuttons ones we made it
            viewModel.CurrentTool = Tools.Freehand;

            //Create RadioButtons for the drawing tools
            CreateDrawingToolButtons();
        }

        #region Create New Screenshot
        private async void newScreenshotButton_Click(object sender, EventArgs e)
        {
            viewModel.Clear(); // Clear all user drawings
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
            mainForm.Height = topBarLabel.Height + 10 + (int)MainFormMeasurements.titleBarHeight;
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
            if (viewModel.CurrentTool == null)
            {
                return;
            }

            if (e.Control && e.KeyCode == Keys.Z)
            {
                viewModel.Undo();
            }
            else if (e.KeyCode == Keys.ShiftKey)
            {
                viewModel.SetShiftPressed(true);
            }
        }
        private void ScreenshotForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ShiftKey && viewModel.CurrentTool != null)
            {
                viewModel.SetShiftPressed(false);
            }
        }


        #endregion

        private void CreateDrawingToolButtons()
        {
            TableLayoutPanel shapePanel = this.shapesTablePanel;
            int ButtonWidth = shapePanel.Width / 4; //TODO: Make rows/columns count dynamic
            int buttonHeight = shapePanel.Height / 4;

            Size buttonSize = new Size(ButtonWidth, buttonHeight);
            foreach (var tool in Tools.ShapeTools)
            {
                PictureBoxButton<Tool> toolButton = new ToolButton(tool, buttonSize, viewModel);
                shapePanel.Controls.Add(toolButton);
            }

            TableLayoutPanel specialToolsPanel = specialToolsTablePanel;
            ButtonWidth = specialToolsPanel.Width / 2; //TODO: Make rows/columns count dynamic
            buttonHeight = specialToolsPanel.Height / 2;

            buttonSize = new Size(ButtonWidth, buttonHeight);
            foreach (var tool in Tools.SpecialTools)
            {
                PictureBoxButton<Tool> toolButton = new ToolButton(tool, buttonSize, viewModel);
                specialToolsPanel.Controls.Add(toolButton);
            }

            TableLayoutPanel colorPickerPanel = colorPickerTablePanel;
            ButtonWidth = colorPickerPanel.Width / 2; //TODO: Make rows/columns count dynamic
            buttonHeight = colorPickerPanel.Height;

            PictureBoxButton<Color> colorStrokeButton = new ColorSelectionStrokeButton(viewModel.penColor, buttonSize, viewModel);
            PictureBoxButton<Color> colorFillButton = new ColorSelectionFillButton(viewModel.fillColor, buttonSize, viewModel);
            colorPickerPanel.Controls.Add(colorStrokeButton);
            colorPickerPanel.Controls.Add(colorFillButton);

            CustomDropdownBox customDropdownBox = new ShapeFillDropdown();
            symbolOptionsTablePanel.Controls.Add(customDropdownBox);
        }

        private void size3Button_Click(object sender, EventArgs e)
        {
            viewModel.PenThickness = 3;
        }

        private void size10Button_Click(object sender, EventArgs e)
        {
            viewModel.PenThickness = 10;
        }

        private void expandSymbolsButton_Click(object sender, EventArgs e)
        {
            if (symbolPanelOpen)
            {
                symbolMainPanel.Size = symbolPanelClosedSize;
                symbolPanelOpen = false;
            }
            else
            {
                symbolMainPanel.Size = symbolPanelOpenSize;
                symbolPanelOpen = true;
            }
        }

        private void screenshotPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void topBarLabel_Click(object sender, EventArgs e)
        {

        }
    }
}
