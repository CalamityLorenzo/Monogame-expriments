using InputTests.MovingMan;

namespace InputTests.Inputs
{
    public class WalkUpCommandRelease : IActorCommand
    {
        public void Execute(IWalkingMan actor)
        {
            actor.EndMoveUp();
        }
    }
}