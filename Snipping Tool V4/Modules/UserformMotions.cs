using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snipping_Tool_V4.Modules
{
    public class UserformMotions
    {
        /// <summary>
        /// Each moving object has a size value of closed and opened (either width or height doesnt matter)
        /// </summary>
        public int openedSize;
        public int closedSize;
        public bool isWidth; // false = height, true = width change
        public string objectInMotion;
        public int pixelsPerMotion;
        public Panel panel;
        public bool expanded;
        public bool endOfMotion = false;



        public UserformMotions(int openedValue, int closedValue, bool widthOrHeight, string currentObject, int pixels,
            Panel currentPanel, bool currentlyExpanded)

        {
            openedSize = openedValue;
            closedSize = closedValue;
            isWidth = widthOrHeight;
            objectInMotion = currentObject;
            pixelsPerMotion = pixels;
            panel = currentPanel;
            expanded = currentlyExpanded;
        }
        public UserformMotions()
        {

        }

        /// <summary>
        /// Method that changes either the width or the height of the userFormMotions object
        /// </summary>
        /// <param name="obj"></param>
        public static void MotionChange(UserformMotions obj)
        {
            int motionSize = obj.isWidth ? obj.panel.Width : obj.panel.Height;

            if (obj.expanded) // Closing the object
            {
                if ((motionSize - obj.pixelsPerMotion) <= obj.closedSize) // If close size has been reached
                {
                    if (obj.isWidth)
                        obj.panel.Width = obj.closedSize;
                    else
                        obj.panel.Height = obj.closedSize;

                    obj.expanded = false;
                    obj.endOfMotion = true;
                    return;
                }
                else // If close size has not been reached, shrink the object with X amount
                {
                    if (obj.isWidth)
                        obj.panel.Width -= obj.pixelsPerMotion;
                    else
                        obj.panel.Height -= obj.pixelsPerMotion;
                }
            }
            else // Expanding the object
            {
                if ((motionSize + obj.pixelsPerMotion) >= obj.openedSize) // If max expantion of object has been reached
                {
                    if (obj.isWidth)
                        obj.panel.Width = obj.openedSize;
                    else
                        obj.panel.Height = obj.openedSize;

                    obj.expanded = true;
                    obj.endOfMotion = true;
                    return;
                }
                else // if max expantion of object has not been reached, expand the object with X amount
                {
                    if (obj.isWidth)
                        obj.panel.Width += obj.pixelsPerMotion;
                    else
                        obj.panel.Height += obj.pixelsPerMotion;
                }
            }
        }
    }

}
