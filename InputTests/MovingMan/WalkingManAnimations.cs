﻿using Library.Animation;
using System;
using System.Collections.Generic;
using System.Text;

namespace InputTests.MovingMan
{
    public class WalkingManAnimations : AnimationHost, IWalkingMan
    {
        private readonly Dictionary<string, BlockAnimationObject> animations;
        private string currentAnim = "";


        public WalkingManAnimations(Dictionary<string, BlockAnimationObject> animations)
        {
            this.animations = animations;
        }

        public void DoubleClickFire()
        {
            throw new NotImplementedException();
        }

        public void Fire()
        {
            throw new NotImplementedException();
        }

        public void MoveDown()
        {
            if (currentAnim != "MoveRight")
            {
                this.SetCurrent(animations["MoveRight"]);
                this.StartCurrent();
                currentAnim = "MoveRight";
            }
        }

        public void MoveLeft()
        {
            if (currentAnim != "MoveLeft")
            {
                this.SetCurrent(animations["MoveLeft"]);
                this.StartCurrent();
                currentAnim = "MoveLeft";

            }
        }

        public void MoveRight()
        {
            if (currentAnim != "MoveRight")
            {
                this.SetCurrent(animations["MoveRight"]);
                this.StartCurrent();
                currentAnim = "MoveRight";

            }

        }

        public void MoveUp()
        {
            if (currentAnim != "MoveLeft")
            {
                this.SetCurrent(animations["MoveLeft"]);
                this.StartCurrent();
                currentAnim = "MoveLeft";

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