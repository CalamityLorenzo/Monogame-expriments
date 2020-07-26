using GameLibrary.AppObjects;
using GameLibrary.Character;

namespace InputTests.RotatorCommands
{

    class StopCommand : IActorCommand<Rotator>
    {
        public void Execute(Rotator actor)
        {
            actor.StopRotation();
        }
    }

    class UpCommand : IActorCommand<Rotator>
    {
        public void Execute(Rotator actor)
        {
            actor.SetDestinationAngle(0);
        }
    }
    class DownCommand : IActorCommand<Rotator>
    {
        public void Execute(Rotator actor)
        {
            actor.SetDestinationAngle(180);
        }
    }

    class LeftCommand : IActorCommand<Rotator>
    {
        public void Execute(Rotator actor)
        {
            actor.SetDestinationAngle(270);
        }
    }

    class RightCommand : IActorCommand<Rotator>
    {
        public void Execute(Rotator actor)
        {
            actor.SetDestinationAngle(90);
        }
    }

    class UpRightCommand : IActorCommand<Rotator>
    {
        public void Execute(Rotator actor)
        {
            actor.SetDestinationAngle(45);
        }
    }
    class UpLeftCommand : IActorCommand<Rotator>
    {
        public void Execute(Rotator actor)
        {
            actor.SetDestinationAngle(315);
        }
    }
    class DownLeftCommand : IActorCommand<Rotator>
    {
        public void Execute(Rotator actor)
        {
            actor.SetDestinationAngle(225);
        }
    }

    class DownRightCommand : IActorCommand<Rotator>
    {
        public void Execute(Rotator actor)
        {
            actor.SetDestinationAngle(135);
        }
    }
}
