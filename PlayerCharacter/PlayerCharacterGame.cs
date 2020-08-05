using GameLibrary.Animation;
using GameLibrary.Animation.Utilities;
using GameLibrary.AppObjects;
using GameLibrary.Extensions;
using Library.Animation;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PlayerCharacter.Character;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Security.AccessControl;
using System.Text;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace PlayerCharacter
{
    public class PlayerCharacterGame : Game
    {
        GraphicsDeviceManager _graphicsDevice;
        SpriteBatch _spriteBatch;
        private StaticMan statMan;

        public MouseRelativePoint MPointer { get; private set; }

        
        SpriteFont arial;
        private Texture2D mousePWidth;
        private Texture2D mouseHWidth;
        private Point mousePosition;

        public PlayerCharacterGame()
        {
            _graphicsDevice = new GraphicsDeviceManager(this);
            _graphicsDevice.PreferredBackBufferWidth = 800;
            _graphicsDevice.PreferredBackBufferHeight = 800;
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(_graphicsDevice.GraphicsDevice);

            this.Content.RootDirectory = "./Content";

            var walkingLeft = Texture2D.FromFile(GraphicsDevice, "./Content/WalkingLeft-headless.png");
            var walkingRight = Texture2D.FromFile(GraphicsDevice, "./Content/WalkingRight-headless.png");
            var standing = Texture2D.FromFile(GraphicsDevice, "./Content/Standing-headless.png");
            var head = Texture2D.FromFile(GraphicsDevice, "./Content/head.png");
            var crossHairs = Texture2D.FromFile(GraphicsDevice, "./Content/CrossHairs_one.png");
            var wlFrames = FramesGenerator.GenerateFrames(new FrameInfo(72, 77), new Dimensions(walkingLeft.Width, walkingLeft.Height));
            var standingFrames = FramesGenerator.GenerateFrames(new FrameInfo(72, 77), new Dimensions(standing.Width, standing.Height));
            var wlAnimation = new OldBlockAnimationObject(wlFrames, new float[] { 0.200f, 0.200f, 0.200f, 0.200f }, true);
            var standingAnimation = new OldBlockAnimationObject(standingFrames, new float[] { 0.500f, 0.250f, 0.250f, 0.250f }, true);

            var bodyAnims = new BodyAnimations(new Dictionary<string, OldBlockAnimationObject>
            {
                { "WalkLeft", wlAnimation },
                { "WalkRight", wlAnimation },
                {"Standing",  standingAnimation }
            });

            var headAnims = new HeadAnimations(new Dictionary<string, OldBlockAnimationObject>
            {
                {"LookUp" , null},
                {"LookDown", null },
                {"LookUpLeft", null },
                {"LookUpRight", null },
                {"LookLeft", null },
                {"LookRight", null },
                {"LookStanding", null }

            });

            //var headAnims = new HeadAnimations(headRect);

            this.mousePWidth = this._spriteBatch.CreateFilledRectTexture(new Rectangle(0, 0, 50, 3), Color.Red);
            this.mouseHWidth = this._spriteBatch.CreateFilledRectTexture(new Rectangle(0, 0, 3, 50), Color.Red);

            this.MPointer = new MouseRelativePoint(new Point(200, 180));
            var statHeader  = new StaticHead(new Vector2(200, 180), StaticHead.StaticHeadState.Left, MPointer);

            //this.statMan = new StaticMan(_spriteBatch, walkingLeft, walkingRight, standing, head, statHeader, headAnims, bodyAnims, new Vector2(200, 150), new Vector2(0f, 0f));


            this.arial = this.Content.Load<SpriteFont>("NewArial");
        }

        protected override void Update(GameTime gameTime)
        {
            var mstate = Mouse.GetState();
            this.mousePosition = mstate.Position;
            var delat = (float)gameTime.TotalGameTime.TotalSeconds;
            //MPointer.SetTerminal(mstate.Position.ToVector2());
            statMan.SetMouseLook(mstate.Position.ToVector2());
            
            statMan.Update(gameTime, delat);
        }

        protected override void Draw(GameTime gameTime)
        {
            //var stringLength = this.arial.MeasureString(statMan.CurrentDirection().ToString());
            

            _graphicsDevice.GraphicsDevice.Clear(Color.Black);
            _spriteBatch.Begin();
            statMan.Draw(gameTime);
            //_spriteBatch.DrawString(arial, statHeader.CurrentDirection().ToString(), new Vector2(200, 10), Color.Crimson);
            _spriteBatch.End();
        }
    }
}
