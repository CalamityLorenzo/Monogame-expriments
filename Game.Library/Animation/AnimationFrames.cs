using System.Collections.Generic;

namespace GameLibrary.Animation
{
    /// <summary>
    /// Simple list of frames as data.
    /// </summary>
    public class AnimationFrames
    {
        public AnimationFrames()
        {
            this.Frames = new List<AnimationFrame>();
        }
        public List<AnimationFrame> Frames { get; set; }
        public int StartFrame { get; set; } = 0;
        public bool IsRepeating { get; set; }
    }
}
