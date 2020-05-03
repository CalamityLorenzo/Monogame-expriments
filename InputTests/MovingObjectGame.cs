using GameLibrary;
using GameLibrary.Animation;
using GameLibrary.AppObjects;
using GameLibrary.Extensions;
using GameLibrary.Models;
using inputTests;
using InputTests.KeyboardInput;
using InputTests.MovingMan;
using Library.Animation;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct2D1;
using System.Collections.Generic;

namespace InputTests
{
    public class MovingObjectGame : Game
    {
        private GraphicsDeviceManager graphics;
        private Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch;
        private SpriteFont arialFont;
        private InputManager iManger;
        private MovingHead headsIWin;
        private IsDownIsUp sendKeys;
        private MovingObjectAnimation _mo4;
        private CrossHairs _mouseHairs;

        public int CurrentSelectedObject { get; private set; }

        public MovingObjectGame()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 800;
            Content.RootDirectory = "Content";

            iManger = new InputManager();
        }


        protected override void LoadContent()
        {
            this.spriteBatch = new Microsoft.Xna.Framework.Graphics.SpriteBatch(GraphicsDevice);
            arialFont = this.Content.Load<SpriteFont>("NewArial");
            var walkingLeft = Texture2D.FromFile(GraphicsDevice, "./Content/WalkingLeft.png");
            var walkingRight = Texture2D.FromFile(GraphicsDevice, "./Content/WalkingRight.png");
            var standing = Texture2D.FromFile(GraphicsDevice, "./Content/Standing.png");
            var crossHairs = Texture2D.FromFile(GraphicsDevice, "./Content/CrossHairs_one.png");
            var wlFrames = FramesGenerator.GenerateFrames(new FrameInfo(72, 77), new Dimensions(walkingLeft.Width, walkingLeft.Height));
            var standingFrames = FramesGenerator.GenerateFrames(new FrameInfo(72, 77), new Dimensions(standing.Width, standing.Height));
            var wlAnimation = new BlockAnimationObject(wlFrames, new float[] { 0.200f, 0.200f, 0.200f, 0.200f }, true);
            var standingAnimation = new BlockAnimationObject(standingFrames, new float[] { 0.500f, 0.250f, 0.250f, 0.250f }, true);
            var walkingAnims = new WalkingManAnimations(new Dictionary<string, BlockAnimationObject>
            {
                { "MoveLeft", wlAnimation },
                { "MoveRight", wlAnimation },
                {"Standing",  standingAnimation }
            });

            var Red40x40 = this.spriteBatch.CreateFilleRectTexture(new Rectangle(0, 0, 40, 40), Color.Red);
            var Green40x40 = this.spriteBatch.CreateFilleRectTexture(new Rectangle(0, 0, 40, 40), Color.Green);
            var Blue40x40 = this.spriteBatch.CreateFilleRectTexture(new Rectangle(0, 0, 40, 40), Color.Black);

            var p1Controls = new PlayerControlKeys
            {
                Up = Keys.W,
                Down = Keys.S,
                Left = Keys.A,
                Right = Keys.D,
                Fire = Keys.LeftControl,
                SecondFire = Keys.Space
            };

            this.headsIWin  = new MovingHead(new Vector2(200,300), new Dimensions(80, 100));

            this.sendKeys = new IsDownIsUp(p1Controls, iManger);

            _mo4 = new MovingObjectAnimation(this.spriteBatch, walkingLeft, walkingRight, standing, walkingAnims, new Vector2(200, 300));
            _mouseHairs = new CrossHairs(spriteBatch, crossHairs, Mouse.GetState().Position.ToVector2(), new Rectangle(0, 0, this.GraphicsDevice.Viewport.Width, this.GraphicsDevice.Viewport.Height - 200));
            this.CurrentSelectedObject = 0;
        }


        protected override void Update(GameTime gameTime)
        {
            var kState = Keyboard.GetState();
            var mState = Mouse.GetState();

            // Escape hatch
            KeyboardFunctions.QuitOnKeys(this, kState, Keys.Escape);
            _mouseHairs.SetPosition(mState.Position.ToVector2());
            //timr
            var delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
            //! Check all the keys and shit
            this.iManger.Update(gameTime, kState, mState );
            this.sendKeys.Update(gameTime, delta, _mo4);
            
            _mo4.Update(gameTime, delta);
            _mouseHairs.Update(gameTime, delta);
            this.headsIWin.SetViewDestination(mState.Position.ToVector2());
            this.headsIWin.Update(gameTime, delta);
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
