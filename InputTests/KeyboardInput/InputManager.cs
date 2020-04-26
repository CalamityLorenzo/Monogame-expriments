using InputTests.KeyboardInput;
using KeyboardInput;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace InputTests
{
    // Which keys are currently being pressed and how long for.
    // This also includes a double click method check.
    public class InputManager
    {
        private float doubleClickLength = 750f; // Time in millisecsond to allow a dobule click
        private Dictionary<Keys, PressedKey> PreviousKeys = new Dictionary<Keys, PressedKey>();
        private Dictionary<Keys, PressedKey> CurrentKeys = new Dictionary<Keys, PressedKey>();
        private Dictionary<MouseButton, PressedKey> CurrentButtons= new Dictionary<MouseButton, PressedKey>();
        private Dictionary<MouseButton, PressedKey> PreviousButtons= new Dictionary<MouseButton, PressedKey>();
        
        
        // When the key was last pressed.
        // used to work out double taps. but is publically available. 
        /// <summary>
        /// float = Time Pressed in millis
        /// </summary>
        private Dictionary<Keys, float> HistoryKeys = new Dictionary<Keys, float>();
        private Dictionary<MouseButton, float> HistoryMouseButtons = new Dictionary<MouseButton, float>();

        public void Update(GameTime time, KeyboardState kState, MouseState mState)
        {
            var delta = (float)time.ElapsedGameTime.TotalSeconds;
            var totalTime = (float)time.TotalGameTime.TotalMilliseconds;
            var pressedKeys = kState.GetPressedKeys();
            
            SetMouseButtons(delta, totalTime, mState);
            // Check if double clicked
            var doubleClickedKeys = this.DoubleClicked(pressedKeys, totalTime, this.doubleClickLength);

            // Reset so we only have the most current keys
            CurrentKeys = new Dictionary<Keys, PressedKey>();
            foreach (var key in pressedKeys)
            {
                if (PreviousKeys.ContainsKey(key))
                {
                    var val = PreviousKeys[key];
                    val.DurationPressed += delta;
                    // you can only have double cliced for like moment.
                    val.IsDoubleClick = false;
                    CurrentKeys.Add(key, val);
                }
                else
                {
                    CurrentKeys.Add(key, new PressedKey { DurationPressed = 0f, IsDoubleClick = DoubleClickedKey(key, totalTime, this.doubleClickLength), Key = key });
                    AddToHistory(key, totalTime);
                }
            }
            PreviousKeys = CurrentKeys;
        }

        private void SetMouseButtons(float delta, float totalTime, MouseState mState)
        {
            throw new NotImplementedException();
        }

        private bool DoubleClickedKey(Keys key, float timePressed, float clickLimit)
        {
            // check to see if a double click happened within a set time, by seeing when the last time a key was pressed.
            return this.HistoryKeys.TryGetValue(key, out var pressed) ? (timePressed - pressed <= clickLimit) : false;
        }

        private void AddToHistory(Keys key, float timePressed)
        {
            this.HistoryKeys[key] = timePressed;
        }

        public Dictionary<Keys, PressedKey> PressedKeys => this.CurrentKeys;

        public Dictionary<Keys, float> HistoryKeyboard => this.HistoryKeys;

        public Dictionary<Keys, bool> DoubleClickedKeys(IEnumerable<Keys> keys, float timePressed, float clickTime)
        {
            // Check the history for keys
            var dbClicked = new Dictionary<Keys, bool>();
            foreach (var key in keys)
            { // if we are presnt in current keys it;s a false
                if (!this.CurrentKeys.ContainsKey(key))
                    dbClicked[key] = this.HistoryKeys.TryGetValue(key, out var pressed) ? (timePressed - pressed <= clickTime) : false;
                else
                    dbClicked[key] = false;
            }
            return dbClicked;
        }
    }
}
