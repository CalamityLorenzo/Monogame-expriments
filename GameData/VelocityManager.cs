using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace GameData
{
    /// <summary>
    /// You compose your object so this is what managers he velocity
    /// Then at update read the values.
    /// </summary>
    public class VelocityManager
    {

        public VelocityManager(float maxVelocity, float startVelocityX, float startVelocityY)
        {
            MaxVelocity = maxVelocity;
            VelocityY = startVelocityY;
            VelocityX = startVelocityX;
        }

        public void SetVelocityX(float x) => this.VelocityX = x >MaxVelocity?MaxVelocity:x;
        public void SetVelocityY(float y) => this.VelocityY = y>MaxVelocity?MaxVelocity:y;

        public float MaxVelocity { get; }
        public float VelocityY { get; internal set; }
        public float VelocityX { get; internal set; }
    }
}
