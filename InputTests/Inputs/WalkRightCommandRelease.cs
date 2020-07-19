using InputTests.MovingMan;

namespace InputTests.Inputs
{
    public class WalkRightCommandRelease : IActorCommand
    {
        public void Execute(IWalkingMan actor)
        {
            actor.EndMoveRight();
        }
    }
}