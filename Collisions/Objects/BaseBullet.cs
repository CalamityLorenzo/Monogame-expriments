using GameLibrary.Animation;
using GameLibrary.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Collisions.Objects
{
    class BaseBullet : IDrawableGameObject, IInteractiveGameObject
    {
        private Vector2 currentPosition;
        private Vector2 direction;
        private float speed;
        private bool isStruck = false;
        public bool IsStruck => isStruck;
        private readonly SpriteBatch spriteBatch;
        private readonly Texture2D atlas;
        private readonly AnimationPlayer player;
        public Point CurrentPosition { get => this.currentPosition.ToPoint(); }

        public BaseBullet(SpriteBatch spriteBatch, Texture2D atlas, AnimationPlayer player)
        {
            this.spriteBatch = spriteBatch;
            this.atlas = atlas;
            this.player = player;
        }

        public virtual void Draw(GameTime gameTime)
        {
            spriteBatch.Draw(this.atlas, this.currentPosition, this.player.CurrentFrame(), Color.White);
        }

        public Rectangle Area => new Rectangle(this.CurrentPosition, new Point(this.player.CurrentFrame().Width, this.player.CurrentFrame().Height));

        public virtual void Update(float delta)
        {  // ZOOOOM!
            this.currentPosition += direction * (speed * delta);

            //var radians = Math.Atan2(direction.Y, direction.X);

            //this.currentPosition.X += (speed * delta * (float)Math.Cos(radians));
            //this.currentPosition.Y += (speed * delta * (float)Math.Sin(radians));
            player.Update(delta);
        }

        public void Struck() { this.isStruck = true; }

        public void SetCurrentPosition(Point setPosition)
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
