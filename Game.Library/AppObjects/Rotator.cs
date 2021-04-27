﻿using GameLibrary.Interfaces;
using System;

namespace GameLibrary.AppObjects
{
    // Which direction we are rotating in 
    public enum RotatorState
    {
        Unknown = 0,
        Diesal,
        Widdershins,
        Stopped
    }

    // Allows you to rotate a set amount degrees per second
    public class Rotator : IUpdateableGameObject
    {

        private float PreviousAngle { get; set; }
        public float RatePerSecond { get; private set; }
        public RotatorState State { get; private set; }
        public float DestinationAngle { get; private set; }
        public float CurrentAngle { get; private set; }

        public Rotator(int startAngle, float anglesPerSecond)
        {
            CurrentAngle = startAngle;
            this.PreviousAngle = CurrentAngle;
            this.DestinationAngle = startAngle;
            RatePerSecond = anglesPerSecond;
            this.State = RotatorState.Unknown;
        }

        public void UpdateRate(float anglesPerSecond)
        {
            RatePerSecond = anglesPerSecond;
        }

        public void SetDestinationAngle(float angleToSet)
        {
            this.DestinationAngle = angleToSet % 360;
            if (DestinationAngle == CurrentAngle) return;
            // Now set the Wsdirect we need to go.
            if (this.DestinationAngle > CurrentAngle)
            {
                if (DestinationAngle - CurrentAngle > 179f)
                {
                    this.State = RotatorState.Widdershins;
                }
                else
                {
                    this.State = RotatorState.Diesal;
                }
            }
            else
            {
                if (CurrentAngle - DestinationAngle > 179f)
                    this.State = RotatorState.Diesal;
                else
                {
                    this.State = RotatorState.Widdershins;

                }
            }
        }

        public void SetState(RotatorState state)
        {
            this.State = state;
        }

        public void Right()
        {
            this.DestinationAngle = -1f;
            this.State = RotatorState.Diesal;
        }

        public void Left()
        {
            this.DestinationAngle = -1f;
            this.State = RotatorState.Widdershins;
        }


        // Basically when you lift a finger it stops
        public void StopRotation()
        {
            this.State = RotatorState.Stopped;
            this.PreviousAngle = CurrentAngle;
        }

        public void Update(float delta)
        {
            // delta time since last update
            // are we stopped or moving?
            if (this.State != RotatorState.Stopped && this.State != RotatorState.Unknown)
            {
                UpdatePosition(delta);
                if (DestinationAngle<0f==false && IsAngleMatched(this.State, DestinationAngle, CurrentAngle, PreviousAngle))
                {
                    this.State = RotatorState.Stopped;
                    this.CurrentAngle = DestinationAngle;
                }
                this.PreviousAngle = CurrentAngle;
            }
        }

        private bool IsAngleMatched(RotatorState state, float destinationAngle, float currentAngle, float previousAngle)
        {
            // Degenerate case 
            if (destinationAngle == currentAngle)
                return true;

            // COs we are dealing with velocities
            // we can miss our angle, so we need to check a range.
            // Howver it's not a simple number line, but a clock. so caution is required,
            if (state != RotatorState.Diesal && state != RotatorState.Widdershins)
                throw new Exception("Rotator all out of whack");
            // 1. Get the difference between current and previous update (the direction informs this...Though it's not the end of t
            var angleDistance = (state == RotatorState.Diesal) ? currentAngle - previousAngle : previousAngle - currentAngle;

            // 3. Is Our angle in the range from Current to Current-DistanceSinceLastUpdate.
            var lowerbound = 0f;
            var upperbound = 0f;

            if (state == RotatorState.Diesal)
            {
                lowerbound = currentAngle - angleDistance;
                upperbound = (int)Math.Floor(currentAngle) %360f; // Current angle can read as 360. This may be a bug...Not confident enough to pull it apart.
            }
            else
            {
                lowerbound = (int)Math.Floor(currentAngle); // notice same change not applied as above.
                upperbound = (angleDistance > 0f) ? currentAngle + angleDistance : 360f + angleDistance;
            }

            // if lower is greater than upper
            // we have widdershined/Clockwised around the clock. So we are calculating from the previous/next loop around.
            // or more numbersie we are using the strict numberline and not the modulo.
            // This is an odd way of managing state. We only know we've gone/goingthe clock if the one of the numbers are outside the range
            if (lowerbound > upperbound && state==RotatorState.Widdershins)
            {
                lowerbound = lowerbound - 360f;
            }

        //    if (lowerbound < 0f) lowerbound += 360f;
            // Finally is our destination angle between the lower/upperbound
            return (lowerbound <= destinationAngle && upperbound >= destinationAngle);
            
        }

        private void UpdatePosition(float delta)
        {
            switch (this.State)
            {
                case RotatorState.Diesal:
                    this.CurrentAngle = (CurrentAngle + (RatePerSecond * delta)) % 360f;
                    break;
                case RotatorState.Widdershins:
                    this.CurrentAngle = (CurrentAngle - (RatePerSecond * delta)) % 360f;
                    break;
                case RotatorState.Stopped:
                case RotatorState.Unknown:
                    this.CurrentAngle = CurrentAngle;
                    break;
            }

            if (this.CurrentAngle < 0)
            {
                // We add this becuase Current angle is a negative (We rolled back across the boundary to previous clock).
                // and require the modulo version of the angle.((Counterclockwise from 0)
                this.CurrentAngle = 360f + CurrentAngle;
            }
        }
    }
}
