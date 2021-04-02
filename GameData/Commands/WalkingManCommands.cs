using GameData.CharacterActions;
using GameLibrary.Character;
using System;

namespace GameData.Commands.WalkingMan
{
    class WalkUpCommand : IActorCommand<ICharacterActions>
    {
        public void Execute(ICharacterActions actor)
        {
            actor.MoveUp();
        }

        public override string ToString()
        {
            return "Up";
        }
    }

    class WalkDownCommand : IActorCommand<ICharacterActions>
    {
        public void Execute(ICharacterActions actor)
        {
            actor.MoveDown();
        }

        public override string ToString()
        {
            return "Down";
        }
    }

    class WalkLeftCommand : IActorCommand<ICharacterActions>
    {
        public void Execute(ICharacterActions actor)
        {
            actor.MoveLeft();
        }

        public override string ToString()
        {
            return "Left";
        }
    }

    class WalkRightCommand : IActorCommand<ICharacterActions>
    {
        public void Execute(ICharacterActions actor)
        {
            actor.MoveRight();
        }

        public override string ToString()
        {
            return "Right";
        }
    }

    public class WalkUpCommandRelease : IActorCommand<ICharacterActions>
    {
        public void Execute(ICharacterActions actor)
        {
            actor.EndMoveUp();
        }

        public override string ToString()
        {
            return "End Up";
        }
    }

    public class WalkDownCommandRelease : IActorCommand<ICharacterActions>
    {
        public void Execute(ICharacterActions actor)
        {
            actor.EndMoveDown();
        }
        public override string ToString()
        {
            return "End Down";
        }
    }

    public class WalkLeftCommandRelease : IActorCommand<ICharacterActions>
    {
        public void Execute(ICharacterActions actor)
        {
            actor.EndMoveLeft();
        }

        public override string ToString()
        {
            return "End Left";
        }
    }

    public class WalkRightCommandRelease : IActorCommand<ICharacterActions>
    {
        public void Execute(ICharacterActions actor)
        {
            actor.EndMoveRight();
        }

        public override string ToString()
        {
            return "End Right";
        }

    }

    public class FireCommand : IActorCommand<ICharacterActions>
    {
        public void Execute(ICharacterActions actor)
        {
            actor.Fire();
        }


        public override string ToString()
        {
            return "Fire";
        }
    }

    public class JumpCommand : IActorCommand<ICharacterActions>
    {
        public void Execute(ICharacterActions actor)
        {
            actor.Jump();
        }


        public override string ToString()
        {
            return "Jump";
        }
    }

    class FireSpecialCommand : IActorCommand<ICharacterActions>
    {
        public void Execute(ICharacterActions actor)
        {
            actor.FireSpecial();
        }


        public override string ToString()
        {
            return "Specials";
        }
    } 

    class NULLWalkingManCommand : IActorCommand<ICharacterActions>
    {
        private NULLWalkingManCommand() { }

        private static readonly Lazy<NULLWalkingManCommand> _privateNull = new Lazy<NULLWalkingManCommand>(() => new NULLWalkingManCommand());
        public static IActorCommand<ICharacterActions> GetCommand => _privateNull.Value;

        public void Execute(ICharacterActions actor)
        {
            //;
        }


        public override string ToString()
        {
            return "Null";
        }
    }
}
