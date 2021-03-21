using AnimationAgain.Character;
using AnimationAgain.Guns;
using GameData;
using GameData.CharacterActions;
using GameData.Commands;
using GameLibrary;
using GameLibrary.Animation;
using GameLibrary.AppObjects;
using GameLibrary.Config.App;
using GameLibrary.Extensions;
using GameLibrary.InputManagement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MovingManAnimation.Config;
using System.Collections.Generic;

namespace AnimationAgain
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private ExperimentalAssetLoader assetsLoader;
        private Dictionary<string, AnimationFramesCollection> playterAnimations;
        private ConfigurationData configData;
        private InputsStateManager inputManager;
        private AnimationPlayer playerAnimationPlayer;
        private AnimationPlayer headAnimationPlayer;

        private AnimationPlayer bulletAnimation;
        private BulletFactory bulletFactory;
        private Texture2D playerAtlas;

        private IVelocinator velos;
        private Vector2 gunPosOffSet;

        private BasicCharacterHead headChar;
        private BasicGun gun;
        private BasicCharacterWithCommands basicChar;

        private List<KeyCommand<IWalkingMan>> velocityCmds;
        private FindVector gunDirection;

        private MouseKeyboardInputsReciever inputReceiver;



        private float animSpeed = 0.5f;
        private int currentBullet = 0;

        public bool FIRE { get; private set; }
        public bool INCREMENTBULLET { get; private set; }

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

            // Controls
            var p1Controls = assetsLoader.Player1KeyboardControls();
            //Image
            this.playerAtlas = assetsLoader.PlayerAtlas();
            
            //animations
            this.playterAnimations = this.assetsLoader.Animations();//.ToDictionary(a => a.Key, a => a.Value.Frames);
            // Movement sppeds
            this.velocityCmds = CommandBuilder.GetBasicMapMotion(p1Controls);
            velos = new BasicVelocityManager(0f, 0f);

            // Character Animations
            this.playerAnimationPlayer = new AnimationPlayer(.330f);
            this.playerAnimationPlayer.SetFrames(playterAnimations["Right"]);
            this.playerAnimationPlayer.SetSpeed(this.animSpeed);
            
            this.headAnimationPlayer = new AnimationPlayer(.330f);
            this.headAnimationPlayer.SetFrames(playterAnimations["BobLeft"]);
            this.headAnimationPlayer.SetSpeed(this.animSpeed);

            // bullet animations for factory
            this.bulletAnimation = new AnimationPlayer(.500f, this.assetsLoader.BulletAnimations());

            this.bulletFactory = new BulletFactory(this._spriteBatch, assetsLoader.BulletAtlases(), bulletAnimation);

            this.gunPosOffSet = new Vector2(1, 37);
            gunDirection = new FindVector(new Point(100, 200), this.inputManager);

            basicChar = new BasicCharacterWithCommands(this._spriteBatch, playterAnimations, this.playerAnimationPlayer, velos, playerAtlas, new Vector2(100, 200), 57, 57);
            Vector2 headOffsetPosition = basicChar.CurrentPosition.Subtract(gunPosOffSet);
            this.headChar= new BasicCharacterHead(this._spriteBatch, playterAnimations, this.headAnimationPlayer, gunDirection, velos, playerAtlas, headOffsetPosition);
            this.gun = new BasicGun(bulletFactory, new Vector2(headChar.CurrentRectangle.Width / 2 + headChar.CurrentPosition.X, headChar.CurrentPosition.Y));
            base.Initialize();
        }


        void SetPlayingFrame(HashSet<Keys> pressed)
        {
            if (pressed.Contains(Keys.Up)) { this.animSpeed += 0.05f; this.playerAnimationPlayer.SetSpeed(this.animSpeed); }
            if (pressed.Contains(Keys.Down))
            {
                this.animSpeed -= 0.05f; this.playerAnimationPlayer.SetSpeed(this.animSpeed);
            }

            var mState = Mouse.GetState();
            if (mState.LeftButton == ButtonState.Pressed)
            {
                if (this.FIRE)
                {
                    this.gun.Fire(mState.Position.ToVector2());
                }
                this.FIRE = false;
            }
            if (mState.LeftButton == ButtonState.Released)
                this.FIRE = true;

            if (mState.RightButton == ButtonState.Pressed)
            {
                if (this.INCREMENTBULLET)
                {
                    this.currentBullet += 1;
                    if (currentBullet > 2) { this.currentBullet = 0; }
                    this.gun.SetBullet(currentBullet);
                }

                this.INCREMENTBULLET = false;
            }

            if (mState.RightButton == ButtonState.Released)
                this.INCREMENTBULLET = true;
        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardFunctions.QuitOnKeys(this, inputManager.PressedKeys(), Keys.Escape);
            var kState = Keyboard.GetState();
            var mState = Mouse.GetState();
            var delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
            var totalTime = (float)gameTime.TotalGameTime.TotalMilliseconds;

            this.inputManager.Update(gameTime, Keyboard.GetState(), Mouse.GetState());

            //gunDirection.SetTerminal(inputManager.MousePosition.ToVector2());
            gunDirection.Update(delta);
            
            var activeCommand = this.inputReceiver.MapKeyboardCommands(this.velocityCmds);
            activeCommand.ForEach(a => a.Execute(basicChar));
            this.basicChar.Update(delta);

            activeCommand.ForEach(a => a.Execute(headChar));
            this.headChar.Update(delta);

            this.gun.SetPosition(new Vector2(headChar.CurrentRectangle.Width / 2 + headChar.CurrentPosition.X, headChar.CurrentPosition.Y));
            gun.Update(delta);
            SetPlayingFrame(inputManager.KeysDown());
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            var frame = playerAnimationPlayer.CurrentFrame();
            // TODO: Add your drawing code here
            this._spriteBatch.Begin();
            //      this._spriteBatch.Draw(this.playerAtlas, new Vector2(100, 200), frame, Color.White);
            this.basicChar.Draw(gameTime);
            this.headChar.Draw(gameTime);
            this.gun.Draw(gameTime);
            base.Draw(gameTime);
            this._spriteBatch.End();

        }
    }
}
