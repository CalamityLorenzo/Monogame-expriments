using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlayerCharacter
{
    internal class PlayerCharacterGame : Game
    {
        GraphicsDeviceManager _graphicsDevice;
        SpriteBatch _spriteBatch;
        SpriteFont arial;
        public PlayerCharacterGame()
        {
            _graphicsDevice = new GraphicsDeviceManager(this);
            _graphicsDevice.PreferredBackBufferWidth = 800;
            _graphicsDevice.PreferredBackBufferHeight = 800;
        }

        protected override void Initialize()
        {
            _spriteBatch = new SpriteBatch(_graphicsDevice.GraphicsDevice);
        }

        protected override void LoadContent()
        {
            this.Content.RootDirectory = "./Content";

            this.arial = this.Content.Load<SpriteFont>("NewArial");
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            _graphicsDevice.GraphicsDevice.Clear(Color.AntiqueWhite);
            _spriteBatch.Begin();
            _spriteBatch.End();
        }
    }
}
