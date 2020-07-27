using GameData.CharacterActions;
using GameLibrary.AppObjects;
using GameLibrary.Character;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameData.Commands
{
    public class UpVelocityCommand : IActorCommand<IBasicMotion>
    {
        public void Execute(IBasicMotion actor)
        {
            actor.MoveUp();
        }
    }

    public class DownVelocityCommand : IActorCommand<IBasicMotion>
    {
        public void Execute(IBasicMotion actor)
        {
            actor.MoveDown();
        }
    }

    public class LeftVelocityCommand : IActorCommand<IBasicMotion>
    {
        public void Execute(IBasicMotion actor)
        {
            actor.MoveLeft();
        }
    }

    public class RightVelocityCommand : IActorCommand<IBasicMotion>
    {
        public void Execute(IBasicMotion actor)
        {
            actor.MoveRight();
        }
    }

    public class UpVelocityCommandRelease : IActorCommand<IBasicMotion>
    {
        public void Execute(IBasicMotion actor)
        {
            actor.EndMoveUp();
        }
    }

    public class DownVelocityCommandRelease : IActorCommand<IBasicMotion>
    {
        public void Execute(IBasicMotion actor)
        {
            actor.EndMoveDown();
        }
    }

    public class LeftVelocityCommandRelease : IActorCommand<IBasicMotion>
    {
        public void Execute(IBasicMotion actor)
        {
            actor.EndMoveLeft();

        }
    }

    public class RightVelocityCommandRelease : IActorCommand<IBasicMotion>
    {
        public void Execute(IBasicMotion actor)
        {
            actor.EndMoveRight();
        }
    }
}
