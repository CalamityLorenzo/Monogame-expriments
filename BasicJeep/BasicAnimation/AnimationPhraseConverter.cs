using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BasicJeep.BasicAnimation
{
    class AnimationPhraseConverter : JsonConverter<AnimationPhraseHost>
    {
        public override AnimationPhraseHost Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using (JsonDocument document = JsonDocument.ParseValue(ref reader))
            {
                var root = document.RootElement;
                var frames = JsonSerializer.Deserialize<IEnumerable<AnimationFrame>>(root.GetProperty("frames").GetString());
                var isRepeatable = root.TryGetProperty("isRepeatable", out var repeating) ? JsonSerializer.Deserialize<Boolean>(repeating.GetString()) : false;
                var sFrame = root.TryGetProperty("startFrame ", out var startFrame) ? JsonSerializer.Deserialize<int>(repeating.GetString()) : 0;
                return new AnimationPhraseHost(frames, isRepeatable, sFrame);
            }
        }

        public override void Write(Utf8JsonWriter writer, AnimationPhraseHost value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
