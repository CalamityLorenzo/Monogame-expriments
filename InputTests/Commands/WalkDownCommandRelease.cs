using InputTests.MovingMan;

namespace InputTests.Commands
{
    public class WalkDownCommandRelease : WalkingManCommandEvent
    {
        public override void Execute(IWalkingMan actor)
        {
            actor.EndMoveDown();
        }
    }
}