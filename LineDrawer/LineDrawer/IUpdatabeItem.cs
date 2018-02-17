using LineDrawer.Common;
using SharpDX;
using SharpDX.Direct2D1;

namespace LineDrawer
{
    public interface IRenderableItem 
    {
        Vector2 StartingPoint { get; set; }
        Vector2 EndingPoint { get; set; }

        /// <summary>
        /// Each items knows how to render itself properly (i.e Drawline, DrawCircule, DrawBitmap, DrawGeometry etc)
        /// </summary>
        /// <param name="renderTarget"></param>
        void Render(RenderTarget renderTarget);
    }

    public interface IRenderable
    {
        

        /// <summary>
        /// Each items knows how to render itself properly (i.e Drawline, DrawCircule, DrawBitmap, DrawGeometry etc)
        /// </summary>
        /// <param name="renderTarget"></param>
        void Render(RenderTarget renderTarget);
    }
}