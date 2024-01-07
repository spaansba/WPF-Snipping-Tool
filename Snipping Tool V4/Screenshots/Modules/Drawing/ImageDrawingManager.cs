using Snipping_Tool_V4.Forms;
using System.Drawing.Drawing2D;

namespace Snipping_Tool_V4.Screenshots.Modules.Drawing
{
    public class ImageDrawingManager
    {

        public PictureBox pictureBox;
        public DrawingEraser userEraser = new DrawingEraser(); //WIP
        public ShapeTool newShape;
        public List<Shape> shapeList = new List<Shape>();

        public bool isDrawing = false;
        public bool isCreatingSymbol = false;
        public bool isErasing = false;

        // user settings
        public DrawingShape chosenShape = DrawingShape.Freehand;
        public Color chosenColor = Color.Black;
        public int chosenSize = 3;
        public DrawingPen userPen;

        public Tool CurrentTool { get; set; }

        public ImageDrawingManager(PictureBox picturebox)
        {
            pictureBox = picturebox;
            pictureBox.MouseDown += picture_MouseDown;
            pictureBox.MouseMove += picture_MouseMove;
            pictureBox.MouseUp += picture_MouseUp;
            pictureBox.Paint += picture_Paint;
        //    newShape = new FreeHand();
        }

        public void picture_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            // Draw all previous shapes
            foreach (Shape shape in shapeList)
            {
            //    shape.Draw(e.Graphics, shape);
            }

            if (this.CurrentTool != null && this.CurrentTool.IsActive)
            {
                this.CurrentTool.Draw(e.Graphics);
            }
        }

        #region Mouse Events
        public void picture_MouseDown(object sender, MouseEventArgs e)
        {
            if(pictureBox.Image == null)
            {
                return;
            }

            switch (e.Button, CurrentTool)
            {
                case (MouseButtons.Left, not null):
                    userPen = new DrawingPen(chosenColor, chosenSize);
                    CurrentTool.Begin(e.Location, userPen);
                    break;
                case (MouseButtons.Right, not null):
                    CurrentTool = null;
                    pictureBox.Invalidate();
                    break;
                case (MouseButtons.Right, null):
                    imageContextMenu imageContextMenu2 = new imageContextMenu(pictureBox);
                    imageContextMenu2.ShowContextMenu(e.Location);
                    break;
            }
        }

        public void picture_MouseMove(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left && CurrentTool != null && CurrentTool.IsActive)
            {
                CurrentTool.Continue(e.Location);
                pictureBox.Invalidate();
            }
        }
        public void picture_MouseUp(object sender, MouseEventArgs e)
        {
            if (CurrentTool != null)
            {
                CurrentTool.Finish(e.Location, this.shapeList);
                pictureBox.Invalidate();
            }
        }
        #endregion
        #region control events
        /// <summary>
        /// If user presses CTRL Z remove the last points from the list and redwaw the picturebox
        /// </summary>
        public void controlZOnDrawing()
        {
            if (shapeList.Count > 0)
            {
            //    newShape = new FreeHand(); //Reset the shape so we can remove it and it doesnt redraw itself
                shapeList.RemoveAt(shapeList.Count - 1);
                pictureBox.Invalidate();
            }
        }
        /// <summary>
        /// If the user presses the right mouse button while drawing, cancel the drawing
        /// </summary>
        public void controlRightMouseButtonDown()
        {
            isDrawing = false;
            isCreatingSymbol = false;
            isErasing = false;
        //    newShape = new FreeHand(); //Reset the shape so we can remove it and it doesnt redraw itself
            pictureBox.Invalidate();
        }

        public void ClearDrawings()
        {
            shapeList.Clear();
            pictureBox.Invalidate();
        }
        #endregion
    }
}
