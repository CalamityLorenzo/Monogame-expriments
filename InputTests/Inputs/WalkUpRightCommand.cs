using InputTests.MovingMan;
using System;
using System.Collections.Generic;
using System.Text;

namespace InputTests.Inputs
{
    class WalkUpRightCommand : IActorCommand
    {
        public void Execute(IWalkingMan actor)
        {
            actor.MoveUp();
            actor.MoveRight();
        }
    }
}
