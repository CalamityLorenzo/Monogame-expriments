using GameLibrary.Config.App;
using GameLibrary.Configuration;
using System;

namespace Parrallax.Eightway
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
                             .AddJsonConverter(new Vector2Converter())
                             .Build();

            //using (var game = new MapsHost(configData))
            using (var game = new ParrallaxHost(configData))
                game.Run();
        }
    }
}
