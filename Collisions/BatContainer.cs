using GameData.CharacterActions;
using GameLibrary.AppObjects;
using GameLibrary.Extensions;
using GameLibrary.Interfaces;
using Microsoft.Xna.Framework;

namespace CollisionsGame
{
    class BatContainer : IInteractiveGameObject, ICharacterActions
    {
        private readonly Sprite batSprite;
        private readonly IVelocinator velocity;
        private readonly Vector2 velocitySpeed;
        private Vector2 _currentPosition;
        private Vector2 _previousPosition;

        public Point CurrentPosition => _currentPosition.ToPoint();

        public Rectangle Area => new Rectangle(this.CurrentPosition.Add(8,4), new Point( this.batSprite.Area.Width-22, batSprite.Area.Height-25));

        public bool Leftin { get; private set; }
        public bool Rightin { get; private set; }
        public bool Upsie { get; private set; }
        public bool Downsie { get; private set; }

        public BatContainer(Sprite batSprite, Point startPos, IVelocinator velocity, Vector2 velocitySpeed)
        {
            this.batSprite = batSprite;
            this.velocity = velocity;
            this.velocitySpeed = velocitySpeed;
            this.batSprite.SetCurrentPosition(startPos);
            this._currentPosition = startPos.ToVector2();
        }

        public void SetCurrentPosition(Point setPosition)
        {
            this._currentPosition = setPosition.ToVector2();
        }

        public void Update(float deltaTime, World theState)
        {
            this._currentPosition = _currentPosition
                    .Add(this.velocity.VelocityX * deltaTime, velocity.VelocityY * deltaTime);

            var elCollido = theState.MapCollision(this.Area);

            if (elCollido != null){
                this._currentPosition = _previousPosition;
            }



            batSprite.SetCurrentPosition(_currentPosition.ToPoint());
            this.batSprite.Update(deltaTime);
            _previousPosition = _currentPosition;
        }



        public void Draw(GameTime gametime)
        {
            this.batSprite.Draw(gametime);
        }

        public void MoveLeft()
        {
            this.Leftin = true;
            this.velocity.SetVelocityX(-this.velocitySpeed.X);
        }

        public void MoveRight()
        {
            this.Rightin = true;
            this.velocity.SetVelocityX(this.velocitySpeed.X);
        }

        public void MoveUp()
        {
            this.Upsie = true;
            this.velocity.SetVelocityY(-this.velocitySpeed.Y);
        }

        public void MoveDown()
        {
            this.Downsie = true;
            this.velocity.SetVelocityY(this.velocitySpeed.Y);
        }

        public void EndMoveLeft()
        {
            this.Leftin = false;
            if(this.Rightin ==false)
                this.velocity.SetVelocityX(0f);
        }

        public void EndMoveRight()
        {
            this.Rightin = false;
            if (this.Leftin == false)
                this.velocity.SetVelocityX(0f);
        }

        public void EndMoveDown()
        {
            this.Downsie = false;
            if (this.Upsie == false)
                this.velocity.SetVelocityY(0f);
        }

        public void EndMoveUp()
        {
            this.Upsie = false;
            if (this.Downsie == false)
                this.velocity.SetVelocityY(0f);
        }

        public void Fire()
        {
        }

        public void FireSpecial()
        {
        }

        public void Jump()
        {
        }

        public void Standing()
        {
        }
    }
}
