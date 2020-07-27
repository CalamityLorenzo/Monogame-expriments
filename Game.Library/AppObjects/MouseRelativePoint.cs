using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameLibrary.AppObjects
{
    /// <summary>
    /// Returns the vector from a one point relative to a fixed point
    /// Also can return the angle as a unitvecotor
    /// </summary>
    public class MouseRelativePoint
    {
        private Vector2 _previousPosition;
        private Vector2 _currentPosition;
        private Vector2 _currentRelativeVector;
        private Vector2 _previousRelativeVector;

        private Vector2 _selectedTerminal;
        private Vector2 _previousSelectedTerminal;

        private float _currentAngle;


        public double ViewingAngle { get; private set; }

        public MouseRelativePoint(Point startPosition)
        {
            this._currentPosition = startPosition.ToVector2();
        }

        public void Update(GameTime gameTime, float delta)
        {
            if (_selectedTerminal != _previousSelectedTerminal || _currentPosition!=_previousPosition)
            {
                _currentRelativeVector = Vector2.Subtract(_selectedTerminal, _currentPosition);
                var radians = Math.Atan2(_currentRelativeVector.Y, _currentRelativeVector.X);
                
                this._currentAngle = (float)(radians * (180 / 3.14159));

                _previousPosition = _currentPosition;
                _previousSelectedTerminal = _selectedTerminal;
                _previousRelativeVector = _currentRelativeVector;
            }
        }
        /// <summary>
        /// Returns the set vector relative from the current position of the point.
        /// </summary>
        /// <returns></returns>
        public Vector2 GetVector() => _currentRelativeVector;
        // Why do I have to add a full turn?
        public float GetAngle() => _currentAngle + (_currentAngle<=-90?360+90:90) ;

        public void SetPosition(Vector2 newPosition)
        {
            if (newPosition != _currentPosition)
            {
                _previousPosition = _currentPosition;
                _currentPosition = newPosition;
            }
        }

        public void SetTerminal(Vector2 terminus)
        {
            if (_selectedTerminal != terminus)
            {
                _previousSelectedTerminal = _selectedTerminal;
                _selectedTerminal = terminus;
            }
        }
    }
}
