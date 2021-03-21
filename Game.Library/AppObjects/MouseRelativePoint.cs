using GameLibrary.InputManagement;
using Microsoft.Xna.Framework;
using System;

namespace GameLibrary.AppObjects
{
    /// <summary>
    /// Returns the vector from a one point relative to a fixed point
    /// Also can return the angle as a unitvecotor
    /// </summary>
    public class FindVector
    {
        private Vector2 _currentPosition;
        private Vector2 _previousPosition;

        private Vector2 _currentRelativeVector;

        private Vector2 _selectedTerminal;
        private Vector2 _previousSelectedTerminal;

        private float _currentAngle;
        private readonly InputsStateManager inputs;

        public double ViewingAngle { get; private set; }

        public FindVector(Point mouseStartPosition, InputsStateManager inputs)
        {
            this._currentPosition = mouseStartPosition.ToVector2();
            this.inputs = inputs;
        }

        public void Update(float delta)
        {
            this._selectedTerminal = inputs.MousePosition.ToVector2();

            if (_selectedTerminal != _previousSelectedTerminal || _currentPosition != _previousPosition)
            {
                _currentRelativeVector = Vector2.Subtract(_selectedTerminal, _currentPosition);
                var radians = Math.Atan2(_currentRelativeVector.Y, _currentRelativeVector.X);

                this._currentAngle = (float)(radians * (180 / 3.14159));

                _previousPosition = _currentPosition;
                _previousSelectedTerminal = _selectedTerminal;
            }

        }
        /// <summary>
        /// Returns the set vector relative from the current position of the point.
        /// </summary>
        /// <returns></returns>
        public Vector2 GetVector() => _currentRelativeVector;
        // Why do I have to add a full turn?
        public float GetAngle() => _currentAngle + (_currentAngle <= -90 ? 360 + 90 : 90);

        // Where are we projecting from.
        public void SetPosition(Vector2 newPosition)
        {
            if (newPosition != _currentPosition)
            {
                _previousPosition = _currentPosition;
                _currentPosition = newPosition;
            }
        }
        //// Where on screen are projecting to
        //public void SetTerminal(Vector2 terminus)
        //{
        //    if (_selectedTerminal != terminus)
        //    {
        //        _previousSelectedTerminal = _selectedTerminal;
        //        _selectedTerminal = terminus;
        //    }
        //}
        // Where the Mouse is.
        public Vector2 GetTerminal() => this._selectedTerminal;
        /// <summary>
        /// The Home point
        /// </summary>
        public Vector2 GetPosition() => this._currentPosition;
    }
}
