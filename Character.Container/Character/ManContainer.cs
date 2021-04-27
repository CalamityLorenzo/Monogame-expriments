 using GameData.CharacterActions;
using GameLibrary.AppObjects;
using GameLibrary.Extensions;
using GameLibrary.Interfaces;
using Microsoft.Xna.Framework;
using System;
using System.Diagnostics;

namespace Character.Container.Character
{
    internal class ManContainer : ICharacterActions, IInteractiveGameObject
    {
        public World World { get; private set; }

        private Vector2 _currentPosition;
        private Rectangle _area;
        private readonly IVelocinator velocity;
        private Vector2 _speed;
        private readonly Sprite body;
        private bool isJumping;
        private Vector2 headOffset;
        private Sprite head;
        private readonly BaseGun gun;
        private float previousFacingAngle;
        private float currentFacingAngle;

        public ManContainer(Point startPosition, IVelocinator velocity, Vector2 speed, BaseGun gun, Sprite body, Sprite head)
        {
            this._currentPosition = startPosition.ToVector2();
            this.velocity = velocity;
            this._speed = speed;

            this.body = body;
            this.body.SetAnimation("Standing");
            this.body.SetCurrentPosition(startPosition);
            this.headOffset = new Vector2(1, 37);
            this.head = head;
            this.head.SetAnimation("FaceLeft");
            var headStart = startPosition.ToVector2().Subtract(headOffset).ToPoint();
            this.head.SetCurrentPosition(headStart);

            //Set the relative point for calculating angles

            this.gun = gun;
            this.gun.SetCurrentPosition(startPosition
                                                .Subtract(headOffset)
                                                .AddX(this.head.Area.Width / 2));
        }

        public void SetCurrentPosition(Point newPosition)
        {
            var newPositionVector = newPosition.ToVector2();
            if (newPositionVector != this._currentPosition)
            {
                this._currentPosition = newPositionVector;
            }
        }
        public Point CurrentPosition { get => _currentPosition.ToPoint(); }

        public Rectangle Area { get => this._area; }

        public void Update(float deltaTime, World theState)
        {
            this.World = theState;

            this._currentPosition = _currentPosition
                    .Add(velocity.VelocityX * deltaTime, velocity.VelocityY * deltaTime);

            this.body.SetCurrentPosition(_currentPosition.ToPoint());
            this.head.SetCurrentPosition(_currentPosition.Subtract(headOffset).ToPoint());
            // Where we reference our point from 
            this.gun.SetCurrentPosition(this.head.CurrentPosition.AddX((int)this.head.Area.Width / 2));
            this.body.Update(deltaTime);
            this.head.Update(deltaTime);
            this.gun.Update(deltaTime);
            //this.body.SetCurrentPosition(_currentPosition.ToPoint());
            this.UpdateAnimation(World.InputState.MousePosition.ToVector2());
        }

        private void UpdateAnimation(Vector2 mousePosition)
        {

            var _vectorToDestination = Vector2.Subtract(mousePosition, _currentPosition);
            var radians = Math.Atan2(_vectorToDestination.Y, _vectorToDestination.X);

            var rawAngle = (float)(radians * (180 / 3.14159));
            var headAngle = rawAngle + (rawAngle <= -90 ? 360 + 90 : 90);
            if (headAngle != currentFacingAngle)
            {
                previousFacingAngle = currentFacingAngle;
                currentFacingAngle = headAngle;
                //mouseActive /= true;
                var facingHead = headAngle switch
                {
                    var f when f > 330 && f <= 360 || f >= 0f && f <= 30f => "FaceUp",
                    var f when f > 30f && f <= 60 => "FaceRightUp",
                    var f when f > 60f && f <= 90 => "FaceRight",
                    var f when f > 300 && f <= 330 => "FaceLeftUp",
                    var f when f >= 270 && f <= 330 => "FaceLeft",
                    _ => this.head.CurrentFrameSet
                };
                this.head.SetAnimation(facingHead);
            }
            /// <summary>
            /// This basically resets the animation status for whennothing is happening.
            /// </summary>

            if ((this.velocity.VelocityX < 1 && velocity.VelocityX > -1) && (velocity.VelocityY < 1 && velocity.VelocityY > -1))
            {
                this.body.SetAnimation("Standing");
                //if (this.head.CurrentFrameSet.Contains("Left"))
                //    this.head.SetAnimation("BobLeft");
                //else
                //    this.head.SetAnimation("BobRight");
            }

        }

        public void Draw(GameTime gameTime)
        {
            this.body.Draw(gameTime);
            this.head.Draw(gameTime);
            this.gun.Draw(gameTime);
        }


        #region IWALKINGMAN
        public void MoveLeft()
        {
            this.velocity.SetVelocityX(-this._speed.X);
            if (!this.isJumping)
            {
                this.body.SetAnimation("Left");
            }
            this.head.SetAnimation("FaceLeft");

        }

        public void MoveRight()
        {
            this.velocity.SetVelocityX(this._speed.Y);
            if (!this.isJumping)
            {
                this.body.SetAnimation("Right");
            }
            this.head.SetAnimation("FaceRight");

        }

        public void MoveUp()
        {
            this.velocity.SetVelocityY(-this._speed.Y);
            //SetAnimation(0);
            if (this.body.CurrentFrameSet != "Left" || this.body.CurrentFrameSet != "Right")
                this.body.SetAnimation("Left");

            if (this.velocity.VelocityX < 1f)
                this.head.SetAnimation("FaceLeft");
            else
                this.head.SetAnimation("FaceRight");
        }

        public void MoveDown()
        {
            this.velocity.SetVelocityY(this._speed.Y);
            if (this.body.CurrentFrameSet != "Left" || this.body.CurrentFrameSet != "Right")
                this.body.SetAnimation("Right");
            if (this.velocity.VelocityX < 1f)
                this.head.SetAnimation("FaceLeft");
            else
                this.head.SetAnimation("FaceRight");
        }

        public void EndMoveLeft()
        {
            if (this.velocity.VelocityX < 0)
                this.velocity.SetVelocityX(0f);
        }

        public void EndMoveRight()
        {
            if (this.velocity.VelocityX > 0)
                this.velocity.SetVelocityX(0f);
        }

        public void EndMoveDown()
        {
            if (this.velocity.VelocityY > 0)
                this.velocity.SetVelocityY(0f);
        }

        public void EndMoveUp()
        {
            if (this.velocity.VelocityY < 0)
                this.velocity.SetVelocityY(0f);
        }

        public void Jump()
        {
            if (!this.isJumping) { }
        }

        public void Fire()
        {
            var _vectorToDestination = Vector2.Subtract(this.World.InputState.MousePosition.ToVector2(), _currentPosition);
            Debug.WriteLine($"Raw : {_vectorToDestination}");
            _vectorToDestination.Normalize();
            Debug.WriteLine($"Normalized: ${_vectorToDestination}");
            var radians = Math.Atan2(_vectorToDestination.Y, _vectorToDestination.X);
            Debug.WriteLine($"Radians: ${radians}");
            this.gun.Fire(this.World.InputState.MousePosition.ToVector2());

        }

        public void FireSpecial()
        {
            // throw new System.NotImplementedException();
        }

        public void Standing()
        {
            // throw new System.NotImplementedException();
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
        #endregion
    }
}
