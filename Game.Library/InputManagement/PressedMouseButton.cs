
using System.Globalization;

namespace GameLibrary.InputManagement
{
    public struct PressedMouseButton
    {
        public MouseButton Button { get; set; }
        public bool IsDoubleClick { get; set; }
        public float DurationPressed { get; set; }
        public override string ToString()
        {
            return $"{Button} : {IsDoubleClick} \n ({DurationPressed.ToString("0.0000", CultureInfo.InvariantCulture)})";
        }
    }
}