using GameData.CharacterActions;
using GameLibrary.Character;
using System;

namespace GameData.Commands.WalkingMan
{
    class WalkUpCommand : IActorCommand<IWalkingMan>
    {
        public void Execute(IWalkingMan actor)
        {
            actor.MoveUp();
        }

        public override string ToString()
        {
            return "Up";
        }
    }

    class WalkDownCommand : IActorCommand<IWalkingMan>
    {
        public void Execute(IWalkingMan actor)
        {
            actor.MoveDown();
        }

        public override string ToString()
        {
            return "Down";
        }
    }

    class WalkLeftCommand : IActorCommand<IWalkingMan>
    {
        public void Execute(IWalkingMan actor)
        {
            actor.MoveLeft();
        }

        public override string ToString()
        {
            return "Left";
        }
    }

    class WalkRightCommand : IActorCommand<IWalkingMan>
    {
        public void Execute(IWalkingMan actor)
        {
            actor.MoveRight();
        }

        public override string ToString()
        {
            return "Right";
        }
    }

    public class WalkUpCommandRelease : IActorCommand<IWalkingMan>
    {
        public void Execute(IWalkingMan actor)
        {
            actor.EndMoveUp();
        }

        public override string ToString()
        {
            return "End Up";
        }
    }

    public class WalkDownCommandRelease : IActorCommand<IWalkingMan>
    {
        public void Execute(IWalkingMan actor)
        {
            actor.EndMoveDown();
        }
        public override string ToString()
        {
            return "End Down";
        }
    }

    public class WalkLeftCommandRelease : IActorCommand<IWalkingMan>
    {
        public void Execute(IWalkingMan actor)
        {
            actor.EndMoveLeft();
        }

        public override string ToString()
        {
            return "End Left";
        }
    }

    public class WalkRightCommandRelease : IActorCommand<IWalkingMan>
    {
        public void Execute(IWalkingMan actor)
        {
            actor.EndMoveRight();
        }

        public override string ToString()
        {
            return "End Right";
        }

    }

    public class FireCommand : IActorCommand<IWalkingMan>
    {
        public void Execute(IWalkingMan actor)
        {
            actor.Fire();
        }


        public override string ToString()
        {
            return "Fire";
        }
    }

    public class JumpCommand : IActorCommand<IWalkingMan>
    {
        public void Execute(IWalkingMan actor)
        {
            actor.Jump();
        }


        public override string ToString()
        {
            return "Jump";
        }
    }

    class FireSpecialCommand : IActorCommand<IWalkingMan>
    {
        public void Execute(IWalkingMan actor)
        {
            actor.FireSpecial();
        }


        public override string ToString()
        {
            return "Specials";
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


        public override string ToString()
        {
            return "Null";
        }
    }
}
