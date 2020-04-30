using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace InputTests
{
    public class CrossHairs
    {
        private readonly SpriteBatch spriteBatch;
        private readonly Texture2D crossHairTexture;
        private Vector2 currentPosition;
        private readonly Rectangle range;

        public CrossHairs(SpriteBatch spriteBatch, Texture2D crossHairTexture, Vector2 Startpos, Rectangle range )
        {
            this.spriteBatch = spriteBatch;
            this.crossHairTexture = crossHairTexture;
            SetCurrentPosition(Startpos);
            this.range = range;
        }

        public void SetPosition(Vector2 mousePos)
        {
            var intX = Convert.ToInt32(mousePos.X);
            var intY = Convert.ToInt32(mousePos.Y);

            if (range.Contains(mousePos))
            {
                SetCurrentPosition(mousePos);
            }
            else
            {
                //if ((range.Y + range.Height <= mousePos.Y) && range.X + range.Width >= mousePos.X)
                //{
                //    SetCurrentPosition(new Vector2(this.currentPosition.Y, mousePos.X));
                //    return;
                //}
                //else if (range.X + range.Width <= mousePos.X && range.Y + range.Height >= mousePos.Y)
                //{
                //    SetCurrentPosition(new Vector2(mousePos.Y, currentPosition.X));
                //    return;
                //}
            }
        }

        private void SetCurrentPosition(Vector2 mousePos)
        {
            // The mouseposition is the centre of the cross hairs. soooooooo we make an offset
            var currentPosOffset = Vector2.Subtract(mousePos, new Vector2((float)crossHairTexture.Width / 2, (float)crossHairTexture.Height / 2));
            currentPosition = currentPosOffset;
        }

        public void Update(GameTime gameTime, float delta)
        {
            // Nothing to see here, or indeed hear here.
        }

        public void Draw(GameTime gameTime)
        {
            spriteBatch.Draw(crossHairTexture, currentPosition, Color.Black);
        }

    }
}
