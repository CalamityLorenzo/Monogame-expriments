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
using MovingManAnimation.Character;
using MovingManAnimation.Config;
using System.Collections.Generic;

namespace MovingManAnimation
{
    internal class MovingManGame : Game
    {
        private GraphicsDeviceManager graphics;
        private InputsStateManager iManger;
        private SpriteBatch spriteBatch;
        private MovingManAssetsLoader assetsLoader;
        private IVelocinator basicVelocity;
        private List<KeyCommand<IBasicMotion>> velocityCmds;
        private BasicCharacterWithCommands manChar;
        private readonly ConfigurationData configData;

        private readonly MouseKeyboardInputsReciever _inputReciever;

        public MovingManGame(ConfigurationData configData)
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 800;
            Content.RootDirectory = "Content";

            iManger = new InputsStateManager();
            _inputReciever = new MouseKeyboardInputsReciever(iManger);

            this.configData = configData;
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            this.spriteBatch = new SpriteBatch(GraphicsDevice);
            this.assetsLoader = new MovingManAssetsLoader(this.configData, this.graphics.GraphicsDevice);
        }

        protected override void Initialize()
        {
            base.Initialize();

            var p1Controls = assetsLoader.Player1KeyboardControls();
            var playerAtlas = assetsLoader.PlayerAtlas();
            var animations = assetsLoader.Animations();
            //this.basicVelocity = new MapVelocityManager(0f, 0f, 45f, 45f);
            this.basicVelocity = new BasicVelocityManager(0f, 0f);
            this.velocityCmds = CommandBuilder.GetBasicMapMotion(p1Controls);

            // My player needs
            // 1. Graphics
            // 2. Position
            // 3. Animations 
            // 4. Velocity
            // this.manChar = new BasicCharacter(this.spriteBatch, playerAtlas, animations, basicVelocity, new Vector2(40, 50));
            this.manChar = new BasicCharacterWithCommands(this.spriteBatch, playerAtlas, animations, basicVelocity, new Vector2(40, 50), 45f, 45f);
        }

        protected override void Update(GameTime gameTime)
        {
            var kState = Keyboard.GetState();
            var mState = Mouse.GetState();

            var delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
            iManger.Update(gameTime, kState, mState);
            // Escape hatch
            KeyboardFunctions.QuitOnKeys(this, iManger.PressedKeys(), Keys.Escape);
            var velocityCmd = this._inputReciever.MapCommands(this.velocityCmds);
            if (velocityCmd != null){
                velocityCmd.Execute(this.manChar);
            }
            manChar.Update(delta);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            this.spriteBatch.Begin();
            this.manChar.Draw(gameTime);
            this.spriteBatch.End();

        }
    }
}
