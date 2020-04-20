using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SharpDX.WIC;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Text;

namespace InputTests
{
    public class PressedKey
    {
        public Keys Key { get; set; }
        public bool IsDoubleClick { get; set; }
        public float DurationPressed { get; set; }
        public override string ToString()
        {
            return $"{Key} : {IsDoubleClick} \n ({DurationPressed.ToString("0.0000", CultureInfo.InvariantCulture)})";
        }
    }


    // Which keys are currently being pressed and how long for.
    public class KeysManager
    {
        private float doubleClickLength = 750f; // Time in millisecsond to allow a dobule click
        private Dictionary<Keys, PressedKey> PreviousKeys = new Dictionary<Keys, PressedKey>();
        private Dictionary<Keys, PressedKey> CurrentKeys = new Dictionary<Keys, PressedKey>();
        // When the key was last pressed.
        // used to work out double taps.
        private Dictionary<Keys, float> HistoryKeys = new Dictionary<Keys, float>();

        public void Update(GameTime time, KeyboardState kState)
        {
            var delta = (float)time.ElapsedGameTime.TotalSeconds;
            var totalTime = (float)time.TotalGameTime.TotalMilliseconds;
            var pressedKeys = kState.GetPressedKeys();

            // Check if double clicked
            var doubleClicked = this.DoubleClicked(pressedKeys, totalTime, this.doubleClickLength);
            //// Add the currently batch of presed keys into the history.
            //AddToHistory(pressedKeys, totalTime);

            // Reset so we only have the most current keys
            CurrentKeys = new Dictionary<Keys, PressedKey>();
            foreach (var key in pressedKeys)
            {
                if (PreviousKeys.ContainsKey(key))
                {
                    var val = PreviousKeys[key];
                    val.DurationPressed += delta;
                    CurrentKeys.Add(key, val);
                }
                else
                {
                    CurrentKeys.Add(key, new PressedKey { DurationPressed = 0f, IsDoubleClick = DoubleClicked(key, totalTime, this.doubleClickLength), Key = key });
                    AddToHistory(key, totalTime);
                }
            }
            PreviousKeys = CurrentKeys;
        }

        private bool DoubleClicked(Keys key, float timePressed, float clickLimit)
        {
            return this.HistoryKeys.TryGetValue(key, out var pressed) ? (timePressed - pressed <= clickLimit) : false;
        }

        private void AddToHistory(Keys key, float timePressed)
        {
            this.HistoryKeys[key] = timePressed;
        }

        public Dictionary<Keys, PressedKey> PressedKeys => this.CurrentKeys;

        public Dictionary<Keys, float> History => this.HistoryKeys;

        public Dictionary<Keys, bool> DoubleClicked(IEnumerable<Keys> keys, float timePressed, float clickTime)
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
