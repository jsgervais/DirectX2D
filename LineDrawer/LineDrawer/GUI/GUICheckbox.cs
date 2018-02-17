using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX;
using SharpDX.Direct2D1;
using SharpDX.DirectWrite;

namespace LineDrawer.GUI
{
    class GUICheckbox : GUIBaseControl
    {
        public enum ChecboxState
        {
            Unchecked,
            Checked
        }

        private ChecboxState _state;

        public GUICheckbox(int x, int y, int w, int h, string text, Action onClickDelegate)
        {
            PositionX = x;
            PositionY = y;
            Width = w;
            Height = h;
            Text = text;
            OnClickDelegate += onClickDelegate;
            OnClickDelegate += HandleClick;

            BackgroundColor = Color.White;
            BorderColor = Color.BlanchedAlmond;
            BackgroundColorHover = Color.AntiqueWhite;
            BorderColorHover = Color.Bisque;
            TextColor = Color.WhiteSmoke;

            TextFormat = new TextFormat(FactoryDWrite, "Calibri", 20) { TextAlignment = TextAlignment.Leading, ParagraphAlignment = ParagraphAlignment.Center };
            TextLayout = new TextLayout(FactoryDWrite, Text, TextFormat, Width -25, Height);

            _state = ChecboxState.Unchecked;
        }

        public override void Render(RenderTarget renderTarget)
        {
            DrawFilledRectangle(renderTarget, PositionX, PositionY, 20, 20, BackgroundColor);  //TODO align checkbox square with text (text is already vertically centered on height)
            DrawBorderBox(renderTarget, PositionX, PositionY, 20, 20, BorderColor);
            DrawText(renderTarget, PositionX + 25, PositionY, TextLayout, TextColor);

            if (_state == ChecboxState.Checked)
            {
                DrawText(renderTarget, PositionX + 2, PositionY, 20, 20, "X", TextFormat, Color.Black);
            }
           
        }

        public override void DrawHover(RenderTarget renderTarget)
        {
            DrawFilledRectangle(renderTarget, PositionX, PositionY, 20, 20, BackgroundColorHover);
            DrawBorderBox(renderTarget, PositionX, PositionY, 20, 20, BorderColorHover);
            DrawText(renderTarget, PositionX + 25, PositionY, TextLayout, TextColor);

            if (_state == ChecboxState.Checked)
            {
                DrawText(renderTarget, PositionX + 2, PositionY, 20, 20, "X", TextFormat, Color.Black);
            }
        }

        private void HandleClick()
        {
            Console.WriteLine("Checkbox clicked");
            _state = _state == ChecboxState.Unchecked ? ChecboxState.Checked : ChecboxState.Unchecked;
        }
    }
}
