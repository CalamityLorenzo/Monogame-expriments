using GameLibrary;
using GameLibrary.Extensions;
using InputTests.MovingMan;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace InputTests
{
    public class MovingObjectGame : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private SpriteFont arialFont;
        private KeysManager kManger;

        private MovableObject _mo;

        public MovingObjectGame()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 800;
            Content.RootDirectory = "Content";

            kManger = new KeysManager();
        }


        protected override void LoadContent()
        {
            this.spriteBatch = new SpriteBatch(GraphicsDevice);
            arialFont = this.Content.Load<SpriteFont>("NewArial");
            var Red40x40 = this.spriteBatch.CreateFilleRectTexture(new Rectangle(0, 0, 40, 40), Color.Red);
            _mo = new MovableObject(this.spriteBatch, Red40x40, new Vector2(30, 30));
        }

        protected override void Update(GameTime gameTime)
        {
            var kState = Keyboard.GetState();
            // Escape hatch
            KeyboardFunctions.QuitOnKeys(this, kState, Keys.Escape);
            var delta = (float)gameTime.ElapsedGameTime.TotalSeconds;

            kManger.Update(gameTime, kState);
            _mo.Update(gameTime,delta);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            this.spriteBatch.Begin();
            _mo.Draw(gameTime);
            this.spriteBatch.End();
        }
    }
}
