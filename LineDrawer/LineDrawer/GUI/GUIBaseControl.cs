using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LineDrawer.Common;
using SharpDX;
using SharpDX.Direct2D1;
using SharpDX.DirectWrite;
using SharpDX.Mathematics.Interop;
using Factory = SharpDX.DirectWrite.Factory;

namespace LineDrawer.GUI
{
    class GUIBaseControl:IRenderable, IUpdatableItem
    {

        public Action OnClickDelegate { get; protected set; }

        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public bool Enabled { get; set; }
        


        public Factory FactoryDWrite { get; private set; }

        public GUIBaseControl()
        {
            FactoryDWrite = new Factory();
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
        /// satisfy IRenderable interface.
        /// </summary>
        /// <param name="renderTarget"></param>
        public virtual void Render(RenderTarget renderTarget)
        {

        }


        /// <summary>
        /// Draws a filled Rectancgle  
        /// </summary>
        /// <param name="renderTarget"></param>
        /// <param name="x">Left</param>
        /// <param name="y">Top</param>
        /// <param name="w">Total Width</param>
        /// <param name="h">Total Height</param>
        /// <param name="color">Background color</param>
        protected static void DrawFilledRectangle(RenderTarget renderTarget, int x, int y, int w, int h, Color color = default(Color))
        {
            var area = new RawRectangleF(x, y, x+w, y+h);
            using (var brush = new SolidColorBrush(renderTarget, color))
            {
                renderTarget.FillRectangle(area, brush);
            }

        }

        protected static void DrawBorderBox(RenderTarget renderTarget, int x, int y, int w, int h, Color color = default(Color))
        {
            var area = new RawRectangleF(x, y, x + w, y + h);
            using (var brush = new SolidColorBrush(renderTarget, color))
            {
                renderTarget.DrawRectangle(area, brush);
            }

        }

        protected static void DrawText(RenderTarget renderTarget, int x, int y, TextLayout textLayout, Color color = default(Color))
        {
            using (var textBrush = new SolidColorBrush(renderTarget, color))
            {
                //renderTarget.DrawText(Text, TextFormat, area, textBrush, DrawTextOptions.Clip );
                renderTarget.DrawTextLayout(new RawVector2(x, y), textLayout, textBrush);
            }
        }
    }
}
