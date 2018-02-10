using SharpDX.Direct2D1;
using SharpDX.DXGI;
using SharpDX;

using AlphaMode = SharpDX.Direct2D1.AlphaMode;
using Factory = SharpDX.Direct2D1.Factory;

namespace LineDrawer.Common
{
    /// <summary>
    /// Root class for Directx11 2D types of App (Under SharpDX) in an existing Windows Form
    /// </summary>
    public class SharpDx2D11BaseWinForm : BaseSharpDXApp
    {
        public Factory Factory2D { get; private set; }
        public SharpDX.DirectWrite.Factory FactoryDWrite { get; private set; }
        public WindowRenderTarget RenderTarget2D { get; private set; }
        public SolidColorBrush SceneColorBrush { get; private set; }

        protected override void Initialize(DisplayWindowConfiguration displayWindowConfiguration)
        {
            Factory2D = new SharpDX.Direct2D1.Factory();
            FactoryDWrite = new SharpDX.DirectWrite.Factory();

            HwndRenderTargetProperties properties = new HwndRenderTargetProperties();
            properties.Hwnd = DisplayHandle;
            properties.PixelSize = new SharpDX.Size2(displayWindowConfiguration.Width, displayWindowConfiguration.Height);
            properties.PresentOptions = PresentOptions.None;

            RenderTarget2D = new WindowRenderTarget(Factory2D, new RenderTargetProperties(new PixelFormat(Format.Unknown, AlphaMode.Premultiplied)), properties);

            RenderTarget2D.AntialiasMode = AntialiasMode.PerPrimitive;


            SceneColorBrush = new SolidColorBrush(RenderTarget2D, Color.White);
        }

        protected override void BeginDraw()
        {
            base.BeginDraw();
            RenderTarget2D.BeginDraw();
        }

        protected override void EndDraw()
        {
            RenderTarget2D.EndDraw();
            base.EndDraw();
        }
    }
}
