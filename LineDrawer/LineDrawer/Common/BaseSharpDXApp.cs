using System;
using System.Windows.Forms;
using SharpDX.Windows;

namespace LineDrawer.Common
{
    /// <summary>
    /// Base class for a SharpDX 2D/3D application
    /// 
    /// Creates the display window based on the DisplayConfiguration specs
    /// Handles the main loop, Initializes and Disposes DirectX 
    /// Attaches Mouse and key events 
    /// </summary>
    public abstract class BaseSharpDXApp
    {
        private readonly Timer clock = new Timer();
        private FormWindowState _currentFormWindowState;
        private bool _disposed;
        private Form _form;
        private float _frameAccumulator;
        private int _frameCount;
        private DisplayWindowConfiguration _displayWindowConfiguration;

    #region "Destructors and Dispose functions"

        /// <summary>
        ///   Performs object finalization.
        /// </summary>
        ~BaseSharpDXApp()
        {
            if (!_disposed)
            {
                Dispose(false);
                _disposed = true;
            }
        }

        /// <summary>
        ///   Disposes of object resources.
        /// </summary>
        public void Dispose()
        {
            if (!_disposed)
            {
                Dispose(true);
                _disposed = true;
            }
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///   Disposes of object resources.
        /// </summary>
        /// <param name = "disposeManagedResources">If true, managed resources should be
        ///   disposed of in addition to unmanaged resources.</param>
        protected virtual void Dispose(bool disposeManagedResources)
        {
            if (disposeManagedResources)
            {
                if (_form != null)
                    _form.Dispose();
            }
        }
    #endregion

        /// <summary>
        /// Create the diplay Form.  can be overriden.
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        protected virtual Form CreateForm(DisplayWindowConfiguration config)
        {
            return new RenderForm(config.WindowTitle)
            {
                ClientSize = new System.Drawing.Size(config.Width, config.Height)
            };
        }

        /// <summary>
        /// Runs the demo with default presentation
        /// </summary>
        public void Run()
        {
            Run(new DisplayWindowConfiguration());
        }

        /// <summary>
        /// Runs the demo.
        /// </summary>
        public void Run(DisplayWindowConfiguration displayWindowConfiguration)
        {
            _displayWindowConfiguration = displayWindowConfiguration ?? new DisplayWindowConfiguration();
            _form = CreateForm(_displayWindowConfiguration);
            Initialize(_displayWindowConfiguration);

            bool isFormClosed = false;
            bool formIsResizing = false;

            _form.MouseMove += HandleMouseMove;
            _form.MouseClick += HandleMouseClick;
            _form.MouseDown += HandleMouseDown;
            _form.MouseUp += HandleMouseUp;
 
            _form.KeyDown += HandleKeyDown;
            _form.KeyUp += HandleKeyUp;

            _form.Resize += (o, args) =>
            {
                if (_form.WindowState != _currentFormWindowState)
                {
                    HandleResize(o, args);
                }

                _currentFormWindowState = _form.WindowState;
            };

            _form.ResizeBegin += (o, args) => { formIsResizing = true; };
            _form.ResizeEnd += (o, args) =>
            {
                formIsResizing = false;
                HandleResize(o, args);
            };

            _form.Closed += (o, args) => { isFormClosed = true; };

            LoadContent();

            clock.Start();
            BeginRun();
            RenderLoop.Run(_form, () =>
            {
                if (isFormClosed)
                {
                    return;
                }

                OnUpdate();
                if (!formIsResizing)
                    Render();
            });

            UnloadContent();
            EndRun();

            // Dispose explicity
            Dispose();
        }

        /// <summary>
        ///   In a derived class, implements logic to initialize the sample.
        /// </summary>
        protected abstract void Initialize(DisplayWindowConfiguration displayWindowConfiguration);

        protected virtual void LoadContent()
        {
        }

        protected virtual void UnloadContent()
        {
        }

        /// <summary>
        /// The Update method is called once per game loop -  It has a simple purpose: update 
        /// scene geometry and game state based on the amount of elapsed time (or elapsed time steps) 
        /// since the previous frame. 
        /// https://msdn.microsoft.com/en-us/library/windows/desktop/dn643746(v=vs.85).aspx 
        /// 
        /// Since communication between the CPU and GPU incurs overhead, make sure you only update buffers 
        /// that have actually changed since the last frame - your constant buffers can be grouped, or split
        /// up, as needed to make this more efficient.
        /// 
        /// </summary>
        protected virtual void Update(Timer time)
        {
        }


        protected virtual void Draw(Timer time)
        {
        }

        protected virtual void BeginRun()
        {
        }

        protected virtual void EndRun()
        {
        }

        /// <summary>
        /// 2D and 3D rendering employs different mechanismn; this function is handled by SharpDx2D11Base or SharpDx3D11Base
        /// </summary>
        protected virtual void BeginDraw()
        {
        }

        /// <summary>
        /// 2D and 3D rendering employs different mechanismn; this function is handled by SharpDx2D11Base or SharpDx3D11Base
        /// </summary>
        protected virtual void EndDraw()
        {
        }

        /// <summary>
        ///   Quits the rendering window or application.
        /// </summary>
        public void Exit()
        {
            _form.Close();
        }

        /// <summary>
        ///   Updates time since last frame
        /// </summary>
        private void OnUpdate()
        {
            FrameDelta = (float)clock.Update();
            Update(clock);
        }



        /// <summary>
        /// This method is called once per game loop after calling Update. Like Update, the Render() 
        /// method is also called from the main class. This is the method where the graphics pipeline 
        /// is constructed and processed for the frame using methods on the ID3D11DeviceContext instance.
        /// 
        /// It’s important to understand that this call (or other similar Draw* calls defined on 
        /// ID3D11DeviceContext) actually executes the pipeline. 
        /// https://msdn.microsoft.com/en-us/library/windows/desktop/dn643746(v=vs.85).aspx 
        /// 
        /// Specifically, this is when Direct3D communicates with the GPU to set drawing state, runs 
        /// each pipeline stage, and writes the pixel results into the render-target buffer resource 
        /// for display by the swap chain. 
        /// 
        /// Since communication between the CPU and GPU incurs overhead, combine multiple draw calls 
        /// into a single one if you can, especially if your scene has a lot of rendered objects.
        /// </summary>
        /// 
        /// OnRender will Call each Active GameObjects to be Rendered 
        protected virtual void Render()
        {
            _frameAccumulator += FrameDelta;
            ++_frameCount;
            
            if (_frameAccumulator >= 1.0f && _displayWindowConfiguration.ShowFramePerSecond)
            {
                FramePerSecond = _frameCount / _frameAccumulator;

                _form.Text = _displayWindowConfiguration.WindowTitle + " - FPS: " + FramePerSecond;
                _frameAccumulator = 0.0f;
                _frameCount = 0;
            }

            BeginDraw();
            OnRender();
            Draw(clock);
            EndDraw();
        }


        protected virtual void OnRender()
        {

        }

        #region "Events"

        protected virtual void MouseClick(MouseEventArgs e)
        {
        }

        protected virtual void MouseMove(MouseEventArgs e)
        {
        }
        protected virtual void MouseDown(MouseEventArgs e)
        {
        }
        protected virtual void MouseUp(MouseEventArgs e)
        {
        }

        protected virtual void KeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Exit();
        }

