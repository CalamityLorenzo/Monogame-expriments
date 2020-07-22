using System.Globalization;
using Microsoft.Xna.Framework.Input;

namespace GameLibrary.InputManagement
{
    public class PressedKey
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