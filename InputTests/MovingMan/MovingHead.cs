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
        private readonly Dimensions widthHeight;

        public double ViewingAngle { get; private set; }

        public MovingHead(Vector2 topLeft, Dimensions widthHeight)
        {
            this.topLeft = topLeft;
            this.widthHeight = widthHeight;
        }

        public void SetOrigin(Vector2 topleft)
        {
            if (this.topLeft != topleft)
                this.topLeft = topleft;
        }

        public void SetViewDestination(Vector2 terminus)
        {
            //
            var currentHomr = new Vector2(this.topLeft.X + (this.widthHeight.Width / 2), this.topLeft.Y);
            var actual = Vector2.Subtract(terminus, currentHomr);
            var unit = Vector2.Normalize(actual);
            var radi = Math.Atan2(-unit.Y, unit.X);
            this.ViewingAngle = radi * 180/3.14159;
        }

        public void Update(GameTime gametime, float deltaTime)
        {

        }

        public void Draw(GameTime gameTime)
        {

        }
    }
}
