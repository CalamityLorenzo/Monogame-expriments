using System;
using System.Collections.Generic;
using System.Text;

namespace InputTests.Inputs
{
    public class ActorCommandsList
    {
        public IActorCommand Fire => new FireCommand();
        public IActorCommand SpecialFire = new FireSpecialCommand();
        public IActorCommand Up = new WalkUpCommand();
        public IActorCommand Down = new WalkDownCommand();
        public IActorCommand Left = new WalkLeftCommand();
        public IActorCommand Right = new WalkRightCommand();
        public IActorCommand UpLeft = new WalkUpLeftCommand();
        public IActorCommand UpRight = new WalkUpLeftCommand();
        public IActorCommand DownLeft = new WalkDownLeftCommand();
        public IActorCommand DownRight = new WalkDownRightCommand();
    }
}
