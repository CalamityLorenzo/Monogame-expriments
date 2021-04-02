using GameLibrary.AppObjects;
using Microsoft.Xna.Framework;

namespace GameLibrary.Interfaces
{
    public interface IInteractiveGameObject
    {
        /// <summary>
        /// These are usually converted to vectors
        /// </summary>
        /// <param name="position"></param>
        public void SetCurrentPosition(Point position);
        public Point CurrentPosition { get; }
        /// <summary>
        ///  can change every update. The shape of an object can be anything
        /// </summary>
        public Rectangle Area { get; }

    }
}
