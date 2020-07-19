using GameLibrary.AppObjects;
using GameLibrary.PlayerThings;
using InputTests.KeyboardInput;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace InputTests.Inputs
{
    public class InputHandler
    {
        private PlayerControlKeys _keys;
        private InputsManager _inputs;
        private readonly ActorCommandsList _commands;

        public InputHandler(PlayerControlKeys keys, InputsManager inputs, ActorCommandsList commands)
        {
            this._keys = keys;
            _inputs = inputs;
            this._commands = commands;
        }

        public IActorCommand Update(GameTime time)
        {
            var keysDown = _inputs.KeysDown();
            var keysUp = _inputs.KeysUp();
            var pressedKeys = _inputs.PressedKeys();
            var mse = _inputs.PressedMouseButtons();
            if (keysDown.Contains(_keys.Up))
            {
                if (keysDown.Contains(_keys.Left)) return _commands.UpLeft;
                else if (keysDown.Contains(_keys.Right)) return _commands.UpRight;

                return _commands.Up;
            }
            else if (keysDown.Contains(_keys.Down))
            {
                if (keysDown.Contains(_keys.Left)) return _commands.DownLeft;
                else if (keysDown.Contains(_keys.Right)) return _commands.DownRight;

                return _commands.Down;
            }
            else if (keysDown.Contains(_keys.Left)) return _commands.Left;
            else if (keysDown.Contains(_keys.Right)) return _commands.Right;
            else if (keysDown.Contains(_keys.Fire) || mse.ContainsKey(GameLibrary.Models.MouseButton.Left))  return _commands.Fire;
            else if (pressedKeys.ContainsKey(_keys.SecondFire) && pressedKeys[_keys.SecondFire].IsDoubleClick) return _commands.SpecialFire;
            else if (keysUp.Contains(_keys.Up)){
                if(keysUp.Contains(_keys.Left)) return _commands.UpLeftRelease;
                if(keysUp.Contains(_keys.Right)) return _commands.UpRightRelease;
                else return _commands.UpRelease;
            }
            else if(keysUp.Contains(_keys.Down))
            {
                if (keysUp.Contains(_keys.Left)) return _commands.DownLeftRelease;
                else if (keysUp.Contains(_keys.Right)) return _commands.DownRightRelease;
                return _commands.DownRelease;
            }
            else if (keysUp.Contains(_keys.Left)) return _commands.LeftRelease;
            else if (keysUp.Contains(_keys.Right)) return _commands.RightRelease;

            else return NULLCommand.GetCommand;

        }
    }
}
