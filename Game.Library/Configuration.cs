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

        public string Get(string propertyName)
        {
            var itm = this.ConfigData.Select(o => o.RootElement).First(p => p.TryGetProperty(propertyName, out var kim));
            return itm.GetProperty(propertyName).ToString();
        }

        public T Get<T>(string propertyName, Func<string, T> mapFunc=null) where T : class
        {
            var matchedObject = this.ConfigData.Select(o => o.RootElement).First(p => p.TryGetProperty(propertyName, out var kim));
            var map = mapFunc??  new Func<string, T>((str)=> JsonSerializer.Deserialize<T>(matchedObject.GetProperty(propertyName).GetRawText()));
            return map(propertyName);
            // sreturn JsonSerializer.Deserialize<T>(matchedObject.GetProperty(propertyName).GetRawText());
        }

        // Simple Case leaning on Json.net
        public T Get<T>(string propertyName) where T : class{
            var itm = this.ConfigData.Where(o => o.RootElement.TryGetProperty(propertyName, out var prop)).First();
            var property = itm.RootElement.GetProperty(propertyName);
            var opts = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            return JsonSerializer.Deserialize<T>(property.ToString(),opts);
        }
    }
    public class ConfigurationBuilder
    {
        private static Lazy<ConfigurationBuilder> configInstance = new Lazy<ConfigurationBuilder>(() => new ConfigurationBuilder());
        private ConfigurationBuilder() { }
        private bool BuildComplete = false;
        public static ConfigurationBuilder Manager => configInstance.Value;

        private HashSet<string> fileNames = new HashSet<string>();
        private List<JsonDocument> LoadedData = new List<JsonDocument>();

        public ConfigurationBuilder LoadJsonFile(string jsonfilePath)
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
