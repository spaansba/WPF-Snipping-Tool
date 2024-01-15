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
            picturePanel = new Panel();
            screenshotResultPicture = new PictureBox();
            label3 = new Label();
            label2 = new Label();
            colorPickerTablePanel = new TableLayoutPanel();
            size3Button = new ReaLTaiizor.Controls.AirButton();
            size10Button = new ReaLTaiizor.Controls.AirButton();
            symbolMainPanel = new Panel();
            expandSymbolsButton = new Button();
            shapesTablePanel = new TableLayoutPanel();
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
            tableLayoutPanel2 = new TableLayoutPanel();
            specialToolsTablePanel = new TableLayoutPanel();
            label1 = new Label();
            symbolOptionsTablePanel = new TableLayoutPanel();
            picturePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)screenshotResultPicture).BeginInit();
            symbolMainPanel.SuspendLayout();
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
            // picturePanel
            // 
            picturePanel.AutoScroll = true;
            picturePanel.AutoSize = true;
            picturePanel.Controls.Add(screenshotResultPicture);
            picturePanel.Dock = DockStyle.Fill;
            picturePanel.Location = new Point(0, 50);
            picturePanel.Name = "picturePanel";
            picturePanel.Size = new Size(709, 384);
            picturePanel.TabIndex = 1;
            picturePanel.Paint += screenshotPanel_Paint;
            // 
            // screenshotResultPicture
            // 
            screenshotResultPicture.Location = new Point(239, 88);
            screenshotResultPicture.Name = "screenshotResultPicture";
            screenshotResultPicture.Size = new Size(247, 142);
            screenshotResultPicture.TabIndex = 2;
            screenshotResultPicture.TabStop = false;
            // 
            // label3
            // 
            label3.BackColor = Color.FromArgb(64, 64, 64);
            label3.Location = new Point(583, 7);
            label3.Name = "label3";
            label3.Size = new Size(1, 32);
            label3.TabIndex = 19;
            // 
            // label2
            // 
            label2.BackColor = Color.FromArgb(64, 64, 64);
            label2.Location = new Point(510, 7);
            label2.Name = "label2";
            label2.Size = new Size(1, 32);
            label2.TabIndex = 18;
            // 
            // colorPickerTablePanel
            // 
            colorPickerTablePanel.BackColor = Color.FromArgb(224, 224, 224);
            colorPickerTablePanel.ColumnCount = 2;
            colorPickerTablePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            colorPickerTablePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            colorPickerTablePanel.Location = new Point(520, 14);
            colorPickerTablePanel.Name = "colorPickerTablePanel";
            colorPickerTablePanel.RowCount = 1;
            colorPickerTablePanel.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            colorPickerTablePanel.Size = new Size(52, 26);
            colorPickerTablePanel.TabIndex = 13;
            // 
            // size3Button
            // 
            size3Button.Customization = "7e3t//Ly8v/r6+v/5ubm/+vr6//f39//p6en/zw8PP8UFBT/gICA/w==";
            size3Button.Font = new Font("Segoe UI", 9F);
            size3Button.Image = null;
            size3Button.Location = new Point(596, 28);
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
            size10Button.Location = new Point(596, 5);
            size10Button.Name = "size10Button";
            size10Button.NoRounding = false;
            size10Button.Size = new Size(66, 21);
            size10Button.TabIndex = 12;
            size10Button.Text = "Size 10";
            size10Button.Transparent = false;
            size10Button.Click += size10Button_Click;
            // 
            // symbolMainPanel
            // 
            symbolMainPanel.BackColor = Color.FromArgb(224, 224, 224);
            symbolMainPanel.BorderStyle = BorderStyle.FixedSingle;
            symbolMainPanel.Controls.Add(expandSymbolsButton);
            symbolMainPanel.Controls.Add(shapesTablePanel);
            symbolMainPanel.Location = new Point(333, 10);
            symbolMainPanel.Name = "symbolMainPanel";
            symbolMainPanel.Size = new Size(117, 30);
            symbolMainPanel.TabIndex = 16;
            // 
            // expandSymbolsButton
            // 
            expandSymbolsButton.BackColor = Color.LightGray;
            expandSymbolsButton.Dock = DockStyle.Right;
            expandSymbolsButton.FlatAppearance.BorderSize = 0;
            expandSymbolsButton.FlatStyle = FlatStyle.Flat;
            expandSymbolsButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            expandSymbolsButton.ImageAlign = ContentAlignment.TopCenter;
            expandSymbolsButton.Location = new Point(101, 0);
            expandSymbolsButton.Margin = new Padding(0);
            expandSymbolsButton.Name = "expandSymbolsButton";
            expandSymbolsButton.Size = new Size(14, 28);
            expandSymbolsButton.TabIndex = 17;
            expandSymbolsButton.Text = "^";
            expandSymbolsButton.UseVisualStyleBackColor = false;
            expandSymbolsButton.Click += expandSymbolsButton_Click;
            // 
            // shapesTablePanel
            // 
            shapesTablePanel.BackColor = Color.White;
            shapesTablePanel.ColumnCount = 4;
            shapesTablePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            shapesTablePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            shapesTablePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            shapesTablePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            shapesTablePanel.Location = new Point(0, 0);
            shapesTablePanel.Margin = new Padding(0);
            shapesTablePanel.Name = "shapesTablePanel";
            shapesTablePanel.RowCount = 4;
            shapesTablePanel.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            shapesTablePanel.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            shapesTablePanel.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            shapesTablePanel.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            shapesTablePanel.Size = new Size(101, 103);
            shapesTablePanel.TabIndex = 13;
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
            timerFlowPanel.Location = new Point(94, -2);
            timerFlowPanel.Margin = new Padding(0);
            timerFlowPanel.Name = "timerFlowPanel";
            timerFlowPanel.Size = new Size(117, 52);
            timerFlowPanel.TabIndex = 8;
            // 
            // timerPanel
            // 
            timerPanel.BackColor = Color.Transparent;
            timerPanel.Controls.Add(timerButton);
            timerPanel.Location = new Point(0, 0);
            timerPanel.Margin = new Padding(0);
            timerPanel.Name = "timerPanel";
            timerPanel.Size = new Size(125, 52);
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
            timerButton.Location = new Point(0, 2);
            timerButton.Margin = new Padding(0);
            timerButton.Name = "timerButton";
            timerButton.Size = new Size(125, 50);
            timerButton.TabIndex = 9;
            timerButton.Text = "         &Timer (0)  ▼";
            timerButton.UseVisualStyleBackColor = false;
            timerButton.Click += timerButton_Click;
            // 
            // timerNoTimerPanel
            // 
            timerNoTimerPanel.Controls.Add(timerNoTimer);
            timerNoTimerPanel.Location = new Point(0, 52);
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
            timerPanel1.Location = new Point(0, 74);
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
            timerPanel2.Location = new Point(0, 96);
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
            timerPanel3.Location = new Point(0, 118);
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
            timerPanel4.Location = new Point(0, 140);
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
            timerPanel5.Location = new Point(0, 162);
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
            newScreenshotButton.Location = new Point(0, 1);
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
            topBarLabel.Size = new Size(709, 50);
            topBarLabel.TabIndex = 3;
            topBarLabel.Click += topBarLabel_Click;
            // 
            // deviderLabel
            // 
            deviderLabel.BackColor = Color.FromArgb(64, 64, 64);
            deviderLabel.Location = new Point(218, 8);
            deviderLabel.Name = "deviderLabel";
            deviderLabel.Size = new Size(1, 32);
            deviderLabel.TabIndex = 3;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 4;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            tableLayoutPanel2.Location = new Point(0, 0);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 3;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel2.Size = new Size(200, 100);
            tableLayoutPanel2.TabIndex = 0;
            // 
            // specialToolsTablePanel
            // 
            specialToolsTablePanel.BackColor = Color.FromArgb(224, 224, 224);
            specialToolsTablePanel.ColumnCount = 3;
            specialToolsTablePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            specialToolsTablePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            specialToolsTablePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            specialToolsTablePanel.Location = new Point(236, 2);
            specialToolsTablePanel.Margin = new Padding(0);
            specialToolsTablePanel.Name = "specialToolsTablePanel";
            specialToolsTablePanel.RowCount = 2;
            specialToolsTablePanel.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            specialToolsTablePanel.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            specialToolsTablePanel.Size = new Size(72, 48);
            specialToolsTablePanel.TabIndex = 13;
            // 
            // label1
            // 
            label1.BackColor = Color.FromArgb(64, 64, 64);
            label1.Location = new Point(320, 7);
            label1.Name = "label1";
            label1.Size = new Size(1, 32);
            label1.TabIndex = 17;
            // 
            // symbolOptionsTablePanel
            // 
            symbolOptionsTablePanel.BackColor = Color.FromArgb(224, 224, 224);
            symbolOptionsTablePanel.ColumnCount = 1;
            symbolOptionsTablePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            symbolOptionsTablePanel.Location = new Point(455, 1);
            symbolOptionsTablePanel.Name = "symbolOptionsTablePanel";
            symbolOptionsTablePanel.RowCount = 2;
            symbolOptionsTablePanel.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            symbolOptionsTablePanel.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            symbolOptionsTablePanel.Size = new Size(45, 48);
            symbolOptionsTablePanel.TabIndex = 15;
            // 
            // ScreenshotForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(255, 255, 252);
            ClientSize = new Size(709, 434);
            ControlBox = false;
            Controls.Add(size3Button);
            Controls.Add(symbolOptionsTablePanel);
            Controls.Add(size10Button);
            Controls.Add(label3);
            Controls.Add(label1);
            Controls.Add(label2);
            Controls.Add(specialToolsTablePanel);
            Controls.Add(colorPickerTablePanel);
            Controls.Add(deviderLabel);
            Controls.Add(newScreenshotButton);
            Controls.Add(symbolMainPanel);
            Controls.Add(timerFlowPanel);
            Controls.Add(picturePanel);
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
            KeyUp += ScreenshotForm_KeyUp;
            picturePanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)screenshotResultPicture).EndInit();
            symbolMainPanel.ResumeLayout(false);
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
        internal Panel picturePanel;
        private Label topBarLabel;
        private Label deviderLabel;
        private ReaLTaiizor.Controls.AirButton size3Button;
        private ReaLTaiizor.Controls.AirButton size10Button;
        private TableLayoutPanel shapesTablePanel;
        private TableLayoutPanel tableLayoutPanel2;
        private Panel symbolMainPanel;
        private Button expandSymbolsButton;
        private TableLayoutPanel specialToolsTablePanel;
        private TableLayoutPanel colorPickerTablePanel;
        private Label label1;
        private Label label2;
        private Label label3;
        private TableLayoutPanel symbolOptionsTablePanel;
    }
}