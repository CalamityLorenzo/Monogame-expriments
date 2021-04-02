using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace GameLibrary.AppObjects
{
    public static class Collisions
    {
        public static bool AABBStruck(Rectangle l, Rectangle r)
        {
            return (l.Left < r.Right &&
                   l.Right > r.Left &&
                   l.Top < r.Bottom &&
                   l.Bottom > r.Top);

        }

        public static Rectangle? AABBCollisions(IEnumerable<Rectangle> map, Rectangle target)
        {
            foreach (var mapBlock in map)
            {
                if (AABBStruck(mapBlock, target)) return mapBlock;
            }
            return null;
        }
        public static Rectangle? AABBCollision(Rectangle gameObject, Rectangle inputObject)
        {
            if (AABBStruck(gameObject, inputObject)) return gameObject;
            return null;
        }
    }
}
