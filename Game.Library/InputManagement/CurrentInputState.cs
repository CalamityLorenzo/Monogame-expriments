using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace GameLibrary.InputManagement
{
    // The output from the InputsStateManager.
    public struct CurrentInputState
    {

        internal CurrentInputState(HashSet<Keys> keysUp, HashSet<Keys> keysDown, Dictionary<MouseButton, PressedMouseButton> currentButtons, HashSet<MouseButton> clickedButtons,  Dictionary<Keys, PressedKey> currentPressedKeys, Dictionary<Keys, float> historyKeys, Dictionary<MouseButton, float> historyMouseButtons, Microsoft.Xna.Framework.Point mousePosition)
        {
            KeysUp = keysUp;
            KeysDown = keysDown;
            this.PressedMouseButtons = currentButtons;
            this.PressedKeys  = currentPressedKeys;
            HistoryKeys = historyKeys;
            HistoryMouseButtons = historyMouseButtons;
            this.ClickedButtons = clickedButtons;
            this.MousePosition = mousePosition;
        }

        public HashSet<Keys> KeysUp { get; } 
        public HashSet<Keys> KeysDown { get; }
        public Dictionary<Keys, PressedKey> PressedKeys { get;  }
        public Dictionary<MouseButton, PressedMouseButton> PressedMouseButtons { get; }
        public HashSet<MouseButton> ClickedButtons { get;}
        public Point MousePosition { get; }
        public Dictionary<Keys, float> HistoryKeys { get; }
        public Dictionary<MouseButton, float> HistoryMouseButtons { get; }
    }
}
