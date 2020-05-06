using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameLibrary.Extensions;
using GameLibrary.AppObjects;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;
using System.Linq;

namespace PlayerCharacter.Character
{
    // All the interactivity of a walking man
    // none of the movement.
    internal class StaticMan
    {
        internal enum StaticManState
        {
            Unknown=0,
            WalkingLeft,
            WalkingRight,
            WalkingUp,
            WalkingDown,
            Standing,
            Jump,
            Fire,
            StoppedHorizontal,
            StoppedVertical
        }

        private HashSet<StaticManState> _currentStates = new HashSet<StaticManState>();
        private HashSet<StaticManState> _previousStates = new HashSet<StaticManState>();
        private HashSet<StaticManState> _Supplomento = new HashSet<StaticManState>();

        private readonly SpriteBatch sb;
        private readonly Texture2D _head;
        private readonly Texture2D _body;
        private readonly IAnimations _headAnimation;
        private readonly IAnimations _bodyAnimation;
        
        private Vector2 _currentPosition;
        private Vector2 _previousPosition;

        private Vector2 _currentVelocity;
        private Vector2 _previousVelocity;

        private Dimensions _dimensions;
        private Vector2 _mousePosition;


        public StaticMan(SpriteBatch spriteBatch, Texture2D head, Texture2D body, IAnimations headAnimation, IAnimations bodyAnimation, Vector2 topLeft, Vector2 startVelocity)
        {
            this.sb = spriteBatch;
            this._head = head;
            this._body = body;
            this._headAnimation = headAnimation;
            this._bodyAnimation = bodyAnimation;
            this._currentPosition = topLeft;
            this._currentVelocity = startVelocity;
        }

        public void Update(GameTime gameTime, float deltaTime)
        {
    // Position
            _currentPosition.X += this._currentVelocity.X * deltaTime;
            _currentPosition.Y += this._currentVelocity.Y * deltaTime;
   // Update character state
            UpdateCharacterStates();
            UpdateCharacter();
   // update the animation
            _headAnimation.Update(gameTime, deltaTime);
            _bodyAnimation.Update(gameTime, deltaTime);
            var bodyFrame = _bodyAnimation.CurrentFrame();
            var headFrame = _headAnimation.CurrentFrame();
    // Current size of our personage.
            _dimensions = new Dimensions(bodyFrame.Width >= headFrame.Width ? bodyFrame.X : headFrame.Y, bodyFrame.Y + headFrame.Y);

            // begin the circle of liiiiiiife
            _previousPosition = _currentPosition;
            _previousStates = _currentStates;
        }

        private void UpdateCharacter()
        {
            if (this._currentStates.Contains(StaticManState.WalkingLeft)) {
                this._bodyAnimation.WalkLeft();
                
                    }
        }


        // depending on the current actions being applied
        // to position to t
        private void UpdateCharacterStates()
        {
            _currentStates.Clear();
            // FIrst off work out what state(s) we are in before we action them
            if (this._currentVelocity.X > 0f) this._currentStates.Add(StaticManState.WalkingLeft);
            if (this._currentVelocity.X < 0f) this._currentStates.Add(StaticManState.WalkingRight);
            
            if (this._currentVelocity.Y < 0f) this._currentStates.Add(StaticManState.WalkingUp);
            if (this._currentVelocity.Y > 0f) this._currentStates.Add(StaticManState.WalkingDown);

            // If we don't have movement.
            if (this._currentVelocity.X == 0f)
                this._currentStates.Add(StaticManState.StoppedHorizontal);
            if (this._currentVelocity.Y == 0f)
                this._currentStates.Add(StaticManState.StoppedVertical);

            // This includes jumping and shooting.
            _currentStates = _Supplomento.Concat(_currentStates).ToHashSet();

            // WE should only be triggering behaviour, not repeating it every update. We continue doing something until we are not.
            // for instance a jump could take seveeral iterations, no need to bombard with requests once it's kicked off.
            _Supplomento.Clear();
        }

        public void Draw(GameTime gameTime)
        {
            // using the top left as the starting point
            var headDimensions = this._headAnimation.CurrentFrame();
            var bodyDimensions = this._bodyAnimation.CurrentFrame();
            sb.Draw(this._head, this._currentPosition, headDimensions, Color.White);
            sb.Draw(this._body, this._currentPosition.AddY(headDimensions.Height), bodyDimensions, Color.White);
        }

        public void SetXVelocity(float X)
        {
            SetVelocity(new Vector2(X, this._currentVelocity.Y));
        }

        public void SetYVelocity(float Y)
        {
            SetVelocity(new Vector2(this._currentVelocity.X, Y));
        }

        public void SetVelocity(Vector2 settings)
        {
            _previousVelocity = _currentVelocity;
            this._currentVelocity = settings;
        }

        public void SetMouseLook(Vector2 MousePosition)
        {
            this._mousePosition = MousePosition;
        }

        public void FireWeapon()
        {
            this._Supplomento.Add(StaticManState.Fire);
        }

        public void Jump()
        {
            this._Supplomento.Add(StaticManState.Jump);
        }

        public Vector2 GetVelocity() => this._currentVelocity;

        public Dimensions Dimensions => _dimensions;

        public Rectangle CurrentPosition() => new Rectangle(_currentPosition.ToPoint(), _dimensions.ToPoint());


    }
}
