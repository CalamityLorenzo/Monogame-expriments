using GameLibrary.AppObjects;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GameLibrary.Configuration
{
    public class DimensionsConverter : JsonConverter<Dimensions>
    {
        public override Dimensions Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using (JsonDocument doc = JsonDocument.ParseValue(ref reader))
            {
                var width = doc.RootElement.GetProperty("Width").GetInt32();
                var height = doc.RootElement.GetProperty("Height").GetInt32();
                return new Dimensions(width, height);
            }

        }

        public override void Write(Utf8JsonWriter writer, Dimensions value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
