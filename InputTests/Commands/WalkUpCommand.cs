using InputTests.MovingMan;
using System;
using System.Collections.Generic;
using System.Text;

namespace InputTests.Commands
{
    class WalkUpCommand : WalkingManCommandEvent
    {
        public override void Execute(IWalkingMan actor)
        {
            actor.MoveUp();
        }
    }
}
