using InputTests.MovingMan;

namespace InputTests.Commands
{
    public class WalkRightCommandRelease : WalkingManCommandEvent
    {
        public override void Execute(IWalkingMan actor)
        {
            actor.EndMoveRight();
        }
    }
}