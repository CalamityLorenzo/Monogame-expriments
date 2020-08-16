using GameLibrary.Animation;
using Microsoft.Xna.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace GameLibrary.Animation
{
    /// <summary>
    /// Runs a set of other IAnimationHost objects
    /// Be they phrases or other sets. This is composition??
    /// Assume the
    /// </summary>
    public class AnimationSet : IAnimationHost, IEnumerable<IAnimationHost>
    {
        private readonly IList<IAnimationHost> animations;

        public bool IsRepeating { get; }

        private IAnimationHost _currentAnimation;

        public AnimationSet(IList<IAnimationHost> animations, bool repeating)
        {
            this.animations = animations;
            this.IsRepeating = repeating;
            this._currentAnimation = animations[0];
        }

        public Rectangle CurrentFrame()
        {
            throw new NotImplementedException();
        }

        public void Update(float deltaTime)
        {
            throw new NotImplementedException();
        }

        public int CurrentFrameIndex()
        {
            throw new NotImplementedException();
        }

        public void SetFrameLength(float frameLength)
        {
            throw new NotImplementedException();
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
            get => null;
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
