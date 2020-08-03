using Microsoft.Xna.Framework;
using SharpDX.Direct2D1.Effects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing.Design;
using System.Text;

namespace BasicJeep.BasicAnimation
{
    internal class AnimationPhrase : IAnimationHost
    {
        private readonly IList<AnimationFrame> frames;
        private readonly bool isRepeating;
        private int _currentFrame;
        private float timeSinceFrameUpdated;

        private bool _paused = false;

        public AnimationPhrase(IEnumerable<AnimationFrame> frames, bool isRepeating, int startFrame = 0)
        {
            this.frames = new List<AnimationFrame>(frames);
            this.isRepeating = isRepeating;
            this._currentFrame = startFrame;
        }

        public void Update(float deltaTime)
        {
            if (!_paused)
            {
                // Between frames we need to calcuate the time taken
                this.timeSinceFrameUpdated += deltaTime;
                // Are we ready to change frames
                if (_currentFrame != -1 && timeSinceFrameUpdated > frames[_currentFrame].LengthOfFrame)
                {
                    if (_currentFrame < frames.Count)
                        _currentFrame += 1;
                    else if (isRepeating)
                        _currentFrame = 0;
                    // either way reset the counter.
                    this.timeSinceFrameUpdated = deltaTime;
                }
            }
        }

        public void Stop()
        {
            this._paused = true;
        }

        public void Start(int? frameId)
        {
            if (frameId.HasValue)
                this._currentFrame = frameId.Value;
            this._paused = false;

        }

        public int CurrentFrameIndex() => this._currentFrame;

        public bool IsRepeating => this.isRepeating;

        public Rectangle CurrentFrame() => this._currentFrame > -1 ? this.frames[_currentFrame].Frame : Rectangle.Empty;
    }
}
