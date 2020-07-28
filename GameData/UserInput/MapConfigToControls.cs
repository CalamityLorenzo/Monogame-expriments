using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameData.UserInput
{
    public static class MapConfigToControls
    {
        public static PlayerKeyboardControls Map(IDictionary<string, string> config)
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
    }
}
