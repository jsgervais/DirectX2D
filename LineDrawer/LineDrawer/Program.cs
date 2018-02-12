using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SharpDX;
using SharpDX.Direct2D1;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using SharpDX.Mathematics.Interop;
using LineDrawer.Common;
using SharpDX.DirectWrite;
using SharpDX.Windows;

using AlphaMode = SharpDX.Direct2D1.AlphaMode;
using Device = SharpDX.Direct3D11.Device;
using Factory = SharpDX.DXGI.Factory;
using Timer = LineDrawer.Common.Timer;

namespace LineDrawer
{

    /// <summary>
    /// DirectX initialization and App Window creation is handled by the bases classes. 
    /// See C++ example on implemeting a small DirectX11 rendering framwork : 
    /// https://msdn.microsoft.com/en-us/library/windows/desktop/dn643747(v=vs.85).aspx
    /// </summary>
    class Program : SharpDx2D11Base
    {
        private const int APP_WIDTH = 1024;
        private const int APP_HEIGHT = 768;

        private MousePosition _mousePosition = new MousePosition();

        public TextLayout TextLayout { get; private set; }
        public TextFormat TextFormat { get; private set; }

        private List<Line> _lines = new List<Line>();
        private Line _currentLine;

        [STAThread]
        private static void Main()
        {
            Program program = new Program();
            var demoConfig = new DisplayWindowConfiguration("LineDrawer", APP_WIDTH, APP_HEIGHT);
            program.Run(demoConfig);

            // Main loop is implemented in base Class BaseSharpDXApp
            
        }

        protected override void Initialize(DisplayWindowConfiguration conf)
        {
            base.Initialize(conf);

            // Initialize a TextFormat
            TextFormat = new TextFormat(FactoryDWrite, "Calibri", 20) { TextAlignment = TextAlignment.Leading, ParagraphAlignment = ParagraphAlignment.Center };

            RenderTarget2D.TextAntialiasMode = SharpDX.Direct2D1.TextAntialiasMode.Cleartype;
            TextLayout = new TextLayout(FactoryDWrite, _mousePosition.GetMouseCoordinates(), TextFormat, conf.Width, conf.Height);

        }


        public class MousePosition
        {
            private string _label = "Mouse at (x:{0}, y:{1})";
            public int x;
            public int y;

            public MousePosition()
            {
            }

            public string GetMouseCoordinates()
            {
                return String.Format(_label, x, y);
            }
        }

         

        /// <summary>
        /// This method is called once per game loop after calling Update. Like Update, the Render() 
        /// method is also called from the main class. This is the method where the graphics pipeline 
        /// is constructed and processed for the frame using methods on the ID3D11DeviceContext instance.
        /// 
        /// It’s important to understand that this call (or other similar Draw* calls defined on 
        /// ID3D11DeviceContext) actually executes the pipeline. 
        /// https://msdn.microsoft.com/en-us/library/windows/desktop/dn643746(v=vs.85).aspx 
        /// 
        /// Specifically, this is when Direct3D communicates with the GPU to set drawing state, runs 
        /// each pipeline stage, and writes the pixel results into the render-target buffer resource 
        /// for display by the swap chain. 
        /// 
        /// Since communication between the CPU and GPU incurs overhead, combine multiple draw calls 
        /// into a single one if you can, especially if your scene has a lot of rendered objects.
        /// </summary>
        /// 
        /// OnRender will Call each Active GameObjects to be Rendered 
        protected override void OnRender()
        {
            //clear previous frame
            RenderTarget2D.Clear(null);

            //if display mouse coords  -- add a checkbox or option menu later \

            //top left corner : (0,0)
            TextLayout.Dispose();
            TextLayout = new TextLayout(FactoryDWrite, _mousePosition.GetMouseCoordinates(), TextFormat, 400, 40);

            RenderTarget2D.DrawTextLayout(new Vector2(0, 0), TextLayout, SceneColorBrush, DrawTextOptions.None);

            //Render current line if not null "?." operator  
            ((IRenderableItem) _currentLine)?.Render(RenderTarget2D);

            //and all added lines 
            foreach (IRenderableItem line in _lines )
            {
                line.Render(RenderTarget2D);
            }
        }




        protected override void MouseClick(MouseEventArgs e)
        {

        }

        protected override void MouseMove(MouseEventArgs e)
        {
            //keep new mouse coordinates
            _mousePosition.x = e.X;
            _mousePosition.y = e.Y;

            if (_currentLine != null)
            {
                _currentLine.EndingingPoint = new Vector2(e.X, e.Y);
            }
        }

        protected override void MouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _currentLine = new Line(new Vector2(_mousePosition.x, _mousePosition.y), 
                                        new Vector2(_mousePosition.x, _mousePosition.y),
                                        RenderTarget2D);
            }
        }

        protected override void MouseUp(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _lines.Add(_currentLine);
                _currentLine = null;
            }
        }

        protected override void KeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Exit();
        }

        protected override void KeyUp(KeyEventArgs e)
        {

        }


    }
}
