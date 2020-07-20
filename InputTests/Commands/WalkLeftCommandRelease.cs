using InputTests.MovingMan;

namespace InputTests.Commands
{
    public class WalkLeftCommandRelease : WalkingManCommandEvent
    {
        public override void Execute(IWalkingMan actor)
        {
            actor.EndMoveLeft();
        }
    }
}