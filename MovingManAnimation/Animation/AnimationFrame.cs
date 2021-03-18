using GameLibrary.Config.App;
using Microsoft.Xna.Framework;
using System.Text.Json.Serialization;

namespace GameLibrary.Animation
{
    [JsonConverter(typeof(AnimationFrameConverter))]
    public class AnimationFrame
    {
        /// <summary>
        /// Location on the atlas for the frame.
        /// </summary>
        public Rectangle Frame { get; set; }
       
        /// <summary>
        /// A multiplier, generally this is (1) = The length of time the frame is displyed = 1 unit of time.
        /// So 200 ms per frame = =1, then 0.5 would be 200ms*0.5 length of time a frame is displayed.
        /// a frame can now have a distinct length of time applied.
        /// (This seemed like a goodidea at the time...I don't think it actually is anymore.
        /// </summary>
        public float LengthOfFrameMultiplier { get; set; }

        public override string ToString()
        {
            return $"{Frame.ToString()} FrameLength:{LengthOfFrameMultiplier}";
        }
    }
}
