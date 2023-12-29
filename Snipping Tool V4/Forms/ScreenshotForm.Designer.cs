namespace Snipping_Tool_V4.Forms
{
    partial class ScreenshotForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScreenshotForm));
            screenshotPanel = new Panel();
            screenshotResultPicture = new PictureBox();
            newScreenshotButton = new Button();
            timerFlowPanel = new FlowLayoutPanel();
            timerPanel = new Panel();
            timerButton = new Button();
            timerNoTimerPanel = new Panel();
            timerNoTimer = new Button();
            timerPanel1 = new Panel();
            timer1 = new Button();
            timerPanel2 = new Panel();
            timer2 = new Button();
            timerPanel3 = new Panel();
            timer3 = new Button();
            timerPanel4 = new Panel();
            timer4 = new Button();
            timerPanel5 = new Panel();
            timer5 = new Button();
            screenShotTimer = new System.Windows.Forms.Timer(components);
            screenshotPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)screenshotResultPicture).BeginInit();
            timerFlowPanel.SuspendLayout();
            timerPanel.SuspendLayout();
            timerNoTimerPanel.SuspendLayout();
            timerPanel1.SuspendLayout();
            timerPanel2.SuspendLayout();
            timerPanel3.SuspendLayout();
            timerPanel4.SuspendLayout();
            timerPanel5.SuspendLayout();
            SuspendLayout();
            // 
            // screenshotPanel
            // 
            screenshotPanel.Controls.Add(screenshotResultPicture);
            screenshotPanel.Location = new Point(123, 387);
            screenshotPanel.Name = "screenshotPanel";
            screenshotPanel.Size = new Size(377, 160);
            screenshotPanel.TabIndex = 1;
            // 
            // screenshotResultPicture
            // 
            screenshotResultPicture.Location = new Point(92, 29);
            screenshotResultPicture.Name = "screenshotResultPicture";
            screenshotResultPicture.Size = new Size(148, 128);
            screenshotResultPicture.TabIndex = 2;
            screenshotResultPicture.TabStop = false;
            // 
            // newScreenshotButton
            // 
            newScreenshotButton.BackColor = Color.Transparent;
            newScreenshotButton.BackgroundImageLayout = ImageLayout.None;
            newScreenshotButton.FlatAppearance.BorderSize = 0;
            newScreenshotButton.FlatStyle = FlatStyle.Flat;
            newScreenshotButton.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            newScreenshotButton.ForeColor = Color.Black;
            newScreenshotButton.Image = (Image)resources.GetObject("newScreenshotButton.Image");
            newScreenshotButton.ImageAlign = ContentAlignment.MiddleLeft;
            newScreenshotButton.Location = new Point(0, 0);
            newScreenshotButton.Margin = new Padding(0);
            newScreenshotButton.Name = "newScreenshotButton";
            newScreenshotButton.RightToLeft = RightToLeft.No;
            newScreenshotButton.Size = new Size(94, 43);
            newScreenshotButton.TabIndex = 6;
            newScreenshotButton.Text = "        &New";
            newScreenshotButton.UseVisualStyleBackColor = true;
            newScreenshotButton.Click += newScreenshotButton_Click;
            // 
            // timerFlowPanel
            // 
            timerFlowPanel.BackColor = Color.FromArgb(255, 255, 252);
            timerFlowPanel.Controls.Add(timerPanel);
            timerFlowPanel.Controls.Add(timerNoTimerPanel);
            timerFlowPanel.Controls.Add(timerPanel1);
            timerFlowPanel.Controls.Add(timerPanel2);
            timerFlowPanel.Controls.Add(timerPanel3);
            timerFlowPanel.Controls.Add(timerPanel4);
            timerFlowPanel.Controls.Add(timerPanel5);
            timerFlowPanel.Location = new Point(97, 0);
            timerFlowPanel.Margin = new Padding(0);
            timerFlowPanel.Name = "timerFlowPanel";
            timerFlowPanel.Size = new Size(117, 43);
            timerFlowPanel.TabIndex = 8;
            // 
            // timerPanel
            // 
            timerPanel.BackColor = Color.Transparent;
            timerPanel.Controls.Add(timerButton);
            timerPanel.Location = new Point(0, 0);
            timerPanel.Margin = new Padding(0);
            timerPanel.Name = "timerPanel";
            timerPanel.Size = new Size(125, 43);
            timerPanel.TabIndex = 0;
            // 
            // timerButton
            // 
            timerButton.BackColor = Color.Transparent;
            timerButton.FlatAppearance.BorderSize = 0;
            timerButton.FlatStyle = FlatStyle.Flat;
            timerButton.Font = new Font("Microsoft Sans Serif", 9.75F);
            timerButton.Image = (Image)resources.GetObject("timerButton.Image");
            timerButton.ImageAlign = ContentAlignment.MiddleLeft;
            timerButton.Location = new Point(0, 0);
            timerButton.Margin = new Padding(0);
            timerButton.Name = "timerButton";
            timerButton.Size = new Size(125, 43);
            timerButton.TabIndex = 9;
            timerButton.Text = "         &Timer (0)";
            timerButton.UseVisualStyleBackColor = false;
            timerButton.Click += timerButton_Click;
            // 
            // timerNoTimerPanel
            // 
            timerNoTimerPanel.Controls.Add(timerNoTimer);
            timerNoTimerPanel.Location = new Point(0, 43);
            timerNoTimerPanel.Margin = new Padding(0);
            timerNoTimerPanel.Name = "timerNoTimerPanel";
            timerNoTimerPanel.Size = new Size(125, 22);
            timerNoTimerPanel.TabIndex = 1;
            // 
            // timerNoTimer
            // 
            timerNoTimer.BackColor = SystemColors.ButtonFace;
            timerNoTimer.FlatAppearance.BorderSize = 0;
            timerNoTimer.FlatStyle = FlatStyle.Flat;
            timerNoTimer.Font = new Font("Microsoft Sans Serif", 9.75F);
            timerNoTimer.Location = new Point(0, 0);
            timerNoTimer.Margin = new Padding(0);
            timerNoTimer.Name = "timerNoTimer";
            timerNoTimer.Size = new Size(125, 22);
            timerNoTimer.TabIndex = 9;
            timerNoTimer.Text = "No Timer";
            timerNoTimer.UseVisualStyleBackColor = false;
            timerNoTimer.Click += timerNoTimer_Click;
            // 
            // timerPanel1
            // 
            timerPanel1.BackColor = Color.Transparent;
            timerPanel1.Controls.Add(timer1);
            timerPanel1.Location = new Point(0, 65);
            timerPanel1.Margin = new Padding(0);
            timerPanel1.Name = "timerPanel1";
            timerPanel1.Size = new Size(125, 22);
            timerPanel1.TabIndex = 2;
            // 
            // timer1
            // 
            timer1.BackColor = SystemColors.ButtonFace;
            timer1.FlatAppearance.BorderSize = 0;
            timer1.FlatStyle = FlatStyle.Flat;
            timer1.Font = new Font("Microsoft Sans Serif", 9.75F);
            timer1.Location = new Point(0, 0);
            timer1.Margin = new Padding(0);
            timer1.Name = "timer1";
            timer1.Size = new Size(125, 22);
            timer1.TabIndex = 9;
            timer1.Text = "&1 Second";
            timer1.UseVisualStyleBackColor = false;
            timer1.Click += timer1_Click;
            // 
            // timerPanel2
            // 
            timerPanel2.Controls.Add(timer2);
            timerPanel2.Location = new Point(0, 87);
            timerPanel2.Margin = new Padding(0);
            timerPanel2.Name = "timerPanel2";
            timerPanel2.Size = new Size(125, 22);
            timerPanel2.TabIndex = 3;
            // 
            // timer2
            // 
            timer2.BackColor = SystemColors.ButtonFace;
            timer2.FlatAppearance.BorderSize = 0;
            timer2.FlatStyle = FlatStyle.Flat;
            timer2.Font = new Font("Microsoft Sans Serif", 9.75F);
            timer2.Location = new Point(0, 0);
            timer2.Margin = new Padding(0);
            timer2.Name = "timer2";
            timer2.Size = new Size(125, 22);
            timer2.TabIndex = 9;
            timer2.Text = "2 Seconds";
            timer2.UseVisualStyleBackColor = false;
            timer2.Click += timer2_Click;
            // 
            // timerPanel3
            // 
            timerPanel3.Controls.Add(timer3);
            timerPanel3.Location = new Point(0, 109);
            timerPanel3.Margin = new Padding(0);
            timerPanel3.Name = "timerPanel3";
            timerPanel3.Size = new Size(125, 22);
            timerPanel3.TabIndex = 4;
            // 
            // timer3
            // 
            timer3.BackColor = SystemColors.ButtonFace;
            timer3.FlatAppearance.BorderSize = 0;
            timer3.FlatStyle = FlatStyle.Flat;
            timer3.Font = new Font("Microsoft Sans Serif", 9.75F);
            timer3.Location = new Point(0, 0);
            timer3.Margin = new Padding(0);
            timer3.Name = "timer3";
            timer3.Size = new Size(125, 22);
            timer3.TabIndex = 9;
            timer3.Text = "3 Seconds";
            timer3.UseVisualStyleBackColor = false;
            timer3.Click += timer3_Click;
            // 
            // timerPanel4
            // 
            timerPanel4.Controls.Add(timer4);
            timerPanel4.Location = new Point(0, 131);
            timerPanel4.Margin = new Padding(0);
            timerPanel4.Name = "timerPanel4";
            timerPanel4.Size = new Size(125, 22);
            timerPanel4.TabIndex = 5;
            // 
            // timer4
            // 
            timer4.BackColor = SystemColors.ButtonFace;
            timer4.FlatAppearance.BorderSize = 0;
            timer4.FlatStyle = FlatStyle.Flat;
            timer4.Font = new Font("Microsoft Sans Serif", 9.75F);
            timer4.Location = new Point(0, 0);
            timer4.Margin = new Padding(0);
            timer4.Name = "timer4";
            timer4.Size = new Size(125, 22);
            timer4.TabIndex = 9;
            timer4.Text = "4 Seconds";
            timer4.UseVisualStyleBackColor = false;
            timer4.Click += timer4_Click;
            // 
            // timerPanel5
            // 
            timerPanel5.Controls.Add(timer5);
            timerPanel5.Location = new Point(0, 153);
            timerPanel5.Margin = new Padding(0);
            timerPanel5.Name = "timerPanel5";
            timerPanel5.Size = new Size(125, 24);
            timerPanel5.TabIndex = 6;
            // 
            // timer5
            // 
            timer5.BackColor = SystemColors.ButtonFace;
            timer5.FlatAppearance.BorderSize = 0;
            timer5.FlatStyle = FlatStyle.Flat;
            timer5.Font = new Font("Microsoft Sans Serif", 9.75F);
            timer5.Location = new Point(0, 0);
            timer5.Margin = new Padding(0);
            timer5.Name = "timer5";
            timer5.Size = new Size(125, 24);
            timer5.TabIndex = 9;
            timer5.Text = "5 Seconds";
            timer5.UseVisualStyleBackColor = false;
            timer5.Click += timer5_Click;
            // 
            // screenShotTimer
            // 
            screenShotTimer.Interval = 15;
            screenShotTimer.Tick += screenShotTimer_Tick;
            // 
            // ScreenshotForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(255, 255, 252);
            ClientSize = new Size(620, 549);
            ControlBox = false;
            Controls.Add(timerFlowPanel);
            Controls.Add(newScreenshotButton);
            Controls.Add(screenshotPanel);
            FormBorderStyle = FormBorderStyle.None;
            Name = "ScreenshotForm";
            SizeGripStyle = SizeGripStyle.Hide;
            StartPosition = FormStartPosition.Manual;
            Text = "ScreenshotForm";
            screenshotPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)screenshotResultPicture).EndInit();
            timerFlowPanel.ResumeLayout(false);
            timerPanel.ResumeLayout(false);
            timerNoTimerPanel.ResumeLayout(false);
            timerPanel1.ResumeLayout(false);
            timerPanel2.ResumeLayout(false);
            timerPanel3.ResumeLayout(false);
            timerPanel4.ResumeLayout(false);
            timerPanel5.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private Panel screenshotPanel;
        private PictureBox screenshotResultPicture;
        private Button newScreenshotButton;
        private FlowLayoutPanel timerFlowPanel;
        private Panel timerPanel;
        private Panel timerNoTimerPanel;
        private Button timerNoTimer;
        private Panel timerPanel1;
        private Button timer1;
        private Panel timerPanel2;
        private Button timer2;
        private Panel timerPanel3;
        private Button timer3;
        private Panel timerPanel4;
        private Button timer4;
        private Panel timerPanel5;
        private Button timer5;
        private Button timerButton;
        private System.Windows.Forms.Timer screenShotTimer;
    }
}