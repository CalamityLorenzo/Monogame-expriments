using Collisions.Objects;
using Collisions.Objects.Paddle;
using GameData.CharacterActions;
using GameLibrary.AppObjects;
using GameLibrary.Extensions;
using GameLibrary.GameObjects;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace CollisionsGame
{
    class BatContainer : GameAgentObject, ICharacterActions, IPaddle
    {
        private readonly BasePaddle batPaddle;
        private readonly BaseGun theGun;
        private readonly IVelocinator velocity;
        private readonly Vector2 velocitySpeed;
        
        public override Rectangle Area => new Rectangle(this.CurrentPosition.Add(8,4), new Point( this.batPaddle.Area.Width-22, batPaddle.Area.Height-25));
        public IList<BaseBullet> FiredBullets => this.theGun.FiredBullets;
        public bool Leftin { get; private set; }
        public bool Rightin { get; private set; }
        public bool Upsie { get; private set; }
        public bool Downsie { get; private set; }

        public BatContainer(BasePaddle batSprite, Point startPos, BaseGun theGun, IVelocinator velocity, Vector2 velocitySpeed) : base(startPos)
        {
            this.batPaddle = batSprite;
            this.theGun = theGun;
            this.velocity = velocity;
            this.velocitySpeed = velocitySpeed;
            this.batPaddle.SetCurrentPosition(startPos);
        }

        public PaddleArea PaddleHit(Rectangle intersect)
        {
            return this.batPaddle.PaddleHit(intersect);
        }

        public override void Update(float deltaTime, World theState)
        {
            var _previousPosition = CurrentPosition;
            this.SetCurrentPosition(CurrentPosition.ToVector2()
                    .Add(this.velocity.VelocityX * deltaTime, velocity.VelocityY * deltaTime).ToPoint());

            var elCollido = theState.MapCollision(this.Area);

            if (elCollido != null){
                SetCurrentPosition( _previousPosition);
            }

            batPaddle.SetCurrentPosition(CurrentPosition);
            theGun.SetCurrentPosition(CurrentPosition);
            this.theGun.Update(deltaTime, theState);
            this.batPaddle.Update(deltaTime);
        }

        public override void Draw(GameTime gametime)
        {
            this.batPaddle.Draw(gametime);
            this.theGun.Draw(gametime);
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
            this.theGun.Fire(new Vector2(0, -1));
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
