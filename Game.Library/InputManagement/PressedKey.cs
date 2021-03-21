using System.Globalization;
using Microsoft.Xna.Framework.Input;

namespace GameLibrary.InputManagement
{
    /// <summary>
    /// State of a pressed key
    /// </summary>
    public struct PressedKey
    {
        public Keys Key { get; set; }
        public bool IsDoubleClick { get; set; }
        public float DurationPressed { get; set; }
        public override string ToString()
        {
            return $"{Key} : {IsDoubleClick} \n ({DurationPressed.ToString("0.0000", CultureInfo.InvariantCulture)})";
        }
    }
}