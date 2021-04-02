using GameLibrary.Animation;
using GameLibrary.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameLibrary.AppObjects
{
    public class Sprite : IDrawableGameObject, IInteractiveGameObject
    {
        private readonly Texture2D atlas;
        private readonly SpriteBatch spriteBatch;
        private readonly AnimationPlayer animPlayer;
        private Vector2 _currentPosition;
        private string _currentAnimationIndex;

        public Sprite(SpriteBatch spritebatch, Texture2D atlas, AnimationPlayer animPlayer, Vector2 startPos)
        {
            if (spritebatch is null)
            {
                throw new System.ArgumentNullException(nameof(spritebatch));
            }

            if (animPlayer is null)
            {
                throw new System.ArgumentNullException(nameof(animPlayer));
            }

            if (atlas is null)
            {
                throw new System.ArgumentNullException(nameof(atlas));
            }

            this.atlas = atlas;
            this.spriteBatch = spritebatch;
            this.animPlayer = animPlayer;
            this._currentPosition = startPos;
        }

        public Point CurrentPosition { get => _currentPosition.ToPoint(); }
        public string CurrentFrameSet { get => this.animPlayer.CurrentSetName(); }

        public void SetCurrentPosition(Point newPosition)
        {
            var newPositionVector = newPosition.ToVector2();
            if (newPositionVector != this._currentPosition)
            {
                this._currentPosition = newPositionVector;
            }
        }

        public void SetAnimation(string animationSet)
        {
            _currentAnimationIndex = animationSet;
            this.animPlayer.SetFrames(_currentAnimationIndex);
        }

        public int GetCurrentFrameId() => this.animPlayer.CurrentFrameIdx();

        public Rectangle Area { get => this.animPlayer.CurrentFrame(); }

        public void Update(float mlSinceupdate)
        {
            this.animPlayer.Update(mlSinceupdate);
        }

        public void Draw(GameTime gameTime)
        {
            spriteBatch.Draw(atlas, this._currentPosition, animPlayer.CurrentFrame(), Color.White);
        }

    }
}
