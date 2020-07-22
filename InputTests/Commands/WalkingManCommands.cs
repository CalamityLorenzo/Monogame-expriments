using GameLibrary.Inputs;
using InputTests.MovingMan;
using System;
using System.Collections.Generic;
using System.Text;

namespace InputTests.WalkingManCommands
{
    class WalkUpCommand : IActorCommand<IWalkingMan>
    {
        public void Execute(IWalkingMan actor)
        {
            actor.MoveUp();
        }
    }

    class WalkDownCommand : IActorCommand<IWalkingMan>
    {
        public void Execute(IWalkingMan actor)
        {
            actor.MoveDown();
        }
    }

    class WalkLeftCommand : IActorCommand<IWalkingMan>
    {
        public void Execute(IWalkingMan actor)
        {
            actor.MoveLeft();
        }
    }

    class WalkRightCommand : IActorCommand<IWalkingMan>
    {
        public void Execute(IWalkingMan actor)
        {
            actor.MoveRight();
        }
    }

    public class WalkUpCommandRelease : IActorCommand<IWalkingMan>
    {
        public void Execute(IWalkingMan actor)
        {
            actor.EndMoveUp();
        }
    }

    public class WalkDownCommandRelease : IActorCommand<IWalkingMan>
    {
        public void Execute(IWalkingMan actor)
        {
            actor.EndMoveDown();
        }
    }

    public class WalkLeftCommandRelease : IActorCommand<IWalkingMan>
    {
        public void Execute(IWalkingMan actor)
        {
            actor.EndMoveLeft();
        }
    }

    public class WalkRightCommandRelease : IActorCommand<IWalkingMan>
    {
        public void Execute(IWalkingMan actor)
        {
            actor.EndMoveRight();
        }
    }

    public class FireCommand : IActorCommand<IWalkingMan>
    {
        public void Execute(IWalkingMan actor)
        {
            actor.Fire();
        }
    }

    class FireSpecialCommand : IActorCommand<IWalkingMan>
    {
        public void Execute(IWalkingMan actor)
        {
            actor.FireSpecial();
        }
    }

    class NULLWalkingManCommand : IActorCommand<IWalkingMan>
    {
        private NULLWalkingManCommand() { }

        private static readonly Lazy<NULLWalkingManCommand> _privateNull = new Lazy<NULLWalkingManCommand>(() => new NULLWalkingManCommand());
        public static IActorCommand<IWalkingMan> GetCommand => _privateNull.Value;

        public void Execute(IWalkingMan actor)
        {
            //;
        }
    }
}
