using GameLibrary.Animation;
using GameLibrary.AppObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameLibrary.Extensions;
namespace Collisions.Objects.Paddle
{


    // The unique thing... about paddles is where they can be hit.
    // there are 6 special splaces (4 on top, 2 either side)
    public class BasePaddle : Sprite, IPaddle
    {
        Rectangle Right;
        Rectangle Left;
        Rectangle FarLeft;
        Rectangle FarRight;

        public BasePaddle(SpriteBatch spritebatch, Texture2D atlas, AnimationPlayer animPlayer, Point startPos) : base(spritebatch, atlas, animPlayer, startPos)
        {
            GenerateRects();
        }

        private void GenerateRects()
        {
            var segmentWidth = this.Area.Width / 4;
            if (FarLeft.Location != CurrentPosition)
            {
                FarLeft = new Rectangle(CurrentPosition, new Point(segmentWidth, 14));
                Left = new Rectangle(FarLeft.Location.AddX(segmentWidth), new Point(segmentWidth, 14));
                Right = new Rectangle(Left.Location.AddX(segmentWidth), new Point(segmentWidth, 14));
                FarRight = new Rectangle(Right.Location.AddX(segmentWidth), new Point(segmentWidth, 14));
            }
        }

        public PaddleArea PaddleHit(Rectangle intersect)
        {
            // Does the area intersetr with any of our particular bits?
            if (intersect.Intersects(Left)) return PaddleArea.Left;
            if (intersect.Intersects(Right)) return PaddleArea.Right;
            // We are not allowing multiples are preferring the centre over the edges.
            if (intersect.Intersects(FarLeft)) return PaddleArea.FarLeft;
            if (intersect.Intersects(FarRight)) return PaddleArea.FarRight;
            
            return PaddleArea.None;
        }

        public override void Update(float mlSinceupdate)
        {
            GenerateRects();
            base.Update(mlSinceupdate);
        }

    }
}
