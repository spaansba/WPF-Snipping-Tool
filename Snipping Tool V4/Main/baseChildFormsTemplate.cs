using Snipping_Tool_V4.Main;
using System.Diagnostics;

namespace Snipping_Tool_V4.Forms
{
    public partial class baseChildFormsTemplate : Form
    {
        public static readonly Color TopBarColor = Color.FromArgb(224, 224, 224);

        // Method called from the childs to deactive form and put it back to original values
        public void deactivateForm(baseChildFormsTemplate formToDeactivate, MainForm mainform, bool closeForm)
        {
            mainform.Width = (int)MainFormMeasurements.formWidth;
            mainform.Height = (int)MainFormMeasurements.formHeight;
            if (closeForm) { this.Close(); }
        }
        public static void PrintLoadedFormsDetails()
        {
            Debug.WriteLine($"Total number of loaded forms: {Application.OpenForms.Count}");

            foreach (baseChildFormsTemplate form in Application.OpenForms)
            {
                Debug.WriteLine($"Form Name: {form.Name}, Type: {form.GetType().FullName}");
            }
        }
    }
}
