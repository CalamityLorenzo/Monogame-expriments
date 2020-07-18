using Library.Animation;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlayerCharacter.Character
{
    class HeadAnimations : AnimationHost
    {
        
        private readonly Dictionary<string, BlockAnimationObject> animations;
        private string currentAnim;

        public HeadAnimations(Dictionary<string, BlockAnimationObject> animations)
        {
            this.animations = animations;
        }

       public void LookLeft()
        {
            if (currentAnim != "LookLeft")
            {
                this.SetCurrent(animations["LookLeft"]);
                this.StartCurrent();
                currentAnim = "LookLeft";
            }
        }

        public void LookRight()
        {
            if (currentAnim != "LookRight")
            {
                this.SetCurrent(animations["LookRight"]);
                this.StartCurrent();
                currentAnim = "LookRight";
            }
        }

        public void LookUp()
        {
            if (currentAnim != "LookUp")
            {
                this.SetCurrent(animations["LookUp"]);
                this.StartCurrent();
                currentAnim = "LookUp";
            }
        }

        public void LookDown()
        {
            if (currentAnim != "LookDown")
            {
                this.SetCurrent(animations["LookDown"]);
                this.StartCurrent();
                currentAnim = "LookDown";
            }
        }

        public void LookUpRight()
        {
            if (currentAnim != "LookUpRight")
            {
                this.SetCurrent(animations["LookUpRight"]);
                this.StartCurrent();
                currentAnim = "LookUpRight";
            }
        }
        public void LookUpLeft()
        {
            if (currentAnim != "LookUpLeft")
            {
                this.SetCurrent(animations["LookUpLeft"]);
                this.StartCurrent();
                currentAnim = "LookUpLeft";
            }
        }


    }
}
