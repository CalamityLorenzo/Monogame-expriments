﻿using System;
using GameLibrary;
using GameLibrary.Animation;
using GameLibrary.AppObjects;
using GameLibrary.Config.App;
using GameLibrary.Drawing.Backgrounds;
using GameLibrary.Extensions;
using GameLibrary.Models;
using GameLibrary.PlayerThings;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Linq;
using System.Collections.Generic;

namespace Parrallax.Eightway
{
    internal class MapsHost : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont arial;
        AngularFourWayDirection fourway;
        private Keyboard4Way _keyboard4Way;
        private ConfigurationData configData;
        private KeyboardState pKState;
        private BoundedBackground backgroundMap;

        public MapsHost(ConfigurationData configData)
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
            var screenData = configData.ToResultType<ScreenData>("ScreenOptions");
            var player1Dictionary = configData.ToResultType<Dictionary<string, string>>("Player1Controls");
            var player1Keys = GeneralExtensions.ConvertToKeySet<PlayerControls>(player1Dictionary);
            // Configure the screen.
            graphics.PreferredBackBufferWidth = screenData.ScreenWidth;
            graphics.PreferredBackBufferHeight = screenData.ScreenHeight;
            graphics.IsFullScreen = screenData.FullScreen;
            graphics.ApplyChanges();
            // Assign the drawerer
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // Can rotate
            this.fourway = new AngularFourWayDirection (FourDirections.Stopped, 0.5f);
            // Allows you to rotate.
            this._keyboard4Way = new Keyboard4Way(this.fourway, player1Keys);

            var slowCloud = this.GraphicsDevice.FromFileName("Content/backBackground.png");
            var fastCloud = this.GraphicsDevice.FromFileName("Content/frontBackground.png");
            var sprite = this.GraphicsDevice.FromFileName("Content/FatBlock.png");
            // all rects on a particular atlas.
            // It's also the entire map for the backfround.
            var atlasRects = FramesGenerator.GenerateFrames(new FrameInfo[] { new FrameInfo(32, 32) }, new Dimensions(96, 64));
            var map = GeneralExtensions.LoadCsvMapData("Maps/BorderMap.csv");
            var topLeft = new Vector2(-123, -127);
            backgroundMap = new BoundedBackground(spriteBatch, sprite, atlasRects, map, new Dimensions(32, 32), new Rectangle(0,0, 1024,1152), fourway, topLeft, GraphicsDevice.Viewport);

            base.Initialize();
        }

        protected override void Update(GameTime gameTime)
        {
            // abort
            var kState = Keyboard.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || kState.IsKeyDown(Keys.Escape))
                Exit();

            var delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
            
            this._keyboard4Way.Update(gameTime, kState, new GamePadState());
            this.fourway.Update(delta);
            backgroundMap.Update(gameTime);

            pKState = kState;
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            // We've divided the screen top and main
            //spriteBatch.DrawFilledRect(new Vector2(0, 0), GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height/10, Color.White);
            backgroundMap.Draw();

            spriteBatch.End();

        }
    }
}