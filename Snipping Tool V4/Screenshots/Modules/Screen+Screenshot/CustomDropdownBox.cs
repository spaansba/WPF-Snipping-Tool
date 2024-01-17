using ReaLTaiizor.Controls;
using Snipping_Tool_V4.Modules;
using Snipping_Tool_V4.Screenshots.Modules.Drawing;
using Snipping_Tool_V4.Screenshots.Modules.Drawing.Tools;
using System.ComponentModel;
using System.Configuration;
using System.Windows.Forms;

namespace Snipping_Tool_V4.Screenshots.Modules.Screen_Screenshot
{
    public abstract class CustomDropdownBox : ComboBox

    {
        public virtual ImageList imageList { get; set; }
        public abstract int ListCount { get; }

        public CustomDropdownBox()
        {
            this.DropDownWidth = 90;
            this.ItemHeight = 26;
            this.Width = 45;
            this.DrawMode = DrawMode.OwnerDrawVariable; // Otherwise we cant change the Size properties
            this.DropDownStyle = ComboBoxStyle.DropDownList; // To disable typing in the ComboBox
            this.FlatStyle = FlatStyle.Flat;
            this.Margin = new Padding(0);
            this.BackColor = Color.FromArgb(224, 224, 224);
            this.Region = new Region(new Rectangle(1, 1, this.Width - 20, this.Height - 1)); //Remove the white border and the dropdown arrow
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            Image image = imageList.Images[e.Index];

            // Adding 2px border around image for better allignment
            int borderSize = 2;
            Bitmap borderedImage = new Bitmap(image.Width + 2 * borderSize, image.Height + 2 * borderSize);
            using (Graphics g = Graphics.FromImage(borderedImage))
            {
                g.DrawImage(image, borderSize, borderSize, image.Width, image.Height);
            }

            e.Graphics.DrawImage(borderedImage, e.Bounds.Left + 2, e.Bounds.Top + 2);

            // Draw a vertical line as a divider between pic and title
            int lineX = e.Bounds.Left + imageList.ImageSize.Width + 11;
            int lineYStart = e.Bounds.Top;
            int lineYEnd = e.Bounds.Bottom;
            e.Graphics.DrawLine(Pens.Gray, lineX, lineYStart, lineX, lineYEnd);

            // Draw the text next to the divider
            e.Graphics.DrawString(this.Items[e.Index].ToString(), e.Font,
                SystemBrushes.ControlText, lineX + 5, e.Bounds.Top);
        }
    }

    public sealed class ShapeFillDropdown : CustomDropdownBox
    {
        public override ImageList imageList { set => base.imageList = value;}

        public override int ListCount => throw new NotImplementedException();

        public ShapeFillDropdown()
        {
            imageList = new ImageList();
            imageList.Images.Add(Properties.Resources.BucketTool);
            imageList.Images.Add(Properties.Resources.SelectionTool);
            this.imageList = imageList;
            this.Items.Add("No Fill");
            this.Items.Add("Solid Fill");
            this.SelectedIndex = 0;
        }
    }
}
