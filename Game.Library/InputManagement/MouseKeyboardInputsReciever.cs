using GameLibrary.Character;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace GameLibrary.InputManagement
{

    /// <summary>
    /// The Reciever that turns key presses into actor commands.
    /// </summary>
    public class MouseKeyboardInputsReciever
    {
        private InputsStateManager _inputs;

        public MouseKeyboardInputsReciever(InputsStateManager inputs)
        {
            _inputs = inputs;
        }

        private bool TestKeyState(Keys key, KeyCommandPress pressType)
        {

            return pressType switch
            {
                KeyCommandPress.Up => this._inputs.KeysUp().Contains(key),
                KeyCommandPress.Down => this._inputs.KeysDown().Contains(key),
                KeyCommandPress.Pressed => this._inputs.PressedKeys().ContainsKey(key),
                _ => false
            };

        }

        private IActorCommand<T> ValidateSubKeys<T>(IEnumerable<KeyCommand<T>> subkeys)
        {
            IActorCommand<T> currentCommand = null;
            foreach (var key in subkeys)
            {
                if (TestKeyState(key.Key, key.PressType))
                    currentCommand = key.Command;
            }
            return currentCommand;
        }

        /// <summary>
        /// Where the magic happends
        /// List of commands can be changed during an update.
        /// This should occur BEFORE any state changes to your entites are applied.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="keyCommands"></param>
        /// <returns></returns>
        //public IActorCommand<T> MapSingleCommands<T>(List<KeyCommand<T>> keyCommands)
        //{
        //    foreach (var keyCommand in keyCommands)
        //    {
        //        if (TestKeyState(keyCommand.Key, keyCommand.PressType))
        //        {
        //            Debug.WriteLine(keyCommand.Key.ToString() + " " + (keyCommand.PressType == KeyCommandPress.Up));
        //            var command = this.ValidateSubKeys(keyCommand.SubKey);
        //            if (command == null)
        //                return keyCommand.Command;
        //            return command;
        //        }
        //    }
        //    return null; //not cool.
        //}

        public List<IActorCommand<T>> MapCommands<T>(List<KeyCommand<T>> keyCommands)
        {
            var results = new List<IActorCommand<T>>();
            foreach (var keyCommand in keyCommands)
            {
                if (TestKeyState(keyCommand.Key, keyCommand.PressType))
                {
                    var command = this.ValidateSubKeys(keyCommand.SubKey);
                    if (command == null)
                    {
                        if (keyCommand.Command != null)
                            results.Add(keyCommand.Command);
                    }
                    else
                        results.Add(command);
                }
            }
            return results;
        }
    }
}
