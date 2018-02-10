namespace LineDrawer.Common
{
    /// <summary>
    /// Handles the display window title, width and height
    /// </summary>
    public class DisplayWindowConfiguration
    {
        public DisplayWindowConfiguration() : this("Powerered by DirectX (C# SharpDX)") {
        }


        public DisplayWindowConfiguration(string windowTitle) : this(windowTitle, 800, 600)
        {
        }

        public DisplayWindowConfiguration(string windowTitle, int width, int height, bool showFramePerSecond = true)
        {
            WindowTitle = windowTitle;
            Width = width;
            Height = height;
            WaitVerticalBlanking = false;
            ShowFramePerSecond = showFramePerSecond;
        }

        /// <summary>
        /// Gets or set the WindowTitle
        /// </summary>
        public string WindowTitle {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the width of the window.
        /// </summary>
        public int Width {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the height of the window.
        /// </summary>
        public int Height {
            get;
            set;
        }

        /// <summary> 
        /// DirectX SwapChain synchInterval.
        /// Specifies how to synchronize presentation of a frame with the vertical blank.
        /// </summary>
        /// <value>
        /// 	True : Synchronize presentation after the last vertical blank.
        ///     False: The presentation occurs immediately, there is no synchronization.
        /// </value>
        /// https://msdn.microsoft.com/en-us/library/windows/desktop/bb174576%28v=vs.85%29.aspx
        public bool WaitVerticalBlanking
        {
            get; set;
        }

        public bool ShowFramePerSecond { get; set; }
    }
}
