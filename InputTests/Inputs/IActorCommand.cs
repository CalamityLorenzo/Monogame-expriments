using InputTests.MovingMan;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Text;

namespace InputTests.Inputs
{
    public interface IActorCommand
    {
        public void Execute(IWalkingMan actor);
    }
}
