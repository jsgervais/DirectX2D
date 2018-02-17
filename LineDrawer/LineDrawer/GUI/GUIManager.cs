using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SharpDX;
using SharpDX.Direct2D1;
using SharpDX.DirectWrite;
using SharpDX.Mathematics.Interop;
using Timer = LineDrawer.Common.Timer;

namespace LineDrawer.GUI
{

    /// <summary>
    /// Helper class to manage and draw basic user interface components under directX.
    /// Manages a set of controls working together.
    /// </summary>
    class GUIManager : IRenderable, IUpdatableItem
    {
 

        private List<GUIBaseControl> _controls = new List<GUIBaseControl>();
        private GUIBaseControl _currentFocusedControl;
        private GUIBaseControl _currentHoverControl;

        public GUIManager()
        {

        }

        /// <summary>
        /// Adds a clickable button in the screen
        /// </summary>
        /// <param name="x">Left coordinate</param>
        /// <param name="y">Top coordinate</param>
        /// <param name="w">With of the button</param>
        /// <param name="h">Height of the button</param>
        /// <param name="text">The button labels</param>
        /// <param name="onClickDelegate">Action to call on button click event </param>
        public void AddButton( int x, int y, int w, int h, string text, Action onClickDelegate)
        {
            var button = new GUIButton(x, y, w, h, text, onClickDelegate);
            _controls.Add(button);
        }

        public void AddRadioButton( int x, int y, int h, Font font, Color color = default(Color))
        {

        }


        public void AddCheckbox( int x, int y, int h, Font font, Color color = default(Color))
        {

        }

        /// <summary>
        /// Update all items (check for on mouse over, focus, etc)
        /// </summary>
        /// <param name="time"></param>
        public void Update(Timer time)
        {
            foreach (IUpdatableItem control in _controls)
            {
                control.Update(time);
            }
        }

        /// <summary>
        /// Render all GUI items
        /// </summary>
        /// <param name="renderTarget"></param>
        public void Render(RenderTarget renderTarget)
        {
            foreach (IRenderable control in _controls)
            {
                control.Render(renderTarget);
            }
            _currentHoverControl?.DrawHover(renderTarget);
            _currentFocusedControl?.DrawFocus(renderTarget);
        }

        /// <summary>
        /// Handles the click event
        /// </summary>
        /// <param name="e"></param>
        public void MouseClick(MouseEventArgs e)
        {
           _currentHoverControl?.OnClickDelegate.Invoke(); //TODO use async BeginInvoke Instead, in case of long running tasks )
        }


        /// <summary>
        /// Handles the mouse move event to check if the mouse is over a control
        /// </summary>
        /// <param name="e"></param>
        public void MouseMove(MouseEventArgs e)
        {
            _currentHoverControl = null;
            //finds the last item in the list (in case of stacking ui controls) where the mouse is within the 
            foreach (GUIBaseControl control in _controls)
            {
                if (control.PositionX < e.X && (control.PositionX + control.Width) > e.X &&
                    control.PositionY < e.Y && (control.PositionY + control.Height) > e.Y)
                {
                    _currentHoverControl = control;
                }
            }
        }


        public bool IsOverGUIItem()
        {
            return _currentHoverControl != null;
        }
    }
}
