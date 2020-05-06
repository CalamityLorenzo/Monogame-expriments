﻿using GameLibrary.Extensions;
using Library.Animation;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Text;

namespace InputTests.MovingMan
{
    internal class MovingObjectAnimation : IWalkingMan
    {
        private readonly SpriteBatch spriteBatch;
        private readonly Texture2D walkLeft;
        private readonly Texture2D walkRight;
        private readonly Texture2D standing;
        private Texture2D currentTexture;
        private MovingHead head;
        private readonly WalkingManAnimations animations;
        private Vector2 _currentPos;

        private float velocityX;
        private float velocityY;
        private Texture2D _magiDot;

        public MovingObjectAnimation(SpriteBatch spriteBatch, Texture2D walkLeft, Texture2D walkRight, Texture2D standing, WalkingManAnimations animations, MovingHead head, Vector2 startpos)
        {
            this.spriteBatch = spriteBatch;
            this.walkLeft = walkLeft;
            this.walkRight = walkRight;
            this.standing = standing;
            this.animations = animations;
            this._currentPos = startpos;
            velocityX = 0f;
            velocityY = 0f;
            this.currentTexture = standing;
            this.head = head;
            head.SetViewDestination(_currentPos);
            animations.Standing();
        }

        public Vector2 CurrentPosition => _currentPos;

        public void Update(GameTime gameTime, float deltaTime)
        {
            _currentPos.X += velocityX * deltaTime;
            _currentPos.Y += velocityY * deltaTime;
            if (velocityX == 0f && velocityY == 0f)
                Standing();
            
            this.head.SetOrigin(new Vector2(_currentPos.X + 30, _currentPos.Y));
            this.head.Update(gameTime, deltaTime);
            if (this._magiDot == null)
                _magiDot = spriteBatch.CreateFilledRectTexture(new Rectangle(0, 0, 2, 2), Color.Black);
            this.animations.Update(gameTime, deltaTime);
        }

        public void Draw(GameTime time)
        {
            // we assume that we don't need to sop and create a new abtchj

            spriteBatch.Draw(currentTexture, this.CurrentPosition, animations.CurrentFrame(), Color.White);
            spriteBatch.Draw(_magiDot, this.head.TopLeft, Color.White);
        }

        public void MoveLeft()
        {
            this.velocityX = -44f;
            this.animations.MoveLeft();
            this.currentTexture = this.walkLeft;
        }

        public void MoveRight()
        {
            this.velocityX = +44f;
            this.animations.MoveRight();

            this.currentTexture = this.walkRight;

        }

        public void MoveUp()
        {
            this.velocityY = -44f;
            this.animations.MoveUp();

            this.currentTexture = this.walkLeft;

        }

        public void MoveDown()
        {
            this.velocityY = 44f;
            this.animations.MoveDown();

            this.currentTexture = this.walkRight;

        }

        public void Fire()
        {
            Console.WriteLine("Pew Pew");
        }

        public void DoubleClickFire()
        {
            Console.WriteLine("Pew Pew");
        }

        public void Standing()
        {
            this.currentTexture = standing;
            this.velocityX = 0f;
            this.velocityY = 0f;
            this.animations.Standing();
        }

        public void EndMoveLeft()
        {
            if(velocityX<0)
            this.velocityX = 0f;
        }

        public void EndMoveRight()
        {
            if(velocityX>0)
                this.velocityX = 0f;
        }

        public void EndMoveDown()
        {
            if(velocityY>0)
            this.velocityY = 0f;
        }

        public void EndMoveUp()
        {
            if(velocityY<0)
            this.velocityY = 0f;
        }
    }
}
