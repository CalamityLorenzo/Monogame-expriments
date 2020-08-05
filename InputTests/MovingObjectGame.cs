using GameData.CharacterActions;
using GameData.Commands;
using GameData.UserInput;
using GameLibrary;
using GameLibrary.Animation;
using GameLibrary.Animation.Utilities;
using GameLibrary.AppObjects;
using GameLibrary.Extensions;
using GameLibrary.InputManagement;
using InputTests.MovingMan;
using Library.Animation;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace InputTests
{
    public class MovingObjectGame : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private SpriteFont arialFont;
        private InputsStateManager iManger = new InputsStateManager();

        private MovingHead headsIWin;
        private List<KeyCommand<IWalkingMan>> p1Commands;
        private MouseKeyboardInputsReciever inputProcessor;
        private MovingObjectAnimation _mo4;
        private CrossHairs _mouseHairs;

        public int CurrentSelectedObject { get; private set; }

        public MovingObjectGame()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 800;
            Content.RootDirectory = "Content";

            iManger = new InputsStateManager();
        }


        protected override void LoadContent()
        {
            this.spriteBatch = new SpriteBatch(GraphicsDevice);
            arialFont = this.Content.Load<SpriteFont>("NewArial");
            var walkingLeft = Texture2D.FromFile(GraphicsDevice, "./Content/WalkingLeft.png");
            var walkingRight = Texture2D.FromFile(GraphicsDevice, "./Content/WalkingRight.png");
            var standing = Texture2D.FromFile(GraphicsDevice, "./Content/Standing.png");
            var crossHairs = Texture2D.FromFile(GraphicsDevice, "./Content/CrossHairs_one.png");
            var wlFrames = FramesGenerator.GenerateFrames(new FrameInfo(72, 77), new Dimensions(walkingLeft.Width, walkingLeft.Height));
            var standingFrames = FramesGenerator.GenerateFrames(new FrameInfo(72, 77), new Dimensions(standing.Width, standing.Height));
            var wlAnimation = new OldBlockAnimationObject(wlFrames, new float[] { 0.200f, 0.200f, 0.200f, 0.200f }, true);
            var standingAnimation = new OldBlockAnimationObject(standingFrames, new float[] { 0.500f, 0.250f, 0.250f, 0.250f }, true);
            var walkingAnims = new WalkingManAnimations(new Dictionary<string, OldBlockAnimationObject>
            {
                { "MoveLeft", wlAnimation },
                { "MoveRight", wlAnimation },
                {"Standing",  standingAnimation }
            });

            var Red40x40 = this.spriteBatch.CreateFilledRectTexture(new Rectangle(0, 0, 40, 40), Color.Red);
            var Green40x40 = this.spriteBatch.CreateFilledRectTexture(new Rectangle(0, 0, 40, 40), Color.Green);
            var Blue40x40 = this.spriteBatch.CreateFilledRectTexture(new Rectangle(0, 0, 40, 40), Color.Black);

            var p1Controls = new PlayerKeyboardControls
            {
                Up = Keys.W,
                Down = Keys.S,
                Left = Keys.A,
                Right = Keys.D,
                Fire = Keys.LeftControl,
                SecondFire = Keys.Space
            };

            this.headsIWin  = new MovingHead(new Vector2(200,300), new Dimensions(80, 100));
            this.p1Commands = CommandBuilder.GetWalkingCommands(p1Controls);
            this.inputProcessor = new MouseKeyboardInputsReciever(this.iManger);

            _mo4 = new MovingObjectAnimation(this.spriteBatch, walkingLeft, walkingRight, standing, walkingAnims, headsIWin, new Vector2(200, 300));
            _mouseHairs = new CrossHairs(spriteBatch, crossHairs, Mouse.GetState().Position.ToVector2(), new Rectangle(0, 0, this.GraphicsDevice.Viewport.Width, this.GraphicsDevice.Viewport.Height - 200));
            this.CurrentSelectedObject = 0;
        }


        protected override void Update(GameTime gameTime)
        {
            var kState = Keyboard.GetState();
            var mState = Mouse.GetState();

            var delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
            iManger.Update(gameTime, kState, mState);
            // Escape hatch
            KeyboardFunctions.QuitOnKeys(this, iManger.PressedKeys(), Keys.Escape);
            _mouseHairs.SetPosition(mState.Position.ToVector2());
            
            var cmds = this.inputProcessor.Process(this.p1Commands);

            if (cmds != null)
                cmds.Execute(this._mo4);
            this.headsIWin.SetViewDestination(mState.Position.ToVector2());
            _mo4.Update(gameTime, delta);
            _mouseHairs.Update(gameTime, delta);
            
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            this.spriteBatch.Begin();
            _mo4.Draw(gameTime);
            _mouseHairs.Draw(gameTime);
            var mString  = this.arialFont.MeasureString($"Angle : {this.headsIWin.ViewingAngle}");
            this.spriteBatch.DrawString(this.arialFont, $"Angle : {this.headsIWin.ViewingAngle}", new Vector2(10, 10), Color.White);
            this.spriteBatch.End();
        }
    }
}
