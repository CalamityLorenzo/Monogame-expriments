using GameLibrary;
using GameLibrary.Extensions;
using inputTests;
using InputTests.KeyboardInput;
using InputTests.MovingMan;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace InputTests
{
    public class MovingObjectGame : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private SpriteFont arialFont;
        private InputManager iManger;
        private SendKeyboardInput sendKeys;
        private MovableObject _mo;

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
            this.spriteBatch = new SpriteBatch(GraphicsDevice);
            arialFont = this.Content.Load<SpriteFont>("NewArial");
            var Red40x40 = this.spriteBatch.CreateFilleRectTexture(new Rectangle(0, 0, 40, 40), Color.Red);
            var p1Controls = new PlayerControlKeys
            {
                Up = Keys.W,
                Down = Keys.S,
                Left = Keys.A,
                Right = Keys.D,
                Fire = Keys.LeftControl,
                SecondFire = Keys.Space
            };
            this.sendKeys = new SendKeyboardInput(p1Controls, kManger);

            _mo = new MovableObject(this.spriteBatch, Red40x40, new Vector2(30, 30));
        }

        protected override void Update(GameTime gameTime)
        {
            var kState = Keyboard.GetState();
            var mState = Mouse.GetState();
            // Escape hatch
            KeyboardFunctions.QuitOnKeys(this, kState, Keys.Escape);
            //timr
            var delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
            //! GAME ON
            this.iManger.Update(gameTime, kState, mState );
            this.sendKeys.Update(gameTime, delta, _mo);
            this._mo.Update(gameTime, delta);
            
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            this.spriteBatch.Begin();
            _mo.Draw(gameTime);
            this.spriteBatch.End();
        }
    }
}
