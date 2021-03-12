using AnimationAgain.Animation;
using AnimationAgain.Character;
using GameData;
using GameData.CharacterActions;
using GameData.Commands;
using GameLibrary;
using GameLibrary.AppObjects;
using GameLibrary.Config.App;
using GameLibrary.InputManagement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MovingManAnimation.Config;
using SharpDX.MediaFoundation;
using System.Collections.Generic;
using System.Linq;

namespace AnimationAgain
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private ExperimentalAssetLoader assetsLoader;
        private Dictionary<string, Rectangle[]> anims;
        private ConfigurationData configData;
        private InputsStateManager inputManager;
        private AnimationPlayer animationPlayer;
        private Texture2D playerAtlas;

        private IVelocinator velos;
        private BasicCharacterWithCommands basicChar;

        private List<KeyCommand<IBasicMotion>> velocityCmds;


        private MouseKeyboardInputsReciever inputReceiver;

        private float animSpeed = 0.5f;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            this.inputManager = new InputsStateManager();
            this.inputReceiver = new MouseKeyboardInputsReciever(inputManager);
        }

        public Game1(ConfigurationData configData) : this()
        {
            this.configData = configData;
        }


        protected override void LoadContent()
        {

            // TODO: use this.Content to load your game content here

            
        }

        protected override void Initialize()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            // TODO: Add your initialization logic here
            this.assetsLoader = new ExperimentalAssetLoader(this.configData, this._graphics.GraphicsDevice);


            var p1Controls = assetsLoader.Player1KeyboardControls();
            this.playerAtlas = assetsLoader.PlayerAtlas();
            this.anims = this.assetsLoader.Animations();

            this.velocityCmds = CommandBuilder.GetBasicMapMotion(p1Controls);

            velos = new BasicVelocityManager(0f, 0f);

            this.animationPlayer = new AnimationPlayer(.200f);
            this.animationPlayer.SetFrames(anims.FirstOrDefault(a => a.Key == "Right"));
            this.animationPlayer.SetSpeed(this.animSpeed);

            basicChar = new BasicCharacterWithCommands(this._spriteBatch, anims, this.animationPlayer, velos, playerAtlas, new Vector2(100, 200), 32, 32);
            base.Initialize();
        }


        void SetPlayingFrame(HashSet<Keys> pressed)
        {

            if (pressed.Contains(Keys.Up)) { this.animSpeed += 0.05f; this.animationPlayer.SetSpeed(this.animSpeed); }
            if (pressed.Contains(Keys.Down))
            {
                this.animSpeed -= 0.05f; this.animationPlayer.SetSpeed(this.animSpeed);
            }

        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardFunctions.QuitOnKeys(this, inputManager.PressedKeys(), Keys.Escape);
            var kState = Keyboard.GetState();
            var mState = Mouse.GetState();
            var delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
            var totalTime = (float)gameTime.TotalGameTime.TotalMilliseconds;

            this.inputManager.Update(gameTime, Keyboard.GetState(), Mouse.GetState());

            var activeCommand = this.inputReceiver.MapCommands(this.velocityCmds);
            if(activeCommand!=null)
            activeCommand.Execute(basicChar);

            basicChar.Update(delta);
            SetPlayingFrame(inputManager.KeysDown());
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            var frame = animationPlayer.CurrentFrame();
            // TODO: Add your drawing code here
            this._spriteBatch.Begin();
            //      this._spriteBatch.Draw(this.playerAtlas, new Vector2(100, 200), frame, Color.White);
            this.basicChar.Draw(gameTime);
            base.Draw(gameTime);
            this._spriteBatch.End();

        }
    }
}
