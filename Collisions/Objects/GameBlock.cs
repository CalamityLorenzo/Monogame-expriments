using GameLibrary.AppObjects;
using GameLibrary.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CollisionsGame.Objects
{
    internal class GameBlock : IDrawableGameObject, IUpdateableGameObject,  IInteractiveGameObject
    {
        private readonly SpriteBatch spriteBatch;
        private readonly Texture2D block;

        public Point CurrentPosition { get; private set; }
        public Rectangle Area { get; private set; }
        public float Health { get; set; }

        public GameBlock(SpriteBatch spriteBatch, Texture2D block, Point startPos, Dimensions dimensions, float startingHealth)
        {
            this.Area = new Rectangle(startPos, dimensions.ToPoint());
            this.spriteBatch = spriteBatch;
            this.block = block;
            this.CurrentPosition = startPos;
            this.Health = startingHealth;
        }

        public void SetCurrentPosition(Point position)
        {
            if (CurrentPosition != position)
            {
                CurrentPosition = position;
                this.Area = new Rectangle(CurrentPosition, new Point(Area.Width, Area.Height));
            }
        }

        public void Draw(GameTime gameTime)
        {
            this.spriteBatch.Draw(this.block, this.Area, Color.White);
        }

        public void Update(float mlSinceupdate)
        {
            // mmmm what happens if struck.
        }

        public virtual void Hit()
        {
            this.Health -= 10;
        }
    }
}
