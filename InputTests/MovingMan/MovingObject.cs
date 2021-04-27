using GameData;
using GameData.CharacterActions;
using GameLibrary.AppObjects;
using GameLibrary.Extensions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace InputTests.MovingMan
{
    class MovingObject : ICharacterActions
    {
        private readonly SpriteBatch spriteBatch;
        private readonly Dimensions blockSize;
        private readonly BasicVelocityManager velocites;
        private Vector2 _currentPos;
        private float velocityX;
        private float velocityY;
        private Texture2D _texture;

        public MovingObject(SpriteBatch spriteBatch, Dimensions blockSize, BasicVelocityManager velocites, Vector2 startPos)
        {
            this.spriteBatch = spriteBatch;
            this.blockSize = blockSize;
            this.velocites = velocites;
            this._currentPos = startPos;
            velocityX = velocites.VelocityX;
            velocityY = velocites.VelocityY;
            _texture = spriteBatch.CreateFilledRectTexture(new Rectangle(0, 0, blockSize.Width, blockSize.Height), Color.Red);
        }

        public void Update(GameTime gameTime, float deltaTime)
        {
            _currentPos.X += velocites.VelocityX * deltaTime;
            _currentPos.Y += velocites.VelocityY * deltaTime;
        }

        public Vector2 CurrentPosition { get => this._currentPos; }

        public void Draw(GameTime time)
        {
            // we assume that we don't need to sop and create a new abtchj
            spriteBatch.Draw(_texture, this.CurrentPosition, null , Color.White);
        }


        public void MoveLeft()
        {
            this.velocites.SetVelocityX(-44f);
        }

        public void MoveRight()
        {
            this.velocites.SetVelocityX(+44f);
        }

        public void MoveUp()
        {
            this.velocites.SetVelocityY(-44f);
        }

        public void MoveDown()
        {
            this.velocites.SetVelocityY(44f);
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
            this.velocites.SetVelocityX(0f);
            this.velocites.SetVelocityY(0f);
        }

        public void EndMoveLeft()
        {
            if (this.velocites.VelocityX< 0)
                this.velocites.SetVelocityX(0f);
        }

        public void EndMoveRight()
        {
            if (this.velocites.VelocityX > 0)
                this.velocites.SetVelocityX(0f);
        }

        public void EndMoveDown()
        {
            if (this.velocites.VelocityY > 0)
                this.velocites.SetVelocityY( 0f);
        }

        public void EndMoveUp()
        {
            if (this.velocites.VelocityY < 0)
                this.velocites.SetVelocityY( 0f);
        }

        public void Jump()
        {
            throw new NotImplementedException();
        }
        public void Action()
        {

        }
        public void EndAction()
        {

        }

        public void EndFireSpecial()
        {

        }
    }
}
