using GameLibrary.AppObjects;
using GameLibrary.GameObjects;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Collisions.Objects
{
    internal class BaseGun : GameAgentObject
    {
        public IList<BaseBullet> FiredBullets { get; set; }
        public BulletFactory Factory { get; }
        public BaseGun(BulletFactory factory, Point startPos) : base(startPos)
        {
            FiredBullets = new List<BaseBullet>();
            Factory = factory;
        }

        public override void Update(float deltaTime, World TheState)
        {
            var bulletRemover = new BaseBullet[FiredBullets.Count];
            var pos = 0;
            foreach (var bullet in FiredBullets)
            {
                // move each one along.
                bullet.Update(deltaTime);


                if(bullet.IsStruck)
                {
                    bulletRemover[pos] = bullet; 
                    pos += 1;
                }
                if (TheState.MapCollision(bullet.Area) != null)
                {
                    bulletRemover[pos] = bullet;
                    pos += 1;
                }
                else if (bullet.CurrentPosition.X > 1000 || bullet.CurrentPosition.Y > 1000)
                {
                    bulletRemover[pos] = bullet;
                    pos += 1;
                }
            }

            for (var x = 0; x < FiredBullets.Count; ++x)
            {
                if (bulletRemover[x] == null) break;
                FiredBullets.Remove(bulletRemover[x]);
            };
        }

        public virtual void Fire(Vector2 direction)
        {
            ;
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (var bullet in FiredBullets)
                bullet.Draw(gameTime);
        }

        internal void SetBullet(int currentBullet)
        {
            // this.currentBulletType = currentBullet;
        }

        public void SetArea(Rectangle area) { if (area != this.Area) Area = area; }
    }
}
