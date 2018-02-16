using System;
using LineDrawer.Common;
using SharpDX;
using SharpDX.Direct2D1;
using SharpDX.DirectWrite;
using SharpDX.Mathematics.Interop;

namespace LineDrawer.GUI
{
    class GUIButton : GUIBaseControl
    {
         
        public bool RoundedButton { get; set; }
        public Color BackgroundColor { get; set; } = Color.Teal;
        public Color BackgroundColorHover { get; set; } = Color.DeepSkyBlue;
        public Color BorderColor { get; set; } = Color.Aquamarine;
        public Color BorderColorHover { get; set; } = Color.DarkBlue;
        public Color TextColor { get; set; } = Color.Black;

        public string Text { get; set; }
        public TextLayout TextLayout { get; private set; }
        public TextFormat TextFormat { get; private set; }

        public GUIButton(int x, int y, int w, int h, string text, Action onClickDelegate)
        {
            PositionX = x;
            PositionY = y;
            Width = w;
            Height = h;
            Text = text;
            OnClickDelegate = onClickDelegate;

            TextFormat = new TextFormat(FactoryDWrite, "Calibri", 20) { TextAlignment = TextAlignment.Center, ParagraphAlignment = ParagraphAlignment.Center };
            TextLayout = new TextLayout(FactoryDWrite, Text, TextFormat, Width, Height);
        }

        public override void Update(Timer time)
        { 


        }

        

        public override void Render(RenderTarget renderTarget)
        {
            DrawFilledRectangle(renderTarget, PositionX, PositionY, Width, Height, BackgroundColor );
            DrawBorderBox(renderTarget, PositionX, PositionY, Width, Height, BorderColor);
            DrawText(renderTarget, PositionX, PositionY, TextLayout, TextColor);
        }

        public override void DrawHover(RenderTarget renderTarget)
        {
            DrawFilledRectangle(renderTarget, PositionX, PositionY, Width, Height, BackgroundColorHover);
            DrawBorderBox(renderTarget, PositionX, PositionY, Width, Height, BorderColorHover);
            DrawText(renderTarget, PositionX, PositionY, TextLayout, TextColor);
        }
    }
}
