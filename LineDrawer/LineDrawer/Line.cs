﻿using System;
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
        public Vector2 StartingPoint { get; set; }
        public Vector2 EndingPoint { get; set; }

        public  Brush DefaultBrush { get; set; }

        private RenderTarget _renderTarget;

        public Line(Vector2 from, Vector2 to, RenderTarget renderTarget)
        {
            //TODO: should move out renderTarget out of the constructor.
            _renderTarget = renderTarget;
            DefaultBrush = new SolidColorBrush(_renderTarget, Color.White);
            StartingPoint = from;
            EndingPoint = to;
        }
        public Line(Vector2 from, Vector2 to, RenderTarget renderTarget, Color lineColor )
        {
            _renderTarget = renderTarget;
            DefaultBrush = new SolidColorBrush(renderTarget, lineColor);

            StartingPoint = from;
            EndingPoint = to;
        }


        void IRenderableItem.Render(RenderTarget renderTarget2D)
        {
            renderTarget2D.DrawLine(StartingPoint, EndingPoint, DefaultBrush);
        }
    }
}
