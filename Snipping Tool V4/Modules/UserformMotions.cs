using Snipping_Tool_V4.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snipping_Tool_V4.Modules
{
    public class UserformMotions
    {
        /// <summary>
        /// Each moving object has a size value of closed and opened (either width or height doesnt matter)
        /// </summary>
        internal int openedSize;
        internal int closedSize;
        internal bool isWidth; // false = height, true = width change
        internal string objectInMotion;
        internal int pixelsPerMotion;
        internal Panel panel;
        internal bool expanded;
        internal bool endOfMotion = false;
        internal string formName;
        internal Button parentButton;
        internal List<Button> childButtons;

        // For on button dropdown boxes
        public const string triangleDown = "▼";
        public const string triangleUp = "▲";

        internal static List<UserformMotions> AllMotions = []; // Static list to store all objects


        public UserformMotions(int openedValue, int closedValue, bool widthOrHeight, string currentObject, int pixels,
            Panel currentPanel, bool currentlyExpanded, string formname, Button currentButton = null, List<Button> currentChildButtons = null)

        {
            openedSize = openedValue;
            closedSize = closedValue;
            isWidth = widthOrHeight;
            objectInMotion = currentObject;
            pixelsPerMotion = pixels;
            panel = currentPanel;
            parentButton = currentButton;
            expanded = currentlyExpanded;
            formName = formname;
            childButtons = currentChildButtons;

            AllMotions.Add(this);
            this.formName = formName;
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
        /// <summary>
        /// Whenever a new dropdown gets opened, close all the other dropdowns
        /// </summary>
        /// <param name="except">Close all dropdowns except this one</param>
        public static void closeAllDropdownsExcept(UserformMotions? except = null)
        {
            foreach (UserformMotions motionObject in AllMotions)
            {
                if (motionObject != null && motionObject != except && motionObject.formName != "Main")
                {
                    motionObject.expanded = false;

                    if (motionObject.parentButton != null)
                    {
                        motionObject.parentButton.Text = motionObject.parentButton.Text.Replace(triangleUp, triangleDown);
                    }

                    if (motionObject.isWidth)
                    {
                        motionObject.panel.Width = motionObject.closedSize;
                    }
                    else
                    {
                        motionObject.panel.Height = motionObject.closedSize;

                    }
                }
            }
        }
        /// <summary>
        /// Color the menu item that is currently selected in the dropdown menu's
        /// </summary>
        public static void colorActiveMenuItem(List<Button> childButtons, Button selected = null)
        {
            foreach (Button button in childButtons)
            {
                button.BackColor = button == selected ? SystemColors.ControlLight : SystemColors.ButtonFace;
            }
        }

        /// <summary>
        /// All dropdown menus in the child forms have an triangle showing if the menu is open or not, this method changes that triangle to up or down
        /// </summary>
        /// <param name="motionObject">The object on which the parent button we are changing the triangle</param>
        public static void setTriangleOnButton(UserformMotions motionObject)
        {
            Button parent = motionObject.parentButton;
            parent.Text = motionObject.expanded ?
            parent.Text.Replace(triangleDown, triangleUp) :
            parent.Text.Replace(triangleUp, triangleDown);
        }

        /// <summary>
        /// Some dropdown menus contain a () with the current value (e.g timer), change the value between these () with this method
        /// </summary>
        /// <param name="buttonTextToChange">The parent button on which the value gets changed</param>
        /// <param name="newValue">the new value that will end up between the parentheses</param>
        public static void changeValueBetweenParentheses(Button buttonTextToChange, string newValue)
        {
            // Find the position of the opening and closing parentheses
            int startIndex = buttonTextToChange.Text.IndexOf('(');
            int endIndex = buttonTextToChange.Text.IndexOf(')');

            if (startIndex != -1 && endIndex != -1 && endIndex > startIndex + 1)
            {
                // Extract the current value between parentheses
                string currentValue = buttonTextToChange.Text.Substring(startIndex + 1, endIndex - startIndex - 1);

                // Replace the current value with the new delay
                string newText = buttonTextToChange.Text.Replace(currentValue, newValue);

                // Set the updated text to the button
                buttonTextToChange.Text = newText;
            }
        }
    }
}
