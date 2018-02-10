using SharpDX.Direct2D1;
using SharpDX.DXGI;
using SharpDX;

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

        protected override void Initialize(DisplayWindowConfiguration displayWindowConfiguration)
        {
            base.Initialize(displayWindowConfiguration);
            Factory2D = new SharpDX.Direct2D1.Factory();
            using (var surface = BackBuffer.QueryInterface<Surface>())
            {
                RenderTarget2D = new RenderTarget(Factory2D, 
                                                  surface,
                                                  new RenderTargetProperties(new PixelFormat(Format.Unknown, AlphaMode.Premultiplied)));
            }
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
    }
}
