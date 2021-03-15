using GameData.CharacterActions;
using GameLibrary.Animation;
using GameLibrary.AppObjects;
using GameLibrary.Extensions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace AnimationAgain.Character
{
    class BasicCharacterHead : IBasicMotion
    {
        private readonly SpriteBatch spriteBatch;
        private readonly Dictionary<string, AnimationFramesCollection> framesets;
        private readonly AnimationPlayer animation;
        private string _currentAnimationIndex { get; set; }

        private readonly MouseRelativePoint mouseInfo;
        private readonly IVelocinator velos;
        private readonly Texture2D atlas;
        private Vector2 currentPosition;
        private Vector2 previousPosition;

        private float currentFacingAngle;
        private float previousFacingAngle;

        private Vector2 currentMouseTerminal;
        private Vector2 previousMouseTerminal;
        private float mouseActiveTimer = 0f;

        /// <summary>
        ///  Switch to determine if the mouse has been moved recently.
        ///  If the Terminal position has not changed in (say 2 seconds) disable the mouse
        /// </summary>
        private bool mouseActive;

        public Vector2 CurrentPosition { get => this.currentPosition; }
        public Rectangle CurrentRectangle { get => this.animation.CurrentFrame(); }

        public BasicCharacterHead(SpriteBatch spriteBatch, Dictionary<string, AnimationFramesCollection> framesets, AnimationPlayer player, MouseRelativePoint mouseInfo, IVelocinator velos, Texture2D atlas, Vector2 startPos)
        {
            this.spriteBatch = spriteBatch;
            this.framesets = framesets;
            this.animation = player;
            this.mouseInfo = mouseInfo;
            this.mouseInfo.SetPosition(this.currentPosition);
            this.velos = velos;
            this.atlas = atlas;
            this.currentPosition = startPos;
            this.previousPosition = startPos;

            mouseActive = false;
        }

        public void SetPosition(Vector2 newPosition)
        {
            if (currentPosition != newPosition)
            {
                previousPosition = currentPosition;
                this.currentPosition = newPosition;
            }
        }

        public void Update(float deltaTime)
        {
            this.currentPosition = currentPosition.AddX(velos.VelocityX * deltaTime)
                                                  .AddY(velos.VelocityY * deltaTime);

            this.mouseInfo.SetPosition(this.currentPosition);

            var angle = this.mouseInfo.GetAngle();
            UpdateMouseActive(deltaTime);
            UpdateAnimation(deltaTime, angle);
        }

        private void UpdateMouseActive(float deltaTime)
        {
            var currentTerminal = this.mouseInfo.GetTerminal();

            TimedAction(() => currentTerminal == currentMouseTerminal, ref this.mouseActiveTimer, deltaTime, () =>
             {
                 currentMouseTerminal = currentTerminal;
                 mouseActiveTimer = 0f;
                 mouseActive = true;
             });
            if (mouseActiveTimer > 3f)
                this.mouseActive = false;

        }
        // General way of managing timed predicates in update methods.
        // Maybe more trouble than I want.
        private void TimedAction(Func<bool> predicate, ref float timervariable, float deltatime, Action predicateFalse)
        {
            if (predicate())
                timervariable += deltatime;
            else
                predicateFalse();
        }

        public void UpdateAnimation(float deltaTime, float headAngle)
        {
            if (headAngle != currentFacingAngle)
            {
                previousFacingAngle = currentFacingAngle;
                currentFacingAngle = headAngle;
                mouseActive = true;

                var facingHead = headAngle switch
                {
                    var f when f > 330 && f <= 360 || f >= 0f && f <= 30f => "FaceUp",
                    var f when f > 30f && f <= 60 => "FaceRightUp",
                    var f when f > 60f && f <= 90 => "FaceRight",

                    var f when f > 300 && f <= 330 => "FaceLeftUp",
                    var f when f >= 270 && f <= 330 => "FaceLeft",
                    _ => this.animation.CurrentSetName()
                };
                this.SetAnimation(facingHead);

            }
            // if we are not moving, and the mouse has not shifted in a while
            // activate the standing animations+69
            if ((velos.VelocityX < 1 && velos.VelocityX > -1) && (velos.VelocityY < 1 && velos.VelocityY > -1) && !mouseActive)
            {
                if (this.animation.CurrentSetName().Contains("Left"))
                    this.SetAnimation("BobLeft");
                else
                    this.SetAnimation("BobRight");
            }



            animation.Update(deltaTime);
        }

        public void Draw(GameTime gameTime)
        {
            spriteBatch.Draw(this.atlas, this.currentPosition, this.animation.CurrentFrame(), Color.White);
        }

        private void SetAnimation(string animationSet)
        {
            _currentAnimationIndex = animationSet;
            this.animation.SetFrames(this.framesets[animationSet]);
        }
        #region IBasicMovie
        public void MoveLeft()
        {
            SetAnimation("FaceLeft");
        }

        public void MoveRight()
        {
            SetAnimation("FaceRight");
        }

        public void MoveUp()
        {
            if (this.velos.VelocityX < 1f)
                SetAnimation("FaceLeft");
            else
                SetAnimation("FaceRight");
        }

        public void MoveDown()
        {
            if (this.velos.VelocityX < 1f)
                SetAnimation("FaceLeft");
            else
                SetAnimation("FaceRight");

        }

        public void EndMoveLeft()
        {
            //SetAnimation("BobLeft");
        }

        public void EndMoveRight()
        {
            //SetAnimation("BobRight");
        }

        public void EndMoveDown()
        {
            // SetAnimation("BobLeft");

        }

        public void EndMoveUp()
        {
            //  SetAnimation("BobRight");
        }

        public void Jump()
        {
            //
        }
        #endregion
    }   
}
