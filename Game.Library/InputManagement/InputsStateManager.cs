using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;

namespace GameLibrary.InputManagement
{
    // Which keys are currently being pressed and how long for.
    // This also includes a double click method check.
    public class InputsStateManager
    {
        private float doubleClickLength = 750f; // Time in millisecsond to allow a dobule click
        
        private Dictionary<Keys, PressedKey> _PreviousPressedKeys = new Dictionary<Keys, PressedKey>();
        private Dictionary<Keys, PressedKey> _CurrentPressedKeys = new Dictionary<Keys, PressedKey>();

        private Dictionary<MouseButton, PressedMouseButton> _CurrentButtons = new Dictionary<MouseButton, PressedMouseButton>();
        private Dictionary<MouseButton, PressedMouseButton> _PreviousButtons = new Dictionary<MouseButton, PressedMouseButton>();

            
        private HashSet<Keys> _KeysUp = new HashSet<Keys>();
        private HashSet<Keys> _KeysDown = new HashSet<Keys>();


        // When a key was last pressed. (SO 1 entry per key)
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

            SetMouseState(delta, totalTime, mState);

            // Compare last with current for upsies.
            // Reset the list so we only have the most current keys
            var _currentPressedKeys = new Dictionary<Keys, PressedKey>();
            var _keysDown = new HashSet<Keys>();
            var doubleClickedKeys = this.DoubleClickedKeys(pressedKeys, totalTime, this.doubleClickLength);
            foreach (var key in pressedKeys){
                // Key already pressed
                if (_PreviousPressedKeys.ContainsKey(key))
                {
                    var val = _PreviousPressedKeys[key];
                    val.DurationPressed += delta;
                    // you can only have double cliced for like moment.
                    val.IsDoubleClick = false;
                    _currentPressedKeys.Add(key, val);
                }
                else
                {
                    // freshly pressed.
                    _keysDown.Add(key);
                    _currentPressedKeys.Add(key, new PressedKey { DurationPressed = 0f, IsDoubleClick = DoubleClicked(key, totalTime, this.doubleClickLength), Key = key });
                    AddToHistory(key, totalTime);
                }


            }
            this._CurrentPressedKeys = _currentPressedKeys;
            this._KeysDown = _keysDown;
            // is when it has been released.
            // So this should be the same keys as added to the history.
            // find kets that were in the previous run, that are not in the current
            this._KeysUp = new HashSet<Keys>(this._PreviousPressedKeys.Where(pk=>!_CurrentPressedKeys.Any(cp=>cp.Key == pk.Key)).Select(p=>p.Key).ToList());

            // Was pressed but now is not (Released Keys)
            _PreviousPressedKeys = _CurrentPressedKeys;

        }

        private void SetMouseState(float delta, float totalTime, MouseState mState)
        {
            SetReleasedButtons(delta, totalTime, mState);
            SetPressedButtons(delta, totalTime, mState);
        }

        private void SetPressedButtons(float delta, float totalTime, MouseState mState)
        {
            _CurrentButtons = new Dictionary<MouseButton, PressedMouseButton>();
            ButtonPressed(MouseButton.Left, mState.LeftButton, this._PreviousButtons, _CurrentButtons, delta, totalTime);
            ButtonPressed(MouseButton.Right, mState.RightButton, this._PreviousButtons, _CurrentButtons, delta, totalTime);
            ButtonPressed(MouseButton.Middle, mState.MiddleButton, this._PreviousButtons, _CurrentButtons, delta, totalTime);
            ButtonPressed(MouseButton.Four, mState.XButton1, this._PreviousButtons, _CurrentButtons, delta, totalTime);
            ButtonPressed(MouseButton.Five, mState.XButton2, this._PreviousButtons, _CurrentButtons, delta, totalTime);
            _PreviousButtons = _CurrentButtons;
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
            if (this._PreviousButtons.ContainsKey(btn) && mouseButton == ButtonState.Released)
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

        private Dictionary<Keys, bool> DoubleClickedKeys(IEnumerable<Keys> keys, float timePressed, float clickTime)
        {
            // Check the history for keys
            var dbClicked = new Dictionary<Keys, bool>();
            foreach (var key in keys)
            { // if we are presnt in current keys it;s a false
                if (!this._PreviousPressedKeys.ContainsKey(key))
                    dbClicked[key] = this.HistoryKeys.TryGetValue(key, out var pressed) ? (timePressed - pressed <= clickTime) : false;
                else
                    dbClicked[key] = false;
            }
            return dbClicked;
        }
        
        public Dictionary<MouseButton, PressedMouseButton> PressedMouseButtons() => this._CurrentButtons;
        public HashSet<MouseButton> ReleasedMouseButtons() => this.ReleasedButtons;

        public Dictionary<Keys, float> HistoryKeyboard() => this.HistoryKeys;
        public Dictionary<Keys, float> HistoryMouse() => this.HistoryKeys;
        
        // Theses are only true once, then they are discarded.
        public HashSet<Keys> KeysUp() => this._KeysUp;
        public HashSet<Keys> KeysDown() => this._KeysDown;
        public Dictionary<Keys, PressedKey> PressedKeys() => this._CurrentPressedKeys;

        public CurrentInputState GetInputState() => new CurrentInputState(_KeysUp, _KeysDown, _CurrentButtons,  _CurrentPressedKeys, HistoryKeys, HistoryMouseButtons);

    }
}
