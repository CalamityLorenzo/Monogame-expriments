using GameLibrary.Inputs;
using InputTests.MovingMan;
using System;
using System.Collections.Generic;
using System.Text;

namespace InputTests.Commands
{
    public abstract class WalkingManCommandEvent : IActorCommand<IWalkingMan>
    {
        public abstract void Execute(IWalkingMan actor);
    }
}
