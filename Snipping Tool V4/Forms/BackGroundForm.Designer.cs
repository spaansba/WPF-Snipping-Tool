namespace Snipping_Tool_V4.Forms
{
    partial class BackGroundForm
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
            SuspendLayout();
            // 
            // BackGroundForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            FormBorderStyle = FormBorderStyle.None;
            Name = "BackGroundForm";
            StartPosition = FormStartPosition.Manual;
            Text = "BackGroundForm";
            Load += BackGroundForm_Load;
            Paint += BackGroundForm_Paint;
            MouseDown += BackGroundForm_MouseDown;
            MouseMove += BackGroundForm_MouseMove;
            MouseUp += BackGroundForm_MouseUp;
            ResumeLayout(false);
        }

        #endregion
    }
}