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

        public string ReactionType { get; }
        public Point CurrentPosition { get; private set; }
        public Rectangle Area { get; private set; }
        public GameBlock(SpriteBatch spriteBatch, Texture2D block, string reactionType, Point startPos, Dimensions dimensions)
        {
            this.Area = new Rectangle(startPos, dimensions.ToPoint());
            this.spriteBatch = spriteBatch;
            this.block = block;
            ReactionType = reactionType;
            this.CurrentPosition = startPos;
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
    }
}
