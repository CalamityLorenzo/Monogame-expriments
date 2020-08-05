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
                var angle when angle > 345 && angle<= 360 || angle >= 0f && angle <= 15f => this.animation[0].CurrentFrame(),  
                var angle when angle > 15f && angle < 45f => this.animation[1].CurrentFrame(),
                var angle when angle >= 45f && angle < 75f => this.animation[2].CurrentFrame(),

                var angle when angle >= 75f && angle <= 105f => this.animation[3].CurrentFrame(),
                var angle when angle > 105f && angle <= 135f => this.animation[4].CurrentFrame(),
                var angle when angle > 135f && angle <= 165f => this.animation[5].CurrentFrame(),

                var angle when angle > 165f && angle < 195f => this.animation[6].CurrentFrame(),
                var angle when angle >= 195f && angle < 225f => this.animation[7].CurrentFrame(),
                var angle when angle >= 225f && angle <= 255f => this.animation[8].CurrentFrame(),

                var angle when angle > 255f && angle <= 285f => this.animation[9].CurrentFrame(),
                var angle when angle > 285f && angle <= 315f => this.animation[10].CurrentFrame(),
                var angle when angle > 315f && angle <= 345f => this.animation[11].CurrentFrame(),
                _ => this.animation[0].CurrentFrame()
            };
        }

        public void Draw(GameTime gameTime)
        {
            spritebatch.Draw(this.atlas, this.currentPos, this.currentFrame, Color.White);
        }
    }
}
