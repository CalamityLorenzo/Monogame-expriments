using Library.Animation;
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

        private float velocityX;
        private float velocityY;
        private BlockAnimationObject bla;

        public MovableObject(SpriteBatch spriteBatch, Texture2D imagetoDo, BlockAnimationObject bla, Vector2 startpos): this(spriteBatch, imagetoDo, startpos)
        {
            this.bla = bla;
            this.bla.Start();
        }

            public MovableObject(SpriteBatch spriteBatch, Texture2D imagetoDo, Vector2 startpos)
        {
            this.spriteBatch = spriteBatch;
            this.imagetoDo = imagetoDo;
            this._currentPos = startpos;
            velocityX = 0f;
            velocityY = 0f;
        }

        public Vector2 CurrentPosition => _currentPos;

        public void Update(GameTime gameTime, float deltaTime)
        {
            _currentPos.X += velocityX * deltaTime;
            _currentPos.Y += velocityY * deltaTime;
            //velocityY = 0f;
            //velocityX = 0f;

            if (bla != null)
                bla.Update(gameTime, deltaTime);
        }

        public void Draw(GameTime time)
        {
            // we assume that we don't need to sop and create a new abtchj
            if(bla!=null)
                spriteBatch.Draw(imagetoDo, this.CurrentPosition, bla.CurrentFrame,  Color.White);
            else
                spriteBatch.Draw(imagetoDo, this.CurrentPosition, Color.White);

        }

        public void MoveLeft()
        {
            this.velocityX = -44f;
        }

        public void MoveRight()
        {
            this.velocityX = +44f;
        }

        public void MoveUp()
        {
            this.velocityY = -44f;
        }

        public void MoveDown()
        {
            this.velocityY = 44f;
        }

        public void Standing()
        {
            this.velocityY = 0f;
            this.velocityX = 0f;

        }


        public void Fire()
        {
            Console.WriteLine("Pew Pew");
        }

        public void DoubleClickFire()
        {
            Console.WriteLine("Pew Pew");
        }
    }
}
