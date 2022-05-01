using Collisions.Objects;
using Collisions.Objects.Balls;
using Collisions.Objects.Paddle;
using CollisionsGame.Objects;
using GameLibrary.AppObjects;
using GameLibrary.Extensions;
using GameLibrary.Models;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Windows.Forms;

namespace CollisionsGame
{
    partial class CollisionsGame
    {
        internal Random BasherAngleChanger = new Random();

        private void BoundaryBash(World state, IEnumerable<BaseBall> balls)
        {
            foreach (var ball in balls)
            {
                var hitRect = state.MapCollision(ball.Area);

                if (hitRect.HasValue)
                {
                    BallStruckRectangle(hitRect.Value, ball);
                }
            }
        }

        private void Shot(IEnumerable<(GameBlock map, BaseBullet bullet)> booms)
        {
            foreach (var itm in booms)
            {
                itm.map.Hit(5);
                itm.bullet.Struck();
            }
        }

        private void Bounced(IEnumerable<(GameBlock map, BaseBall ball)> boing)
        {
            foreach (var itm in boing)
            {
                itm.map.Hit(10);
                // itm.ball.Bounce();
                // BallStruckRectangle(itm.map.Area, itm.ball);
                var wallRect = itm.map.Area;
                var ballRect = itm.ball.Area;
                var ballVel = itm.ball.Velocity;
                var direciton = itm.ball.Direction;
                CollisionWithVelocity(itm.ball, ballRect, wallRect, ballVel, direciton);
            }

        }

        private void CollisionWithVelocity(BaseBall ball, Rectangle ballRect, Rectangle bigRect, Vector2 ballVel, Vector2 direction)
        {
            if (ballRect.X + ballRect.Width + ballVel.X > bigRect.X &&
            ballRect.X + ballVel.X < bigRect.X + bigRect.Width &&
            ballRect.Y + ballRect.Height > bigRect.Y &&
            ballRect.Y < bigRect.Y + bigRect.Height)
            {
                ball.SetDirection(new Vector2(direction.X *= -1, direction.Y)); ;
            }



            if (ballRect.X + ballRect.Width > bigRect.X &&
                  ballRect.X < bigRect.X + bigRect.Width &&
                  ballRect.Y + ballRect.Height + ballVel.Y > bigRect.Y &&
                  ballRect.Y + ballVel.Y < bigRect.Y + bigRect.Height)
            {
                ball.SetDirection(new Vector2(direction.X, direction.Y *= -1));
            }


        }

