using Microsoft.Xna.Framework;

namespace GameLibrary.AppObjects
{
    // LIke a point...BUt for width and heights
    public struct Dimensions
    {
        public Dimensions(int width, int height)
        {
            Width = width;
            Height = height;
        }
        public int Width { get;  }
        public int Height { get;  }

        public static Dimensions Zero => new Dimensions(0, 0);

        public override string ToString()
        {
            return $"{Width}:{Height}";
        }

        public Point ToPoint()=> new Point(Width, Height);
        public Vector2 ToVector2() => new Vector2(Width, Height);
    }


}
