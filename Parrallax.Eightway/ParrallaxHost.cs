using GameData.Commands;
using GameData.UserInput;
using GameLibrary;
using GameLibrary.Animation.Utilities;
using GameLibrary.AppObjects;
using GameLibrary.Config.App;
using GameLibrary.Drawing.Backgrounds;
using GameLibrary.Extensions;
using GameLibrary.InputManagement;
using GameLibrary.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Parrallax.Eightway
{
    internal class ParrallaxHost : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont arial;
        private MouseKeyboardInputsReciever _inputProcessor;
        private InputsStateManager iStateManager;
        Rotator rotator;
        private List<KeyCommand<Rotator>> _p1Commands;

        private ConfigurationData configData;
        private BackgroundRectanglesLayer _foregroundLayter;
        private BackgroundRectanglesLayer _foregroundLayter2;
        private Vector2 _centrePoint;

        public ParrallaxHost(ConfigurationData configData)
        {
            this.configData = configData;
            Content.RootDirectory = "Content";
            graphics = new GraphicsDeviceManager(this);

        }

        protected override void LoadContent()
        {
            // TODO: use this.Content to load your game content here
            arial = this.Content.Load<SpriteFont>("Arial");

        }

        protected override void Initialize()
        {
            var screenData = configData.Get<ScreenData>("ScreenOptions");
            var player1Dictionary = configData.Get<Dictionary<string, string>>("Player1Controls");
            
            // Configure the screen.
            graphics.PreferredBackBufferWidth = screenData.ScreenWidth;
            graphics.PreferredBackBufferHeight = screenData.ScreenHeight;
            graphics.IsFullScreen = screenData.FullScreen;
            graphics.ApplyChanges();
            // Assign the drawerer
            spriteBatch = new SpriteBatch(GraphicsDevice);

            var centreHoriz = this.GraphicsDevice.Viewport.Width / 2;
            var centreVert = this.GraphicsDevice.Viewport.Height / 2;
            // create the integral to convert to float/
            _centrePoint = new Point(centreHoriz, centreVert).ToVector2();

            iStateManager = new InputsStateManager();

            var p1Controls = new PlayerKeyboardControls
            {
                Up = Keys.W,
                Down = Keys.S,
                Left = Keys.A,
                Right = Keys.D,
                Fire = Keys.LeftControl,
                SecondFire = Keys.Space
            };
            this._inputProcessor = new MouseKeyboardInputsReciever(this.iStateManager);
            this._p1Commands = CommandBuilder.GetRotatorCommands(p1Controls);
            // Can rotate
            this.rotator = new Rotator(0, 202);
            

            var slowCloud = this.GraphicsDevice.FromFileName("Content/backBackground.png");// spriteBatch.CreateFilleRectTexture( new Rectangle(0,0, gameWidth + 50, gameHEight + 50), Color.LightCyan);
            var fastCloud = this.GraphicsDevice.FromFileName("Content/frontBackground.png");  //spriteBatch.CreateFilleRectTexture(new Rectangle(0, 0, gameWidth + 50, gameHEight + 50), Color.Orange);

            // all rects on a particular atlas.
            // It's also the entire map for the backfround.
            var atlasRects = FramesGenerator.GenerateFrames(new FrameInfo[] { new FrameInfo(25, 25) }, new Dimensions(500, 500));
            var map = Enumerable.Range(0, atlasRects.Length).Select(i => i).ToList();
            
            this._foregroundLayter = new BackgroundRectanglesLayer(spriteBatch, new Texture2D[] { fastCloud, fastCloud }, rotator, .42f, new Vector2(876, 486));
            this._foregroundLayter2 = new BackgroundRectanglesLayer(spriteBatch, new Texture2D[] { slowCloud, slowCloud }, rotator, .26f, new Vector2(876, 486));

            base.Initialize();
        }

        protected override void Update(GameTime gameTime)
        {
            // abort
            var delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
            var kState = Keyboard.GetState();
            var mState = Mouse.GetState();
            this.iStateManager.Update(gameTime, kState, mState);

            KeyboardFunctions.QuitOnKeys(this, this.iStateManager.GetInputState().PressedKeys, Keys.Escape);

            
            var cmds= this._inputProcessor.MapKeyboardCommands(this._p1Commands);

            cmds.ForEach(cmd => cmd.Execute(rotator));

            rotator.Update(delta);

            _foregroundLayter.Update(gameTime);
            _foregroundLayter2.Update(gameTime);


        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            _foregroundLayter2.Draw();
            _foregroundLayter.Draw();

            //_backgroundTiles.Draw();

            // We've divided the screen top and main
            //spriteBatch.DrawFilledRect(new Vector2(0, 0), GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height/10, Color.White);
            spriteBatch.DrawString(arial, Math.Floor(this.rotator.CurrentAngle).ToString(), new Vector2(10, 10), Color.Plum);


            //// not an effective way of doing this.
            spriteBatch.DrawLine(_centrePoint.AddX(1), 99, this.rotator.CurrentAngle, Color.White);
            spriteBatch.DrawLine(_centrePoint, 100, this.rotator.CurrentAngle, Color.White);
            spriteBatch.DrawLine(_centrePoint.AddX(-1), 99, this.rotator.CurrentAngle, Color.White);


            spriteBatch.End();

        }
    }
}