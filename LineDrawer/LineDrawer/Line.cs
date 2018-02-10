using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX;
using SharpDX.Direct2D1;

namespace LineDrawer
{
    class Line : IRenderableItem
    {
        private Vector2 _startingPoint;
        private Vector2 _endingingPoint;
        private Brush _defaultBrush;
        private RenderTarget _renderTarget;

        public Line(Vector2 from, Vector2 to, RenderTarget renderTarget)
        {
            _renderTarget = renderTarget;
            _defaultBrush = new SolidColorBrush(_renderTarget, Color.White);
        }
        public Line(Vector2 from, Vector2 to, RenderTarget renderTarget, Brush brush )
        {
            _renderTarget = renderTarget;
            _defaultBrush = brush;
        }


        void IRenderableItem.Render(RenderTarget renderTarget2D)
        {
            renderTarget2D.DrawLine(_startingPoint, _endingingPoint, _defaultBrush);
        }
    }
}
