using BasicJeep.BasicAnimation;
using GameLibrary.Animation;
using GameLibrary.AppObjects;
using GameLibrary.Extensions;
    using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace BasicJeep
{
    internal class JeepCharacter
    {
        private readonly SpriteBatch spritebatch;
        private readonly Texture2D atlas;
        private readonly IAnimationHost animation;
        private readonly Rectangle[] frames;
        private readonly Rotator rotator;
        private readonly IVelocinator velos;
        private Vector2 currentPos;
        private Rectangle currentFrame;

        public JeepCharacter(SpriteBatch spritebatch, Texture2D atlas, IEnumerable<IAnimationHost> animation, Rotator rotator, IVelocinator velos, Vector2 startPos)
        {
            this.spritebatch = spritebatch;
            this.atlas = atlas;
            this.animation = animation.ElementAt(0);
            //this.frames = frames;
            
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
                var angle when angle > 350 && angle<= 360 || angle >= 0f && angle <= 20f => frames[0],  
                var angle when angle > 20f && angle <= 50f => frames[1],
                var angle when angle > 50f && angle <= 80f => frames[2],

                var angle when angle > 80f && angle <= 110f => frames[3],
                var angle when angle > 110f && angle <= 140f => frames[4],
                var angle when angle > 140f && angle <= 170f => frames[5],

                var angle when angle > 170f && angle <= 200f => frames[6],
                var angle when angle > 200f && angle <= 230f => frames[7],
                var angle when angle > 230f && angle <= 260f => frames[8],

                var angle when angle > 260f && angle <= 300f => frames[9],
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
