using InputTests.MovingMan;

namespace InputTests.Commands
{
    public class WalkUpRightCommandRelease : WalkingManCommandEvent
    {
        public override void Execute(IWalkingMan actor)
        {
            actor.EndMoveUp();
            actor.EndMoveRight();
        }
    }
}