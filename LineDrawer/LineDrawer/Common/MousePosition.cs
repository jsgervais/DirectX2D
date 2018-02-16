using System;

namespace LineDrawer.Common
{
   
    public class MousePosition
    {
        private string _label = "Mouse at (x:{0}, y:{1})";
        public int x;
        public int y;

        public string GetMouseCoordinates()
        {
            return String.Format(_label, x, y);
        }
    }
     
}