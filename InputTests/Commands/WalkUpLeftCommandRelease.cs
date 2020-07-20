using InputTests.MovingMan;

namespace InputTests.Commands
{
    public class WalkUpLeftCommandRelease : WalkingManCommandEvent
    {
        public override void Execute(IWalkingMan actor)
        {
            actor.EndMoveUp();
            actor.EndMoveLeft();
        }
    }
}