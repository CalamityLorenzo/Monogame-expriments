using GameData.CharacterActions;
using GameData.Commands.WalkingMan;
using GameData.UserInput;
using GameLibrary.AppObjects;
using GameLibrary.Character;
using GameLibrary.InputManagement;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameData.Commands
{
    // Helper class to build the list of commands required
    public class CommandBuilder
    {
        private readonly static Lazy<CommandBuilder> _cmd = new Lazy<CommandBuilder>(() => new CommandBuilder());
        private CommandBuilder() { }

        private List<KeyCommand<ICharacterActions>> WalkingCommands(PlayerKeyboardControls keys) => new List<KeyCommand<ICharacterActions>>
        {
            new KeyCommand<ICharacterActions>(keys.Up, KeyCommandPress.Down, new WalkUpCommand()
                ),
            new KeyCommand<ICharacterActions>(keys.Down, KeyCommandPress.Down, new WalkDownCommand()
                ),
            new KeyCommand<ICharacterActions>(keys.Left, KeyCommandPress.Down, new WalkLeftCommand()),
            new KeyCommand<ICharacterActions>(keys.Right, KeyCommandPress.Down, new WalkRightCommand()),
            new KeyCommand<ICharacterActions>(keys.Fire, KeyCommandPress.Down, new FireCommand()),

            new KeyCommand<ICharacterActions>(keys.Up, KeyCommandPress.Up, new WalkUpCommandRelease()
                ),
            new KeyCommand<ICharacterActions>(keys.Down, KeyCommandPress.Up, new WalkDownCommandRelease()
                ),
            new KeyCommand<ICharacterActions>(keys.Left, KeyCommandPress.Up, new WalkLeftCommandRelease()),
            new KeyCommand<ICharacterActions>(keys.Right, KeyCommandPress.Up, new WalkRightCommandRelease()),
        };

        private List<KeyCommand<T>> MapMouseCommand<T>(MouseButton btn, Dictionary<KeyCommandPress, IActorCommand<T>> cmds) => cmds.Select(cmd => new KeyCommand<T>(btn, cmd.Key, cmd.Value)).ToList();

        private List<KeyCommand<T>> MapKeyboardCommand<T>(Keys key, Dictionary<KeyCommandPress, IActorCommand<T>> cmds) => cmds.Select(cmd => new KeyCommand<T>(key, cmd.Key, cmd.Value)).ToList();

        private List<KeyCommand<ICharacterActions>> WalkingManCommands(Dictionary<string, object> inputs)
        {
            var Up = inputs["Up"] switch
            {
                MouseButton btn => MapMouseCommand(btn, new Dictionary<KeyCommandPress, IActorCommand<ICharacterActions>>
                {
                    [KeyCommandPress.Clicked] = new WalkUpCommand(),
                    [KeyCommandPress.Released] = new WalkUpCommandRelease()
                }),
                Keys key => MapKeyboardCommand<ICharacterActions>(key, new Dictionary<KeyCommandPress, IActorCommand<ICharacterActions>>
                {
                    [KeyCommandPress.Down] = new WalkUpCommand(),
                    [KeyCommandPress.Up] = new WalkUpCommandRelease()
                }),
                _ => throw new ArgumentException("Not recognised input type key/mouse")
            };

            var Down = inputs["Down"] switch
            {
                MouseButton btn => MapMouseCommand(btn, new Dictionary<KeyCommandPress, IActorCommand<ICharacterActions>>
                {
                    [KeyCommandPress.Clicked] = new WalkDownCommand(),
                    [KeyCommandPress.Released] = new WalkDownCommandRelease()
                }),
                Keys key => MapKeyboardCommand(key, new Dictionary<KeyCommandPress, IActorCommand<ICharacterActions>>
                {
                    [KeyCommandPress.Down] = new WalkDownCommand(),
                    [KeyCommandPress.Up] = new WalkDownCommandRelease()
                }),
                _ => throw new ArgumentException("Not recognised input type key/mouse")
            };

            var Left = inputs["Left"] switch
            {
                MouseButton btn => MapMouseCommand(btn, new Dictionary<KeyCommandPress, IActorCommand<ICharacterActions>>
                {
                    [KeyCommandPress.Clicked] = new WalkLeftCommand(),
                    [KeyCommandPress.Released] = new WalkLeftCommandRelease()
                }),
                Keys key => MapKeyboardCommand(key, new Dictionary<KeyCommandPress, IActorCommand<ICharacterActions>>
                {
                    [KeyCommandPress.Down] = new WalkLeftCommand(),
                    [KeyCommandPress.Up] = new WalkLeftCommandRelease()
                }),
                _ => throw new ArgumentException("Not recognised input type key/mouse")
            };

            var Right = inputs["Right"] switch
            {
                MouseButton btn => MapMouseCommand(btn, new Dictionary<KeyCommandPress, IActorCommand<ICharacterActions>>
                {
                    [KeyCommandPress.Clicked] = new WalkRightCommand(),
                    [KeyCommandPress.Released] = new WalkRightCommandRelease()
                }),
                Keys key => MapKeyboardCommand(key, new Dictionary<KeyCommandPress, IActorCommand<ICharacterActions>>
                {
                    [KeyCommandPress.Down] = new WalkRightCommand(),
                    [KeyCommandPress.Up] = new WalkRightCommandRelease()
                }),
                _ => throw new ArgumentException("Not recognised input type key/mouse")
            };

            var Fire = inputs["Fire"] switch
            {
                MouseButton btn => MapMouseCommand(btn, new Dictionary<KeyCommandPress, IActorCommand<ICharacterActions>>
                {
                    [KeyCommandPress.Clicked] = new FireCommand()
                }),
                Keys key => MapKeyboardCommand(key, new Dictionary<KeyCommandPress, IActorCommand<ICharacterActions>>
                {
                    [KeyCommandPress.Down] = new FireCommand()
                }),
                _ => throw new ArgumentException("Not recognised input type key/mouse")
            };

            var FireSpecial = inputs["Special"] switch
            {
                MouseButton btn => MapMouseCommand(btn, new Dictionary<KeyCommandPress, IActorCommand<ICharacterActions>>
                {
                    [KeyCommandPress.Clicked] = new FireSpecialCommand()
                }),
                Keys key => MapKeyboardCommand(key, new Dictionary<KeyCommandPress, IActorCommand<ICharacterActions>>
                {
                    [KeyCommandPress.Down] = new FireSpecialCommand()
                }),
                _ => throw new ArgumentException("Not recognised input type key/mouse")
            };

            var Jump = inputs["Jump"] switch
            {
                MouseButton btn => MapMouseCommand(btn, new Dictionary<KeyCommandPress, IActorCommand<ICharacterActions>>
                {
                    [KeyCommandPress.Clicked] = new GameData.Commands.WalkingMan.JumpCommand()
                }),
                Keys key => MapKeyboardCommand(key, new Dictionary<KeyCommandPress, IActorCommand<ICharacterActions>>
                {
                    [KeyCommandPress.Down] = new GameData.Commands.WalkingMan.JumpCommand()
                }),
                _ => throw new ArgumentException("Not recognised input type key/mouse")
            };

            return Up.Concat(Down).Concat(Left).Concat(Right).Concat(Fire).Concat(FireSpecial).Concat(Jump).ToList();

        }

        //    new List<KeyCommand<IWalkingMan>>
        //{

        //    new KeyCommand<IWalkingMan>(keys.Up, KeyCommandPress.Down, new WalkUpCommand()
        //        ),
        //    new KeyCommand<IWalkingMan>(keys.Down, KeyCommandPress.Down, new WalkDownCommand()
        //        ),
        //    new KeyCommand<IWalkingMan>(keys.Left, KeyCommandPress.Down, new WalkLeftCommand()),
        //    new KeyCommand<IWalkingMan>(keys.Right, KeyCommandPress.Down, new WalkRightCommand()),
        //    new KeyCommand<IWalkingMan>(keys.Fire, KeyCommandPress.Down, new FireCommand()),

        //    new KeyCommand<IWalkingMan>(keys.Up, KeyCommandPress.Up, new WalkUpCommandRelease()
        //        ),
        //    new KeyCommand<IWalkingMan>(keys.Down, KeyCommandPress.Up, new WalkDownCommandRelease()
        //        ),
        //    new KeyCommand<IWalkingMan>(keys.Left, KeyCommandPress.Up, new WalkLeftCommandRelease()),
        //    new KeyCommand<IWalkingMan>(keys.Right, KeyCommandPress.Up, new WalkRightCommandRelease()),
        //};

        private List<KeyCommand<Rotator>> RotatorCmds(PlayerKeyboardControls keys) => new List<KeyCommand<Rotator>>
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

        private List<KeyCommand<IBasicMotion>> MapMovement(PlayerKeyboardControls keys) => new List<KeyCommand<IBasicMotion>>
        {
            new KeyCommand<IBasicMotion>(keys.Up, KeyCommandPress.Down, new UpVelocityCommand()),
            new KeyCommand<IBasicMotion>(keys.Down, KeyCommandPress.Down, new DownVelocityCommand()),
            new KeyCommand<IBasicMotion>(keys.Left, KeyCommandPress.Down, new LeftVelocityCommand()),
            new KeyCommand<IBasicMotion>(keys.Right, KeyCommandPress.Down, new RightVelocityCommand()),
            new KeyCommand<IBasicMotion>(keys.Up, KeyCommandPress.Up, new UpVelocityCommandRelease()
                ),
            new KeyCommand<IBasicMotion>(keys.Down, KeyCommandPress.Up, new DownVelocityCommandRelease()
                ),
            new KeyCommand<IBasicMotion>(keys.Left, KeyCommandPress.Up, new LeftVelocityCommandRelease()),
            new KeyCommand<IBasicMotion>(keys.Right, KeyCommandPress.Up, new RightVelocityCommandRelease()),
            new KeyCommand<IBasicMotion>(keys.Special, KeyCommandPress.Down, new JumpCommand())
        };


        public static List<KeyCommand<ICharacterActions>> GetWalkingCommands(Dictionary<string, object> desktopInput)
        {
            return _cmd.Value.WalkingManCommands(desktopInput);
        }

        public static List<KeyCommand<ICharacterActions>> GetWalkingCommands(PlayerKeyboardControls keys)
        {
            return _cmd.Value.WalkingCommands(keys);
        }

        public static List<KeyCommand<Rotator>> GetRotatorCommands(PlayerKeyboardControls keys)
        {
            return _cmd.Value.RotatorCmds(keys);
        }

        public static List<KeyCommand<IBasicMotion>> GetBasicMapMotion(PlayerKeyboardControls keys) => _cmd.Value.MapMovement(keys);
    }
}
