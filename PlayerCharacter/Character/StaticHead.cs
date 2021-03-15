using GameLibrary.AppObjects;
using Microsoft.Xna.Framework;
using SharpDX.Direct3D11;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlayerCharacter.Character
{
    internal class StaticHead
    {
        internal enum StaticHeadState
        {
            Unknown=0,
            Left=1,
            Right=2,
            LeftUp=3,
            Up=4,
            RightUp=5
        }

        private Vector2 _currentPosition;
        private MouseRelativePoint mousePoint;
        private float previousAngle;
        private StaticHeadState _currentState;
        private StaticHeadState _previousState;

        public StaticHead(Vector2 StartPos, StaticHeadState currentDirection, MouseRelativePoint mousePoint)
        {
            this._currentPosition = StartPos;
            this.mousePoint = mousePoint;
            this.mousePoint.SetPosition(StartPos);
            this._currentState = currentDirection;
        }
        // Set the point we are measuring from.
        internal void SetPosition(Vector2 position) => this._currentPosition = position;

        public void Update(GameTime gameTime, float deltaTime)
        {
            //this.mousePoint.Update(gameTime, deltaTime);
            var currentAngle = this.mousePoint.GetAngle();
            this._currentState = UpdateState(currentAngle);
            _previousState = _currentState;
        }

        public StaticHeadState UpdateState(float currentAngle)
        {
            if (this.previousAngle != currentAngle)
            {
               switch (currentAngle)
                {
                    case float s when s >= 0 && s < 30:
                        return StaticHeadState.Up;
                    case float s when s >= 30 && s < 60:
                        return StaticHeadState.RightUp;
                    case float s when s >= 60 && s < 180:
                        return StaticHeadState.Right;
                    case float s when s >= 180 && s < 300:
                        return StaticHeadState.Left;
                    case float s when s >= 300 && s < 330:
                        return StaticHeadState.LeftUp;
                    case float s when s >= 330 && s < 360:
                        return StaticHeadState.Up;
                    default:
                        return StaticHeadState.Left;

                }
            }
            return _previousState;
        }

        public StaticHeadState CurrentDirection() => this._currentState;

        internal void MouseLook(Vector2 mousePosition)
        {
            this.mousePoint.SetTerminal(mousePosition);
        }

        internal void SetDirection(StaticHeadState direction)
        {
            if(_previousState!=direction)
            {
                _previousState = _currentState;
                _currentState = direction;
            }
        }
    }
}
