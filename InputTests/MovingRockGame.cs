using GameLibrary;
using GameLibrary.Animation;
using GameLibrary.AppObjects;
using GameLibrary.InputManagement;
using InputTests.KeyboardInput;
using InputTests.MovingMan;
using InputTests.WalkingManCommands;
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
        private InputsStateManager inputsManager;

        private MovingObject movingObject;
        private List<KeyCommand<IWalkingMan>> p1Commands;
        private MouseKeyboardInputsProcessor inputProcessor;

        public MovingRockGame()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 800;
            Content.RootDirectory = "Content";

            inputsManager = new InputsStateManager();
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

            
            this.p1Commands = PlayerCommands.SetCommands(p1Controls);
            this.inputProcessor = new MouseKeyboardInputsProcessor(this.inputsManager);
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
            var command = this.inputProcessor.Process(this.p1Commands);
            if(command!=null)
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
