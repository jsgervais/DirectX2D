using System;
using System.Windows.Forms;
using SharpDX;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using Device = SharpDX.Direct3D11.Device;

namespace LineDrawer.Common
{
    /// <summary>
    /// Root class for Directx11 3D types of App (Under SharpDX) 
    /// </summary>
    public class SharpDx3D11Base : BaseSharpDXApp
    {
        protected Device _device;
        protected SwapChain _swapChain;
        protected Texture2D _backBuffer;
        protected RenderTargetView _backBufferView;

        /// <summary>
        /// Returns the device
        /// </summary>
        public Device Device
        {
            get
            {
                return _device;
            }
        }

        /// <summary>
        /// Returns the backbuffer used by the SwapChain
        /// </summary>
        public Texture2D BackBuffer
        {
            get
            {
                return _backBuffer;
            }
        }

        /// <summary>
        /// Returns the render target view on the backbuffer used by the SwapChain.
        /// </summary>
        public RenderTargetView BackBufferView
        {
            get
            {
                return _backBufferView;
            }
        }

        protected override void Initialize(DisplayWindowConfiguration displayWindowConfiguration)
        {
            // SwapChain description
            var desc = new SwapChainDescription()
            {
                BufferCount = 1,
                ModeDescription = 
                    new ModeDescription(displayWindowConfiguration.Width, 
                                        displayWindowConfiguration.Height,
                                        new Rational(60, 1), 
                                        Format.R8G8B8A8_UNorm),
                IsWindowed = true,
                OutputHandle = DisplayHandle,
                SampleDescription = new SampleDescription(1, 0),
                SwapEffect = SwapEffect.Discard,
                Usage = Usage.RenderTargetOutput
            };

            // Create Device and SwapChain
            Device.CreateWithSwapChain(DriverType.Hardware, DeviceCreationFlags.BgraSupport, new [] { FeatureLevel.Level_10_0 }, desc, out _device, out _swapChain);

            // Ignore all windows events
            Factory factory = _swapChain.GetParent<Factory>();
            factory.MakeWindowAssociation(DisplayHandle, WindowAssociationFlags.IgnoreAll);

            // New RenderTargetView from the backbuffer
            _backBuffer = Texture2D.FromSwapChain<Texture2D>(_swapChain, 0);

            _backBufferView = new RenderTargetView(_device, _backBuffer);
        }


       

        protected override void BeginDraw()
        {
            base.BeginDraw();
            Device.ImmediateContext.Rasterizer.SetViewport(new Viewport(0, 0, Config.Width, Config.Height));
            Device.ImmediateContext.OutputMerger.SetTargets(_backBufferView);
        }


        protected override void EndDraw()
        {
            _swapChain.Present(Config.WaitVerticalBlanking?1:0, PresentFlags.None);
        }
    }
}