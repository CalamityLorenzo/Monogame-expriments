using GameLibrary.Inputs;
using InputTests.MovingMan;
using System;
using System.Collections.Generic;
using System.Text;

namespace InputTests.WalkingManCommands
{
    public class ActorCommandsList
    {
        public IActorCommand<IWalkingMan> Fire => new FireCommand();
        public IActorCommand<IWalkingMan> SpecialFire = new FireSpecialCommand();
        public IActorCommand<IWalkingMan> Up = new WalkUpCommand();
        public IActorCommand<IWalkingMan> Down = new WalkDownCommand();
        public IActorCommand<IWalkingMan> Left = new WalkLeftCommand();
        public IActorCommand<IWalkingMan> Right = new WalkRightCommand();
        public IActorCommand<IWalkingMan> UpRelease = new WalkUpCommandRelease();
        public IActorCommand<IWalkingMan> DownRelease = new WalkDownCommandRelease();
        public IActorCommand<IWalkingMan> LeftRelease = new WalkLeftCommandRelease();
        public IActorCommand<IWalkingMan> RightRelease = new WalkRightCommandRelease();

        public IActorCommand<IWalkingMan> UpLeft = NULLWalkingManCommand.GetCommand;
        public IActorCommand<IWalkingMan> UpRight = NULLWalkingManCommand.GetCommand;
        public IActorCommand<IWalkingMan> DownLeft = NULLWalkingManCommand.GetCommand;
        public IActorCommand<IWalkingMan> DownRight = NULLWalkingManCommand.GetCommand;
        public IActorCommand<IWalkingMan> UpLeftRelease = NULLWalkingManCommand.GetCommand;
        public IActorCommand<IWalkingMan> UpRightRelease = NULLWalkingManCommand.GetCommand;
        public IActorCommand<IWalkingMan> DownLeftRelease = NULLWalkingManCommand.GetCommand;
        public IActorCommand<IWalkingMan> DownRightRelease = NULLWalkingManCommand.GetCommand;


    }
}
