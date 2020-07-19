using InputTests.MovingMan;
using System;
using System.Collections.Generic;
using System.Text;

namespace InputTests.Inputs
{
    class WalkDownLeftCommand : IActorCommand
    {
        public void Execute(IWalkingMan actor)
        {
            actor.MoveDown();
            actor.MoveLeft();
        }
    }
}
