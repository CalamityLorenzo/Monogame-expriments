using GameLibrary.Animation;
using GameLibrary.AppObjects;
using GameLibrary.Extensions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace BasicJeep
{
    internal class JeepCharacter
    {
        private readonly SpriteBatch spritebatch;
        private readonly Texture2D atlas;
        private readonly AnimationPlayer animation;
        private readonly Rotator rotator;
        private readonly IVelocinator velos;
        private Vector2 currentPos;
        private float currentAngle;

        public JeepCharacter(SpriteBatch spritebatch, Texture2D atlas, AnimationPlayer animation, Rotator rotator, IVelocinator velos, Vector2 startPos)
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
            this.currentPos = currentPos.AddX(velos.VelocityX * deltaTime)
                                    .AddY(velos.VelocityY * deltaTime);
            animation.Update(deltaTime);
            SetCurrentFrame(rotator.CurrentAngle);
        }

        public void SetCurrentFrame(float currentAngle)
        {
            if (this.currentAngle == currentAngle) return;
            var frameSet =  currentAngle switch
            {
                var angle when angle > 345 && angle<= 360 || angle >= 0f && angle <= 15f => "Up",
                
                var angle when angle > 15f && angle < 45f => "UpUpRight",
                var angle when angle >= 45f && angle < 75f => "UpRight",
                var angle when angle >= 75f && angle <= 105f => "Right",

                var angle when angle > 105f && angle <= 135f => "DownRight",
                var angle when angle > 135f && angle <= 165f => "DownDownRight",
                var angle when angle > 165f && angle < 195f => "Down",

                var angle when angle >= 195f && angle < 225f => "DownDownLeft",
                var angle when angle >= 225f && angle <= 255f => "DownLeft",
                var angle when angle > 255f && angle <= 285f => "Left",

                var angle when angle > 285f && angle <= 315f => "UpLeft",
                var angle when angle > 315f && angle <= 345f => "UpUpLeft",
                _ => this.animation.CurrentSetName(),
            };
            this.currentAngle = currentAngle;
            this.animation.SetFrames(frameSet);
        }

        public void Draw(GameTime gameTime)
        {
            spritebatch.Draw(this.atlas, this.currentPos, animation.CurrentFrame(), Color.White);
        }
    }
}
