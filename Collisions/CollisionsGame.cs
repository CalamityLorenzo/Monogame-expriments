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
using System.Diagnostics;
using System.Linq;

namespace CollisionsGame
{
    partial class CollisionsGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private readonly InputsStateManager inputManager;
        private readonly MouseKeyboardInputsReciever inputReceiver;

        private World TheState;
        private CollisionMap collisionMap;
        private BatContainer batContainer;

        public ConfigurationData ConfigurationData { get; }
        public Texture2D RedSquare { get; private set; }

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
            var p1Controls = MapConfigToControls.MapKeyboard(p1ControlList);

            this.movementCmds = CommandBuilder.GetWalkingCommands(p1Controls);

            TheState = new World()
            {
                ViewPort = _graphics.GraphicsDevice.Viewport,
                ScreenResolution = new Vector2(this.GraphicsDevice.Adapter.CurrentDisplayMode.Width, this.GraphicsDevice.Adapter.CurrentDisplayMode.Width),
            };

            var tileDimensions = ConfigurationData.Get<Dimensions>("TileDimensions");
            var mapRowsAndCols = ConfigurationData.Get<Dimensions>("MapDimensions");
            this.RedSquare = _spriteBatch.CreateFilledRectTexture(new Rectangle(0, 0, tileDimensions.Width, tileDimensions.Height), Color.DarkRed);
            this.collisionMap = new CollisionMap(map, tileDimensions, new Vector2(0, 0), TheState.ViewPort, mapRowsAndCols);
            var batAtlas = Texture2D.FromFile(this._graphics.GraphicsDevice, ConfigurationData.Get("BatAtlas"));
            var batAnimation = new AnimationPlayer(.200f, ConfigurationData.Get<IEnumerable<AnimationFramesCollection>>("BatFrames").ToDictionary(a => a.Name, a => a));
            var bar = new Sprite(_spriteBatch, batAtlas, batAnimation, new Vector2(500, 500));
            this.batContainer = new BatContainer(bar, new Vector2(100, 675).ToPoint(), new BasicVelocityManager(0f, 0f), new Vector2(230, 245)); ;

            this.CreateGamesBlock(TheState.ViewPort);

        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            this.inputManager.Update(gameTime, Keyboard.GetState(), Mouse.GetState());
            // TODO: Add your update logic here
            var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            collisionMap.Update(deltaTime, TheState);

            TheState = new World()
            {
                ViewPort = TheState.ViewPort,
                ScreenResolution = TheState.ScreenResolution,
                InputState = inputManager.GetInputState(),
                Map = collisionMap.ViewPortCollisions.ToList()
            };
            // ALl those lovely colourful blocks
            foreach(var block in gamesBlocks)
            {
                block.Update(deltaTime);
            }

            var previousBatPos = batContainer.CurrentPosition;
            var activeCommand = this.inputReceiver.MapKeyboardCommands(this.movementCmds);
            activeCommand.ForEach(a => a.Execute(batContainer));
            this.batContainer.Update(deltaTime, TheState);

            // Has the player object struck an game objet (Not an environment object)
            var objectsStruck = GameObjectCollisions(this.gamesBlocks, batContainer);
            foreach (var struck in objectsStruck)
            {
                if (struck.ReactionType == "Remove")
                    this.gamesBlocks.Remove(struck);
                else
                    this.batContainer.SetCurrentPosition(previousBatPos);

            }

            base.Update(gameTime);
        }



        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            this._spriteBatch.Begin();
            // TODO: Add your drawing code here
            this.DrawCollisionMap();
            this.DrawGamesBlocks(gameTime);
            this.batContainer.Draw(gameTime);

            this._spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
