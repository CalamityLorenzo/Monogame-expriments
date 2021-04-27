using Microsoft.Xna.Framework;

namespace Collisions.Objects.Guns
{
    internal class LazerGun : BaseGun
    {
        private readonly float bulletSpeed;

        public LazerGun(BulletFactory factory, Point startPos, Rectangle startArea, float bulletSpeed ) : base(factory, startPos)
        {
            this.Area = startArea;
            this.bulletSpeed = bulletSpeed;
        }

        public override void Fire(Vector2 direction)
        {
            // The gun has the same matching area as the paddle
            // we are createing two bullets at either end of the Rectnagle.
            var bulletl = this.Factory.CreateBullet(0, bulletSpeed, direction);
            var bulletr = this.Factory.CreateBullet(0, bulletSpeed, direction);
            var lPos = new Point(this.CurrentPosition.X + 25, this.CurrentPosition.Y - bulletl.Area.Height);
            var rPos = new Point(this.CurrentPosition.X + (this.Area.Width - bulletr.Area.Width) - 50, this.CurrentPosition.Y - bulletl.Area.Height);
            bulletl.SetCurrentPosition(lPos);
            bulletr.SetCurrentPosition(rPos);
            this.FiredBullets.Add(bulletl);
            this.FiredBullets.Add(bulletr);
        }

    }
}
