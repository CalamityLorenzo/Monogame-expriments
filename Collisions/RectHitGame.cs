using Collisions.Objects;
using GameData.CharacterActions;
using GameData.Commands;
using GameData.UserInput;
using GameLibrary.AppObjects;
using GameLibrary.Config.App;
using GameLibrary.Extensions;
using GameLibrary.InputManagement;
using GameLibrary.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Collisions
{
    internal class RectHitGame : Game
    {
        private readonly ConfigurationData configurationData;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private readonly InputsStateManager inputManager;
        private readonly MouseKeyboardInputsReciever inputReceiver;

        private SpriteFont arialFont;

        private Rectangle MajorRect;

        public MinorRectPlayersObject MinorRecwt { get; private set; }

        private Rectangle CenterRect;
        private List<KeyCommand<ICharacterActions>> movementCmds;
        private Texture2D smallTexture;
        private Texture2D smallTexture2;
        private Color ColorHit = Color.Red;
        private bool hasCollision = false;
        public RectHitGame(ConfigurationData configurationData)
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            this.inputManager = new InputsStateManager();
            this.inputReceiver = new MouseKeyboardInputsReciever(inputManager);
            this.configurationData = configurationData;
        }

        private void setScreenData(ScreenData data)
        {
            this._graphics.PreferredBackBufferWidth = data.ScreenWidth;
            this._graphics.PreferredBackBufferHeight = data.ScreenHeight;
            this._graphics.IsFullScreen = data.FullScreen;
            this._graphics.ApplyChanges();
        }

        protected override void Initialize()
        {
            base.Initialize();

            var screenData = this.configurationData.Get<ScreenData>("ScreenOptions");
            this.setScreenData(screenData);
            var p1ControlList = configurationData.Get<Dictionary<string, string>>("Player1Controls");
            var p1Controls = MapConfigToControls.Map(p1ControlList);
            this.movementCmds = CommandBuilder.GetWalkingCommands(p1Controls);

            this.smallTexture = _spriteBatch.CreateFilledRectTexture(new Dimensions(1, 1), Color.White);
            this.smallTexture2 = _spriteBatch.CreateFilledRectTexture(new Dimensions(1, 1), Color.White);
            this.MajorRect = new Rectangle(new Point(_graphics.GraphicsDevice.Viewport.Width / 2 - 50, _graphics.GraphicsDevice.Viewport.Height / 2 - 50), new Point(150, 75));


            var velocity = new Vector2(150f, 150f);
            var position = new Point(210, 180);
            this.MinorRecwt = new MinorRectPlayersObject(this._spriteBatch, this.smallTexture2, new Rectangle(position, new Point(40, 40)), velocity);
            this.CenterRect = new Rectangle(new Point((this.MajorRect.X + MajorRect.Width / 2) - 2, (this.MajorRect.Y + MajorRect.Height / 2) - 2), new Point(3, 3));

        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            arialFont = this.Content.Load<SpriteFont>("NewArial");
            // TODO: use this.Content to load your game content here

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            // The state of the keyboard and mouse.
            this.inputManager.Update(gameTime, Keyboard.GetState(), Mouse.GetState());
            // TODO: Add your update logic here
            var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            var activeCommand = this.inputReceiver.MapKeyboardCommands(this.movementCmds);
            activeCommand.ForEach(a => a.Execute(MinorRecwt));
            this.MinorRecwt.Update(deltaTime);


            var ballRect = this.MinorRecwt.Area;
            var ballVel = this.MinorRecwt.Velocity;
            var bigRect = this.MajorRect;
            var direction = this.MinorRecwt.GetDirection();

                CollisionWithVelocity(ballRect, bigRect, ballVel, direction);
            //if (GameLibrary.AppObjects.Collisions.AABBStruck(this.MinorRecwt.Area, MajorRect))
            //{
            //    this.hasCollision = true;
            //}
            //else
            //{
            //    this.hasCollision = false;
            //}

            //if (hasCollision)
            //{
            //}

            BoundaryBash(ballRect, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight, direction);


            this.ColorHit = hasCollision ? Color.Red : Color.White;
        }

        private void BoundaryBash(Rectangle ballRect, int preferredBackBufferWidth, int preferredBackBufferHeight, Vector2 direction)
        {
            if (ballRect.X < 0 || ballRect.X + ballRect.Width > _graphics.PreferredBackBufferWidth)
            {
                this.MinorRecwt.SetDirection(new Vector2(direction.X *= -1, direction.Y));
            }

            if (ballRect.Y < 0 || ballRect.Y + ballRect.Height > _graphics.PreferredBackBufferHeight)
            {

                this.MinorRecwt.SetDirection(new Vector2(direction.X, direction.Y *= -1));
            }
        }

        private void CollisionWithVelocity(Rectangle ballRect, Rectangle bigRect, Vector2 ballVel, Vector2 direction)
        {
            if (ballRect.X + ballRect.Width + ballVel.X > bigRect.X &&
            ballRect.X + ballVel.X < bigRect.X + bigRect.Width &&
            ballRect.Y + ballRect.Height > bigRect.Y &&
            ballRect.Y < bigRect.Y + bigRect.Height)
            {
                this.MinorRecwt.SetDirection(new Vector2(direction.X *= -1, direction.Y)); ;
            }
            


            if (ballRect.X + ballRect.Width > bigRect.X &&
                  ballRect.X < bigRect.X + bigRect.Width &&
                  ballRect.Y + ballRect.Height + MinorRecwt.Velocity.Y > bigRect.Y &&
                  ballRect.Y + this.MinorRecwt.Velocity.Y < bigRect.Y + bigRect.Height)
            {
                this.MinorRecwt.SetDirection(new Vector2(direction.X, direction.Y *= -1));
            }
            

        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            _spriteBatch.Draw(smallTexture, this.MajorRect, this.ColorHit);
            _spriteBatch.Draw(smallTexture, this.CenterRect, Color.Black);
            MinorRecwt.Draw(gameTime);
            _spriteBatch.End();
        }

    }
}
