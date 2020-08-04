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
        private readonly IList<IAnimationHost> animation;
        private readonly Rotator rotator;
        private readonly IVelocinator velos;
        private Vector2 currentPos;
        private Rectangle currentFrame;

        public JeepCharacter(SpriteBatch spritebatch, Texture2D atlas, IList<IAnimationHost> animation, Rotator rotator, IVelocinator velos, Vector2 startPos)
        {
            this.spritebatch = spritebatch;
            this.atlas = atlas;
            this.animation = animation;

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
                var angle when angle > 350 && angle<= 360 || angle >= 0f && angle <= 20f => this.animation[0].CurrentFrame(),  
                var angle when angle > 20f && angle <= 50f => this.animation[1].CurrentFrame(),
                var angle when angle > 50f && angle <= 80f => this.animation[2].CurrentFrame(),

                var angle when angle > 80f && angle <= 110f => this.animation[3].CurrentFrame(),
                var angle when angle > 110f && angle <= 140f => this.animation[4].CurrentFrame(),
                var angle when angle > 140f && angle <= 170f => this.animation[5].CurrentFrame(),

                var angle when angle > 170f && angle <= 200f => this.animation[6].CurrentFrame(),
                var angle when angle > 200f && angle <= 230f => this.animation[7].CurrentFrame(),
                var angle when angle > 230f && angle <= 260f => this.animation[8].CurrentFrame(),

                var angle when angle > 260f && angle <= 300f => this.animation[9].CurrentFrame(),
                var angle when angle > 300f && angle <= 325f => this.animation[10].CurrentFrame(),
                var angle when angle > 325f && angle <= 350f => this.animation[11].CurrentFrame(),
                _ => this.animation[0].CurrentFrame()
            };
        }

        public void Draw(GameTime gameTime)
        {
            spritebatch.Draw(this.atlas, this.currentPos, this.currentFrame, Color.White);
        }
    }
}
