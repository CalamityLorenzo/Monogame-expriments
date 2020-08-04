using GameLibrary.Config.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BasicJeep
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
                            .LoadJsonFile("Config/opts.json")
                            .LoadJsonFile("Config/opts2.json")
                            .LoadJsonFile("Config/BasicJeepData.json")
                            .Build();

            using (var game = new BasicJeepGame(configData))
                game.Run();
        }
    }
}
