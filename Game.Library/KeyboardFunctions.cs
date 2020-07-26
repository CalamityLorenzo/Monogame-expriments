using GameLibrary.InputManagement;
using GameLibrary.Models;
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
    
        /// <summary>
        /// Pass in a list of keys that trigger a hard quit (basically it's escape).
        /// </summary>
        /// <param name="hostObject"></param>
        /// <param name="pressedKeys"></param>
        /// <param name="quitKeys"></param>
        public static void QuitOnKeys(Game hostObject, Dictionary<Keys, PressedKey> pressedKeys, params Keys[] quitKeys)
        {
            foreach(var item in quitKeys)
            {
                if(pressedKeys.ContainsKey(item))
                    hostObject.Exit();
            }
        }
    }
}
