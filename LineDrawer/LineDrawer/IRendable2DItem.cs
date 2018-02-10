using SharpDX.Direct2D1;

namespace LineDrawer
{
    public interface IRenderableItem
    {
         /// <summary>
         /// Each items knows how to render itself properly (i.e Drawline, DrawCircule, DrawBitmap, DrawGeometry etc)
         /// </summary>
         /// <param name="renderTarget"></param>
         void Render(RenderTarget renderTarget);
    }
}