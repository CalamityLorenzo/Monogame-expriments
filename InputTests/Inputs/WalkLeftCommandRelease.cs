using InputTests.MovingMan;

namespace InputTests.Inputs
{
    public class WalkLeftCommandRelease : IActorCommand
    {
        public void Execute(IWalkingMan actor)
        {
            actor.EndMoveLeft();
        }
    }
}