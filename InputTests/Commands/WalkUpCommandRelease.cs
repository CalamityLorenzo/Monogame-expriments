using InputTests.MovingMan;

namespace InputTests.Commands
{
    public class WalkUpCommandRelease : WalkingManCommandEvent
    {
        public override void Execute(IWalkingMan actor)
        {
            actor.EndMoveUp();
        }
    }
}