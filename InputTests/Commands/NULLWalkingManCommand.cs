using GameLibrary.Inputs;
using InputTests.MovingMan;
using System;
using System.Collections.Generic;
using System.Text;

namespace InputTests.Commands
{
    class NULLWalkingManCommand : WalkingManCommandEvent
    {
        private NULLWalkingManCommand() { }

        private static readonly Lazy<NULLWalkingManCommand> _privateNull = new Lazy<NULLWalkingManCommand>(()=>new NULLWalkingManCommand());
        public static WalkingManCommandEvent GetCommand => _privateNull.Value;

        public override void Execute(IWalkingMan actor)
        {
            //;
        }
    }
}
