using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace BasicJeep.BasicAnimation
{ 
    [JsonConverter(typeof(AnimationFrameConverter))]
    internal class AnimationFrame
    {
        /// <summary>
        /// Location on the atlas for the frame.
        /// </summary>
        public Rectangle Frame { get; set; }
        /// <summary>
        /// A multiplier, generally this is (1) = The length of time the frame is displyed = 1 unit of time.
        /// So 200 ms per frame = =1, then 0.5 would be 200ms*0.5 length of time a frame is displayed.
        /// </summary>
        public float LengthOfFrame { get; set; } 
    }
}
