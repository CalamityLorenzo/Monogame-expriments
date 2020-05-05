using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlayerCharacter.Character
{
    class BodyAnimations : IAnimations
    {
        private readonly Rectangle body;

        public BodyAnimations(Rectangle body)
        {
            this.body = body;
        }

        public Rectangle CurrentFrame()
        {
            return body;
        }
    }
}
