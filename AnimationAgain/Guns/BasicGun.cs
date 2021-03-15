using GameLibrary.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Policy;

namespace AnimationAgain.Guns
{
    internal class BasicGun
    {
        private readonly SpriteBatch spriteBatch;
        private readonly IDrawableGameObject bullet;
        private Vector2 currentPosition;
        private IList<BulletObject> FiredBullets { get; set; }
        public BulletFactory Factory { get; }
        public Vector2 CurrentPosition { get => currentPosition; }
        
        public BasicGun(SpriteBatch spriteBatch, BulletFactory factory, Vector2 startPos)
        {
            FiredBullets = new List<BulletObject>();
            this.spriteBatch = spriteBatch;
            Factory = factory;
            this.currentPosition = startPos;
        }

        public void Update(float deltaTime)
        {
            var bulletRemover = new BulletObject[FiredBullets.Count];
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

            for(var x=0;x<FiredBullets.Count;++x)
            {
                if (bulletRemover[x] == null) break;
                FiredBullets.Remove(bulletRemover[x]);
            };
        }

        public void SetPosition(Vector2 newPosition)
        {
            currentPosition = newPosition;
        }

        public void Fire(Vector2 direction)
        { 
            var bullet = this.Factory.CreateBullet(1);
            var magnitude = direction.Length();
            //var untiVe = new Vector2(direction.X > 0 ? direction.X / magnitude : 0,
            //                    direction.Y > 0 ? direction.Y / magnitude : 0);
            bullet.SetPosition(this.currentPosition.ToPoint());

            // Calculate the unit vector between the gun, and the destination vector, to be used as a direction
            var relativeVector = Vector2.Subtract(direction,this.currentPosition);
            relativeVector.Normalize();
                
            bullet.SetDirection(relativeVector);
            bullet.SetSpeed(88f);
            this.FiredBullets.Add(bullet);
        }

        public void Draw(GameTime gameTime)
        {
            foreach (var bullet in FiredBullets)
                bullet.Draw(gameTime);
        }
    }
}
