using InputTests.MovingMan;

namespace InputTests.Inputs
{
    public class WalkUpLeftCommandRelease : IActorCommand
    {
        public void Execute(IWalkingMan actor)
        {
            actor.EndMoveUp();
            actor.EndMoveLeft();
        }
    }
}