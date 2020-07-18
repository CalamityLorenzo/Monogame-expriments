using GameLibrary.PlayerThings;
using InputTests.KeyboardInput;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace InputTests.Inputs
{
    public class InputHandler
    {
        private PlayerControlKeys _keys;

        public InputHandler(PlayerControlKeys Keys)
        {
            this._keys = Keys;
        }

        public void Update(GameTime time)
        {
            var delta = (float)time.ElapsedGameTime.TotalSeconds;
            var totalTime = (float)time.TotalGameTime.TotalMilliseconds;


        }
    }
}
