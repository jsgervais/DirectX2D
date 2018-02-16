using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LineDrawer.Common;
using SharpDX.Direct2D1;

namespace LineDrawer.GUI
{
    class GUIBaseControl:IRenderableItem, IUpdatableItem
    {
        public Action OnClickDelegate { get; protected set; }

        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public bool Enabled { get; set; }

        public SharpDX.DirectWrite.Factory FactoryDWrite { get; private set; }

        public GUIBaseControl()
        {
            FactoryDWrite = new SharpDX.DirectWrite.Factory();
        }

        /// <summary>
        /// Used to display on mouse over state 
        /// </summary>
        /// <param name="renderTarget"></param>
        public virtual void DrawHover(RenderTarget renderTarget)
        {

        }

        /// <summary>
        /// Used to display which control corrently has focus 
        /// </summary>
        /// <param name="renderTarget"></param>
        public virtual void DrawFocus(RenderTarget renderTarget)
        {

        }
 

        /// <summary>
        /// Each controls knows how to update their current states.
        /// </summary>
        /// <param name="time"></param>
        public virtual void Update(Timer time)
        {

        }

        /// <summary>
        /// Each controls knows how to render themselves, must derive this function to 
        /// satisfy IRenderableItem interface.
        /// </summary>
        /// <param name="renderTarget"></param>
        public virtual void Render(RenderTarget renderTarget)
        {

        }
    }
}
