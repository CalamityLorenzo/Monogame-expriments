using InputTests.MovingMan;

namespace InputTests.Inputs
{
    public class WalkDownCommandRelease : IActorCommand
    {
        public void Execute(IWalkingMan actor)
        {
            actor.EndMoveDown();
        }
    }
}