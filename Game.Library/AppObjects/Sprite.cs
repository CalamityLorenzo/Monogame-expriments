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
        private Point _currentPosition;
        private string _currentAnimationIndex;

        public Sprite(SpriteBatch spritebatch, Texture2D atlas, AnimationPlayer animPlayer, Point startPos)
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

        public Point CurrentPosition { get => _currentPosition; }
        public string CurrentFrameSet { get => this.animPlayer.CurrentSetName(); }

        public void SetCurrentPosition(Point newPosition)
        {
            if (newPosition != this._currentPosition)
            {
                this._currentPosition = newPosition;
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
            var frame  = animPlayer.CurrentFrame();
            var pos = new Rectangle(_currentPosition, new Point(frame.Width, frame.Height));
            spriteBatch.Draw(atlas,pos , Color.White);
        }

    }
}
