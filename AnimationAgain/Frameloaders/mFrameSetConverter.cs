using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AnimationAgain.Frameloaders
{
    internal class mFrameSetConverter : JsonConverter<Dictionary<string, Rectangle[]>>
    {
        public override Dictionary<string, Rectangle[]> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using (JsonDocument doc = JsonDocument.ParseValue(ref reader))
            {
                Dictionary<string, Rectangle[]> results = new Dictionary<string, Rectangle[]>();
                foreach (var frameSet in doc.RootElement.EnumerateArray())
                {
                    var name = frameSet.GetProperty("Name").GetString();
                    List<Rectangle> frames = new List<Rectangle>();
                    foreach (var frame in frameSet.GetProperty("frames").EnumerateArray())
                    {
                        var x = frame.GetProperty("X").GetInt32();
                        var y = frame.GetProperty("Y").GetInt32();
                        var width = frame.GetProperty("Width").GetInt32();
                        var height = frame.GetProperty("Height").GetInt32();
                        var length = frame.TryGetProperty("LengthOfFrame", out var lengthof) ? (float)lengthof.GetDouble() : 1;
                        frames.Add(new Rectangle(x, y, width, height));
                    }
                    results.Add(name,
                        frames.ToArray()
                        );
                }
                return results;
            }
        }

        public override void Write(Utf8JsonWriter writer, Dictionary<string, Rectangle[]> value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
