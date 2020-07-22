using GameLibrary.Inputs;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;

namespace InputTests.KeyboardInput
{
    /// <summary>
    /// Basic unit for a keyboard command
    /// It's hierarchial so the first key is the 'most' important.
    /// If the subKey is KeyCommant<T>.Empty use that command.
    /// </summary>
    public class KeyCommand<T>
    {
        private readonly static Lazy<KeyCommand<T>> _privateCommand = new Lazy<KeyCommand<T>>(() => new KeyCommand<T>());

        private KeyCommand()
        {
            this.Key = Keys.ImeConvert;
            this.Command = null;
            this.PressType = KeyCommandPress.Unknown;
            this.SubKey = Enumerable.Empty<KeyCommand<T>>();
        }

        public KeyCommand(Keys key, KeyCommandPress pressType, IActorCommand<T> command, IEnumerable<KeyCommand<T>> subKey)
        {
            Key = key;
            PressType = pressType == KeyCommandPress.Unknown? throw new Exception("Cannot be unknown keypress"): pressType;
            Command = command ?? throw new ArgumentNullException(nameof(command));
            SubKey = subKey ?? throw new ArgumentNullException(nameof(subKey));
        }

        public KeyCommand(Keys key, KeyCommandPress pressType,  IActorCommand<T> command):this(key, pressType, command, Enumerable.Empty<KeyCommand<T>>())
        {
        }

        public Keys Key { get; }
        public KeyCommandPress PressType { get; }
        public IActorCommand<T> Command { get; }
        public IEnumerable<KeyCommand<T>> SubKey { get; }

        public static KeyCommand<T> Empty => _privateCommand.Value;
    }

    public enum KeyCommandPress
    {
        Unknown = 0,
        Up = 1, // Key has just been released
        Down = 2, // Key has just been pressed
        Pressed = 3 // Key has is being held down.
    }
}
