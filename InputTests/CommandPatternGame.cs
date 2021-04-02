using GameData.CharacterActions;
using GameData.Commands;
using GameData.UserInput;
using GameLibrary;
using GameLibrary.Animation;
using GameLibrary.Animation.Utilities;
using GameLibrary.AppObjects;
using GameLibrary.InputManagement;
using InputTests.MovingMan;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace InputTests
{
    public class CommandPatternGame : Game
    {
        private GraphicsDeviceManager graphics;
        private Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch;
        private InputsStateManager keymouseState;
        private MouseKeyboardInputsReciever inputReciever;
        
        private SpriteFont arialFont;
        private List<KeyCommand<ICharacterActions>> p1Commands;
        private MovingHead headsIWin;

        private MovingObjectAnimation _mo4;
        private CrossHairs _mouseHairs;

        public CommandPatternGame()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 800;
            Content.RootDirectory = "Content";

            keymouseState = new InputsStateManager();
        }

        protected override void LoadContent()
        {
            this.spriteBatch = new Microsoft.Xna.Framework.Graphics.SpriteBatch(GraphicsDevice);
            arialFont = this.Content.Load<SpriteFont>("NewArial");
            var walkingLeft = Texture2D.FromFile(GraphicsDevice, "./Content/WalkingLeft.png");
            var walkingRight = Texture2D.FromFile(GraphicsDevice, "./Content/WalkingRight.png");
            var standing = Texture2D.FromFile(GraphicsDevice, "./Content/Standing.png");
            var wlFrames = FramesGenerator.GenerateFrames(new FrameInfo(72, 77), new Dimensions(walkingLeft.Width, walkingLeft.Height));
            var standingFrames = FramesGenerator.GenerateFrames(new FrameInfo(72, 77), new Dimensions(standing.Width, standing.Height));
            var wlAnimation = new AnimationFramesCollection("MoveLeft", true, 0, wlFrames); // new float[] { 0.200f, 0.200f, 0.200f, 0.200f }, true);
            var standingAnimation = new AnimationFramesCollection("Standing", true,0, standingFrames); // new float[] { 0.500f, 0.250f, 0.250f, 0.250f }, true);
            var wrAnimation = new AnimationFramesCollection("MoveRight", true, 0, wlFrames); // new float[] { 0.200f, 0.200f, 0.200f, 0.200f }, true);

            var walkingAnims = new AnimationPlayer(.200f, new Dictionary<string, AnimationFramesCollection>
            {
                { "MoveLeft", wlAnimation },
                { "MoveRight", wrAnimation },
                {"Standing",  standingAnimation }
            });

            var crossHairs = Texture2D.FromFile(GraphicsDevice, "./Content/CrossHairs_one.png");
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

            this.inputReciever = new MouseKeyboardInputsReciever(keymouseState);

            this.headsIWin = new MovingHead(new Vector2(200, 300), new Dimensions(80, 100));
            _mo4 = new MovingObjectAnimation(this.spriteBatch, walkingLeft, walkingRight, standing, walkingAnims, headsIWin, new Vector2(200, 300));
            _mouseHairs = new CrossHairs(spriteBatch, crossHairs, Mouse.GetState().Position.ToVector2(), new Rectangle(0, 0, this.GraphicsDevice.Viewport.Width, this.GraphicsDevice.Viewport.Height - 200));
        }

        protected override void Update(GameTime gameTime)
        {
            var kState = Keyboard.GetState();
            var mState = Mouse.GetState();
            this.keymouseState.Update(gameTime, kState, mState);
            // Escape hatch
            KeyboardFunctions.QuitOnKeys(this, keymouseState.PressedKeys(), Keys.Escape);
            _mouseHairs.SetCurrentPosition(mState.Position.ToVector2());
            //time
            var delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
            //! Check all the keys and shit
            var command = this.inputReciever.MapKeyboardCommands(this.p1Commands);
            
                command.ForEach(cmd => cmd.Execute(_mo4));

            this.headsIWin.SetViewDestination(mState.Position.ToVector2());
            _mo4.Update(gameTime, delta);
            _mouseHairs.Update(gameTime, delta);
            //this.headsIWin.Update(gameTime, delta);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            this.spriteBatch.Begin();
            _mo4.Draw(gameTime);
            _mouseHairs.Draw(gameTime);
            var mString = this.arialFont.MeasureString($"Angle : {this.headsIWin.ViewingAngle}");
            this.spriteBatch.DrawString(this.arialFont, $"Angle : {this.headsIWin.ViewingAngle}", new Vector2(10, 10), Color.White);
            this.spriteBatch.End();
        }
    }
}
