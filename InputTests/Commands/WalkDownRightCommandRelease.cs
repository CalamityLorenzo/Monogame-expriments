using InputTests.MovingMan;

namespace InputTests.Commands
{
    public class WalkDownRightCommandRelease : WalkingManCommandEvent
    {
        public override void Execute(IWalkingMan actor)
        {
            actor.EndMoveDown();
            actor.EndMoveRight();
        }
    }
}