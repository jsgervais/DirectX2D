using System;
using System.Windows.Forms;
using SharpDX.Direct2D1;
using SharpDX.DXGI;
using SharpDX;
using SharpDX.Direct3D11;
using AlphaMode = SharpDX.Direct2D1.AlphaMode;
using Factory = SharpDX.Direct2D1.Factory;

namespace LineDrawer.Common
{
    /// <summary>
    /// Root class for Directx11 2D types of App (Under SharpDX) 
    /// </summary>
    public class SharpDx2D11Base : SharpDx3D11Base
    {
        public Factory Factory2D { get; private set; }
        public SharpDX.DirectWrite.Factory FactoryDWrite { get; private set; }
        public RenderTarget RenderTarget2D { get; private set; }
        public SolidColorBrush SceneColorBrush { get; private set;}

        private Surface _surface;

        protected override void Initialize(DisplayWindowConfiguration displayWindowConfiguration)
        {
            base.Initialize(displayWindowConfiguration);
            Factory2D = new SharpDX.Direct2D1.Factory();
            _surface = BackBuffer.QueryInterface<Surface>();
            RenderTarget2D = new RenderTarget(Factory2D, 
                                                _surface,
                                                new RenderTargetProperties(new PixelFormat(Format.Unknown, AlphaMode.Premultiplied)));
             
            RenderTarget2D.AntialiasMode = AntialiasMode.PerPrimitive;

            FactoryDWrite = new SharpDX.DirectWrite.Factory();

            SceneColorBrush = new SolidColorBrush(RenderTarget2D, Color.White);
        }

        /// <summary>
        /// Sets the initial state BeginDraw on the RenderTarget2D.  Should only be called once by frame, in the main run loop.
        /// </summary>
        protected override void BeginDraw()
        {
            base.BeginDraw();
            RenderTarget2D.BeginDraw();
        }

        /// <summary>
        /// Sets the state EndDraw on the RenderTarget2D.  Should only be called once by frame, in the main run loop.
        /// </summary>
        protected override void EndDraw()
        {
            RenderTarget2D.EndDraw();
            base.EndDraw();
        }



        protected override void HandleResize(object sender, EventArgs e)
        {
            if (_form.WindowState == FormWindowState.Minimized)
            {
                return;
            }
            //var form = (Form) sender;
            //unbinds everything
            _device.ImmediateContext.ClearState();

            UnloadContent();

            RenderTarget2D.Dispose();
            _backBufferView?.Dispose();
            _backBuffer.Dispose();
            _surface.Dispose();

            _swapChain.ResizeBuffers(1,
                                    _form.ClientSize.Width,
                                    _form.ClientSize.Height,
                                    Format.Unknown,
                                    SwapChainFlags.AllowModeSwitch);

            _backBuffer = Texture2D.FromSwapChain<Texture2D>(_swapChain, 0);
            _backBufferView = new RenderTargetView(_device, _backBuffer);

            _surface = BackBuffer.QueryInterface<Surface>();
            RenderTarget2D = new RenderTarget(Factory2D,
                                             _surface,
                                             new RenderTargetProperties(new PixelFormat(Format.Unknown, AlphaMode.Premultiplied)));
             

            LoadContent();
        }
    }
}
