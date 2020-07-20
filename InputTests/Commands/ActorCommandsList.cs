using GameLibrary.Inputs;
using InputTests.MovingMan;
using System;
using System.Collections.Generic;
using System.Text;

namespace InputTests.Commands
{
    public class ActorCommandsList
    {
        public IActorCommand<IWalkingMan> Fire => new FireCommand();
        public IActorCommand<IWalkingMan> SpecialFire = new FireSpecialCommand();
        public IActorCommand<IWalkingMan> Up = new WalkUpCommand();
        public IActorCommand<IWalkingMan> Down = new WalkDownCommand();
        public IActorCommand<IWalkingMan> Left = new WalkLeftCommand();
        public IActorCommand<IWalkingMan> Right = new WalkRightCommand();
        public IActorCommand<IWalkingMan> UpLeft = new WalkUpLeftCommand();
        public IActorCommand<IWalkingMan> UpRight = new WalkUpLeftCommand();
        public IActorCommand<IWalkingMan> DownLeft = new WalkDownLeftCommand();
        public IActorCommand<IWalkingMan> DownRight = new WalkDownRightCommand();

        public IActorCommand<IWalkingMan> UpRelease = new WalkUpCommandRelease();
        public IActorCommand<IWalkingMan> DownRelease = new WalkDownCommandRelease();
        public IActorCommand<IWalkingMan> LeftRelease = new WalkLeftCommandRelease();
        public IActorCommand<IWalkingMan> RightRelease = new WalkRightCommandRelease();
        public IActorCommand<IWalkingMan> UpLeftRelease = new WalkUpLeftCommandRelease();
        public IActorCommand<IWalkingMan> UpRightRelease = new WalkUpRightCommandRelease();
        public IActorCommand<IWalkingMan> DownLeftRelease = new WalkDownLeftCommandRelease();
        public IActorCommand<IWalkingMan> DownRightRelease = new WalkDownRightCommandRelease();

    }
}
