using InputTests.MovingMan;

namespace InputTests.Inputs
{
    public class WalkDownRightCommandRelease : IActorCommand
    {
        public void Execute(IWalkingMan actor)
        {
            actor.EndMoveDown();
            actor.EndMoveRight();
        }
    }
}