using InputTests.MovingMan;

namespace InputTests.Commands
{
    public class WalkDownLeftCommandRelease : WalkingManCommandEvent
    {
        public override void Execute(IWalkingMan actor)
        {
            actor.EndMoveDown();
            actor.EndMoveLeft();
        }
    }
}