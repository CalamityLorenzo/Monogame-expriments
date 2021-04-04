using Character.Container.Character;
using GameData;
using GameData.CharacterActions;
using GameData.Commands;
using GameLibrary;
using GameLibrary.Animation;
using GameLibrary.AppObjects;
using GameLibrary.Config.App;
using GameLibrary.InputManagement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MovingManAnimation.Config;
using System.Collections.Generic;

namespace Character.Container
{
    public class CharacterContainerGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private ContainerAssetsLoader assetsLoader;
        private ConfigurationData configData;
        private InputsStateManager inputManager;
        private MouseKeyboardInputsReciever inputReceiver;
        private List<KeyCommand<ICharacterActions>> movementCmds;

        public BasicVelocityManager VelocityManager { get; private set; }

        public World TheState { get; set; } = new World();
        internal ManContainer ManContainer { get; private set; }

        public CharacterContainerGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            this.inputManager = new InputsStateManager();
            this.inputReceiver = new MouseKeyboardInputsReciever(inputManager);
        }

        public CharacterContainerGame(ConfigurationData configData) : this()
        {
            this.configData = configData;
        }
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            this.assetsLoader = new ContainerAssetsLoader(this.configData, this._graphics.GraphicsDevice);

            this.TheState = new World()
            {
                ViewPort = _graphics.GraphicsDevice.Viewport,
                ScreenResolution = new Vector2(this.GraphicsDevice.Adapter.CurrentDisplayMode.Width, this.GraphicsDevice.Adapter.CurrentDisplayMode.Width)
            };

            // Controls
            var p1Controls = assetsLoader.Player1Controls();
            //Image
            var bodyAtlas = assetsLoader.PlayerAtlas();
            this.movementCmds = CommandBuilder.GetWalkingCommands(p1Controls);

            this.VelocityManager = new BasicVelocityManager(0f, 0f);

            var playerAnimations = this.assetsLoader.Animations();//.ToDictionary(a => a.Key, a => a.Value.Frames);
            // Character Animations
            var bodyAnimationPlayer = new AnimationPlayer(.330f, playerAnimations);
            var headAnimationsPlayer = new AnimationPlayer(.330f, playerAnimations);
            bodyAnimationPlayer.SetFrames("Right");
            headAnimationsPlayer.SetFrames("BobLeft");
            // TODO: use this.Content to load your game content here


            // bullet animations for factory
            var bulletAnimation = new AnimationPlayer(.500f, this.assetsLoader.BulletAnimations());

            var bulletFactory = new BulletFactory(this._spriteBatch, assetsLoader.BulletAtlases(), bulletAnimation);

            var Body = new Sprite(this._spriteBatch, bodyAtlas, bodyAnimationPlayer, new Point(100, 100));
            var Head = new Sprite(this._spriteBatch, bodyAtlas, headAnimationsPlayer, new Point(100, 100));
            var baseGun = new BaseGun(bulletFactory, new Vector2(40, 50));
            this.ManContainer = new ManContainer(new Point(40, 50), VelocityManager, new Vector2(87, 87), baseGun, Body, Head);
            base.Initialize();
        }

        protected override void LoadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardFunctions.QuitOnKeys(this, inputManager.PressedKeys(), Keys.Escape);
            var kState = Keyboard.GetState();
            var mState = Mouse.GetState();
            var delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
            var totalTime = (float)gameTime.TotalGameTime.TotalMilliseconds;

            this.inputManager.Update(gameTime, Keyboard.GetState(), Mouse.GetState());

            this.TheState = new World
            {
                InputState = inputManager.GetInputState(),
                ScreenResolution = TheState.ScreenResolution,
                ViewPort = TheState.ViewPort,
                Map = TheState.Map
            };

            var activeCommand = this.inputReceiver.MapKeyboardCommands(this.movementCmds);
            activeCommand.ForEach(a => a.Execute(ManContainer));
            this.ManContainer.Update(delta, TheState);
            // TODO: Add your update logic here
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            // TODO: Add your drawing code here
            this.ManContainer.Draw(gameTime);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
