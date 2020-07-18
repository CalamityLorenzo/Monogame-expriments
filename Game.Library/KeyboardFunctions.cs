using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary
{
    public static class KeyboardFunctions
    {
        public static void QuitOnKeys(Game hostObject, KeyboardState keyboardState, params Keys[] quitKeys)
        {
            foreach(var item in quitKeys)
            {
                if(keyboardState.IsKeyDown(item))
                    hostObject.Exit();
            }
        }
    }
}
