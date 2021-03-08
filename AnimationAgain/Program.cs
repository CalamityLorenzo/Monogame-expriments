using GameLibrary.Config.App;
using System;

namespace AnimationAgain
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {

            var configData = ConfigurationBuilder.Manager
                             .LoadJsonFile("config/opts.json")
                             .LoadJsonFile("config/opts2.json")
                             .LoadJsonFile("config/MovingMan.json")
                             .Build();

            using (var game = new Game1(configData))
                game.Run();
        }
    }
}
