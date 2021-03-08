using GameData.CharacterActions;
using GameLibrary.Animation;
using GameLibrary.AppObjects;
using GameLibrary.Extensions;
using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MovingManAnimation.Character
{
    class BasicCharacterWithCommands : IBasicMotion
    {
        private int _currentAnimationIndex;
        private readonly SpriteBatch spritebatch;
        private readonly Texture2D atlas;
        private readonly IVelocinator velos;
        //private readonly List<IAnimationHost> animation;
        private readonly AnimationSet animation;
        private IAnimationHost currentAnimation;
        private IAnimationHost currentHeadAnimation;
        private Vector2 currentPos;  // toppest of topmost left
        private Vector2 headOffset;
        private readonly float speedX;
        private readonly float speedY;

        private bool isJumping = false;
        private float realtiveJumpHeight = 40f;
        private float jumpSpeed = 60f;
        private float jumpCutOff = 0f; // When we start a jump, if we 'fall' back to this position the jump is complete.

        public BasicCharacterWithCommands(SpriteBatch spritebatch, Texture2D atlas, AnimationSet animation, IVelocinator velos, Vector2 startPos, float speedX, float speedY)
        {
            this._currentAnimationIndex = -1;  // Standing
            this.spritebatch = spritebatch;
            this.atlas = atlas;
            this.velos = velos;
            this.animation = animation;
            this.currentAnimation = this.animation[0];
            this.currentHeadAnimation = this.animation[7];
            this.currentPos = startPos;
            this.speedX = speedX;
            this.speedY = speedY;
            // Where we draw the in relative position to the rest of the body.
            this.headOffset = new Vector2(-5, -42);
        }


        public void Update(float deltaTime)
        {
            this.currentPos = currentPos.AddX(velos.VelocityX * deltaTime)
                        .AddY(velos.VelocityY * deltaTime);
            this.ManageJump();
            this.UpdateAnimationState();
            this.currentAnimation.Update(deltaTime);
            this.currentHeadAnimation.Update(deltaTime);
        }

        private void UpdateAnimationState()
        {

            if ((velos.VelocityX < 1 && velos.VelocityX > -1) && (velos.VelocityY < 1 && velos.VelocityY > -1))
            {
                var animIndex = 4;
                if (animIndex != _currentAnimationIndex)
                {
                    _currentAnimationIndex = animIndex;
                    this.currentAnimation = this.animation[4];
                }
            }
        }

        public void Draw(GameTime time)
        {
            // Body
            spritebatch.Draw(atlas, this.currentPos, currentAnimation.CurrentFrame(), Color.White);
            // head
            spritebatch.Draw(atlas,  Vector2.Add(this.headOffset, this.currentPos), currentHeadAnimation.CurrentFrame(), Color.White);

        }

        private void SetAnimation(int animationIdx)
        {
            if (animationIdx != _currentAnimationIndex)
            {
                _currentAnimationIndex = animationIdx;
                this.currentAnimation = this.animation[_currentAnimationIndex];
            } 
        }

        public void MoveLeft()
        {
            this.velos.SetVelocityX(-this.speedX);
            if (!this.isJumping)
            {
                var animationIdx = 1;
                SetAnimation(animationIdx);
            }

        }

        public void MoveRight()
        {
            this.velos.SetVelocityX(this.speedX);
            if (!this.isJumping)
            {
                var animationIdx = 0;
                SetAnimation(animationIdx);
            }
        }

        public void MoveUp()
        {
            this.velos.SetVelocityY(-this.speedY);
                SetAnimation(0);
            if (_currentAnimationIndex == 1) ;
        }

        public void MoveDown()
        {
            this.velos.SetVelocityY(this.speedY);
                SetAnimation(1);
            if (_currentAnimationIndex == 1) ;

        }

        public void EndMoveLeft()
        {
            if (this.velos.VelocityX < 0)
                this.velos.SetVelocityX(0f);
        }

        public void EndMoveRight()
        {
            if (this.velos.VelocityX > 0)
                this.velos.SetVelocityX(0f);
        }

        public void EndMoveDown()
        {
            if (this.velos.VelocityY > 0)
                this.velos.SetVelocityY(0f);
        }

        public void EndMoveUp()
        {
            if (this.velos.VelocityY < 0)
                this.velos.SetVelocityY(0f);
        }

        public void Jump()
        {
            if (!this.isJumping)
            {
                this.BeginJump();
            }
        }

        private void BeginJump()
        {
            this.isJumping = true;
            this.jumpCutOff = this.currentPos.Y;
            var anim = 4;
            // What direction are we facing?
            if (this._currentAnimationIndex == 0)
                anim = 5;
            else if (this.velos.VelocityX > 0)
            {
                anim = 6;
            }
            if (this.velos.VelocityX < 0)
            {
                anim = 5;
            }

            this.SetAnimation(anim);
            this.velos.SetVelocityY(-this.jumpSpeed); //Launch into SPAAAAAACE
        }

        private void ManageJump()
        {
            if (this.isJumping)
            {

                var anim = 0;
                // What direction are we facing?
                if (this._currentAnimationIndex == 4)
                    anim = 6;
                else if (this.velos.VelocityX > 0)
                {
                    anim = 6;
                }
                if (this.velos.VelocityX < 0)
                {
                    anim = 7;
                }
                if(anim!=0) 
                this.SetAnimation(anim);


                // DId we get high enough.
                if (currentPos.Y <= jumpCutOff - realtiveJumpHeight)
                {
                    this.velos.SetVelocityY(+this.jumpSpeed);
                }
                else if (currentPos.Y >= this.jumpCutOff)  // did we return back to the start
                {
                    this.isJumping = false;
                    this.velos.SetVelocityY(0f);
                    // change the animation to somwthing more appropriate
                    var subAnim = 0;
                    if (this.velos.VelocityX > 0)
                    {
                        subAnim = 2;
                    }
                    if (this.velos.VelocityX < 0)
                    {
                        subAnim = 3;
                    }
                    
                    if (subAnim != 0)
                        this.SetAnimation(subAnim);
                }
            }
        }
    }
}
