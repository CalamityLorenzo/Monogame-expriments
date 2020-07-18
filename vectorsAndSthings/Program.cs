using Microsoft.Xna.Framework;
using System;

namespace vectorsAndSthings
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {

            var startV = new Vector2(10, 10);
            var endV = new Vector2(0, 9);


            CalculateAngle(startV, endV);
        }

        private static void CalculateAngle(Vector2 start, Vector2 end)
        {
            var theVector = Vector2.Subtract(end, start);// new Vector2(end.X - start.X, end.Y - start.Y);
            var unit = Vector2.Normalize(theVector);
            
            var radians = Math.Atan2(end.Y - start.Y, end.X-start.X);
            Console.WriteLine(radians);

            var angleDeg = radians * (180 /3.14159);
            Console.WriteLine(angleDeg);

        }
    }
}
