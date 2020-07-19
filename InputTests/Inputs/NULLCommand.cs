using InputTests.MovingMan;
using System;
using System.Collections.Generic;
using System.Text;

namespace InputTests.Inputs
{
    class NULLCommand : IActorCommand
    {
        private NULLCommand() { }

        private static readonly Lazy<NULLCommand> _privateNull = new Lazy<NULLCommand>(()=>new NULLCommand());
        public static IActorCommand GetCommand => _privateNull.Value;

        public void Execute(IWalkingMan actor)
        {
            //;
        }
    }
}
