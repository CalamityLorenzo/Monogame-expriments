using GameData;
using GameData.CharacterActions;
using GameData.Commands;
using GameData.UserInput;
using GameLibrary;
using GameLibrary.AppObjects;
using GameLibrary.InputManagement;

using InputTests.MovingMan;
using Microsoft.Xna.Framework;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

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
        private MouseKeyboardInputsReciever inputProcessor;

        public BasicVelocityManager VelocityManager { get; private set; }

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

            var p1Controls = new PlayerKeyboardControls
            {
                Up = Keys.W,
                Down = Keys.S,
                Left = Keys.A,
                Right = Keys.D,
                Fire = Keys.LeftControl,
                SecondFire = Keys.Space
            };


            this.p1Commands = CommandBuilder.GetWalkingCommands(p1Controls);
            this.inputProcessor = new MouseKeyboardInputsReciever(this.inputsManager);
            this.VelocityManager = new BasicVelocityManager(0f, 0f);

            this.movingObject = new MovingObject(this.spriteBatch, new Dimensions(50, 50), this.VelocityManager, new Vector2(80, 180));

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
            var cmds = this.inputProcessor.MapKeyboardCommands(this.p1Commands);
            cmds.ForEach(command => command.Execute(movingObject));

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
