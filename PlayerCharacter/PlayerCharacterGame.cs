using GameLibrary.Extensions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PlayerCharacter.Character;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlayerCharacter
{
    public class PlayerCharacterGame : Game
    {
        GraphicsDeviceManager _graphicsDevice;
        SpriteBatch _spriteBatch;
        private StaticMan statMan;
        SpriteFont arial;
        public PlayerCharacterGame()
        {
            _graphicsDevice = new GraphicsDeviceManager(this);
            _graphicsDevice.PreferredBackBufferWidth = 800;
            _graphicsDevice.PreferredBackBufferHeight = 800;
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(_graphicsDevice.GraphicsDevice);

            this.Content.RootDirectory = "./Content";
            var headRect = new Rectangle(0, 0, 77, 35);
            var bodyRect = new Rectangle(0, 0, 77, 65);
            var head = this._spriteBatch.CreateFilledRectTexture(headRect, Color.LightGoldenrodYellow);
            var body = this._spriteBatch.CreateFilledRectTexture(bodyRect, Color.DeepSkyBlue);

            var headAnims = new HeadAnimations(headRect);
            var bodyAnims = new BodyAnimations(bodyRect);

            this.statMan = new StaticMan(_spriteBatch, head, body, headAnims, bodyAnims, new Vector2(10, 150), new Vector2(0f, 0f));
            this.arial = this.Content.Load<SpriteFont>("NewArial");
        }

        protected override void Update(GameTime gameTime)
        {
            var delat = (float)gameTime.TotalGameTime.TotalSeconds;
            statMan.Update(gameTime, delat);
        }

        protected override void Draw(GameTime gameTime)
        {
            _graphicsDevice.GraphicsDevice.Clear(Color.Black);
            _spriteBatch.Begin();
            statMan.Draw(gameTime);
            _spriteBatch.End();
        }
    }
}
