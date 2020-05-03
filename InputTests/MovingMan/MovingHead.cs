using GameLibrary.AppObjects;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace InputTests.MovingMan
{
    public class MovingHead
    {
        private Vector2 topLeft;
        private Vector2 _viewTerminus;
        private Vector2 _currentHeadVector;
        private readonly Dimensions widthHeight;
        private Vector2 centralPoint;

        public double ViewingAngle { get; private set; }

        public MovingHead(Vector2 topLeft, Dimensions widthHeight)
        {
            this.topLeft = topLeft;
            this.widthHeight = widthHeight;
            // All things should revolve around this.
            this.centralPoint = new Vector2(this.topLeft.X + (this.widthHeight.Width / 2), this.topLeft.Y);
            this._currentHeadVector = new Vector2(10, 1);
        }

        public Vector2 TopLeft => this.topLeft;

        public void SetOrigin(Vector2 topleft)
        {
            if (this.topLeft != topleft)
            {
                this.topLeft = topleft;
                this.centralPoint = new Vector2(this.topLeft.X + (this.widthHeight.Width / 2), this.topLeft.Y);
            }
        }
        public void SetViewDestination(Vector2 terminus)
        {
            this._viewTerminus = terminus;
        }

        public void Update(GameTime gametime, float deltaTime)
        {
            var headVector = Vector2.Subtract(_viewTerminus, centralPoint);

            if (headVector != this._currentHeadVector) // If the body, or mousr pointer has moved
            {
                var unit = Vector2.Normalize(headVector);
                var radi = Math.Atan2(-unit.Y, unit.X);
                this.ViewingAngle = radi * 180 / 3.14159;
                this._currentHeadVector = headVector;
            }
        }

        public void Draw(GameTime gameTime)
        {

        }
    }
}
