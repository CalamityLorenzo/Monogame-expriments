using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;

namespace AnimationAgain.Animation
{
    class AnimationPlayer
    {
        private readonly float defaultTimebetweenframes;
        private float velocity = 1f;
        private AnimationFramesCollection currentSet;
        private Rectangle[] currentFrames;
        private Rectangle currentFrame;
        private int currentTotalFrames;
        private int currentFrameIdx;
        private float timeSinceFrameUpdated;
        private bool paused;
        private Dictionary<string, AnimationFramesCollection> completeFrameset;

        public AnimationPlayer(float defaultTimebetweenframes)
        {
            this.defaultTimebetweenframes = defaultTimebetweenframes;
            paused = false;
            currentTotalFrames = -1;
            currentFrameIdx = 0;
            currentSet = AnimationFramesCollection.Empty;
        }

        public AnimationPlayer(float defaultTimeBetweenFrames, Dictionary<string, AnimationFramesCollection> frameSets) : this(defaultTimeBetweenFrames)
        {
            this.completeFrameset = new Dictionary<string, AnimationFramesCollection>(frameSets);
            if (this.completeFrameset.Count > 0) { this.SetFrames(this.completeFrameset.First().Value); }
        }

        public void Update(float deltaTime)
        {
            if (!paused)
            {
                this.timeSinceFrameUpdated += deltaTime;
                // Are we ready to change frames 
                if (currentTotalFrames != -1 && timeSinceFrameUpdated > (velocity * defaultTimebetweenframes))
                {
                    if (currentFrameIdx < this.currentTotalFrames - 1)
                        currentFrameIdx += 1;
                    else if (currentFrameIdx >= this.currentTotalFrames - 1 && this.currentSet.IsRepeating)
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
            return currentSet.Name;
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

        public void SetFrames(AnimationFramesCollection frameSet)
        {
            if (this.currentSet.Name != frameSet.Name)
            {
                this.currentSet = frameSet;
                currentFrames = new Rectangle[frameSet.Frames.Length];
                frameSet.Frames.CopyTo(currentFrames, 0);
                this.currentTotalFrames = this.currentFrames.Length;
                // reset the counter (so we don't looklike we are skipping frames early on)
                // this.timeSinceFrameUpdated = 0f;
                // move back to the begining of the frameset.
                this.currentFrameIdx = this.currentSet.StartFrame;
                SetCurrentFrame();
            }
        }

        public void SetFrames(string framesetName)
        {
            this.SetFrames(this.completeFrameset[framesetName]);
        }
    }
}
