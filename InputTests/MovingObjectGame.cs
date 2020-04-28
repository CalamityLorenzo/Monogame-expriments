using GameLibrary;
using GameLibrary.Animation;
using GameLibrary.AppObjects;
using GameLibrary.Extensions;
using inputTests;
using InputTests.KeyboardInput;
using InputTests.MovingMan;
using KeyboardInput;
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
        private SendKeyboardInput sendKeys;
        private MovableObject _mo;
        private MovableObject _mo1;
        private MovableObject _mo2;
        private MovableObject _mo3;
        private MovingObjectAnimation _mo4;

        internal List<MovableObject> MovableObjects { get; private set; }
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

            this.sendKeys = new SendKeyboardInput(p1Controls, iManger);

            _mo = new MovableObject(this.spriteBatch, Red40x40, new Vector2(30, 30));
            _mo1 = new MovableObject(this.spriteBatch, Green40x40, new Vector2(90, 30));
            _mo2 = new MovableObject(this.spriteBatch, Blue40x40, new Vector2(150, 30));
            _mo3 = new MovableObject(this.spriteBatch, walkingLeft, wlAnimation,  new Vector2(220, 30));
            _mo4 = new MovingObjectAnimation(this.spriteBatch, walkingLeft, walkingRight, standing, walkingAnims, new Vector2(290, 30));
            this.MovableObjects = new List<MovableObject> { _mo, _mo1, _mo2, _mo3 };
            this.CurrentSelectedObject = 3;
        }

        private IWalkingMan CheckSelected(InputManager manager)
        {
            if(manager.ReleasedMouseButtons().Contains(MouseButton.Left)){
                var next = this.CurrentSelectedObject += 1;
                if (next >= this.MovableObjects.Count+1)
                    next = 0;
                this.CurrentSelectedObject = next;
                if (CurrentSelectedObject == MovableObjects.Count)
                    return _mo4;

                return MovableObjects[next];
            }
            if (manager.ReleasedMouseButtons().Contains(MouseButton.Right))
            {
                var next = this.CurrentSelectedObject -= 1;
                if (next < 0)
                    next = this.MovableObjects.Count;
                this.CurrentSelectedObject = next;
                if (CurrentSelectedObject == MovableObjects.Count)
                    return _mo4;
                return MovableObjects[next];
            }
            if (CurrentSelectedObject == MovableObjects.Count)
                return _mo4;
            return MovableObjects[CurrentSelectedObject];
        }

        protected override void Update(GameTime gameTime)
        {
            var kState = Keyboard.GetState();
            var mState = Mouse.GetState();
            // Escape hatch
            KeyboardFunctions.QuitOnKeys(this, kState, Keys.Escape);
            //timr
            var delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
            //! Check all the keys and shit
            this.iManger.Update(gameTime, kState, mState );
            var current = CheckSelected(iManger);
            this.sendKeys.Update(gameTime, delta, current);
            this.MovableObjects.ForEach(i => i.Update(gameTime, delta));
            _mo4.Update(gameTime, delta);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            this.spriteBatch.Begin();
            this.MovableObjects.ForEach(i => i.Draw(gameTime));
            _mo4.Draw(gameTime); 
            this.spriteBatch.End();
        }
    }
}
