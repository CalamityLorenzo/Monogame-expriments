using Microsoft.Xna.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace GameLibrary.Animation
{
    /// <summary>
    /// Runs a set of other IAnimationHost objects
    /// Be they phrases or other sets. This is composition??
    /// A host of phrases. An entire character is to be host in a set.
    /// Assume the
    /// </summary>
    public class AnimationSet : IAnimationHost, IEnumerable<IAnimationHost>
    {
        private readonly IList<IAnimationHost> animations;
        private readonly float timebetweenFrames;

        public bool IsRepeating => this._currentAnimation.IsRepeating;

        private IAnimationHost _currentAnimation;

        public AnimationSet(IList<AnimationPhrase> animations, float timebetweenFrames)
        {
            this.animations = animations.Select(o => {o.SetFrameLength(timebetweenFrames); return (IAnimationHost)o; }).ToList();
            this.timebetweenFrames = timebetweenFrames;
            this._currentAnimation = animations[0];
        }

        public Rectangle CurrentFrame()
        {
            throw new NotImplementedException();
        }

        public void Update(float deltaTime)
        {
            this._currentAnimation.Update(deltaTime);
        }

        public int CurrentFrameIndex()
        {
            return this._currentAnimation.CurrentFrameIndex();
        }

        public void SetFrameLength(float frameLength)
        {
            foreach (var itm in this.animations)
                itm.SetFrameLength(frameLength);
        }

        public void Start(int? frameId=null)
        {
            this._currentAnimation.Start(frameId);
        }

        public void Stop()
        {
            this._currentAnimation.Stop();
        }

        public IAnimationHost this [int index]
        {
            get { this.SelectAnimation(index); return _currentAnimation; }
            set => this.SelectAnimation(index);
        }

        private void SelectAnimation(int index)
        {
            this.Stop();
            this._currentAnimation = animations[index];
            this.Start();
        }

        public IEnumerator<IAnimationHost> GetEnumerator()
        {
            return this.animations.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
