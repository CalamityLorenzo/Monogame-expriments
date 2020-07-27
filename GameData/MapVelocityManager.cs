using GameData.CharacterActions;
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameData
{
    public class MapVelocityManager : BasicVelocityManager, IBasicMotion
    {
        private float speedX;
        private float speedY;

        public MapVelocityManager(float startVelocityX, float startVelocityY, float speedX, float speedY) : base(startVelocityX, startVelocityY)
        {
            this.speedX = speedX;
            this.speedY = speedY;
        }

        public void EndMoveDown()
        {
            if (this.VelocityY > 0)
                this.VelocityY = 0f;
        }

        public void EndMoveLeft()
        {
            if (this.VelocityX< 0)
                this.VelocityX = 0;
        }

        public void EndMoveRight()
        {
            if (this.VelocityX> 0)
                this.VelocityX = 0;
        }

        public void EndMoveUp()
        {
            if (this.VelocityY < 0)
                this.VelocityY = 0f;
        }

        public void MoveDown()
        {
            this.VelocityY = +this.speedY;
        }

        public void MoveLeft()
        {
            this.VelocityX = -this.speedX;
        }

        public void MoveRight()
        {
            this.VelocityX = +this.speedX;
        }

        public void MoveUp()
        {
            this.VelocityY = - this.speedY;

        }
    }
}
