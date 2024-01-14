namespace Snipping_Tool_V4.Forms
{
    public partial class SettingsForm : baseChildFormsTemplate
    {
        private MainForm mainForm;
        public SettingsForm(MainForm mainform)
        {
            InitializeComponent();
            mainForm = mainform;

        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            this.ControlBox = false;
        }

        private void testButton_Click(object sender, EventArgs e)
        {
        }
    }
}
