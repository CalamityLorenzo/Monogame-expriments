using Microsoft.Xna.Framework;
using System;

namespace GameLibrary.Extensions
{
    public static class Vector2Extensions
    {
        public static Vector2 AddX(this Vector2 @this, int X) => new Vector2(@this.X + X, @this.Y);
        public static Vector2 AddY(this Vector2 @this, int Y) => new Vector2(@this.X, @this.Y + Y);
        public static Vector2 AddX(this Vector2 @this, float X) => new Vector2(@this.X + X, @this.Y);
        public static Vector2 AddY(this Vector2 @this, float Y) => new Vector2(@this.X, @this.Y + Y);
        public static Vector2 Subtract(this Vector2 @this, Vector2 sub) => new Vector2(@this.X - sub.X, @this.Y - sub.Y);
        public static Vector2 GetMantissa(this Vector2 @this)
        {
            // math dot floor rounds...THat may be a problem
            var oX = @this.X - (float)Math.Floor(@this.X);
            var oY = @this.Y - (float)Math.Floor(@this.Y);
            return new Vector2(oX, oY);
        }
    }
}
