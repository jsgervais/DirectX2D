using LineDrawer.Common;
using SharpDX.Direct2D1;

namespace LineDrawer
{
    public interface IUpdatableItem
    {
         /// <summary>
         /// Anything that moves or have to change state before being rendered
         /// </summary>
         /// <param name="renderTarget"></param>
         void Update(Timer time);
    }
}