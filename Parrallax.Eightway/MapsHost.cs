﻿using GameData;
using GameData.CharacterActions;
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

namespace Parrallax.Eightway
{
    internal class MapsHost : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont arial;
        private ConfigurationData configData;
        private MapVelocityManager velocityManager;
        private Rotator rotator;
        private InputsStateManager inputState;

        public MouseKeyboardInputsReciever InputsReciever { get; private set; }

        private List<KeyCommand<Rotator>> rTateCmds;
        private List<KeyCommand<ICharacterActions>> p1Cmds;
        private List<KeyCommand<IBasicMotion>> mapCmds;
        private BoundedBackground backgroundMap;

        public MapsHost(ConfigurationData configData)
        {
            this.configData = configData;
            Content.RootDirectory = "Content";
            this.IsMouseVisible = true;
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

            var p1Controls = new PlayerKeyboardControls
            {
                Up = Enum.Parse<Keys>(player1Dictionary["Up"]),
                Down = Enum.Parse<Keys>(player1Dictionary["Down"]),
                Left = Enum.Parse<Keys>(player1Dictionary["Left"]),
                Right = Enum.Parse<Keys>(player1Dictionary["Right"]),
                Fire = Enum.Parse<Keys>(player1Dictionary["Fire"]),
                SecondFire = Enum.Parse<Keys>(player1Dictionary["Special"])
            };

            // Configure the screen.
            graphics.PreferredBackBufferWidth = screenData.ScreenWidth;
            graphics.PreferredBackBufferHeight = screenData.ScreenHeight;
            graphics.IsFullScreen = screenData.FullScreen;
            graphics.ApplyChanges();
            // Assign the drawerer
            spriteBatch = new SpriteBatch(GraphicsDevice);

            var slowCloud = this.GraphicsDevice.FromFileName("Content/backBackground.png");
            var fastCloud = this.GraphicsDevice.FromFileName("Content/frontBackground.png");
            var sprite = this.GraphicsDevice.FromFileName("Content/FatBlock.png");
            // all rects on a particular atlas.
            // It's also the entire map for the backfround.
            var atlasRects = FramesGenerator.GenerateFrames(new FrameInfo[] { new FrameInfo(32, 32) }, new Dimensions(96, 64));
            var map = GeneralExtensions.LoadCsvMapData("Maps/BorderMap.csv");
            var topLeft = configData.Get<Vector2>("MapStartPos");
            this.velocityManager = new MapVelocityManager(0f, 0f, 0.8f, 0.8f);
            this.rotator = new Rotator(27, 99);
            this.inputState = new InputsStateManager();
            this.InputsReciever = new MouseKeyboardInputsReciever(inputState);

            this.rTateCmds = CommandBuilder.GetRotatorCommands(p1Controls);
            this.p1Cmds = CommandBuilder.GetWalkingCommands(p1Controls);
            this.mapCmds = CommandBuilder.GetBasicMapMotion(p1Controls);

            backgroundMap = new BoundedBackground(spriteBatch, sprite, atlasRects, map, new Dimensions(32, 32), new Rectangle(0,0, 1024,1152), rotator, velocityManager, topLeft, GraphicsDevice.Viewport);

            base.Initialize();
        }

        protected override void Update(GameTime gameTime)
        {
            // abort
            var delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
            var kState = Keyboard.GetState();
            this.inputState.Update(gameTime, kState, Mouse.GetState());

            KeyboardFunctions.QuitOnKeys(this, this.inputState.PressedKeys(), Keys.Escape);

            var rTateCmd = this.InputsReciever.MapKeyboardCommands(rTateCmds);
            var mapCmd = this.InputsReciever.MapKeyboardCommands(mapCmds);

            rTateCmd.ForEach(rt => rt.Execute(this.rotator));
            mapCmd.ForEach(mp => mp.Execute(this.velocityManager));
            
            backgroundMap.Update(gameTime);

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