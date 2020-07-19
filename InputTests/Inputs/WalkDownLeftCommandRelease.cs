using InputTests.MovingMan;

namespace InputTests.Inputs
{
    public class WalkDownLeftCommandRelease : IActorCommand
    {
        public void Execute(IWalkingMan actor)
        {
            actor.EndMoveDown();
            actor.EndMoveLeft();
        }
    }
}