        private void BallStruckRectangle(Rectangle wallRect, BaseBall ball)
        {

            var isVertical = wallRect.Width < wallRect.Height;
            var currentY = ball.CurrentPosition.Y;
            var currentX = ball.CurrentPosition.X;

            // Get the vector from the centre of the hit rectangle. 
            // to the centre of the ball area.

            var origin = wallRect.Center;
            var striker = ball.Area.Center;
            var originCollision = new Vector2(striker.X - origin.X, striker.Y - origin.Y);
            // A unit vector tells us which side of the rectangle has been whacked.
            var angle = ball.Direction.GetAngleDegreesFromUnit();

            float w = 0.5f * (wallRect.Width + ball.Area.Width);
            float h = 0.5f * (wallRect.Height + ball.Area.Height);
            float dx = wallRect.Center.X - ball.Area.Center.X;
            float dy = wallRect.Center.Y - ball.Area.Center.Y;
            float wy = w * dy;
            float hx = h * dx; // originCollision.X;

            originCollision.Normalize();
            var newDirection = -ball.Direction;
            var chanceOfChange = BasherAngleChanger.Next(0, 30);

            // this gets you into the correct 'half' of the rectangle
            if (wy >= hx)
            {

                if (wy >= -hx && (angle<270 && angle>90))//top of block
                {
                    currentY = wallRect.Y - ball.Area.Height;
                    if (chanceOfChange > 2)
                        newDirection.X = -newDirection.X;
                }
                else//right of block
                {
                    currentX = wallRect.X + wallRect.Width;
                    if (chanceOfChange > 2)
                        newDirection.Y = -newDirection.Y;
                }

                // We did actually hit the right hand side
                //if (ball.Area.Y > wallRect.Y && ball.Area.Height + ball.Area.Y < wallRect.Y+wallRect.Height)
                //{
                //    currentX = wallRect.X + wallRect.Width;
                //    if (chanceOfChange > 2)
                //        newDirection.Y = -newDirection.Y;
                //}
                //else
                //{

                //}
            }
            else
            {

                // We are either a the left or bottom.
                // we can scan from the top of the ball down
 
                if (wy >= -hx && ball.CurrentPosition.X < wallRect.X)
                {//left of block
                    currentX = wallRect.X - ball.Area.Width;
                    if (chanceOfChange > 2)
                        newDirection.Y = -newDirection.Y;
                }
                else//bottom of block
                {
                    currentY = wallRect.Y + wallRect.Height;
                    if (chanceOfChange > 2)
                        newDirection.X = -newDirection.X;
                }
                //// we are completly inside the damn thing
                //if (ball.Area.X > wallRect.X && ball.Area.Width + ball.Area.X > wallRect.X)
                //{
                //    currentY = wallRect.Y + wallRect.Height;
                //    if (chanceOfChange > 8)
                //        newDirection.X = -newDirection.X;
                //}
                //else
                //{

                //}

            }


            ball.SetCurrentPosition(new Point(currentX, currentY));

            // Automatically assume we are reversing, cos Somethings gotta bounce.

            //if (isVertical)
            //{
            //    if (chanceOfChange > 2)
            //        newDirection.Y = -newDirection.Y;

            //}
            //else
            //{
            //    if (chanceOfChange > 3)
            //        newDirection.X = -newDirection.X;
            //}
            ball.SetDirection(newDirection);
        }

        private void CaroomedOffPaddle(BatContainer bats, IEnumerable<BaseBall> balls)
        {
            // fired after we know we have a hit.
            // Just need to get the details as to wwwwwwhhhhheeeeere on the dolly did we hit it.
            foreach (var ball in balls)
            {
                var returnAngle = bats.PaddleHit(ball.Area) switch
                {
                    PaddleArea.None => -1,
                    PaddleArea.FarLeft => 285,
                    PaddleArea.Left => 320,
                    PaddleArea.Right => 40,
                    PaddleArea.FarRight => 75
                };

                if (returnAngle != -1)
                {
                    ball.SetDirection(GeneralExtensions.UnitVectorFromDegrees(returnAngle));
                    ball.SetCurrentPosition(new Point(ball.CurrentPosition.X, bats.CurrentPosition.Y - ball.Area.Height));
                }
            }

        }

        private void setScreenData(ScreenData data)
        {
            this._graphics.PreferredBackBufferWidth = data.ScreenWidth;
            this._graphics.PreferredBackBufferHeight = data.ScreenHeight;
            this._graphics.IsFullScreen = data.FullScreen;
            this._graphics.ApplyChanges();
        }

        //private IEnumerable<GameBlock> GameObjectCollisions(IEnumerable<GameBlock> gameObjects, BatContainer playerObject)
        //{
        //    var results = new List<GameBlock>();
        //    foreach (var block in gameObjects)
        //        if (GameLibrary.AppObjects.Collisions.AABBStruck(block.Area, playerObject.Area))
        //            results.Add(block);
        //    return results;
        //}

        private void DrawCollisionMap()
        {
            // The rectangle is the point of 'boop' for the aabb thingy.
            for (var x = 0; x < this.TheState.Map.Count; ++x)
            {
                //var beep = this.ColourSquares.Count() / x;
                //var tile = this.ColourSquares[beep];
                var item = this.TheState.Map[x];
                this._spriteBatch.Draw(this.ColourSquares[4], item, Color.White);

            }


            //foreach (var item in this.boundaryMap.ViewPortCollisions)
            //{
            //    this._spriteBatch.Draw(this.GreenSquare, item, Color.White);
            //}
        }

    }
}
