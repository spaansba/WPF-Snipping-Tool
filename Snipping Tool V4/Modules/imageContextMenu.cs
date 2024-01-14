/// <summary>
/// This class is made to add a right click option to a picture to either copy or save the image
/// </summary>
public class imageContextMenu
{
    private ContextMenuStrip contextMenuStrip;
    private PictureBox pictureBox;

    public imageContextMenu(PictureBox pictureBox)
    {
        this.pictureBox = pictureBox;
        InitializeContextMenu();
        WireUpEvents();
    }

    private void InitializeContextMenu()
    {
        contextMenuStrip = new ContextMenuStrip();

        ToolStripMenuItem saveToolStripMenuItem = new ToolStripMenuItem("Save Image");
        saveToolStripMenuItem.Click += SaveToolStripMenuItem_Click;

        ToolStripMenuItem copyToolStripMenuItem = new ToolStripMenuItem("Copy Image");
        copyToolStripMenuItem.Click += CopyToolStripMenuItem_Click;

        contextMenuStrip.Items.AddRange(new ToolStripItem[] { saveToolStripMenuItem, copyToolStripMenuItem });

        pictureBox.ContextMenuStrip = contextMenuStrip;
    }
    private void WireUpEvents()
    {
        pictureBox.LostFocus += PictureBox_LostFocus;
        pictureBox.MouseUp += PictureBox_MouseUp;
    }

    private void PictureBox_LostFocus(object sender, EventArgs e)
    {
        if (contextMenuStrip != null)
        {
            contextMenuStrip.Dispose();
        }
    }

    private void PictureBox_MouseUp(object sender, MouseEventArgs e)
    {
        if (e.Button != MouseButtons.Right && contextMenuStrip != null)
        {
            contextMenuStrip.Dispose();
        }
    }

    private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (pictureBox.Image != null)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.FileName = "Screen Capture";
                saveFileDialog.Filter = "JPEG Image|*.jpg|Bitmap Image|*.bmp|GIF Image|*.gif|PNG Image|*.png";
                saveFileDialog.Title = "Save an Image File";
                saveFileDialog.ShowDialog();

                if (saveFileDialog.FileName != "")
                {
                    System.IO.FileStream fs = (System.IO.FileStream)saveFileDialog.OpenFile();
                    switch (saveFileDialog.FilterIndex)
                    {
                        case 1:
                            pictureBox.Image.Save(fs, System.Drawing.Imaging.ImageFormat.Jpeg);
                            break;
                        case 2:
                            pictureBox.Image.Save(fs, System.Drawing.Imaging.ImageFormat.Bmp);
                            break;
                        case 3:
                            pictureBox.Image.Save(fs, System.Drawing.Imaging.ImageFormat.Gif);
                            break;
                        case 4:
                            pictureBox.Image.Save(fs, System.Drawing.Imaging.ImageFormat.Png);
                            break;
                    }
                    fs.Close();
                }
            }
        }
    }
    public void ShowContextMenu(Point location)
    {
        if (contextMenuStrip != null)
        {
            contextMenuStrip.Show(pictureBox, location);
        }
    }
    private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (pictureBox.Image != null)
        {
            Clipboard.SetImage(pictureBox.Image);
        }
    }
}
