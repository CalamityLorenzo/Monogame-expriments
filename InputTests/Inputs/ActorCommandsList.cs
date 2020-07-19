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

        public IActorCommand UpRelease = new WalkUpCommandRelease();
        public IActorCommand DownRelease = new WalkDownCommandRelease();
        public IActorCommand LeftRelease = new WalkLeftCommandRelease();
        public IActorCommand RightRelease = new WalkRightCommandRelease();
        public IActorCommand UpLeftRelease = new WalkUpLeftCommandRelease();
        public IActorCommand UpRightRelease = new WalkUpRightCommandRelease();
        public IActorCommand DownLeftRelease = new WalkDownLeftCommandRelease();
        public IActorCommand DownRightRelease = new WalkDownRightCommandRelease();
    
    }
}
