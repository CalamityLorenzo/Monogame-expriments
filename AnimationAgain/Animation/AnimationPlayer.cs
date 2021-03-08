using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace AnimationAgain.Animation
{
    class AnimationPlayer
    {
        private readonly float defaultTimebetweenframes;
        private float velocity = 1f;
        private string currentSetName;
        private Rectangle[] currentFrames;
        private Rectangle currentFrame;
        private int currentTotalFrames;
        private int currentFrameIdx;
        private float timeSinceFrameUpdated;
        private bool paused;

        public AnimationPlayer(float defaultTimebetweenframes)
        {
            this.defaultTimebetweenframes = defaultTimebetweenframes;
            paused = false;
            currentTotalFrames = -1;
            currentFrameIdx = 0;
        }

        public void Update(float deltaTime)
        {
            if (!paused)
            {
                this.timeSinceFrameUpdated += deltaTime;
                // Are we ready to change frames 
                if (currentTotalFrames != -1 && timeSinceFrameUpdated > (velocity * defaultTimebetweenframes))
                {
                    currentFrameIdx += 1;
                    if (currentFrameIdx >= this.currentTotalFrames)
                    {
                        currentFrameIdx = 0;
                    }
                    this.currentFrame = this.currentFrames[currentFrameIdx];
                    this.timeSinceFrameUpdated = deltaTime;
                }
            }
        }

        // What we render from.
        public Rectangle CurrentFrame()
        {
            return currentFrame;
        }

        public string CurrentSetName()
        {
            return currentSetName;
        }

        public void Start()
        {
            this.paused = false;
        }

        public void Stop()
        {
            this.paused = true;
        }

        internal void SetSpeed(float animSpeed)
        {
            this.velocity = animSpeed;
        }

        private void SetCurrentFrame()
        { 
            this.currentFrame = this.currentFrames[currentFrameIdx];
        }
        public void SetFrames(KeyValuePair<string, Rectangle[]> frameSet)
        {
            if (this.currentSetName != frameSet.Key)
            {
                this.currentSetName = frameSet.Key;
                currentFrames = new Rectangle[frameSet.Value.Length];
                frameSet.Value.CopyTo(currentFrames, 0);
                this.currentTotalFrames = this.currentFrames.Length;
                // reset the counter (so we don't looklike we are skipping frames early on)
                this.timeSinceFrameUpdated = 0f;
                // move back to the begining of the frameset.
                this.currentFrameIdx = 0;
                SetCurrentFrame();
            }
        }
    }
}
