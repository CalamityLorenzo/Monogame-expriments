using GameLibrary.Animation;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;

namespace Library.Animation
{

    // Given a set of frames and a series of timings.
    // Calculate and return the next frame.
    [Obsolete]
    public class OldBlockAnimationObject
    {
        private readonly Rectangle[] frames;
        private readonly float[] timings;

        private readonly int[] _timingsIdx; // When we have an unequal number of timings vs frames (must be more frames) use this to look the current timing index.
        private readonly bool isRepeating;

        // state management
        private bool _isStarted = false;

        public void Start()
        {
            this._isStarted = true; 
        }

        private int _currentFrame = 0;
        private float _frameDelta = 0f; // Accumlator for between frame updates. ONce this cross the threshold.

        public OldBlockAnimationObject(Rectangle[] frames, float[] timings, bool isRepeating)
        {
            this.frames = frames;
            this.timings = timings;
            this.isRepeating = isRepeating;
            // When we have unquel amounts of timings, and frames.
            if(timings.Length != frames.Length)
            {
                _timingsIdx = new int[frames.Length];
                for(var x = 0; x < frames.Length;++x)
                {
                    _timingsIdx[x] = x % timings.Length;
                }
            }

        }

        public void Update(GameTime time, float deltaTime)
        {
            if(_isStarted)
            {
                _frameDelta += deltaTime;
                float timing  = getCurrentTiming(_currentFrame);

                if (_frameDelta > timing) {
                    _currentFrame += 1;
                    _frameDelta = 0f;
                }
                // reset if we can.
                if (_currentFrame > frames.Length - 1 && isRepeating) 
                    _currentFrame = 0;
            }
        }

        private float getCurrentTiming(int currentFrame)
        {
            // Timings, and frames don;t have to match up.
            // eg..8 walking frames, make actually be 1 timing. so just return that.
            // or 5 frames 3 timings for some interesting interactivity.
            if(this.timings.Length ==1 )  return this.timings[0];

            if (timings.Length != frames.Length) { return timings[_timingsIdx[currentFrame]]; }

            else return timings[currentFrame];

        }

        public void CancelAnimation()
        {
            this._isStarted = false;
            _currentFrame = 0;
        }

        public Rectangle CurrentFrame => frames[_currentFrame];
    }
}
