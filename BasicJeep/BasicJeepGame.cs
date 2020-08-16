using GameData;
using GameData.CharacterActions;
using GameData.Commands;
using GameData.UserInput;
using GameLibrary;
using GameLibrary.AppObjects;
using GameLibrary.Config.App;
using GameLibrary.InputManagement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;

namespace BasicJeep
{
    public class BasicJeepGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont arial;
        private Rotator rotator;
        private List<KeyCommand<Rotator>> rotatorCommands;
        private readonly MouseKeyboardInputsReciever _inputReciever;
        private readonly InputsStateManager _iStateManager;
        private readonly ConfigurationData configData;
        private MapVelocityManager basicVelocity;
        private List<KeyCommand<IBasicMotion>> velocityCmds;

        private AssetsLoader assets;

        internal JeepCharacter JeepChar { get; private set; }

        public BasicJeepGame(ConfigurationData configData)
        {
            _iStateManager = new InputsStateManager();
            _inputReciever = new MouseKeyboardInputsReciever(_iStateManager);
            this.configData = configData;
            Content.RootDirectory = "Content";

            graphics = new GraphicsDeviceManager(this);
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            arial = this.Content.Load<SpriteFont>("Arial");
            this.assets = new AssetsLoader(this.configData, this.GraphicsDevice);
        }

        protected override void Initialize()
        {
            base.Initialize();
            spriteBatch = new SpriteBatch(GraphicsDevice);
            var p1ControlList = configData.Get<Dictionary<string, string>>("Player1Controls");
            var p1Controls = MapConfigToControls.Map(p1ControlList);
           
            // jeep movement and direction are handleed by two classes.
            this.rotator = new Rotator(348, 202);
            this.rotatorCommands = CommandBuilder.GetRotatorCommands(p1Controls);
            this.basicVelocity = new MapVelocityManager(0f, 0f, 45f, 45f);
            this.velocityCmds = CommandBuilder.GetBasicMapMotion(p1Controls);
            
            var jeepAtlas = this.assets.JeepAtlas();
            var jeepFrames = this.assets.JeepAnimations();
            this.JeepChar = new JeepCharacter(this.spriteBatch, jeepAtlas, jeepFrames.ToList(), rotator, basicVelocity, new Vector2(80, 80));
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            var delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
            this._iStateManager.Update(gameTime, Keyboard.GetState(), Mouse.GetState());
            KeyboardFunctions.QuitOnKeys(this, this._iStateManager.GetInputState().PressedKeys, Keys.Escape);

            var rotateCmd = this._inputReciever.Process(this.rotatorCommands);
            var velocityCmd = this._inputReciever.Process(this.velocityCmds);
            if (rotateCmd != null)
                rotateCmd.Execute(rotator);
            if (velocityCmd != null)
                velocityCmd.Execute(basicVelocity);
            rotator.Update(delta);
            JeepChar.Update(delta);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            JeepChar.Draw(gameTime);
            spriteBatch.End();

        }


    }
}
