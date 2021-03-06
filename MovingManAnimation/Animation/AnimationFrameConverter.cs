﻿using GameLibrary.Animation;
using Microsoft.Xna.Framework;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GameLibrary.Config.App
{
    /// <summary>
    /// Used to import json on animation frames (Mainly the sodding rectangles)
    /// </summary>
    internal class AnimationFrameConverter : JsonConverter<AnimationFrame>
    {
        public AnimationFrameConverter()
        {
        }

        public override AnimationFrame Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            
            using(JsonDocument doc = JsonDocument.ParseValue(ref reader))
            {
                var frame = doc.RootElement.GetProperty("Frame");
                var x= frame.GetProperty("X").GetInt32();
                var y= frame.GetProperty("Y").GetInt32();
                var width= frame.GetProperty("Width").GetInt32();
                var height= frame.GetProperty("Height").GetInt32();
                var length = frame.TryGetProperty("LengthOfFrame", out var lengthof) ? (float) lengthof.GetDouble() : 1;
                return new AnimationFrame { Frame = new Rectangle(x, y, width, height), LengthOfFrameMultiplier = length };
            }
        }

        public override void Write(Utf8JsonWriter writer, AnimationFrame value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
