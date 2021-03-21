using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Character.Container.Character
{
    internal class BaseGun
    {
        private Vector2 currentPosition;
        private int currentBulletType;

        private IList<BaseBullet> FiredBullets { get; set; }
        public BulletFactory Factory { get; }
        public Vector2 CurrentPosition { get => currentPosition; }

        private Random rnd = new Random();

        public BaseGun(BulletFactory factory, Vector2 startPos)
        {
            FiredBullets = new List<BaseBullet>();
            Factory = factory;
            this.currentPosition = startPos;
            this.currentBulletType = 0;
        }

        public virtual void Update(float deltaTime)
        {
            var bulletRemover = new BaseBullet[FiredBullets.Count];
            var pos = 0;
            foreach (var bullet in FiredBullets)
            {
                // move each one along.
                bullet.Update(deltaTime);
                if (bullet.CurrentPosition.X > 1000 || bullet.CurrentPosition.Y > 1000)
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

        public void SetCurrentPosition(Vector2 newPosition)
        {
            currentPosition = newPosition;
        }

        public virtual void Fire(Vector2 direction)
        {
            var bullet = this.Factory.CreateBullet(rnd.Next(0, 3));
            var magnitude = direction.Length();
            bullet.SetCurrentPosition(this.currentPosition.ToPoint());
            // Calculate the unit vector between the gun, and the destination vector, to be used as a direction
            var relativeVector = Vector2.Subtract(direction, this.currentPosition);
            relativeVector.Normalize();
            Debug.WriteLine(relativeVector);
            Debug.WriteLine(this.currentPosition.ToPoint());
            bullet.SetDirection(relativeVector);

            bullet.SetSpeed(rnd.Next(95, 130));
            this.FiredBullets.Add(bullet);
        }

        public void Draw(GameTime gameTime)
        {
            foreach (var bullet in FiredBullets)
                bullet.Draw(gameTime);
        }

        internal void SetBullet(int currentBullet)
        {
            this.currentBulletType = currentBullet;
        }
    }
}
