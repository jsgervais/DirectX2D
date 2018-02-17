using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX;
using SharpDX.Direct2D1;
using SharpDX.Mathematics.Interop;

namespace LineDrawer
{
    class Rectangle : IRenderableItem
    {
        public Vector2 StartingPoint { get; set; }
        public Vector2 EndingPoint { get; set; }
        public  Brush DefaultBrush { get; set; }
        public bool FilledRectangle = false;

        private RenderTarget _renderTarget;

        public Rectangle(Vector2 from, Vector2 to, RenderTarget renderTarget)
        {
            //TODO: should move out renderTarget out of the constructor.
            _renderTarget = renderTarget;
            DefaultBrush = new SolidColorBrush(_renderTarget, Color.White);
            StartingPoint = from;
            EndingPoint = to;
        }
        public Rectangle(Vector2 from, Vector2 to, RenderTarget renderTarget, Color lineColor )
        {
            _renderTarget = renderTarget;
            DefaultBrush = new SolidColorBrush(renderTarget, lineColor);

            StartingPoint = from;
            EndingPoint = to;
        }


        void IRenderableItem.Render(RenderTarget renderTarget2D)
        {
            var rect = new RawRectangleF( Math.Min(StartingPoint.X, EndingPoint.X),
                                          Math.Max(StartingPoint.Y, EndingPoint.Y),
                                          Math.Max(StartingPoint.X, EndingPoint.X),
                                          Math.Min(StartingPoint.Y, EndingPoint.Y));
            
            renderTarget2D.DrawRectangle(rect, DefaultBrush);
            if (FilledRectangle) renderTarget2D.FillRectangle(rect, DefaultBrush);
        }
    }
}
