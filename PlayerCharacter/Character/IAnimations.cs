using Microsoft.Xna.Framework;

namespace PlayerCharacter.Character
{
    internal interface IAnimations
    {
        Rectangle CurrentFrame();
        void Update(GameTime gameTime, float delta);
    }
}