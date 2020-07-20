using InputTests.MovingMan;
using System;
using System.Collections.Generic;
using System.Text;

namespace InputTests.Commands
{
    class FireSpecialCommand : WalkingManCommandEvent
    {
        public override void Execute(IWalkingMan actor)
        {
            actor.FireSpecial();
        }
    }
}
