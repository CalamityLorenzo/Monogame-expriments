
using Microsoft.Xna.Framework;

namespace Collisions.Objects.Paddle
{
    public enum PaddleArea
    {
        None = 0,
        FarLeft = 1,
        Left = 2,
        Right = 3,
        FarRight = 4
    };

    public interface IPaddle
    {
        PaddleArea PaddleHit(Rectangle intersect);
    }
}