        protected virtual void KeyUp(KeyEventArgs e)
        {
        }

        /// <summary>
        ///   Handles a mouse click event.
        /// </summary>
        /// <param name = "sender">The sender.</param>
        /// <param name = "e">The <see cref = "System.Windows.Forms.MouseEventArgs" /> instance containing the event data.</param>
        private void HandleMouseClick(object sender, MouseEventArgs e)
        {
            MouseClick(e);
        }

        private void HandleMouseMove(object sender, MouseEventArgs e)
        {
            MouseMove(e);
        }

        private void HandleMouseDown(object sender, MouseEventArgs e)
        {
            MouseDown(e);
        }

        private void HandleMouseUp(object sender, MouseEventArgs e)
        {
            MouseUp(e);
        }

        /// <summary>
        ///   Handles a key down event.
        /// </summary>
        /// <param name = "sender">The sender.</param>
        /// <param name = "e">The <see cref = "System.Windows.Forms.KeyEventArgs" /> instance containing the event data.</param>
        private void HandleKeyDown(object sender, KeyEventArgs e)
        {
            KeyDown(e);
        }

        /// <summary>
        ///   Handles a key up event.
        /// </summary>
        /// <param name = "sender">The sender.</param>
        /// <param name = "e">The <see cref = "System.Windows.Forms.KeyEventArgs" /> instance containing the event data.</param>
        private void HandleKeyUp(object sender, KeyEventArgs e)
        {
            KeyUp(e);
        }

        private void HandleResize(object sender, EventArgs e)
        {
            if (_form.WindowState == FormWindowState.Minimized)
            {
                return;
            }

            // UnloadContent();

            //_configuration.WindowWidth = _form.ClientSize.Width;
            //_configuration.WindowHeight = _form.ClientSize.Height;

            //if( Context9 != null ) {
            //    userInterfaceRenderer.Dispose();

            //    Context9.PresentParameters.BackBufferWidth = _configuration.WindowWidth;
            //    Context9.PresentParameters.BackBufferHeight = _configuration.WindowHeight;

            //    Context9.Device.Reset( Context9.PresentParameters );

            //    userInterfaceRenderer = new UserInterfaceRenderer9( Context9.Device, _form.ClientSize.Width, _form.ClientSize.Height );
            //} else if( Context10 != null ) {
            //    userInterfaceRenderer.Dispose();

            //    Context10.SwapChain.ResizeBuffers( 1, WindowWidth, WindowHeight, Context10.SwapChain.Description.ModeDescription.Format, Context10.SwapChain.Description.Flags );


            //    userInterfaceRenderer = new UserInterfaceRenderer10( Context10.Device, _form.ClientSize.Width, _form.ClientSize.Height );
            //}

            // LoadContent();
        }

     

        
         
        #endregion


        #region "Properties"
        /// <summary>
        /// Return the Handle to display to.
        /// </summary>
        protected IntPtr DisplayHandle
        {
            get
            {
                return _form.Handle;
            }
        }

        /// <summary>
        /// Gets the config.
        /// </summary>
        /// <value>The config.</value>
        public DisplayWindowConfiguration Config
        {
            get
            {
                return _displayWindowConfiguration;
            }
        }

        /// <summary>
        ///   Gets the number of seconds passed since the last frame.
        /// </summary>
        public float FrameDelta { get; private set; }

        /// <summary>
        ///   Gets the number of seconds passed since the last frame.
        /// </summary>
        public float FramePerSecond { get; private set; }


        /// <summary>
        /// Gets the rendering window Size  (height and with structure)
        /// </summary>
        protected System.Drawing.Size RenderingSize
        {
            get
            {
                return _form.ClientSize;
            }
        }

        #endregion


    }
}