using Snipping_Tool_V4.Forms;
using Snipping_Tool_V4.Main;
using Snipping_Tool_V4.Modules;
using System.Reflection;
using static Snipping_Tool_V4.Modules.UserformMotions;

namespace Snipping_Tool_V4
{
    public partial class MainForm : Form
    {

        // Create all the moving objects and their opened and closed values (widht or height are interchangable, we are only expanding in 1 direction)
        private UserformMotions sidePanel;
        private UserformMotions sidePanelFirstMenu;
        private UserformMotions currentMovingObject = new UserformMotions();
        private UserformMotions currentMovingObject2 = null; //Sometimes we have to move 2 objects at ones

        // Forms
        private SettingsForm settings;
        private ScreenshotForm screenshot;

        public MainForm()
        {
            InitializeComponent();
            mdiProp();

            sidePanel = new UserformMotions((int)MainFormMeasurements.formSidePanelOpenWidth, (int)MainFormMeasurements.formSidePanelClosedWidth, true, "SidePanel", 15, this.sidebarFlowPanel, true, "Main");
            sidePanelFirstMenu = new UserformMotions(136, 43, false, "SidePanelFirstMenu", 20, this.menuFlowPanel, false, "Main", this.menuButton);

            EnableDoubleBufferingForControls(this);

            screenshot = new ScreenshotForm(this);
            ChildFormProperties(screenshot);
        }

        /// <summary>
        /// e.g. after taking a screenshot we want to reset the form to its original state
        /// </summary>
        public void resetFormState()
        {
            Width = (int)MainFormMeasurements.formWidth;
            Height = (int)MainFormMeasurements.formHeight;
            WindowState = FormWindowState.Normal;
            FormBorderStyle = FormBorderStyle.Sizable;
            Visible = true;
            sidebarFlowPanel.Width = (int)MainFormMeasurements.formSidePanelClosedWidth;
            menuHideExpandButton.Enabled = true;
            BringToFront();
        }

        #region Userform settings
        private void mdiProp()
        {
            this.SetBevel(false);
            Controls.OfType<MdiClient>().FirstOrDefault().BackColor = Color.FromArgb(232, 234, 237);
        }

        /// <summary>
        /// Activates double buffering for panels and buttons, it should reduce flickering on motion effects but not actually sure if it does.
        /// </summary>
        private static void EnableDoubleBufferingForControls(Control control)
        {
            foreach (Control ctrl in control.Controls)
            {
                if (ctrl is Panel panel)
                {
                    typeof(Panel).InvokeMember("DoubleBuffered", BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic, null, panel, new object[] { true });
                }
                else if (ctrl is ButtonBase button)
                {
                    typeof(ButtonBase).InvokeMember("DoubleBuffered", BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic, null, button, new object[] { true });
                }

                // If the control has nested controls, recursively enable double buffering
                if (ctrl.HasChildren)
                {
                    EnableDoubleBufferingForControls(ctrl);
                }
            }
        }
        #endregion
        #region Moving panels
        private void menuHideExpandButton_Click(object sender, EventArgs e)
        {
            if (sidePanelFirstMenu.expanded) // Close all submenu's if we minimalize the menu bar
            {
                currentMovingObject2 = sidePanelFirstMenu;
            }
            currentMovingObject = sidePanel;
            objectMotionTimer.Start();
        }

        private void menuButton_Click(object sender, EventArgs e)
        {
            if (!sidePanel.expanded) // for if sidebar is closed, open it to show submenu's
            {
                currentMovingObject2 = sidePanel;
            }
            currentMovingObject = sidePanelFirstMenu;
            objectMotionTimer.Start();
        }
        private void objectMotionTimer_Tick(object sender, EventArgs e)
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
                objectMotionTimer.Stop();
                currentMovingObject = null;
            }
        }
        #endregion
        #region Loading the mdiForms within the main form
        private void screenshotButton_Click(object sender, EventArgs e)
        {
            if (screenshot == null)
            {
                screenshot = new ScreenshotForm(this);
                ChildFormProperties(screenshot);
            }
            else
            {
                screenshot.Activate();
            }
        }
        private void settingsSidebarButton_Click(object sender, EventArgs e)
        {
            if (settings == null)
            {
                settings = new SettingsForm(this);
                ChildFormProperties(settings);
            }
            else
            {
                settings.Activate();
            }

        }
        private void ChildFormProperties(baseChildFormsTemplate currentform)
        {
            //currentform.FormClosed += generalCloseEvent_FormClosed;
            currentform.MdiParent = this;
            currentform.BackColor = Color.White;
            currentform.Dock = DockStyle.Fill;
            currentform.Show();
        }

        #endregion
    }
}
