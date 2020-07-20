using GameLibrary.Inputs;
using InputTests.MovingMan;
using System;
using System.Collections.Generic;
using System.Text;

namespace InputTests.Commands
{
    public class FireCommand : WalkingManCommandEvent
    {
        public override void Execute(IWalkingMan actor)
        {
            actor.Fire();
        }
    }
}
