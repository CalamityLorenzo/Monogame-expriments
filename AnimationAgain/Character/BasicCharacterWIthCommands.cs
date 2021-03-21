using GameData.CharacterActions;
using GameLibrary.Animation;
using GameLibrary.AppObjects;
using GameLibrary.Extensions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace AnimationAgain.Character
{
    class BasicCharacterWithCommands : IBasicMotion
    {
        private string _currentAnimationIndex;
        private readonly SpriteBatch spritebatch;
        private readonly Dictionary<string, AnimationFramesCollection> frameSets;
        private readonly AnimationPlayer animPlayer;
        private readonly IVelocinator velos;
        private readonly Texture2D atlas;
        //private readonly List<IAnimationHost> animation;
        private Vector2 currentPos;  // toppest of topmost left
        private readonly float speedX;
        private readonly float speedY;

        private bool isJumping = false;
        private float realtiveJumpHeight = 40f;
        private float jumpSpeed = 60f;
        private float jumpCutOff = 0f; // When we start a jump, if we 'fall' back to this position the jump is complete.

        public Vector2 CurrentPosition { get => this.currentPos; }

        public BasicCharacterWithCommands(SpriteBatch spritebatch, Dictionary<string, AnimationFramesCollection> frameSets, AnimationPlayer animPlayer, IVelocinator velos, Texture2D atlas, Vector2 startPos, float speedX, float speedY)
        {
            this._currentAnimationIndex = "Standing";  // Standing
            this.spritebatch = spritebatch;
            this.frameSets = frameSets;
            this.animPlayer = animPlayer;
            this.velos = velos;
            this.atlas = atlas;
            this.currentPos = startPos;
            this.speedX = speedX;
            this.speedY = speedY;
            // Where we draw the in relative position to the rest of the body.
            animPlayer.SetFrames(frameSets[_currentAnimationIndex]);
        }


        public void Update(float deltaTime)
        {

            // This applies a value to the current position.
            this.currentPos = currentPos.Add(velos.VelocityX * deltaTime, velos.VelocityY * deltaTime);
            //this.currentPos = Vector2.Multiply(currentPos, deltaTime);
            this.ManageJump();
            this.animPlayer.Update(deltaTime);
            this.UpdateAnimationState();
        }

        /// <summary>
        /// This basically resets the animation status for whennothing is happening.
        /// </summary>
        private void UpdateAnimationState()
        {

            if ((velos.VelocityX < 1 && velos.VelocityX > -1) && (velos.VelocityY < 1 && velos.VelocityY > -1))
            {
                this.SetAnimation("Standing");
                //animPlayer.SetFrames(frameSets.Single(a => a.Key == "Standing"));
            }
        }

        public void Draw(GameTime time)
        {
            // Body
            spritebatch.Draw(atlas, this.currentPos, animPlayer.CurrentFrame(), Color.White);
        }

        private void SetAnimation(string animationSet)
        {
            _currentAnimationIndex = animationSet;
            this.animPlayer.SetFrames(this.frameSets[animationSet]);
        }

        public void MoveLeft()
        {
            this.velos.SetVelocityX(-this.speedX);
            if (!this.isJumping)
            {
                SetAnimation("Left");
            }
        }

        public void MoveRight()
        {
            this.velos.SetVelocityX(this.speedX);
            if (!this.isJumping)
            {
                SetAnimation("Right");
            }
        }

        public void MoveUp()
        {
            this.velos.SetVelocityY(-this.speedY);
            //SetAnimation(0);
            if (_currentAnimationIndex == "Left" || _currentAnimationIndex == "Right")
                SetAnimation(_currentAnimationIndex);
            else
                SetAnimation("Left");

        }

        public void MoveDown()
        {
            this.velos.SetVelocityY(this.speedY);
            if (_currentAnimationIndex == "Left" || _currentAnimationIndex == "Right")
                SetAnimation(_currentAnimationIndex);
            else
                SetAnimation("Right");

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
                this.BeginJump();
        }

        private void BeginJump()
        {
            this.isJumping = true;
            this.jumpCutOff = this.currentPos.Y;
            var anim = "JumpRight";
            // What direction are we facing?
            if (this._currentAnimationIndex == "Left")
                anim = "JumpLeft";
            this.SetAnimation(anim);
            this.velos.SetVelocityY(-this.jumpSpeed); //Launch into SPAAAAAACE
        }

        private void ManageJump()
        {
            if (this.isJumping)
            {

                var anim = "JumpRight";
                // What direction are we facing?
                if (this._currentAnimationIndex == "JumpLeft")
                    anim = "JumpLeft";
                else if (this.velos.VelocityX > 0)
                {
                    anim = "JumpLeft";
                }
                if (this.velos.VelocityX < 0)
                {
                    anim = "JumpRight";
                }
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
                    var subAnim = "Standing";
                    if (this.velos.VelocityX > 0)
                    {
                        subAnim = "Right";
                    }
                    if (this.velos.VelocityX < 0)
                    {
                        subAnim = "Left";
                    }

                    this.SetAnimation(subAnim);
                }
            }
        }
    }

}
