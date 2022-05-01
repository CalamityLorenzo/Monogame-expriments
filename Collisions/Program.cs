using Collisions;
using GameLibrary.Config.App;
using GameLibrary.Configuration;
using System;

namespace CollisionsGame
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            var configData = ConfigurationBuilder.Manager
                   .LoadJsonFile("Config/opts.json")
                   .LoadJsonFile("Config/opts2.json")
                   .LoadJsonFile("Config/ball.json")
                   .AddJsonConverter(new DimensionsConverter())
                   .AddJsonConverter(new AnimationFramesCollectionConverter())
                   .Build();

            using (var game = new CollisionsGame(configData))
                //using (var game = new RectHitGame(configData))
                game.Run();
        }
    }
}
