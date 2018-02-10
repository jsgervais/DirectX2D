using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX.Direct2D1;

namespace LineDrawer
{
    /// <summary>
    /// Loads and collects all Renderable items in the scene.  
    /// 
    /// Each scene is bound to a Render target
    /// </summary>
    class SceneManager : IDisposable
    {
        public List<IRenderableItem> activeItems { get; set; }  //visible in the scene
        public List<IRenderableItem> selectedItems { get; set; }  //selected in the scene


        public RenderTarget RenderTarget { get; set; }

        SceneManager(RenderTarget renderTarget)
        {
            RenderTarget = renderTarget;
        }

        void IDisposable.Dispose()
        {
            foreach (var item in activeItems.OfType<IDisposable>())
            {
                item.Dispose();
            }
            activeItems.Clear();
        }
    }
}
