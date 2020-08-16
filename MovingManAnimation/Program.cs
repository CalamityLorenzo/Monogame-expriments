using GameLibrary.Config.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MovingManAnimation
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
                             .LoadJsonFile("config/opts.json")
                             .LoadJsonFile("config/opts2.json")
                             .LoadJsonFile("config/MovingMan.json")
                             .Build();
            using (var game = new MovingManGame(configData))
                game.Run();
        }
    }
}
