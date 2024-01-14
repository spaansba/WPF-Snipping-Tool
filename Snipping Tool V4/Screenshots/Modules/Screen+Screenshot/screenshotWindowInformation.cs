using Shell32;
using Snipping_Tool_V4.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace Snipping_Tool_V4.Screenshots.Modules
{
    public class WindowInformation
    {
        #region Window API calls
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool EnumWindows(EnumWindowsProc lpEnumFunc, nint lParam);
        private delegate bool EnumWindowsProc(nint hWnd, nint lParam);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool IsWindowVisible(nint hWnd);

        [DllImport("user32.dll")]
        private static extern bool IsIconic(nint hWnd); // Check if window is minimized

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern int GetWindowText(nint hWnd, StringBuilder lpString, int nMaxCount);

        [DllImport("user32.dll")]
        private static extern nint GetWindow(nint hWnd, uint uCmd);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool PtInRect(ref RECT lprc, Point pt); // is point in a rectangle
        [DllImport("user32.dll")]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        Process[] processes = Process.GetProcessesByName("processname");

        #endregion
        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public Point TopLeft;
            public Point BottomRight;

            // Helper property to check if the rectangle is empty
            public bool IsEmpty => TopLeft == Point.Empty && BottomRight == Point.Empty;
        }

        // Import the GetWindowInfo function from the user32.dll library
        [DllImport("user32.dll")]
        public static extern bool GetWindowInfo(nint hwnd, ref WINDOWINFO pwi);

        [StructLayout(LayoutKind.Sequential)]
        public struct WINDOWINFO
        {
            public uint cbSize;
            public RECT rcWindow;
            public RECT rcClient;
            public uint dwStyle;
            public uint dwExStyle;
            public uint dwWindowStatus;
            public uint cxWindowBorders;
            public uint cyWindowBorders;
            public ushort atomWindowType;
            public ushort wCreatorVersion;
        }

        public List<(nint Handle, string Title, RECT RectangleLocation, int ZOrder, List<(nint overlappingHandle, string overlappingTitle, RECT overlappingRect)> OverlappingWindows)> VisibleParentWindows { get; private set; }
        public int currentWindowTupleOnTop = 0;


        public WindowInformation()
        {
            VisibleParentWindows = new List<(nint Handle, string Title, RECT RectangleLocation, int ZOrder, List<(nint overlappingHandle, string overlappingTitle, RECT overlappingRect)> OverlappingWindows)>();
            CreateListOfAllWindowInformation();
            SortWindowsByZOrderAscending();
            AddOverlappingWindows();
            DebugWindowList();
        }
        #region debugging
        /// <summary>
        /// Call this method to debug a full list of the windows for debugging
        /// </summary>
        public void DebugWindowList()
        {
            // Display information about visible parent windows in this form
            foreach (var (hWnd, title, rect, zOrder, overlapping) in VisibleParentWindows)
            {
                Debug.WriteLine($"Window Handle: {hWnd}, Title: {title}, TopLeft: {rect.TopLeft}, BottomRight: {rect.BottomRight}, ZOrder: {zOrder}");
            }
        }
        #endregion
        #region Populate the VisibleParentWindow information List and sort it
        private void CreateListOfAllWindowInformation()
        {
            VisibleParentWindows.Add((0, "Starter Value For First Loop", new RECT(), 0, new List<(nint overlappingHandle, string overlappingTitle, RECT overlappingRect)>()));
            EnumWindows((hWnd, lParam) =>
            {
                if (IsWindowVisible(hWnd))
                {
                    if (!IsIconic(hWnd)) // Skip minimized windows
                    {
                        // Initialize the WINDOWINFO structure
                        WINDOWINFO windowInfo = new WINDOWINFO();
                        windowInfo.cbSize = (uint)Marshal.SizeOf(typeof(WINDOWINFO));

                        GetWindowInfo(hWnd, ref windowInfo); // we dont use GetClientRect or GetWindowRect as those are not correct
                        RECT rect = windowInfo.rcClient;

                        StringBuilder title = new StringBuilder(256);
                        GetWindowText(hWnd, title, title.Capacity);

                        // Check if the window's RECT is not empty and has a title
                        // No title typically belongs to system windows or specialized applications that don't require user interaction
                        if (!rect.IsEmpty && title.Length > 0 && RemoveOverlayWindows(rect))
                        {
                            bool z = GetWindowZOrder(hWnd, out int zOrder);
                            VisibleParentWindows.Add((hWnd, title.ToString(), rect, zOrder, new List<(nint overlappingHandle, string overlappingTitle, RECT overlappingRect)>())); // Assign an empty List<RECT> we add this later
                        }
                    }
                }
                return true; // Continue enumeration
            }, nint.Zero);

            // add the background of all screens in there as well with a Zscore of 1
            foreach (var screen in Screen.AllScreens)
            {
                RECT screenRect = new RECT
                {
                    TopLeft = new Point(screen.Bounds.Left, screen.Bounds.Top),
                    BottomRight = new Point(screen.Bounds.Right, screen.Bounds.Bottom)
                };
                VisibleParentWindows.Add((nint.Zero, $"Screen: {screen.DeviceName}", screenRect, 1, new List<(nint, string, RECT)>()));
            }

        }

        /// <summary>
        /// Highest Z order means on top of the screen and lowest Z order means behind all other windows
        /// </summary>
        private bool GetWindowZOrder(nint hwnd, out int zOrder)
        {
            const uint GW_HWNDPREV = 3;
            const uint GW_HWNDLAST = 1;

            nint lowestHwnd = GetWindow(hwnd, GW_HWNDLAST);
            int z = 0;
            nint hwndTmp = lowestHwnd;

            while (hwndTmp != nint.Zero)
            {
                if (hwnd == hwndTmp)
                {
                    zOrder = z;
                    return true;
                }

                hwndTmp = GetWindow(hwndTmp, GW_HWNDPREV);
                z++;
            }

            zOrder = int.MinValue;
            return false;
        }

        /// <summary>
        /// this method removes some invisible windows like NVIDIA overlay
        /// </summary>
        private bool RemoveOverlayWindows(RECT rect)
        {
            int screenHeigth = SystemInformation.VirtualScreen.Height;

            if (rect.TopLeft.X <= 0 && rect.TopLeft.Y <= 0 && rect.BottomRight.Y >= screenHeigth - 5) // - 5 because screens are fucked
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// For easier access and debugging we order the list by Z order from low to high
        /// </summary>
        private void SortWindowsByZOrderAscending()
        {
            VisibleParentWindows = VisibleParentWindows.OrderBy(thing => thing.ZOrder).ToList();
        }
        /// <summary>
        /// We only want the part of the window that is visible, to get this we get the whole window and add a list of overlapping windows
        /// We only add overlapping windows if they have a higher Z order than the original window in question (meaning they are above the window)
        /// </summary>
        private void AddOverlappingWindows()
        {
            for (int i = 0; i < VisibleParentWindows.Count; i++)
            {
                var currentWindow = VisibleParentWindows[i];

                for (int j = i + 1; j < VisibleParentWindows.Count; j++) // Start from i as the Z orders are in ascending order
                {
                    var otherWindow = VisibleParentWindows[j];

                    // Check if the other window overlaps with the current window
                    if (RectanglesIntersect(currentWindow.RectangleLocation.TopLeft, currentWindow.RectangleLocation.BottomRight,
                        otherWindow.RectangleLocation.TopLeft, otherWindow.RectangleLocation.BottomRight))
                    {
                        currentWindow.OverlappingWindows.Add((otherWindow.Handle, otherWindow.Title, otherWindow.RectangleLocation));
                    }
                }

                bool visibleWindow = IsPixelInAnyOverlapWindow(currentWindow);

                if (visibleWindow)
                {
                    // Remove the currentWindow from the VisibleParentWindows list
                    VisibleParentWindows.RemoveAt(i);
                    i--; // Decrement i as the list size has been reduced by removing an element
                }
            }
        }
        /// <summary>
        /// This method loops through each pixel of the current window and checks 
        /// all overlapping windows if that pixel is availeble. If every pixel is in any of the overlapping windows return true
        /// if a pixel is not in the overlapping windows it means the window is visible and return false
        /// </summary>
        /// <param name="currentWindow">the currentwindow of the loop of AddOverlappingWindows</param>
        /// <returns>true = window is visible somehow, false = window is not visible by the user</returns>
        private bool IsPixelInAnyOverlapWindow((nint Handle, string Title, RECT RectangleLocation, int ZOrder, List<(nint overlappingHandle, string overlappingTitle, RECT overlappingRect)> OverlappingWindows) currentWindow)
        {
            // loop over each pixel in currentWindow.RectangleLocation
            for (int x = currentWindow.RectangleLocation.TopLeft.X; x <= currentWindow.RectangleLocation.BottomRight.X; x++)
            {
                for (int y = currentWindow.RectangleLocation.TopLeft.Y; y <= currentWindow.RectangleLocation.BottomRight.Y; y++)
                {
                    bool pixelIsInOverlapWindow = false;
                    // Check if the pixel coordinates (x, y) fall within any of the overlapping windows
                    foreach (var overlapWindow in currentWindow.OverlappingWindows)
                    {
                        if (IsCoordinateInsideRectangle(x, y, overlapWindow.overlappingRect))
                        {
                            pixelIsInOverlapWindow = true;
                        }
                    }
                    if (!pixelIsInOverlapWindow)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        // Helper method to check if a coordinate falls within a rectangle
        private bool IsCoordinateInsideRectangle(int x, int y, RECT rectangle)
        {
            return x >= rectangle.TopLeft.X && x <= rectangle.BottomRight.X &&
                   y >= rectangle.TopLeft.Y && y <= rectangle.BottomRight.Y;
        }

        /// <summary>
        /// Check if 2 rectangles overlap, based on: https://www.geeksforgeeks.org/find-two-rectangles-overlap/
        /// </summary>
        /// <param name="l1">Top left of first rect</param>
        /// <param name="r1">Bottom right of first rect</param>
        /// <param name="l2">Top left of second rect</param>
        /// <param name="r2">bottom right of sscond rect</param>
        /// <returns>bool if the rects intersect or not</returns>
        public bool RectanglesIntersect(Point l1, Point r1, Point l2, Point r2)
        {
            // if rectangle has area 0, no overlap
            if (l1.X == r1.X || l1.Y == r1.Y || r2.X == l2.X || l2.Y == r2.Y)
            {
                return false;
            }

            // If one rectangle is on left side of other 
            if (l1.X > r2.X || l2.X > r1.X)
            {
                return false;
            }

            // If one rectangle is above the other 
            if (r1.Y <= l2.Y || r2.Y <= l1.Y)
            {
                return false;
            }
            return true;
        }


        #endregion

        public void TopWindowUnderMouse(Point mouseLocation, TakingScreenShot form)
        {
            var previousTuple = VisibleParentWindows[currentWindowTupleOnTop];
            bool checkForNewWindow = true;

            // check if the new mouse location is within the previous window on top
            if (PtInRect(ref previousTuple.RectangleLocation, mouseLocation))
            {
                // make sure the mouse location is not in a overlapping window with higher Z score
                for (int j = 0; j < previousTuple.OverlappingWindows.Count; j++)
                {
                    checkForNewWindow = false;
                    var overlappingWindow = previousTuple.OverlappingWindows[j];
                    if (PtInRect(ref overlappingWindow.overlappingRect, mouseLocation))
                    {
                        checkForNewWindow = true;
                        break;
                    }
                }
            }

            if (checkForNewWindow)
            {
                for (int i = VisibleParentWindows.Count - 1; i >= 0; i--) // ran in reverser order to start with highest Z score
                {
                    var windowTuple = VisibleParentWindows[i];
                    bool isCursorInsideWindow = PtInRect(ref windowTuple.RectangleLocation, mouseLocation);
                    if (isCursorInsideWindow)
                    {
                        currentWindowTupleOnTop = i;
                        break;
                    }
                }
            }

        }
        public void BringWindowToFront(IntPtr handle, string title = "nothing")
        {
            // if its a screen, show background otherwise highlight the window
            if (title.StartsWith("Screen:"))
            {
                ShellClass objShel = new ShellClass();
                objShel.ToggleDesktop();
                Thread.Sleep(500); // make time to show the desktop otherwise picture will be still light gray
            }
            else
            {
                SetForegroundWindow(handle);
            }
        }
    }
}