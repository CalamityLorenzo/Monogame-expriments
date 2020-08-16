using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameLibrary.Animation
{
    /// <summary>
    /// Runs the included set of frames.
    /// </summary>
    public class AnimationPhrase : IAnimationHost
    {
        private List<AnimationFrames> _allFrames;
        private int _currentFrameSet;
        /// <summary>
        /// Where we are in relation to the total number of frames acrss the collection
        /// </summary>
        private int _currentFrameTotal;
        /// <summary>
        /// The total number f frames across the collection
        /// </summary>
        private int _totalFrameCount;
        /// <summary>
        /// Frame position in the currently selected AnimationFrame _allFrames[_currentFrameSet].Frames[_curretFrmameLocalidx]
        /// </summary>
        private int _currentFrameLocalIdx;
        private float timeSinceFrameUpdated;

        private bool _paused = false;
        private float timeBetweenframes;
        private bool _isRepeating;

        public AnimationPhrase(IEnumerable<AnimationFrames> frames, float timeBetweenframes, bool isRepeating = true)
        {
            this._allFrames = new List<AnimationFrames>(frames);
            this._totalFrameCount = _allFrames.Select(o => o.Frames).Sum(o => o.Count);
            _currentFrameSet = 0;
            _currentFrameTotal = -1;
            _currentFrameLocalIdx = 0;
            this.timeBetweenframes = timeBetweenframes;
            this._isRepeating = isRepeating;
        }

        public void Update(float deltaTime)
        {
            if (!_paused)
            {
                // Between frames we need to calcuate the time taken
                this.timeSinceFrameUpdated += deltaTime;

                var currentFrames = this._allFrames[_currentFrameSet];
                // Are we ready to change frames 
                if (_currentFrameTotal != -1 && timeSinceFrameUpdated > (currentFrames.Frames[_currentFrameLocalIdx].LengthOfFrameMultiplier * timeBetweenframes))
                {
                    if (_currentFrameTotal < this._totalFrameCount)
                    {
                        _currentFrameTotal += 1;
                        IncrementFrame(currentFrames);
                    }
                    else if (this.IsRepeating)
                    {
                        _currentFrameTotal = 0;
                        IncrementFrame(currentFrames);
                    }
                    // either way reset the counter.
                    this.timeSinceFrameUpdated = deltaTime;
                }
            }
        }

        private void IncrementFrame(AnimationFrames currentSelection)
        {
            var totalFrames = currentSelection.Frames.Count-1;
            if (_currentFrameLocalIdx < totalFrames)
                _currentFrameLocalIdx += 1;
            else
            {
                // We need to move up to the next list if possible.
                if (_currentFrameSet < _allFrames.Count - 1)
                {
                    _currentFrameSet += 1;
                    _currentFrameLocalIdx += 0;
                }
                else if (this.IsRepeating)
                {
                    _currentFrameSet = 0;
                    _currentFrameLocalIdx = 0;
                }
            }
        }

        public void Stop()
        {
            this._paused = true;
            this._currentFrameLocalIdx = 0;
            this._currentFrameTotal = 0;
            this._currentFrameSet = 0;
        }

        public void Start(int? frameId)
        {
            if (frameId.HasValue)
            {
                this._currentFrameTotal = frameId.Value;
                SetLocalFramePosition(_currentFrameTotal);
            }

            if (_currentFrameTotal == -1) _currentFrameTotal = 0;
            this._paused = false;
        }

        private void SetLocalFramePosition(int targetFrame)
        {
            // take the target value and turn into to a co-rdinate
            // update the local positions in the framesets, and positions (or rows/columns)
            var keepGoing = true;
            var grpIdx = 0;
            var acc = 0;
            while (keepGoing)
            {
                var framesCount = _allFrames[grpIdx].Frames.Count;
                acc += framesCount;
                // Here we have the correct grpIdx __currentFrameSet, now refine to find the correct column(frmae)
                // grpIdx, Frame
                if(acc>= targetFrame)
                {
                    _currentFrameSet = grpIdx;
                    _currentFrameLocalIdx = acc - targetFrame;
                    keepGoing = false;
                }
            }
        }

        public int CurrentFrameIndex() => this._currentFrameTotal;

        public bool IsRepeating => this._isRepeating;

        public Rectangle CurrentFrame() => this._currentFrameTotal > -1 ? this._allFrames[_currentFrameSet].Frames[_currentFrameLocalIdx].Frame : Rectangle.Empty;

        public void SetFrameLength(float frameLength)
        {
            this.timeBetweenframes = frameLength;
        }
    }
}
