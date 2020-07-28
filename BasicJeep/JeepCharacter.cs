using GameLibrary.Animation;
using GameLibrary.AppObjects;
using GameLibrary.Extensions;
    using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace BasicJeep
{
    public class JeepCharacter
    {
        private readonly SpriteBatch spritebatch;
        private readonly Texture2D atlas;
        private readonly Rectangle[] frames;
        private readonly Rotator rotator;
        private readonly IVelocinator velos;
        private Vector2 currentPos;
        private Rectangle currentFrame;

        public JeepCharacter(SpriteBatch spritebatch, Texture2D atlas, Rectangle[] frames, Rotator rotator, IVelocinator velos, Vector2 startPos)
        {
            this.spritebatch = spritebatch;
            this.atlas = atlas;
            this.frames = frames;
            this.rotator = rotator;
            this.velos = velos;
            this.currentPos = startPos;
        }

        public void Update(float deltaTime)
        {
            this.currentFrame = GetCurrentFrame(rotator.CurrentAngle);
            this.currentPos = currentPos.AddX(velos.VelocityX * deltaTime)
                                    .AddY(velos.VelocityY * deltaTime);
        }

        public Rectangle GetCurrentFrame(float currentAngle)
        {
            return currentAngle switch
            {
                var angle when angle >= 0f && angle <= 20f => frames[0],  
                var angle when angle > 20f && angle <= 44f => frames[1],
                var angle when angle > 44f && angle <= 80f => frames[2],

                var angle when angle > 80f && angle <= 110f => frames[3],
                var angle when angle > 110f && angle <= 145f => frames[4],
                var angle when angle > 145f && angle <= 175f => frames[5],

                var angle when angle > 175f && angle <= 210f => frames[6],
                var angle when angle > 210f && angle <= 235f => frames[7],
                var angle when angle > 235f && angle <= 265f => frames[8],

                var angle when angle > 265f && angle <= 300f => frames[9],
                var angle when angle > 300f && angle <= 325f => frames[10],
                var angle when angle > 325f && angle <= 350f => frames[11],
                _ => frames[0]
            };
        }

        public void Draw(GameTime gameTime)
        {
            spritebatch.Draw(this.atlas, this.currentPos, this.currentFrame, Color.White);
        }
    }
}
