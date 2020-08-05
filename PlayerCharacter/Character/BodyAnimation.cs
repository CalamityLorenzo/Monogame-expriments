using Library.Animation;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlayerCharacter.Character
{
    class BodyAnimations : OldAnimationHost, IWalkingAnimationState
    {
        private Dictionary<string, OldBlockAnimationObject> animations;
        private string currentAnim;

        public BodyAnimations(Dictionary<string, OldBlockAnimationObject> animations)
        {
            this.animations = animations;
        }

        public void ClimbDown()
        {
            throw new NotImplementedException();
        }

        public void ClimbUp()
        {
            throw new NotImplementedException();
        }

        public void WalkDown()
        {
            if (currentAnim != "WalkDown")
            {
                this.SetCurrent(animations["WalkDown"]);
                this.StartCurrent();
                currentAnim = "WalkDown";
            }
        }

        public void WalkLeft()
        {
            if (currentAnim != "WalkLeft")
            {
                this.SetCurrent(animations["WalkLeft"]);
                this.StartCurrent();
                currentAnim = "WalkLeft";
            }
        }

        public void WalkRight()
        {
            if (currentAnim != "WalkRight")
            {
                this.SetCurrent(animations["WalkRight"]);
                this.StartCurrent();
                currentAnim = "WalkRight";
            }
        }

        public void WalkUp()
        {
            if (currentAnim != "WalkUp")
            {
                this.SetCurrent(animations["WalkUp"]);
                this.StartCurrent();
                currentAnim = "WalkUp";
            }
        }

        public void Standing()
        {
            if (currentAnim != "Standing")
            {
                this.SetCurrent(animations["Standing"]);
                this.StartCurrent();
                currentAnim = "Standing";
            }
        }
    }
}
