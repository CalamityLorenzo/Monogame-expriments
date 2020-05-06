using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlayerCharacter.Character
{
    class HeadAnimations : IAnimations
    {
        private readonly Rectangle head;

        public HeadAnimations(Rectangle head)
        {
            this.head = head;
        }

        public Rectangle CurrentFrame()
        {
            return head;
        }

        public void Update(GameTime gameTime, float delta)
        {
            // Pick a frame
        }
    }
}
