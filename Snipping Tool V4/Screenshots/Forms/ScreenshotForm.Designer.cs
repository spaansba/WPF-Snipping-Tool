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
            label1 = new Label();
            screenshotResultPicture = new PictureBox();
            screenShotTimer = new System.Windows.Forms.Timer(components);
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
            newScreenshotButton = new Button();
            topBarLabel = new Label();
            deviderLabel = new Label();
            RectangleShapeButton = new ReaLTaiizor.Controls.AirButton();
            freeHandButton = new ReaLTaiizor.Controls.AirButton();
            redLineButton = new ReaLTaiizor.Controls.AirButton();
            blackLineButton = new ReaLTaiizor.Controls.AirButton();
            size3Button = new ReaLTaiizor.Controls.AirButton();
            size10Button = new ReaLTaiizor.Controls.AirButton();
            circleButton = new ReaLTaiizor.Controls.AirButton();
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
            screenshotPanel.AutoScroll = true;
            screenshotPanel.AutoSize = true;
            screenshotPanel.Controls.Add(circleButton);
            screenshotPanel.Controls.Add(label1);
            screenshotPanel.Controls.Add(screenshotResultPicture);
            screenshotPanel.Dock = DockStyle.Fill;
            screenshotPanel.Location = new Point(0, 45);
            screenshotPanel.Name = "screenshotPanel";
            screenshotPanel.Size = new Size(555, 389);
            screenshotPanel.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(291, 6);
            label1.Name = "label1";
            label1.Size = new Size(139, 15);
            label1.TabIndex = 3;
            label1.Text = "temp buttons for testing:";
            // 
            // screenshotResultPicture
            // 
            screenshotResultPicture.Location = new Point(116, 112);
            screenshotResultPicture.Name = "screenshotResultPicture";
            screenshotResultPicture.Size = new Size(247, 142);
            screenshotResultPicture.TabIndex = 2;
            screenshotResultPicture.TabStop = false;
            screenshotResultPicture.MouseClick += screenshotResultPicture_MouseClick;
            // 
            // screenShotTimer
            // 
            screenShotTimer.Interval = 15;
            screenShotTimer.Tick += screenShotTimer_Tick;
            // 
            // timerFlowPanel
            // 
            timerFlowPanel.BackColor = Color.FromArgb(224, 224, 224);
            timerFlowPanel.Controls.Add(timerPanel);
            timerFlowPanel.Controls.Add(timerNoTimerPanel);
            timerFlowPanel.Controls.Add(timerPanel1);
            timerFlowPanel.Controls.Add(timerPanel2);
            timerFlowPanel.Controls.Add(timerPanel3);
            timerFlowPanel.Controls.Add(timerPanel4);
            timerFlowPanel.Controls.Add(timerPanel5);
            timerFlowPanel.Location = new Point(94, -3);
            timerFlowPanel.Margin = new Padding(0);
            timerFlowPanel.Name = "timerFlowPanel";
            timerFlowPanel.Size = new Size(117, 47);
            timerFlowPanel.TabIndex = 8;
            // 
            // timerPanel
            // 
            timerPanel.BackColor = Color.Transparent;
            timerPanel.Controls.Add(timerButton);
            timerPanel.Location = new Point(0, 0);
            timerPanel.Margin = new Padding(0);
            timerPanel.Name = "timerPanel";
            timerPanel.Size = new Size(125, 47);
            timerPanel.TabIndex = 0;
            // 
            // timerButton
            // 
            timerButton.BackColor = Color.Transparent;
            timerButton.FlatAppearance.BorderSize = 0;
            timerButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(192, 192, 192);
            timerButton.FlatStyle = FlatStyle.Flat;
            timerButton.Font = new Font("Microsoft Sans Serif", 9.75F);
            timerButton.Image = (Image)resources.GetObject("timerButton.Image");
            timerButton.ImageAlign = ContentAlignment.MiddleLeft;
            timerButton.Location = new Point(0, 0);
            timerButton.Margin = new Padding(0);
            timerButton.Name = "timerButton";
            timerButton.Size = new Size(125, 47);
            timerButton.TabIndex = 9;
            timerButton.Text = "         &Timer (0)  ▼";
            timerButton.UseVisualStyleBackColor = false;
            timerButton.Click += timerButton_Click;
            // 
            // timerNoTimerPanel
            // 
            timerNoTimerPanel.Controls.Add(timerNoTimer);
            timerNoTimerPanel.Location = new Point(0, 47);
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
            timerPanel1.Location = new Point(0, 69);
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
            timerPanel2.Location = new Point(0, 91);
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
            timerPanel3.Location = new Point(0, 113);
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
            timerPanel4.Location = new Point(0, 135);
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
            timerPanel5.Location = new Point(0, 157);
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
            // newScreenshotButton
            // 
            newScreenshotButton.BackColor = Color.FromArgb(224, 224, 224);
            newScreenshotButton.BackgroundImageLayout = ImageLayout.None;
            newScreenshotButton.FlatAppearance.BorderSize = 0;
            newScreenshotButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(192, 192, 192);
            newScreenshotButton.FlatStyle = FlatStyle.Flat;
            newScreenshotButton.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            newScreenshotButton.ForeColor = Color.Black;
            newScreenshotButton.Image = (Image)resources.GetObject("newScreenshotButton.Image");
            newScreenshotButton.ImageAlign = ContentAlignment.MiddleLeft;
            newScreenshotButton.Location = new Point(0, -3);
            newScreenshotButton.Margin = new Padding(0);
            newScreenshotButton.Name = "newScreenshotButton";
            newScreenshotButton.RightToLeft = RightToLeft.No;
            newScreenshotButton.Size = new Size(94, 47);
            newScreenshotButton.TabIndex = 6;
            newScreenshotButton.Text = "        &New";
            newScreenshotButton.UseVisualStyleBackColor = false;
            newScreenshotButton.Click += newScreenshotButton_Click;
            // 
            // topBarLabel
            // 
            topBarLabel.BackColor = Color.FromArgb(224, 224, 224);
            topBarLabel.Dock = DockStyle.Top;
            topBarLabel.Location = new Point(0, 0);
            topBarLabel.Name = "topBarLabel";
            topBarLabel.Size = new Size(555, 45);
            topBarLabel.TabIndex = 3;
            // 
            // deviderLabel
            // 
            deviderLabel.BackColor = Color.FromArgb(64, 64, 64);
            deviderLabel.Location = new Point(218, 8);
            deviderLabel.Name = "deviderLabel";
            deviderLabel.Size = new Size(1, 31);
            deviderLabel.TabIndex = 3;
            // 
            // RectangleShapeButton
            // 
            RectangleShapeButton.Customization = "7e3t//Ly8v/r6+v/5ubm/+vr6//f39//p6en/zw8PP8UFBT/gICA/w==";
            RectangleShapeButton.Font = new Font("Segoe UI", 9F);
            RectangleShapeButton.Image = null;
            RectangleShapeButton.Location = new Point(462, 1);
            RectangleShapeButton.Name = "RectangleShapeButton";
            RectangleShapeButton.NoRounding = false;
            RectangleShapeButton.Size = new Size(81, 21);
            RectangleShapeButton.TabIndex = 3;
            RectangleShapeButton.Text = "Rectangle";
            RectangleShapeButton.Transparent = false;
            RectangleShapeButton.Click += RectangleShapeButton_Click;
            // 
            // freeHandButton
            // 
            freeHandButton.Customization = "7e3t//Ly8v/r6+v/5ubm/+vr6//f39//p6en/zw8PP8UFBT/gICA/w==";
            freeHandButton.Font = new Font("Segoe UI", 9F);
            freeHandButton.Image = null;
            freeHandButton.Location = new Point(462, 21);
            freeHandButton.Name = "freeHandButton";
            freeHandButton.NoRounding = false;
            freeHandButton.Size = new Size(81, 21);
            freeHandButton.TabIndex = 4;
            freeHandButton.Text = "Free Hand";
            freeHandButton.Transparent = false;
            freeHandButton.Click += freeHandButton_Click;
            // 
            // redLineButton
            // 
            redLineButton.Customization = "7e3t//Ly8v/r6+v/5ubm/+vr6//f39//p6en/zw8PP8UFBT/gICA/w==";
            redLineButton.Font = new Font("Segoe UI", 9F);
            redLineButton.Image = null;
            redLineButton.Location = new Point(363, 0);
            redLineButton.Name = "redLineButton";
            redLineButton.NoRounding = false;
            redLineButton.Size = new Size(81, 21);
            redLineButton.TabIndex = 9;
            redLineButton.Text = "Red Line";
            redLineButton.Transparent = false;
            redLineButton.Click += redLineButton_Click;
            // 
            // blackLineButton
            // 
            blackLineButton.Customization = "7e3t//Ly8v/r6+v/5ubm/+vr6//f39//p6en/zw8PP8UFBT/gICA/w==";
            blackLineButton.Font = new Font("Segoe UI", 9F);
            blackLineButton.Image = null;
            blackLineButton.Location = new Point(363, 21);
            blackLineButton.Name = "blackLineButton";
            blackLineButton.NoRounding = false;
            blackLineButton.Size = new Size(81, 21);
            blackLineButton.TabIndex = 10;
            blackLineButton.Text = "Black Line";
            blackLineButton.Transparent = false;
            blackLineButton.Click += blackLineButton_Click;
            // 
            // size3Button
            // 
            size3Button.Customization = "7e3t//Ly8v/r6+v/5ubm/+vr6//f39//p6en/zw8PP8UFBT/gICA/w==";
            size3Button.Font = new Font("Segoe UI", 9F);
            size3Button.Image = null;
            size3Button.Location = new Point(291, 1);
            size3Button.Name = "size3Button";
            size3Button.NoRounding = false;
            size3Button.Size = new Size(66, 21);
            size3Button.TabIndex = 11;
            size3Button.Text = "Size 3";
            size3Button.Transparent = false;
            size3Button.Click += size3Button_Click;
            // 
            // size10Button
            // 
            size10Button.Customization = "7e3t//Ly8v/r6+v/5ubm/+vr6//f39//p6en/zw8PP8UFBT/gICA/w==";
            size10Button.Font = new Font("Segoe UI", 9F);
            size10Button.Image = null;
            size10Button.Location = new Point(291, 23);
            size10Button.Name = "size10Button";
            size10Button.NoRounding = false;
            size10Button.Size = new Size(66, 21);
            size10Button.TabIndex = 12;
            size10Button.Text = "Size 10";
            size10Button.Transparent = false;
            size10Button.Click += size10Button_Click;
            // 
            // circleButton
            // 
            circleButton.Customization = "7e3t//Ly8v/r6+v/5ubm/+vr6//f39//p6en/zw8PP8UFBT/gICA/w==";
            circleButton.Font = new Font("Segoe UI", 9F);
            circleButton.Image = null;
            circleButton.Location = new Point(462, 0);
            circleButton.Name = "circleButton";
            circleButton.NoRounding = false;
            circleButton.Size = new Size(81, 21);
            circleButton.TabIndex = 5;
            circleButton.Text = "Circle";
            circleButton.Transparent = false;
            circleButton.Click += circleButton_Click;
            // 
            // ScreenshotForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(255, 255, 252);
            ClientSize = new Size(555, 434);
            ControlBox = false;
            Controls.Add(size10Button);
            Controls.Add(size3Button);
            Controls.Add(blackLineButton);
            Controls.Add(redLineButton);
            Controls.Add(freeHandButton);
            Controls.Add(RectangleShapeButton);
            Controls.Add(deviderLabel);
            Controls.Add(newScreenshotButton);
            Controls.Add(timerFlowPanel);
            Controls.Add(screenshotPanel);
            Controls.Add(topBarLabel);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.None;
            KeyPreview = true;
            Name = "ScreenshotForm";
            SizeGripStyle = SizeGripStyle.Hide;
            StartPosition = FormStartPosition.Manual;
            Text = "ScreenshotForm";
            Deactivate += ScreenshotForm_Deactivate;
            KeyDown += ScreenshotForm_KeyDown;
            screenshotPanel.ResumeLayout(false);
            screenshotPanel.PerformLayout();
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
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.Timer screenShotTimer;
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
        private Button newScreenshotButton;
        internal Button timerButton;
        internal PictureBox screenshotResultPicture;
        internal Panel screenshotPanel;
        private Label topBarLabel;
        private Label deviderLabel;
        private ReaLTaiizor.Controls.AirButton RectangleShapeButton;
        private ReaLTaiizor.Controls.AirButton freeHandButton;
        private ReaLTaiizor.Controls.AirButton redLineButton;
        private ReaLTaiizor.Controls.AirButton blackLineButton;
        private Label label1;
        private ReaLTaiizor.Controls.AirButton size3Button;
        private ReaLTaiizor.Controls.AirButton size10Button;
        private ReaLTaiizor.Controls.AirButton circleButton;
    }
}