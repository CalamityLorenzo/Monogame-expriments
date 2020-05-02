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
        }

        public void Update(GameTime gametime, float deltaTime)
        {

        }

        public void Draw(GameTime gameTime)
        {

        }
    }
}
