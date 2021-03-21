using GameLibrary.InputManagement;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameData.UserInput
{
    public static class MapConfigToControls
    {
        public static PlayerKeyboardControls MapKeyboard(IDictionary<string, string> config)
        {
            return new PlayerKeyboardControls
            {
                Up = Enum.Parse<Keys>(config["Up"]),
                Down = Enum.Parse<Keys>(config["Down"]),
                Left = Enum.Parse<Keys>(config["Left"]),
                Right = Enum.Parse<Keys>(config["Right"]),
                Fire = Enum.Parse<Keys>(config["Fire"]),
                SecondFire = Enum.Parse<Keys>(config["Action"]),
                Special = Enum.Parse<Keys>(config["Special"])
            };
        }

        public static Dictionary<string, object> Map(Dictionary<string, string> config)
        {
            return config.ToList().ToDictionary(a => a.Key, a => Enum.TryParse<MouseButton>(a.Value, out var btn) ? (object)btn : (object)Enum.Parse<Keys>(a.Value));
        }

    }
}
