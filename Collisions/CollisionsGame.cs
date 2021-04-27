using Collisions.Objects;
using Collisions.Objects.Balls;
using Collisions.Objects.Guns;
using CollisionsGame.Objects;
using GameData;
using GameData.CharacterActions;
using GameData.Commands;
using GameData.UserInput;
using GameLibrary.Animation;
using GameLibrary.AppObjects;
using GameLibrary.Backgrounds;
using GameLibrary.Config.App;
using GameLibrary.Extensions;
using GameLibrary.InputManagement;
using GameLibrary.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;


namespace CollisionsGame
{
    partial class CollisionsGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private readonly InputsStateManager inputManager;
        private readonly MouseKeyboardInputsReciever inputReceiver;

        private SpriteFont arialFont;

        private World TheState;
        private CollisionMap collisionMap;
        private BoundaryCollisionsMap boundaryMap;
        private BatContainer batContainer;
        private BallContainer ballContainer;
        private Rotator debugGunRotator;

        public ConfigurationData ConfigurationData { get; }
        public Texture2D RedSquare { get; private set; }
        public Texture2D GreenSquare { get; private set; }
        public BlockMap GameMapBlocks { get; private set; }

        private AimedGun debugGun;

        private List<KeyCommand<ICharacterActions>> movementCmds;

        public CollisionsGame(ConfigurationData configurationData)
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            this.inputManager = new InputsStateManager();
            this.inputReceiver = new MouseKeyboardInputsReciever(inputManager);
            ConfigurationData = configurationData;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();

            var screenData = this.ConfigurationData.Get<ScreenData>("ScreenOptions");
            this.setScreenData(screenData);
            var map = GeneralExtensions.LoadCsvMapData("Maps/BorderMap.csv");
            var p1ControlList = ConfigurationData.Get<Dictionary<string, string>>("Player1Controls");
            var p1Controls = MapConfigToControls.Map(p1ControlList);

            this.movementCmds = CommandBuilder.GetWalkingCommands(p1Controls);

            TheState = new World()
            {
                ViewPort = _graphics.GraphicsDevice.Viewport,
                ScreenResolution = new Vector2(this.GraphicsDevice.Adapter.CurrentDisplayMode.Width, this.GraphicsDevice.Adapter.CurrentDisplayMode.Width),
            };

            var bulletAnim = new AnimationPlayer(.200f, ConfigurationData.Get<IEnumerable<AnimationFramesCollection>>("Bullets:Frames").ToDictionary(a => a.Name, a => a));
            var bulletAtlas = Texture2D.FromFile(this.GraphicsDevice, ConfigurationData.Get("BulletAtlas"));
            var factory = new BulletFactory(_spriteBatch, new[] { bulletAtlas }, bulletAnim);

            var tileDimensions = ConfigurationData.Get<Dimensions>("TileDimensions");
            var mapRowsAndCols = ConfigurationData.Get<Dimensions>("MapDimensions");
            this.RedSquare = _spriteBatch.CreateFilledRectTexture(new Rectangle(0, 0, tileDimensions.Width, tileDimensions.Height), Color.DarkRed);
            this.GreenSquare= _spriteBatch.CreateFilledRectTexture(new Rectangle(0, 0, tileDimensions.Width, tileDimensions.Height), Color.DarkGreen);
            this.collisionMap = new CollisionMap(map, tileDimensions, new Vector2(0, 0), TheState.ViewPort, mapRowsAndCols);
            this.boundaryMap = new BoundaryCollisionsMap(map, tileDimensions, new Vector2(0, 0), TheState.ViewPort, mapRowsAndCols);
            var batAtlas = Texture2D.FromFile(this._graphics.GraphicsDevice, ConfigurationData.Get("BatAtlas"));
            var batAnimation = new AnimationPlayer(.200f, ConfigurationData.Get<IEnumerable<AnimationFramesCollection>>("BatFrames").ToDictionary(a => a.Name, a => a));
            var bar = new Sprite(_spriteBatch, batAtlas, batAnimation, new Point(500, 500));
            //var bulletAnims = new AnimationPlayer(.200f)
            var theGun = new LazerGun(factory, new Point(230, 245), batAnimation.CurrentFrame(), 500f);
            var batStartPos = new Vector2(100, 675).ToPoint();


