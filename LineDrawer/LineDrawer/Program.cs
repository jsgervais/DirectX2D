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
using SharpDX.Windows;

using AlphaMode = SharpDX.Direct2D1.AlphaMode;
using Device = SharpDX.Direct3D11.Device;
using Factory = SharpDX.DXGI.Factory;

namespace LineDrawer
{

    /// <summary>
    /// DirectX initialization and App Window creation is handled by the bases classes. 
    /// See C++ example on implemeting a small DirectX11 rendering framwork : 
    /// https://msdn.microsoft.com/en-us/library/windows/desktop/dn643747(v=vs.85).aspx
    /// </summary>
    class Program : SharpDx2D11Base
    {
        [STAThread]
        private static void Main()
        {
            Program program = new Program();
            var demoConfig = new DisplayWindowConfiguration("LineDrawer", 1024,768);
            program.Run(demoConfig);

            // Main loop is implemented in base Class BaseSharpDXApp
            
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

            //SolidColorBrush brush = new SolidColorBrush(RenderTarget2D, Color.Azure);

            var vectorFrom = new RawVector2(5f, 10f);
            var vectorTo = new RawVector2(200f, 250f);

            RenderTarget2D.DrawLine(vectorFrom, vectorTo, SceneColorBrush);
        }




        protected override void MouseClick(MouseEventArgs e)
        {

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
