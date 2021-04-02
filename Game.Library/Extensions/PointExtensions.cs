using Microsoft.Xna.Framework;

namespace GameLibrary.Extensions
{
    public static class PointExtensions
    {
        public static Point AddX(this Point @this, int x)
        {
            return new Point(@this.X + x, @this.Y);
        }

        public static Point AddY(this Point @this, int y)
        {
            return new Point(@this.X, @this.Y + y);
        }

        public static Point Add(this Point @this, int x, int y) => new Point(@this.X + x, @this.Y + y);

        public static Point Subtract(this Point @this, Point point)
        {
            return new Point(@this.X - point.X, @this.Y - point.Y);
        }

        public static Point Subtract(this Point @this, Vector2 toPoint) => PointExtensions.Subtract(@this, toPoint.ToPoint());
    }
}
