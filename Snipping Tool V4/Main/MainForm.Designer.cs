

namespace Snipping_Tool_V4
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            sidebarFlowPanel = new FlowLayoutPanel();
            menuButtonPanel = new Panel();
            divederline = new Label();
            menuHideExpandButton = new Button();
            screenshotsSidebarPanel = new Panel();
            screenshotSidebarButton = new Button();
            colorPickerPanel = new Panel();
            colorPickerSidebarButton = new Button();
            menuFlowPanel = new FlowLayoutPanel();
            menuPanel = new Panel();
            menuButton = new Button();
            panel4 = new Panel();
            button2 = new Button();
            panel5 = new Panel();
            button3 = new Button();
            settingsPanel = new Panel();
            settingsSidebarButton = new Button();
            objectMotionTimer = new System.Windows.Forms.Timer(components);
            sidebarFlowPanel.SuspendLayout();
            menuButtonPanel.SuspendLayout();
            screenshotsSidebarPanel.SuspendLayout();
            colorPickerPanel.SuspendLayout();
            menuFlowPanel.SuspendLayout();
            menuPanel.SuspendLayout();
            panel4.SuspendLayout();
            panel5.SuspendLayout();
            settingsPanel.SuspendLayout();
            SuspendLayout();
            // 
            // sidebarFlowPanel
            // 
            sidebarFlowPanel.BackColor = Color.FromArgb(23, 24, 29);
            sidebarFlowPanel.Controls.Add(menuButtonPanel);
            sidebarFlowPanel.Controls.Add(screenshotsSidebarPanel);
            sidebarFlowPanel.Controls.Add(colorPickerPanel);
            sidebarFlowPanel.Controls.Add(menuFlowPanel);
            sidebarFlowPanel.Controls.Add(settingsPanel);
            sidebarFlowPanel.Dock = DockStyle.Left;
            sidebarFlowPanel.FlowDirection = FlowDirection.TopDown;
            sidebarFlowPanel.Location = new Point(0, 0);
            sidebarFlowPanel.Name = "sidebarFlowPanel";
            sidebarFlowPanel.Padding = new Padding(0, 5, 0, 0);
            sidebarFlowPanel.Size = new Size(147, 465);
            sidebarFlowPanel.TabIndex = 4;
            // 
            // menuButtonPanel
            // 
            menuButtonPanel.BackColor = Color.Transparent;
            menuButtonPanel.Controls.Add(divederline);
            menuButtonPanel.Controls.Add(menuHideExpandButton);
            menuButtonPanel.Location = new Point(3, 8);
            menuButtonPanel.Name = "menuButtonPanel";
            menuButtonPanel.Size = new Size(180, 43);
            menuButtonPanel.TabIndex = 7;
            // 
            // divederline
            // 
            divederline.BorderStyle = BorderStyle.Fixed3D;
            divederline.Location = new Point(-3, 38);
            divederline.Margin = new Padding(0);
            divederline.Name = "divederline";
            divederline.Size = new Size(149, 10);
            divederline.TabIndex = 6;
            // 
            // menuHideExpandButton
            // 
            menuHideExpandButton.BackColor = Color.FromArgb(23, 24, 29);
            menuHideExpandButton.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            menuHideExpandButton.ForeColor = Color.White;
            menuHideExpandButton.Image = (Image)resources.GetObject("menuHideExpandButton.Image");
            menuHideExpandButton.ImageAlign = ContentAlignment.MiddleLeft;
            menuHideExpandButton.Location = new Point(-5, -8);
            menuHideExpandButton.Name = "menuHideExpandButton";
            menuHideExpandButton.Padding = new Padding(12, 0, 0, 8);
            menuHideExpandButton.Size = new Size(163, 59);
            menuHideExpandButton.TabIndex = 5;
            menuHideExpandButton.Text = "            Hide Menu";
            menuHideExpandButton.TextAlign = ContentAlignment.MiddleLeft;
            menuHideExpandButton.UseVisualStyleBackColor = false;
            menuHideExpandButton.Click += menuHideExpandButton_Click;
            // 
            // screenshotsSidebarPanel
            // 
            screenshotsSidebarPanel.BackColor = Color.Transparent;
            screenshotsSidebarPanel.Controls.Add(screenshotSidebarButton);
            screenshotsSidebarPanel.Location = new Point(3, 57);
            screenshotsSidebarPanel.Name = "screenshotsSidebarPanel";
            screenshotsSidebarPanel.Size = new Size(180, 43);
            screenshotsSidebarPanel.TabIndex = 6;
            // 
            // screenshotSidebarButton
            // 
            screenshotSidebarButton.BackColor = Color.FromArgb(23, 24, 29);
            screenshotSidebarButton.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            screenshotSidebarButton.ForeColor = Color.White;
            screenshotSidebarButton.Image = (Image)resources.GetObject("screenshotSidebarButton.Image");
            screenshotSidebarButton.ImageAlign = ContentAlignment.MiddleLeft;
            screenshotSidebarButton.Location = new Point(-6, -9);
            screenshotSidebarButton.Name = "screenshotSidebarButton";
            screenshotSidebarButton.Padding = new Padding(12, 0, 0, 0);
            screenshotSidebarButton.Size = new Size(163, 59);
            screenshotSidebarButton.TabIndex = 5;
            screenshotSidebarButton.Text = "            Screenshots";
            screenshotSidebarButton.TextAlign = ContentAlignment.MiddleLeft;
            screenshotSidebarButton.UseVisualStyleBackColor = false;
            screenshotSidebarButton.Click += screenshotButton_Click;
            // 
            // colorPickerPanel
            // 
            colorPickerPanel.BackColor = Color.Transparent;
            colorPickerPanel.Controls.Add(colorPickerSidebarButton);
            colorPickerPanel.Location = new Point(3, 106);
            colorPickerPanel.Name = "colorPickerPanel";
            colorPickerPanel.Size = new Size(180, 43);
            colorPickerPanel.TabIndex = 8;
            // 
            // colorPickerSidebarButton
            // 
            colorPickerSidebarButton.BackColor = Color.FromArgb(23, 24, 29);
            colorPickerSidebarButton.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            colorPickerSidebarButton.ForeColor = Color.White;
            colorPickerSidebarButton.Image = (Image)resources.GetObject("colorPickerSidebarButton.Image");
            colorPickerSidebarButton.ImageAlign = ContentAlignment.MiddleLeft;
            colorPickerSidebarButton.Location = new Point(-6, -9);
            colorPickerSidebarButton.Name = "colorPickerSidebarButton";
            colorPickerSidebarButton.Padding = new Padding(12, 0, 0, 0);
            colorPickerSidebarButton.Size = new Size(163, 59);
            colorPickerSidebarButton.TabIndex = 5;
            colorPickerSidebarButton.Text = "            Color Picker";
            colorPickerSidebarButton.TextAlign = ContentAlignment.MiddleLeft;
            colorPickerSidebarButton.UseVisualStyleBackColor = false;
            // 
            // menuFlowPanel
            // 
            menuFlowPanel.BackColor = Color.FromArgb(32, 33, 36);
            menuFlowPanel.Controls.Add(menuPanel);
            menuFlowPanel.Controls.Add(panel4);
            menuFlowPanel.Controls.Add(panel5);
            menuFlowPanel.Location = new Point(3, 155);
            menuFlowPanel.Name = "menuFlowPanel";
            menuFlowPanel.Size = new Size(180, 42);
            menuFlowPanel.TabIndex = 9;
            // 
            // menuPanel
            // 
            menuPanel.BackColor = Color.Transparent;
            menuPanel.Controls.Add(menuButton);
            menuPanel.Location = new Point(0, 0);
            menuPanel.Margin = new Padding(0);
            menuPanel.Name = "menuPanel";
            menuPanel.Size = new Size(180, 43);
            menuPanel.TabIndex = 11;
            // 
            // menuButton
            // 
            menuButton.BackColor = Color.FromArgb(23, 24, 29);
            menuButton.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            menuButton.ForeColor = Color.White;
            menuButton.Image = (Image)resources.GetObject("menuButton.Image");
            menuButton.ImageAlign = ContentAlignment.MiddleLeft;
            menuButton.Location = new Point(-6, -9);
            menuButton.Name = "menuButton";
            menuButton.Padding = new Padding(12, 0, 0, 0);
            menuButton.Size = new Size(163, 59);
            menuButton.TabIndex = 5;
            menuButton.Text = "            Menu";
            menuButton.TextAlign = ContentAlignment.MiddleLeft;
            menuButton.UseVisualStyleBackColor = false;
            menuButton.Click += menuButton_Click;
            // 
            // panel4
            // 
            panel4.BackColor = Color.FromArgb(32, 33, 36);
            panel4.Controls.Add(button2);
            panel4.Location = new Point(0, 43);
            panel4.Margin = new Padding(0);
            panel4.Name = "panel4";
            panel4.Size = new Size(180, 43);
            panel4.TabIndex = 10;
            // 
            // button2
            // 
            button2.BackColor = Color.FromArgb(32, 33, 36);
            button2.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button2.ForeColor = Color.White;
            button2.Image = (Image)resources.GetObject("button2.Image");
            button2.ImageAlign = ContentAlignment.MiddleLeft;
            button2.Location = new Point(-6, -9);
            button2.Margin = new Padding(0);
            button2.Name = "button2";
            button2.Padding = new Padding(20, 0, 0, 0);
            button2.Size = new Size(163, 59);
            button2.TabIndex = 5;
            button2.Text = "            Settings";
            button2.TextAlign = ContentAlignment.MiddleLeft;
            button2.UseVisualStyleBackColor = false;
            // 
            // panel5
            // 
            panel5.BackColor = Color.FromArgb(32, 33, 36);
            panel5.Controls.Add(button3);
            panel5.Location = new Point(0, 86);
            panel5.Margin = new Padding(0);
            panel5.Name = "panel5";
            panel5.Size = new Size(180, 43);
            panel5.TabIndex = 11;
            // 
            // button3
            // 
            button3.BackColor = Color.FromArgb(32, 33, 36);
            button3.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button3.ForeColor = Color.White;
            button3.Image = (Image)resources.GetObject("button3.Image");
            button3.ImageAlign = ContentAlignment.MiddleLeft;
            button3.Location = new Point(-6, -6);
            button3.Margin = new Padding(0);
            button3.Name = "button3";
            button3.Padding = new Padding(20, 0, 0, 0);
            button3.Size = new Size(212, 59);
            button3.TabIndex = 5;
            button3.Text = "            About";
            button3.TextAlign = ContentAlignment.MiddleLeft;
            button3.UseVisualStyleBackColor = false;
            // 
            // settingsPanel
            // 
            settingsPanel.BackColor = Color.Transparent;
            settingsPanel.Controls.Add(settingsSidebarButton);
            settingsPanel.Location = new Point(3, 203);
            settingsPanel.Name = "settingsPanel";
            settingsPanel.Size = new Size(180, 43);
            settingsPanel.TabIndex = 7;
            // 
            // settingsSidebarButton
            // 
            settingsSidebarButton.BackColor = Color.FromArgb(23, 24, 29);
            settingsSidebarButton.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            settingsSidebarButton.ForeColor = Color.White;
            settingsSidebarButton.Image = (Image)resources.GetObject("settingsSidebarButton.Image");
            settingsSidebarButton.ImageAlign = ContentAlignment.MiddleLeft;
            settingsSidebarButton.Location = new Point(-6, -9);
            settingsSidebarButton.Name = "settingsSidebarButton";
            settingsSidebarButton.Padding = new Padding(12, 0, 0, 0);
            settingsSidebarButton.Size = new Size(163, 59);
            settingsSidebarButton.TabIndex = 5;
            settingsSidebarButton.Text = "            Settings";
            settingsSidebarButton.TextAlign = ContentAlignment.MiddleLeft;
            settingsSidebarButton.UseVisualStyleBackColor = false;
            settingsSidebarButton.Click += settingsSidebarButton_Click;
            // 
            // objectMotionTimer
            // 
            objectMotionTimer.Interval = 15;
            objectMotionTimer.Tick += objectMotionTimer_Tick;
            // 
            // MainForm
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(839, 465);
            Controls.Add(sidebarFlowPanel);
            DoubleBuffered = true;
            Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            IsMdiContainer = true;
            Name = "MainForm";
            Text = "Bart's Toolboxje";
            sidebarFlowPanel.ResumeLayout(false);
            menuButtonPanel.ResumeLayout(false);
            screenshotsSidebarPanel.ResumeLayout(false);
            colorPickerPanel.ResumeLayout(false);
            menuFlowPanel.ResumeLayout(false);
            menuPanel.ResumeLayout(false);
            panel4.ResumeLayout(false);
            panel5.ResumeLayout(false);
            settingsPanel.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private Panel screenshotsSidebarPanel;
        private Button screenshotSidebarButton;
        private Panel colorPickerPanel;
        private Button colorPickerSidebarButton;
        private FlowLayoutPanel menuFlowPanel;
        private Panel menuPanel;
        private Button menuButton;
        private Panel panel4;
        private Button button2;
        private Button settingsSidebarButton;
        private Panel settingsPanel;
        private Panel panel5;
        private Button button3;
        private System.Windows.Forms.Timer menuTransition;
        private Button firstMenuButton;
        private System.Windows.Forms.Timer objectMotionTimer;
        internal FlowLayoutPanel sidebarFlowPanel;
        private Panel menuButtonPanel;
        private Label divederline;
        internal Button menuHideExpandButton;
    }
}