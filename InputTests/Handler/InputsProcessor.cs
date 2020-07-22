using System;
using System.Collections.Generic;
using System.Text;
using GameLibrary.AppObjects;
using GameLibrary.Inputs;
using InputTests.KeyboardInput;
using Microsoft.Xna.Framework.Input;

namespace InputTests.Handler
{
    class InputsProcessor
    {
        private InputsManager _inputs;

        public InputsProcessor(InputsManager inputs)
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
                _=>false
            };

        }

        private IActorCommand<T> ValidateSubKeys<T>(IEnumerable<KeyCommand<T>> subkeys)
        {
            IActorCommand<T> currentCommand = null;
            foreach(var key in subkeys)
            {
                if (TestKeyState(key.Key, key.PressType))
                    currentCommand = key.Command;
            }
            return currentCommand;
        }

        public IActorCommand<T> Process<T>(List<KeyCommand<T>> keyCommands)
        {
            foreach(var keyCommand in keyCommands)
            {
                if(TestKeyState(keyCommand.Key, keyCommand.PressType))
                {
                    var command = this.ValidateSubKeys(keyCommand.SubKey);
                    if (command == null)
                        return keyCommand.Command;
                    return command;
                }
            }
            return null; //not cool.
        }
    }
}
