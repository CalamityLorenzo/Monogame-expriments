using GameLibrary.AppObjects;
using GameLibrary.Extensions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace InputTests.MovingMan
{
    class MovingObject : IWalkingMan
    {
        private readonly SpriteBatch spriteBatch;
        private readonly Dimensions blockSize;
        private Vector2 _currentPos;
        private float velocityX;
        private float velocityY;
        private Texture2D _texture;

        public MovingObject(SpriteBatch spriteBatch, Dimensions blockSize, Vector2 startPos)
        {
            this.spriteBatch = spriteBatch;
            this.blockSize = blockSize;
            this._currentPos = startPos;
            velocityX = 0f;
            velocityY = 0f;
            _texture = spriteBatch.CreateFilledRectTexture(new Rectangle(0, 0, blockSize.Width, blockSize.Height), Color.Red);
        }

        public void Update(GameTime gameTime, float deltaTime)
        {
            _currentPos.X += velocityX * deltaTime;
            _currentPos.Y += velocityY * deltaTime;
        }

        public Vector2 CurrentPosition { get => this._currentPos; }

        public void Draw(GameTime time)
        {
            // we assume that we don't need to sop and create a new abtchj
            spriteBatch.Draw(_texture, this.CurrentPosition, null , Color.White);
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

        public void Fire()
        {
            Console.WriteLine("Pew Pew");
        }

        public void FireSpecial()
        {
            Console.WriteLine("Pew Pew");
        }

        public void Standing()
        {
            this.velocityX = 0f;
            this.velocityY = 0f;
        }

        public void EndMoveLeft()
        {
            if (velocityX < 0)
                this.velocityX = 0f;
        }

        public void EndMoveRight()
        {
            if (velocityX > 0)
                this.velocityX = 0f;
        }

        public void EndMoveDown()
        {
            if (velocityY > 0)
                this.velocityY = 0f;
        }

        public void EndMoveUp()
        {
            if (velocityY < 0)
                this.velocityY = 0f;
        }
    }
}
