using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace InputTests.MovingMan
{
    internal class PositionsVelocites : IWalkingMan
    {
        private float _currentXVelocity;
        private float _currentYVelocity;
        public PositionsVelocites(float startingXSpeed, float startingYSpeed)
        {
            _currentXVelocity = startingXSpeed;
            _currentYVelocity = startingYSpeed;
        }

        public void Update(GameTime gameTime, float deltaTime)
        {

        }

        public void DoubleClickFire()
        {
            throw new NotImplementedException();
        }

        public void Fire()
        {
            throw new NotImplementedException();
        }

        public void MoveLeft()
        {
            throw new NotImplementedException();
        }

        public void MoveRight()
        {
            throw new NotImplementedException();
        }

        public void MoveUp()
        {
            throw new NotImplementedException();
        }

        public void MoveDown()
        {
            throw new NotImplementedException();
        }

        public void Standing()
        {
            throw new NotImplementedException();
        }
    }
}
