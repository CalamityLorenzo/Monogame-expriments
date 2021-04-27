using GameLibrary.Animation;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Collisions.Objects.Balls
{
    class BallFactory
    {
        private readonly SpriteBatch spriteBatch;
        private readonly Dictionary<string, Texture2D> atlasi;
        private readonly AnimationPlayer player;

        public BallFactory(SpriteBatch spriteBatch, Dictionary<string, Texture2D> atlas, AnimationPlayer player)
        {
            this.spriteBatch = spriteBatch;
            this.atlasi = atlas;
            this.player = player;
        }

        public BaseBall NewBall(string BallType, Vector2 unitDirection, float initialSpeed, Point startPos )
        {
            return new BaseBall(this.spriteBatch, atlasi[BallType],this.player, startPos, unitDirection, initialSpeed);
        }
    }
}

