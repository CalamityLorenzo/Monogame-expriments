using Microsoft.Xna.Framework;

namespace BasicJeep.BasicAnimation
{
    internal interface IAnimationHost
    {
        bool IsRepeating { get; }
        Rectangle CurrentFrame();
        int CurrentFrameIndex();
        void Start(int? frameId);
        void Stop();
        void Update(float deltaTime);
    }
}