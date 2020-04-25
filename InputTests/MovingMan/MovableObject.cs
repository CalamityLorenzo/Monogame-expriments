using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace InputTests.MovingMan
{
    internal class MovableObject : IWalkingMan
    {
        private readonly SpriteBatch spriteBatch;
        private readonly Texture2D imagetoDo;
        private Vector2 _currentPos;
        public MovableObject(SpriteBatch spriteBatch, Texture2D imagetoDo, Vector2 startpos)
        {
            this.spriteBatch = spriteBatch;
            this.imagetoDo = imagetoDo;
            this._currentPos = startpos;
        }

        public Vector2 CurrentPosition => _currentPos;

        public void Update(GameTime gameTime, float deltaTime)
        {

        }

        public void Draw(GameTime time)
        {
            // we assume that we don't need to sop and create a new abtchj

            spriteBatch.Draw(imagetoDo, this.CurrentPosition, Color.White);

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

        public void Fire()
        {
            throw new NotImplementedException();
        }

        public void DoubleClickFire()
        {
            throw new NotImplementedException();
        }
    }
}
