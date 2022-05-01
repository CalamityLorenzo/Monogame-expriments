using GameData.CharacterActions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Windows.Forms;

namespace Collisions.Objects
{
    public class MinorRectPlayersObject : ICharacterActions
    {
        private readonly SpriteBatch spriteBatch;
        private readonly Texture2D teture;
        private Rectangle minorRect;
        public Vector2 Velocity { get; private set; } // Distance moved per frame over each axis

        private Vector2 m_velocity;
        private Vector2 VelocitySpeed;
        

        public bool Leftin { get; private set; }
        public bool Rightin { get; private set; }
        public bool Upsie { get; private set; }
        public bool Downsie { get; private set; }

        public Rectangle Area => minorRect;
        private Rectangle CenterRect;
        public MinorRectPlayersObject(SpriteBatch spriteBatch, Texture2D teture, Rectangle go, Vector2 veloocity)
        {
            this.spriteBatch = spriteBatch;
            this.teture = teture;
            this.minorRect = go;
            this.m_velocity = veloocity;
            this.VelocitySpeed = veloocity;
        }

        public void Update(float deltaTime)
        {
            var location = this.minorRect.Location.ToVector2();
            // This is added to location, every frame.
            // Take the current Angle, per axid and mul
            this.Velocity = new Vector2(this.m_velocity.X * deltaTime, m_velocity.Y * deltaTime);
            location += this.Velocity;

            this.minorRect = new Rectangle(location.ToPoint(), minorRect.Size);

            this.CenterRect = new Rectangle(new Point((this.minorRect.X + minorRect.Width / 2) - 2, (this.minorRect.Y + minorRect.Height / 2) - 2), new Point(3, 3));

        }

        public void SetDirection(Vector2 newDirection)
        {
            this.m_velocity = newDirection;
        }

        public Vector2 GetDirection() => this.m_velocity;




        public void Draw(GameTime gameTime)
        {
            this.spriteBatch.Draw(teture, this.minorRect, Color.DarkBlue);
            this.spriteBatch.Draw(teture, this.CenterRect, Color.White);
        }

        public void MoveLeft()
        {
            this.Leftin = true;
            this.m_velocity.X = (-this.VelocitySpeed.X);
        }

        public void MoveRight()
        {
            this.Rightin = true;
            this.m_velocity.X = (this.VelocitySpeed.X);
        }

        public void MoveUp()
        {
            this.Upsie = true;
            this.m_velocity.Y = (-this.VelocitySpeed.Y);
        }

        public void MoveDown()
        {
            this.Downsie = true;
            this.m_velocity.Y = (this.VelocitySpeed.Y);
        }

        public void EndMoveLeft()
        {
            this.Leftin = false;
            if (this.Rightin == false)
                this.m_velocity.X = (0f);
        }

        public void EndMoveRight()
        {
            this.Rightin = false;
            if (this.Leftin == false)
                this.m_velocity.X = (0f);
        }

        public void EndMoveDown()
        {
            this.Downsie = false;
            if (this.Upsie == false)
                this.m_velocity.Y = (0f);
        }

        public void EndMoveUp()
        {
            this.Upsie = false;
            if (this.Downsie == false)
                this.m_velocity.Y = (0f);
        }

        public void Fire()
        {

        }

        public void Action()
        {

        }
        public void EndAction()
        {

        }

        public void FireSpecial()
        {

        }

        public void EndFireSpecial()
        {

        }

        public void Jump()
        {

        }


        public void Standing()
        {

        }
    }
}
