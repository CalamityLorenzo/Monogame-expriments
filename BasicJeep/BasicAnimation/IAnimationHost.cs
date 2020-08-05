using Microsoft.Xna.Framework;

namespace BasicJeep.BasicAnimation
{
    /// <summary>
    /// Animation can be orchaestrated (more than one set of moving parts joined to each other)
    /// This is the consumer interface for the a client. They don't care  how just get the damn frames.
    /// </summary>
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