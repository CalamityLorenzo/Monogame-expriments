using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

namespace Library.Animation
{
    public abstract class AnimationHost
    {
        protected BlockAnimationObject CurrentAnimation { get; private set; }

        protected void SetCurrent(BlockAnimationObject animation)
        {
            this.CurrentAnimation?.CancelAnimation();
            this.CurrentAnimation = animation;
        }

        public void StartCurrent()
        {
            this.CurrentAnimation.Start();
        }

        public void StopCurrent()
        {
            this.CurrentAnimation?.CancelAnimation();
        }

        public Rectangle CurrentFrame()
        {
            return this.CurrentAnimation.CurrentFrame;
        }

        public virtual void Update(GameTime gameTime, float delta)
        {
            if (this.CurrentAnimation != null)
                this.CurrentAnimation.Update(gameTime, delta);
        }
    }
}
