using System.Net.NetworkInformation;

namespace Snipping_Tool_V4.Forms
{
    partial class baseChildFormsTemplate
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

        /// <summary>
        /// This method shows a popup message at the bottom left of a mdi child form
        /// </summary>
        /// <param name="message">Message that shows up in the pop-up</param>
        /// <param name="childForm">the form the message gets displayed on</param>
        /// <param name="popupShownTime">How long the popup will be visible in MS</param>
        public static void ShowMessage(string message, baseChildFormsTemplate childForm, int popupShownTime = 2000)
        {
            removePreviousLabels(childForm);
            Label messageLabel = new Label
            {
                Location = new Point(10, childForm.ClientSize.Height - 30), //Add it to bottom left of childform
                AutoSize = true,
                Padding = new Padding(5),
                Text = message,
                Name = "Popup Label",
                Tag = "Popup Label",
                Visible = true,
                Font = new Font("Microsoft Sans Serif", 9.75f),
                ForeColor = Color.Black,
                BackColor = Color.BlanchedAlmond
            };

            childForm.Controls.Add(messageLabel);
            messageLabel.BringToFront();

            // Hide the label after x seconds 
            Task.Delay(popupShownTime).ContinueWith(_ =>
            {
                childForm.Invoke((MethodInvoker)delegate
                {
                    childForm.Controls.Remove(messageLabel);
                    messageLabel.Dispose();
                });
            });
        }

        /// <summary>
        /// Sometimes when multiple labels get called in a row it overlaps before the time runs out. In that case remove them like this.
        /// </summary>
        public static void removePreviousLabels(baseChildFormsTemplate childForm)
        {
            foreach (Control control in childForm.Controls)
            {
                if (control.Tag != null && control.Tag.ToString() == "Popup Label")
                {
                    childForm.Controls.Remove(control);
                    control.Dispose();
                }
            }
        }
        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Text = "baseChildFormsTemplate";
        }

        #endregion
    }
}