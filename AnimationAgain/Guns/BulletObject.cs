using AnimationAgain.Animation;
using GameLibrary.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Color = Microsoft.Xna.Framework.Color;
using Point = Microsoft.Xna.Framework.Point;

namespace AnimationAgain.Guns
{
    internal class BulletObject : IDrawableGameObject
    {
        private Vector2 currentPosition;
        private Vector2 direction;
        private float speed;
        private readonly SpriteBatch spriteBatch;
        private readonly Texture2D atlas;
        private readonly AnimationPlayer player;
        public Vector2 CurrentPosition { get => this.currentPosition; }
        public BulletObject(SpriteBatch spriteBatch, Texture2D atlas, AnimationPlayer player)
        {
            this.spriteBatch = spriteBatch;
            this.atlas = atlas;
            this.player = player;
        }

        public virtual void Draw(GameTime gameTime)
        {
            spriteBatch.Draw(this.atlas, this.currentPosition, this.player.CurrentFrame(), Color.White);
        }

        public virtual void Update(float delta)
        {  // ZOOOOM!
            this.currentPosition += direction * (speed * delta);

            //var radians = Math.Atan2(direction.Y, direction.X);

            //this.currentPosition.X += (speed * delta * (float)Math.Cos(radians));
            //this.currentPosition.Y += (speed * delta * (float)Math.Sin(radians));
            player.Update(delta);
        }

        public void SetPosition(Point setPosition)
        {
            this.currentPosition = setPosition.ToVector2();
        }

        public void SetDirection(Vector2 unitDirection)
        {
            this.direction = unitDirection;
        }

        public void SetSpeed(float speed)
        {
            this.speed = speed;
        }
    }
}
