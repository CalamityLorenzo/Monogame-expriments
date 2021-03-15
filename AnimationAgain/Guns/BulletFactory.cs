using GameLibrary.Animation;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace AnimationAgain.Guns
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

        public BulletObject CreateBullet(int type)
        {
            return new BulletObject(spriteBatch, atlas[type], player);
        }
    }
}
