using System;
using System.Collections.Generic;
using System.Text;

namespace GameLibrary.AppObjects
{
    public interface IVelocinator
    {
        public void SetVelocityX(float x);
        public void SetVelocityY(float y);

        public float VelocityX { get; }
        public float VelocityY { get; }
    }
}
