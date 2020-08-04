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
    internal class AnimationPhraseHost : IAnimationHost
    {
        private int _currentFrame;
        private float timeSinceFrameUpdated;

        private bool _paused = false;
        private AnimationPhrase _phrase;

        public AnimationPhraseHost(AnimationPhrase phrase)
        {
            this._phrase = phrase;
        }

        public void Update(float deltaTime)
        {
            if (!_paused)
            {
                // Between frames we need to calcuate the time taken
                this.timeSinceFrameUpdated += deltaTime;
                // Are we ready to change frames
                if (_currentFrame != -1 && timeSinceFrameUpdated > this._phrase.Frames[_currentFrame].LengthOfFrame)
                {
                    if (_currentFrame < this._phrase.Frames.Count)
                        _currentFrame += 1;
                    else if (this._phrase.IsRepeating)
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

        public bool IsRepeating => this._phrase.IsRepeating;

        public Rectangle CurrentFrame() => this._currentFrame > -1 ? this._phrase.Frames[_currentFrame].Frame : Rectangle.Empty;
    }
}
