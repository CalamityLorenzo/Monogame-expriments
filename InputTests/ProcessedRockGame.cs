using GameLibrary;
using GameLibrary.Animation;
using GameLibrary.AppObjects;
using InputTests.WalkingManCommands;
using InputTests.Handler;
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
using System.Windows.Forms;
using Keys = Microsoft.Xna.Framework.Input.Keys;
using GameLibrary.Extensions;

namespace InputTests
{
    public class ProcessedRockGAme : Game
    {
        private GraphicsDeviceManager graphics;
        private Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch;
        private SpriteFont arialFont;
        private InputsManager inputsManager;
        private InputsProcessor inputsProcessor;

        private List<KeyCommand<IWalkingMan>> player1Inputs;
        private List<KeyCommand<Rotator>> rTaterInputs;
        private MovingObject movingObject;

        private Rotator rTater;
        private Vector2 _centrePoint;

        public ProcessedRockGAme()
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


            var centreHoriz = this.GraphicsDevice.Viewport.Width / 2;
            var centreVert = this.GraphicsDevice.Viewport.Height / 2;
            // create the integral to convert to float/
            _centrePoint = new Point(centreHoriz, centreVert).ToVector2();

            this.inputsProcessor = new InputsProcessor(inputsManager);
            this.player1Inputs = PlayerCommands.SetCommands(p1Controls);
            this.rTaterInputs = PlayerCommands.SetRotatorCommands(p1Controls);
            this.rTater = new Rotator(47, 80.4f);
            this.movingObject = new MovingObject(this.spriteBatch, new Dimensions(50, 50), new Vector2(80, 180));

        }

        protected override void Update(GameTime gameTime)
        {
            var kState = Keyboard.GetState();
            var mState = Mouse.GetState();

            // Keys that are pressed
            this.inputsManager.Update(gameTime, kState, mState);
            // Escape hatch
            KeyboardFunctions.QuitOnKeys(this, this.inputsManager.GetInputState().PressedKeys, Keys.Escape);
            //time
            var delta = (float)gameTime.ElapsedGameTime.TotalSeconds;

            var command = this.inputsProcessor.Process(this.player1Inputs);
            var rTaterCommand = this.inputsProcessor.Process(this.rTaterInputs);
            if(command!=null)
                command.Execute(movingObject);
            if (rTaterCommand != null)
                rTaterCommand.Execute(this.rTater);

            movingObject.Update(gameTime, delta);
            rTater.Update(delta);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            this.spriteBatch.Begin();
            movingObject.Draw(gameTime);

            //// not an effective way of doing this.
            spriteBatch.DrawLine(_centrePoint.AddX(1), 99, this.rTater.CurrentAngle, Color.White);
            spriteBatch.DrawLine(_centrePoint, 100, this.rTater.CurrentAngle, Color.White);
            spriteBatch.DrawLine(_centrePoint.AddX(-1), 99, this.rTater.CurrentAngle, Color.White);

            this.spriteBatch.End();
        }
    }
}
