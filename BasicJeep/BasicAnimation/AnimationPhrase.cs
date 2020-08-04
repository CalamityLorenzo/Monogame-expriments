using System;
using System.Collections.Generic;
using System.Text;

namespace BasicJeep.BasicAnimation
{
    internal class AnimationPhrase
    {
        public List<AnimationFrame> Frames { get; set; }
        public int StartFrame { get; set; } = 0;
        public bool IsRepeating { get; set; }
    }
}
