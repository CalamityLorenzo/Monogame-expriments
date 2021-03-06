﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

namespace Library.Animation
{

    [Obsolete]
    public abstract class OldAnimationHost
    {
        protected OldBlockAnimationObject CurrentAnimation { get; private set; }

        protected void SetCurrent(OldBlockAnimationObject animation)
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
