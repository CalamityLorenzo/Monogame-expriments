using System.Text.Json;
using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary.Config.App
{
    public class ConfigurationData
    {
        private List<JsonDocument> ConfigData;
        internal ConfigurationData(List<JsonDocument> configData)
        {
            ConfigData = new List<JsonDocument>(configData);
        }

        public T ToResultType<T>(string propertyName, Func<string, T> mapFunc) where T : class
        {
            var matchedObject = this.ConfigData.Select(o => o.RootElement).First(p => p.TryGetProperty(propertyName, out var kim));
            return JsonSerializer.Deserialize<T>(matchedObject.GetProperty(propertyName).GetRawText());
        }

        // Simple Case leaning on Json.net
        public T ToResultType<T>(string propertyName) where T : class
        {
            return this.ToResultType<T>(propertyName, (str) => JsonSerializer.Deserialize<T>(str));
        }
    }
    public class Configuration
    {
        private static Lazy<Configuration> configInstance = new Lazy<Configuration>(() => new Configuration());
        private Configuration() { }
        private bool BuildComplete = false;
        public static Configuration Manager => configInstance.Value;

        private HashSet<string> fileNames = new HashSet<string>();
        private List<JsonDocument> LoadedData = new List<JsonDocument>();

        public Configuration LoadJsonFile(string jsonfilePath)
        {
            fileNames.Add(jsonfilePath);
            return this;
        }


        public ConfigurationData Build()
        {
            if (!BuildComplete)
            {
                var items = fileNames.Select(fn => JsonDocument.Parse(File.ReadAllText(fn)));
                LoadedData.AddRange(items);
                BuildComplete = true;
                return new ConfigurationData(LoadedData);
            }
            throw new ArgumentOutOfRangeException("Build has already been completed.");
        }

    }
}
