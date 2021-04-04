using GameLibrary.Animation;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace Collisions.Objects
{
    internal class BulletFactory
    {
        private readonly SpriteBatch spriteBatch;
        private readonly AnimationPlayer player;
        private readonly IList<Texture2D> atlas;

        public BulletFactory(SpriteBatch spriteBatch, IEnumerable<Texture2D> atlas, AnimationPlayer player)
        {
            this.spriteBatch = spriteBatch;
            this.player = player;
            this.atlas = atlas.ToList();
        }

        public BaseBullet CreateBullet(int type)
        {
            return new BaseBullet(spriteBatch, atlas[type], player);
        }

        public BaseBullet CreateBullet(int type, float speed, Vector2 direction)
        {
            var bullet = new BaseBullet(spriteBatch, atlas[type], player); ;
            bullet.SetSpeed(speed);
            bullet.SetDirection(direction);
            return bullet;

        }
    }
}
