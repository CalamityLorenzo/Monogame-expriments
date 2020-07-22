using GameLibrary.AppObjects;
using GameLibrary.InputManagement;
using InputTests.RotatorCommands;
using System;
using System.Collections.Generic;
using System.Text;

namespace InputTests.KeyboardInput
{
    public class PlayerCommands
    {
        private readonly static Lazy<PlayerCommands> _cmd = new Lazy<PlayerCommands>(() => new PlayerCommands());
        private PlayerCommands() { }

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


        public static List<KeyCommand<Rotator>> SetRotatorCommands(PlayerControlKeys keys)
        {
            return _cmd.Value.RotatorCmds(keys);
        }
    }
}
