namespace Snipping_Tool_V4.Forms
{
    partial class SettingsForm
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
            testButton = new ReaLTaiizor.Controls.AirButton();
            SuspendLayout();
            // 
            // testButton
            // 
            testButton.Customization = "7e3t//Ly8v/r6+v/5ubm/+vr6//f39//p6en/zw8PP8UFBT/gICA/w==";
            testButton.Font = new Font("Segoe UI", 9F);
            testButton.Image = null;
            testButton.Location = new Point(163, 116);
            testButton.Name = "testButton";
            testButton.NoRounding = false;
            testButton.Size = new Size(243, 181);
            testButton.TabIndex = 0;
            testButton.Text = "airButton1";
            testButton.Transparent = false;
            testButton.Click += testButton_Click;
            // 
            // SettingsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(539, 395);
            Controls.Add(testButton);
            Name = "SettingsForm";
            Text = "SettingsForm";
            Load += SettingsForm_Load;
            ResumeLayout(false);
        }

        #endregion

        private ReaLTaiizor.Controls.AirButton testButton;
    }
}