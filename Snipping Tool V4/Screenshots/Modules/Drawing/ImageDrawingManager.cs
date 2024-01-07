using Snipping_Tool_V4.Forms;
using System.Drawing.Drawing2D;

namespace Snipping_Tool_V4.Screenshots.Modules.Drawing
{
    public class ImageDrawingManager
    {

        public PictureBox pictureBox;
        public DrawingEraser userEraser = new DrawingEraser(); //WIP
        public Shape newShape;
        public List<Shape> shapeList = new List<Shape>();

        public bool isDrawing = false;
        public bool isCreatingSymbol = false;
        public bool isErasing = false;

        // user settings
        public DrawingShape chosenShape = DrawingShape.Freehand;
        public Color chosenColor = Color.Black;
        public int chosenSize = 3;
        public DrawingPen userPen;

        public ImageDrawingManager(PictureBox picturebox)
        {
            pictureBox = picturebox;
            pictureBox.MouseDown += picture_MouseDown;
            pictureBox.MouseMove += picture_MouseMove;
            pictureBox.MouseUp += picture_MouseUp;
            pictureBox.Paint += picture_Paint;
            newShape = new FreeHand();
        }

        public void picture_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            // Draw all previous shapes
            foreach (Shape shape in shapeList)
            {
                shape.Draw(e.Graphics, shape);
            }

            // Draw the current shape
            if (newShape.startPoint != new Point(0, 0))
            {
                newShape.Draw(e.Graphics, newShape);
            }
        }

        #region Mouse Events
        public void picture_MouseDown(object sender, MouseEventArgs e)
        {
            if (MouseButtons.Left == e.Button && pictureBox.Image != null)
            {
                if (isErasing)
                {
                    //WIP
                }
                else
                {
                    userPen = new DrawingPen(chosenColor, chosenSize);
                    switch (chosenShape)
                    {
                        case DrawingShape.Freehand:
                            newShape = new FreeHand(e.Location, userPen);
                            isDrawing = true;
                            break;
                        case DrawingShape.Rectangle:
                            newShape = new RectangleShape(e.Location, userPen);
                            newShape.CreateShape(e.Location);
                            pictureBox.Invalidate();
                            isCreatingSymbol = true;
                            break;
                        case DrawingShape.Line:
                            //    newShape = new LineShape(e.Location, userPen);
                            break;
                        case DrawingShape.Arrow:
                            //    newShape = new ArrowShape(e.Location, userPen);
                            break;
                        case DrawingShape.Ellipse:
                                  newShape = new EllipseShape(e.Location, userPen);
                                  isCreatingSymbol = true;
                            break;
                        case DrawingShape.Text:
                            //    newShape = new TextShape(e.Location, userPen);
                            break;
                        default:
                            break;
                    }
                }
            }
            if (e.Button == MouseButtons.Right && pictureBox.Image != null)
            {
                if (isCreatingSymbol || isDrawing || isErasing)
                {
                    // Cancel the drawing if user is drawing and presses right mouse button
                    controlRightMouseButtonDown();
                }
                else
                {
                    // Show the context menu where user can save/copy the image
                    imageContextMenu imageContextMenu2 = new imageContextMenu(pictureBox);
                    imageContextMenu2.ShowContextMenu(e.Location);        
                }
            }
        }

        public void picture_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDrawing)
            {
                newShape.currentDrawingPointss.Add(e.Location);
                pictureBox.Invalidate();
  
            }
            else if (isCreatingSymbol)
            {
                newShape.CreateShape(e.Location);
                pictureBox.Invalidate();
            }
            else if (isErasing)
            {

            }
        }
        public void picture_MouseUp(object sender, MouseEventArgs e)
        {
            if (isErasing)
            {
                
            }
            else
            {
                shapeList.Add(newShape);
            }

            isErasing = false;
            isCreatingSymbol = false;
            isDrawing = false;  
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
                newShape = new FreeHand(); //Reset the shape so we can remove it and it doesnt redraw itself
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
            newShape = new FreeHand(); //Reset the shape so we can remove it and it doesnt redraw itself
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
