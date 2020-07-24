using GameLibrary.Models;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameLibrary.InputManagement
{
    public class CurrentInputState
    {

        public CurrentInputState(HashSet<Keys> keysUp, HashSet<Keys> keysDown, Dictionary<MouseButton, PressedMouseButton> currentButtons, Dictionary<Keys, PressedKey> currentPressedKeys, Dictionary<Keys, float> historyKeys, Dictionary<MouseButton, float> historyMouseButtons)
        {
            KeysUp = keysUp;
            KeysDown = keysDown;
            this.PressedMouseButtons = currentButtons;
            this.PressedKeys  = currentPressedKeys;
            HistoryKeys = historyKeys;
            HistoryMouseButtons = historyMouseButtons;
        }

        public HashSet<Keys> KeysUp { get; } 
        public HashSet<Keys> KeysDown { get; }
        public Dictionary<Keys, PressedKey> PressedKeys { get;  }
        public Dictionary<MouseButton, PressedMouseButton> PressedMouseButtons { get; }
        public Dictionary<Keys, float> HistoryKeys { get; }
        public Dictionary<MouseButton, float> HistoryMouseButtons { get; }
    }
}
