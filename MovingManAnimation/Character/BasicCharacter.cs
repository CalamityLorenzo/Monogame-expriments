using GameLibrary.Animation;
using GameLibrary.AppObjects;
using GameLibrary.Extensions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;


namespace MovingManAnimation.Character
{
    internal class BasicCharacter
    {
        private int _currentAnimationIndex;
        private readonly SpriteBatch spritebatch;
        private readonly Texture2D atlas;
        private readonly IVelocinator velos;
        //private readonly List<IAnimationHost> animation;
        private readonly AnimationSet animation;
        private IAnimationHost currentAnimation;
        private Vector2 currentPos;

        public BasicCharacter(SpriteBatch spritebatch, Texture2D atlas, AnimationSet animation, IVelocinator velos, Vector2 startPos)
        {
            this._currentAnimationIndex = -1;  // Standing
            this.spritebatch = spritebatch;
            this.atlas = atlas;
            this.velos = velos;
            this.animation = animation;
            this.currentAnimation = this.animation[0]; 
            this.currentPos = startPos;
        }

        public void Update(float deltaTime)
        {
            this.currentPos = currentPos.AddX(velos.VelocityX * deltaTime)
                        .AddY(velos.VelocityY * deltaTime);
            this.UpdateAnimation();
            this.currentAnimation.Update(deltaTime);
        }

        private void UpdateAnimation()
        {
            var animationIdx = 0;
            // Left
            if (velos.VelocityX > 0) animationIdx = 2;
            // Right
            if (velos.VelocityX < 0) animationIdx = 3;
            // Down
            if (velos.VelocityY > 0) animationIdx = 1;
            // Up
            if (velos.VelocityY < 0) animationIdx = 0;

            if ((velos.VelocityX < 1 && velos.VelocityX > -1) && (velos.VelocityY < 1 && velos.VelocityY > -1)) animationIdx = 4;

            if (animationIdx != _currentAnimationIndex){
                _currentAnimationIndex = animationIdx;
                this.currentAnimation = this.animation[_currentAnimationIndex];
            }
        }

        

        public void Draw(GameTime time)
        {
            spritebatch.Draw(atlas, this.currentPos, currentAnimation.CurrentFrame(), Color.White);
        }
    }
}
