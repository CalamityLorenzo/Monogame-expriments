using InputTests.MovingMan;

namespace InputTests.Inputs
{
    public class WalkUpRightCommandRelease : IActorCommand
    {
        public void Execute(IWalkingMan actor)
        {
            actor.EndMoveUp();
            actor.EndMoveRight();
        }
    }
}