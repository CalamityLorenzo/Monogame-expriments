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
    public class CommandPatternGame : Game
    {
        private GraphicsDeviceManager graphics;
        private Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch;
        private SpriteFont arialFont;
        private InputsManager inputsManager;
        private InputHandler inputHandler;
        private MovingHead headsIWin;

        private MovingObjectAnimation _mo4;
        private CrossHairs _mouseHairs;

        public CommandPatternGame()
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
            var walkingLeft = Texture2D.FromFile(GraphicsDevice, "./Content/WalkingLeft.png");
            var walkingRight = Texture2D.FromFile(GraphicsDevice, "./Content/WalkingRight.png");
            var standing = Texture2D.FromFile(GraphicsDevice, "./Content/Standing.png");
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

            var crossHairs = Texture2D.FromFile(GraphicsDevice, "./Content/CrossHairs_one.png");
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

            this.headsIWin = new MovingHead(new Vector2(200, 300), new Dimensions(80, 100));
            _mo4 = new MovingObjectAnimation(this.spriteBatch, walkingLeft, walkingRight, standing, walkingAnims, headsIWin, new Vector2(200, 300));
            _mouseHairs = new CrossHairs(spriteBatch, crossHairs, Mouse.GetState().Position.ToVector2(), new Rectangle(0, 0, this.GraphicsDevice.Viewport.Width, this.GraphicsDevice.Viewport.Height - 200));
        }

        protected override void Update(GameTime gameTime)
        {
            var kState = Keyboard.GetState();
            var mState = Mouse.GetState();
            this.inputsManager.Update(gameTime, kState, mState);
            

            // Escape hatch
            KeyboardFunctions.QuitOnKeys(this, inputsManager.PressedKeys(), Keys.Escape);
            _mouseHairs.SetPosition(mState.Position.ToVector2());
            //time
            var delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
            //! Check all the keys and shit
            var command = this.inputHandler.Update(gameTime);

            command.Execute(_mo4);

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
