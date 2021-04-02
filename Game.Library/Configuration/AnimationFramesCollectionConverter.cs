using GameLibrary.Animation;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GameLibrary.Configuration
{
    public class AnimationFramesCollectionConverter : JsonConverter<IEnumerable<AnimationFramesCollection>>
    {
        public override IEnumerable<AnimationFramesCollection> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using (JsonDocument doc = JsonDocument.ParseValue(ref reader))
            {
                List<AnimationFramesCollection> results = new List<AnimationFramesCollection>();
                foreach (var frameSet in doc.RootElement.EnumerateArray())
                {
                    var name = frameSet.GetProperty("Name").GetString();
                    var isRepeating = frameSet.GetProperty("isRepeating").GetBoolean();
                    var startFrame = frameSet.GetProperty("startFrame").GetInt32();

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

                    results.Add(new AnimationFramesCollection(name, isRepeating, startFrame, frames.ToArray()));
                }
                return results;
            }
        }

        public override void Write(Utf8JsonWriter writer, IEnumerable<AnimationFramesCollection> value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
