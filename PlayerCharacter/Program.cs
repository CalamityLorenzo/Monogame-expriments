using GameLibrary.Config.App;
using System;

namespace PlayerCharacter
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var configData = ConfigurationBuilder.Manager
                 .LoadJsonFile("opts.json")
                 .LoadJsonFile("opts2.json")
                 .Build();

            using (var game = new PlayerCharacterGame(configData))
            {
                game.Run();
            }
        }
    }
}
