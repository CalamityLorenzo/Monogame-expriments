using System;

namespace InputTests
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            using (var game = new MovingObjectGame()) // ProcessedRockGAme()) // MovingObjectGame()) 
                game.Run();
        }
    }
}
