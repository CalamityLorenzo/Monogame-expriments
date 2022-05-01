using GameLibrary.Animation;
using GameLibrary.AppObjects;
using GameLibrary.Extensions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Collisions.Objects.Balls
{
    class BaseBall : Sprite
    {
        private Vector2 unitDirection;
        private float speed;
        private Point previousPosition;
        private Random random;
        public float Speed => speed;
        public Vector2 Direction => unitDirection;
        public Vector2 Velocity { get; private set; }
        private Vector2 _currentPosition;

        public BaseBall(SpriteBatch spriteBatch, Texture2D atlas, AnimationPlayer player, Point startPos, Vector2 unitDirection, float initialSpeed):base(spriteBatch, atlas, player, startPos)
        {
            this.unitDirection = unitDirection;
            this.speed = initialSpeed;
            random = new Random();
            _currentPosition = this.CurrentPosition.ToVector2();
        }

        
        public void SetSpeed(float newSpeed)
        {
            if (this.speed != newSpeed)
                speed = newSpeed;
        }

        public void SetDirection(Vector2 unitDirection)
        {
            if (unitDirection != this.unitDirection)
                this.unitDirection = unitDirection;
        }

        public override void Update(float delta)
        {
            this.previousPosition = CurrentPosition;
            base.Update(delta);
            // ZOOOOM!
            if (speed > 0f)
            {
                var currentVector = CurrentPosition.ToVector2();
                Velocity = unitDirection * (speed * delta);
                currentVector += Velocity;
                _currentPosition += Velocity;
                this.SetCurrentPosition(currentVector.ToPoint());
            }
        }
        /// <summary>
        /// Based on current angle return a bounce.
        /// </summary>
        /// <param name="currentDegrees"></param>
        private void RandomBouncetDirection(int currentDegrees)
        {
            // Turn the range of degrees in to 4 quads of 90 degrees
            // 315 - 45
            // 46  - 135
            // 136 - 225
            // 226 - 315
            // find where the curentAngle is and return in a range of 160 degrees
            // (this should stop some odd looking straight angles)
            var angleDecider = this.random.Next(-80, 80);

            //var angle = currentDegrees switch
            //{
            //    int ang when ang >= 315 && ang <= 360 || ang >= 0 && ang <= 45 => (angleDecider < 45) ? angleDecider + 315 : angleDecider - 45,
            //    int ang when ang > 45 && ang < 136 => angleDecider + 45,
            //    int ang when ang > 135 && ang < 226 => angleDecider + 135,
            //    int ang when ang > 225 && ang < 315 => angleDecider + 225,
            //   _ => throw new Exception("")
            //};


            var angle = currentDegrees switch
            {
                int ang when ang >= 315 && ang <= 360 || ang >= 0 && ang <= 45 => (angleDecider < 0) ? 360 + angleDecider : angleDecider,
                int ang when ang > 45 && ang < 136 => 90 + angleDecider,
                int ang when ang > 135 && ang < 226 => 180 + angleDecider,
                int ang when ang > 225 && ang < 315 => 270 + angleDecider,
                _ => throw new Exception("Bounce failed!")
            };

            //var angle =currentDegrees+=angleDecider;
            this.SetDirection(GeneralExtensions.UnitVectorFromDegrees(angle));
        }

        // To Bounce from something.
        // Need to know
        // Where we hit this rect (Top bottom right left etc)
        internal void Bounce()
        {
            // move back to where we came from
            this.SetCurrentPosition(previousPosition);
            // get current the current angle in degrees
            //   var degrees = (int)this.unitDirection.GetAngleDegreesFromUnit();
            unitDirection.Normalize();
            this.unitDirection = -unitDirection;

            //var directionX = "";
            //var directionY = "";

            //if (this.unitDirection.X > 0f)
            //    directionX = "Right";
            //else if (this.unitDirection.X < 0f)
            //    directionX = "Left";
            //else directionX = "None";

            //if (this.unitDirection.Y > 0f)
            //    directionY = "Down";
            //else if (this.unitDirection.Y < 0f)
            //    directionY = "Up";
            //else directionY = "None";

            //// now reverse it so it points the other way.
            //degrees = degrees + 180;
            //if (degrees > 360) degrees -= 360;
            //// lets have a semi random bounce.
           // RandomBouncetDirection((int)this.unitDirection.GetAngleDegreesFromUnit());
            
        }
    }
}