            this.batContainer = new BatContainer(bar, batStartPos, theGun, new BasicVelocityManager(0f, 0f), new Vector2(380, 380));

            //this.CreateGamesBlock(TheState.ViewPort);
            this.GameMapBlocks = new BlockMap(_spriteBatch, new Point(75, 120), new Dimensions(55, 30));

            var ballAtlas = Texture2D.FromFile(this.GraphicsDevice, ConfigurationData.Get("BallAtlas"));
            var ballAnim = new AnimationPlayer(.200f, ConfigurationData.Get<IEnumerable<AnimationFramesCollection>>("Ball:Frames").ToDictionary(a => a.Name, a => a));
            var ballFactory = new BallFactory(this._spriteBatch, new Dictionary<string, Texture2D> { { "BaseBall", ballAtlas } }, ballAnim);

            var ball = ballFactory.NewBall("BaseBall", new Vector2(0, 1), 300f, batStartPos.AddY(-100));

            this.ballContainer = new BallContainer(new Point(16, 23), new[] { ball }, ballFactory);
            this.debugGunRotator = new Rotator(0, 190f);
            this.debugGun = new AimedGun(factory, batStartPos, _spriteBatch, debugGunRotator, batContainer, ballContainer);
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
            collisionMap.Update(deltaTime, TheState);
            this.boundaryMap.Update(deltaTime, TheState);
            TheState = new World()
            {
                ViewPort = TheState.ViewPort,
                ScreenResolution = TheState.ScreenResolution,
                InputState = inputManager.GetInputState(),
                Map = boundaryMap.ViewPortCollisions.ToList()
            };

            var previousBatPos = batContainer.CurrentPosition;
            // Using the state of the keyboard and the mouse, find out which cmds have been triggered.
            var activeCommand = this.inputReceiver.MapKeyboardCommands(this.movementCmds);
            activeCommand.ForEach(a => a.Execute(batContainer));
            activeCommand.ForEach(a => a.Execute(debugGun));

            this.batContainer.Update(deltaTime, TheState);
            debugGun.Update(deltaTime, TheState);
            this.ballContainer.Update(deltaTime, TheState);
            this.ballContainer.AgentCollisions(this.batContainer);
            
            // Has the player object struck an game objet (Not an environment object)
            // var objectsStruck = GameObjectCollisions(this.gamesBlocks, batContainer);
            this.GameMapBlocks.AgentCollisions(this.batContainer);
            var shot = this.GameMapBlocks.InteractiveObjectCollisions(this.batContainer.FiredBullets);
        //    this.GameMapBlocks.ObjectCollisions(this.batContainer.FiredBullets);
            var struck = this.GameMapBlocks.InteractiveObjectCollisions(this.ballContainer.Balls);

            this.Shot(shot);
            this.Bounced(struck);

            this.GameMapBlocks.Update(deltaTime, TheState);

            base.Update(gameTime);
        }

        private void Shot(IEnumerable<(GameBlock map, BaseBullet bullet)> booms)
        {
            foreach(var itm in booms)
            {
                itm.map.Hit(5);
                itm.bullet.Struck();
            }
        }

        private void Bounced(IEnumerable<(GameBlock map, BaseBall ball)> boing)
        {
            foreach (var itm in boing)
            {
                itm.map.Hit(10);
                itm.ball.Bounce(itm.map.Area);
            }

        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            this._spriteBatch.Begin();
            // TODO: Add your drawing code here
            debugGun.Draw(gameTime);
            this.batContainer.Draw(gameTime);
            this.ballContainer.Draw(gameTime);
            this.GameMapBlocks.Draw(gameTime);
            var mString = this.arialFont.MeasureString($"Angle : {GeneralExtensions.UnitVectorFromDegrees(this.debugGunRotator.CurrentAngle)}");
            var (vectX, vectY) = GeneralExtensions.UnitVectorFromDegrees(this.debugGunRotator.CurrentAngle);
            this._spriteBatch.DrawString(this.arialFont, $"Angle : {{X: {vectX.ToString("N3")} Y: {vectY.ToString("N3")}}}", new Vector2(10, 10), Color.White);

            this.DrawCollisionMap();
            this._spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
