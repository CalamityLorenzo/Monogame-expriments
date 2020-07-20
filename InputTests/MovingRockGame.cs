using GameLibrary;
using GameLibrary.Animation;
using GameLibrary.AppObjects;
using InputTests.Commands;
using InputTests.Inputs;
using InputTests.KeyboardInput;
using InputTests.MovingMan;
using Library.Animation;
using Microsoft.Xna.Framework;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace InputTests
{
    public class MovingRockGame : Game
    {
        private GraphicsDeviceManager graphics;
        private Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch;
        private SpriteFont arialFont;
        private InputsManager inputsManager;
        private InputHandler inputHandler;

        private MovingObject movingObject;

        public MovingRockGame()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 800;
            Content.RootDirectory = "Content";

            inputsManager = new InputsManager();
        }

        protected override void LoadContent()
        {
            this.spriteBatch = new Microsoft.Xna.Framework.Graphics.SpriteBatch(GraphicsDevice);
            arialFont = this.Content.Load<SpriteFont>("NewArial");

            var p1Controls = new PlayerControlKeys
            {
                Up = Keys.W,
                Down = Keys.S,
                Left = Keys.A,
                Right = Keys.D,
                Fire = Keys.LeftControl,
                SecondFire = Keys.Space
            };

            this.inputHandler = new InputHandler(p1Controls, inputsManager, new ActorCommandsList());

            this.movingObject = new MovingObject(this.spriteBatch, new Dimensions(50, 50), new Vector2(80, 180));

        }

        protected override void Update(GameTime gameTime)
        {
            var kState = Keyboard.GetState();
            var mState = Mouse.GetState();

            this.inputsManager.Update(gameTime, kState, mState);
            // Escape hatch
            KeyboardFunctions.QuitOnKeys(this, this.inputsManager.GetInputState().PressedKeys, Keys.Escape);
            //time
            var delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
            
            //! Check all the keys and shit
            var command = this.inputHandler.Update(gameTime);

            command.Execute(movingObject);

            movingObject.Update(gameTime, delta);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            this.spriteBatch.Begin();
            movingObject.Draw(gameTime);
            this.spriteBatch.End();
        }
    }
}
