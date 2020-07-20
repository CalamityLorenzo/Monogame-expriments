using GameLibrary.AppObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct3D9;
using SharpDX.XInput;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace InputTests
{
    public class InputTestsGame : Game
    {
        private SpriteBatch spriteBatch;
        private GraphicsDeviceManager graphics;
        private SpriteFont arialFont;

        private InputsManager inputManager;
        private List<string> keysPressedStrings;
        private List<Vector2> keysPressedwidths;
        private string histry;

        public string KeyString { get; private set; }

        public InputTestsGame()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 600;
            graphics.PreferredBackBufferHeight = 600;
            Content.RootDirectory = "Content";

            inputManager = new InputsManager();
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            this.spriteBatch = new SpriteBatch(GraphicsDevice);
            arialFont = this.Content.Load<SpriteFont>("NewArial");

        }

        protected override void Update(GameTime gameTime)
        {
            var kState = Keyboard.GetState();
            var mState= Mouse.GetState();
            inputManager.Update(gameTime, kState, mState);
            this.keysPressedStrings = inputManager.PressedKeys().Select(p => $"{p.Key} : {p.Value}").ToList();
            this.keysPressedwidths = keysPressedStrings.Select(k => arialFont.MeasureString(k)).ToList();
            this.histry = String.Join("\n", inputManager.HistoryKeyboard().Select(o => $"{o.Key} : {o.Value}"));

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            this.spriteBatch.Begin();
            //var vectorStart = new Vector2(10, 15);
            //for (var x = 0; x < this.keysPressedwidths.Count; ++x)
            //{
            //    if (x == 0)
            //    {
            //        this.spriteBatch.DrawString(this.arialFont, keysPressedStrings[x], vectorStart, Color.Red);
            //    }
            //    else
            //    {
            //        vectorStart.X += keysPressedwidths[x - 1].X;
            //        this.spriteBatch.DrawString(this.arialFont, keysPressedStrings[x], vectorStart, Color.Red);
            //    }

            //}

            this.spriteBatch.DrawString(this.arialFont, histry, new Vector2(300, 15), Color.Yellow);

            this.spriteBatch.End();
            //base.Draw(gameTime);
        }
    }
}
