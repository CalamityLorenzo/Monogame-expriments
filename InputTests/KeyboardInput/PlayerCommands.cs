using GameLibrary.AppObjects;
using InputTests.MovingMan;
using InputTests.RotatorCommands;
using InputTests.WalkingManCommands;
using System;
using System.Collections.Generic;
using System.Text;

namespace InputTests.KeyboardInput
{
    public class PlayerCommands
    {
        private readonly static Lazy<PlayerCommands> _cmd = new Lazy<PlayerCommands>(() => new PlayerCommands());
        private PlayerCommands() { }

        private List<KeyCommand<IWalkingMan>> Commands(PlayerControlKeys keys) => new List<KeyCommand<IWalkingMan>>
        {
            new KeyCommand<IWalkingMan>(keys.Up, KeyCommandPress.Down, new WalkUpCommand()
                ),
            new KeyCommand<IWalkingMan>(keys.Down, KeyCommandPress.Down, new WalkDownCommand()
                ),
            new KeyCommand<IWalkingMan>(keys.Left, KeyCommandPress.Down, new WalkLeftCommand()),
            new KeyCommand<IWalkingMan>(keys.Right, KeyCommandPress.Down, new WalkRightCommand()),
            new KeyCommand<IWalkingMan>(keys.Fire, KeyCommandPress.Down, new FireCommand()),

            new KeyCommand<IWalkingMan>(keys.Up, KeyCommandPress.Up, new WalkUpCommandRelease()
                ),
            new KeyCommand<IWalkingMan>(keys.Down, KeyCommandPress.Up, new WalkDownCommandRelease()
                ),
            new KeyCommand<IWalkingMan>(keys.Left, KeyCommandPress.Up, new WalkLeftCommandRelease()),
            new KeyCommand<IWalkingMan>(keys.Right, KeyCommandPress.Up, new WalkRightCommandRelease()),
        };

        private List<KeyCommand<Rotator>> RotatorCmds(PlayerControlKeys keys) => new List<KeyCommand<Rotator>>
        {
            new KeyCommand<Rotator>(keys.Up, KeyCommandPress.Down, new UpCommand(),
                new List<KeyCommand<Rotator>>{
                    new KeyCommand<Rotator>(keys.Left, KeyCommandPress.Pressed, new UpLeftCommand()),
                    new KeyCommand<Rotator>(keys.Right, KeyCommandPress.Pressed, new UpRightCommand()),
            }),
            new KeyCommand<Rotator>(keys.Down, KeyCommandPress.Down, new DownCommand(),
                new List<KeyCommand<Rotator>>{
                    new KeyCommand<Rotator>(keys.Left, KeyCommandPress.Pressed, new DownLeftCommand()),
                    new KeyCommand<Rotator>(keys.Right, KeyCommandPress.Pressed, new DownRightCommand()),
            }),
            new KeyCommand<Rotator>(keys.Right, KeyCommandPress.Down, new RightCommand(),
                   new List<KeyCommand<Rotator>>{
                    new KeyCommand<Rotator>(keys.Up, KeyCommandPress.Pressed, new UpRightCommand()),
                    new KeyCommand<Rotator>(keys.Down, KeyCommandPress.Pressed, new DownRightCommand()),
            }),
            new KeyCommand<Rotator>(keys.Left, KeyCommandPress.Down, new LeftCommand(),
                   new List<KeyCommand<Rotator>>{
                    new KeyCommand<Rotator>(keys.Up, KeyCommandPress.Pressed, new UpLeftCommand()),
                    new KeyCommand<Rotator>(keys.Down, KeyCommandPress.Pressed, new DownLeftCommand()),
            }),
            new KeyCommand<Rotator>(keys.Up, KeyCommandPress.Up, new StopCommand(),
                new List<KeyCommand<Rotator>>{
                    new KeyCommand<Rotator>(keys.Left, KeyCommandPress.Pressed, new LeftCommand()),
                    new KeyCommand<Rotator>(keys.Right, KeyCommandPress.Pressed, new RightCommand()),
                    new KeyCommand<Rotator>(keys.Down, KeyCommandPress.Pressed, new DownCommand()),
            }),
            new KeyCommand<Rotator>(keys.Down, KeyCommandPress.Up, new StopCommand(),
                new List<KeyCommand<Rotator>>{
                    new KeyCommand<Rotator>(keys.Left, KeyCommandPress.Pressed, new LeftCommand()),
                    new KeyCommand<Rotator>(keys.Right, KeyCommandPress.Pressed, new RightCommand()),
                    new KeyCommand<Rotator>(keys.Up, KeyCommandPress.Pressed, new UpCommand()),
            }),
            new KeyCommand<Rotator>(keys.Right, KeyCommandPress.Up, new StopCommand(),
                new List<KeyCommand<Rotator>>{
                     new KeyCommand<Rotator>(keys.Up, KeyCommandPress.Pressed, new UpCommand()),
                     new KeyCommand<Rotator>(keys.Down, KeyCommandPress.Pressed, new DownCommand()),
                     new KeyCommand<Rotator>(keys.Left, KeyCommandPress.Pressed, new LeftCommand())
            }),
            new KeyCommand<Rotator>(keys.Left, KeyCommandPress.Up, new StopCommand(),
                new List<KeyCommand<Rotator>>{
                     new KeyCommand<Rotator>(keys.Up, KeyCommandPress.Pressed, new UpCommand()),
                     new KeyCommand<Rotator>(keys.Down, KeyCommandPress.Pressed, new DownCommand()),
                     new KeyCommand<Rotator>(keys.Right, KeyCommandPress.Pressed, new RightCommand())

            })
        };

        public static List<KeyCommand<IWalkingMan>> SetCommands(PlayerControlKeys keys)
        {
            return _cmd.Value.Commands(keys);
        }

        public static List<KeyCommand<Rotator>> SetRotatorCommands(PlayerControlKeys keys)
        {
            return _cmd.Value.RotatorCmds(keys);
        }
    }
}
