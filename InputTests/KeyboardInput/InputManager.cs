using InputTests.KeyboardInput;
using InputTests.MouseInput;
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
        private Dictionary<MouseButton, PressedMouseButton> CurrentButtons = new Dictionary<MouseButton, PressedMouseButton>();
        private Dictionary<MouseButton, PressedMouseButton> PreviousButtons = new Dictionary<MouseButton, PressedMouseButton>();


        // When the key was last pressed.
        // used to work out double taps. but is publically available. 
        /// <summary>
        /// float = Time Pressed in millis
        /// </summary>
        private Dictionary<Keys, float> HistoryKeys = new Dictionary<Keys, float>();
        private Dictionary<MouseButton, float> HistoryMouseButtons = new Dictionary<MouseButton, float>();

        public HashSet<MouseButton> ReleasedButtons { get; private set; }

        public void Update(GameTime time, KeyboardState kState, MouseState mState)
        {
            var delta = (float)time.ElapsedGameTime.TotalSeconds;
            var totalTime = (float)time.TotalGameTime.TotalMilliseconds;
            var pressedKeys = kState.GetPressedKeys();

            SetMouseButtons(delta, totalTime, mState);

            // Check if double clicked
            var doubleClickedKeys = this.DoubleClickedKeys(pressedKeys, totalTime, this.doubleClickLength);

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
                    CurrentKeys.Add(key, new PressedKey { DurationPressed = 0f, IsDoubleClick = DoubleClicked(key, totalTime, this.doubleClickLength), Key = key });
                    AddToHistory(key, totalTime);
                }
            }
            PreviousKeys = CurrentKeys;
        }

        private void SetMouseButtons(float delta, float totalTime, MouseState mState)
        {
            SetReleasedButtons(delta, totalTime, mState);
            SetPressedButtons(delta, totalTime, mState);
        }

        private void SetPressedButtons(float delta, float totalTime, MouseState mState)
        {
            CurrentButtons = new Dictionary<MouseButton, PressedMouseButton>();
            ButtonPressed(MouseButton.Left, mState.LeftButton, this.PreviousButtons, CurrentButtons, delta, totalTime);
            ButtonPressed(MouseButton.Right, mState.RightButton, this.PreviousButtons, CurrentButtons, delta, totalTime);
            ButtonPressed(MouseButton.Middle, mState.MiddleButton, this.PreviousButtons, CurrentButtons, delta, totalTime);
            ButtonPressed(MouseButton.Four, mState.XButton1, this.PreviousButtons, CurrentButtons, delta, totalTime);
            ButtonPressed(MouseButton.Five, mState.XButton2, this.PreviousButtons, CurrentButtons, delta, totalTime);
            PreviousButtons = CurrentButtons;
        }

        private void SetReleasedButtons(float delta, float totalTime, MouseState mState)
        {
            // The mouse state has the current list of released buttons
            // The Previous has List of buttons, has the pressed buttons.
            // If was pressed (last time previous) and it now (MouseState) not pressed, Then it is released
            this.ReleasedButtons = new HashSet<MouseButton>();
            ButtonReleased(MouseButton.Left, mState.LeftButton);
            ButtonReleased(MouseButton.Right, mState.RightButton);
            ButtonReleased(MouseButton.Middle, mState.MiddleButton);
            ButtonReleased(MouseButton.Four, mState.XButton1);
            ButtonReleased(MouseButton.Five, mState.XButton2);

        }

        private void ButtonReleased(MouseButton btn, ButtonState mouseButton)
        {
            if (this.PreviousButtons.ContainsKey(btn) && mouseButton == ButtonState.Released)
                this.ReleasedButtons.Add(btn);
        }

        private void ButtonPressed(MouseButton btn, ButtonState mouseBtnState, Dictionary<MouseButton, PressedMouseButton> current, Dictionary<MouseButton, PressedMouseButton> next, float delta, float totalTime)
        {
            // Key is pressed
            if (mouseBtnState == ButtonState.Pressed)
            {
                var pressedButton = new PressedMouseButton { Button = btn };
                if (current.ContainsKey(btn))
                {
                    pressedButton.DurationPressed += current[btn].DurationPressed + delta;
                    pressedButton.IsDoubleClick = false;
                }
                else
                {
                    pressedButton.DurationPressed = 0f;
                    pressedButton.IsDoubleClick = DoubleClicked(btn, totalTime, this.doubleClickLength);
                    AddToHistory(btn, totalTime);
                }
                // Add an entry
                next.Add(btn, pressedButton);
            }

        }

        private bool DoubleClicked(MouseButton btn, float timePressed, float clickLimit)
        {
            // check to see if a double click happened within a set time, by seeing when the last time a key was pressed.
            return this.HistoryMouseButtons.TryGetValue(btn, out var pressed) ? (timePressed - pressed <= clickLimit) : false;
        }


        private bool DoubleClicked(Keys key, float timePressed, float clickLimit)
        {
            // check to see if a double click happened within a set time, by seeing when the last time a key was pressed.
            return this.HistoryKeys.TryGetValue(key, out var pressed) ? (timePressed - pressed <= clickLimit) : false;
        }

        private void AddToHistory(Keys key, float timePressed)
        {
            this.HistoryKeys[key] = timePressed;
        }

        private void AddToHistory(MouseButton button, float timePressed)
        {
            this.HistoryMouseButtons[button] = timePressed;
        }

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

        public Dictionary<Keys, PressedKey> PressedKeys() => this.CurrentKeys;

        public Dictionary<MouseButton, PressedMouseButton> PressedMouseButtons() => this.CurrentButtons;
        public HashSet<MouseButton> ReleasedMouseButtons() => this.ReleasedButtons;

        public Dictionary<Keys, float> HistoryKeyboard() => this.HistoryKeys;
        public Dictionary<Keys, float> HistoryMouse() => this.HistoryKeys;

    }
}
