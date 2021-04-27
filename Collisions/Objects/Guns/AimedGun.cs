using GameData.CharacterActions;
using GameLibrary.AppObjects;
using GameLibrary.Extensions;
using GameLibrary.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Collisions.Objects.Guns
{
    internal class AimedGun : BaseGun, ICharacterActions
    {
        private readonly SpriteBatch spriteBatch;
        private readonly Rotator rotator;
        private readonly GameAgentObject hostObject;
        private readonly BallContainer balls;

        public AimedGun(BulletFactory factory, Point startPos, SpriteBatch spriteBatch, Rotator rotator, GameAgentObject hostObject, BallContainer balls) : base(factory, startPos)
        {
            this.spriteBatch = spriteBatch;
            this.rotator = rotator;
            this.hostObject = hostObject;
            this.balls = balls;
        }

        public override void Update(float deltaTime, World TheState)
        {
            this.SetCurrentPosition(hostObject.CurrentPosition);
            this.rotator.Update(deltaTime);
            base.Update(deltaTime, TheState);
        }

        public override void Draw(GameTime gameTime)
        {
            // WE draw a !stick! showing currently selected the angle
            var pos = new Point(this.CurrentPosition.X + (this.hostObject.Area.Width / 2), this.CurrentPosition.Y + (this.hostObject.Area.Height / 2));

            this.spriteBatch.DrawLine(pos.ToVector2(), 100, this.rotator.CurrentAngle, Color.White);

        }

        public void MoveLeft()
        {
        }

        public void MoveRight()
        {
        }

        public void EndMoveLeft()
        {
        }

        public void EndMoveRight()
        {
        }

        public void Fire()
        {
           
        }

        public void Action()
        {
            // Left
            rotator.Left();
        }

        public void EndAction()
        {
            rotator.StopRotation();
        }

        public void FireSpecial()
        {
            //Riht
            rotator.Right();

        }

        public void EndFireSpecial()
        {
            rotator.StopRotation();
        }


        public void EndMoveDown()
        {

        }

        public void EndMoveUp()
        {

        }
        public void MoveUp()
        {

        }

        public void MoveDown()
        {

        }

        public void Jump()
        {
            var pos = new Point(this.CurrentPosition.X + (this.hostObject.Area.Width / 2), this.CurrentPosition.Y - 16);
            this.balls.CreateBasicBall(GeneralExtensions.UnitVectorFromDegrees(this.rotator.CurrentAngle), 330f, pos);
        }

        public void Standing()
        {

        }
    }
}
