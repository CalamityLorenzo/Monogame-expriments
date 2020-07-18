using SharpDX.XAudio2;

namespace PlayerCharacter.Character
{
    internal interface IWalkingAnimationState
    {
        public void WalkLeft();
        public void WalkRight();
        public void WalkUp();
        public void WalkDown();
        public void ClimbUp();
        public void ClimbDown();
        public void Standing();
    }
}