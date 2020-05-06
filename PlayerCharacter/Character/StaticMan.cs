using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameLibrary.Extensions;

namespace PlayerCharacter.Character
{
    // All the interactivity of a walking man
    // none of the movement.
    internal class StaticMan
    {
        private readonly SpriteBatch sb;
        private readonly Texture2D _head;
        private readonly Texture2D _body;
        private readonly IAnimations _headAnimation;
        private readonly IAnimations _bodyAnimation;
        private readonly Vector2 _currentPosition;
        private Vector2 _currentVelocity;

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
            this._currentVelocity = settings;
        }
    }
}
